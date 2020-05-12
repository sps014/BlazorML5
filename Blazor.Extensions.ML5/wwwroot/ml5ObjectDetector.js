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

function objectDetectorDetect(hash, dotnet, imageData)
{
    ObjectDetectors[hash].detect(imageData, detection.bind(dotnet));
}
function detection(err, res) {
    this.invokeMethodAsync("ODFDR", err, res);
}

function getElementPosition(elem) {
    const rect = elem.getBoundingClientRect();
    console.log(rect);
    return { x: rect.left + window.scrollX, y: rect.top + window.scrollY, width: rect.width, height: rect.height };
}
function DrawOnImage(img, cnvs,x,y,w,h) {

    cnvs.style.position = "absolute";
    cnvs.style.left = img.offsetLeft + "px";
    cnvs.style.top = img.offsetTop + "px";

    var ctx = cnvs.getContext("2d");
    ctx.beginPath();
    ctx.rect(x,y,w,h);
    ctx.lineWidth = 3;
    ctx.strokeStyle = '#00ff00';
    ctx.stroke();
}