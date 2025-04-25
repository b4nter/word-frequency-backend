using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using WordCounter.Interfaces;
using WordCounter.Models;
using WordCounter.objects;

namespace WordCounter
{
    public class TitleWordCounter(ITitleFetcher titleFetcher, IConfiguration configuration, IMemoryCache memoryCache) : ITitleWordCounter
    {
        private readonly int MEMORY_CACHE_EXPIRATION = configuration.GetSection("MemoryCacheAbsoluteExpirationInSeconds").Get<int>();

        private List<CountedWord> GetCountedWords()
        {
            List<CountedWord> result = new List<CountedWord>();
            string[] wordsToSkip = configuration.GetSection("WordsToSkip").Get<string[]>();
            int minWordLength = configuration.GetSection("MinWordLength").Get<int>();
            int maxWordLength = configuration.GetSection("MaxWordLength").Get<int>();

            List<NewsOutletTitle> titles = titleFetcher.GetTitles();

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

        public void CountWords()
        {
            memoryCache.TryGetValue("cachedCountedWords", out List<CountedWord>? cachedCountedWords);

            if (cachedCountedWords == null)
            {
                cachedCountedWords = GetCountedWords();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(MEMORY_CACHE_EXPIRATION));
                memoryCache.Set("cachedCountedWords", cachedCountedWords, cacheEntryOptions);
            }
        }

        public List<CountedWord> GetWords()
        {
            CountWords();
            List<CountedWord>? words = memoryCache.Get<List<CountedWord>>("cachedCountedWords");
    
            if(words == null)
            {
                throw new Exception("TitleWordCounter.GetWords: Something went wrong, no counted words found.");
            }

            return words;
        }
    }
}
