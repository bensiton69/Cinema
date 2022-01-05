using API.Data;
using API.Interfaces;
using API.Interfaces.IRepositories;
using API.Interfaces.IServices;
using API.Mapping;
using API.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();

            services.AddSingleton(mapper);
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddSingleton<IReservationService, ReservationService>();
            services.AddScoped<ISeatManagerService, SeatManagerService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IVenueRepository, VenueRepository>();
            services.AddScoped<IShowTimeRepository, ShowTimeRepository>();
            services.AddDbContextPool<DataContext>(options =>
                options.UseSqlServer(config.GetConnectionString("CinemaDBConnection")));

            return services;
        }
    }
}
