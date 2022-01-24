using System.Text.Json.Serialization;

namespace BlazorML5.Helpers;

public record NeuralNetworkOptions
{
    /// <summary>
    ///Inputs Can be a number or any array of numbers.
    /// </summary>
    [JsonPropertyName("inputs")]
    public  object? Inputs { get; init; }
    
    /// <summary>
    /// Outputs can be a number or any array of numbers.
    /// </summary>
    [JsonPropertyName("outputs")]
    public  object? Outputs { get; init; }

    /// <summary>
    /// Url from where to get the model.
    /// </summary>
    [JsonPropertyName("dataUrl")] 
    public string? DataUrl { get; init; } = null;
    
    /// <summary>
    /// Url of pretrained model to load.
    /// </summary>
    [JsonPropertyName("modelUrl")]
    public string? ModelUrl { get; init; } = null;
    
    /// <summary>
    /// if you want custom layers specify an anonymous object with matching ml5.js custom layer structure.
    /// </summary>
    [JsonPropertyName("layers")]
    public  object[]? Layers { get; init; }
    
    /// <summary>
    /// Kind of task this neural Network is used for. 'classification', 'regression', 'imageClassification'
    /// </summary>
    [JsonPropertyName("task")]
    public TaskType? Task { get; init; } = null;
    
    /// <summary>
    /// determines whether or not to show the training visualization
    /// </summary>
    [JsonPropertyName("debug")]
    public bool Debug { get; init; } = false;
    
    [JsonPropertyName("learningRate")]
    public double LearningRate { get; init; } = 0.2;
    
    [JsonPropertyName("hiddenUnits")]
    public int HiddenUnits { get; init; } = 16;
    
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