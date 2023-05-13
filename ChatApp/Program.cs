using ChatApp.Contexts;
using ChatApp.Hubs;
using ChatApp.Models;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//mapper
builder.Services.AddAutoMapper(typeof(Program));

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();

//swagger and api
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//database
builder.Services.AddDbContext<ChatAppContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//identity
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ChatAppContext>()
    .AddDefaultTokenProviders();

//signalR
builder.Services.AddSignalR();

//paths
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//if dev enable swagger ui
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapHub<ChatHub>("/chat");

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();