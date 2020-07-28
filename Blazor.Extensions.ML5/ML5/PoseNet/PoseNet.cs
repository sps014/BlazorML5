using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ML5
{
    public class PoseNet
    {
        public IJSRuntime Runtime { get; set; }
        public static int PoseNetHash { get; private set; } = 0;
        public int Hash { get; private set; }
        public DotNetObjectReference<PoseNet> DotNetObjectRef { get; private set; }
        public PoseNet(IJSRuntime runtime)
        {
            Init(runtime, null, null, null);
        }
        public PoseNet(IJSRuntime runtime,ElementReference element,DetectionType type)
        {
            Init(runtime, element, null, type);
            //function poseNetML5(hash, dotnet, video = null, options = null, type = null)
        }
        public PoseNet(IJSRuntime runtime, ElementReference element)
        {
            Init(runtime, element, null, null);
            //function poseNetML5(hash, dotnet, video = null, options = null, type = null)
        }
        public PoseNet(IJSRuntime runtime, ElementReference element, PoseNetOptions options)
        {
            Init(runtime, element, options, null);
        }
        public PoseNet(IJSRuntime runtime, PoseNetOptions options)
        {
            Init(runtime, null, options, null);

        }
        private async void Init(IJSRuntime runtime,ElementReference? element,PoseNetOptions options,DetectionType? type)
        {
            Runtime = runtime;
            DotNetObjectRef = DotNetObjectReference.Create(this);
            Hash = PoseNetHash++;
            await runtime.InvokeVoidAsync("poseNetML5", Hash.ToString(), DotNetObjectRef, element, options, type);

        }
        public async Task<List<PoseResult>> SinglePose(ElementReference? canvasOrVideoOrImage=null)
        {
            List<PoseResult> jsonRes;
            if (canvasOrVideoOrImage != null)
                jsonRes = await Runtime.InvokeAsync<List<PoseResult>>("poseNetsinglePoseML5", Hash.ToString(), canvasOrVideoOrImage);
            else
                jsonRes = await Runtime.InvokeAsync<List<PoseResult>>("poseNetsinglePoseML5", Hash.ToString(), null);

            return jsonRes;
        }
        public async Task<List<PoseResult>> MultiPose(ElementReference? canvasOrVideoOrImage = null)
        {
            List<PoseResult> jsonRes;
            if (canvasOrVideoOrImage != null)
                jsonRes = await Runtime.InvokeAsync<List<PoseResult>>("poseNetmultiPoseML5", Hash.ToString(), canvasOrVideoOrImage);
            else
                jsonRes = await Runtime.InvokeAsync<List<PoseResult>>("poseNetmultiPoseML5", Hash.ToString(), null);

            return jsonRes;
        }
        ~PoseNet()
        {
            destroy();
        }
        async void destroy()
        {
            await Runtime.InvokeVoidAsync("destroyPoseNetML5", Hash.ToString());

        }




        [JSInvokable("PNFML")]
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task __ModelLoadedPN__()
        {
            OnModelLoad?.Invoke();
        }
        [JSInvokable("PNCBF")]
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task __CBKPN__(List<PoseResult> results)
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
