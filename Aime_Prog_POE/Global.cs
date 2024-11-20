using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aime_Prog_POE
{
    public static class Global
    {

        public static string UserName { get; set; } = string.Empty; // Store the logged-in username
        public static string Role { get; set; } = string.Empty; // Store the role of the logged-in user (e.g., Lecturer, HR)
    }
}
