using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PublishingHouse_Business.Abstract;
using PublishingHouse_Business.Concrete;
using PublishingHouse_Business.Mapping;
using PublishingHouse_DataAccess.Data;
using PublishingHouse_DataAccess.Repositories.Abstract;
using PublishingHouse_DataAccess.Repositories.Concrete;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IBookService, BookManager>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<ICustomerService, CustomerManager>();
builder.Services.AddScoped<IShoppingService, ShoppingManager>();
builder.Services.AddScoped<IWriterService, WriterManager>();


builder.Services.AddScoped<IBookRepository, EfBookRepository>();
builder.Services.AddScoped<ICategoryRepository, EfCategoryRepository>();
builder.Services.AddScoped<ICustomerRepository, EfCustomerRepository>();
builder.Services.AddScoped<IShoppingRepository, EfShoppingRepository>();
builder.Services.AddScoped<IWriterRepository, EfWriterRepository>();


builder.Services.AddAutoMapper(typeof(MapProfile));

builder.Services.AddDbContext<PublishingHouseDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("db")));

builder.Services.AddCors(opt => opt.AddPolicy("allow", cpb =>
{
    cpb.AllowAnyOrigin();
    cpb.AllowAnyHeader();
    cpb.AllowAnyMethod();
    //cpb.WithMethods();//Belli adreslerden gelen istekler api ye eriþimi olsun istiyorsak

}));

var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("token:secret").Value));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                                .AddJwtBearer(options =>
                                {
                                    options.SaveToken = true; //eðer authorization baþarýlý olursa ben bu üretilen token i sunucuda autencation property içinde saklarým
                                    options.TokenValidationParameters = new TokenValidationParameters
                                    {
                                        ValidateIssuer = true ,
                                        ValidateAudience=true,
                                        ValidateActor=true,
                                        ValidIssuer = "u.civgin@gmail.com",
                                        ValidAudience ="u.civgin@gmail.com",
                                        IssuerSigningKey=key
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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
