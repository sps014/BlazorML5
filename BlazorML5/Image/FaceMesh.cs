using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorBindGen;

namespace BlazorML5.Image;

public class FaceMesh
{
#nullable disable
    private JObjPtr _facemesh;
    internal FaceMesh() { }

#nullable enable

    public delegate void OnPredictHandler(FaceResult[] results);
    public event OnPredictHandler? OnPredict;
}