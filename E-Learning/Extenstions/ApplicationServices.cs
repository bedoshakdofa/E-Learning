using E_Learning.Data;
using E_Learning.Interfaces;
using E_Learning.services;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Extenstions
{
    public static class ApplicationServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<DbContextApp>(opt => opt.UseNpgsql(config["ConnectionStrings:Database"]));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<ITokenServices, TokenServices>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<ILectureRepository, LectureRepository>();
            services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
            services.AddScoped<IFileServices, FileServices>();

            return services;
        }
    }
}
