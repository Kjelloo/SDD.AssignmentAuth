﻿namespace SSD.Assignment.Authorization.WebAPI.Model;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public Roles Role { get; set; }
}