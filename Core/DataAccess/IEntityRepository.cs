using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    //Verilen T'nin alabileceği değerleri sınırlandırma.
    //class: Referans tip olabilir demektir bu.
    //IEntity: IEntity olabilir veya IEntity implemente eden bir nesne olabilir.
    //New(): new'lenebilir olmalı. 
    public interface IEntityRepository<T> where T: class, IEntity
    {
        Task<List<T>> GetAll(Expression<Func<T, bool>> filter = null);
        Task<T> Get(Expression<Func<T,bool>> filter);
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);

        //BU YAPI O kadar işini kolaylaştırıyor ki, bu generic yapı sayesinde hiçbir şeyi birdaha yazmak zorunda değilsin.
        //T bir tutucu, içine istediğin tipi yolla, yerine koyar ve geçer. 
        //Örnek yapalım, 2 AY SONRA PROJEYE YENİ BİR Özellik ekleniyor, Customer EKLEYELİM, BAK ŞİMDİ NE KADAR KOLAYLAŞIYOR.
        //Entitites de Customer.cs oluştur, içine prop ekle, sonra IEntity.cs interface'ine bağla.
        //DataAccess Dosyasından Abstract'a gel, yeni dosya oluştur, ICustomerDal.cs, sonra bunu IEntityRepository interface'ini buna iplement et ve bitti la.



        //Expression<Func<T,bool>> filter=null
        //Bu yapı ürünleri çağırırken filtreleme yapabilir.
        //Managerda göreceksin.
        //BİRKERE YAZACAKSIN BİRDAHA YAZMAYACAKSIN.
        //MÜKEMMEL BİR YAPI!!!!!!!!!
    }
}
