﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace EFCorePr.Models;

public partial class Customer
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string PhoneNum { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<Rent> Rent { get; set; } = new List<Rent>();

    public Customer() { IsDeleted = false; }
}