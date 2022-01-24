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
        await _neuralNetwork.SetPropCallBackAsync("callback", (_) => OnModelOrDataLoaded?.Invoke(this));
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
    public async Task AddDataAsync<T,S>(T[] xs, S[] ys)
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

    public async Task TrainAsync(NeuralNetworkTrainOptions? options = default)
    {
        options??=new NeuralNetworkTrainOptions();
        await _neuralNetwork.CallVoidAsync("train"
            ,options,(JSCallback)OnTrainingCallback
            ,(JSCallback)OnTrainingEndCallback);
    }

    public  delegate void OnLoadedHandler(NeuralNetwork neuralNetwork);
    
    /// <summary>
    /// Fires when data or model is loaded and ready to use.
    /// </summary>
    public event  OnLoadedHandler? OnModelOrDataLoaded;

    /// <summary>
    /// Fires when data is loaded and ready to use.
    /// </summary>
    public event OnLoadedHandler? OnDataLoaded;

    public delegate void DoneTrainingHandler();
    public event DoneTrainingHandler? OnTrainingComplete;

    public delegate void WhileTrainingHandler(int epoch,double loss);
    public event WhileTrainingHandler? OnTraining;

    private  void OnDataLoadedCallback(JObjPtr[] _)
    {
        OnDataLoaded?.Invoke(this);
    }
    
    /// <summary>
    /// Called several time during training.
    /// </summary>
    /// <param name="obj">represents parameter</param>
    private async void OnTrainingCallback(JObjPtr[] obj)
    {
        if(OnTraining==null) return;
        var epoch = obj[0].To<int>();
        var loss = await obj[1].PropValAsync<double>("loss");
        OnTraining.Invoke(epoch,loss);
    }
    /// <summary>
    /// Called when finished training.
    /// </summary>
    /// <param name="obj">represents parameter</param>
    private void OnTrainingEndCallback(JObjPtr[] obj)
    {
        OnTrainingComplete?.Invoke();
    }
}