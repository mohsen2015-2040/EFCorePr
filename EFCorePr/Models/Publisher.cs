﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace EFCorePr.Models;

public partial class Publisher
{
    public int Id { get; set; }

    public string FullName { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<Book> Book { get; set; } = new List<Book>();
}