using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ML5
{
    public class ObjectDetector
    {
        public IJSRuntime Runtime { get; set; }
        public string Hash { get; private set; }
        public DotNetObjectReference<ObjectDetector> DotNet { get; private set; }
        public ObjectDetector(IJSRuntime runtime, string modelURL, ObjectDetectorOptions serializableOptions = null)
        {
            Runtime = runtime;
            Init(modelURL, serializableOptions);
        }
        public ObjectDetector(IJSRuntime runtime, ObjectDetectorModel model, ObjectDetectorOptions serializableOptions = null)
        {
            Runtime = runtime;
            Init(model.ToString().ToLower(), serializableOptions);
        }
        private async void Init(string model, object opt = null)
        {
            Hash = Helper.UIDGenerator();
            DotNet = DotNetObjectReference.Create(this);
            await Runtime.InvokeVoidAsync("initObjectDetectorML5", Hash, DotNet, model, opt);
        }

        ~ObjectDetector()
        {
            Destroy();
        }
        private async void Destroy()
        {
            await Runtime.InvokeVoidAsync("destroyObjectDetector", Hash);
        }
        [JSInvokable("ODFML")]
        #pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task __ModelLoadedOD__()
        {
            OnModelLoad?.Invoke();
        }

        public delegate void ModelLoadedHandler();
        public event ModelLoadedHandler OnModelLoad;
    }

    public partial class ObjectDetectorOptions
    {
        public double filterBoxesThreshold { get; set; } = 0.01;

        public double IOUThreshold { get; set; } = 0.4;

        public double classProbThreshold { get; set; } = 0.4;
    }
    public enum ObjectDetectorModel
    {
        COCOSSD,
        YOLO
    }
}
