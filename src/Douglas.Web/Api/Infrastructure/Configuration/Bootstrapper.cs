using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Autofac;
using Douglas.Domain;
using Douglas.Web.Api.Infrastructure.Authentication;
using Douglas.Web.Api.Infrastructure.RestExceptions;
using Nancy;
using Nancy.Authentication.Stateless;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Autofac;
using Nancy.Conventions;
using Nancy.Security;
using Newtonsoft.Json;
using TokenDoesNotExistException = Douglas.Web.Api.Infrastructure.Authentication.TokenDoesNotExistException;

namespace Douglas.Web.Api.Infrastructure.Configuration
{
    public class Bootstrapper : AutofacNancyBootstrapper
    {
        static readonly Action<Response> CorsResponse = x =>
        {
            x.WithHeader("Access-Control-Allow-Methods",
                         "GET, POST, PUT, DELETE, OPTIONS");
            x.WithHeader("Access-Control-Allow-Headers",
                         "Content-Type, Accept");
            x.WithHeader("Access-Control-Max-Age", "1728000");
            x.WithHeader("Access-Control-Allow-Origin", "*");
        };

        readonly List<IBootstrapperTask<ContainerBuilder>> _tasks;

        public Bootstrapper()
        {
            _tasks = new List<IBootstrapperTask<ContainerBuilder>>
                         {
                             new ConfigureCommonDependencies(),
                             new ConfigureDatabaseWiring(),
                             new ConfigureAutomapperMappings(),                             
                         };
        }

        protected override void ConfigureConventions(NancyConventions conventions)
        {
            base.ConfigureConventions(conventions);

            conventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("App"));
            conventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("Assets"));
        }

        protected override void ConfigureApplicationContainer(ILifetimeScope existingContainer)
        {
            var builder = new ContainerBuilder();
            _tasks.ForEach(task => task.Task.Invoke(builder));
            builder.Update(existingContainer.ComponentRegistry);
            base.ConfigureApplicationContainer(existingContainer);
        }

        protected override void RequestStartup(ILifetimeScope container, IPipelines pipelines, NancyContext context)
        {
            StaticConfiguration.DisableErrorTraces = false;

            var configuration =
                new StatelessAuthenticationConfiguration(
                    ctx =>
                    {
                        string token = GetTokenFromRequest(ctx);

                        bool hasToken = !string.IsNullOrEmpty(token);

                        if (hasToken)
                        {
                            var apiUserMapper = container.Resolve<IApiUserMapper<Guid>>();
                            Guid tokenGuid;
                            if (!string.IsNullOrEmpty(token) && Guid.TryParse(token, out tokenGuid))
                            {
                                try
                                {
                                    IUserIdentity userFromAccessToken =
                                        apiUserMapper.GetUserFromAccessToken(tokenGuid);
                                    return userFromAccessToken;
                                }
                                catch (TokenDoesNotExistException)
                                {
                                }
                            }
                        }

                        return new LoggedInUserIdentity(new VisitorSession());
                    });

            StatelessAuthentication.Enable(pipelines, configuration);

            RestExceptionRepackager.Configure(x => x.WithResponse(CorsResponse)).Register(pipelines);

            pipelines.AfterRequest.AddItemToEndOfPipeline(x => CorsResponse(x.Response));

            base.RequestStartup(container, pipelines, context);
        }

        static string GetTokenFromRequest(NancyContext ctx)
        {
            var token = (string)ctx.Request.Query.token;
            if (token == null)
            {
                token = (string)ctx.Request.Form.token;
            }
            if (token == null)
            {
                const string headerName = "Authorization";
                bool hasAuthHeader = ctx.Request.Headers.Keys.Contains(headerName);
                if (hasAuthHeader)
                {
                    string authHeader =
                        ctx.Request.Headers[headerName].FirstOrDefault();
                    if (authHeader != null)
                        token = authHeader.Replace("Bearer ", "");
                }
            }
            if (token == null)
            {
                var stream = new StreamReader(ctx.Request.Body);
                string body = stream.ReadToEnd();
                ctx.Request.Body.Seek(0, SeekOrigin.Begin);
                try
                {
                    var bodyWithToken = JsonConvert.DeserializeObject<BodyWithToken>(body);
                    if (bodyWithToken != null && bodyWithToken.Token != Guid.Empty)
                        token = bodyWithToken.Token.ToString();
                }
                catch
                {
                }
            }
            return token;
        }

        public class BodyWithToken
        {
            public Guid Token { get; set; }
        }
    }
}