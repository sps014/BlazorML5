# ML5-Blazor
 
 ### My attempt to use ML5 library from Blazor WASM (Client Side)

## Features
1. Neural Network 
2. Image Classification
3. Sound Classifier
4. WebCam Helper

## Demo
1. [Color Classifier](https://github.com/sps014/Color-Classifier-Demo-Source) Training in Browser With Neural Network

## Installation
```Nuget
Install-Package BlazorML5 -Version 1.0.0
```

### Usage


```C#
//on top of your  razor file add
@inject IJSRuntime JSRuntime
```

in your index.html in client app

import these libraries

```
<script src="https://unpkg.com/ml5@0.4.3/dist/ml5.min.js"></script>
```
import 1st library or 2nd or both depends on your need.
```
    <script src="_content/BlazorML5/ml5ImageClassifier.js"></script>
    <script src="_content/BlazorML5/ml5NeuralNetwork.js"></script>
```


 #### Neural Network
 1. Creating Neural Network ,you can also visit [ML5 NN Docs](https://learn.ml5js.org/docs/#/reference/neural-network)
 
 ```C# 
         //inputs and outputs
        NeuralNetwork net = new NeuralNetwork(JSRuntime, 2,3);
 ```
 ```C#
 NeuralNetworkOptions options = new NeuralNetworkOptions()
 {
            inputs=new string[] {"dob","age"},
            outputs=new string[] {"gender"},
            hiddenUnits=3,
            task = NetworkTask.classification
};
NeuralNetwork net = new NeuralNetwork(JSRuntime, options);
 ```
  Custom Network Options

 ```C#
 NeuralNetworkOptions options = new NeuralNetworkOptions()
        {
            activationHidden = Activation.relu,
            activationOutput = Activation.linear,
            inputs = 2,
            outputs = 3,
            task = NetworkTask.regression,
            layers = new NetworkLayer[]
            {
            new NetworkLayer()
            {
                type = "dense", units = 12, activation = Activation.relu
            },
            new NetworkLayer()
            {
                type = "dense", units = 12, activation = Activation.sigmoid
            }
            },
            epochs = 32,
            batchSize = 15,
            dataUrl = "url",
            hiddenUnits = 21,
            debug = true,
            modelOptimizer = Optimizers.rmsprop,
            learningRate=0.001,
            modelLoss="meanSquareError",
            modelMetrics= "accuracy"
        };
        
        //Create Object
        NeuralNetwork net = new NeuralNetwork(JSRuntime, options);


 ```
 
 Events for Prediction,Classification, Load and Save Model and Data
 ```C#
 
 //when option in constructor is specified for dataURL
        net.OnModelLoaded += ModelLoaded;
 //when prediction 
        net.Predict(new double[] { 1, 2 });
        net.OnPredict += (err, Results) => { var s = Results[0].value; };

 //when classfication 
        net.Classify(new double[] { 1, 2 });
        net.OnClassification += (err, Results) => { var s = Results[0].label; };

 //when saving data or loading  data
        net.SaveData("optionalPath");
        net.OnDataSave += () => { };
        net.LoadData("Path");
        net.OnDataLoad += () => { };

  //save,load model
        net.Save();
        net.OnSave += () => { };
        net.Load("path");
        net.Load(new ModelOptions { metadata = "", model = "", weights = "" });
        net.OnLoad += () => { };


 
 ```
Training 
```C#
//training with optional training options 
        net.Train(new TrainingOptions() { batchSize=32,epochs=12},true);
        net.OnTrainingComplete += () => { };
        //work only if the 2nd parameter is Train() is true
        net.WhileTraining += (epochs, loss) => { };
```

Adding Data
```
        net.AddData(inputsArray, outputsArray);

```

 #### Image Classifier
 
 1. Creating Classifier
  ```C#
 ImageClassifier classifier = new ImageClassifier(JSRuntime, ImageModel.MobileNet);//BuiltIn Model
 //Custom Teachable Machine
 ImageClassifier classifier = new ImageClassifier(JSRuntime, "path/to/wwwroot/model/or/url"); 
  ```
 2. events
 ```C#
  classifier.OnModelLoad+=ModelLoaded;
         
 /// when Model Loaded
 void ModelLoaded()
 {
      //do prediction
 }
```
3. Classify

```c#
  void ModelLoaded()
    {
    //result
        classifier.OnClassification += GetResult;
        //video or img element,optional no of classes
        classifier.Classify(ElementReference,?noOfClasses);
        //imag data as parameter
        classifier.Classify(imageData);
    }
    void GetResult(string err,CResult[] result)
    {
        var cofidence = result[0].confidence;
        var label = result[0].label;
    }

```
