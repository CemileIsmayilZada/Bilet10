using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBusiness.ViewModels.TeamMemberrs
{
    public class UpdateTeamMemberVm
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Position { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImagePath { get; set; }
        public string? Facebook { get; set; }
        public string? Instagram { get; set; }
        public string? Twitter { get; set; }
    }
}
