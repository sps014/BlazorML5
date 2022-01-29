using BlazorBindGen;
using BlazorML5.Helpers;
using BlazorML5.Image;
using BlazorML5.Sound;
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
    
    /// <summary>
    ///  is a method to create an object that classifies an image using a pre-trained model.
    /// </summary>
    /// <param name="model">A String value of a valid model OR a url to a model.json that contains a pre-trained model. Case insensitive. Models available are: 'MobileNet', 'Darknet' and 'Darknet-tiny','DoodleNet', or any image classification model trained in Teachable Machine. Below are some examples of creating a new image classifier:</param>
    /// <param name="video"></param>
    /// <param name="options"> An object to change the defaults (shown below). The available options are </param>
    /// <returns></returns>
    public static async Task<ImageClassifier> ImageClassifierAsync(string model,object? video=null, ImageClassifierOptions? options=null)
    {
        var ic = new ImageClassifier();
        JObjPtr ptr;
        if (options is null && video is null)
            ptr = await Ml5.Ml5Ptr.CallRefAsync("imageClassifier", model, (JSCallback)ic.OnModelLoadCallback);
        else if(video is null)
            ptr = await Ml5.Ml5Ptr.CallRefAsync("imageClassifier", model, options!, (JSCallback)ic.OnModelLoadCallback);
        else if(options is null)
            ptr=await Ml5.Ml5Ptr.CallRefAsync("imageClassifier", model, video, (JSCallback)ic.OnModelLoadCallback);
        else 
            ptr = await Ml5.Ml5Ptr.CallRefAsync("imageClassifier", model, video, options, (JSCallback)ic.OnModelLoadCallback);
        return ic.Init(ptr);
    }
    /// <summary>
    ///  is a method to create an object that classifies an image using a pre-trained model.
    /// </summary>
    /// <param name="model">A String value of a valid model OR a url to a model.json that contains a pre-trained model. Case insensitive. Models available are: 'MobileNet', 'Darknet' and 'Darknet-tiny','DoodleNet', or any image classification model trained in Teachable Machine. Below are some examples of creating a new image classifier:</param>
    /// <param name="video"></param>
    /// <param name="options"> An object to change the defaults (shown below). The available options are </param>
    /// <returns></returns>
    public static async Task<ImageClassifier> ImageClassifierAsync(ImageModel model=ImageModel.MobileNet,object? video=null, ImageClassifierOptions? options=null)
    {
        return await ImageClassifierAsync(model.GetName(),video,options);
    }
    /// <summary>
    /// Create an instance of Pose detector
    /// </summary>
    /// <param name="video"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static async Task<PoseNet> PoseNetAsync(object? video=null,PoseNetOptions? options=null)
    {
        var pn = new PoseNet();
        JObjPtr ptr;
        if (options is null && video is null)
            ptr = await Ml5.Ml5Ptr.CallRefAsync("poseNet", (JSCallback)pn.OnModelLoadCallback);
        else if (video is null)
            ptr = await Ml5.Ml5Ptr.CallRefAsync("poseNet", options!, (JSCallback)pn.OnModelLoadCallback);
        else if (options is null)
            ptr = await Ml5.Ml5Ptr.CallRefAsync("poseNet", video, (JSCallback)pn.OnModelLoadCallback);
        else
            ptr = await Ml5.Ml5Ptr.CallRefAsync("poseNet", video, options, (JSCallback)pn.OnModelLoadCallback);
        return await pn.InitAsync(ptr);
    }
    /// <summary>
    /// Create an Object Detector Model with a url to a model.json that contains a pre-trained model.
    /// </summary>
    /// <param name="modelName"></param>
    /// <param name="options"></param>
    /// <returns></returns>

    public static async Task<ObjectDetector> ObjectDetectorAsync(string modelName,ObjectDetectorOptions? options=null)
    {
        var od = new ObjectDetector();
        JObjPtr ptr;
        if (options is null)
            ptr = await Ml5.Ml5Ptr.CallRefAsync("objectDetector", modelName, (JSCallback)od.OnModelLoadCallback);
        else
            ptr = await Ml5.Ml5Ptr.CallRefAsync("objectDetector", modelName, options, (JSCallback)od.OnModelLoadCallback);
        return od.Init(ptr);
    }
    /// <summary>
    /// Create an object detector model with a pre-trained model.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static async Task<ObjectDetector> ObjectDetectorAsync(ObjectDetectorModel model=ObjectDetectorModel.Yolo,ObjectDetectorOptions? options=null)
    {
        return await ObjectDetectorAsync(Enum.GetName(typeof(ObjectDetectorModel),model)!.ToUpper(),options);
    }
    /// <summary>
    /// allows you to classify audio. With the right pre-trained models, you can detect whether a certain noise was made (e.g. a clapping sound or a whistle) or a certain word was said (e.g. Up, Down, Yes, No). At this moment, with the ml5.soundClassifier(), you can use your own custom pre-trained speech commands or use the the "SpeechCommands18w" which can recognize "the ten digits from "zero" to "nine", "up", "down", "left", "right", "go", "stop", "yes", "no", as well as the additional categories of "unknown word" and "background noise"."
    /// </summary>
    /// <param name="modelName"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static async Task<SoundClassifier> SoundClassifierAsync(string modelName="SpeechCommands18w",SoundClassifierOptions? options=null)
    {
        var sc = new SoundClassifier();
        JObjPtr ptr;
        if (options is null)
            ptr = await Ml5.Ml5Ptr.CallRefAsync("soundClassifier", modelName, (JSCallback)sc.OnModelLoadCallback);
        else
            ptr = await Ml5.Ml5Ptr.CallRefAsync("soundClassifier", modelName, options, (JSCallback)sc.OnModelLoadCallback);
        return sc.Init(ptr);
    }
}
