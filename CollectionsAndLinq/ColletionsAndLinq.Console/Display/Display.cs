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
    public class Display
    {
        public Display(Dictionary<string, int> TasksCountInProjectsByUser)
        {
            if (TasksCountInProjectsByUser.Count == 0)
            {
                System.Console.WriteLine("No matching results.");
                //return;
            }

            System.Console.WriteLine("Project id : Name : Number of tasks\n");
            foreach (var item in TasksCountInProjectsByUser)
            {
                System.Console.WriteLine($"{item.Key} : {item.Value}");
            }
        }

        public Display(List<TaskDto> CapitalTasksByUser)
        {
            if (CapitalTasksByUser.Count == 0)
            {
                System.Console.WriteLine("No matching results.");
                //return;
            }
            System.Console.WriteLine("Task id : Name : State : CreatedAt : (FinishedAt)\n");
            foreach (var item in CapitalTasksByUser)
            {
                System.Console.WriteLine($"{item.Id} : {item.Name} : {item.State} : {item.CreatedAt} : {item.FinishedAt}");
                System.Console.WriteLine($"\nDescription: {item.Description}");
            }
        }

        public Display(List<(int Id, string Name)> ProjectsByTeamSize)
        {
            if (ProjectsByTeamSize.Count == 0)
            {
                System.Console.WriteLine("No matching results.");
                //return;
            }

            System.Console.WriteLine("Project id : Name\n");
            foreach (var item in ProjectsByTeamSize)
            {
                System.Console.WriteLine($"{item.Id} : {item.Name}");
            }
        }

        public Display(List<TeamWithMembersDto> SortedTeamByMembersWithYear)
        {
            if (SortedTeamByMembersWithYear.Count == 0)
            {
                System.Console.WriteLine("No matching results.");
                //return;
            }
            System.Console.WriteLine("Team id : Name : List of Members\n");
            foreach (var item in SortedTeamByMembersWithYear)
            {
                System.Console.WriteLine($"{item.Id} : {item.Name}");
                foreach (var member in item.Members)
                {
                    System.Console.WriteLine($"\t({member.Id}) {member.FirstName} {member.LastName} : {member.Email} : Registered {member.RegisteredAt} : Birthday {member.BirthDay}");
                }
                System.Console.WriteLine();
            }
        }

        public Display(List<UserWithTasksDto> SortedUsersWithSortedTasks)
        {
            if (SortedUsersWithSortedTasks.Count == 0)
            {
                System.Console.WriteLine("No matching results.");
                //return;
            }

            System.Console.WriteLine("User id : Name : Email : List of Tasks\n");
            foreach (var item in SortedUsersWithSortedTasks)
            {
                System.Console.WriteLine($"({item.Id}) {item.FirstName} {item.LastName} : {item.Email} : Registered {item.RegisteredAt} : Birthday {item.BirthDay}");
                foreach (var task in item.Tasks)
                {
                    System.Console.WriteLine($"\t({task.Id})  {task.Name} : {task.State} : Created {task.CreatedAt} : Finished {(task.FinishedAt == null ? "no information" : task.FinishedAt)}");
                    System.Console.WriteLine($"\tDescription: {task.Description}");
                }
                System.Console.WriteLine();
            }
        }

        public Display(UserInfoDto UserInfo)
        {

            if (UserInfo is null)
            {
                System.Console.WriteLine("No matching results.");
                //return;
            }

            System.Console.WriteLine("User id : Name : Email : List of Tasks\n");
            System.Console.WriteLine($"({UserInfo.User.Id}) {UserInfo.User.FirstName} {UserInfo.User.LastName} : {UserInfo.User.Email} : Registered {UserInfo.User.RegisteredAt} : Birthday {UserInfo.User.BirthDay}");


            System.Console.WriteLine("\nLast Project id : Name");
            System.Console.WriteLine($"{UserInfo.LastProject.Id} : {UserInfo.LastProject.Name} : Created {UserInfo.LastProject.CreatedAt} : Deadline {UserInfo.LastProject.Deadline}");
            System.Console.WriteLine($"\tDescription: {UserInfo.LastProject.Description}");

            System.Console.WriteLine($"\nNumber of tasks in last project: {UserInfo.LastProjectTasksCount}.");
            System.Console.WriteLine($"\nNumber of unfinished or cancelled tasks in last project: {UserInfo.LastProjectTasksCount}.");

            System.Console.WriteLine("\nLongest task");
            System.Console.WriteLine($"({UserInfo.LongestTask.Id})  {UserInfo.LongestTask.Name} : {UserInfo.LongestTask.State} : Created {UserInfo.LongestTask.CreatedAt} : Finished {(UserInfo.LongestTask.FinishedAt == null ? "no information" : UserInfo.LongestTask.FinishedAt)}");
            System.Console.WriteLine($"\nDescription: {UserInfo.LongestTask.Description}");
        }

        public Display(List<ProjectInfoDto> ProjectsInfo)
        {

            if (ProjectsInfo.Count == 0)
            {
                System.Console.WriteLine("No matching results.");
                //return;
            }

            System.Console.WriteLine("\nProject id : Name");
            foreach (var projectInfo in ProjectsInfo)
            {
                System.Console.WriteLine($"{projectInfo.Project.Id} : {projectInfo.Project.Name} : Created {projectInfo.Project.CreatedAt} : Deadline {projectInfo.Project.Deadline}");
                System.Console.WriteLine($"\tDescription: {projectInfo.Project.Description}");

                System.Console.WriteLine("\nLongest task by description");
                System.Console.WriteLine($"({projectInfo.LongestTaskByDescription.Id})  {projectInfo.LongestTaskByDescription.Name} : {projectInfo.LongestTaskByDescription.State} : Created {projectInfo.LongestTaskByDescription.CreatedAt} : Finished {(projectInfo.LongestTaskByDescription.FinishedAt == null ? "no information" : projectInfo.LongestTaskByDescription.FinishedAt)}");
                System.Console.WriteLine($"\nDescription: {projectInfo.LongestTaskByDescription.Description}");


                System.Console.WriteLine("\nShortest task by name");
                System.Console.WriteLine($"({projectInfo.ShortestTaskByName.Id})  {projectInfo.ShortestTaskByName.Name} : {projectInfo.ShortestTaskByName.State} : Created {projectInfo.ShortestTaskByName.CreatedAt} : Finished {(projectInfo.ShortestTaskByName.FinishedAt == null ? "no information" : projectInfo.ShortestTaskByName.FinishedAt)}");
                System.Console.WriteLine($"\nDescription: {projectInfo.ShortestTaskByName.Description}");

                if (projectInfo.TeamMembersCount is not null)
                {
                    System.Console.WriteLine($"\nNumber of members in the team: {projectInfo.TeamMembersCount}.");
                }
            }
        }

        public Display(PagedList<FullProjectDto> SortedFilteredPageOfProjects)
        {

            if (SortedFilteredPageOfProjects.TotalCount == 0)
            {
                System.Console.WriteLine("No matching results.");
                //return;
            }

            System.Console.WriteLine("\nProject id : Name");
            foreach (var project in SortedFilteredPageOfProjects.Items)
            {
                System.Console.WriteLine($"{project.Id} : {project.Name} : Created {project.CreatedAt} : Deadline {project.Deadline}");
                System.Console.WriteLine($"\tDescription: {project.Description}");

                foreach (var task in project.Tasks)
                {
                    System.Console.WriteLine($"\t({task.Id})  {task.Name} : {task.State} : Created {task.CreatedAt} : Finished {(task.FinishedAt == null ? "no information" : task.FinishedAt)}");
                    System.Console.WriteLine($"\nDescription: {task.Description}");
                    System.Console.WriteLine($"\tPerformer: ({task.Perfomer.Id}) {task.Perfomer.FirstName} {task.Perfomer.LastName}");
                }


                System.Console.WriteLine("Author of the project id : Name : Email : List of Tasks\n");
                System.Console.WriteLine($"({project.Author.Id}) {project.Author.FirstName} {project.Author.LastName} : {project.Author.Email} : Registered {project.Author.RegisteredAt} : Birthday {project.Author.BirthDay}");

                System.Console.WriteLine("Team leading the project id : Name :\n");
                System.Console.WriteLine($"{project.Team.Id} : {project.Team.Name} : Created {project.Team.CreatedAt}");
            }
        }
    }
}
