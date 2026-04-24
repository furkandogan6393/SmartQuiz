using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {
        T Get<T>(string key);
        //Key'i verip çağırma.
        object Get(string key);

        //Bir isim, herşey bir obje olduğundan dolayı obje, birde süre tutuyoruz.
        void Add(string key, object value, int duration);

        //Cache'de var mı?
        bool IsAdd(string key);

        //Cache'ten uçurma(silme)
        void Remove(string key);

        //Burda belirlediğimiz şartlara veya özelliklere göre silsin istiyoruz.
        void RemoveByPattern(string pattern);

    }
}
