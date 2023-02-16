using CollectionsAndLinq.BL.Services;
using CollectionsAndLinq.BL.MappingProfiles;
using AutoMapper;
using CollectionsAndLinq.BL.Models.Projects;
using ColletionsAndLinq.Console.Display;

namespace ColletionsAndLinq.Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            var mapper = AddMapper();

            var service = AddServices(mapper);

            var result = service.GetSortedFilteredPageOfProjectsAsync(new(5, 10), null, new(SortingProperty.Name, SortingOrder.Descending)).Result;

            Display.Display dis = new(result);
        }

        public static Mapper AddMapper()
        {
            MapperConfiguration mappercfg =
            new(cfg =>
            {
                cfg.AddProfile<UserProfile>();
                cfg.AddProfile<TeamProfile>();
                cfg.AddProfile<TaskProfile>();
                cfg.AddProfile<ProjectProfile>();
            });

            return new Mapper(mappercfg);
        }

        public static DataProcessingService AddServices(IMapper mapper)
        {
            HttpClient client = new();
            DataProvider dataProvider = new(client);
            
            return new(dataProvider, mapper);
        }
    }
}