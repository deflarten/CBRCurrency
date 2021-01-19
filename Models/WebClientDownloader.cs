using System.Net;

namespace CBRCurrency.Models
{
    public class WebClientDownloader : IDownloader
    {
        public string DownloadString(string url)
        {
            using var wc = new WebClient();
            return wc.DownloadString(url);
        }
    }
}
