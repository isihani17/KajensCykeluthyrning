
using System.Text;

namespace KajensCykeluthyrning;

public class Program
{
    public static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        RentalService service = new RentalService();
        bool running = true;

        while (running)
        {
            Console.WriteLine();
            Console.WriteLine("=== Kajens Cykeluthyrning ===");
            Console.WriteLine("1. Visa fordon  2. Registrera kund  3. Hyr ut");
            Console.WriteLine("4. Lämna tillbaka  5. Ladda  6. Avsluta");
            Console.Write("Val: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ShowVehicles(service);
                    break;

                case "2":
                    RegisterCustomer(service);
                    break;

                case "3":
                    RentVehicle(service);
                    break;

                case "4":
                    ReturnVehicle(service);
                    break;

                case "5":
                    ChargeVehicle(service);
                    break;

                case "6":
                    running = false;
                    break;

                default:
                    Console.WriteLine("Ogiltigt val.");
                    break;
            }
        }
    }

    // Visar alla fordon
    static void ShowVehicles(RentalService service)
    {
        Console.WriteLine("Nr  Typ      Status  Batteri");

        foreach (Vehicle v in service.Vehicles)
        {
            string status = v.IsRented ? "uthyrd" : "ledig";

            Console.WriteLine(
                v.Number + "   " +
                v.GetTypeName().PadRight(8) + " " +
                status.PadRight(7) + " " +
                v.GetBatteryText());
        }
    }

    // Registrerar en kund
    static void RegisterCustomer(RentalService service)
    {
        Console.Write("Namn: ");
        string name = Console.ReadLine();

        int age = ReadAge();

        Console.Write("Medlem (j/n): ");
        string answer = Console.ReadLine();
        bool isMember = answer.ToLower() == "j";

        service.RegisterCustomer(name, age, isMember);

        Console.WriteLine(name + " är registrerad.");
    }

    // Kontrollerar att åldern är ett giltigt, icke-negativt heltal
    static int ReadAge()
    {
        while (true)
        {
            Console.Write("Ålder: ");

            if (int.TryParse(Console.ReadLine(), out int age) && age >= 0)
            {
                return age;
            }

            Console.WriteLine("Ange en giltig ålder.");
        }
    }

    // Hyr ut ett fordon
    static void RentVehicle(RentalService service)
    {
        Console.Write("Kund: ");
        Customer customer = service.FindCustomer(Console.ReadLine());

        if (customer == null)
        {
            Console.WriteLine("Kunden finns inte.");
            return;
        }

        Vehicle vehicle = service.FindVehicle(ReadInt("Fordonsnummer: "));

        if (vehicle == null)
        {
            Console.WriteLine("Fordonet finns inte.");
            return;
        }

        int hours = ReadInt("Antal timmar: ");

        if (hours <= 0)
        {
            Console.WriteLine("Antal timmar måste vara minst 1.");
            return;
        }

        Console.WriteLine(service.RentVehicle(customer, vehicle, hours));
    }

    // Tar emot ett uthyrt fordon
    static void ReturnVehicle(RentalService service)
    {
        int number = ReadInt("Fordonsnummer: ");
        Console.WriteLine(service.ReturnVehicle(number));
    }

    // Laddar ett fordon
    static void ChargeVehicle(RentalService service)
    {
        Vehicle vehicle = service.FindVehicle(ReadInt("Fordonsnummer: "));

        if (vehicle == null)
        {
            Console.WriteLine("Fordonet finns inte.");
            return;
        }

        Console.WriteLine(service.ChargeVehicle(vehicle));
    }

    // Läser ett heltal
    static int ReadInt(string text)
    {
        Console.Write(text);

        if (int.TryParse(Console.ReadLine(), out int result))
        {
            return result;
        }

        return -1;
    }
}
