using RssReader.Core.Interfaces.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RssReader.Core.Classes.Services
{
    public class FeedTransport: IFeedTransport
    {
        public async Task<XDocument> ReadFeedUrl(string url)
        {
            XDocument doc = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var responseMessage = await client.GetAsync(url);
                var responseString = await responseMessage.Content.ReadAsStringAsync();
                doc = XDocument.Parse(responseString);
            }
            return doc;
        }
    }
}