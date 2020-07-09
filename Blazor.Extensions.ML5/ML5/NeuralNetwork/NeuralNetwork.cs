using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ML5
{
    public class NeuralNetwork
    {
        public static int HashCounter { get; set; } = 0;
        public string Hash { get; private set; }
        public IJSRuntime Runtime { get; }

        public NeuralNetwork()
        {
            Init();
        }

        void Init()
        {
            Hash = HashCounter++.ToString();
        }

    }
}
