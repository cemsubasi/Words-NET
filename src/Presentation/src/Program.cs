using Words.Persistence.Contexts;
using Words.Persistence.Extensions;
using Words.Persistence.Middlewares;
using Words.Presentation.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

builder.Services.AddRepositories(ServiceLifetime.Scoped);
builder.Services.AddServices(ServiceLifetime.Scoped);
builder.Services.AddValidations(ServiceLifetime.Scoped);
builder.Services.AddUtils(ServiceLifetime.Singleton);
builder.Services.AddAuthentication(args);
builder.Services.AddSwagger();

builder.Services.AddAuthorization();
builder.Services.AddScoped<SessionMiddleware>();
builder.Services.ConfigureOptions<JwtOptionSetup>();
builder.Services.ConfigureOptions<DbOptionSetup>();
builder.Services.AddDbContext<MainDbContext>();

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseCors(builder =>
        builder.AllowAnyOrigin()
          .AllowAnyHeader()
          .AllowAnyMethod());

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<SessionMiddleware>();

app.MapControllers();

app.Run();
