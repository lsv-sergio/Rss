using Autofac;
using Autofac.Core;
using Microsoft.AspNetCore.Http;
using RssReader.Core.Classes.Services;
using RssReader.Core.Interfaces.Commands;
using RssReader.Core.Interfaces.Entities;
using RssReader.Core.Interfaces.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RssReader.Core.Classes.Entities
{
    public class Channel : IChannel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<IFeed> Feeds { get; set; }


        private IChannelDeleteCommand _deleteCommand;
        private IChannelSaveCommand _saveCommand;
        IFeedCreator _feedCreator;

        public Channel(IChannelSaveCommand saveCommand, IChannelDeleteCommand deleteCommand, IFeedCreator feedCreator)
        {
            _saveCommand = saveCommand;
            _deleteCommand = deleteCommand;
            _feedCreator = feedCreator;
        }

        public async Task<IList<IArticle>> GetAllarticle()
        {
            IList<IArticle> chanelArticles = new List<IArticle>();
            foreach (var feed in Feeds)
            {
                var feedArticles = await feed.GetAllArticles();
                chanelArticles.Union(feedArticles);
            }
            return chanelArticles.OrderByDescending(article => article.PublishDate).ToList();
        }

        public async Task<IFeed> AddRssFeed(string url, string name)
        {
            IFeed newFeed = _feedCreator.CreateRssFeed(url, name);
            await newFeed.CompleteFeed();
            return newFeed;
        }

        public async Task<IFeed> AddAtomFeed(string url, string name)
        {
            IFeed newFeed = _feedCreator.CreateRssFeed(url, name);
            await newFeed.CompleteFeed();
            return newFeed;
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