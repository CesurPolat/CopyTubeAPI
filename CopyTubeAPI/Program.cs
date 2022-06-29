using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.AddSecurityDefinition("Bearer",new OpenApiSecurityScheme
    {
        Scheme="Bearer",
        BearerFormat="JWT",
        In=ParameterLocation.Header,
        Name="Authorization",
        Description="Bearer Authentication with JWT Token",
        Type=SecuritySchemeType.Http
    });
    x.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference=new OpenApiReference
                {
                    Id="Bearer",
                    Type=ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });

    //Add Description
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    x.IncludeXmlComments(xmlPath);

});

var tokenOptions = builder.Configuration;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateActor=true,
        ValidateAudience=true,
        ValidateLifetime=true,
        ValidateIssuerSigningKey=true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseSwagger(x => x.SerializeAsV2 = false);
app.UseSwaggerUI(x =>
{
    x.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
});


app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoint =>
{
    endpoint.MapControllers();
});

app.Run();
