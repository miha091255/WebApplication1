using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class UserModel
    {
        public int id { get; set; }

        public string name { get; set; }

        public string surname { get; set; }

        public string login { get; set; }

        public string pass { get; set; }
    }

    public class Admin: UserModel
    {
        //
    }
    public class Student : UserModel
    {
        //
    }
    public class Teacher : UserModel
    {
        //
    }
}