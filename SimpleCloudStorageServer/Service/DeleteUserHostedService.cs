using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Timer = System.Threading.Timer;

namespace SimpleCloudStorageServer.Service
{
    public class DeleteUserHostedService : IHostedService, IDisposable
    {

        private ILogger<DeleteUserHostedService> _logger;
        private Timer _timer;

        public DeleteUserHostedService(ILogger<DeleteUserHostedService> logger)
        {
            _logger = logger;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Delete Users Hosted Service running.");

            _timer = new Timer(DeleteUsers, null, TimeSpan.Zero, TimeSpan.FromHours(1));

            return Task.CompletedTask;
        }

        private void DeleteUsers(object state)
        {
                
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Delete Users Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }
    }
}
