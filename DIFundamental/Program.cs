using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace DIFundamental
{
    public interface ISomeClient
    {
        string Id { get; set; }
    }
    public interface IOtherClient : ISomeClient
    {

    }

    public class SomeClient : ISomeClient
    {
        public string Id {get; set;}
        public SomeClient()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        public SomeOption SomeOptions {get; set;}
    }
    public class OtherClient : IOtherClient
    {
        public string Id { get; set; }
        public OtherClient()
        {
            this.Id = Guid.NewGuid().ToString();
        }

    }

    public interface ISpecialClient : ISomeClient
    {

    }
    public class SpecialClient : ISpecialClient
    {
        public string Id { get; set; }
        public SpecialClient()
        {
            this.Id = Guid.NewGuid().ToString();
        }

    }

    public class SomeOption
    {
        public string Value1 { get; set; }
        public string Value2 { get; set; }
    }

    public interface ISomeService
    {
        void Greeting();
    }

    public interface INoImplementation
    {
        void Decline();
    }

    public class SomeService : ISomeService
    {
        private ISomeClient client;
        public SomeService (ISomeClient client)
        {
            this.client = client;
        }
        public void Greeting()
        {
            Console.WriteLine($"Type: {this.client.GetType()} Id: {this.client.Id}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();

            // Basic Registration
            services.AddSingleton<ISomeClient, SomeClient>();

            // Registration with Function
            services.AddTransient<IOtherClient>(client =>
            {
                return new OtherClient();
            });

            // Register Option value object
            services.Configure<SomeOption>(o => new SomeOption
            {
                Value1 = "Hello",
                Value2 = "World"
            });
            services.AddSingleton<ISomeService, SomeService>();
            // Options
            services.AddSingleton<SomeClient>(serviceProvider =>
            {
                var options = serviceProvider.GetRequiredService<IOptions<SomeOption>>();
                var client =  new SomeClient();
                client.SomeOptions = options.Value;
                return client;
            });

            var provider = services.BuildServiceProvider();

            // Singleton
            var someClient1 = provider.GetRequiredService<ISomeClient>();
            Console.WriteLine($"SomeClient1(Singleton): Type: {someClient1.GetType()} Id: {someClient1.Id}");            
            var someClient2 = provider.GetRequiredService<ISomeClient>();
            Console.WriteLine($"SomeClient2(Singleton): Type: {someClient2.GetType()} Id: {someClient2.Id}");
            var someClient3 = (ISomeClient)provider.GetRequiredService(typeof(ISomeClient));
            Console.WriteLine($"SomeClient3(Singleton): Type: {someClient3.GetType()} Id: {someClient3.Id}");
            // Transient
            var otherClient1 = provider.GetService<IOtherClient>();
            Console.WriteLine($"OtherClient1(Transient): Type: {otherClient1.GetType()} Id: {otherClient1.Id}");
            var otherClient2 = provider.GetService<IOtherClient>();
            Console.WriteLine($"OtherClient2(Transient): Type: {otherClient2.GetType()} Id: {otherClient2.Id}");

            // Options
            var clientWithOption = provider.GetService<SomeClient>();
            Console.WriteLine($"Client With Option: Type: {clientWithOption.GetType()} Value1: {clientWithOption.SomeOptions.Value1} Value2: {clientWithOption.SomeOptions.Value2}");

            // Constructor Injection
            var service = provider.GetRequiredService<ISomeService>();
            Console.WriteLine($"SomeService Greeting calling...");
            service.Greeting();            

            // Behavior difference between GetService and GetRequiredService
            var noImplementation1 = provider.GetService<INoImplementation>();
            Console.WriteLine($"NoImplementation (GetService): {noImplementation1}");
            try
            {
                var noImplementation2 = provider.GetRequiredService<INoImplementation>();
                Console.WriteLine($"NoImplementation (GetRequiredService): {noImplementation2}");
            } catch (Exception e)
            {
                Console.WriteLine($"NoImplementation (GetRequiredService) Exception: {e.Message}");
            }

            Console.ReadLine();

        }
    }
}
