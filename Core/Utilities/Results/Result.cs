using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {

        public Result(bool success, string message) : this(success) //this(success) dediğinde, aynı isme sahip,
        {                                                           //içinde sadece success imzası olan
            Message = message;                                      //constructor methodunuda çalıştır diyoruz.
        }                                                           //2 Method aynı anda çalışıyor böylelikle.
        public Result(bool success)
        {
            Success = success;
        }
        //Get bile olsa, constructor içinde set edilebilir.
        public bool Success {  get; }

        public string Message { get; }
    }
}

//OKUUUUU
//Burada veri yok, buradaki olay şu:
//productmanagerde:
//return new Result(true,"Başarılı") dediğimizde burda
//Başarılı olan bir nesne oluşturulur, içine veri göndermeyiz,
//new diyip içine veri koyduğumuzda veriyle doğar zaten bu nesne.
//this kısmı ise 2 methodun aynı anda çalışmasını sağlar biliyon zaten.