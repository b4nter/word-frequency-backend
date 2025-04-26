using WordCounter.Models;
using WordCounter.objects;

namespace WordCounter.Interfaces
{
    public interface ITitleWordCounter
    {
        List<CountedWord> GetCountedWords();
        List<NewsOutletTitle> GetTitlesContainingWord(string word);
    }
}
