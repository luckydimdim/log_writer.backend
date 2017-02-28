using System;
using Autofac;
using error_handler.backend.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Autofac;
using Nancy.Responses.Negotiation;

namespace log_writer.backend
{
    public class Bootstrapper : AutofacNancyBootstrapper
    {
        private readonly IServiceProvider _serviceProvider = null;

        public Bootstrapper(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override void ApplicationStartup(ILifetimeScope container, IPipelines pipelines)
        {
            // No registrations should be performed in here, however you may
            // resolve things that are needed during application startup.
        }

        protected override void ConfigureApplicationContainer(ILifetimeScope existingContainer)
        {
            // Perform registration that should have an application lifetime
        }

        protected override void ConfigureRequestContainer(ILifetimeScope container, NancyContext context)
        {
            // Perform registrations that should have a request lifetime
        }

        protected override void RequestStartup(ILifetimeScope container, IPipelines pipelines, NancyContext context)
        {
            // No registrations should be performed in here, however you may
            // resolve things that are needed during request startup.
            var handler = new ErrorHandler(container.Resolve<ILoggerFactory>());

            handler.Enable(pipelines, container.Resolve<IResponseNegotiator>());
        }

        protected override ILifetimeScope GetApplicationContainer()
        {
            return _serviceProvider.GetService<ILifetimeScope>();
        }

        protected override Func<ITypeCatalog, NancyInternalConfiguration> InternalConfiguration
        {
            get
            {
                return NancyInternalConfiguration.WithOverrides(config => {
                    config.StatusCodeHandlers = new[] { typeof(StatusCodeHandler404), typeof(StatusCodeHandler500) };
                    config.ResponseProcessors = new[] { typeof(JsonProcessor) };
                });
            }
        }
    }
}