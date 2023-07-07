using Microsoft.AspNetCore.Mvc;
using WordCounter;
using WordCounter.objects;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
