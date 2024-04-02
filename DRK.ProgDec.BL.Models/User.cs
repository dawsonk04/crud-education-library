﻿namespace DRK.ProgDec.BL.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }

        public string FullName { get { return FirstName + " " + LastName; } }
    }
}
