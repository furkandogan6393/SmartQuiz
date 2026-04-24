using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public interface IDataResult<T>:IResult 
    {
        T Data { get; }
    }
}

//OKUUU
//Bunda ki olay özelleşmiş interface olması, öncekinin mirası var.
//Yani içinde success ve messages var ama ekstra Data da ekliyor burda.