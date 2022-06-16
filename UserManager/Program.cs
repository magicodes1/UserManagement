var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.Logger.LogInformation("Building Application......................");

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Logger.LogInformation("Launching Application..........................");

app.Run();
