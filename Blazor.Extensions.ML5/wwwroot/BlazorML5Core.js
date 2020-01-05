LoadCoreLibraries();

function LoadCoreLibraries()
{
    let Libraries = [];
    Libraries.push('https://unpkg.com/ml5@0.4.3/dist/ml5.min.js');
    Libraries.push('_content/BlazorML5/WebCam.js');
    Libraries.push('_content/BlazorML5/ml5ImageClassifier.js');
    Libraries.push('_content/BlazorML5/ml5NeuralNetwork.js');



    for (var i = 0; i < Libraries.length; i++)
    {
        let imported = document.createElement('script');
        imported.src = Libraries[i];
        document.head.appendChild(imported);
    }
}
