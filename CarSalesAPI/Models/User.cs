using System;
using System.Collections.Generic;

namespace CarSalesAPI.Models;

public partial class User
{
    public string Username { get; set; } = null!;

    public string HashedPsw { get; set; } = null!;

    public string Permission { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly BirthDate { get; set; }

    public string Gender { get; set; } = null!;

    public DateOnly JoinDate { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string Email { get; set; } = null!;
}
