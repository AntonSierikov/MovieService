using FluentValidation;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MovieService.ApplicationServices;
using MovieService.ApplicationServices.Hangfire;
using MovieService.Persistence;
using MovieService.Settings;
using MovieService.Validations;
using TmbdClient;

[assembly: ApiController]

var builder = WebApplication.CreateBuilder(args);
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .Build();
var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var services = builder.Services;
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddOptions();

services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy  =>
        {
            policy.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
        });
});
AddHangfire();

services.AddDbContext<MovieContext>(
    (sp, options) =>
        options.UseNpgsql(
            sp.GetRequiredService<IOptions<MovieAppOptions>>().Value.ConnectionStrings.PostgresSqlConnectionString));


services.Configure<MovieAppOptions>(configuration);
services.AddScoped<IUnitOfWork, UnitOfWork>();
services.AddTransient<MoviesService>();
services.AddTransient<MovieAgent>();
services.AddValidatorsFromAssemblyContaining<MovieReviewDtoValidation>();

var app = builder.Build();

app.UseAuthorization();
app.UseCors(MyAllowSpecificOrigins);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.MapControllers();
app.Run();



RunMigration(app);
app.UseHangfireDashboard();
// RunHangfire();


void RunMigration(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var movieContext = scope.ServiceProvider.GetRequiredService<MovieContext>();
    movieContext.Database.Migrate();
}

void RunHangfire()
{
    RecurringJob.AddOrUpdate<FeedTopMoviesBackgroundJob>(
        nameof(FeedTopMoviesBackgroundJob),
        x => x.ExecuteAsync(),
        "*/1 * * * *");
}

void AddHangfire()
{
    var movieAppOptions = configuration
        .Get<MovieAppOptions>();
    var postgresSqlConnectionString = movieAppOptions
        .ConnectionStrings.PostgresSqlConnectionString;

    services.AddHangfire(configuration =>
            configuration.UsePostgreSqlStorage(
                postgresSqlConnectionString,
                new PostgreSqlStorageOptions()
                {
                    SchemaName = "hangfire"
                }))
        .AddHangfireServer();

    services.AddTransient<FeedTopMoviesBackgroundJob>();
}