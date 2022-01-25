using BlazorBindGen;

namespace BlazorML5.Text;

public class Sentiment
{
    #nullable disable
    private JObjPtr _sentiment;
    /// <summary>
    /// set to true if the model is loaded and ready, false if it is not.
    /// </summary>
    public ValueTask<bool> Ready => _sentiment.PropValAsync<bool>("ready");
    internal Sentiment(){}
    #nullable restore 
    internal Sentiment Init(JObjPtr sentiment)
    {
        _sentiment = sentiment;
        return this;
    }
    /// <summary>
    /// Predict the sentiment of the text
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public async Task<SentimentScore> PredictAsync(string text)
    {
        return await _sentiment.CallAsync<SentimentScore>("predict", text);
    }

    public delegate void OnModelLoadedHandler();
    
    /// <summary>
    /// Fires when model is loaded
    /// </summary>
    public event OnModelLoadedHandler? OnModelLoaded;

    internal void OnModelLoadedCallback(JObjPtr[] _)
    {
        OnModelLoaded?.Invoke();
    }

}

public record SentimentScore(double Score);