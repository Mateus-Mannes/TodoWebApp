using System.Text.Json.Serialization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using TodoApp.Services;
using Microsoft.AspNetCore.ResponseCompression;
using TodoApp.Extensions;
using TodoApp.Options;

var builder = WebApplication.CreateBuilder(args);
ConfigureAuthentication();
ConfigureMvc();
ConfigureOptions();
ConfigureServices();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseResponseCompression();
app.MapControllers();
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();

void ConfigureAuthentication()
{
    var options = new JwtOptions();
    builder.Configuration.GetSection(JwtOptions.Section).Bind(options);
    var key = Encoding.ASCII.GetBytes(options.JwtKey ?? "");
    builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(x =>
    {
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
}

void ConfigureMvc()
{
    builder.Services.AddControllers()
    .AddJsonOptions(x => { x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; })
    .ConfigureApiBehaviorOptions(x => x.SuppressModelStateInvalidFilter = true);

    builder.Services.AddResponseCompression(options =>
    {
        options.Providers.Add<GzipCompressionProvider>();
    });

    builder.Services.Configure<GzipCompressionProviderOptions>(options =>
    {
        options.Level = System.IO.Compression.CompressionLevel.Optimal;
    });
}

void ConfigureOptions()
{
    builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(JwtOptions.Section));
}

void ConfigureServices()
{
    builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
    builder.Services.AddDbContext();
    builder.Services.AddRepositories();
    builder.Services.AddMapper();
    builder.Services.AddTransient<TokenService>();
}
