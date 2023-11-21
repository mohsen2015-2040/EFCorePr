﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace EFCorePr.Models;

public partial class Books
{
    public int Id { get; set; }

    public int PublisherId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public bool IsAvailable { get; set; }

    public bool IsDeleted { get; set; }

    public string Isbn { get; set; }

    public virtual Publisher Publisher { get; set; }

    public virtual ICollection<Rent> Rent { get; set; } = new List<Rent>();

    public Books() 
    { 
        IsAvailable = true;
        IsDeleted = false; 
    }
}