using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ML5
{
    public class PoseNetOptions
    {
        public string architecture { get; set; } = "MobileNetV1";
        public double imageScaleFactor { get; set; } = 0.3;
        public int outputStride { get; set; } = 16;
        public bool flipHorizontal { get; set; } = false;
        public double minConfidence { get; set; } = 0.5;
        public int maxPoseDetections { get; set; } = 5;
        public double scoreThreshold { get; set; } = 0.5;
        public int nmsRadius { get; set; } = 20;
        public string detectionType { get; set; } = "multiple";
        public int inputResolution { get; set; } = 513;
        public double multiplier { get; set; } = 0.75;
        public int quantBytes { get; set; } = 2;
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    

    public class Position
    {
        public double x { get; set; }
        public double y { get; set; }

    }

    public class Keypoint
    {
        public double score { get; set; }
        public string part { get; set; }
        public Position position { get; set; }

    }

    public class Nose
    {
        public double x { get; set; }
        public double y { get; set; }
        public double confidence { get; set; }

    }

    public class LeftEye
    {
        public double x { get; set; }
        public double y { get; set; }
        public double confidence { get; set; }

    }

    public class RightEye
    {
        public double x { get; set; }
        public double y { get; set; }
        public double confidence { get; set; }

    }

    public class LeftEar
    {
        public double x { get; set; }
        public double y { get; set; }
        public double confidence { get; set; }

    }

    public class RightEar
    {
        public double x { get; set; }
        public double y { get; set; }
        public double confidence { get; set; }

    }

    public class LeftShoulder
    {
        public double x { get; set; }
        public double y { get; set; }
        public double confidence { get; set; }

    }

    public class RightShoulder
    {
        public double x { get; set; }
        public double y { get; set; }
        public double confidence { get; set; }

    }

    public class LeftElbow
    {
        public double x { get; set; }
        public double y { get; set; }
        public double confidence { get; set; }

    }

    public class RightElbow
    {
        public double x { get; set; }
        public double y { get; set; }
        public double confidence { get; set; }

    }

    public class LeftWrist
    {
        public double x { get; set; }
        public double y { get; set; }
        public double confidence { get; set; }

    }

    public class RightWrist
    {
        public double x { get; set; }
        public double y { get; set; }
        public double confidence { get; set; }

    }

    public class LeftHip
    {
        public double x { get; set; }
        public double y { get; set; }
        public double confidence { get; set; }

    }

    public class RightHip
    {
        public double x { get; set; }
        public double y { get; set; }
        public double confidence { get; set; }

    }

    public class LeftKnee
    {
        public double x { get; set; }
        public double y { get; set; }
        public double confidence { get; set; }

    }

    public class RightKnee
    {
        public double x { get; set; }
        public double y { get; set; }
        public double confidence { get; set; }

    }

    public class LeftAnkle
    {
        public double x { get; set; }
        public double y { get; set; }
        public double confidence { get; set; }

    }

    public class RightAnkle
    {
        public double x { get; set; }
        public double y { get; set; }
        public double confidence { get; set; }

    }

    public class Pose
    {
        public double score { get; set; }
        public List<Keypoint> keypoints { get; set; }
        public Nose nose { get; set; }
        public LeftEye leftEye { get; set; }
        public RightEye rightEye { get; set; }
        public LeftEar leftEar { get; set; }
        public RightEar rightEar { get; set; }
        public LeftShoulder leftShoulder { get; set; }
        public RightShoulder rightShoulder { get; set; }
        public LeftElbow leftElbow { get; set; }
        public RightElbow rightElbow { get; set; }
        public LeftWrist leftWrist { get; set; }
        public RightWrist rightWrist { get; set; }
        public LeftHip leftHip { get; set; }
        public RightHip rightHip { get; set; }
        public LeftKnee leftKnee { get; set; }
        public RightKnee rightKnee { get; set; }
        public LeftAnkle leftAnkle { get; set; }
        public RightAnkle rightAnkle { get; set; }

    }

    public class PoseResult
    {
        public Pose pose { get; set; }
        public List<List<Keypoint>> skeleton { get; set; }

    }

}

