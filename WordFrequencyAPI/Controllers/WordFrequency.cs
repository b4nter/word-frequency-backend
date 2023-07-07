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
            var bbc = "http://feeds.bbci.co.uk/news/uk/rss.xml";
            return Counter.GetCountedWords(bbc, "BBC"); ;
        }
    }
}
