using Core.Entities.Concrete;
using Core.Utilities.Results;
using WebAPI.DataAccess.Abstract;
using WebAPI.Services.Abstract;

namespace WebAPI.Services.Concrete
{
    public class QuizManager : IQuizService
    {
        readonly IQuizDal _quizDal;
        public QuizManager(IQuizDal quizDal)
        {
            _quizDal = quizDal;
        }

        public async Task<IDataResult<Quiz>> AddAsync(Quiz quiz)
        {
            if(quiz == null)
            {
                return new ErrorDataResult<Quiz>("quiz verisi boş olamaz.");
            }
            await _quizDal.Add(quiz);
            return new SuccessDataResult<Quiz>(quiz,"Başarıyla Eklendi");
        }

        public async Task<IDataResult<Quiz>> DeleteAsync(int id)
        {
            var deletedQuiz = await _quizDal.Get(q => q.Id == id);
            if (deletedQuiz == null)
            {
                return new ErrorDataResult<Quiz>(null, "Böyle Bir Sınav yok");
            }
            await _quizDal.Delete(deletedQuiz);
            return new SuccessDataResult<Quiz>(deletedQuiz,"Başarıyla Silindi");
            
        }

        public async Task<IDataResult<List<Quiz>>> GetAllAsync()
        {
            var quizzes  = await _quizDal.GetAll();
            if (!quizzes.Any())
                return new ErrorDataResult<List<Quiz>>("Henüz Sınav yok");
            return new SuccessDataResult<List<Quiz>>(quizzes, "Tüm veri getirildi.");
        }

        public async Task<IDataResult<Quiz>> GetByIdAsync(int id)
        {
            var quizById = await _quizDal.Get(q => q.Id == id);
            if(quizById == null)
            {
                return new ErrorDataResult<Quiz>(null, "Quiz Bulunamadı");
            }

            return new SuccessDataResult<Quiz>(quizById, "Tüm veri Getirildi");
        }

        public async Task<IDataResult<Quiz>> UpdateAsync(Quiz quiz)
        {
            var updateQuiz = await _quizDal.Get(q => q.Id ==  quiz.Id);
            if(updateQuiz == null)
                return new ErrorDataResult<Quiz>(null, "Güncellenecek Quiz Bulunamadı");
            
            await _quizDal.Update(quiz);
            return new SuccessDataResult<Quiz>(quiz, "Quiz Güncellendi");
        }
    }
}
