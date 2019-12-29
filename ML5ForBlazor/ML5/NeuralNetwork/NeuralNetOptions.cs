using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ML5
{
    public class NeuralNetworkOptions
    {
        public string dataUrl { get; set; }
        public string task { get; set; }
        public string activationHidden { get; set; }
        public string activationOutput { get; set; }
        public bool debug { get; set; }
        public double learningRate { get; set; } = 0.25;
        public object inputs { get; set; }
        public object outputs { get; set; }
        public object noVal { get; set; }
        public int hiddenUnits { get; set; }
        public object modelMetrics { get; set; }
        public string modelLoss {get;set;}
        public object modelOptimizer { get; set; }
        public int batchSize { get; set; } = 64;
        public int epochs { get; set; } = 32;

        public enum Task
        {
            regression,
            classification
        }
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
}
