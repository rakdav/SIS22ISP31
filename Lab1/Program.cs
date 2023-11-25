//Дана функция f и массив действительных чисел. Написать программу, находящую среднее значение функции от элементов массива.
//Вычисления распараллелить на несколько потоков. Сравнить время работы программы при параллельных и последовательных вычислениях

using System.Diagnostics;
Console.Write("Введите размер массива:");
int n=int.Parse(Console.ReadLine()!);
Thread.CurrentThread.Name = "main";
Avg(n);
Thread mythread = new Thread(Avg);
mythread.Name = "back";
mythread.Start(n);
void Avg(object? size)
{
    Console.WriteLine();
    Console.WriteLine(Thread.CurrentThread.Name + " начал работу...");
    Random random=new Random();
    int[] mas = new int[(int)size!];
    for (int i = 0; i < mas.Length; i++)
        mas[i] = random.Next(100, 999);
    Stopwatch stpWatch = new Stopwatch();
    stpWatch.Start();
    double s = 0;
    for (int i = 0; i < mas.Length; i++)
    {
        s += mas[i];
    }
    Console.WriteLine($"Avg={s/mas.Length:F2}");
    stpWatch.Stop();
    Console.WriteLine("StopWatch: " + stpWatch.ElapsedTicks.ToString());
    Console.WriteLine(Thread.CurrentThread.Name + " закончил работу...");
}
