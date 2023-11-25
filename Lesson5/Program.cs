//Упражнение 1
//Console.Write("Введите n:");
//int n=int.Parse(Console.ReadLine()!);
//Console.Write("Введите x:");
//double x = double.Parse(Console.ReadLine()!);
//Task<double>[] tasks = new Task<double>[3]
//{
//    new Task<double>(()=>Sum1(n)),
//    new Task<double>(()=>Sum2(n)),
//    new Task<double>(()=>Sum3(n,x))
//};
//for (int i = 0; i < tasks.Length; i++)
//{
//    tasks[i].Start();
//    Console.WriteLine($"S{i+1}={tasks[i].Result:F2}");
//}
//double Sum1(int n)
//{
//    double s = 0;
//    for (int i = 1; i <= n; i++)
//        s += (i + 1) / Math.Pow(2, i);
//    return s;
//}
//double Sum2(int n)
//{
//    double s = 0;
//    for (int i = 1; i <= n; i++)
//        s += (i + 2) / (2*i-1);
//    return s; 
//}
//double Sum3(int n,double x)
//{
//    double s = 0;
//    for (int i = 1; i <= n; i++)
//        s += (Math.Pow(x,i)/i)/i;
//    return s;
//}
//Упражнение 2
Task<int> monthTask=new Task<int>(()=>getMonth());
Task<int> dayTask=monthTask.ContinueWith(task=>getDay(task.Result));
Task<int[]> hms = dayTask.ContinueWith(task => HMS());
Task print = hms.ContinueWith(task => Print(monthTask.Result, dayTask.Result, task.Result[0],
    task.Result[1], task.Result[2]));
monthTask.Start();
dayTask.Wait();
hms.Wait();
print.Wait();

int getMonth()
{
    Random randon=new Random();
    return randon.Next(1,12);
}
int getDay(int n)
{
    return DateTime.DaysInMonth(DateTime.Now.Year, n);
}
int[] HMS()
{
    Random randon = new Random();
    int[] mas = new int[3];
    mas[0] = randon.Next(1,24);
    mas[1] = randon.Next(1, 60);
    mas[2] = randon.Next(1, 60);
    return mas;
}
void Print(int month,int d,int h,int m,int s)
{
    Console.WriteLine($"Month:{month}, day:{d},{h}:{m}:{s}");
}