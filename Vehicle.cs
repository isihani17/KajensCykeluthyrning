namespace KajensCykeluthyrning;

// Basklassen innehåller det som både vanliga cyklar och elcyklar har gemensamt.
public class Vehicle
{
    public int Number { get; set; }
    public decimal PricePerHour { get; set; }
    public bool IsRented { get; set; }

    public Vehicle(int number, decimal pricePerHour)
    {
        Number = number;
        PricePerHour = pricePerHour;
        IsRented = false; // Nya fordon är alltid lediga.
    }

    // virtual betyder att subklasser kan skriva över metoden med override.
    public virtual string GetTypeName()
    {
        return "Fordon";
    }

    // Minsta ålder för att hyra. Elcykel skriver över denna till 18.
    public virtual int GetMinimumAge()
    {
        return 15;
    }

    // En vanlig cykel behöver inget batteri, så den kan alltid hyras ut.
    public virtual bool HasEnoughBattery()
    {
        return true;
    }

    // Text som visas i fordonslistan. Tom för cyklar, "80 %" för elcyklar.
    public virtual string GetBatteryText()
    {
        return "";
    }

    // Vanliga cyklar har inget batteri, så här händer ingenting.
    public virtual void UseBattery(int hours)
    {
    }

    // Returnerar false eftersom en vanlig cykel inte går att ladda.
    public virtual bool Charge()
    {
        return false;
    }

    // Beräknar priset före eventuell rabatt.
    public decimal CalculatePrice(int hours)
    {
        return PricePerHour * hours;
    }
}
