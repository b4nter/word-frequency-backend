using System.ServiceModel.Syndication;

namespace WordCounter
{
    public static class SyndicationItemExtension
    {
        public static string GetUrl(this SyndicationItem item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            if(item.Links.Count > 0)
            {
                return item.Links[0].Uri.ToString();
            }
            return "";
        }
    }
}
