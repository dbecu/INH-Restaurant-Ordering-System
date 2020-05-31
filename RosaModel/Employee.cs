﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace RosaModel
{
    public class Employee
    {
        public int employeeId { get; set; }
        public Roles role { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string username { get; set; } //used for login
        public string password { get; set; } //used for login

        public string[] personalNotes { get; set; }
      
    }
}
