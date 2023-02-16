using CollectionsAndLinq.BL.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsAndLinq.BL.Entities
{
    public record TaskWithPerfomer(
    int Id,
    string Name,
    string Description,
    int ProjectId,
    TaskState State,
    DateTime CreatedAt,
    DateTime? FinishedAt,
    User Perfomer)
    {

    }
}
