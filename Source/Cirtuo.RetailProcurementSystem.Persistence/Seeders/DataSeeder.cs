using Bogus;
using Cirtuo.RetailProcurementSystem.Domain;

namespace Cirtuo.RetailProcurementSystem.Persistence.Seeders;

public class DataSeeder
{
    private const int StartQuarter = 1;
    private const int EndQuarter = 4;
    private const int StartYear = 2022;
    private const int EndYear = 2025;
    public IReadOnlyCollection<Location> Locations { get; }
    public IReadOnlyCollection<Contact> Contacts { get; }
    public IReadOnlyCollection<Manager> Managers { get; }
    public IReadOnlyCollection<Retailer> Retailers { get; }
    public IReadOnlyCollection<Supplier> Suppliers { get; }
    public IReadOnlyCollection<SupplierRetailer> SuppliersRetailers { get; }
    public IReadOnlyCollection<StoreItem> StoreItems { get; }
    public IReadOnlyCollection<SupplierStoreItem> SupplierStoreItems { get; }
    public IReadOnlyCollection<Order> Orders { get; }
    public IReadOnlyCollection<OrderItem> OrderItems { get; }

    public DataSeeder()
    {
        Locations = GenerateLocations(50);
        Contacts = GenerateContacts(50);
        Managers = GenerateManagers(600, Contacts.ToList());
        Retailers = GenerateRetailers(50, Contacts.ToList(), Managers.ToList(), Locations.ToList());
        Suppliers = GenerateSuppliers(40, Contacts.ToList(), Locations.ToList());
        SuppliersRetailers = GenerateSuppliersRetailers(500, Suppliers.ToList(), Retailers.ToList());
        StoreItems = GenerateStoreItems(500);
        SupplierStoreItems = GenerateSupplierStoreItems(1000, Suppliers.ToList(), StoreItems.ToList());
        Orders = GenerateOrders(200, Retailers.ToList());
        OrderItems = GenerateOrderItems(1000, Orders.ToList(), SupplierStoreItems.ToList());
    }

    private IReadOnlyCollection<Location> GenerateLocations(int amount)
    {
        var id = 1;
        var locationFaker = new Faker<Location>()
            .RuleFor(x => x.Id, f => id++)
            .RuleFor(x => x.Address, f => f.Address.StreetAddress())
            .RuleFor(x => x.City, f => f.Address.City())
            .RuleFor(x => x.State, f => f.Address.State())
            .RuleFor(x => x.ZipCode, f => f.Address.ZipCode());

        return locationFaker.Generate(amount);
    }
    
    private IReadOnlyCollection<Contact> GenerateContacts(int amount)
    {
        Func<Faker, string> phoneRule = f =>
        {
            var phone = f.Person.Phone;
            return phone.Length > 20 ? phone.Substring(0, 20) : phone;
        };
        
        var id = 1;
        var contactFaker = new Faker<Contact>()
            .RuleFor(x => x.Id, f => id++)
            .RuleFor(x => x.Email, f => f.Person.Email)
            .RuleFor(x => x.Phone, f => phoneRule(f));

        return contactFaker.Generate(amount);
    }
    
    private IReadOnlyCollection<Manager> GenerateManagers(int amount, List<Contact> contacts)
    {
        var id = 1;
        var managerFaker = new Faker<Manager>()
            .RuleFor(x => x.Id, f => id++)
            .RuleFor(x => x.Name, f => $"{f.Person.FirstName} {f.Person.LastName}")
            .RuleFor(x => x.ContactId, f => f.Random.ListItem(contacts).Id);

        return managerFaker.Generate(amount);
    }
    
    private IReadOnlyCollection<Retailer> GenerateRetailers(
        int amount,
        List<Contact> contacts,
        List<Manager> managers,
        List<Location> locations
    )
    {
        var id = 1;
        var retailerFaker = new Faker<Retailer>()
            .RuleFor(x => x.Id, f => id++)
            .RuleFor(x => x.Name, f => f.Company.CompanyName())
            .RuleFor(x => x.LocationId, f => f.Random.ListItem(locations).Id)
            .RuleFor(x => x.ContactId, f => f.Random.ListItem(contacts).Id)
            .RuleFor(x => x.ManagerId, f => f.Random.ListItem(managers).Id);

        return retailerFaker.Generate(amount);
    }
    
    private IReadOnlyCollection<Supplier> GenerateSuppliers(
        int amount,
        List<Contact> contacts,
        List<Location> locations
    )
    {
        var id = 1;
        var supplierFaker = new Faker<Supplier>()
            .RuleFor(x => x.Id, f => id++)
            .RuleFor(x => x.Name, f => f.Company.CompanyName())
            .RuleFor(x => x.LocationId, f => f.Random.ListItem(locations).Id)
            .RuleFor(x => x.ContactId, f => f.Random.ListItem(contacts).Id);

        return supplierFaker.Generate(amount);
    }
    
    private IReadOnlyCollection<SupplierRetailer> GenerateSuppliersRetailers(
        int amount,
        List<Supplier> suppliers,
        List<Retailer> retailers
    )
    {
        Func<DateTime> startDateRule = () =>
        {
            var quarter = new Faker().Random.Int(StartQuarter, EndQuarter);
            var year = new Faker().Random.Int(StartYear, EndYear);
            
            var startMonth = quarter switch
            {
                1 => 1,
                2 => 4,
                3 => 7,
                4 => 10,
                _ => throw new ArgumentOutOfRangeException(nameof(quarter), quarter, null)
            };
            return new DateTime(year, startMonth, 1).ToUniversalTime();
        };
        
        Func<DateTime, DateTime> endDateRule = startDate => startDate.AddMonths(3);
        
        var id = 1;
        var supplierRetailerFaker = new Faker<SupplierRetailer>()
            .RuleFor(x => x.Id, f => id++)
            .RuleFor(x => x.SupplierId, f => f.Random.ListItem(suppliers).Id)
            .RuleFor(x => x.RetailerId, f => f.Random.ListItem(retailers).Id)
            .RuleFor(x => x.StartDate, _ => startDateRule())
            .RuleFor(x => x.EndDate, (_, x) => endDateRule(x.StartDate));

        return supplierRetailerFaker.Generate(amount);
    }
    
