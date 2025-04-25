using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using WordCounter.Interfaces;

namespace WordCounter
{
    public class WordCounterBackgroundService(ITitleWordCounter wordCounter, IConfiguration configuration) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int delay = configuration.GetSection("WordCounterBackgroundServiceDelayInMiliseconds").Get<int>();

            while (!stoppingToken.IsCancellationRequested)
            {
                wordCounter.CountWords();
                await Task.Delay(delay, stoppingToken);
            }
        }
    }
}
