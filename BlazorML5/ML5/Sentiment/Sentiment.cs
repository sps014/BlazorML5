using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace ML5
{
    public class Sentiment
    {
        public IJSInProcessRuntime Runtime { get; set; }
        public static int SentimentNetHash { get; private set; } = 0;
        public int Hash { get; private set; }
        public DotNetObjectReference<Sentiment> DotNet { get; private set; }

        private IJSInProcessObjectReference JSReference;

        public Sentiment(IJSRuntime runtime)
        {
            Runtime = runtime as IJSInProcessRuntime;
            Init();
        }
        public Sentiment(IJSRuntime runtime,string modelPath)
        {
            Runtime = runtime as IJSInProcessRuntime;
            Init(modelPath);
        }
        public double Predict(string text)
        {
            return JSReference.Invoke<double>("predictSentiMl5", Hash, text);
        }
        private async void Init(string model= "movieReviews")
        {
            Hash = SentimentNetHash++;
            DotNet = DotNetObjectReference.Create(this);
            JSReference = await Runtime.InvokeAsync<IJSInProcessObjectReference>("import", "./_content/BlazorML5/ml5Sentiment.js");
            JSReference.InvokeVoid("intSentiMl5", Hash, DotNet, model);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [JSInvokable("SALMSG")]
        public void __ModelLoadedSC__()
        {
            ModelLoaded?.Invoke();
        }

        public delegate void ModelLoadedHandler();
        public event ModelLoadedHandler ModelLoaded;
    }
}
