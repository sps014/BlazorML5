let NeuralNetworks = new Object();

function createNNML5(hash, inputs, outputs)
{
    let nn = ml5.neuralNetwork(inputs, outputs);
    NeuralNetworks[hash] = nn;
}

function createNNConfigML5(hash,config,isCallBack,dotNet)
{
    //clean up config
    Object.keys(config).forEach((key) => (config[key] == null) && delete config[key]);

    let nn;
    if (isCallBack == true)
        nn = ml5.neuralNetwork(config, ml5ModelLoaded.bind(dotNet));
    else
        nn = ml5.neuralNetwork(config);
    NeuralNetworks[hash] = nn;
}

function ml5ModelLoaded()
{
    this.invokeMethodAsync("NNCBML", "__ModelLoaded__");
}
function destroyNNML5(hash)
{
    delete NeuralNetworks[hash];
}
function addDataML5(hash, xs, ys)
{
    NeuralNetworks[hash].addData(xs, ys);
}
function normalizeDataML5(hash)
{
    NeuralNetworks[hash].normalizeData();
}
function trainML5(Hash, DotNet,subCallBack, trainingOptions)
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
function predictML5(hash,dotnet,inputs)
{
    NeuralNetworks[hash].predict(inputs, ml5Predict.bind(dotnet));
}
function ml5Predict(err, result)
{
    console.log(result);
    this.invokeMethodAsync("NNCBPD", err, result);
}