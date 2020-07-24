﻿using Microsoft.AspNetCore.Components;
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
        public async Task<string> OnSinglePose(ElementReference? canvasOrVideoOrImage=null)
        {
            if(canvasOrVideoOrImage!=null)
            return await Runtime.InvokeAsync<string>("poseNetsinglePoseML5", Hash.ToString(), canvasOrVideoOrImage);
            else
                return await Runtime.InvokeAsync<string>("poseNetsinglePoseML5", Hash.ToString(), null);

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

        public delegate void ModelLoadedHandler();
        public event ModelLoadedHandler OnModelLoad;

        public enum DetectionType
        {
            single,multiple
        }
    }
}