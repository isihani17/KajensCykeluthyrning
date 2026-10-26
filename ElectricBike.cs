namespace KajensCykeluthyrning;

// Elcykel ärver också från Vehicle men har ett batteri på köpet.
public class ElectricBike : Vehicle
{
    public int BatteryLevel { get; set; } // procent, 0 till 100

    // Konstruktor som tar fordonets nummer och batterinivå
    public ElectricBike(int number, int batteryLevel) : base(number, 70m)
    {
        BatteryLevel = batteryLevel;
    }

    // Returnerar fordonstypen som en sträng
    public override string GetTypeName()
    {
        return "Elcykel";
    }

    // Elcykel kräver att man är minst 18
    public override int GetMinimumAge()
    {
        return 18;
    }

    // Under 30 % får cykeln inte hyras ut
    public override bool HasEnoughBattery()
    {
        return BatteryLevel >= 30;
    }

    // Returnerar batterinivån som en sträng
    public override string GetBatteryText()
    {
        return BatteryLevel + " %";
    }

    // Tar bort 10 procentenheter per timme, men aldrig under 0.
    public override void UseBattery(int hours)
    {
        BatteryLevel = BatteryLevel - hours * 10;
        if (BatteryLevel < 0)
        {
            BatteryLevel = 0;
        }
    }

    // Laddar upp batteriet till 100 %
    public override bool Charge()
    {
        BatteryLevel = 100;
        return true;
    }
}