let ObjectDetectors = new Object();

function initObjectDetectorML5(Hash, DotNet, model, options)
{
    let od;
    if (options != null)
        od = ml5.objectDetector(model, options, ml5ModelLoadedObjectDetector.bind(DotNet));
    else
        od = ml5.objectDetector(model, ml5ModelLoadedObjectDetector.bind(DotNet));

    ObjectDetectors[Hash] = od;

}

function ml5ModelLoadedObjectDetector() {
    this.invokeMethodAsync("ODFML", "__ModelLoadedOD__");
}

function destroyObjectDetector(hash) {
    delete ObjectDetectors[hash];
}

function objectDetectorDetect(hash, dotnet, imageData) {

}