using System.Text.RegularExpressions;
using WordCounter.objects;

namespace WordCounter
{
    public static class Counter
    {
        private static string[] _filterWords = { "out", "for", "how", "the", "not", "who", "and", "her", "his", "you", "was", "from", "with", "after", "over", "into" };
        private static int _wordMinLength = 3;
        public static List<CountedWord> GetCountedWords(string url, string newsOutlet)
        {
            var reader = new RssFeedReader();
            var titles = reader.GetTitles(url);
            var words = Counter.GetWordsOutOfSentence(titles);
            var filteredWords = Counter.ClearWords(words, _filterWords, _wordMinLength);
            var groupedWords = Counter.GroupWords(filteredWords);

            return ToCountedWords(groupedWords, newsOutlet);
        }
        
        private static List<CountedWord> ToCountedWords(Dictionary<string, int> words, string newsOutlet)
        {
            var countedWords = new List<CountedWord>();
            foreach (var word in words)
            {
                countedWords.Add(new CountedWord
                {
                    Word = word.Key,
                    Frequency = word.Value,
                    NewsOutlet = newsOutlet
                });
            }

            return countedWords;
        }
        public static string[] GetWordsOutOfSentence(string[] sentences)
        {
            string[] words = { };
            foreach (var sentence in sentences)
            {
                string wordsOnly = Regex.Replace(sentence, @"[^\w\s]", "");
                string cleanedSentence = Regex.Replace(wordsOnly, @"\s+", " ").Trim();

                words = words.Concat(cleanedSentence.Split(" ")).ToArray();
            }

            return words;
        }

        public static Dictionary<string, int> GroupWords(string[] words)
        {
            var result = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (!result.ContainsKey(word))
                {
                    result[word] = 1;
                }
                else
                {
                    result[word]++;
                }
            }

            return result;
        }

        public static string[] ClearWords(string[] words, string[] filterWords, int wordMinLength = 0)
        {
            var filtered = new List<string>();
            foreach (var word in words)
            {
                var lowerCaseWord = word.ToLower();
                if (!filterWords.Contains(lowerCaseWord) && lowerCaseWord.Length >= wordMinLength)
                {
                    filtered.Add(lowerCaseWord);
                }
            }
            return filtered.ToArray();
        }
    }
}