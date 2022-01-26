using BlazorBindGen;

namespace BlazorML5.Image;

public class PoseNet
{
    #nullable disable
    private JObjPtr _poseNet;
    internal  PoseNet(){}
    internal async Task<PoseNet> InitAsync(JObjPtr poseNet)
    {
        _poseNet = poseNet;
        await _poseNet.CallVoidAsync("on", "pose", (JSCallback) OnPoseCallback);
        return this;
    }
    /// <summary>
    /// Get Pose Estimation
    /// </summary>
    /// <param name="canvasOrVideoOrImage">A HTML video or image element or a p5 image or video element. If no input is provided, the default is to use the video given in the constructor.</param>
    /// <returns></returns>
    public async  Task SinglePoseAsync(object canvasOrVideoOrImage=null)
    {
        if(canvasOrVideoOrImage is not null)
            await _poseNet.CallVoidAsync("singlePose",canvasOrVideoOrImage);
        else
            await _poseNet.CallVoidAsync("singlePose");

    }
    /// <summary>
    /// Get multiple poses
    /// </summary>
    /// <param name="canvasOrVideoOrImage">A HTML video or image element or a p5 image or video element. If no input is provided, the default is to use the video given in the constructor.</param>
    /// <returns></returns>
    public async Task MultiPoseAsync(object canvasOrVideoOrImage = null)
    {
        if(canvasOrVideoOrImage is not null)
            await _poseNet.CallVoidAsync("multiPose",canvasOrVideoOrImage);
        else
            await _poseNet.CallVoidAsync("multiPose");
    }
#nullable restore

    public delegate void OnModelLoadHandler();
    public event OnModelLoadHandler? OnModelLoad;
    public delegate void OnPoseHandler(IReadOnlyList<PoseResult> result);
    public event OnPoseHandler? OnPose;
    internal void OnModelLoadCallback(JObjPtr[] _)
    {
        OnModelLoad?.Invoke();
    }
    private void OnPoseCallback(JObjPtr[] args)
    {
        OnPose?.Invoke(args[0].To<IReadOnlyList<PoseResult>>());
    }
}