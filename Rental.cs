namespace KajensCykeluthyrning;

// Håller reda på en pågående uthyrning så vi kan räkna ut priset när cykeln lämnas tillbaka.
public class Rental
{
    // Fordonet som hyrs. Vehicle är basklassen, så det kan vara både Bike och ElectricBike.
    public Vehicle Vehicle { get; set; }

    // Kunden som hyr fordonet. Vi behöver veta om kunden är medlem för att kunna ge rabatt.
    public Customer Customer { get; set; }

    // Antal timmar som fordonet hyrs. Används för att räkna ut pris och batteriförbrukning.
    public int Hours { get; set; }

    // Konstruktorn sätter alla tre värden när uthyrningen skapas.
    public Rental(Vehicle vehicle, Customer customer, int hours)
    {
        Vehicle = vehicle;
        Customer = customer;
        Hours = hours;
    }
}