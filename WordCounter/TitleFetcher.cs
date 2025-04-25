using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Xml;
using WordCounter.Interfaces;
using WordCounter.Models;
using WordCounter.objects;

namespace WordCounter
{
    public class TitleFetcher(IConfiguration configuration, IMemoryCache memoryCache) : ITitleFetcher
    {
        private readonly int MEMORY_CACHE_EXPIRATION = configuration.GetSection("MemoryCacheAbsoluteExpirationInSeconds").Get<int>();

        public List<NewsOutletTitle> GetTitles()
        {
            UpdateTitleCache();
            List<NewsOutletTitle>? titles = memoryCache.Get<List<NewsOutletTitle>>("cachedTitles");

            if (titles == null)
            {
                throw new Exception("TitleFetcher.GetTitles: Something went wrong, not titles found");
            }

            return titles;
        }

        public void UpdateTitleCache()
        {
            memoryCache.TryGetValue("cachedTitles", out List<NewsOutletTitle>? cachedTitles);

            if (cachedTitles == null)
            {
                cachedTitles = FetchTitles();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(MEMORY_CACHE_EXPIRATION));
                memoryCache.Set("cachedTitles", cachedTitles, cacheEntryOptions);
            }
        }

        private List<NewsOutletTitle> FetchTitles()
        {
            Dictionary<string, string> newsOutlets = configuration.GetSection("NewsOutlets")
                                                                  .AsEnumerable()
                                                                  .ToDictionary(x => x.Key.Replace("NewsOutlets:", ""), x => x.Value ?? "");

            List<NewsOutletTitle> titles = new List<NewsOutletTitle>();

            foreach (KeyValuePair<string, string> newsOutlet in newsOutlets)
            {
                if (string.IsNullOrEmpty(newsOutlet.Value)) continue;

                using (XmlReader reader = XmlReader.Create(newsOutlet.Value))
                {
                    SyndicationFeed feed = SyndicationFeed.Load(reader);

                    foreach (SyndicationItem item in feed.Items)
                    {
                        titles.Add(new NewsOutletTitle
                        {
                            NewsOutletName = newsOutlet.Key,
                            Title = item.Title.Text
                        });
                    }
                }
            }

            return titles;
        }
    }
}
