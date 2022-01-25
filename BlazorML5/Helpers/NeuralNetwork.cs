using BlazorBindGen;
using static BlazorML5.ML5;
namespace BlazorML5.Helpers;

public class NeuralNetwork
{
    /// <summary>
    /// Pointer to JS Neural Network Object
    /// </summary>
    private  JObjPtr _neuralNetwork;
    
    /// <summary>
    /// set to true if the model is loaded and ready, false if it is not.
    /// </summary>
    public ValueTask<bool> Ready => _neuralNetwork.PropValAsync<bool>("ready");

    private NeuralNetwork() { }

    internal async Task<NeuralNetwork> InitAsync(JObjPtr neuralNetwork)
    {
        _neuralNetwork = neuralNetwork;
        await _neuralNetwork.SetPropCallBackAsync("callback", (_) => OnDataLoaded?.Invoke(this));
        return this;
    }

    /// <summary>
    /// Creates a new neural network with options like input,output,hidden layers, etc.
    /// </summary>
    /// <param name="options">Specify configuration of neural network</param>
    /// <returns></returns>
    public static async Task<NeuralNetwork> CreateAsync(NeuralNetworkOptions? options=null)
    {
        options ??= new NeuralNetworkOptions();
        var nn = new NeuralNetwork();
        var nnPtr = await Ml5Ptr.CallRefAsync("neuralNetwork",await options.EliminateNullPropObject()
        ,(JSCallback)nn.OnDataLoadedCallback);
        return await nn.InitAsync(nnPtr);
    }
    
    /// <summary>
    /// Add data to the neural network for training purpose.
    /// </summary>
    /// <param name="xs">features can be a array ,number objects</param>
    /// <param name="ys">labels can be array or number</param>
    public async Task AddDataAsync<T,TS>(T[] xs, TS[] ys)
    {
        await _neuralNetwork.CallVoidAsync("addData", xs, ys );
    }
    
    /// <summary>
    /// normalizes the data stored in the neural network.
    /// </summary>
    public async Task NormalizeDataAsync()
    {
        await _neuralNetwork.CallVoidAsync("normalizeData");
    }

    /// <summary>
    /// Start training on store data
    /// </summary>
    /// <param name="options">Specify training options like epochs etc</param>
    public async Task TrainAsync(NeuralNetworkTrainOptions? options = default)
    {
        options??=new NeuralNetworkTrainOptions();
        await _neuralNetwork.CallVoidAsync("train"
            ,options,(JSCallback)OnTrainingCallback
            ,(JSCallback)OnTrainingEndCallback);
    }
    /// <summary>
    /// Start prediction on trained data,subscribe to the event to get the result
    /// </summary>
    /// <param name="xs">input array or object</param>
    public async Task PredictAsync<T>(T[] xs)
    {
        await _neuralNetwork.CallVoidAsync("predict", xs,(JSCallback)OnPredictCallback);
    }
    /// <summary>
    /// Perform multiple prediction on trained data,use only when you have more than 1 objects, subscribe to the event to get the result
    /// </summary>
    /// <param name="xs">array of array or array of objects</param>
    /// <typeparam name="T"></typeparam>
    public async Task PredictMultipleAsync<T>(T[] xs)
    {
        await _neuralNetwork.CallVoidAsync("predictMultiple", xs,(JSCallback)OnPredictMultipleCallback);
    }
    /// <summary>
    /// Perform classification on trained data, subscribe to the event to get the result
    /// </summary>
    /// <param name="xs">array or object </param>
    /// <typeparam name="T"></typeparam>
    public  async Task ClassifyAsync<T>(T[] xs)
    {
        await _neuralNetwork.CallVoidAsync("classify", xs,(JSCallback)OnClassifyCallback);
    }
    /// <summary>
    /// Perform multiple classification on trained data,use only when you have more than 1 objects, subscribe to the event to get the result
    /// </summary>
    /// <param name="xs">array of array or array of objects</param>
    /// <typeparam name="T"></typeparam>
    public async Task ClassifyMultipleAsync<T>(T[] xs)
    {
        await _neuralNetwork.CallVoidAsync("classifyMultiple", xs,(JSCallback)OnClassifyMultipleCallback);
    }

