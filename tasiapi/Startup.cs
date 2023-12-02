//using Microsoft.AspNetCore.Builder;

//namespace tasiapi
//{
//    public class Startup
//    {
//        public void Configure(IApplicationBuilder app)
//        {
//            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
//        }
//    }
//}




using tasiapi.Interfaces;

namespace tasiapi.Data;

public class Startup
{
    public IConfiguration Configuration { get; }



    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        // Servisleri burada tanımlayın
        //services.AddTransient<IMyTransientService, MyTransientService>();
        //services.AddScoped<IMyScopedService, MyScopedService>();
        //services.AddSingleton<IMySingletonService, MySingletonService>();
        services.AddScoped<IAppRepository, AppRepository> ();
        services.AddScoped<IAppRepository, tasiapi.Data.AppRepository>();
        services.AddControllers();
        //services.AddAutoMapper();
        //services.AddAutoMapper(typeof(Startup));

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            // Prodüksiyon ortamı için gerekli yapılandırmaları yapın
        }

        


        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}