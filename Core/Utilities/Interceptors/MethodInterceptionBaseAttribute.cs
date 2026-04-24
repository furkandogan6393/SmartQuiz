using Castle.DynamicProxy;
using Core.Aspects.Autofac.Caching;
using System;
using IInterceptor = Castle.DynamicProxy.IInterceptor;

namespace Core.Utilities.Interceptors
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)] // Classlara, Methodlara uygulanabilir, çoklu kullanılabilir, miras alınabilir demek.
    public abstract class MethodInterceptionBaseAttribute : Attribute, IInterceptor // Proxy bu class'ı tanısın, bu bir interceptor desin diye işaretleme.
    {                                                                               //Attribute: Köşeli parantezle işaretleme yapabilelim.
        public int Priority { get; set; } //Öncelik sıralaması oluşturmamızı sağlar. [ValidationAspect(Priority = 1)]   önce bu
                                                                                   //[CacheAspect(Priority = 2)]        sonra bu

        public virtual void Intercept(IInvocation invocation) //MethodInterception bunu kullanacak. 
        {

        }
    }
}
