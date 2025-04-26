using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using WordCounter.Interfaces;
using WordCounter.Models;
using WordCounter.objects;

namespace WordCounter
{
    public class TitleWordCounter(ITitleFetcher titleFetcher, IConfiguration configuration) : ITitleWordCounter
    {
        private readonly string[] wordsToSkip = configuration.GetSection("WordsToSkip").Get<string[]>();
        private readonly int minWordLength = configuration.GetSection("MinWordLength").Get<int>();
        private readonly int maxWordLength = configuration.GetSection("MaxWordLength").Get<int>();
        public List<CountedWord> GetCountedWords()
        {
            List<CountedWord> result = new List<CountedWord>();
            List<NewsOutletTitle> titles = titleFetcher.GetTitles();

            Dictionary<string, List<string>> groupedWords = new Dictionary<string, List<string>>();
            foreach (var title in titles)
            {
                string[] clearedWords = WordCounterHelper.GetClearedWordsOutOfSentence(title.Title, wordsToSkip, minWordLength, maxWordLength);
                if (!groupedWords.ContainsKey(title.NewsOutletName))
                {
                    groupedWords.Add(title.NewsOutletName, new List<string>());
                }
                groupedWords[title.NewsOutletName].AddRange(clearedWords);
            }

            foreach (KeyValuePair<string, List<string>> outletWords in groupedWords)
            {
                Dictionary<string, int> countedWords = WordCounterHelper.CountWords(outletWords.Value.ToArray());
                result.AddRange(WordCounterHelper.ToCountedWords(countedWords, outletWords.Key));
            }

            return result;
        }

        public List<NewsOutletTitle> GetTitlesContainingWord(string word)
        {
            List<NewsOutletTitle> titles = titleFetcher.GetTitles();
            List<NewsOutletTitle> result = titles.FindAll(t =>
                WordCounterHelper.GetClearedWordsOutOfSentence(t.Title, wordsToSkip, minWordLength, maxWordLength).Contains(word.ToLower())
            );

            return result;
        }
    }
}
