let NeuralNetworks = new Object();

export function createNNML5(hash, inputs, outputs,dotnet)
{
    let nn = ml5.neuralNetwork(inputs, outputs, ml5ModelLoaded.bind(dotnet));
    NeuralNetworks[hash] = nn;
}

export function createNNConfigML5(hash,config,isCallBack,dotNet)
{

    //enum handling
    config["task"] = getTask(config["task"]);
    config["activationHidden"] = getActivation(config["activationHidden"]);
    config["activationOutput"] = getActivation(config["activationOutput"]);
    config["modelOptimizer"] = getOptimizer(config["modelOptimizer"]);


    //set enum for layers if supplied
    if (config["layers"] != null)
    {
        for (var i = 0; i < config["layers"].length; i++)
        {
            config["layers"][i]["activation"] = getActivation(config["layers"][i]["activation"]);
        }
    }

    //clean up config
    Object.keys(config).forEach((key) => (config[key] == null) && delete config[key]);

    let nn;
    if (isCallBack == true)
        nn = ml5.neuralNetwork(config, ml5ModelLoaded.bind(dotNet));
    else
        nn = ml5.neuralNetwork(config);
    NeuralNetworks[hash] = nn;
}


function getTask(num)
{
    switch (num)
    {
        case 0:
            return "regression";
        case 2:
            return "imageClassification";
        default:
            return "classification";
    }
}

function getActivation(num)
{
    switch (num)
    {
        case 0:
            return 'elu';
        case 1:
            return 'hardSigmoid';
        case 2:
            return 'linear';
        case 3:
            return 'relu';
        case 4:
            return 'relu6';
        case 5:
            return 'selu';
        case 6:
            return 'sigmoid';
        case 7:
            return 'softmax';
        case 8:
            return 'softplus';
        case 9:
            return 'softsign';
        case 10:
            return 'tanh';
        default:
            return null;
    }
}

function getOptimizer(num)
{
    switch (num) {
        case 0:
            return ml5.tf.train.sgd(0.25);
        case 1:
            return ml5.tf.train.momentum(0.25,0.1);
        case 2:
            return ml5.tf.train.adagrad(0.25);
        case 3:
            return ml5.tf.train.adadelta();
        case 4:
            return ml5.tf.train.adam();
        case 5:
            return ml5.tf.train.adamax();
        case 6:
            return ml5.tf.train.rmsprop(0.25);
        default:
            return null;
    }
}







function ml5ModelLoaded()
{
    this.invokeMethodAsync("NNCBML", "__ModelLoaded__");
}
export function destroyNNML5(hash)
{
    delete NeuralNetworks[hash];
}
export function addDataML5(hash, xs, ys)
{
   NeuralNetworks[hash].addData(xs, ys);
}
export function normalizeDataML5(hash)
{
    NeuralNetworks[hash].normalizeData();
}
export function trainML5(Hash, DotNet,subCallBack, trainingOptions)
{
    if (trainingOptions != null && subCallBack == true)
    {
        NeuralNetworks[Hash].train(trainingOptions, ml5WhileTraining.bind(DotNet), ml5DoneTraining.bind(DotNet));
    }
    else if (trainingOptions != null && subCallBack == false)
    {
        NeuralNetworks[Hash].train(trainingOptions, ml5DoneTraining.bind(DotNet));
    }
    else if (subCallBack == true)
    {
        NeuralNetworks[Hash].train(ml5WhileTraining.bind(DotNet), ml5DoneTraining.bind(DotNet));
    }
    else
    {
        NeuralNetworks[Hash].train(ml5DoneTraining.bind(DotNet));
    }
}
function ml5WhileTraining(epoch, loss)
{
    this.invokeMethodAsync("NNCBWT", epoch, loss.loss);
}
function ml5DoneTraining()
{
    this.invokeMethodAsync("NNCBDT", "__DoneTraining__");
}
export function predictML5(hash,dotnet,inputs)
{
        NeuralNetworks[hash].predict(inputs, ml5Predict.bind(dotnet));
}
export function ml5Predict(err, result)
{
    if (err != null) {
        console.error(err);
        return;
    }
    this.invokeMethodAsync("NNCBPD", err, result);
}
export function classifyML5(hash, dotnet, inputs)
{
    NeuralNetworks[hash].classify(inputs, ml5classify.bind(dotnet));
}
function ml5classify(err, result)
{

    if (err != null) {
        console.error(err);
        return;
    }
    this.invokeMethodAsync("NNCBCF", err, result);
}





export function loadML5(hash, dotnet, path, options)
{
    if (options != null)
    {
        NeuralNetworks[hash].load(options, ml5Load.bind(dotnet));
    }
    else 
    {
        NeuralNetworks[hash].load(path, ml5Load.bind(dotnet));
    }
}
function ml5Load()
{
    this.invokeMethodAsync("NNCBMLS","__ModelLoad__");
}

export function saveML5(hash, dotnet, path)
{
    if (path != null)
        NeuralNetworks[hash].save(path, ml5Save.bind(dotnet));
    else 
        NeuralNetworks[hash].save(ml5Save.bind(dotnet));

}
function ml5Save()
{
    this.invokeMethodAsync("NNCBMS", "__ModelSave__");
}
export function loadDataML5(hash, dotnet, path)
{
    NeuralNetworks[hash].save(path, ml5Save.bind(dotnet));
}
function ml5loadData()
{
    this.invokeMethodAsync("NNCBDL","__DataLoad__");
}
export function saveDataML5(hash, dotnet, path)
{
    if (path != null)
        NeuralNetworks[hash].saveData(path, ml5SaveData.bind(dotnet));
    else
        NeuralNetworks[hash].saveData(ml5SaveData.bind(dotnet));

}
function ml5SaveData()
{
    this.invokeMethodAsync("NNCBDS", "__DataSave__");
}

















function getLayersInfoML5(hash)
{
    return NeuralNetworks[hash].model.layers.length;

}
function getWeightsML5(hash, layerNo)
{
    let wts = NeuralNetworks[hash].model.layers[layerNo].getWeights()[0];

    let wtVal = [];
    let tData = wts.dataSync();
    for (var i = 0; i < tData.length; i++)
    {
        wtVal.push(tData[i]);
    }
    let payload =
    {
        Data: wtVal,
        Shape:wts.shape
    }

    return payload;
}
function getBiasML5(hash, layerNo)
{
    let wts = NeuralNetworks[hash].model.layers[layerNo].getWeights()[1];

    let wtVal = [];
    let tData = wts.dataSync();
    for (var i = 0; i < tData.length; i++) {
        wtVal.push(tData[i]);
    }
    let payload =
    {
        Data: wtVal,
        Shape: [wts.shape[0],1]
    }
    return payload;
}
function setWeightsML5(hash, layerNo, array,r,c,bias)
{
    ml5.tf.tidy(() =>
    {
        NeuralNetworks[hash].model.layers[layerNo].setWeights([ml5.tf.tensor2d(array, shape = [r, c]), ml5.tf.tensor1d(bias)]);
    });
}









function print(obj)
{
    console.log(obj);
}