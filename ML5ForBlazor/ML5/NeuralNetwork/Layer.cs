using Microsoft.JSInterop;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ML5
{
    public class Layer
    {
        public IJSRuntime Runtime { get; set; }
        public string PHash { get; private set; }
        public int LayerNumber { get; private set; }
        public Layer(IJSRuntime runtime,string hash,int layerNumber)
        {
            Runtime = runtime;
            PHash = hash;
            LayerNumber = layerNumber;
        }
        public async Task<Matrix> GetWeights()
        {
            Weight  wt=await Runtime.InvokeAsync<Weight>("getWeightsML5",PHash,LayerNumber);
           
            return GetMatrix(wt);
        }
        public async Task<Matrix> GetBias()
        {
            Weight wt = await Runtime.InvokeAsync<Weight>("getBiasML5", PHash, LayerNumber);

            return GetMatrix(wt);
        }
        private Matrix GetMatrix(Weight wt)
        {
            return Matrix.FromArray(wt.Data,wt.Shape[0],wt.Shape[1]);
        }
        public async void SetWeights(Matrix weights,Matrix bias)
        {
            double[] array = weights.ToArray();
            double[] biasArray = bias.ToArray();
            await Runtime.InvokeVoidAsync("setWeightsML5", PHash, LayerNumber,array,weights.Rows,weights.Columns,biasArray);
        }
    }
    public class Weight
    {
        public int[] Shape { get; set; }
        public double[] Data { get; set; }
    }
}
