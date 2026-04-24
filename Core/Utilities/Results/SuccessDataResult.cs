using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class SuccessDataResult<T>:DataResult<T>
    {
        //BURADA data nasıl mı geliyor, bussiness'e geldiğinde, 
        //(_productDal.GetAll(),Messages.ProductsListed) Diyor, 
        //_productDal.GetAll() = Burası data, Messages.ProductsListed = Burası ise mesajdır.
        public SuccessDataResult(T data, string message):base(data,true,message)
        {
            
        }
        public SuccessDataResult(T data):base(data,true)
        {
            
        }
        public SuccessDataResult(string message):base(default, true,message) 
        {
            
        }
        public SuccessDataResult():base(default,true)
        {
            
        }
    }
}
//OKUUU
//Burdaki mantıkta şu, biz base verileri DataResulta gönderiyoruz.
//Burda hepsi true kombinasyonuyla kurulmuştur, Çünkü burası başarı dönecek.
//Product Managerda bunu if ile kontrol edebiliriz:
//İşlem başarılı ise bu blok, başarılı değil ise ErrorDataResult diyebiliriz.
