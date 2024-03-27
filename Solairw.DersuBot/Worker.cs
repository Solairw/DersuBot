﻿using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Solairw.DersuBot.Core;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger, Dersu dersu)
    {
        _logger = logger;
    }


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            
        }
    }
}