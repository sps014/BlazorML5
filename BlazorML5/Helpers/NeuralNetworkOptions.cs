using System.Text.Json.Serialization;
using BlazorBindGen;

namespace BlazorML5.Helpers;

public record NeuralNetworkOptions
{
    /// <summary>
    ///Inputs Can be a number or any array of numbers.
    /// </summary>
    public  object? Inputs { get; init; }
    
    /// <summary>
    /// Outputs can be a number or any array of numbers.
    /// </summary>
    public  object? Outputs { get; init; }

    /// <summary>
    /// Url from where to get the model.
    /// </summary>
    public string? DataUrl { get; init; } = null;
    
    /// <summary>
    /// Url of pretrained model to load.
    /// </summary>
    public string? ModelUrl { get; init; } = null;
    
    /// <summary>
    /// if you want custom layers specify an anonymous object with matching ml5.js custom layer structure.
    /// </summary>
    public  object[]? Layers { get; init; }
    
    /// <summary>
    /// Kind of task this neural Network is used for. 'classification', 'regression', 'imageClassification'
    /// </summary>
    public TaskType? Task { get; init; } = null;
    
    /// <summary>
    /// determines whether or not to show the training visualization
    /// </summary>
    public bool Debug { get; init; } = false;
    
    public double LearningRate { get; init; } = 0.2;
    
    public int HiddenUnits { get; init; } = 16;

    internal async Task<JObjPtr> EliminateNullPropObject()
    {
        var obj = await BindGen.Window.CallRefAsync("Object");
        if(Inputs!=null)  
            await obj.SetPropValAsync("inputs", Inputs);
        if (Outputs != null)
            await obj.SetPropValAsync("outputs", Outputs);
        if (DataUrl != null)
            await obj.SetPropValAsync("dataUrl", DataUrl);
        if(ModelUrl!=null)
            await obj.SetPropValAsync("modelUrl", ModelUrl);
        if(Layers!=null)
            await obj.SetPropValAsync("layers", Layers);
        if(this.Task!=null)
            await obj.SetPropValAsync("task",UtilHelper.FirstCharSmall(Enum.GetName(typeof(TaskType), this.Task)!));
        
        await obj.SetPropValAsync("debug", Debug);
        await obj.SetPropValAsync("learningRate", LearningRate);
        await obj.SetPropValAsync("hiddenUnits", HiddenUnits);
        
        return obj;
    }
}

/// <summary>
/// Represents kind of task that the neural network should perform.
/// </summary>
public  enum  TaskType
{
    Regression,
    Classification,
    ImageClassification
}

public record NeuralNetworkTrainOptions
{
    [JsonPropertyName("batchSize")]
    public int BatchSize { get; init; } = 32;
    [JsonPropertyName("epochs")]
    public int Epochs { get; init; } = 16;
}

public record PredictionResult(double Value, string Label);
public record ClassificationResult(double Confidence,string Label);