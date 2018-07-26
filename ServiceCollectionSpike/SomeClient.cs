using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceCollectionSpike
{
    public class SomeClientOptions
    {
        public string OptionA { get; set; }
        public SomeOptions SomeOptions { get; set; }
    }

    public class SomeOptions
    {
        public string OptionB { get; set; }
        public string OptionC { get; set; }
    }

    public interface ISomeClient
    {
        string OptionA { get; set; }
        SomeOptions options { get; set; }

        string ShowOptions();
    }
    public class SomeClient : ISomeClient
    {
        public string OptionA { get; set; }
        public SomeOptions options {get; set;}
        public SomeClient(string optionA, SomeOptions options)
        {
            this.OptionA = optionA;
            this.options = options;
        }

        public string ShowOptions()
        {
            return $"SomeClient OptionA: {this.OptionA} OptionB: {this.options?.OptionB} OptionC: {this.options?.OptionC}";
        }
    }
}
