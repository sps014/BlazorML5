using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ML5
{
    public class NeuralNetwork
    {
        public IJSRuntime Runtime { get; set; }
        public string Hash { get; private set; }
        public Task<Layer[]> Layers
        {
            get
            {
                return GetLayers();
            }
        }
        public DotNetObjectReference<NeuralNetwork> DotNet { get; private set; }

        public NeuralNetwork(IJSRuntime jSRuntime,int inputs,int outputs)
        {
            Runtime = jSRuntime;
            Hash = Helper.UIDGenerator();
            Init(inputs, outputs);

        }
        private async Task<Layer[]> GetLayers()
        {
            int layerCount = await GetLayersInfo();
            List<Layer> layers = new List<Layer>();
            for (int i = 0; i < layerCount; i++)
            {
                layers.Add(new Layer(Runtime, Hash, i));
            }
            return layers.ToArray();
        }
        private async Task<int> GetLayersInfo()
        {
            return await Runtime.InvokeAsync<int>("getLayersInfoML5", Hash);
        }
        public NeuralNetwork(IJSRuntime jSRuntime, NeuralNetworkOptions options)
        {
            Runtime = jSRuntime;
            Hash = Helper.UIDGenerator();
            InitConfig(options);

        }
        private async void Init(int inputs,int outputs)
        {
            await Runtime.InvokeVoidAsync("createNNML5", Hash,inputs,outputs,DotNet);
        }
        private async void InitConfig(NeuralNetworkOptions options, bool isCallBack=true)
        {
            DotNet = DotNetObjectReference.Create(this);
            await Runtime.InvokeVoidAsync("createNNConfigML5", Hash, options,isCallBack,DotNet);
        }
        ~NeuralNetwork()
        {
            Destroy();
        }
        private async void Destroy()
        {
            await Runtime.InvokeVoidAsync("destroyNNML5", Hash);
        }
        public async void AddData(object xs,object ys)
        {
            await Runtime.InvokeVoidAsync("addDataML5",Hash, xs, ys);
        }
        public async void NormalizeData()
        {
            await Runtime.InvokeVoidAsync("normalizeDataML5", Hash);
        }
        /// <summary>
        /// Start training model
        /// </summary>
        /// <param name="trainingOptions"></param>
        /// <param name="subscribeCallBack">enable callbacks for whileTraining</param>
        public async void Train(TrainingOptions trainingOptions=null,bool subscribeCallBack=false)
        {
            await Runtime.InvokeVoidAsync("trainML5", Hash, DotNet,subscribeCallBack,trainingOptions);
        }
        public async void Predict(object inputs)
        {
            await Runtime.InvokeVoidAsync("predictML5", Hash, DotNet, inputs);
        }
        public async void Classify(object inputs)
        {
            await Runtime.InvokeVoidAsync("classifyML5", Hash, DotNet, inputs);
        }
        public async void SaveData(string path=null)
        {
            await Runtime.InvokeVoidAsync("saveDataML5", Hash, DotNet,path);
        }
        public async void LoadData(string path = null)
        {
            await Runtime.InvokeVoidAsync("loadDataML5", Hash, DotNet, path);
        }
        public async void Save(string path = null)
        {
            await Runtime.InvokeVoidAsync("saveML5", Hash, DotNet, path);
        }
        public async void Load(string path = null)
        {
            await Runtime.InvokeVoidAsync("loadML5", Hash, DotNet, path);
        }
        public async void Load(ModelOptions options)
        {
            await Runtime.InvokeVoidAsync("loadML5", Hash, DotNet, null,options);

        }
        public async void Print(object obj)
        {
            await Runtime.InvokeVoidAsync("print", obj);

        }


        //NeuralNet CallBack Model Load,While Train,Done Training
        [JSInvokable("NNCBML")]
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task __ModelLoaded__()
        {
            OnModelLoaded?.Invoke();

        }
        [JSInvokable("NNCBWT")]
        public async Task __WhileTraining__(int epoch,double loss)
        {

            WhileTraining?.Invoke(epoch, loss);

        }
        [JSInvokable("NNCBDT")]
        public async Task __DoneTraining__()
        {
            OnTrainingComplete?.Invoke();
        }

        [JSInvokable("NNCBPD")]
        public async Task __Predict__(string error,Result[] result)
        {
            OnPredict?.Invoke(error,result);
        }
        [JSInvokable("NNCBCF")]
        public async Task __Classify__(string error, CResult[] result)
        {
            OnClassification?.Invoke(error, result);
        }
        [JSInvokable("NNCBMS")]
        public async Task __ModelSave__()
        {
            OnSave?.Invoke();
        }
        [JSInvokable("NNCBMLS")]
        public async Task __ModelLoad__()
        {
            OnLoad?.Invoke();
        }
        [JSInvokable("NNCBDS")]
        public async Task __DataSave__()
        {
            OnDataSave?.Invoke();
        }
        [JSInvokable("NNCBDL")]
        public async Task __DataLoad__()
        {
            OnDataLoad?.Invoke();
        }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously


        public delegate void ModelLoadedHandler();
        /// <summary>
        /// When given training model loads
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
        public event LoadSaveHandler OnLoad;
        public event LoadSaveHandler OnSave;
        /// <summary>
        /// When NN Data is loaded
        /// </summary>
        public event LoadSaveHandler OnDataLoad;
        public event LoadSaveHandler OnDataSave;

    }
}
