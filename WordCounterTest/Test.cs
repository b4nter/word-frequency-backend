using WordCounter;


namespace WordCounterTest
{
    [TestClass]
    public class Test
    {
        [DataTestMethod]
        [DataRow("Chris Pincher: MP faces eight-", ["Chris", "Pincher", "MP", "faces", "eight"])]
        [DataRow("Dog  'almost died'", ["Dog", "almost", "died"])]
        [DataRow("Hannah Dingley: Forest Green Rovers name first female boss of a men's professional football side", 
                ["Hannah", "Dingley", "Forest", "Green", "Rovers", "name", "first", 
                 "female", "boss", "of", "a", "mens", "professional", "football", "side"])]
        public void Test_GetWordsOutOfSentence1(string input, params string[] expected)
        {
            string[] result = WordCounterHelper.GetWordsOutOfSentence(input);
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_GroupWords()
        {
            string[] words = { "word", "dog", "cat", "dog", "word", "something", "word" };

            Dictionary<string, int> expected = new Dictionary<string, int>
            {
                { "word", 3 },
                { "dog", 2 },
                { "cat", 1 },
                { "something", 1 }
            };

            Dictionary<string, int> result = WordCounterHelper.CountWords(words);

            CollectionAssert.AreEquivalent(expected, result);
        }

        [TestMethod]
        public void Test_ClearWords()
        {
            string[] words = { "Word", "FOO", "Dog", "Cat", "dog", "foo", "WorD", "Something", "Word" };
            string[] unwantedWords = { "word", "foo" };
            string[] expected = { "dog", "cat", "dog", "something" };

            string[] result = WordCounterHelper.ClearWords(words, unwantedWords,0, 100);

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_ClearWords_WithMinLength()
        {
            string[] words = { "Word", "FOO","is", "Dog", "Cat", "dog","to", "foo", "WorD", "Something", "Word" };
            string[] unwantedWords = { "word", "foo" };
            string[] expected = { "something" };

            string[] words2 = { "Word", "FOO", "is", "Dog", "Cat", "dog", "to", "foo", "WorD", "Something", "Word" };
            string[] unwantedWords2 = { "word", "foo" };
            string[] expected2 = { "dog", "cat", "dog", "something" };

            string[] result = WordCounterHelper.ClearWords(words, unwantedWords, 4, 100);

            string[] result2 = WordCounterHelper.ClearWords(words2, unwantedWords2, 3, 100);

            CollectionAssert.AreEqual(expected, result);
            CollectionAssert.AreEqual(expected2, result2);
        }
    }
}

