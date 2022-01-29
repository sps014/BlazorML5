using BlazorBindGen;
using BlazorML5.Helpers;

namespace BlazorML5.Sound;

public class SoundClassifier
{
    #nullable disable
    private JObjPtr _soundClassifier;
    
    internal SoundClassifier(){}
    internal SoundClassifier Init(JObjPtr soundClassifier)
    {
        _soundClassifier = soundClassifier;
        return this;
    }
    #nullable restore

    public async Task ClassifyAsync(object? audio = null)
    {
        if(audio is null)   
            await _soundClassifier.CallVoidAsync("classify",(JSCallback)OnClassifyCallback);
        else                
            await _soundClassifier.CallVoidAsync("classify", audio,(JSCallback)OnClassifyCallback);
    }
    public delegate void OnModelLoadHandler();
    public event OnModelLoadHandler? OnModelLoad;
    public delegate void OnClassifyHandler(string err, ClassificationResult[] result);
    public event OnClassifyHandler? OnClassify;
    internal void OnModelLoadCallback(JObjPtr[] args)
    {
        OnModelLoad?.Invoke();
    }

    private void OnClassifyCallback(JObjPtr[] args)
    {
        if(OnClassify is null)
            return;
        string err=args[0].To<string>();
        ClassificationResult[] result=args[1].To<ClassificationResult[]>();
        OnClassify.Invoke(err,result);
        
    }
}

public record SoundClassifierOptions(double ProbabilityThreshold);