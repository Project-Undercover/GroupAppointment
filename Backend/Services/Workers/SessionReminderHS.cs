using Core.IPersistence;
using Core.IServices.Others;
using Infrastructure.Entities.Sessions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Services.Workers
{
    public class SessionReminderHS : IHostedService
    {
        private readonly ILogger<SessionReminderHS> _logger;
        private Timer _timer;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public SessionReminderHS(ILogger<SessionReminderHS> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }


        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(3));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            IUnitOfWork? unitOfWork = _serviceScopeFactory.CreateScope().ServiceProvider.GetService<IUnitOfWork>();
            IFCMService? fcm = _serviceScopeFactory.CreateScope().ServiceProvider.GetService<IFCMService>();

            if (unitOfWork is null || fcm is null) return;
            DateTime date = DateTime.Now;
            DateTime endDate = date.AddHours(1);
            var (count, sessions) = await unitOfWork.Repository<Session>().Find(s => s.Participants.Count() > 0 && s.CreatedAt > date && s.CreatedAt <= endDate);

            List<Session> notifySessions = sessions.Where(s => s.CreatedAt.Subtract(date).Minutes == 30).ToList();
            Console.WriteLine($"Notifiying session {notifySessions.Count()}");
        }
    }
}
