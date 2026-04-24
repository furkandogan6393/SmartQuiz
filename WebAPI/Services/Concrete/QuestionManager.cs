using Core.Entities.Concrete;
using Core.Utilities.Results;
using WebAPI.DataAccess.Abstract;
using WebAPI.Services.Abstract;

namespace WebAPI.Services.Concrete
{
    public class QuestionManager : IQuestionService
    {

        readonly IQuestionDal _questionDal;
        public QuestionManager(IQuestionDal questionDal)
        {
            _questionDal = questionDal;
        }
        public async Task<IDataResult<Question>> AddAsync(Question question)
        {
            if(question==null)
                return new ErrorDataResult<Question>("Testin İçi Boş");

            await _questionDal.Add(question);
            return new SuccessDataResult<Question>(question, "Başarıyla Eklendi");

        }

        public async Task<IDataResult<Question>> DeleteAsync(int id)
        {
            var deletedQuiz = await _questionDal.Get(q => q.Id == id);
            if (deletedQuiz == null)
                return new ErrorDataResult<Question>("Sınavın İçi Boş"); 
            await _questionDal.Delete(deletedQuiz);
            return new SuccessDataResult<Question>(deletedQuiz, "Başarıyla Silindi");
        }

        public async Task<IDataResult<List<Question>>> GetAllAsync()
        {
            var allQuestion = await _questionDal.GetAll();
            if (allQuestion == null)
                return new ErrorDataResult<List<Question>>(null, "Liste Boş");
            return new SuccessDataResult<List<Question>>(allQuestion, "Sorular Başarıyla Getirildi");
        }

        public async Task<IDataResult<List<Question>>> GetByQuizIdAsync(int quizId)
        {
            var quizbyId = await _questionDal.GetQuestionsByQuizIdAsync(quizId);
            if (quizbyId == null)
                return new ErrorDataResult<List<Question>>(null, "Testin İçi boş");
            return new SuccessDataResult<List<Question>>(quizbyId, "Sorular Getirildi");
        }

        public async Task<IDataResult<Question>> UpdateAsync(Question question)
        {
            var updatedQuestion = await _questionDal.Get(q => q.Id == question.Id);
            if (updatedQuestion == null)
                return new ErrorDataResult<Question>(null, "İçi Boş");
            await _questionDal.Update(question);
            return new SuccessDataResult<Question>("Veri Güncellendi");
        }
    }
}
