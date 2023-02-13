using CollectionsAndLinq.BL.Services;
using CollectionsAndLinq.BL.MappingProfiles;
using AutoMapper;
using System.Net.Http.Json;

namespace ColletionsAndLinq.Console
{
    public class Program
    {
        static void Main(string[] args)
        {

            MapperConfiguration mappercfg =
            new (cfg =>
            {
                cfg.AddProfile<UserProfile>();
                cfg.AddProfile<TeamProfile>();
                cfg.AddProfile<TaskProfile>();
            });

            IMapper mapper = new Mapper(mappercfg);

            HttpClient client = new();
            DataProvider dataProvider = new(client);
            DataProcessingService ser = new(dataProvider, mapper);

            var result = ser.GetUserInfoAsync(2).Result;

            System.Console.WriteLine($"{result.User.LastName} : {result.LastProject}");
        }
    }
}