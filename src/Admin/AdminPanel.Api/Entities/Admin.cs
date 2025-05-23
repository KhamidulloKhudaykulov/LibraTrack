﻿namespace AdminPanel.Api.Entities;

public class Admin
{
    public Guid Id { get; private set; }
    public string Login { get; private set; }
    public string Password { get; private set; }

    protected Admin(Guid id, string login, string password)
    {
        Id = id;
        Login = login;
        Password = password;
    }

    public static Admin Create(string login, string password)
    {
        if (login.Length < 5)
        {
            throw new ArgumentException("Lenght Login can't shorter than 5");
        }

        if (password.Length < 8)
        {
            throw new ArgumentException("Length password must be longer than 8");
        }

        return new Admin(Guid.NewGuid(), login, password);
    }

    public void HashPassword(string hashedPassword)
    {
        Password = hashedPassword;
    }
}
