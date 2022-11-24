using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using touristmgmApi.Repository;
using touristmgmApi.BusinessLayer;
using touristmgmApi.ActionFilters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(Options => {
    Options.AddPolicy("TouristMgmCORSPolicy",
        builder => builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
        .WithHeaders());
});
var accessKey = Environment.GetEnvironmentVariable("ACCESS_KEY");
var secretKey = Environment.GetEnvironmentVariable("SECRET_KEY");

var credentials = new BasicAWSCredentials(accessKey, secretKey);
var config = new AmazonDynamoDBConfig()
{
    RegionEndpoint = RegionEndpoint.USEast2
};

var client = new AmazonDynamoDBClient(credentials, config);
builder.Services.AddSingleton<IAmazonDynamoDB>(client);
builder.Services.AddSingleton<IDynamoDBContext, DynamoDBContext>();

builder.Services.AddScoped<ITouristRepository, TouristRepository>();
builder.Services.AddScoped<ITouristBusiness, TouristBusiness>();


builder.Services.AddControllers();
builder.Services.AddControllers((config) => {
    config.Filters.Add(new ValidateModelAttribute());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

app.UseCors("TouristMgmCORSPolicy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers();
//    endpoints.MapGet("/", async context =>
//    {
//        await context.Response.WriteAsync("Welcome to running ASP.NET Core on AWS Lambda");
//    });
//});

app.MapControllers();

app.Run();
