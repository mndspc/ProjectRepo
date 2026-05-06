using DAL.DataAccess;
using DAL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(); //web api needs controllers only
builder.Services.AddScoped<IProductService<Product>, ProductService>();
builder.Services.AddScoped<IAdminInfoService<AdminInfo>,AdminInfoService>();

//To configure swagger service
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Ecommerce API",
        Description = "Ecommerce Application",
        TermsOfService = new Uri("https://www.Cognizant.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Soyeb Ghachi",
            Email = "shoaib.ghachi@gmail.com",
            Url = new Uri("https://linkedin.com/soyeb")
        },
        License = new OpenApiLicense
        {
            Name = "Cognizant",
            Url = new Uri("https://cognizant.com/license")
        }
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please Enter Token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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

});

//To Configure JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

//CORS policies
//Default CORS policy
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        //To grant access for any domain-for any header any method
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();

        //To grant access for specific domain for specific method
        //builder.WithOrigins("http://192.168.2.1", "http://localhost:4200").AllowAnyHeader().WithMethods("GET");
    });
});

//Named CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowGetAndPost", builder =>
    {
        builder.WithOrigins("http://192.168.30.1", "http://localhost:4200").AllowAnyHeader().WithMethods("GET","POST") ;
    });
}
    );

//API Versioning
builder.Services.AddApiVersioning(options =>
{
    //To configure default API Versioning
    options.DefaultApiVersion = new ApiVersion(1,0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseRouting();
app.UseCors();//middleware for default CORS
app.UseCors("AllowGetAndPost");//middleware for named CORS
app.UseAuthentication();
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json","My API v1");
});
app.UseMiddleware<ExceptionMiddleware>();
app.UseEndpoints(endpoints => {  endpoints.MapControllers(); });

app.Run();
