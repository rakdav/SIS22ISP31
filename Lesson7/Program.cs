//Класс Parallel
//Invoke
//Parallel.Invoke(
//    Display,
//    ()=>
//    {
//        Console.WriteLine($"Выполняется задача {Task.CurrentId}");
//        Thread.Sleep(1000);
//    },
//    () =>Factorial(5)
//    );
//Parallel.For
//Parallel.For(1, 5, Factorial);
////Parallel.ForEach
//ParallelLoopResult result = Parallel.ForEach<int>(
//    new List<int>() { 1,4,7,9},Factorial);
//if(!result.IsCompleted)
//    Console.WriteLine($"Выполнение цикла завершено на итерации {result.LowestBreakIteration}");
//void Display()
//{
//    Console.WriteLine($"Выполняется задача {Task.CurrentId}");
//    Thread.Sleep(1000);
//}
//void Factorial(int n,ParallelLoopState pls)
//{
//    long fact = 1;
//    if (n == 4) pls.Break();
//    for (int i = 1; i <= n; i++) fact *= i;
//    Console.WriteLine($"Выполняется задача {Task.CurrentId}");
//    Console.WriteLine($"Factorial={fact}");
//    Thread.Sleep(1000);
//}

//CancellationToken
CancellationTokenSource source = new CancellationTokenSource();
CancellationToken token=source.Token;
Task task = new Task(() =>
{
    for (int i = 1; i < 10; i++)
    {
        if (token.IsCancellationRequested)
        {
            Console.WriteLine("Операция прервана");
            return;
        }
        Console.WriteLine($"Квадрат числа {i} равен {i*i}");
        Thread.Sleep(200);
    }
},token
);
task.Start();
Thread.Sleep(1000);
//task.Wait();
source.Cancel();

Console.WriteLine($"Task Status:{task.Status}");
//source.Dispose();
