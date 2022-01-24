using BlazorBindGen;
using Microsoft.JSInterop;

namespace BlazorML5;

public static class ML5
{
    internal const string CdnMl5 = "https://unpkg.com/ml5@latest/dist/ml5.min.js";
    public static async ValueTask InitAsync(IJSRuntime runtime)
    {
        await BindGen.InitAsync(runtime);
        await BindGen.ImportAsync(CdnMl5);
    }
}