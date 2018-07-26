using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DIFundamental
{
    public static class ServiceCollectionExteions
    {
        public static void AddSpecialClient(this IServiceCollection services)
        {
            services.AddSingleton<ISpecialClient>();
        }
    }
}
