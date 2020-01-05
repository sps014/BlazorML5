using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;


namespace ML5.WebCam
{
    public class WebCam
    {
        public IJSRuntime Runtime { get; private set; }
        public WebCam(IJSRuntime runtime)
        {
            Runtime = runtime;
        }
        public async void Capture(ElementReference video, CamOptions option=null)
        {
            if (option == null)
                option = new CamOptions();
            await Runtime.InvokeVoidAsync("initWebCam", video,option);
        }
    }
}
