using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorML5
{
    public class NeuralNetworkOptions
    {
        [JsonPropertyName("inputs")]
        public List<object> Inputs { get; set; }

        [JsonPropertyName("outputs")]
        public List<object> Outputs { get; set; }

        [JsonPropertyName("dataUrl")]
        public string DataUrl { get; set; }

        [JsonPropertyName("modelUrl")]
        public string ModelUrl { get; set; }

        [JsonPropertyName("layers")]
        public List<object> Layers { get; set; }

        [JsonPropertyName("task")]
        public object Task { get; set; }

        [JsonPropertyName("debug")]
        public bool Debug { get; set; }

        [JsonPropertyName("learningRate")]
        public double LearningRate { get; set; }

        [JsonPropertyName("hiddenUnits")]
        public int HiddenUnits { get; set; }

        [JsonPropertyName("activationHidden")]
        public string ActivationHidden { get; set; }

        [JsonPropertyName("activationOutput")]
        public string ActivationOutput { get; set; }

        [JsonPropertyName("batchSize")]
        public int BatchSize { get; set; } = 32;

        [JsonPropertyName("epochs")]
        public int Epochs { get; set; } = 64;

        [JsonPropertyName("modelMetrics")]
        public string ModelMetrics { get; set; }


    }
    public static class Activation
    {
        public const string Elu = "elu";
        public const string Selu = "selu";
        public const string Relu = "relu";
        public const string Relu6 = "relu6";
        public const string Linear = "linear";
        public const string Sigmoid = "sigmoid";
        public const string HardSigmoid = "hardSigmoid";
        public const string Softplus = "softplus";
        public const string Softsign = "softsign";
        public const string Tanh = "tanh";
        public const string Softmax = "softmax";
        public const string LogSoftmax = "logSoftmax";
        public const string Swish = "swish";


    }
    public static class Metric
    {
        public const string BinaryAccuracy= "binaryAccuracy";
        public const string BinaryCrossentropy = "binaryCrossentropy";
        public const string CategoricalAccuracy = "categoricalAccuracy";
        public const string CategoricalCrossentropy = "categoricalCrossentropy";
        public const string CosineProximity = "cosineProximity";
        public const string MeanAbsoluteError = "meanAbsoluteError";
        public const string MeanAbsolutePercentageError = "meanAbsolutePercentageError";
        public const string MeanSquaredError = "meanSquaredError";
        public const string Precision = "precision";
        public const string SparseCategoricalAccuracy = "sparseCategoricalAccuracy";
        public const string Recall = "recall";
    }

    public class NeuralNetworkOptions
    {
        public string dataUrl { get; set; }
        public NetworkTask task { get; set; } = NetworkTask.regression;
        public Activation activationHidden { get; set; } = Activation.none;
        public Activation activationOutput { get; set; } = Activation.none;
        public bool debug { get; set; }
        public object learningRate { get; set; }
        public object inputs { get; set; }
        public object outputs { get; set; }
        public object noVal { get; set; }
        public object hiddenUnits { get; set; }
        public object modelMetrics { get; set; }
        public string modelLoss {get;set;}
        public Optimizers modelOptimizer { get; set; } = Optimizers.none;
        public int batchSize { get; set; } = 32;
        public int epochs { get; set; } = 64;
        public NetworkLayer[] layers { get; set; }
    }
    public enum NetworkTask
    {
        regression,
        classification,
        imageClassification
    }
    public class NetworkLayer
    {
        /// <summary>
        /// layer name eg. "dense"
        /// </summary>
        public string type { get; set; }
        public int units { get; set; }
        public Activation activation { get; set; }
    }

    public class TrainingOptions
    {
        public int batchSize { get; set; } = 64;
        public int epochs { get; set; } = 32;
    }
    public class Result
    {
        public double value { get; set; }
        public string label { get; set; }
    }
    public class CResult
    {
        public double confidence { get; set; }
        public string label { get; set; }
    }
    public class ModelOptions
    {
        public string model { get; set; }
        public string metadata { get; set; }
        public string weights { get; set; }

    }
    public enum Activation
    {
        elu,
        hardSigmoid,
        linear,
        relu,
        relu6,
        selu,
        sigmoid,
        softmax,
        softplus,
        softsign,
        tanh,
        none
    }
    public enum Optimizers
    {
        sgd,
        momentum,
        adagrad,
        adadelta,
        adam,
        adamax,
        rmsprop,
        none
    }

}

