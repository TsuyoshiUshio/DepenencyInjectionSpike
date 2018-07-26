using Microsoft.Extensions.DependencyInjection;
using System;

namespace ServiceCollectionSpike
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddSingleton<ISomeService, SomeService>();

            //services.AddSomeClient(options =>
            //{
            //    options.OptionA = "Hello";
            //});

            services.AddSomeClient(options =>
            {
                options.OptionA = "Hello";
            }).SetSomeClientOptions(new SomeOptions
            {
                OptionB = "World",
                OptionC = "Again"
            });

            var someService = services.BuildServiceProvider().GetRequiredService<ISomeService>();
            someService.DisplayOptions();
            Console.ReadLine();
        }
    }
}
