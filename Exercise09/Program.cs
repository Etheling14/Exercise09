decimal CC, PC, PA, VOL, VR_V, VR_P, CA, CE, PDP;
string TP, TC, MA;
Console.BackgroundColor = ConsoleColor.DarkCyan;
Console.ForegroundColor = ConsoleColor.White;
Console.Clear();

do
{
    RequestData(out CC, out TP, out TC, out PC, out PA, out VOL, out MA);
    CA = CalculateCA(TP, CC, TC, PC, PA, VOL);
    PDP = CalculatePDP(PA);
    CE = CalculateCE(TP, TC, MA, CA);
    VR_P = (CC + CA + CE) * PDP;
    if (TP == "p") VR_V = VR_P * 1.4M;
    else VR_V = VR_P * 1.2M;
    ShowResults(CA, PDP, CE, VR_P, VR_V);

}while(true);

static void RequestData(out decimal CC, out string TP, out string TC, out decimal PC, out decimal PA, out decimal VOL, out string MA)
{
    Console.ForegroundColor = ConsoleColor.Magenta; Console.Clear();
    Console.WriteLine($" ...Imput Data...");
    Console.ForegroundColor = ConsoleColor.White;
    Console.Write("Purchase Cost............................................:");
    CC = Convert.ToDecimal(Console.ReadLine()); 
    Console.Write("Kind of product: [P]erishable, [N]onperishable...........:");
    TP = Console.ReadLine();
    TP = TP.ToUpper();
    Console.Write("Type of conservation: [C]old, [A]tmosphere...............:");
    TC = Console.ReadLine();
    TC = TC.ToUpper();
    Console.Write("Conservation period (Days)...............................:");
    PC = Convert.ToDecimal(Console.ReadLine());
    Console.Write("Storage period(Days).....................................:");
    PA = Convert.ToDecimal(Console.ReadLine());
    Console.Write("Volume (litres)..........................................:");
    VOL = Convert.ToDecimal(Console.ReadLine());
    Console.Write("Storage Medium: [R]efrigerators, [F]rezzer, [S]helving, [G]uacal:");
    MA = Console.ReadLine();
    MA = MA.ToUpper();
}

static decimal CalculateCA(string TP, decimal CC, string TC, decimal PC, decimal PA, decimal VOL)
{
if ( TP == "p") // Perishable
    {
        if (TC == "C" && PC < 10) return CC * 0.05M;
        if (TC == "C" && PC >= 10) return CC * 0.10M;
        if (TC == "A" && PA < 20) return CC * 0.03M;
        if (TC == "A" && PA > 20) return CC * 0.10M;
        return CC * 0.05M;
    }
    else        // Nonperishable
    {
        if (VOL >= 50) return CC * 0.10M;
        else return CC * 0.20M;
    }
}

static decimal CalculatePDP(decimal PA)
{
    if (PA < 30) return 0.95M;
    else return 0.85M; 
}

static decimal CalculateCE(string TP, string TC, string MA, decimal CA)
{
    if (TP == "p")
    {
        if (TC == "C" && MA == "R") return CA * 2;
        else return CA;
    }
    else
    {
        if (MA == "C") return CA * 0.05M;
        else return CA * 0.07M;
    }
}

static void ShowResults(decimal CA, decimal PDP, decimal CE, decimal VR_P, decimal VR_V)
{
    Console.ForegroundColor = ConsoleColor.Magenta; 
    Console.WriteLine($"...Calculations...");
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("Product depreciation percentage: {0:P}", CA);
    Console.WriteLine("Cost of Storage................: {0,14:N2}", CA);
    Console.WriteLine("Cost of exhibition.............: {0,14:N2}", CE);
    Console.WriteLine("Value product..................: {0,14:N2}", VR_P);
    Console.WriteLine("Sale value.....................: {0,14:N2}", VR_V);
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"========================================");
    Console.ForegroundColor = ConsoleColor.DarkMagenta; 
    Console.WriteLine("Press enter to calculate another sale value or Ctrl + C to finish.");
    Console.ReadKey();  
}