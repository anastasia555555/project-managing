using CollectionsAndLinq.BL.Services;
using System.Reflection;
using AutoMapper;
using CollectionsAndLinq.BL.MappingProfiles;
using CollectionsAndLinq.BL.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.Configuration;
using Microsoft.Extensions.Options;
using CollectionsAndLinq.WebApi.Extentions;

namespace CollectionsAndLinq.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.RegisterAutoMapper();
            builder.Services.RegisterCustomServices();

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

    }
}