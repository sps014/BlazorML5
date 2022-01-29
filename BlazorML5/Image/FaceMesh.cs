using BlazorBindGen;

namespace BlazorML5.Image;

public class FaceMesh
{
#nullable disable
    private JObjPtr _faceMesh;
    internal FaceMesh() { }
    internal FaceMesh Init(JObjPtr faceMesh)
    {
        _faceMesh = faceMesh;
        return this;
    }

#nullable restore

    public async Task PredictAsync(object? media = null)
    {
        if( media == null )
        {
            await _faceMesh.CallVoidAsync("predict",(JSCallback)OnPredictCallback);
        }
        else
        {
            await _faceMesh.CallVoidAsync("predict", media,(JSCallback)OnPredictCallback);
        }
    }

    private void OnPredictCallback(JObjPtr[] args)
    {
        if(OnPredict is null) return;
        var res = args[0].To<FaceResult[]>();
        OnPredict(res);
    }

    public delegate void OnModelLoadHandler();
    public event OnModelLoadHandler? OnModelLoad;
    
    public delegate void OnPredictHandler(FaceResult[] results);
    public event OnPredictHandler? OnPredict;
    
    internal void OnModelLoadCallback(JObjPtr[] args)
    {
        OnModelLoad?.Invoke();
    }
}