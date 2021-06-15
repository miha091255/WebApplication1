using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class Test
    {
        public int id { get; set; }

        public string name { get; set; }

        public string list { get; set; }
    }
}