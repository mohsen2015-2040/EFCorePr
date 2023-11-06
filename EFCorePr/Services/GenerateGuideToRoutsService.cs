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
                    string getBooks = "~/GetBooks";
                    string addBook = "~/add";
                    string editBooks = "~/Edit";
                    string deleteBooks = "~/Delete";

                    return $"All Books: {getBooks}\nAdd New: {addBook}\nEdit Book: {editBooks}\nDelete Book: {deleteBooks}";

                case Type t when t == typeof(Customer):
                    string getCustomers = "~/GetCustomers";
                    string addCustomer = "~/add";
                    string editCustomer = "~/Edit";
                    string deleteCustomer = "~/Delete";

                    return $"All Users: {getCustomers}\nAdd New: {addCustomer}\nEdit User: {editCustomer}\nDelete User: {deleteCustomer}";

                case Type t when t == typeof(Publisher):
                    string getPublishers = "~/GetPublishers";
                    string addPublishers = "~/Add";
                    string editPublisher = "~/Edit";
                    string deletePublisher = "~/Delete";

                    return $"All Publishers: {getPublishers}\nAdd New: {addPublishers}\nEdit Publisher: {editPublisher}\nDelete Publisher: {deletePublisher}";

                case Type t when t == typeof(Rent):
                    string getRents = "~/GetRents";
                    string addRent = "~/Add";
                    string editRent = "~/Edit";

                    return $"All Rents: {getRents}\nAdd New: {addRent}\nEdit Rent: {editRent}";

                
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
