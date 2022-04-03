using Hangfire;
using Microsoft.Extensions.DependencyInjection;

namespace Scheduler.Jobs.Sample.Infrastructure.IoC
{
    public class ContainerJobActivator : JobActivator
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IServiceScope _scope;

        public ContainerJobActivator(IServiceProvider serviceProvider)
        {
            _scope = serviceProvider.CreateScope();
            
            _serviceProvider = _scope.ServiceProvider;
        }

        public override object ActivateJob(Type type) => _serviceProvider.GetService(type);
    }
}
