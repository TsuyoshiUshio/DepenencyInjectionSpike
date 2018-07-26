using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceCollectionSpike
{
    public static class ServiceCollectionExtensions
    {
        public static ISomeClientBuilder AddSomeClient(this IServiceCollection services, Action<SomeClientOptions> setupAction)
        {
            services.Configure(setupAction);
            services.AddSingleton<ISomeClient>(provider =>
            {
                var options = provider.GetRequiredService<IOptions<SomeClientOptions>>();
                return new SomeClient(options.Value.OptionA, options.Value.SomeOptions);
            }
            );
            return new SomeClientBuilder(services);
        }

        public static ISomeClientBuilder SetSomeClientOptions(this ISomeClientBuilder builder, SomeOptions options)
        {
            if (builder == null) throw new ArgumentException(nameof(builder));

            builder.Services.Configure<SomeClientOptions>(o => o.SomeOptions = options);
            return builder;
        }
        
        public interface ISomeClientBuilder
        {
            IServiceCollection Services { get; }
        }
        public class SomeClientBuilder : ISomeClientBuilder
        {
            public SomeClientBuilder(IServiceCollection services)
            {
                this.Services = services;
            }
            public IServiceCollection Services { get; }
        }
    }
}
