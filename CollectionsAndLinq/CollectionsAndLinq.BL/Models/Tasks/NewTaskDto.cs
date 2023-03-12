using CollectionsAndLinq.DAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsAndLinq.BL.Models.Tasks
{
    public record NewTaskDto(
    int Id,
    int ProjectId,
    int PerformerId,
    string Name,
    string Description,
    TaskState State,
    DateTime CreatedAt,
    DateTime? FinishedAt)
    {

    }
}
