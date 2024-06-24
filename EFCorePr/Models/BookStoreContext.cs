﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EFCorePr.Models;

public partial class BookStoreContext : DbContext
{
    public BookStoreContext(DbContextOptions<BookStoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Author { get; set; }

    public virtual DbSet<AuthorBook> AuthorBook { get; set; }

    public virtual DbSet<Book> Book { get; set; }

    public virtual DbSet<Cart> Cart { get; set; }

    public virtual DbSet<CartBook> CartBook { get; set; }

    public virtual DbSet<Customer> Customer { get; set; }

    public virtual DbSet<Publisher> Publisher { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Fname)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("FName");
            entity.Property(e => e.Lname)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("LName");
        });

        modelBuilder.Entity<AuthorBook>(entity =>
        {
            entity.ToTable("Author_Book");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AuthorId).HasColumnName("AuthorID");
            entity.Property(e => e.BookId).HasColumnName("BookID");

            entity.HasOne(d => d.Author).WithMany(p => p.AuthorBook)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK_Author_Book_Author");

            entity.HasOne(d => d.Book).WithMany(p => p.AuthorBook)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK_Author_Book_Book");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasIndex(e => e.Isbn, "IX_Book_Isbn_Unique")
                .IsUnique()
                .HasFilter("([IsDeleted]=(0))");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.Isbn)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PublisherId).HasColumnName("PublisherID");
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasOne(d => d.Publisher).WithMany(p => p.Book)
                .HasForeignKey(d => d.PublisherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Book_Publisher");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_Cart_UserID_Unique").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.TotalItem).HasColumnName("TotalITem");
            entity.Property(e => e.UserId).HasColumnName("UserID");
        });

        modelBuilder.Entity<CartBook>(entity =>
        {
            entity.ToTable("Cart_Book");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BookId).HasColumnName("BookID");
            entity.Property(e => e.CartId).HasColumnName("CartID");

            entity.HasOne(d => d.Book).WithMany(p => p.CartBook)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK_Cart_Book_Book");

            entity.HasOne(d => d.Cart).WithMany(p => p.CartBook)
                .HasForeignKey(d => d.CartId)
                .HasConstraintName("FK_Cart_Book_Cart");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_User");

            entity.HasIndex(e => e.CartId, "IX_Customer_CartID_Unique").IsUnique();

            entity.HasIndex(e => e.PhoneNum, "IX_Customer_PhoneNum_Unique")
                .IsUnique()
                .HasFilter("([IsDeleted]=(0))");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.CartId).HasColumnName("CartID");
            entity.Property(e => e.Fname)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("FName");
            entity.Property(e => e.Lname)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("LName");
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNum)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Cart).WithOne(p => p.Customer)
                .HasForeignKey<Customer>(d => d.CartId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_User_Cart");
        });

        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.HasIndex(e => e.FullName, "IX_Publisher").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FullName)
                .IsRequired()
                .HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}