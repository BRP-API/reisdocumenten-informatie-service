using Brp.Shared.Infrastructure.Logging;
using Brp.Shared.Infrastructure.Utils;
using HaalCentraal.ReisdocumentService.Extensions;
using HaalCentraal.ReisdocumentService.Repositories;
using Serilog;

Log.Logger = SerilogHelpers.SetupSerilogBootstrapLogger();

try
{
    Log.Information($"Starting {AssemblyHelpers.Name} v{AssemblyHelpers.Version}. TimeZone: {TimeZoneInfo.Local.StandardName}. Now: {DateTime.Now}");

    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddHttpContextAccessor();

    builder.SetupSerilog(Log.Logger);

    builder.Services.AddControllers()
                    .ConfigureInvalidModelStateHandling()
                    .AddNewtonsoftJson();

    builder.Services.AddScoped<ReisdocumentRepository>();

    var app = builder.Build();

    app.SetupSerilogRequestLogging();

    app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, $"{AssemblyHelpers.Name} terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
