


async function initWebCam(element, options) {
    try {
        const stream = await navigator.mediaDevices.getUserMedia(options);
        handleWebCamSuccess(stream, element);
    }
    catch (e) {
        console.error(`navigator.getUserMedia error:${e.toString()}`);
    }
}

function handleWebCamSuccess(stream, video)
{
    video.srcObject = stream;
}
