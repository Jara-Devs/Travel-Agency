using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Travel_Agency_Core;
using Travel_Agency_DataBase;
using Travel_Agency_DataBase.Core;
using Travel_Agency_DataBase.Queries;
using Travel_Agency_DataBase.Queries.Users;
using Travel_Agency_Logic;
using Travel_Agency_Logic.Auth;
using Travel_Agency_Logic.Core;
using Travel_Agency_Logic.Services;
using Travel_Agency_Logic.Offers;
using Travel_Agency_Domain.Offers;
using Travel_Agency_Domain.Packages;
using Travel_Agency_Domain.Services;
using Travel_Agency_Domain.Users;
using Travel_Agency_Logic.Packages;
using Travel_Agency_Domain.Payments;

namespace Travel_Agency_Api;

public static class ProgramServices
{
    public static void AddAllServices(this IServiceCollection services)
    {
        // Configure queries
        services.AddScoped<IQueryEntity<UserAgency>, UserAgencyQuery>();
        services.AddScoped<IQueryEntity<User>, UserAppQuery>();
        services.AddScoped<IQueryEntity<Excursion>, PublicQuery<Excursion>>();
        services.AddScoped<IQueryEntity<OverNightExcursion>, PublicQuery<OverNightExcursion>>();
        services.AddScoped<IQueryEntity<Hotel>, PublicQuery<Hotel>>();
        services.AddScoped<IQueryEntity<TouristActivity>, PublicQuery<TouristActivity>>();
        services.AddScoped<IQueryEntity<TouristPlace>, PublicQuery<TouristPlace>>();
        services.AddScoped<IQueryEntity<ExcursionOffer>, PublicQuery<ExcursionOffer>>();
        services.AddScoped<IQueryEntity<HotelOffer>, PublicQuery<HotelOffer>>();
        services.AddScoped<IQueryEntity<FlightOffer>, PublicQuery<FlightOffer>>();
        services.AddScoped<IQueryEntity<Package>, PublicQuery<Package>>();

        // Configure commands
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IExcursionService, ExcursionService>();
        services.AddScoped<IHotelService, HotelService>();
        services.AddScoped<ITouristPlaceService, TouristPlaceService>();
        services.AddScoped<IOfferService<HotelOffer>, OfferService<HotelOffer>>();
        services.AddScoped<IOfferService<ExcursionOffer>, OfferService<ExcursionOffer>>();
        services.AddScoped<IOfferService<FlightOffer>, OfferService<FlightOffer>>();
        services.AddScoped<ITouristActivityService, TouristActivityService>();
        services.AddScoped<IHotelService, HotelService>();
        services.AddScoped<IPackageService, PackageService>();

        services.AddScoped<SecurityService>();
    }

    public static void AddDataBase(this IServiceCollection services, IConfigurationRoot configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        var serverVersion = new MySqlServerVersion(new Version(8, 0, 33));

        services.AddDbContext<TravelAgencyContext>(options =>
            options.UseMySql(connectionString, serverVersion));
    }

    public static void AddMyAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey =
                        new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
                };
            });
    }

    public static void ConfigureOdata(this IServiceCollection services)
    {
        services.AddControllers().AddOData(opt =>
            opt.Select().Count().Filter().Expand().Select().OrderBy().SetMaxTop(50)
                .AddRouteComponents("odata", GetEdmModel()));
    }

    public static void ConfigurePolicies(this IServiceCollection service)
    {
        service.AddAuthorization(options =>
            options.AddPolicy(Policies.App, policy => policy.RequireRole(Roles.AdminApp, Roles.EmployeeApp)));
        service.AddAuthorization(options =>
            options.AddPolicy(Policies.Agency,
                policy => policy.RequireRole(Roles.AdminAgency, Roles.ManagerAgency, Roles.EmployeeAgency)));
        service.AddAuthorization(options =>
            options.AddPolicy(Policies.AgencyManager,
                policy => policy.RequireRole(Roles.AdminAgency, Roles.ManagerAgency)));
    }

    private static IEdmModel GetEdmModel()
    {
        var builder = new ODataConventionModelBuilder();

        // Configure entities
        builder.EntitySet<UserAgency>("UserAgency");
        builder.EntitySet<User>("UserApp");
        builder.EntitySet<Excursion>("Excursion");
        builder.EntitySet<Hotel>("Hotel");
        builder.EntitySet<TouristPlace>("TouristPlace");
        builder.EntitySet<TouristActivity>("TouristActivity");
        builder.EntitySet<HotelOffer>("HotelOffer");
        builder.EntitySet<ExcursionOffer>("ExcursionOffer");
        builder.EntitySet<FlightOffer>("FlightOffer");
        builder.EntitySet<Package>("Package");
        builder.EntitySet<OverNightExcursion>("OverNightExcursion");
        builder.EntitySet<ReserveTicket>("ReserveTicket");

        return builder.GetEdmModel();
    }
}