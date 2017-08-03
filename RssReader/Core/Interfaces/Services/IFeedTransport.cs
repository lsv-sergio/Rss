using System.Threading.Tasks;
using System.Xml.Linq;

namespace RssReader.Core.Interfaces.Services
{
    public interface IFeedTransport
    {
        Task<XDocument> ReadFeedUrl(string url);
    }
}
