using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ML5
{
    public class ImageClassifier
    {
        public IJSRuntime Runtime { get; set; }
        public string Hash { get; private set; }
        public DotNetObjectReference<ImageClassifier> DotNet { get; private set; }
        public ImageClassifier(IJSRuntime runtime, ImageModel model, ElementReference video, object serializableOptions = null)
        {
            Runtime = runtime;
            string modelStr;
            if(model==ImageModel.DarknetTiny)
            {
                modelStr = "Darknet-tiny";
            }
            else
            {
                modelStr = model.ToString();
            }
            Init(modelStr, video, serializableOptions);

        }
        public ImageClassifier(IJSRuntime runtime, string modelURL, ElementReference video, object serializableOptions = null)
        {
            Runtime = runtime;
            Init(modelURL, video, serializableOptions);
        }
        public ImageClassifier(IJSRuntime runtime, ImageModel model, object serializableOptions = null)
        {
            Runtime = runtime;
            string modelStr;
            if (model == ImageModel.DarknetTiny)
            {
                modelStr = "Darknet-tiny";
            }
            else
            {
                modelStr = model.ToString();
            }
            Init(modelStr, serializableOptions);
        }
        public ImageClassifier(IJSRuntime runtime, string modelURL, object serializableOptions = null)
        {
            Runtime = runtime;
            Init(modelURL, serializableOptions);

        }

        private async void Init(string modelURL, ElementReference video, object options = null)
        {
            Hash = Helper.UIDGenerator();
            DotNet = DotNetObjectReference.Create(this);
            await Runtime.InvokeVoidAsync("initImageClassifierVidML5", Hash,DotNet, modelURL, video, options);
        }
        private async  void Init(string model,object opt=null)
        {
            Hash = Helper.UIDGenerator();
            DotNet = DotNetObjectReference.Create(this);
            await Runtime.InvokeVoidAsync("initImageClassifierStrML5", DotNet,Hash,model,opt);
        }

        ~ImageClassifier()
        {
            Destroy();
        }
        private async void Destroy()
        {
            await Runtime.InvokeVoidAsync("destroyImageClassifier", Hash);
        }
        public async void Classify(ElementReference videoOrImageOrCanvas,int noOfClasses=0)
        {
            if(noOfClasses==0)
            await Runtime.InvokeVoidAsync("imageClassifierClassify",Hash,DotNet, videoOrImageOrCanvas);
            else
                await Runtime.InvokeVoidAsync("imageClassifierClassify", Hash, DotNet, videoOrImageOrCanvas,noOfClasses);
        }
        public async void Classify(object imageData, int noOfClasses = 0)
        {
            if (noOfClasses == 0)
                await Runtime.InvokeVoidAsync("imageClassifierClassify", Hash, DotNet, imageData);
            else
                await Runtime.InvokeVoidAsync("imageClassifierClassify", Hash, DotNet, imageData, noOfClasses);
        }

        [JSInvokable("ICFML")]
        public async Task __ModelLoaded__()
        {
            OnModelLoad?.Invoke();
        }
        [JSInvokable("ICFCF")]
        public async Task __Classify__(string err,CResult[] results)
        {
            OnClassification?.Invoke(err,results);
        }

        public delegate void ModelLoadedHandler();
        public event ModelLoadedHandler OnModelLoad;
        public delegate void OnClassfyHandler(string err,CResult[] result);
        public event OnClassfyHandler OnClassification;
    }

    public enum ImageModel
    {
        MobileNet,
        Darknet,
        DarknetTiny,
        DoodleNet
    }
}
