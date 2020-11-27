using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace BlazorML5
{
    public static class ML5
    {
        public static IJSInProcessRuntime Runtime { get; private set; }
        public static IJSUnmarshalledRuntime URuntime { get; private set; }

        public static void Init(IJSRuntime runtime)
        {
            Runtime = runtime as IJSInProcessRuntime;
            URuntime = runtime as IJSUnmarshalledRuntime;
        }
    }
}
