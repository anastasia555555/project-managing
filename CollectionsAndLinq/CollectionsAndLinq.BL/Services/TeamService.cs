using AutoMapper;
using CollectionsAndLinq.BL.Interfaces;
using CollectionsAndLinq.BL.Models.Teams;
using CollectionsAndLinq.BL.Services.Abstract;
using CollectionsAndLinq.DAL.Context;
using CollectionsAndLinq.DAL.Entities.Team;
using Microsoft.EntityFrameworkCore;

namespace CollectionsAndLinq.BL.Services
{
    public class TeamService : BaseService, ITeamService
    {
        public TeamService(ThreadContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<List<TeamDto>> GetTeamsAsync()
        {
            return await _context.Teams.Select(x => _mapper.Map<TeamDto>(x)).ToListAsync();
        }

        public async Task<TeamDto> CreateTeam(NewTeamDto team)
        {
            var teamEntity = _mapper.Map<Team>(team);

            _context.Teams.Add(teamEntity);
            await _context.SaveChangesAsync();

            var createdTeam = await _context.Teams
                .FirstAsync(x => x.Id == team.Id);

            return _mapper.Map<TeamDto>(createdTeam);
        }

        public async Task<TeamDto> UpdateTeam(NewTeamDto team)
        {
            var teamEntity = _mapper.Map<Team>(team);
            var removeEntity = await _context.Teams.FirstAsync(x => x.Id == team.Id);

            _context.Teams.Remove(removeEntity);
            _context.Teams.Add(teamEntity);
            await _context.SaveChangesAsync();

            var updatedEntity = await _context.Teams.FirstAsync(x => x.Id == team.Id);

            return _mapper.Map<TeamDto>(updatedEntity);
        }

        public async Task<TeamDto> DeleteTeam(int id)
        {
            var teamEntity = await _context.Teams.FirstAsync(x => x.Id == id);

            _context.Teams.Remove(teamEntity);
            await _context.SaveChangesAsync();

            return _mapper.Map<TeamDto>(teamEntity);
        }

        public async Task<List<TeamWithMembersDto>> GetSortedTeamByMembersWithYearAsync(int year)
        {
            var teams = await _context.Teams.ToListAsync();
            var users = await _context.Users.ToListAsync();

            var sortedTeams = teams
                .GroupJoin(users,
                team => team.Id,
                user => user.TeamId,
                (team, usersInTeam) => new TeamWithMembers(
                    team.Id,
                    team.Name,
                    usersInTeam
                    .OrderByDescending(x => x.RegisteredAt)
                    .ToList()))
                .Where(team => team.Members.All(user => user.BirthDay.Year < year))
                .Where(team => team.Members.Count > 0)
                .OrderBy(team => team.Name)
                .ToList();

            return _mapper.Map<List<TeamWithMembersDto>>(sortedTeams);
        }
    }
}
