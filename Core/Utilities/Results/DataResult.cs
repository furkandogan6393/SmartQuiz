using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class DataResult<T> : Result, IDataResult<T>
    {
        public DataResult(T data, bool success, string message):base(success, message)
        {
            Data = data;
        }
        public DataResult(T data, bool success):base(success) 
        {
            Data = data;
        }
        public T Data { get; }
    }

    //OKU
    //Burada Resultun aynısı var, sadece ekstra Data da var,
    //base diyerek zaten Success ve Message verileri ordan çekiliyor.
    //Data kısmınıda biz burda veriyoruz.
}