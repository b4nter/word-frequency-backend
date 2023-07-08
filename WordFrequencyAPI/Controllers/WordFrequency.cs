using Microsoft.AspNetCore.Mvc;
using WordCounter;
using WordCounter.objects;

namespace WordFrequencyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordFrequency : ControllerBase
    {
        // GET: api/WordFrequency
        [HttpGet]
        public IEnumerable<CountedWord> Get()
        {
            return Counter.GetWords();
        }
    }
}
