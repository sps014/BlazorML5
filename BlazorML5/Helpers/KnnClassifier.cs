using BlazorBindGen;

namespace BlazorML5.Helpers;

public class KnnClassifier
{
    private readonly JObjPtr _knn;
    internal KnnClassifier(JObjPtr knn)
    {
        _knn = knn;
    }
    
    /// <summary>
    /// Adding an example to a class.
    /// </summary>
    /// <param name="example"> An example to add to the dataset, usually an activation from another model</param>
    /// <param name="indexOrLabel">String | Number. The class index(number) or label(string) of the example.</param>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TS"></typeparam>
    public async Task AddExampleAsync<T, TS>(T example, TS indexOrLabel)
    {
        if(example is Logits l)
            await _knn.CallVoidAsync("addExample", l._logits, indexOrLabel!);
        else 
            await _knn.CallVoidAsync("addExample", example!, indexOrLabel!);
    }
    /// <summary>
    /// Classify an new input.
    /// </summary>
    /// <param name="input">. An example to make a prediction on, could be an activation from another model or an array of numbers.</param>
    /// <param name="k">The K value to use in K-nearest neighbors. The algorithm will first find the K nearest examples from those it was previously shown, and then choose the class that appears the most as the final prediction for the input example.</param>
    /// <typeparam name="T"></typeparam>
    public async Task ClassifyAsync<T>(T input, int k = 3)
    {
        if(input is Logits l)
            await _knn.CallVoidAsync("classify", l._logits,k,(JSCallback)OnClassifyCallback);
        else
            await _knn.CallVoidAsync("classify", input!, k,(JSCallback)OnClassifyCallback);
    }
    /// <summary>
    /// Clears the specified label.
    /// </summary>
    /// <param name="indexOrLabel">The class index or label, a number or a string.</param>
    /// <typeparam name="T"></typeparam>
    public async Task ClearLabelAsync<T>(T indexOrLabel)
    {
        await _knn.CallVoidAsync("clearLabel", indexOrLabel!);
    }
    /// <summary>
    /// Clear all examples in all labels
    /// </summary>
    public async Task ClearAllLabelsAsync()
    {
        await _knn.CallVoidAsync("clearAllLabels");
    }
    /// <summary>
    /// Get the example count for each label. It returns an object that maps class label to example count for each class.
    /// </summary>
    /// <returns></returns>
    public async Task<IReadOnlyDictionary<string, int>> GetCountByLabelAsync()
    {
        return await _knn.CallAsync<IReadOnlyDictionary<string, int>>("getCountByLabel");
    }
    /// <summary>
    /// Get the example count for each class. It returns an object that maps class index to example count for each class.
    /// </summary>
    /// <returns></returns>
    public async Task<IReadOnlyDictionary<string, int>> GetCountAsync()
    {
        return await _knn.CallAsync<IReadOnlyDictionary<string, int>>("getCount");
    }
    /// <summary>
    /// It returns the total number of labels.
    /// </summary>
    /// <returns></returns>
    public async Task<int> GetNumLabelsAsync()
    {
        return await _knn.CallAsync<int>("getNumLabels");
    }
    /// <summary>
    /// Download the whole dataset as a JSON file. It's useful for saving state.
    /// </summary>
    /// <param name="path"></param>
    public async Task SaveAsync(string? path=null)
    {
        if(path is null)    
            await _knn.CallVoidAsync("save");
        else
            await _knn.CallVoidAsync("save",path);
    }
    /// <summary>
    /// Load a dataset from a JSON file. It's useful for restoring state.
    /// </summary>
    /// <param name="url"></param>
    public async Task LoadAsync(string url)
    {
        await _knn.CallVoidAsync("load",url,(JSCallback)OnDataLoadCallback);
    }

    public delegate void OnDataLoadHandler();
    public event OnDataLoadHandler? OnDataLoad;
    
    public delegate void OnClassfifyHandler(KnnResult result);
    public event OnClassfifyHandler? OnClassify;

    private void OnClassifyCallback(JObjPtr[] args)
    {
        if(OnClassify is null) return;
        OnClassify?.Invoke(args[1].To<KnnResult>());
    }

    private void OnDataLoadCallback(JObjPtr[] _)
    {
        OnDataLoad?.Invoke();
    }
}
public  record KnnResult(string Label,int ClassIndex,IReadOnlyDictionary<string,double> Confidences,IReadOnlyDictionary<string,double> ConfidencesByLabel);