using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.ComponentModel;
using System.Threading.Tasks;

namespace ML5
{
    public class ImageClassifier
    {
        public IJSInProcessRuntime Runtime { get; set; }
        public string Hash { get; private set; }
        public DotNetObjectReference<ImageClassifier> DotNet { get; private set; }
        public ImageClassifier(IJSInProcessRuntime runtime, ImageModel model, ElementReference video, object serializableOptions = null)
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
        public ImageClassifier(IJSInProcessRuntime runtime, string modelURL, ElementReference video, object serializableOptions = null)
        {
            Runtime = runtime;
            Init(modelURL, video, serializableOptions);
        }
        public ImageClassifier(IJSInProcessRuntime runtime, ImageModel model, object serializableOptions = null)
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
        public ImageClassifier(IJSInProcessRuntime runtime, string modelURL, object serializableOptions = null)
        {
            Runtime = runtime;
            Init(modelURL, serializableOptions);

        }

        private void Init(string modelURL, ElementReference video, object options = null)
        {
            Hash = Helper.UIDGenerator();
            DotNet = DotNetObjectReference.Create(this);
            Runtime.InvokeVoid("initImageClassifierVidML5", Hash,DotNet, modelURL, video, options);
        }
        private  void Init(string model,object opt=null)
        {
            Hash = Helper.UIDGenerator();
            DotNet = DotNetObjectReference.Create(this);
            Runtime.InvokeVoid("initImageClassifierStrML5", Hash,DotNet, model,opt);
        }

        ~ImageClassifier()
        {
            Destroy();
        }
        private void Destroy()
        {
            Runtime.InvokeVoid("destroyImageClassifier", Hash);
        }
        public void Classify(ElementReference videoOrImageOrCanvas,int noOfClasses=0)
        {
            if(noOfClasses==0)
               Runtime.InvokeVoid("imageClassifierClassify",Hash,DotNet, videoOrImageOrCanvas);
            else
                Runtime.InvokeVoid("imageClassifierClassify", Hash, DotNet, videoOrImageOrCanvas,noOfClasses);
        }
        public  void Classify(object imageData, int noOfClasses = 0)
        {
            if (noOfClasses == 0)
                Runtime.InvokeVoid("imageClassifierClassify", Hash, DotNet, imageData);
            else
                Runtime.InvokeVoid("imageClassifierClassify", Hash, DotNet, imageData, noOfClasses);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [JSInvokable("ICFML")]
        public void __ModelLoadedIC__()
        {
            OnModelLoad?.Invoke();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [JSInvokable("ICFCF")]
        public void __Classify__(string err,CResult[] results)
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
