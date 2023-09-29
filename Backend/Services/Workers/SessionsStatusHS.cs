using Core.IPersistence;
using Infrastructure.Entities.Sessions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Services.Workers
{
    public class SessionsStatusHS : IHostedService
    {
        private readonly ILogger<SessionsStatusHS> _logger;
        private Timer _timer;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public SessionsStatusHS(ILogger<SessionsStatusHS> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }


        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            try
            {

                IUnitOfWork? unitOfWork = _serviceScopeFactory.CreateScope().ServiceProvider.GetService<IUnitOfWork>();
                if (unitOfWork is null) return;

                DateTime date = DateTime.Now;
                DateTime endDate = date.AddHours(1);

                var (_, finishedSessions) = await unitOfWork.Repository<Session>().Find(s =>
                    (s.Status == Infrastructure.Enums.Enums.SessionStatus.Available || s.Status == Infrastructure.Enums.Enums.SessionStatus.Full || s.Status == Infrastructure.Enums.Enums.SessionStatus.Started)
                    && s.EndDate < DateTime.UtcNow);

                var (_, startedSessions) = await unitOfWork.Repository<Session>().Find(s =>
                    (s.Status == Infrastructure.Enums.Enums.SessionStatus.Available || s.Status == Infrastructure.Enums.Enums.SessionStatus.Full)
                    && s.StartDate <= DateTime.UtcNow
                    && s.EndDate >= DateTime.UtcNow);

                foreach (var session in finishedSessions)
                {
                    session.Status = Infrastructure.Enums.Enums.SessionStatus.Finished;
                }

                foreach (var session in startedSessions)
                {
                    session.Status = Infrastructure.Enums.Enums.SessionStatus.Started;
                }

                await unitOfWork.Repository<Session>().UpdateRangeAsync(startedSessions);
                await unitOfWork.Repository<Session>().UpdateRangeAsync(finishedSessions);
                await unitOfWork.Commit();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }
    }
}
