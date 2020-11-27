using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorML5
{
    public class NeuralNetwork
    {
        private int Hash = 0;
        private static int HashCount = 0;

        private IJSUnmarshalledObjectReference _unmarshalledReference;

        internal NeuralNetwork(int hash,IJSUnmarshalledObjectReference reference)
        {
            Hash = hash;
            _unmarshalledReference = reference;
        }
        public static async Task<NeuralNetwork> Create(int input,int output)
        {
            var (h,r)=await InitCore();
            r.InvokeUnmarshalled<int,int,int,object>("createNetworkFromInput", h, input, output);
            return new NeuralNetwork(h, r);
            
        }
        public static async Task<NeuralNetwork> Create(object options)
        {
            var (h, r) = await InitCore();
            return new NeuralNetwork(h, r);

        }
        private static async Task<(int hash,IJSUnmarshalledObjectReference runtime)> InitCore()
        {
             await ML5.Runtime.InvokeVoidAsync("import", "./_content/BlazorML5/ml5NeuralNetwork.js");
             var runtime=ML5.URuntime.InvokeUnmarshalled<IJSUnmarshalledObjectReference>("NeuralNetworkML5");
             return (HashCount++,runtime);
        }
        ~NeuralNetwork()
        {
            _unmarshalledReference.InvokeUnmarshalled<int,object>("disposeNeuralNetwork", Hash);
        }
    }
}
