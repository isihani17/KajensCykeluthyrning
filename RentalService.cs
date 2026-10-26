namespace KajensCykeluthyrning;

// Här ligger alla regler. Program.cs sköter bara menyn.
public class RentalService
{
    // Listor för fordon och kunder, och en lista för uthyrningar som pågår just nu
    
    public List<Vehicle> Vehicles = new List<Vehicle>();
    public List<Customer> Customers = new List<Customer>();
    private List<Rental> activeRentals = new List<Rental>();

    public RentalService()
    {
        // Lägger in två cyklar och två elcyklar när programmet startar
        
        Vehicles.Add(new Bike(1));
        Vehicles.Add(new Bike(2));
        Vehicles.Add(new ElectricBike(3, 80));
        Vehicles.Add(new ElectricBike(4, 25));
    }

    // Skapar en ny kund och lägger till den i kundlistan
    
    public void RegisterCustomer(string name, int age, bool isMember)
    {
        Customers.Add(new Customer(name, age, isMember));
    }



    // Går igenom kundlistan och jämför namnen. ToLower gör att det inte spelar
    // någon roll om man skriver stora eller små bokstäver. Returnerar null om kunden inte finns.
    
    public Customer FindCustomer(string name)
    {
        foreach (Customer c in Customers)
        {
            if (c.Name.ToLower() == name.ToLower())
            {
                return c;
            }
        }
        return null;
    }


    // Samma sak fast med fordonsnummer. Returnerar null om fordonet inte finns.

    public Vehicle FindVehicle(int number)
    {
        foreach (Vehicle v in Vehicles)
        {
            if (v.Number == number)
            {
                return v;
            }
        }
        return null;
    }

    // Kollar reglerna en i taget. Om något stoppar uthyrningen returneras ett felmeddelande direkt.
    public string RentVehicle(Customer customer, Vehicle vehicle, int hours)
    {
        // Regel 1: fordonet får inte redan vara uthyrt
        if (vehicle.IsRented)
        {
            return vehicle.GetTypeName() + " " + vehicle.Number + " är redan uthyrd.";
        }

        // Regel 2: kunden måste vara tillräckligt gammal
        if (customer.Age < vehicle.GetMinimumAge())
        {
            return customer.Name + " måste vara minst " + vehicle.GetMinimumAge() + " år för att hyra " + vehicle.GetTypeName() + ".";
        }

        // Regel 3: elcyklar måste ha tillräckligt med batteri
        if (!vehicle.HasEnoughBattery())
        {
            return vehicle.GetTypeName() + " " + vehicle.Number + " har för lite batteri (" + vehicle.GetBatteryText() + "). Ladda den först.";
        }

        // Om vi kommer hit är alla regler okej, så fordonet blir uthyrt
        vehicle.IsRented = true;
        activeRentals.Add(new Rental(vehicle, customer, hours));
        return customer.Name + " hyr " + vehicle.GetTypeName() + " " + vehicle.Number + " i " + hours + " timmar.";
    }

    // När kunden lämnar tillbaka fordonet räknas priset ut, rabatten dras av och ett kvitto returneras.
    public string ReturnVehicle(int vehicleNumber)
    {
        // Leta upp uthyrningen som hör till fordonet
        Rental rental = null;
        foreach (Rental r in activeRentals)
        {
            if (r.Vehicle.Number == vehicleNumber)
            {
                rental = r;
            }
        }

        // Hittades ingen uthyrning är fordonet inte uthyrt
        if (rental == null)
        {
            return "Fordon " + vehicleNumber + " är inte uthyrt.";
        }

        // Räkna ut priset, rabatten och summan att betala
        decimal price = rental.Vehicle.CalculatePrice(rental.Hours);
        decimal discount = 0;
        if (rental.Customer.IsMember)
        {
            discount = price * 0.10m;
        }
        decimal total = price - discount;

        // Elcykeln förbrukar batteri, vanlig cykel påverkas inte
        rental.Vehicle.UseBattery(rental.Hours);

        // Fordonet blir ledigt igen
        rental.Vehicle.IsRented = false;
        activeRentals.Remove(rental);

        // Bygger kvittot rad för rad
        string receipt = "KVITTO" + Environment.NewLine;
        receipt += rental.Vehicle.GetTypeName() + ", " + rental.Hours + " timmar à " + rental.Vehicle.PricePerHour + " kr   " + price.ToString("F2") + " kr" + Environment.NewLine;
        if (rental.Customer.IsMember)
        {
            receipt += "Medlemsrabatt (10 %)   -" + discount.ToString("F2") + " kr" + Environment.NewLine;
        }
        receipt += "Att betala   " + total.ToString("F2") + " kr";
        return receipt;
    }


    public string ChargeVehicle(Vehicle vehicle)
    {
        // Om fordonet är uthyrt kan det inte laddas
        if (vehicle.IsRented)
        {
            return "Fordonet är uthyrt och kan inte laddas nu.";
        }

        // Charge() returnerar true om fordonet har ett batteri och laddas, annars false
        bool worked = vehicle.Charge();
        if (worked)
        {
            return vehicle.GetTypeName() + " " + vehicle.Number + " är laddad till " + vehicle.GetBatteryText() + ".";
        }
        return vehicle.GetTypeName() + " " + vehicle.Number + " har inget batteri.";
    }
}