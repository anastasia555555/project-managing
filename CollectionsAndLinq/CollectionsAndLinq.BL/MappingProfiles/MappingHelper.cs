using CollectionsAndLinq.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsAndLinq.BL.MappingProfiles
{
    public static class MappingHelper
    {
        public static string Capitalize(string str)
        {
            if (String.IsNullOrEmpty(str))
            {
                return String.Empty;
            }

            return str.ToUpper()[0] + str.ToLower()[1..];
        }

        public static string TaskStateToString(TaskState state)
        {
            string str;

            switch (state)
            {
                case TaskState.ToDo:
                    str = "To Do";
                    break;
                case TaskState.InProgress:
                    str = "In Progress";
                    break;
                case TaskState.Done:
                    str = "Done";
                    break;
                case TaskState.Canceled:
                    str = "Canceled";
                    break;
                default:
                    str = string.Empty;
                    break;
            }

            return str;
        }
    }
}
