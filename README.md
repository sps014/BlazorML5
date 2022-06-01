# ML5-Blazor
 [![NuGet Package](https://img.shields.io/badge/nuget-v1.0.6%20Preview%204-orange.svg)](https://www.nuget.org/packages/BlazorML5/)
[![NuGet Badge](https://buildstats.info/nuget/BlazorML5)](https://www.nuget.org/packages/BlazorML5)
![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)

  
 ### An Easy Machine Learning Library for Blazor.
 Now supports both Blazor Server and WASM and MAUI Hybrid.

## Current Features
1. Neural Network
2. Image Classification
3. Sound Classifier
4. Object Detector (YOLO and COCOSSD based)
5. PoseNet
6. Sentiment Analyzer
7. FaceMesh

#### Hosted Sample
[Live Demo](https://blazor-ml5-sample.netlify.com/) 


#### Documentation
Install Asp.Net Core payload and then follow [Installation Instructions here](https://github.com/sps014/BlazorML5/wiki/BlazorML5-Installation) to configure ML5 to use it from C# Blazor app.<br>
API documentation can be followed from [here](https://learn.ml5js.org/#/reference/index) after lib configuration.
.

#### Sample Neural Network
```cs
@page "/nn"
@using BlazorML5
@using BlazorML5.Helpers
@inject IJSRuntime runtime

<PageTitle>Index</PageTitle>
<button @onclick="AddData">Add Data and Train</button>
@code
{
    NeuralNetwork _network;
    protected override async Task OnInitializedAsync()
    {
        await Ml5.InitAsync(runtime);

        _network = await Ml5.NeuralNetworkAsync(new NeuralNetworkOptions()
        {
            Task = TaskType.Classification,
            DataUrl = "https://raw.githubusercontent.com/ml5js/ml5-library/main/examples/p5js/NeuralNetwork/NeuralNetwork_color_classifier/data/colorData_small.json",
            Debug = true,
            Inputs = new object[]{"r", "g", "b"},
            Outputs = new object[]{"label"}
        });
        _network.OnTraining+=(l,e)=>
        {
            Console.WriteLine($"Training: {e}%");
        };
        _network.OnTrainingComplete+=async ()=>
        {
            Console.WriteLine($"Training Complete");
            await _network.ClassifyMultipleAsync(new object[]{new object[]{12,13,14},new object[]{15,16,17}});
        };
        _network.OnDataLoaded+=(e)=>
        {
            Console.WriteLine($"Data Loaded");
        };
        _network.OnClassify+=async (l,e)=>
        {
            Console.WriteLine(e.Length);
            Console.WriteLine(e[0].Label);
        };
        _network.OnClassifyMultiple+=async (l,e)=>
        {
            Console.WriteLine(e.Length);
            Console.WriteLine(e[0].Length);
            Console.WriteLine(e[0][0].Label);
        };

    }
    async void AddData()
    {
        //data fetched from .csv file no need to manually add and hence directly training 
        //await _network.NormalizeDataAsync();
        await _network.TrainAsync();
    }

}
```

More samples [here](https://github.com/sps014/BlazorML5/tree/master/SampleApp/Pages)
