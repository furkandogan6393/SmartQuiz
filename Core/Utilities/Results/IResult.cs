using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    //Temel Voidler için başlangıç.
    public interface IResult
    {

        //Mantık: Yaptığın işlem başarılı ise true : Success
        //True ise, Başarılı sonucunu dön. : Message
        bool Success { get; } //Get: Sadece Okunabilir olsun 
        string Message { get; }
    }
}

//OKU
//İLK AŞAMADA UYULMASI GEREKEN ŞARTLAR BURDA BELİRTİLİYOR.
//IResultu implemente eden yani miras alan tüm sınıflar,
//Success ve Message dönmek zorunda demektir bu.
//Bu sadece sözleşme şu an.