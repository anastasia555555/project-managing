﻿using System.Reflection;
using AutoMapper;
using CollectionsAndLinq.BL.Interfaces;
using CollectionsAndLinq.BL.MappingProfiles;
using CollectionsAndLinq.BL.Services;

namespace CollectionsAndLinq.WebApi.Extentions
{
    public static class ServiceExtentions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddTransient<ProjectService>();
            services.AddTransient<TaskService>();
            services.AddTransient<TeamService>();
            services.AddTransient<UserService>();
        }

        public static void AddAutoMapper(this IServiceCollection services)
        {

            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<ProjectProfile>();
                cfg.AddProfile<TaskProfile>();
                cfg.AddProfile<TeamProfile>();
                cfg.AddProfile<UserProfile>();
            },
            Assembly.GetExecutingAssembly());
        }
    }
}
