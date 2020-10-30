using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ML5
{
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

