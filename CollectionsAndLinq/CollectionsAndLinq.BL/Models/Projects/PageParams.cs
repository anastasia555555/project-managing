namespace CollectionsAndLinq.BL.Models.Projects
{
    public record PageParams(
        PageModel pageModel = null, 
        FilterModel filterModel = null, 
        SortingModel sortingModel = null)
    {
    }
}
