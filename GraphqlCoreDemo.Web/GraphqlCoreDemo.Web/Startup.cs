using GraphiQl;
using GraphQL;
using GraphQL.DataLoader;
using GraphQL.Http;
using GraphQL.Types;
using GraphqlCoreDemo.Core;
using GraphqlCoreDemo.Core.GraphTypes;
using GraphqlCoreDemo.Core.InputGraphTypes;
using GraphqlCoreDemo.Core.Mutations;
using GraphqlCoreDemo.Core.Queries;
using GraphqlCoreDemo.Infrastructure;
using GraphqlCoreDemo.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GraphqlCoreDemo.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var connection = @"Server=(localdb)\mssqllocaldb;Database=MyGraphqlDemo;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));

            services.AddSingleton<IDocumentWriter, DocumentWriter>();
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<IDataLoaderContextAccessor, DataLoaderContextAccessor>();
            services.AddSingleton<DataLoaderDocumentListener>();

            services.AddSingleton<ProductType>();
            services.AddSingleton<StoreType>();

            services.AddSingleton<ProductInputType>();
            services.AddSingleton<StoreInputType>();

            services.AddSingleton<QueryBase>();
            services.AddSingleton<MutationBase>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            var sp = services.BuildServiceProvider();
            services.AddSingleton<ISchema>(new GraphqlSchema(new FuncDependencyResolver(type => sp.GetService(type))));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseGraphiQl("/api/graphql");
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
