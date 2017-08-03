using Autofac.Core;
using RssReader.Core.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RssReader.Core.Interfaces.Services
{
    public interface IFeedCreator
    {
        IFeed CreateRssFeed(string url, string name);
        IFeed CreateAtomFeed(string url, string name);
    }
}
