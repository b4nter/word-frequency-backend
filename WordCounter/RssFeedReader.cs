using System.ServiceModel.Syndication;
using System.Xml;

namespace WordCounter
{
    public class RssFeedReader
    {
        public string[] GetTitles(string url)
        {
            var titles = new List<string>();
            using (XmlReader reader = XmlReader.Create(url))
            {
                SyndicationFeed feed = SyndicationFeed.Load(reader);

                int RssItemCount = feed.Items.Count();

                foreach (SyndicationItem item in feed.Items)
                {
                    string title = item.Title.Text;
                    titles.Add(title);
                }
            }

            return titles.ToArray();
        }
    }
}
