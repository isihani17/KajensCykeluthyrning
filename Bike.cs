namespace KajensCykeluthyrning;

// Cykel ärver från basklassen Vehicle och får dess egenskaper och metoder.
public class Bike : Vehicle
{
    // Skickar cykelns nummer och timpriset 40 kr vidare till basklassen Vehicle.
    public Bike(int number) : base(number, 40m)
    {
    }

    // Skriver över metoden i Vehicle så att typen visas som "Cykel".
    public override string GetTypeName()
    {
        return "Cykel";
    }
}