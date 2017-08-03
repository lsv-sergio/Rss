using Autofac;
using RssReader.Core.Classes.Services;
using RssReader.Core.Interfaces.Commands;
using RssReader.Core.Interfaces.Entities;
using RssReader.Core.Interfaces.Services;
using ServiceObjects.Classes.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RssReader.Core.Classes.Entities
{
    public class Feed: IFeed
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ChanelId { get; set; }
        public IChannel Chanel { get; set; }
        public string Url { get; set; }
        public IList<IArticle> Articles { get; set; }
        public FeedTypes FeedType { get; set; }

        private IFeedTransport _feedTransport;
        private IFeedParser _feedParser;
        private IFeedDeleteCommand _deleteCommand;
        private IFeedSaveCommand _saveCommand;

        public void Init()
        {
            _saveCommand = DIService.Container.Resolve<IFeedSaveCommand>();
            _deleteCommand = DIService.Container.Resolve<IFeedDeleteCommand>();
            if (FeedType == FeedTypes.ATOM)
            {
                _feedParser = DIService.Container.Resolve<IFeedAtomParser>();
            }
            else
            {
                _feedParser = DIService.Container.Resolve<IFeedRssParser>();
            }
            _feedTransport = DIService.Container.Resolve<IFeedTransport>(); ;
        }

        public Feed()
        {
            Init();
        }

        public Feed(string name, string url, IFeedTransport feedTransport, IFeedParser feedParser, IChannel channel, FeedTypes feedType)
        {
            Chanel = channel;
            ChanelId = channel.Id;
            Name = name;
            Url = url;
            FeedType = feedType;
            Init();
        }

        public async Task<IList<IArticle>> GetAllArticles()
        {
            XDocument doc = await _feedTransport.ReadFeedUrl(Url);
            return _feedParser.ParseArticle(this, doc);
        }

        public async Task CompleteFeed()
        {
            XDocument doc = await _feedTransport.ReadFeedUrl(Url);
            Description = "add get description";
        }
        
        public async Task Save()
        {
            await _saveCommand.Save(this);
        }

        public async Task Save(UnitOfWork uow)
        {
            await _saveCommand.Save(uow, this);
        }
        public async Task Delete()
        {
            await _deleteCommand.Delete(this);
        }

        public async Task Delete(UnitOfWork uow)
        {
            await _deleteCommand.Delete(uow, this);
        }
    }
}