    /// <summary>
    /// Saves the data that has been added
    /// </summary>
    /// <param name="path">:Optional. String. An output name you'd like your data to be called. If no input is given, then the name will be data_YYYY-MM-DD_mm-hh</param>
    public async Task SaveDataAsync(string? path = null)
    {
        if(path is null)
            await _neuralNetwork.CallVoidAsync("saveData",(JSCallback)OnDataSavedCallback);
        else
            await _neuralNetwork.CallVoidAsync("saveData", path,(JSCallback)OnDataSavedCallback);
    }
    /// <summary>
    /// Load the data that has been saved
    /// </summary>
    /// <param name="path">REQUIRED. String | InputFiles. A string path to a .json data object or InputFiles from html input type="file". Must be structured for example as: {"data": [ { xs:{input0:1, input1:2}, ys:{output0:"a"},  ...]}</param>
    public  async Task LoadDataAsync(string path)
    {
        await _neuralNetwork.CallVoidAsync("loadData", path,(JSCallback)OnDataLoadedCallback);
    }
    /// <summary>
    /// Save trained Neural Network Model
    /// </summary>
    /// <param name="outputName"></param>
    public async Task SaveAsync(string? outputName=null)
    {
        if(outputName is null)
            await _neuralNetwork.CallVoidAsync("save",(JSCallback)OnModelSavedCallback);
        else
            await _neuralNetwork.CallVoidAsync("save", outputName,(JSCallback)OnModelSavedCallback);
    }
    public async Task LoadAsync(string path)
    {
        await _neuralNetwork.CallVoidAsync("load", path,(JSCallback)OnModelLoadCallback);
    }

    public  delegate void OnLoadedHandler(NeuralNetwork neuralNetwork);
    

    /// <summary>
    /// Fires when data is loaded and ready to use.
    /// </summary>
    public event OnLoadedHandler? OnDataLoaded;

    public delegate void DoneTrainingHandler();
    
    /// <summary>
    /// Fires when training is done.
    /// </summary>
    public event DoneTrainingHandler? OnTrainingComplete;

    public delegate void WhileTrainingHandler(int epoch,double loss);
    /// <summary>
    /// Fires when training is in progress.
    /// </summary>
    public event WhileTrainingHandler? OnTraining;

    public delegate void OnDataSaveHandler();
    /// <summary>
    /// Fires when data in Neural Network is saved locally.
    /// </summary>
    public event OnDataSaveHandler? OnDataSaved;
    
    public  delegate void OnPredictHandler(string error,PredictionResult[] predictions);
    
    /// <summary>
    /// Fires when prediction is done. (Task Regression)
    /// </summary>
    public event OnPredictHandler? OnPredict;
    public  delegate void OnClassifyHandler(string error,ClassificationResult[] predictions);
    /// <summary>
    /// Fires when classification is done. (Task Classification)
    /// </summary>
    public event OnClassifyHandler? OnClassify;
    public  delegate void OnClassifyMultipleHandler(string error,ClassificationResult[][] predictions);
    /// <summary>
    /// FIres when result of multiple classification is available (Task Classification)
    /// </summary>
    public event OnClassifyMultipleHandler? OnClassifyMultiple;
    public delegate void OnPredictMultipleHandler(string err,PredictionResult[][] predictions);
    /// <summary>
    /// Fires when result of multiple prediction is available (Task Regression)
    /// </summary>
    public event OnPredictMultipleHandler? OnPredictMultiple;
    
    public delegate void OnModelSavedHandler();
    /// <summary>
    /// Fires when model is saved.
    /// </summary>
    public event OnModelSavedHandler? OnModelSaved;
    public  delegate void OnModelLoadHandler();
    public  event  OnModelLoadHandler? OnModelLoaded;
    
    
    private void OnPredictCallback(JObjPtr[] result)
    {
        if(OnPredict==null) return;
        var err=result[0].To<string>();
        var predictions=result[1].To<PredictionResult[]>();
        OnPredict?.Invoke(err,predictions);
    }
    private void OnPredictMultipleCallback(JObjPtr[] result)
    {
        if(OnPredictMultiple==null) return;
        var err=result[0].To<string>();
        var predictions=result[1].To<PredictionResult[][]>();
        OnPredictMultiple?.Invoke(err,predictions);
    }
    private void OnClassifyMultipleCallback(JObjPtr[] result)
    {
        if(OnClassifyMultiple==null) return;
        var err=result[0].To<string>();
        var predictions=result[1].To<ClassificationResult[][]>();
        OnClassifyMultiple?.Invoke(err,predictions);
    }
    private void OnClassifyCallback(JObjPtr[] result)
    {
        if(OnClassify==null) return;
        var err=result[0].To<string>();
        var predictions=result[1].To<ClassificationResult[]>();
        OnClassify?.Invoke(err,predictions);
    }
    private  void OnDataLoadedCallback(JObjPtr[] _)
    {
        OnDataLoaded?.Invoke(this);
    }
    
    private async void OnTrainingCallback(JObjPtr[] obj)
    {
        if(OnTraining==null) return;
        var epoch = obj[0].To<int>();
        var loss = await obj[1].PropValAsync<double>("loss");
        OnTraining.Invoke(epoch,loss);
    }
    private void OnTrainingEndCallback(JObjPtr[] obj)
    {
        OnTrainingComplete?.Invoke();
    }
    private void OnDataSavedCallback(JObjPtr[] obj)
    {
        OnDataSaved?.Invoke();
    }
    private  void OnModelSavedCallback(JObjPtr[] obj)
    {
        OnModelSaved?.Invoke();
    }
    private  void OnModelLoadCallback(JObjPtr[] obj)
    {
        OnModelLoaded?.Invoke();
    }
}