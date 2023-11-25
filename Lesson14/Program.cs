Console.Write("Введите n:");
int n = int.Parse(Console.ReadLine()!);
double x = 0;
int y = 0;
Task allTask = null;
try
{
    Task t1 = Task.Run(() => Sum1(n));
    Task t2 = Task.Run(() => Sum2(n));
    allTask = Task.WhenAll(t1, t2);
    await allTask;
}
catch (Exception ex)
{
    Console.WriteLine($"Причина ошибки:{ex.Message}");
    Console.WriteLine($"x={x} y={y}");
}
async void Sum1(int n)
{
    Random random = new Random();
    double s = 0;
    for (int i = 0; i < n; i++)
    {
        x = random.NextDouble() * 4 * Math.PI - 2 * Math.PI;
        s += 3 / Math.Sqrt(Math.Cos(x));
    }
    Console.WriteLine($"S1={s:F2}");
}
async void Sum2(int n)
{
    Random random = new Random();
    double s = 0;
    for (int i = 0; i < n; i++)
    {
        y = random.Next(-20, 20);
        double Fact = 1;
        for (int j = 1; j <= y; j++) Fact *= j;
        s += Math.Pow(7, y) / (Fact - Math.Pow(9, y));
    }
    Console.WriteLine($"S2={s:F2}");
}

