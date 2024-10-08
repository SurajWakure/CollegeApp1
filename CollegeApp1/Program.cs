using Collegeapp1.Configurations;
using Collegeapp1.Data;
using Collegeapp1.Data.IReposetory;
using Collegeapp1.Data.Reposetory;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<CollegeDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(AutomapperConfig));
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "jwt autherization header using the bearer scheme add yout token in htt eetext unput eg like",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference=new OpenApiReference
                {
                    Id="Bearer",
                    Type=ReferenceType.SecurityScheme

                },
                Scheme="oauth2",
                Name="Bearer",
                In=ParameterLocation.Header
            },
            new List<string>()
        }
    });
}
);

builder.Services.AddTransient<IStudentReposetory, StudentReposetory>();
builder.Services.AddScoped(typeof(ICollegeReposetory<>), typeof(CollegeReposetory<>));

builder.Services.AddCors(options => {


    /* options.AddDefaultPolicy(policy =>
     {

         //policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();//only few origins 
         policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
         //we need to define the body first 
     });*/
    //ifdefault then we dont need this down policy 

    options.AddPolicy("AllowAllDomain", policy =>
    {

        //policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();//only few origins 
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        //we need to define the body first 
    });
    options.AddPolicy("AllowOnlyLocalHost", policy =>
    {

        policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();//only few origins 
                                                                                      // policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); we need to define the body first 
    });
    options.AddPolicy("AllowOnlyGoogleApps", policy =>
    {

        policy.WithOrigins("http://google.com,http://gmail.com,http://drive.google.com").AllowAnyHeader().AllowAnyMethod();//only few origins 
                                                                                      // policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); we need to define the body first 
    });
    options.AddPolicy("AllowOnlyMicrosoft", policy =>
    {

        policy.WithOrigins("http://outlook.com,http://microsoft.com,http://ondrive.google.com").AllowAnyHeader().AllowAnyMethod();//only few origins 
                                                                                                                           // policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); we need to define the body first 
    });
});   //allow any corese 



//JWT 
var Key1 = Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("JWTSecretKeyForGoogle"));
var Key2 = Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("JWTSecretKeyForMicrosoft"));
var  Key3=Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("JWTSecretForLocal"));
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer("LoginFoeGoogleUsers ", optons =>
{
    ///optons.RequireHttpsMetadata = false;
    optons.SaveToken = true;
    optons.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new
        SymmetricSecurityKey(Key1),
        ValidateIssuer = false,
    };
}).AddJwtBearer("LoginForMicrosoftUsers ", optons =>
{
    ///optons.RequireHttpsMetadata = false;
    optons.SaveToken = true;
    optons.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new
        SymmetricSecurityKey(Key2),
        ValidateIssuer = false,
    };
}).AddJwtBearer("LoginForLocalUsers ", optons =>
{
    ///optons.RequireHttpsMetadata = false;
    optons.SaveToken = true;
    optons.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new
        SymmetricSecurityKey(Key3),
        ValidateIssuer = false,
    };
});



var app = builder.Build();   

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAllDomain");
//we are using default policy 
//named policies
app.UseRouting();
app.MapControllers();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("api/testingendpoint",
        context => context.Response.WriteAsync("Test Response"))
        .RequireCors("AllowOnlyLocalHost");

    endpoints.MapControllers().RequireCors("AllowAllDomain");

    endpoints.MapGet("api/testingendpoint2",
       context => context.Response.WriteAsync(builder.Configuration.GetValue<string>("JWTSecret")));
});



app.Run();


//some changes are there for merging 
//some changes are here 
//another change


