using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;

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
            Init(runtime);
        }
        public PoseNet(IJSRuntime runtime,ElementReference element,DetectionType type)
        {
            Init(runtime);

        }
        public PoseNet(IJSRuntime runtime, ElementReference element, PoseNetOptions options)
        {
            Init(runtime);

        }
        public PoseNet(IJSRuntime runtime, PoseNetOptions options)
        {
            Init(runtime);

        }
        private void Init(IJSRuntime runtime)
        {
            Runtime = runtime;
            DotNetObjectRef = DotNetObjectReference.Create(this);
            Hash = PoseNetHash++;
        }
        public enum DetectionType
        {
            single,multiple
        }
    }
}
