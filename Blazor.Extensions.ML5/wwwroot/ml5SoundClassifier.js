let SoundClassifiers = new Object();

function initSoundClassifierML5(Hash, DotNet, model, options) {
    let icf;
    if (options != null)
        icf = ml5.soundClassifier(model, options, ml5ModelLoadedSoundClassifier.bind(DotNet));
    else
        icf = ml5.soundClassifier(model, ml5ModelLoadedSoundClassifier.bind(DotNet));

    SoundClassifiers[Hash] = icf;

}

function destroySoundClassifier(hash) {
    delete SoundClassifiers[hash];
}

function ml5ModelLoadedSoundClassifier() {
    this.invokeMethodAsync("SCFML", "__ModelLoadedSC__");
}

function soundClassifierClassify(hash, DotNet, sound) {
    console.log(hash, SoundClassifiers[hash])

    if (sound != null)
        SoundClassifiers[hash].classify(sound, soundResultClassification.bind(DotNet));
    else
        SoundClassifiers[hash].classify(soundResultClassification.bind(DotNet));


}
function soundResultClassification(err, res) {
    this.invokeMethodAsync("SCFCF", err, res);

}