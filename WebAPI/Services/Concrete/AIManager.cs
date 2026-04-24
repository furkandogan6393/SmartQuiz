using Core.Entities.Dto;
using Core.Utilities.Results;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;
using UglyToad.PdfPig;
using WebAPI.DataAccess.Abstract;
using WebAPI.Services.Abstract;

namespace WebAPI.Services.Concrete
{
    public class AIManager : IAIService
    {
        readonly IDocumentDal _documentDal;
        readonly IConfiguration _configuration;

        public AIManager(IDocumentDal documentDal, IConfiguration configuration)
        {
            _documentDal = documentDal;
            _configuration = configuration;
        }

        public List<string> GetPromptTemplates()
        {
            return new List<string>
            {
                "Kolay sorular hazırla, temel kavramları sorgula",
                "Orta zorlukta sorular hazırla, kavramlar arası ilişkileri sorgula",
                "Zor sorular hazırla, derin anlayış ve analiz gerektiren sorular sor",
                "Her başlıktan en az bir soru olacak şekilde kolay sorular hazırla",
                "Her başlıktan en az bir soru olacak şekilde orta zorlukta sorular hazırla",
                "Her başlıktan en az bir soru olacak şekilde zor sorular hazırla",
                "Yaratıcı ve analitik düşünme gerektiren zor sorular hazırla",
                "Tüm konuları kapsayan karma zorlukta sorular hazırla"
            };
        }

        public async Task<IDataResult<string>> GenerateQuizAsync(AIQuizRequest request)
        {
            var pdfText = new StringBuilder();

            foreach (var docId in request.DocumentIds)
            {
                var document = await _documentDal.Get(d => d.Id == docId);
                if (document == null) continue;

                var extension = Path.GetExtension(document.FilePath).ToLower();

                if (extension == ".pdf")
                {
                    using var pdf = PdfDocument.Open(document.FilePath);
                    foreach (var page in pdf.GetPages())
                        pdfText.Append(page.Text);
                }
                else if (extension == ".pptx")
                {
                    using var pptx = PresentationDocument.Open(document.FilePath, false);
                    var slides = pptx.PresentationPart?.SlideParts;
                    if (slides != null)
                    {
                        foreach (var slide in slides)
                        {
                            var texts = slide.Slide.Descendants<DocumentFormat.OpenXml.Drawing.Text>();
                            foreach (var t in texts)
                                pdfText.Append(t.Text + " ");
                        }
                    }
                }
                else if (extension == ".docx")
                {
                    using var docx = WordprocessingDocument.Open(document.FilePath, false);
                    var texts = docx.MainDocumentPart?.Document.Body?.Descendants<Text>();
                    if (texts != null)
                        foreach (var t in texts)
                            pdfText.Append(t.Text + " ");
                }

                pdfText.AppendLine();
            }

            var promptText = !string.IsNullOrEmpty(request.DifficultyTemplate)
                ? request.DifficultyTemplate
                : request.Prompt;

            var prompt = $@"Aşağıdaki içeriği oku ve şu kurallara göre sınav hazırla:
{promptText}
Soru sayısı: {request.QuestionCount}
Şık sayısı: {request.OptionCount}
İçerik:
{pdfText}
Soruları şu formatta yaz:
1. Soru metni
A) Şık
B) Şık
Doğru Cevap: A
Önce tüm soruları yaz, sonra 'CEVAP ANAHTARI' başlığı altında sadece cevapları listele.";

            var apiKey = _configuration["Gemini:ApiKey"];
            var baseUrl = _configuration["Gemini:BaseUrl"];

            var httpClient = new HttpClient();
            var requestBody = new
            {
                contents = new[]
                {
                    new { parts = new[] { new { text = prompt } } }
                }
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"{baseUrl}?key={apiKey}", content);
            var responseString = await response.Content.ReadAsStringAsync();

            var parsed = JsonDocument.Parse(responseString);
            var responseText = parsed.RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString();

            return new SuccessDataResult<string>(responseText, "Sınav Oluşturuldu");
        }

        public async Task<IDataResult<byte[]>> GenerateQuizWordAsync(AIQuizRequest request)
        {
            var quizResult = await GenerateQuizAsync(request);
            if (!quizResult.Success)
                return new ErrorDataResult<byte[]>(quizResult.Message);

            var fullText = quizResult.Data;
            var parts = fullText.Split("CEVAP ANAHTARI", StringSplitOptions.None);
            var questionsText = parts[0].Trim();
            var answersText = parts.Length > 1 ? parts[1].Trim() : "";

            var questionLines = questionsText.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var answerLines = answersText.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            using var memoryStream = new MemoryStream();
            using (var wordDoc = WordprocessingDocument.Create(memoryStream, WordprocessingDocumentType.Document, true))
            {
                var mainPart = wordDoc.AddMainDocumentPart();
                mainPart.Document = new Document(new Body());
                var body = mainPart.Document.Body;

                body.AppendChild(new Paragraph(
                    new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
                    new Run(new RunProperties(new Bold(), new FontSize { Val = "32" }),
                        new Text("SINAV SORULARI"))
                ));

                body.AppendChild(new Paragraph(new Run(new Text(""))));

                foreach (var line in questionLines)
                {
                    var trimmed = line.Trim();
                    if (string.IsNullOrWhiteSpace(trimmed)) continue;

                    var para = new Paragraph();
                    var run = new Run();

                    if (System.Text.RegularExpressions.Regex.IsMatch(trimmed, @"^\d+\."))
                    {
                        run.AppendChild(new RunProperties(new Bold()));
                        body.AppendChild(new Paragraph(new Run(new Text(""))));
                    }

                    run.AppendChild(new Text(trimmed) { Space = SpaceProcessingModeValues.Preserve });
                    para.AppendChild(run);
                    body.AppendChild(para);
                }

                body.AppendChild(new Paragraph(
                    new Run(new Break { Type = BreakValues.Page })
                ));

                body.AppendChild(new Paragraph(
                    new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
                    new Run(new RunProperties(new Bold(), new FontSize { Val = "32" }),
                        new Text("CEVAP ANAHTARI"))
                ));

                body.AppendChild(new Paragraph(new Run(new Text(""))));

                foreach (var line in answerLines)
                {
                    var trimmed = line.Trim();
                    if (string.IsNullOrWhiteSpace(trimmed)) continue;

                    body.AppendChild(new Paragraph(
                        new Run(new Text(trimmed) { Space = SpaceProcessingModeValues.Preserve })
                    ));
                }

                mainPart.Document.Save();
            }

            return new SuccessDataResult<byte[]>(memoryStream.ToArray(), "Word dosyası oluşturuldu");
        }
    }
}