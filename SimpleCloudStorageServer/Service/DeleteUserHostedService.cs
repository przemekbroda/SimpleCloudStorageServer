using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

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

            //_timer = new Timer(DeleteUsers, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
            /*Task.Run(async () =>
            {
                while (true)
                {
                    _logger.LogInformation("Delete User called");
                    await Task.Delay(1000 * 5);
                }
            });*/

            return Task.CompletedTask;
        }

        private void DeleteUsers(object state)
        {
            _logger.LogInformation("DeleteUsers called");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Delete Users Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }
    }
}
