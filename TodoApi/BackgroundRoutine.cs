﻿namespace TodoApi
{
    public class BackgroundRoutine: IHostedService, IDisposable
    {
        private readonly ILogger<DataManager> logger;
        private Timer timer;
        private int number;

        public BackgroundRoutine(ILogger<DataManager> logger)
        {
            this.logger = logger;
        }

        public void Dispose()
        {
            timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(async o => {
                Interlocked.Increment(ref number);
                //logger.LogInformation($"Printing the worker number {number}");
                if(DataManager.IsReady())
                    DataManager.MajDatabase();
            },
            null,
            TimeSpan.Zero,
            TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
