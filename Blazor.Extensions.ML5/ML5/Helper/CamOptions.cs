namespace ML5.WebCam
{
    public class CamOptions
    {
        /// <summary>
        /// Pass a class object for it , use https://developer.mozilla.org/en-US/docs/Web/API/MediaDevices/getUserMedia
        /// for documentation of parameters that can be passed default is true 
        /// </summary>
        public object audio { get; set; } = true;
        /// <summary>
        /// Pass a class object for it , use https://developer.mozilla.org/en-US/docs/Web/API/MediaDevices/getUserMedia
        /// for documentation of parameters that can be passed default is true you can pass object like this with 
        /// class videoOptions
        /// {
        ///     public int width{get;set;}=1280;
        ///     public int height {get;set;}=786;
        /// } 
        /// </summary>
        public object video { get; set; } = true;
    }
}