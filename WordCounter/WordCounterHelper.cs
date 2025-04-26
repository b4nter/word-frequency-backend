using System.Text.RegularExpressions;
using WordCounter.objects;

namespace WordCounter
{
    public static class WordCounterHelper
    {
        public static string[] GetClearedWordsOutOfSentence(string sentence, string[] wordsToSkip, int wordMinLength, int wordMaxLength)
        {
            string[] words = GetWordsOutOfSentence(sentence);
            return ClearWords(words, wordsToSkip, wordMinLength, wordMaxLength);
        }

        public static string[] GetWordsOutOfSentence(string sentence)
        {
            string[] words = { };
            string wordsOnly = Regex.Replace(sentence, @"[^\w\s]", "");
            string cleanedSentence = Regex.Replace(wordsOnly, @"\s+", " ").Trim();

            words = words.Concat(cleanedSentence.Split(" ")).ToArray();

            return words;
        }

        public static Dictionary<string, int> CountWords(string[] words)
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

        public static string[] ClearWords(string[] words, string[] wordsToSkip, int wordMinLength, int wordMaxLength)
        {
            List<string> filtered = (from string word in words
                                     let lowerCaseWord = word.ToLower()
                                     where !wordsToSkip.Contains(lowerCaseWord) && lowerCaseWord.Length >= wordMinLength && lowerCaseWord.Length <= wordMaxLength
                                     select lowerCaseWord).ToList();
            return filtered.ToArray();
        }

        public static List<CountedWord> ToCountedWords(Dictionary<string, int> words, string newsOutlet)
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
    }
}