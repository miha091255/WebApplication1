using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class Quest
    {
        public int id { get; set; }

        public string quest { get; set; }

        public string answer1 { get; set; }

        public string answer2 { get; set; }

        public string answer3 { get; set; }

        public string answer4 { get; set; }

        public int rightNO { get; set; }
    }
}