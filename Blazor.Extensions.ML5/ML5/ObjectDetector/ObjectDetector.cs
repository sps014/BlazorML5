using Microsoft.AspNetCore.Components;
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
        public async void Detect(ElementReference videoOrImageOrCanvas)
        {
            await Runtime.InvokeVoidAsync("objectDetectorDetect", Hash, DotNet, videoOrImageOrCanvas);
        }
        public async void Detect(object imageData, int noOfClasses = 0)
        {
            await Runtime.InvokeVoidAsync("objectDetectorDetect", Hash, DotNet, imageData);
        }
        [JSInvokable("ODFML")]
        #pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task __ModelLoadedOD__()
        {
            OnModelLoad?.Invoke();
        }
        [JSInvokable("ODFDR")]
        public async Task __Detect__(string err, ObjectResult[] results)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            OnDetection?.Invoke(err, results);
        }
        public delegate void ModelLoadedHandler();
        public event ModelLoadedHandler OnModelLoad;
        public delegate void OnDetectHandler(string err, ObjectResult[] result);
        public event OnDetectHandler OnDetection;
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
    public class ObjectBox
    {
        public double x { get; set; }
        public double y { get; set; }
        public double width { get; set; }
        public double height { get; set; }
    }
    public class ObjectResult
    {
        public string label { get; set; }
        public double x { get; set; }
        public double y { get; set; }
        public double width { get; set; }
        public double height { get; set; }
        public ObjectBox normalized { get; set; }
    }
}
