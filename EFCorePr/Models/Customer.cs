﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace EFCorePr.Models;

public partial class Customer
{
    public int Id { get; set; }

    public string Fname { get; set; }

    public string Lname { get; set; }

    public string Address { get; set; }

    public string PhoneNum { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

    public bool IsDeleted { get; set; } = false;

    public int? CartId { get; set; }

    public virtual Cart Cart { get; set; }
}