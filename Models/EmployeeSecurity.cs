using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using thirtProject;

namespace thirtProject.Models
{
    public class EmployeeSecurity
    {
        public static bool login(string username, string password)
        {
            using (EmployeeDBEntities entites = new EmployeeDBEntities())
            {
                return entites.Users.Any(user => user.Username.Equals(username, StringComparison.OrdinalIgnoreCase) 
                && user.Password == password);
            }
        }
    }
}