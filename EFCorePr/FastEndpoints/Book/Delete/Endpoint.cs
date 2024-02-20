using EFCorePr.Models;
using FastEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCorePr.FastEndpoints.Book.Delete
{
    internal sealed class Endpoint : Endpoint<DeleteBookRequest, DeleteBookResponse>
    {
        private readonly BookStoreEFCoreContext _context;

        public Endpoint(BookStoreEFCoreContext context) 
        { 
            _context = context;
        }

        public override void Configure()
        {
            Delete("MyLibrary/books/delete/{Id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(DeleteBookRequest r, CancellationToken c)
        {
            var bookToDelete = await _context.Book.FirstOrDefaultAsync(b => b.Id == r.Id && !b.IsDeleted);

            if(bookToDelete == null) { await SendAsync(new DeleteBookResponse { Message = "Book Not Found!" }); return; }

            bookToDelete.IsDeleted = true;
            await _context.SaveChangesAsync();

            await SendAsync(new DeleteBookResponse { Message = "Successfully Removed."});
        }
    }
}