using Microsoft.AspNetCore.Mvc;
using WordCounter.Interfaces;
using WordCounter.objects;

namespace WordFrequencyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordFrequency(ITitleWordCounter counter) : ControllerBase
    {
        // GET: api/WordFrequency
        [HttpGet]
        public IEnumerable<CountedWord> Get()
        {
            return counter.GetCountedWords();
        }
    }
}
