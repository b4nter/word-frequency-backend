using Microsoft.Extensions.Configuration;
using System.ServiceModel.Syndication;
using System.Xml;
using WordCounter.Interfaces;
using WordCounter.Models;

namespace WordCounter
{
    public class TitleFetcher(IConfiguration configuration) : ITitleFetcher
    {
        public List<NewsOutletTitle> GetTitles()
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
