using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsAndLinq.BL.Models.Teams
{
    public record NewTeamDto(
    int Id,
    string Name,
    DateTime CreatedAt)
    {

    }
}
