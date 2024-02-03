using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Models.VMs.AppUserVMs
{
    public class ResetPasswordVM
    {
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
