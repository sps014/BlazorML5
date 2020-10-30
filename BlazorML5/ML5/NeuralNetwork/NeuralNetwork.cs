using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ML5
{
    //[Obsolete("Please consider using ML.Net model builder instead,never train heavy models on client side with it, heavy perf penality with js interops")]
    public class NeuralNetwork
    {
        public IJSInProcessRuntime Runtime { get; set; }
        public string Hash { get; private set; }

        public DotNetObjectReference<NeuralNetwork> DotNet { get; private set; }

        public NeuralNetwork(IJSInProcessRuntime jSRuntime,int inputs,int outputs)
        {
            Runtime = jSRuntime;
            Hash = Helper.UIDGenerator();
            Init(inputs, outputs);

        }
        private int GetLayersInfo()
        {
            return  Runtime.Invoke<int>("getLayersInfoML5", Hash);
        }
        public NeuralNetwork(IJSInProcessRuntime jSRuntime, NeuralNetworkOptions options)
        {
            Runtime = jSRuntime;
            Hash = Helper.UIDGenerator();
            InitConfig(options);

        }
        private  void Init(int inputs,int outputs)
        {
             Runtime.InvokeVoid("createNNML5", Hash,inputs,outputs,DotNet);
        }
        private  void InitConfig(NeuralNetworkOptions options, bool isCallBack=true)
        {
            DotNet = DotNetObjectReference.Create(this);
            Runtime.InvokeVoid("createNNConfigML5", Hash, options,isCallBack,DotNet);
        }
        ~NeuralNetwork()
        {
            Destroy();
        }
        private  void Destroy()
        {
             Runtime.InvokeVoid("destroyNNML5", Hash);
        }
        public  void AddData(object[] xs,object[] ys)
        {
             Runtime.InvokeVoid("addDataML5",Hash, xs, ys);
        }
        public  void NormalizeData()
        {
             Runtime.InvokeVoid("normalizeDataML5", Hash);
        }
        /// <summary>
        /// Start training model
        /// </summary>
        /// <param name="trainingOptions"></param>
        /// <param name="subscribeCallBack">enable callbacks for whileTraining [False][costly operation instead use debug flag on initial neural network]</param>
        public  void Train(TrainingOptions trainingOptions=null,bool subscribeCallBack=false)
        {
             Runtime.InvokeVoid("trainML5", Hash, DotNet,subscribeCallBack,trainingOptions);
        }
        public  void Predict(object[] inputs)
        {
             Runtime.InvokeVoid("predictML5", Hash, DotNet, inputs);
        }
        public  void Classify(object[] inputs)
        {
             Runtime.InvokeVoid("classifyML5", Hash, DotNet, inputs);
        }
        public  void SaveData(string path=null)
        {
             Runtime.InvokeVoid("saveDataML5", Hash, DotNet,path);
        }
        public  void LoadData(string path = null)
        {
             Runtime.InvokeVoid("loadDataML5", Hash, DotNet, path);
        }
        public  void Save(string path = null)
        {
             Runtime.InvokeVoid("saveML5", Hash, DotNet, path);
        }
        public  void Load(string path = null)
        {
             Runtime.InvokeVoid("loadML5", Hash, DotNet, path);
        }
        public  void Load(ModelOptions options)
        {
             Runtime.InvokeVoid("loadML5", Hash, DotNet, null,options);

        }
        public void Print(object obj)
        {
             Runtime.InvokeVoid("print", obj);

        }


        //NeuralNet CallBack Model Load,While Train,Done Training
        [EditorBrowsable(EditorBrowsableState.Never)]
        [JSInvokable("NNCBML")]
        public void __ModelLoaded__()
        {
            OnModelLoaded?.Invoke();

        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        [JSInvokable("NNCBWT")]
        public void __WhileTraining__(int epoch,double loss)
        {

            WhileTraining?.Invoke(epoch, loss);

        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        [JSInvokable("NNCBDT")]
        public void __DoneTraining__()
        {
            OnTrainingComplete?.Invoke();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [JSInvokable("NNCBPD")]
        public void __Predict__(string error,Result[] result)
        {
            OnPredict?.Invoke(error,result);
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        [JSInvokable("NNCBCF")]
        public void __Classify__(string error, CResult[] result)
        {
            OnClassification?.Invoke(error, result);
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        [JSInvokable("NNCBMS")]
        public void __ModelSave__()
        {
            OnSave?.Invoke();
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        [JSInvokable("NNCBMLS")]
        public void __ModelLoad__()
        {
            OnLoad?.Invoke();
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        [JSInvokable("NNCBDS")]
        public void __DataSave__()
        {
            OnDataSave?.Invoke();
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        [JSInvokable("NNCBDL")]
        public void __DataLoad__()
        {
            OnDataLoad?.Invoke();
        }


        public delegate void ModelLoadedHandler();
        /// <summary>
        /// When a given training model configured 
        /// </summary>
        public event ModelLoadedHandler OnModelLoaded;

        public delegate void DoneTrainingHandler();
        public event DoneTrainingHandler OnTrainingComplete;

        public delegate void WhileTrainingHandler(int epoch,double loss);
        public event WhileTrainingHandler WhileTraining;


        public delegate void OnPredictHandler(string error, Result[] result);
        public event OnPredictHandler OnPredict;
        public delegate void OnClassifyHandler(string error, CResult[] result);
        public event OnClassifyHandler OnClassification;


        public delegate void LoadSaveHandler();
        /// <summary>
        /// Load An Existing NN Model
        /// </summary>
        public event LoadSaveHandler OnLoad;
        public event LoadSaveHandler OnSave;
        /// <summary>
        /// When NN Data is loaded
        /// </summary>
        public event LoadSaveHandler OnDataLoad;
        public event LoadSaveHandler OnDataSave;

    }
}
