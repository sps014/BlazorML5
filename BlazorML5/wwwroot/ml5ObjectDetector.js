let ObjectDetectors = new Object();

export function initObjectDetectorML5(Hash, DotNet, model, options)
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

export function destroyObjectDetector(hash) {
    delete ObjectDetectors[hash];
}

export function objectDetectorDetect(hash, dotnet, imageData)
{
    ObjectDetectors[hash].detect(imageData, detection.bind(dotnet));
}
function detection(err, res) {
    this.invokeMethodAsync("ODFDR", err, res);
}
