using MongoDB.Driver;
using CustomerApp.Models;

namespace CustomerApp
{
    public class MongoService
    {
        private readonly IMongoCollection<Customer> _customers;

        public MongoService()
        {
            var client = new MongoClient("mongodb+srv://admin:hoxKLzguDzJ4LdeQ@cluster0.fumnr2u.mongodb.net/");
            var database = client.GetDatabase("CustomerDb");
            _customers = database.GetCollection<Customer>("Customers");
        }

        public void AddCustomer(Customer customer)
        {
            _customers.InsertOne(customer);
            Console.WriteLine("Sunucuya baglandi.");
        }

        public List<Customer> GetCustomers()
        {
            return _customers.Find(customer => true).ToList();
        }

      
        public Customer GetCustomerById(string id)
        {
            return _customers.Find(c => c.Id == id).FirstOrDefault();
        }

        public void UpdateCustomer(Customer customer)
        {
            _customers.ReplaceOne(c => c.Id == customer.Id, customer);
        }

        public void DeleteCustomer(string id)
        {
            _customers.DeleteOne(c => c.Id == id);
        }
    }
}
