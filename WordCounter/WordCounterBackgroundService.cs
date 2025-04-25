using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using WordCounter.Interfaces;

namespace WordCounter
{
    public class WordCounterBackgroundService(ITitleFetcher titleFetcher, IConfiguration configuration) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int delay = configuration.GetSection("WordCounterBackgroundServiceDelayInMiliseconds").Get<int>();

            while (!stoppingToken.IsCancellationRequested)
            {
                titleFetcher.UpdateTitleCache();
                await Task.Delay(delay, stoppingToken);
            }
        }
    }
}
