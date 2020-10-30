using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace ML5
{
    public class PoseNet
    {
        public IJSInProcessRuntime Runtime { get; set; }
        public static int PoseNetHash { get; private set; } = 0;
        public int Hash { get; private set; }
        public DotNetObjectReference<PoseNet> DotNetObjectRef { get; private set; }
        public PoseNet(IJSInProcessRuntime runtime)
        {
            Init(runtime, null, null, null);
        }
        public PoseNet(IJSInProcessRuntime runtime,ElementReference element,DetectionType type)
        {
            Init(runtime, element, null, type);
            //function poseNetML5(hash, dotnet, video = null, options = null, type = null)
        }
        public PoseNet(IJSInProcessRuntime runtime, ElementReference element)
        {
            Init(runtime, element, null, null);
            //function poseNetML5(hash, dotnet, video = null, options = null, type = null)
        }
        public PoseNet(IJSInProcessRuntime runtime, ElementReference element, PoseNetOptions options)
        {
            Init(runtime, element, options, null);
        }
        public PoseNet(IJSInProcessRuntime runtime, PoseNetOptions options)
        {
            Init(runtime, null, options, null);

        }
        private  void Init(IJSInProcessRuntime runtime,ElementReference? element,PoseNetOptions options,DetectionType? type)
        {
            Runtime = runtime;
            DotNetObjectRef = DotNetObjectReference.Create(this);
            Hash = PoseNetHash++;
            runtime.InvokeVoid("poseNetML5", Hash.ToString(), DotNetObjectRef, element, options, type);

        }
        public async  Task<List<PoseResult>> SinglePose(ElementReference? canvasOrVideoOrImage=null)
        {
            List<PoseResult> jsonRes;
            jsonRes = await Runtime.InvokeAsync<List<PoseResult>>("poseNetsinglePoseML5", Hash.ToString(), canvasOrVideoOrImage);

            return jsonRes;
        }
        public async Task<List<PoseResult>> MultiPose(ElementReference? canvasOrVideoOrImage = null)
        {
            List<PoseResult> jsonRes;
            jsonRes = await Runtime.InvokeAsync<List<PoseResult>>("poseNetmultiPoseML5", Hash.ToString(), canvasOrVideoOrImage);

            return jsonRes;
        }
        ~PoseNet()
        {
            destroy();
        }
         void destroy()
        {
             Runtime.InvokeVoid("destroyPoseNetML5", Hash.ToString());

        }


        [EditorBrowsable(EditorBrowsableState.Never)]
        [JSInvokable("PNFML")]
        public void __ModelLoadedPN__()
        {
            OnModelLoad?.Invoke();
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        [JSInvokable("PNCBF")]
        public void __CBKPN__(List<PoseResult> results)
        {
            OnPose?.Invoke(results);
        }



        public delegate void ModelLoadedHandler();
        public event ModelLoadedHandler OnModelLoad;

        public delegate void OnPoseHandler(List<PoseResult> result);
        public event OnPoseHandler OnPose;
        public enum DetectionType
        {
            single,multiple
        }
    }
}
