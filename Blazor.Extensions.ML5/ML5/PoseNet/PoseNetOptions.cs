using System;
using System.Collections.Generic;
using System.Text;

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
}
