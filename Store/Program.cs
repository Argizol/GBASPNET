using NetStore.Abstraction;
using NetStore.Repositories;

namespace NetStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(typeof(MappingProfiles));
            builder.Services.AddMemoryCache();

            // регистрация с Autofac
            //builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            //builder.Host.ConfigureContainer<ContainerBuilder>(cb => cb.RegisterType<ProductRepository>()
            //            .As<IProductRepository>());

            //builder.Host.ConfigureContainer<ContainerBuilder>(cb => cb.RegisterType<GroupRepository>()
            //.As<IGroupRepository>());

            builder.Services.AddSingleton<IProductRepository, ProductRepository>();
            builder.Services.AddSingleton<IGroupRepository, GroupRepository>();
            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
