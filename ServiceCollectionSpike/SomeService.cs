using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceCollectionSpike
{
    public interface ISomeService
    {
        void DisplayOptions();
    }
    public class SomeService : ISomeService
    {
        private ISomeClient client;
        public SomeService(ISomeClient client)
        {
            this.client = client;
        }

        public void DisplayOptions()
        {
            Console.WriteLine(this.client.ShowOptions());
        }

    }
}
