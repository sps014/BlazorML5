using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using BlazorBindGen;
using Microsoft.JSInterop;

namespace BlazorML5;

public static class ML5
{
    /// <summary>
    /// Url of ML5 library
    /// </summary>
    private const string CdnMl5 = "https://unpkg.com/ml5@latest/dist/ml5.min.js";

    #nullable disable
    /// <summary>
    /// Represent Ml5 object pointer in js runtime
    /// </summary>
    internal static JObjPtr Ml5Ptr { get; private set; }
    
    #nullable restore
    /// <summary>
    /// Initialize ML5 library
    /// </summary>
    /// <param name="runtime">Get reference to current pages IJSRuntime  `@inject IJSRuntime runtime` and pass runtime here</param>
    public static async ValueTask InitAsync(IJSRuntime runtime)
    {
        await BindGen.InitAsync(runtime);
        await BindGen.ImportAsync(CdnMl5);
        Ml5Ptr=await BindGen.Window.PropRefAsync("ml5");
    }
   
}