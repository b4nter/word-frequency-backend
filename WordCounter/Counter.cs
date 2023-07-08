using System;
using System.Text.RegularExpressions;
using WordCounter.objects;

namespace WordCounter
{
    public static class Counter
    {
        private static readonly string[] FILTER_WORDS = { "out", "for", "how", "the", "not", "who", "and", "her", "his", "you", "was", "from", "with", "after", "over", "into", "bbc" };
        private const int WORD_MIN_LENGTH = 3;
        private static readonly Dictionary<string, string> NEWS_OUTLETS_URLS = new Dictionary<string, string>
        {
            {"bbc","http://feeds.bbci.co.uk/news/uk/rss.xml"},
            {"mirror", "https://www.mirror.co.uk/?service=rss" },
            {"dailyMail", "https://www.dailymail.co.uk/ushome/index.rss" },
            {"independent", "https://www.independent.co.uk/news/uk/rss" },
            {"sky", "https://feeds.skynews.com/feeds/rss/uk.xml" },
            {"guardian", "https://www.theguardian.com/uk/rss" }
        };

        public static List<CountedWord> GetWords()
        {
            List<CountedWord> words = new List<CountedWord>();
            foreach (KeyValuePair <string,string> newsOutletUrl in NEWS_OUTLETS_URLS)
            {
                List<CountedWord> groupedWords = GetCountedWordsFor(newsOutletUrl.Key, newsOutletUrl.Value);
                words.AddRange(groupedWords);
            }
            return words;
        }

        private static List<CountedWord> GetCountedWordsFor(string newsOutlet, string url)
        {
            RssFeedReader reader = new RssFeedReader();
            string[] titles = reader.GetTitles(url);
            string[] words = GetWordsOutOfSentence(titles);
            string[] filteredWords = ClearWords(words, FILTER_WORDS, WORD_MIN_LENGTH);
            Dictionary<string, int> groupedWords = GroupWords(filteredWords);

            return ToCountedWords(groupedWords, newsOutlet);
        }
        
        private static List<CountedWord> ToCountedWords(Dictionary<string, int> words, string newsOutlet)
        {
            List<CountedWord> countedWords = new List<CountedWord>();
            foreach (KeyValuePair<string, int> word in words)
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
            foreach (string sentence in sentences)
            {
                string wordsOnly = Regex.Replace(sentence, @"[^\w\s]", "");
                string cleanedSentence = Regex.Replace(wordsOnly, @"\s+", " ").Trim();

                words = words.Concat(cleanedSentence.Split(" ")).ToArray();
            }

            return words;
        }

        public static Dictionary<string, int> GroupWords(string[] words)
        {
            Dictionary<string, int> result = new Dictionary<string, int>();
            foreach (string word in words)
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
            List<string> filtered = new List<string>();
            foreach (string word in words)
            {
                string lowerCaseWord = word.ToLower();
                if (!filterWords.Contains(lowerCaseWord) && lowerCaseWord.Length >= wordMinLength)
                {
                    filtered.Add(lowerCaseWord);
                }
            }
            return filtered.ToArray();
        }
    }
}