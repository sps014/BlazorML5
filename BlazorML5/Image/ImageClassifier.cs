using BlazorBindGen;
using BlazorML5.Helpers;

namespace BlazorML5.Image;

public class ImageClassifier
{
    #nullable disable
    private JObjPtr _imageClassifier;
    internal ImageClassifier(){}
    #nullable restore
    
    public ValueTask<string> ModelName=>_imageClassifier.PropValAsync<string>("modelName");
    public ValueTask<string> ModelUrl=>_imageClassifier.PropValAsync<string>("modelUrl");
    internal ImageClassifier Init(JObjPtr imageClassifier)
    {
        _imageClassifier = imageClassifier;
        return this;
    }
    public async Task ClassifyAsync<T>(T input,int? numOfClasses=null)
    {
        if(numOfClasses is null)
            await _imageClassifier.CallVoidAsync("classify",input!,(JSCallback)OnClassifyCallback);
        else
            await _imageClassifier.CallVoidAsync("classify",input!,numOfClasses,(JSCallback)OnClassifyCallback);
    }
    
    public delegate void ModelLoadedHandler();

    public event ModelLoadedHandler? OnModelLoad;
    public  event NeuralNetwork.OnClassifyHandler? OnClassify;
    internal void OnModelLoadCallback(JObjPtr[] _)
    {
            OnModelLoad?.Invoke();
    }
    private void OnClassifyCallback(JObjPtr[] args)
    {
        if(OnClassify is null)  return;
        OnClassify.Invoke(args[0].To<string>()
            ,args[1].To<ClassificationResult[]>());
    }
}
public enum ImageModel
{
    MobileNet,
    Darknet,
    DarknetTiny,
    DoodleNet
}

public static class ImageModelExtension
{
    public  static string GetName(this ImageModel model)
    {
        switch (model)
        {
            case ImageModel.MobileNet:
                return "MobileNet";
            case ImageModel.Darknet:
                return "Darknet";
            case ImageModel.DarknetTiny:
                return "Darknet-tiny";
            case ImageModel.DoodleNet:
                return "DoodleNet";
            default:
                throw new ArgumentOutOfRangeException(nameof(model), model, null);
        }
    }
}

public record ImageClassifierOptions(int Version=1,double Alpha=1.0,int TopK=3);