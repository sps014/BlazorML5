namespace BlazorML5.Image;

public class PoseNetOptions
    {
        public string Architecture { get; set; } = "MobileNetV1";
        public double ImageScaleFactor { get; set; } = 0.3;
        public int OutputStride { get; set; } = 16;
        public bool FlipHorizontal { get; set; } = false;
        public double MinConfidence { get; set; } = 0.5;
        public int MaxPoseDetections { get; set; } = 5;
        public double ScoreThreshold { get; set; } = 0.5;
        public int NmsRadius { get; set; } = 20;
        public string DetectionType { get; set; } = "multiple";
        public int InputResolution { get; set; } = 513;
        public double Multiplier { get; set; } = 0.75;
        public int QuantBytes { get; set; } = 2;
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    

    public class Position    {
        public double X { get; set; } 
        public double Y { get; set; } 
    }

    public class Keypoint    {
        public double Score { get; set; } 
        public string? Part { get; set; } 
        public Position? Position { get; set; } 
    }

    public class Nose    {
        public double X { get; set; } 
        public double Y { get; set; } 
        public double Confidence { get; set; } 
    }

    public class LeftEye    {
        public double X { get; set; } 
        public double Y { get; set; } 
        public double Confidence { get; set; } 
    }

    public class RightEye    {
        public double X { get; set; } 
        public double Y { get; set; } 
        public double Confidence { get; set; } 
    }

    public class LeftEar    {
        public double X { get; set; } 
        public double Y { get; set; } 
        public double Confidence { get; set; } 
    }

    public class RightEar    {
        public double X { get; set; } 
        public double Y { get; set; } 
        public double Confidence { get; set; } 
    }

    public class LeftShoulder    {
        public double X { get; set; } 
        public double Y { get; set; } 
        public double Confidence { get; set; } 
    }

    public class RightShoulder    {
        public double X { get; set; } 
        public double Y { get; set; } 
        public double Confidence { get; set; } 
    }

    public class LeftElbow    {
        public double X { get; set; } 
        public double Y { get; set; } 
        public double Confidence { get; set; } 
    }

    public class RightElbow    {
        public double X { get; set; } 
        public double Y { get; set; } 
        public double Confidence { get; set; } 
    }

    public class LeftWrist    {
        public double X { get; set; } 
        public double Y { get; set; } 
        public double Confidence { get; set; } 
    }

    public class RightWrist    {
        public double X { get; set; } 
        public double Y { get; set; } 
        public double Confidence { get; set; } 
    }

    public class LeftHip    {
        public double X { get; set; } 
        public double Y { get; set; } 
        public double Confidence { get; set; } 
    }

    public class RightHip    {
        public double X { get; set; } 
        public double Y { get; set; } 
        public double Confidence { get; set; } 
    }

    public class LeftKnee    {
        public double X { get; set; } 
        public double Y { get; set; } 
        public double Confidence { get; set; } 
    }

    public class RightKnee    {
        public double X { get; set; } 
        public double Y { get; set; } 
        public double Confidence { get; set; } 
    }

    public class LeftAnkle    {
        public double X { get; set; } 
        public double Y { get; set; } 
        public double Confidence { get; set; } 
    }

    public class RightAnkle    {
        public double X { get; set; } 
        public double Y { get; set; } 
        public double Confidence { get; set; } 
    }

    public class Pose    {
        public double Score { get; set; } 
        public IReadOnlyList<Keypoint>? Keypoints { get; set; } 
        public Nose? Nose { get; set; } 
        public LeftEye? LeftEye { get; set; } 
        public RightEye? RightEye { get; set; } 
        public LeftEar? LeftEar { get; set; } 
        public RightEar? RightEar { get; set; } 
        public LeftShoulder? LeftShoulder { get; set; } 
        public RightShoulder? RightShoulder { get; set; } 
        public LeftElbow? LeftElbow { get; set; } 
        public RightElbow? RightElbow { get; set; } 
        public LeftWrist? LeftWrist { get; set; } 
        public RightWrist? RightWrist { get; set; } 
        public LeftHip? LeftHip { get; set; } 
        public RightHip? RightHip { get; set; } 
        public LeftKnee? LeftKnee { get; set; } 
        public RightKnee? RightKnee { get; set; } 
        public LeftAnkle? LeftAnkle { get; set; } 
        public RightAnkle? RightAnkle { get; set; } 
    }

    public class PoseResult    {
        public Pose? Pose { get; set; } 
        public IReadOnlyList<IReadOnlyList<Keypoint>>? Skeleton { get; set; } 
    }