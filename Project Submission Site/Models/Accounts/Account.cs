﻿namespace Project_Submission_Site.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Email { get; set; } 
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Verified { get; set; }
        public string VerifyCode { get; set; }
    }
}
