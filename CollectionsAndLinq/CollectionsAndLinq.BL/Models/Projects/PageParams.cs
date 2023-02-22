using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsAndLinq.BL.Models.Projects
{
    public record PageParams(
        PageModel pageModel = null, 
        FilterModel filterModel = null, 
        SortingModel sortingModel = null)
    {
    }
}
