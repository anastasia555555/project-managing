using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsAndLinq.BL.Models.Projects
{
    public record NewProjectDto(
        int Id,
        int AuthorId,
        int TeamId,
        string Name,
        string Description,
        DateTime CreatedAt,
        DateTime Deadline)
    {
    }
}
