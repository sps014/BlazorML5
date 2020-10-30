using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace ML5
{
    public class ObjectDetector
    {
        public IJSInProcessRuntime Runtime { get; set; }
        public string Hash { get; private set; }
        public DotNetObjectReference<ObjectDetector> DotNet { get; private set; }
        public ObjectDetector(IJSInProcessRuntime runtime, string modelURL, ObjectDetectorOptions serializableOptions = null)
        {
            Runtime = runtime;
            Init(modelURL, serializableOptions);
        }
        public ObjectDetector(IJSInProcessRuntime runtime, ObjectDetectorModel model, ObjectDetectorOptions serializableOptions = null)
        {
            Runtime = runtime;
            Init(model.ToString().ToLower(), serializableOptions);
        }
        private void Init(string model, object opt = null)
        {
            Hash = Helper.UIDGenerator();
            DotNet = DotNetObjectReference.Create(this);
            Runtime.InvokeVoid("initObjectDetectorML5", Hash, DotNet, model, opt);
        }

        ~ObjectDetector()
        {
            Destroy();
        }
        private  void Destroy()
        {
            Runtime.InvokeVoid("destroyObjectDetector", Hash);
        }
        public  void Detect(ElementReference videoOrImageOrCanvas)
        {
            Runtime.InvokeVoid("objectDetectorDetect", Hash, DotNet, videoOrImageOrCanvas);
        }
       
        public  void Detect(object imageData, int noOfClasses = 0)
        {
             Runtime.InvokeVoid("objectDetectorDetect", Hash, DotNet, imageData);
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        [JSInvokable("ODFML")]
        public void __ModelLoadedOD__()
        {
            OnModelLoad?.Invoke();
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        [JSInvokable("ODFDR")]
        public void __Detect__(string err, ObjectResult[] results)
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
