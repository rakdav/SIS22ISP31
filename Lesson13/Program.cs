//Упражнение 4
//Console.Write("Введите n:");
//int n=int.Parse(Console.ReadLine()!);
//await Task.Run(()=>GenNumber());
//SumSequence(n);
//async void GenNumber()
//{
//    Random random=new Random();
//    int first = 0, second = 0,count=0;
//    do
//    {
//        first=random.Next(100,1000000);
//        second=random.Next(100, 1000000);
//        count++;
//    }
//    while (first + second != 54982);
//    await Console.Out.WriteLineAsync($"first={first} second={second} " +
//        $"count={count}");
//}
//void SumSequence(int n)
//{
//    double sum = 0;
//    for (double i = 1;  i <= n;  i++)
//    {
//        sum += Math.Pow(1 + (1 / i), i);
//    }
//    Console.WriteLine($"Сумма равна: {sum:F2}");
//}

//упражнение 5
Console.Write("Введите a:");
int a=int.Parse(Console.ReadLine()!);
Console.Write("Введите b:");
int b = int.Parse(Console.ReadLine()!);
VoidAsync(a, b);
await TaskAsync(a, b);
Console.WriteLine($"sum={await TaskTypeAsync(a,b)}");
async Task<int> TaskTypeAsync(int a, int b)
{
    return await Task.Run(() => ReturnSum(a, b));
}
async Task TaskAsync(int a,int b)
{
    await Task.Run(()=>VoidSum(a,b));
}
async void VoidAsync(int a,int b)
{
    await Task.Run(()=>VoidSum(a,b));
}
void VoidSum(int a,int b)
{
    int s = 0;
    for (int i = a;i<=b;i++)
    {
        int k = 0;
        for(int j = 2;j<i;j++) 
        {
            if(i%j==0)
            {
                k++;
                break;
            }
        }
        if (k == 0) s += i;
    }
    Console.WriteLine($"Sum={s}");
}
int ReturnSum(int a, int b)
{
    int s = 0;
    for (int i = a; i <= b; i++)
    {
        int k = 0;
        for (int j = 2; j < i; j++)
        {
            if (i % j == 0)
            {
                k++;
                break;
            }
        }
        if (k == 0) s += i;
    }
    return s;
}
