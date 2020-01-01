# ML5 For Blazor
 
 ### My attempt to use ML5 library from Blazor WASM

#### Features
1. Neural Network 
2. Image Classification

#### Demo
1. [Color Classifier](https://github.com/sps014/Color-Classifier-Demo-Source) Training in Browser With Neural Network


#### Usage

```C#
//on top of your  razor file add
@inject IJSRuntime JSRuntime
```

 ##### Neural Network
 1. Creating NN
 ```C#
 
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

 ##### Image Classifier
 
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
