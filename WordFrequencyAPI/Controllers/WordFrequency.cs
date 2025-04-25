using Microsoft.AspNetCore.Mvc;
using WordCounter.Interfaces;
using WordCounter.objects;

namespace WordFrequencyAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WordFrequency(ITitleWordCounter counter) : ControllerBase
    {
        // GET: api/WordFrequency/GetCountedWords
        [HttpGet]
        public IEnumerable<CountedWord> GetCountedWords()
        {
            return counter.GetCountedWords();
        }
    }
}
