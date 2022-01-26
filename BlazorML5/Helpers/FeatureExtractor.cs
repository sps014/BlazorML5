using BlazorBindGen;

namespace BlazorML5.Helpers;

public class FeatureExtractor
{
    #nullable disable
    private JObjPtr _featureExtractor;
    
    public ValueTask<bool> ModelLoaded => _featureExtractor.PropValAsync<bool>("modelLoaded");
    public  ValueTask<bool> HasAnyTrainedClass => _featureExtractor.PropValAsync<bool>("hasAnyTrainedClass");
    public ValueTask<string> UsageType=> _featureExtractor.PropValAsync<string>("usageType");
    public ValueTask<bool> IsPredicting => _featureExtractor.PropValAsync<bool>("isPredicting");
    
    internal FeatureExtractor(){}
    #nullable restore
    internal FeatureExtractor Init(JObjPtr ptr)
    {
        _featureExtractor = ptr;
        return this;
    }
    /// <summary>
    /// Get a new classification based feature extractor
    /// </summary>
    /// <param name="video">optional video element or ElementReference</param>
    /// <returns></returns>
    public async Task<FeatureExtractor> ClassificationAsync(object? video = default)
    {
        JObjPtr nPtr;
        var n = new FeatureExtractor();
        if (video is not null)
            nPtr=await _featureExtractor.CallRefAsync("classification", video,(JSCallback)n.OnVideoLoadedCallback);
        else
            nPtr=await _featureExtractor.CallRefAsync("classification",(JSCallback)n.OnVideoLoadedCallback);
        return n.Init(nPtr);
    }
    /// <summary>
    /// Get a new regression based feature extractor
    /// </summary>
    /// <param name="video">optional video element or ElementReference</param>
    /// <returns></returns>
    public async Task<FeatureExtractor> RegressionAsync(object? video = default)
    {
        JObjPtr nPtr;
        var n = new FeatureExtractor();
        if (video is not null)
            nPtr=await _featureExtractor.CallRefAsync("regression", video,(JSCallback)n.OnVideoLoadedCallback);
        else
            nPtr=await _featureExtractor.CallRefAsync("regression",(JSCallback)n.OnVideoLoadedCallback);
        return n.Init(nPtr);
    }
    
    /// <summary>
    /// Adds a new image element to the featureExtractor for training
    /// </summary>
    /// <param name="label"> The label to associate the new image with. When using the classifier this can be strings or numbers. For a regression, this needs to be a number.</param>
    /// <param name="input">Optional. An HTML image or video element or a p5 image or video element. If not input is provided, the video element provided in the method-type will be used.</param>
    /// <typeparam name="T"></typeparam>
    public async Task AddImageAsync<T>(T label, object? input = default)
    {
        if(input is null)
            await _featureExtractor.CallRefAsync("addImage", label!,(JSCallback)OnImageAddedCallback);
        else
            await  _featureExtractor.CallVoidAsync("addImage", label!, input,(JSCallback)OnImageAddedCallback);
    }
    /// <summary>
    /// Start training the feature extractor
    /// </summary>
    public async Task TrainAsync()
    {
        await _featureExtractor.CallVoidAsync("train",(JSCallback)WhileTrainingCallback);
    }
    
