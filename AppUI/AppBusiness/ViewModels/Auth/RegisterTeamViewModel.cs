using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBusiness.ViewModels.Auth
{
    public class RegisterTeamViewModel
    {
        public int Id { get; set; }
        public string? Fullname { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }

        public string? Password { get; set; }
        public string? ConfirmedPassword { get; set; }



    }
}
