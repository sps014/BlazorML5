let NeuralNetworks = new Object();


window.NeuralNetworkML5 = () => {
    return {
        dispose: function () {
            DotNet.disposeJSObjectReference(this);
        },
        createNetworkFromInput: (h, i, o) =>
        {
            const nn = ml5.neuralNetwork(i, o);
            NeuralNetworks[h] = nn;
            console.log(nn);
        },
        disposeNeuralNetwork: (h) =>
        {
            delete NeuralNetworks[h];
        }
    };
}