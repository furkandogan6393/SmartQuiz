using Autofac;
using Autofac.Extensions.DependencyInjection;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.DependencyResolvers;
using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Diagnostics;
using WebAPI.DataAccess.Concrete;
using WebAPI.DependencyResolvers;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();



builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Bearer {token}",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
    c.MapType<IFormFile>(() => new OpenApiSchema
    {
        Type = "string",
        Format = "binary"
    });
});

// builder.Services satýrlarýnýn altýna ekle
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", builder =>
    {
        builder.WithOrigins("http://localhost:4200")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(b => 
{
    b.RegisterModule(new AutofacBusinessModule());
}
);

builder.Services.AddDbContext<UserDbContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = tokenOptions.Issuer,
            ValidAudience = tokenOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
        };
    }); 

builder.Services.AddDependencyResolvers(new ICoreModule[] {
    new CoreModule() // Core/DependencyResolvers/CoreModule.cs dosyan varsa
});

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAngular");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<UserDbContext>();

    if (!context.OperationClaims.Any())
    {
        context.OperationClaims.AddRange(
            new OperationClaim { Name = "superadmin" },
            new OperationClaim { Name = "admin" },
            new OperationClaim { Name = "user" }
        );
        context.SaveChanges();
    }

    if (!context.Users.Any())
    {
        byte[] passwordHash, passwordSalt;
        HashingHelper.CreatePasswordHash("ferhafbt6393", out passwordHash, out passwordSalt);
        var superAdmin = new User
        {
            UserId = Guid.NewGuid().ToString(),
            FirstName = "Super",
            LastName = "Admin",
            Email = "admin@system.com",
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            TenantId = "system"
        };
        context.Users.Add(superAdmin);
        context.SaveChanges();

        var superAdminClaim = context.OperationClaims.First(x => x.Name == "superadmin");
        context.UserOperationClaims.Add(new UserOperationClaim
        {
            UserId = superAdmin.UserId,
            OperationClaimId = superAdminClaim.Id
        });
        context.SaveChanges();
    }
}

app.Run();
