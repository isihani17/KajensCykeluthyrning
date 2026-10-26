namespace KajensCykeluthyrning;

public class Customer
{
    // Egenskaper för kundens namn, ålder och medlemskap
    public string Name { get; set; }
    public int Age { get; set; }
    public bool IsMember { get; set; } // medlemmar får 10 % rabatt

    // Konstruktor för att skapa en kund med namn, ålder och medlemskap
    public Customer(string name, int age, bool isMember)
    {
        Name = name;
        Age = age;
        IsMember = isMember;
    }
}