using Microsoft.Extensions.Configuration;
using WordCounter.Interfaces;
using WordCounter.Models;
using WordCounter.objects;

namespace WordCounter
{
    public class TitleWordCounter(ITitleFetcher titleFetcher, IConfiguration configuration) : ITitleWordCounter
    {
        public List<CountedWord> GetCountedWords()
        {
            List<CountedWord> result = new List<CountedWord>();
            string[] wordsToSkip = configuration.GetSection("WordsToSkip").Get<string[]>();
            int minWordLength = configuration.GetSection("MinWordLength").Get<int>();
            int maxWordLength = configuration.GetSection("MaxWordLength").Get<int>();

            List <NewsOutletTitle> titles = titleFetcher.GetTitles();

            Dictionary<string, List<string>> groupedWords = new Dictionary<string, List<string>>();
            foreach (var title in titles)
            {
                string[] words = Counter.GetWordsOutOfSentence(title.Title);
                string[] clearedWords = Counter.ClearWords(words, wordsToSkip, minWordLength, maxWordLength);
                if (!groupedWords.ContainsKey(title.NewsOutletName))
                {
                    groupedWords.Add(title.NewsOutletName, new List<string>());
                }
                groupedWords[title.NewsOutletName].AddRange(clearedWords);
            }

            foreach (KeyValuePair<string, List<string>> outletWords in groupedWords)
            {
                Dictionary<string, int> countedWords = Counter.CountWords(outletWords.Value.ToArray());
                result.AddRange(Counter.ToCountedWords(countedWords, outletWords.Key));
            }

            return result;
        }
    }
}
