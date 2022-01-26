using System.Text.Json.Serialization;
using BlazorBindGen;
using BlazorML5.Text;

namespace BlazorML5.Image;

public class ObjectDetector
{
    #nullable disable
    private JObjPtr _objectDetector;
    internal ObjectDetector()
    {
    }
    public  ObjectDetector Init(JObjPtr ptr)
    {
        _objectDetector = ptr;
        return this;
    }
#nullable restore
    
    

    public async Task DetectAsync<T>(T input)
    {
        await _objectDetector.CallVoidAsync("detect",input!,(JSCallback)OnDetectCallback);
    }

    public delegate void OnModelLoadHandler();
    public event OnModelLoadHandler? OnModelLoad;
    public delegate void OnDetectHandler(string err, ObjectResult[] result);
    public event OnDetectHandler? OnDetect;
    internal void OnModelLoadCallback(JObjPtr[] obj)
    {
        OnModelLoad?.Invoke();
    }

    private void OnDetectCallback(JObjPtr[] args)
    {
        if (OnDetect is null) return;
        var err = args[0].To<string>();
        var res=args[1].To<ObjectResult[]>();
        OnDetect?.Invoke(err,res);
    }


}

public  record ObjectDetectorOptions
{
    public double FilterBoxesThreshold { get; init; } = 0.01;
    [JsonPropertyName("IOUThreshold")]
    public double IouThreshold { get; init; } = 0.4;
    public double ClassProbThreshold { get; init; } = 0.4;
}
public enum ObjectDetectorModel
{
    CocoSsd,
    Yolo
}
public record ObjectBox
{
    public double X { get; init; }
    public double Y { get; init; }
    public double Width { get; init; }
    public double Height { get; init; }
}
public class ObjectResult
{
    public string? Label { get; init; }
    public double X { get; init; }
    public double Y { get; init; }
    public double Width { get; init; }
    public double Height { get; init; }
    public ObjectBox? Normalized { get; init; }
}