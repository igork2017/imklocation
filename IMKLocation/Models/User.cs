﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMKLocation.Models
{
    public class User:IUser
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}