using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class SuccessResult : Result
    {
        public SuccessResult(string message) : base(true, message) {}
        public SuccessResult():base(true) {}
    }
}

//OKU
//Bunun Resulttan bir farkı yok, sadece okunabilirlik farklı.
//içine sadece mesaj göndererekte yapabilirsin, zaten true kısmı base ile veriliyor.
//base şu işe yarar:
//Miras aldığım sınıfın constructorüne True ve Message parametrelerini ver.
//Ona göre çalışsınlar.
//Yani önceki constructorü senin için çalışması için parametre verip çağırıyorsun.
//Bunun sayesinde, SuccessResult("Ürün Eklendi") kısmı verilsede sistem bunun
//True olduğunu anlıyor. alt kısmı ise öncekiyle aynı, alttakini doldurmak için.