using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using BlazorBindGen;
using BlazorML5.Helpers;
using BlazorML5.Text;
using Microsoft.JSInterop;

namespace BlazorML5;

public static class Ml5
{
    /// <summary>
    /// Url of ML5 library
    /// </summary>
    private const string CdnMl5 = "https://unpkg.com/ml5@latest/dist/ml5.min.js";

    #nullable disable
    /// <summary>
    /// Represent Ml5 object pointer in js runtime
    /// </summary>
    private static JObjPtr Ml5Ptr { get; set; }
    
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

    /// <summary>
    /// Creates a new neural network with options like input,output,hidden layers, etc.
    /// </summary>
    /// <param name="options">Specify configuration of neural network</param>
    /// <returns></returns>
    public static async Task<NeuralNetwork> NeuralNetworkAsync(NeuralNetworkOptions? options = null)
    {
        options ??= new NeuralNetworkOptions();
        var nn = new NeuralNetwork();
        var nnPtr = await Ml5Ptr.CallRefAsync("neuralNetwork", await options.EliminateNullPropObject()
        , (JSCallback)nn.OnDataLoadedCallback);
        return await nn.InitAsync(nnPtr);
    }
    /// <summary>
    /// Create a feature extractor
    /// </summary>
    /// <param name="model">model name or url</param>
    /// <param name="options">configuration of model like learning rate etc.</param>
    /// <returns></returns>

    public static async Task<FeatureExtractor> FeatureExtractorAsync(string model = "MobileNet", object? options=null)
    {
        var fe = new FeatureExtractor();
       JObjPtr ptr;
           if(options is null)
               ptr=await Ml5.Ml5Ptr.CallRefAsync("featureExtractor",model,(JSCallback)fe.OnModelLoadCallback);
           else
               ptr=await Ml5.Ml5Ptr.CallRefAsync("featureExtractor",model,options,(JSCallback)fe.OnModelLoadCallback);
       return fe.Init(ptr);
    }
    /// <summary>
    /// Create a KNN classifier
    /// </summary>
    /// <returns></returns>
    public static async Task<KnnClassifier> KnnClassifierAsync()
    {
        var ptr = await Ml5Ptr.CallRefAsync("KNNClassifier");
        return new KnnClassifier(ptr);
    }

    /// <summary>
    /// Create a new Sentiment Analysis Classifier
    /// </summary>
    /// <param name="model">url of the model , defaults to movie review model</param>
    /// <returns></returns>
    public static async Task<Sentiment> SentimentAsync(string model = "movieReviews")
    {
        Sentiment ss = new();
        var sPtr = await Ml5.Ml5Ptr.CallRefAsync("sentiment", model, (JSCallback)ss.OnModelLoadedCallback);
        return ss.Init(sPtr);
    }

}