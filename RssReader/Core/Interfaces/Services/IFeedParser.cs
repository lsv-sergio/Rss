using RssReader.Core.Interfaces.Entities;
using System.Collections.Generic;
using System.Xml.Linq;

namespace RssReader.Core.Interfaces.Services
{
    public interface IFeedParser
    {
        IList<IArticle> ParseArticle(IFeed feed, XDocument doc);
        void ParseFeed(IFeed feed, XDocument doc);
    }
}
