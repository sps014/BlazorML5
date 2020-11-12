using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ML5
{
    public class SoundClassifier
    {
        public IJSInProcessRuntime Runtime { get; set; }
        public string Hash { get; private set; }
        private IJSInProcessObjectReference JSReference;
        public DotNetObjectReference<SoundClassifier> DotNet { get; private set; }
        public SoundClassifier(IJSInProcessRuntime runtime, string modelURL, SoundOptions serializableOptions = null)
        {
            Runtime = runtime;
            Init(modelURL, serializableOptions);
        }
        public SoundClassifier(IJSInProcessRuntime runtime, SoundModel model, SoundOptions serializableOptions = null)
        {
            Runtime = runtime;
            Init(model.ToString(), serializableOptions);
        }

        private async void Init(string model, object opt = null)
        {
            Hash = Helper.UIDGenerator();
            DotNet = DotNetObjectReference.Create(this);
            JSReference = await Runtime.InvokeAsync<IJSInProcessObjectReference>("import", "./_content/BlazorML5/ml5SoundClassifier.js");
            JSReference.InvokeVoid("initSoundClassifierML5", Hash,DotNet, model, opt);
        }

        ~SoundClassifier()
        {
            Destroy();
        }
        private  void Destroy()
        {
             JSReference.InvokeVoid("destroySoundClassifier", Hash);
        }
        public  void Classify(object serializableOpt = null)
        {
            JSReference.InvokeVoid("soundClassifierClassify", Hash, DotNet,serializableOpt);
        }
        public void Classify(ElementReference videoImageElement,object serializableOpt=null)
        {
            JSReference.InvokeVoid("soundClassifierClassify", Hash, DotNet,videoImageElement,serializableOpt);
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        [JSInvokable("SCFML")]
        public void __ModelLoadedSC__()
        {
            OnModelLoad?.Invoke();
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        [JSInvokable("SCFCF")]
        public void __Classify__(string err, CResult[] results)
        {
            OnClassification?.Invoke(err, results);
        }

        public delegate void ModelLoadedHandler();
        public event ModelLoadedHandler OnModelLoad;
        public delegate void OnClassfyHandler(string err, CResult[] result);
        public event OnClassfyHandler OnClassification;
    }

    public enum SoundModel
    {
        SpeechCommands18w
    }
}

public class SoundOptions
{
    public object imageScaleFactor { get; set; }
    public object outputStride { get; set; }
    public object flipHorizontal { get; set; }
    public object minConfidence { get; set; }
    public object maxPoseDetections { get; set; }
    public object scoreThreshold { get; set; }
    public object nmsRadius { get; set; }
    public object detectionType { get; set; }
    public object multiplier { get; set; }
}
