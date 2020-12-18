let Senti = new Object();

export function intSentiMl5(Hash, DotNet, model) {
    let icf = ml5.sentiment(model, ml5SentiLoaded.bind(DotNet));
    Senti[Hash] = icf;

}

export function destroySenti(hash) {
    delete Senti[hash];
}

function ml5SentiLoaded() {
    this.invokeMethodAsync("SALMSG", "__ModelLoadedSC__");
}
export function predictSentiMl5(h, text) {
    return Senti[h].predict(text).score;
}