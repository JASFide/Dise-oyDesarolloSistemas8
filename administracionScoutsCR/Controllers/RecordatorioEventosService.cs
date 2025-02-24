using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;
using administracionScoutsCR.Controllers;

public class RecordatorioEventosService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public RecordatorioEventosService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
              //  var controller = scope.ServiceProvider.GetRequiredService<EventoesController>();
             //   await controller.VerificarEventosProximos();
            }

            await Task.Delay(TimeSpan.FromDays(1), stoppingToken); // Ejecutar cada 24 horas
        }
    }
}
