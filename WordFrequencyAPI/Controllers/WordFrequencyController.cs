using Microsoft.AspNetCore.Mvc;
using WordCounter.Interfaces;
using WordCounter.Models;
using WordCounter.objects;

namespace WordFrequencyAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WordFrequencyController(ITitleWordCounter counter) : ControllerBase
    {
        [HttpGet]
        public IEnumerable<CountedWord> GetCountedWords()
        {
            return counter.GetCountedWords();
        }

        [HttpGet]
        public IEnumerable<NewsOutletTitle> GetTitlesContainingWord(string word)
        {
            return counter.GetTitlesContainingWord(word);
        }
    }
}
