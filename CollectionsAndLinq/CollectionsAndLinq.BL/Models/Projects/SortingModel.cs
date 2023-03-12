using CollectionsAndLinq.BL.Models.Enums;

namespace CollectionsAndLinq.BL.Models.Projects;

public record SortingModel(
    SortingProperty Property,
    SortingOrder Order)
{

}
