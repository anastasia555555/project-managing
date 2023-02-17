using AutoMapper.Execution;
using CollectionsAndLinq.BL.Entities;
using CollectionsAndLinq.BL.Models;
using CollectionsAndLinq.BL.Models.Projects;
using CollectionsAndLinq.BL.Models.Tasks;
using CollectionsAndLinq.BL.Models.Teams;
using CollectionsAndLinq.BL.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColletionsAndLinq.Console.Display
{
    public static class Display
    {
        
        public static void Show(Dictionary<string, int> TasksCountInProjectsByUser)
        {
            if (TasksCountInProjectsByUser.Count == 0)
            {
                System.Console.WriteLine("No matching results.");
                return;
            }

            System.Console.WriteLine("Project id : Name : Number of tasks");
            foreach (var item in TasksCountInProjectsByUser)
            {
                System.Console.WriteLine("<-------------------------------------->------------<------------------------------------->");
                System.Console.WriteLine($"\nProject ({item.Key}) : {item.Value}");
            }
        }

        public static void Show(List<TaskDto> CapitalTasksByUser)
        {
            if (CapitalTasksByUser.Count == 0)
            {
                System.Console.WriteLine("No matching results.");
                return;
            }
            System.Console.WriteLine("Task id : Name : State : CreatedAt : (FinishedAt)");
            foreach (var item in CapitalTasksByUser)
            {
                System.Console.WriteLine("<-------------------------------------->------------<------------------------------------->");
                System.Console.WriteLine($"\n{item.Id} : {item.Name} : {item.State} : {item.CreatedAt} : {item.FinishedAt}");
                System.Console.WriteLine($"\tDescription: {item.Description}");
            }
        }

        public static void Show(List<(int Id, string Name)> ProjectsByTeamSize)
        {
            if (ProjectsByTeamSize.Count == 0)
            {
                System.Console.WriteLine("No matching results.");
                return;
            }

            System.Console.WriteLine("Project id : Name");
            System.Console.WriteLine("<-------------------------------------->------------<------------------------------------->");

            foreach (var item in ProjectsByTeamSize)
            {
                System.Console.WriteLine($"\n{item.Id} : {item.Name}");
            }
        }

        public static void Show(List<TeamWithMembersDto> SortedTeamByMembersWithYear)
        {
            if (SortedTeamByMembersWithYear.Count == 0)
            {
                System.Console.WriteLine("No matching results.");
                return;
            }
            System.Console.WriteLine("Team id : Name : List of Members");
            foreach (var item in SortedTeamByMembersWithYear)
            {
                System.Console.WriteLine("<-------------------------------------->------------<------------------------------------->");
                System.Console.WriteLine($"\n{item.Id} : {item.Name}");
                foreach (var member in item.Members)
                {
                    System.Console.WriteLine($"\tMember ({member.Id}) {member.FirstName} {member.LastName} : {member.Email} : Registered {member.RegisteredAt} : Birthday {member.BirthDay}");
                }
            }
        }

        public static void Show(List<UserWithTasksDto> SortedUsersWithSortedTasks)
        {
            if (SortedUsersWithSortedTasks.Count == 0)
            {
                System.Console.WriteLine("No matching results.");
                return;
            }

            System.Console.WriteLine("User id : Name : Email : List of Tasks");
            foreach (var item in SortedUsersWithSortedTasks)
            {
                System.Console.WriteLine("<-------------------------------------->------------<------------------------------------->");
                System.Console.WriteLine($"\n({item.Id}) {item.FirstName} {item.LastName} : {item.Email} : Registered {item.RegisteredAt} : Birthday {item.BirthDay}");
                foreach (var task in item.Tasks)
                {
                    System.Console.WriteLine($"\tTask ({task.Id})  {task.Name} : {task.State} : Created {task.CreatedAt} : Finished {(task.FinishedAt == null ? "no information" : task.FinishedAt)}");
                    System.Console.WriteLine($"\tDescription: {task.Description}");
                }
                System.Console.WriteLine();
            }
        }

        public static void Show(UserInfoDto UserInfo)
        {

            if (UserInfo is null)
            {
                System.Console.WriteLine("No matching results.");
                return;
            }

            System.Console.WriteLine("\n<-------------------------------------->------------<------------------------------------->");

            System.Console.WriteLine("User id : Name : Email");
            System.Console.WriteLine($"({UserInfo.User.Id}) {UserInfo.User.FirstName} {UserInfo.User.LastName} : {UserInfo.User.Email} : Registered {UserInfo.User.RegisteredAt} : Birthday {UserInfo.User.BirthDay}");

            if(UserInfo.LastProject is not null)
            {
                System.Console.WriteLine("\nLast Project id : Name");
                System.Console.WriteLine($"{UserInfo.LastProject.Id} : {UserInfo.LastProject.Name} : Created {UserInfo.LastProject.CreatedAt} : Deadline {UserInfo.LastProject.Deadline}");
                System.Console.WriteLine($"\tDescription: {UserInfo.LastProject.Description}");
            }

            System.Console.WriteLine($"\nNumber of tasks in last project: {UserInfo.LastProjectTasksCount}.");
            System.Console.WriteLine($"\nNumber of unfinished or cancelled tasks in last project: {UserInfo.LastProjectTasksCount}.");

            if(UserInfo.LongestTask is not null)
            {
                System.Console.WriteLine("\nLongest task");
                System.Console.WriteLine($"({UserInfo.LongestTask.Id})  {UserInfo.LongestTask.Name} : {UserInfo.LongestTask.State} : Created {UserInfo.LongestTask.CreatedAt} : Finished {(UserInfo.LongestTask.FinishedAt == null ? "no information" : UserInfo.LongestTask.FinishedAt)}");
                System.Console.WriteLine($"\tDescription: {UserInfo.LongestTask.Description}");
            }
            System.Console.WriteLine("<-------------------------------------->------------<------------------------------------->");
        }

        public static void Show(List<ProjectInfoDto> ProjectsInfo)
        {

            if (ProjectsInfo.Count == 0)
            {
                System.Console.WriteLine("No matching results.");
                return;
            }

            System.Console.WriteLine("Project id : Name");
            foreach (var projectInfo in ProjectsInfo)
            {
                System.Console.WriteLine("<-------------------------------------->------------<------------------------------------->");
                System.Console.WriteLine($"\n{projectInfo.Project.Id} : {projectInfo.Project.Name} : Created {projectInfo.Project.CreatedAt} : Deadline {projectInfo.Project.Deadline}");
                System.Console.WriteLine($"\tDescription: {projectInfo.Project.Description}");

                if(projectInfo.LongestTaskByDescription is not null)
                {
                    System.Console.WriteLine("\nLongest task by description");
                    System.Console.WriteLine($"({projectInfo.LongestTaskByDescription.Id})  {projectInfo.LongestTaskByDescription.Name} : {projectInfo.LongestTaskByDescription.State} : Created {projectInfo.LongestTaskByDescription.CreatedAt} : Finished {(projectInfo.LongestTaskByDescription.FinishedAt == null ? "no information" : projectInfo.LongestTaskByDescription.FinishedAt)}");
                    System.Console.WriteLine($"\tDescription: {projectInfo.LongestTaskByDescription.Description}");

                }

                if(projectInfo.ShortestTaskByName is not null)
                {
                    System.Console.WriteLine("\nShortest task by name");
                    System.Console.WriteLine($"({projectInfo.ShortestTaskByName.Id})  {projectInfo.ShortestTaskByName.Name} : {projectInfo.ShortestTaskByName.State} : Created {projectInfo.ShortestTaskByName.CreatedAt} : Finished {(projectInfo.ShortestTaskByName.FinishedAt == null ? "no information" : projectInfo.ShortestTaskByName.FinishedAt)}");
                    System.Console.WriteLine($"\tDescription: {projectInfo.ShortestTaskByName.Description}");
                }

                if (projectInfo.TeamMembersCount is not null)
                {
                    System.Console.WriteLine($"\nNumber of members in the team: {projectInfo.TeamMembersCount}.");
                }
            }
        }

        public static void Show(PagedList<FullProjectDto> SortedFilteredPageOfProjects)
        {

            if (SortedFilteredPageOfProjects.TotalCount == 0)
            {
                System.Console.WriteLine("No matching results.");
                return;
            }

            System.Console.WriteLine("Project id : Name");
            foreach (var project in SortedFilteredPageOfProjects.Items)
            {
                System.Console.WriteLine("<-------------------------------------->------------<------------------------------------->");
                System.Console.WriteLine($"\n{project.Id} : {project.Name} : Created {project.CreatedAt} : Deadline {project.Deadline}");
                System.Console.WriteLine($"\tDescription: {project.Description}");

                foreach (var task in project.Tasks)
                {
                    System.Console.WriteLine($"\t\tTask ({task.Id})  {task.Name} : {task.State} : Created {task.CreatedAt} : Finished {(task.FinishedAt == null ? "no information" : task.FinishedAt)}");
                    System.Console.WriteLine($"\t\tDescription: {task.Description}");
                    System.Console.WriteLine($"\t\tPerformer: ({task.Perfomer.Id}) {task.Perfomer.FirstName} {task.Perfomer.LastName}");
                }


                System.Console.WriteLine("\nAuthor of the project id : Name : Email : List of Tasks");
                System.Console.WriteLine($"({project.Author.Id}) {project.Author.FirstName} {project.Author.LastName} : {project.Author.Email} : Registered {project.Author.RegisteredAt} : Birthday {project.Author.BirthDay}");

                System.Console.WriteLine("\nTeam leading the project id : Name ");
                System.Console.WriteLine($"{project.Team.Id} : {project.Team.Name} : Created {project.Team.CreatedAt}");
            }
        }

    }
}
