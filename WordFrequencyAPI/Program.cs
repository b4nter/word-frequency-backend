using Microsoft.Extensions.Caching.Memory;
using WordCounter;
using WordCounter.Interfaces;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyHeader()
               .AllowAnyMethod()
               .SetIsOriginAllowed(origin => true)
               .AllowAnyOrigin();
    });
});

builder.Services.AddHostedService<WordCounterBackgroundService>();

builder.Services.AddSingleton<ITitleFetcher, TitleFetcher>();
builder.Services.AddSingleton<ITitleWordCounter, TitleWordCounter>();
builder.Services.AddSingleton<IMemoryCache, MemoryCache>();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors();

app.Run();
