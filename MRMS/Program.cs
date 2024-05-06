using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MRMS.Data;
using Microsoft.AspNetCore.Identity;
using MRMS.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<MRMSContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MRMSContext") ?? throw new InvalidOperationException("Connection string 'MRMSContext' not found.")));

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<MRMSContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    //app.UseExceptionHandler("/Error");
    app.UseDeveloperExceptionPage();
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