    #nullable disable
    /// <summary>
    /// Get result of classification
    /// </summary>
    /// <param name="input">Optional. An HTML image or video element or a p5 image or video element. If not input is provided, the video element provided in the method-type will be used.</param>
    /// <returns></returns>
    public async Task ClassifyAsync(object input = null)
    {
        if (input is null)
            await _featureExtractor.CallVoidAsync("classify",(JSCallback)OnClassifyCallback);
        else
            await _featureExtractor.CallVoidAsync("classify", input,(JSCallback)OnClassifyCallback);
    }
    /// <summary>
    /// Get result of regression on a video
    /// </summary>
    /// <param name="input">Optional. An HTML image or video element or a p5 image or video element. If not input is provided, the video element provided in the method-type will be used.</param>
    /// <returns></returns>
    public async Task PredictAsync(object input = null)
    {
        if (input is null)
            await _featureExtractor.CallVoidAsync("predict",(JSCallback)OnPredictionCallback);
        else
            await _featureExtractor.CallVoidAsync("predict", input,(JSCallback)OnPredictionCallback);
    }
    /// <summary>
    /// [Undocumented] Get features image snapshots of video
    /// </summary>
    /// <param name="input">input video</param>
    /// <returns>Logit object</returns>
    public async Task<Logits> InferAsync<T>(T input)
    {
        JObjPtr ptr= await _featureExtractor.CallRefAsync("infer", input);
        return new Logits(ptr);
    }
    #nullable restore
    
   
    public async Task SaveAsync(string? name=null)
    {
        if (name is null)
            await _featureExtractor.CallVoidAsync("save",(JSCallback)SaveCallback);
        else
            await _featureExtractor.CallVoidAsync("save",(JSCallback)SaveCallback, name);
    }
    
    public async Task LoadAsync(string? name=null)
    {
        if (name is null)
            await _featureExtractor.CallVoidAsync("load",(JSCallback)LoadCallback);
        else
            await _featureExtractor.CallVoidAsync("load",name,(JSCallback)LoadCallback );
    }
    public async Task LoadAsync(string[]? name=null)
    {
        if (name is null)
            await _featureExtractor.CallVoidAsync("load",(JSCallback)LoadCallback);
        else
            await _featureExtractor.CallVoidAsync("load",name,(JSCallback)LoadCallback );
    }
    
    public delegate void ModelLoadedHandler();
    public event ModelLoadedHandler? OnModelLoaded;

    public delegate void VideoLoadedHandler();
    public event VideoLoadedHandler? OnVideoLoaded;
    
    public  delegate void ImageAddedHandler();
    public event ImageAddedHandler? OnImageAdded;
    
    public delegate void OnTrainingHandler(double loss);
    public event OnTrainingHandler? OnTraining;
    
    public  delegate void DoneTrainingHandler();
    public event DoneTrainingHandler? OnTrainingFinished;

    public delegate void SaveHandler();
    public event SaveHandler? OnSave;
    
    public delegate void LoadHandler();
    public event LoadHandler? OnLoad;
    
    public delegate void OnClassificationHandler(ClassificationResult[] result);
    public event OnClassificationHandler? OnClassify;
    public delegate void OnPredictionHandler(PredictionResult result);
    public event OnPredictionHandler? OnPredict;

    private void SaveCallback(JObjPtr[] _)
    {
        OnSave?.Invoke();
    }
    private void LoadCallback(JObjPtr[] _)
    {
        OnLoad?.Invoke();
    }
    private  void OnImageAddedCallback(JObjPtr[] _)
    {
        OnImageAdded?.Invoke();
    }
    private void OnVideoLoadedCallback(JObjPtr[] _)
    {
        OnVideoLoaded?.Invoke();
    }
    internal void OnModelLoadCallback(JObjPtr[] _)
    {
        OnModelLoaded?.Invoke();
    }
    private void OnClassifyCallback(JObjPtr[] args)
    {
        if (OnClassify is null) return;
        OnClassify?.Invoke(args[1].To<ClassificationResult[]>());
    }
    private void OnPredictionCallback(JObjPtr[] args)
    {
        if (OnPredict is null) return;
        OnPredict?.Invoke(args[1].To<PredictionResult>());
    }

    private void WhileTrainingCallback(JObjPtr[] args)
    {
        if(OnTraining is null && OnTrainingFinished is null)
            return;
        var arg0=args[0].To<string>();
        if(string.IsNullOrWhiteSpace(arg0))
            OnTrainingFinished?.Invoke();
        else
        {
            double.TryParse(arg0,out double num);
            OnTraining?.Invoke(num);
        }
    }

}

public record Logits
{
    /// <summary>
    /// Exposes raw js object that represents logits as tensor 
    /// </summary>
    public readonly JObjPtr _logits;
    internal Logits(JObjPtr logits)
    {
        _logits = logits;
    }
}