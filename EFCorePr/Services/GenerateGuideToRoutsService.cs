using EFCorePr.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EFCorePr.Services
{
    public class GenerateGuideToRoutsService : IGenerateGuideToRoutsService
    {
        public string GenerateMessage(Type entity)
        {
            switch (entity)
            {
                case Type t when t == typeof(Books):
                    string getBooks = "~/get-all";
                    string searchBook = "~/search-book";
                    string addBook = "~/add";
                    string editBooks = "~/Edit";
                    string deleteBooks = "~/Delete";

                    return $"All Books: {getBooks}\nSearch Book: {searchBook}\nAdd New: {addBook}\nEdit Book: {editBooks}\nDelete Book: {deleteBooks}";

                case Type t when t == typeof(Customer):
                    string getCustomers = "~/get-all";
                    string searchCustomer = "~/search-user";
                    string addCustomer = "~/add";
                    string editCustomer = "~/Edit";
                    string deleteCustomer = "~/Delete";

                    return $"All Users: {getCustomers}\nSearch Customer: {searchCustomer}\nAdd New: {addCustomer}\nEdit User: {editCustomer}\nDelete User: {deleteCustomer}";

                case Type t when t == typeof(Publisher):
                    string getPublishers = "~/get-all";
                    string searchPublisher = "~/search-publisher";
                    string addPublishers = "~/Add";
                    string editPublisher = "~/Edit";
                    string deletePublisher = "~/Delete";

                    return $"All Publishers: {getPublishers}\nSearch Publisher: {searchPublisher}\nAdd New: {addPublishers}\nEdit Publisher: {editPublisher}\nDelete Publisher: {deletePublisher}";

                case Type t when t == typeof(Rent):
                    string getRents = "~/get-all";
                    string searchRent = "~/search-rent";
                    string addRent = "~/Add";
                    string editRent = "~/Edit";

                    return $"All Rents: {getRents}\nSearch Rent: {searchRent}\nAdd New: {addRent}\nEdit Rent: {editRent}";

                
                default:
                    string bookUrl = "MyLibrary/Books";
                    string publisherUrl = "MyLibrary/Publisher";
                    string customerUrl = "MyLibrary/Customer";
                    string rentUrl = "MyLibrary/Rent";

                    return $"Books: {bookUrl}\nPublishers: {publisherUrl}\nUsers: {customerUrl}\nRents: {rentUrl}";
            }
        }
    }
}
