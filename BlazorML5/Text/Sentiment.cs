using BlazorBindGen;

namespace BlazorML5.Text;

public class Sentiment
{
    private JObjPtr _sentiment;
    /// <summary>
    /// set to true if the model is loaded and ready, false if it is not.
    /// </summary>
    public ValueTask<bool> Ready => _sentiment.PropValAsync<bool>("ready");
    private Sentiment(){}

    private Sentiment InitAsync(JObjPtr sentiment)
    {
        _sentiment = sentiment;
        return this;
    }
    /// <summary>
    /// Create a new Sentiment Analysis Classifier
    /// </summary>
    /// <param name="model">url of the model , defaults to movie review model</param>
    /// <returns></returns>
    public static async Task<Sentiment> CreateAsync(string model = "movieReviews")
    {
        Sentiment ss = new();
        var sPtr=await ML5.Ml5Ptr.CallRefAsync("sentiment", model, (JSCallback)ss.OnModelLoadedCallback);
        return ss.InitAsync(sPtr);
    }
    
    public async Task<SentimentScore> PredictAsync(string text)
    {
        return await _sentiment.CallAsync<SentimentScore>("predict", text);
    }

    public delegate void OnModelLoadedHandler();
    
    /// <summary>
    /// Fires when model is loaded
    /// </summary>
    public event OnModelLoadedHandler? OnModelLoaded;

    private void OnModelLoadedCallback(JObjPtr[] _)
    {
        OnModelLoaded?.Invoke();
    }

}

public record SentimentScore(double Score);