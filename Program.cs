using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using VueBlog.API.DbContext;
using VueBlog.API.Services.Essay;
using VueBlog.API.Services.User;
var builder = WebApplication.CreateBuilder(args);

////Add Rate-limit
//builder.Services.AddMemoryCache();
//builder.Services.Configure<IpRateLimitOptions>(options =>
//{
//    options.GeneralRules = new List<RateLimitRule>
//    {
//        new RateLimitRule
//        {
//            Endpoint = "*",
//            Limit = 10, // 10 次
//            Period = "10s" // 每 10 秒
//        }
//    };
//});
//builder.Services.AddInMemoryRateLimiting();
//builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();


//Add JWT Authentication
var key = Encoding.UTF8.GetBytes("1ryVPIoTM+h6BgvwWeFpG/pvuCMrZ/GnkveWvRHa5Ac=");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,

            ValidIssuer = "SenBlog",
            ValidAudience = "SenBlog",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1ryVPIoTM+h6BgvwWeFpG/pvuCMrZ/GnkveWvRHa5Ac=")),
            ClockSkew = TimeSpan.Zero // 避免时间误差
        };
    
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization(ops =>
{
    ops.AddPolicy("Admin", policy => policy.RequireClaim("role", "Admin"));
    ops.AddPolicy("User", policy => policy.RequireClaim("role", "User"));
});


builder.Services.AddAuthorization();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//add healthcheck

builder.Services.AddHealthChecks();

// migration

var ConnectString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(ops =>
{
    ops.UseSqlServer(ConnectString);
});

//Add custom DI
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEssayService, EssayService>();

//allow cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVueBlog",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000") // 允许的前端域名
                  .AllowAnyHeader() // 允许任何请求头（需要支持 Authorization）
                  .AllowAnyMethod() // 允许 GET、POST、PUT、DELETE 等
                  .AllowCredentials(); // 允许携带身份凭证（JWT 需要）
        });
});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//add ratelimit mid-component
//app.UseIpRateLimiting();


//allow cors

app.UseCors("AllowVueBlog");


//add useHealthCheck

app.UseHealthChecks("/health");

//add mid-component
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
