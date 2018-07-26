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

            var someOptions = new SomeOptions
            {
                OptionB = "World",
                OptionC = "Again"
            };
            services.AddSomeClient(options =>
            {
                options.OptionA = "Hello";
            }).SetSomeClientOptions(someOptions);

            var someService = services.BuildServiceProvider().GetRequiredService<ISomeService>();
            someService.DisplayOptions();
            Console.ReadLine();
        }
    }
}
