# ML5 For Blazor
 
 ### My attempt to use ML5 library from Blazor WASM

#### Features
1. Neural Network 
2. Image Classification

#### Demo
1. [Color Classifier](https://github.com/sps014/Color-Classifier-Demo-Source) Training in Browser With Neural Network


#### Usage


 ##### Image Classifier
 
 1. Creating Classifier
  ```C#
 ImageClassifier classifier = new ImageClassifier(jsruntime, ImageModel.MobileNet);//BuiltIn Model
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
