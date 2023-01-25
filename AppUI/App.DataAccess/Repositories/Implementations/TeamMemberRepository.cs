using App.Core.Entities;
using App.DataAccess.Contexts;
using App.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DataAccess.Repositories.Implementations
{
    public class TeamMemberRepository : Repository<TeamMember>, ITeamMemberRepository
    {
        public TeamMemberRepository(AppDbContex context) : base(context)
        {
        }
    }
}
