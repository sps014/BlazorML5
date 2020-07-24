let PoseNets = new Object();

function poseNetML5(hash, dotnet, video = null, options = null, type = null)
{
    const poseNet;
    if (video != null) {
        if (options != null) {
            poseNet = ml5.poseNet(video, options, poseNetModelLoad.bind(dotnet));
        }
        else if (type != null) {
            poseNet = ml5.poseNet(video, type, poseNetModelLoad.bind(dotnet));
        }
        else {
            poseNet = ml5.poseNet(video , poseNetModelLoad.bind(dotnet));
        }
    }
    poseNet = ml5.poseNet(?video, ?type, ?callback);

}
function poseNetModelLoad() {
    console.log("Loaded model");
}