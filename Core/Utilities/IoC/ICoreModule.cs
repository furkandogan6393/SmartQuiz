using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.IoC
{
    public interface ICoreModule //Framework katmanımız, şirket değiştirsek bile kullanabileceğimiz katman.
    {
        void Load(IServiceCollection serviceCollection);
    }
}
