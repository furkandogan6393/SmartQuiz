using Core.Entities.Concrete;
using Core.Utilities.Results;
using WebAPI.DataAccess.Abstract;
using WebAPI.Services.Abstract;

namespace WebAPI.Services.Concrete
{
    public class AnswerManager : IAnswerService
    {

        readonly IAnswerDal _answerDal;
        public AnswerManager(IAnswerDal answerDal)
        {
            _answerDal = answerDal;
        }

        public async Task<IDataResult<Answer>> AddAsync(Answer answer)
        {
            if(answer == null)
                return new ErrorDataResult<Answer>(null,"boş cevap gönderdiniz");

            await _answerDal.Add(answer);
            return new SuccessDataResult<Answer>(answer,"Cevaplar Eklendi");
        }

        public async Task<IDataResult<Answer>> DeleteAsync(int id)
        {
            var deletedAnswer = await _answerDal.Get(a => a.Id == id);
            if (deletedAnswer == null)
                return new ErrorDataResult<Answer>(null, "Böyle bir cevap zaten yok");
            await _answerDal.Delete(deletedAnswer);
            return new SuccessDataResult<Answer>(deletedAnswer, "Cevap Silindi");
        }

        public async Task<IDataResult<List<Answer>>> GetAllAsync()
        {
            var allAnswer = await _answerDal.GetAll();
            if (allAnswer == null)
                return new ErrorDataResult<List<Answer>>(null, "Cevap Yok");
            return new SuccessDataResult<List<Answer>>(allAnswer, "Tüm Cevaplar Getirildi");
        }

        public async Task<IDataResult<List<Answer>>> GetByQuestionIdAsync(int questionId)
        {
            var getanswerById = await _answerDal.GetAnswersByQuestionIdAsync(questionId);
            if (getanswerById == null)
                return new ErrorDataResult<List<Answer>>(null, "Böyle Bir Cevap Yok");
            return new SuccessDataResult<List<Answer>>(getanswerById, "Cevaplar Getirildi");
        }

        public async Task<IDataResult<Answer>> UpdateAsync(Answer answer)
        {
            var updatedQuestion = await _answerDal.Get(a => a.Id == answer.Id);
            if (updatedQuestion == null)
                return new ErrorDataResult<Answer>(null, "Bu Id de bir cevap yok.");
            if (answer == null)
                return new ErrorDataResult<Answer>(null, "Cevabın İçi Boş");
            
            await _answerDal.Update(answer);
            return new SuccessDataResult<Answer>(answer, "Cevap Güncellendi");
        }
    }
}
