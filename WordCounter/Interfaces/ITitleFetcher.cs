using WordCounter.Models;

namespace WordCounter.Interfaces
{
    public interface ITitleFetcher
    {
        List<NewsOutletTitle> GetTitles();
    }
}
