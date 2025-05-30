using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Services
{
    public class ServiceFactory : IServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ServiceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IGenericCrudService<T> GetService<T>() where T : class
        {
            return _serviceProvider.GetRequiredService<IGenericCrudService<T>>();
        }
    }
}