    private IReadOnlyCollection<StoreItem> GenerateStoreItems(int amount)
    {
        var id = 1;
        var storeItemFaker = new Faker<StoreItem>()
            .RuleFor(x => x.Id, f => id++)
            .RuleFor(x => x.Name, f => f.Commerce.ProductName())
            .RuleFor(x => x.Description, f => f.Commerce.ProductDescription())
            .RuleFor(x => x.Sku, f => f.Commerce.Ean13())
            .RuleFor(x => x.Category, f => f.PickRandom<StoreItemCategory>());

        return storeItemFaker.Generate(amount);
    }
    
    private IReadOnlyCollection<SupplierStoreItem> GenerateSupplierStoreItems(
        int amount,
        List<Supplier> suppliers,
        List<StoreItem> storeItems
    )
    {
        Func<int, int, DateTime> startDateRule = (year, quarter) =>
        {
            var startMonth = quarter switch
            {
                1 => 1,
                2 => 4,
                3 => 7,
                4 => 10,
                _ => throw new ArgumentOutOfRangeException(nameof(quarter), quarter, null)
            };
            return new DateTime(year, startMonth, 1).ToUniversalTime();
        };
        
        Func<int, int, DateTime> endDateRule = (year, quarter) =>
        {
            var endMonth = quarter switch
            {
                1 => 3,
                2 => 6,
                3 => 9,
                4 => 12,
                _ => throw new ArgumentOutOfRangeException(nameof(quarter), quarter, null)
            };
            return new DateTime(year, endMonth, 1).ToUniversalTime();
        };
        
        var id = 1;
        var supplierStoreItemFaker = new Faker<SupplierStoreItem>()
            .RuleFor(x => x.Id, f => id++)
            .RuleFor(x => x.SupplierId, f => f.Random.ListItem(suppliers).Id)
            .RuleFor(x => x.StoreItemId, f => f.Random.ListItem(storeItems).Id)
            .RuleFor(x => x.ItemPrice, f => f.Random.Decimal(0.01m, 1000.00m))
            .RuleFor(x => x.SoldItems, f => f.Random.Int(0, 1000))
            .RuleFor(x => x.Quarter, f => f.Random.Int(1, 4))
            .RuleFor(x => x.Year, f => f.Random.Int(2024, 2028))
            .RuleFor(x => x.StartDate, (_, x) => startDateRule(x.Year, x.Quarter))
            .RuleFor(x => x.EndDate, (_, x) => endDateRule(x.Year, x.Quarter));

        return supplierStoreItemFaker.Generate(amount);
    }
    
    private IReadOnlyCollection<Order> GenerateOrders(int amount, List<Retailer> retailers)
    {
        var id = 1;
        var orderFaker = new Faker<Order>()
            .RuleFor(x => x.Id, f => id++)
            .RuleFor(x => x.RetailerId, f => f.Random.ListItem(retailers).Id)
            .RuleFor(x => x.OrderDate, f => _dateRule(f, 1))
            .RuleFor(x => x.DeliveryDate, f => _optionalDateRule(f, 10))
            .RuleFor(x => x.PaymentDate, f => _optionalDateRule(f, 5));

        return orderFaker.Generate(amount);
    }
    
    private IReadOnlyCollection<OrderItem> GenerateOrderItems(
        int amount,
        List<Order> orders,
        List<SupplierStoreItem> supplierStoreItems
    )
    {
        var id = 1;
        var orderItemFaker = new Faker<OrderItem>()
            .RuleFor(x => x.Id, f => id++)
            .RuleFor(x => x.OrderId, f => f.Random.ListItem(orders).Id)
            .RuleFor(x => x.SupplierStoreItemId, f => f.Random.ListItem(supplierStoreItems).Id)
            .RuleFor(x => x.ItemPrice, (_, x) => supplierStoreItems.First(s => s.Id == x.SupplierStoreItemId).ItemPrice)
            .RuleFor(x => x.Quantity, f => f.Random.Int(1, 300));

        var orderItems = orderItemFaker.Generate(amount); 
        orderItems.ForEach(x =>
        {
            var order = orders.First(o => o.Id == x.OrderId);
            order.SetTotalPrice(x.ItemPrice * x.Quantity);
        });
        
        return orderItems;
    }

    private Func<Faker, int, DateTime?> _optionalDateRule = (f, hours) =>
        f.Date.FutureOffset(refDate: new DateTimeOffset(2024, 1, 1, 12, 12, 0, TimeSpan.FromHours(hours))).DateTime
            .ToUniversalTime().OrNull(f);
    
    private Func<Faker, int, DateTime> _dateRule = (f, hours) =>
        f.Date.FutureOffset(refDate: new DateTimeOffset(2024, 1, 1, 12, 12, 0, TimeSpan.FromHours(hours))).DateTime
            .ToUniversalTime();
    
    private static T SeedRow<T>(Faker<T> faker, int seed) where T : class => faker.UseSeed(seed).Generate();
}