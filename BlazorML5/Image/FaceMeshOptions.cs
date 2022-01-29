using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorML5.Image;

// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
public class BoundingBox
{
    [JsonPropertyName("topLeft")]
    public List<List<double>> TopLeft { get; set; }

    [JsonPropertyName("bottomRight")]
    public List<List<double>> BottomRight { get; set; }
}

public class Annotations
{
    [JsonPropertyName("silhouette")]
    public List<List<double>> Silhouette { get; set; }

    [JsonPropertyName("lipsUpperOuter")]
    public List<List<double>> LipsUpperOuter { get; set; }

    [JsonPropertyName("lipsLowerOuter")]
    public List<List<double>> LipsLowerOuter { get; set; }

    [JsonPropertyName("lipsUpperInner")]
    public List<List<double>> LipsUpperInner { get; set; }

    [JsonPropertyName("lipsLowerInner")]
    public List<List<double>> LipsLowerInner { get; set; }

    [JsonPropertyName("rightEyeUpper0")]
    public List<List<double>> RightEyeUpper0 { get; set; }

    [JsonPropertyName("rightEyeLower0")]
    public List<List<double>> RightEyeLower0 { get; set; }

    [JsonPropertyName("rightEyeUpper1")]
    public List<List<double>> RightEyeUpper1 { get; set; }

    [JsonPropertyName("rightEyeLower1")]
    public List<List<double>> RightEyeLower1 { get; set; }

    [JsonPropertyName("rightEyeUpper2")]
    public List<List<double>> RightEyeUpper2 { get; set; }

    [JsonPropertyName("rightEyeLower2")]
    public List<List<double>> RightEyeLower2 { get; set; }

    [JsonPropertyName("rightEyeLower3")]
    public List<List<double>> RightEyeLower3 { get; set; }

    [JsonPropertyName("rightEyebrowUpper")]
    public List<List<double>> RightEyebrowUpper { get; set; }

    [JsonPropertyName("rightEyebrowLower")]
    public List<List<double>> RightEyebrowLower { get; set; }

    [JsonPropertyName("leftEyeUpper0")]
    public List<List<double>> LeftEyeUpper0 { get; set; }

    [JsonPropertyName("leftEyeLower0")]
    public List<List<double>> LeftEyeLower0 { get; set; }

    [JsonPropertyName("leftEyeUpper1")]
    public List<List<double>> LeftEyeUpper1 { get; set; }

    [JsonPropertyName("leftEyeLower1")]
    public List<List<double>> LeftEyeLower1 { get; set; }

    [JsonPropertyName("leftEyeUpper2")]
    public List<List<double>> LeftEyeUpper2 { get; set; }

    [JsonPropertyName("leftEyeLower2")]
    public List<List<double>> LeftEyeLower2 { get; set; }

    [JsonPropertyName("leftEyeLower3")]
    public List<List<double>> LeftEyeLower3 { get; set; }

    [JsonPropertyName("leftEyebrowUpper")]
    public List<List<double>> LeftEyebrowUpper { get; set; }

    [JsonPropertyName("leftEyebrowLower")]
    public List<List<double>> LeftEyebrowLower { get; set; }

    [JsonPropertyName("midwayBetweenEyes")]
    public List<List<double>> MidwayBetweenEyes { get; set; }

    [JsonPropertyName("noseTip")]
    public List<List<double>> NoseTip { get; set; }

    [JsonPropertyName("noseBottom")]
    public List<List<double>> NoseBottom { get; set; }

    [JsonPropertyName("noseRightCorner")]
    public List<List<double>> NoseRightCorner { get; set; }

    [JsonPropertyName("noseLeftCorner")]
    public List<List<double>> NoseLeftCorner { get; set; }

    [JsonPropertyName("rightCheek")]
    public List<List<double>> RightCheek { get; set; }

    [JsonPropertyName("leftCheek")]
    public List<List<double>> LeftCheek { get; set; }
}

public class FaceResult
{
    [JsonPropertyName("faceInViewConfidence")]
    public int FaceInViewConfidence { get; set; }

    [JsonPropertyName("boundingBox")]
    public BoundingBox BoundingBox { get; set; }

    [JsonPropertyName("mesh")]
    public List<List<double>> Mesh { get; set; }

    [JsonPropertyName("scaledMesh")]
    public List<List<double>> ScaledMesh { get; set; }

    [JsonPropertyName("annotations")]
    public Annotations Annotations { get; set; }
}


