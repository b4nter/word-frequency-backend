using WordCounter;


namespace WordCounterTest
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test_GetWordsOutOfSentence()
        {
            string[] sentences = { "Chris Pincher: MP faces eight-",
                                   "Dog  'almost died'",
                                   "Hannah Dingley: Forest Green Rovers name first female boss of a men's professional football side"};
            string[] expected = { "Chris", "Pincher", "MP", "faces", "eight", "Dog", "almost", "died",
                                  "Hannah","Dingley","Forest","Green","Rovers", "name", "first", "female",
                                  "boss","of","a","mens","professional","football", "side" };

            var result = Counter.GetWordsOutOfSentence(sentences);
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Test_GroupWords()
        {
            string[] words = { "word", "dog", "cat", "dog", "word", "something", "word" };

            var expected = new Dictionary<string, int>
            {
                { "word", 3 },
                { "dog", 2 },
                { "cat", 1 },
                { "something", 1 }
            };

            var result = Counter.GroupWords(words);

            CollectionAssert.AreEquivalent(expected, result);
        }

        [TestMethod]
        public void Test_ClearWords()
        {
            string[] words = { "Word", "FOO", "Dog", "Cat", "dog", "foo", "WorD", "Something", "Word" };
            string[] unwantedWords = { "word", "foo" };
            string[] expected = { "dog", "cat", "dog", "something" };

            var result = Counter.ClearWords(words, unwantedWords);

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

            var result = Counter.ClearWords(words, unwantedWords, 4);

            var result2 = Counter.ClearWords(words2, unwantedWords2, 3);

            CollectionAssert.AreEqual(expected, result);
            CollectionAssert.AreEqual(expected2, result2);
        }

        [TestMethod]
        public void Test_MergeCountedWords()
        {
            var expected = new Dictionary<string, int>
            {
                { "word", 3 },
                { "dog", 2 },
                { "cat", 1 },
                { "something", 1 }
            };
        }
    }
}

