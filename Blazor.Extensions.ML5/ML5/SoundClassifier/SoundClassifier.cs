using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ML5
{
    public class SoundClassifier
    {
        public IJSRuntime Runtime { get; set; }
        public string Hash { get; private set; }
        public DotNetObjectReference<SoundClassifier> DotNet { get; private set; }
        public SoundClassifier(IJSRuntime runtime, string modelURL, SoundOptions serializableOptions = null)
        {
            Runtime = runtime;
            Init(modelURL, serializableOptions);
        }
        public SoundClassifier(IJSRuntime runtime, SoundModel model, SoundOptions serializableOptions = null)
        {
            Runtime = runtime;
            
            Init(model.ToString(), serializableOptions);
        }

        private async void Init(string model, object opt = null)
        {
            Hash = Helper.UIDGenerator();
            DotNet = DotNetObjectReference.Create(this);
            await Runtime.InvokeVoidAsync("initSoundClassifierML5", Hash,DotNet, model, opt);
        }

        ~SoundClassifier()
        {
            Destroy();
        }
        private async void Destroy()
        {
            await Runtime.InvokeVoidAsync("destroySoundClassifier", Hash);
        }
        public async void Classify(object serializableOpt = null)
        {
                await Runtime.InvokeVoidAsync("soundClassifierClassify", Hash, DotNet,serializableOpt);
        }
        public async void Classify(ElementReference videoImageElement,object serializableOpt=null)
        {
            await Runtime.InvokeVoidAsync("soundClassifierClassify", Hash, DotNet,videoImageElement,serializableOpt);
        }

        [JSInvokable("SCFML")]
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task __ModelLoadedSC__()
        {
            OnModelLoad?.Invoke();
        }
        [JSInvokable("SCFCF")]
        public async Task __Classify__(string err, CResult[] results)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
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
