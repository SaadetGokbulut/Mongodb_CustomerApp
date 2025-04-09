using CustomerApp;
using CustomerApp.Models;
using MongoDB.Bson; // ObjectId için

var service = new MongoService();

while (true)
{
    Console.Clear();
    Console.WriteLine("Müşteri Yönetim Sistemi");
    Console.WriteLine("1- Müşteri Ekle");
    Console.WriteLine("2- Müşterileri Listele");
    Console.WriteLine("3- Müşteri Sil");
    Console.WriteLine("4- Müşteri Seç");
    Console.WriteLine("5- Çıkış");
    Console.Write("Seçimin: ");
    var choice = Console.ReadLine();

    if (choice == "1")
{
    // Müşteri Ekleme
    Console.Write("İsim: ");
    var name = Console.ReadLine();

    string email;
    while (true)
    {
        Console.Write("Email     (Mailiniz @gmail.com ile bitmeli!): ");
        email = Console.ReadLine();

        if (email.EndsWith("@gmail.com"))
        {
            break;
        }
        else
        {
            Console.WriteLine("Geçerli bir e-posta adresi giriniz (örn: isim@gmail.com).");
        }
    }

    var customer = new Customer { Name = name, Email = email };
    service.AddCustomer(customer);
    Console.WriteLine("Müşteri başarıyla eklendi!");
}

    else if (choice == "2")
    {
        // Müşteri Listeleme
        var customers = service.GetCustomers();
        Console.WriteLine("\nMüşteri Listesi:");
        foreach (var c in customers)
        {
            Console.WriteLine($"ID: {c.Id} - İsim: {c.Name} - Email: {c.Email}");
        }
    }
    else if (choice == "3")
{
   // Müşteri Silme
Console.WriteLine("\nMevcut Müşteriler:");
var customers = service.GetCustomers();
foreach (var customer in customers)
{
    Console.WriteLine($"ID: {customer.Id}, Ad: {customer.Name}, Email: {customer.Email}");
}

Console.Write("\nSilmek istediğiniz müşteri ID: ");
var id = Console.ReadLine();

// ID geçerli bir ObjectId mi kontrol et
if (!ObjectId.TryParse(id, out ObjectId objectId))
{
    Console.WriteLine("Geçersiz ID formatı! Lütfen doğru bir ID giriniz.");
}
else
{
    var customerToDelete = service.GetCustomerById(id);
    if (customerToDelete != null)
    {
        Console.WriteLine($"\nSilinecek müşteri: {customerToDelete.Name} - {customerToDelete.Email}");
        Console.Write("Emin misiniz? (E/H): ");
        var confirm = Console.ReadLine().ToUpper();

        if (confirm == "E")
        {
            service.DeleteCustomer(id);
            Console.WriteLine("Müşteri başarıyla silindi!");
        }
        else
        {
            Console.WriteLine("Silme işlemi iptal edildi.");
        }
    }
    else
    {
        Console.WriteLine("Belirtilen ID'ye sahip müşteri bulunamadı!");
    }
}
}

    else if (choice == "4")
{
    // Müşteri Seçme
    Console.WriteLine("\nMevcut Müşteriler:");
    var customers = service.GetCustomers();
    foreach (var customer in customers)
    {
        Console.WriteLine($"ID: {customer.Id}, Ad: {customer.Name}, Email: {customer.Email}");
    }

    Console.Write("\nSeçmek istediğiniz müşteri ID: ");
    var id = Console.ReadLine();

    if (!ObjectId.TryParse(id, out ObjectId objectId))
    {
        Console.WriteLine("Geçersiz ID formatı! Lütfen geçerli bir ID giriniz (örnek: 662f0fc86edec1c9564e0c2a).");
    }
    else
    {
        var selectedCustomer = service.GetCustomerById(id);
        if (selectedCustomer != null)
        {
            Console.WriteLine("\nSeçilen Müşteri Bilgileri:");
            Console.WriteLine($"ID: {selectedCustomer.Id}");
            Console.WriteLine($"İsim: {selectedCustomer.Name}");
            Console.WriteLine($"Email: {selectedCustomer.Email}");

            // Seçilen müşteri üzerinde işlem yapma
            Console.WriteLine("\nYapılacak İşlem:");
            Console.WriteLine("1- Bilgileri Güncelle");
            Console.WriteLine("2- Ana Menüye Dön");
            Console.Write("Seçimin: ");
            var subChoice = Console.ReadLine();

            if (subChoice == "1")
            {
                Console.Write("Yeni İsim: ");
                selectedCustomer.Name = Console.ReadLine();

                string newEmail;
                while (true)
                {
                    Console.Write("Yeni Email (@gmail.com ile bitmeli): ");
                    newEmail = Console.ReadLine();
                    if (newEmail.EndsWith("@gmail.com"))
                        break;
                    else
                        Console.WriteLine("Geçerli bir e-posta adresi giriniz (örnek: isim@gmail.com).");
                }
                selectedCustomer.Email = newEmail;

                service.UpdateCustomer(selectedCustomer);
                Console.WriteLine("Müşteri bilgileri güncellendi!");
            }
        }
        else
        {
            Console.WriteLine("Belirtilen ID'ye sahip müşteri bulunamadı!");
        }
    }
}

    else if (choice == "5")
    {
        break;
    }
    else
    {
        Console.WriteLine("Geçersiz seçim! Lütfen 1-5 arasında bir numara girin.");
    }

    Console.WriteLine("\nDevam etmek için bir tuşa basın...");
    Console.ReadKey();
}