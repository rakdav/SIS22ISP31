Console.WriteLine("Для завершения задачи нажмите y");
CancellationTokenSource cToken = new CancellationTokenSource();
CancellationToken token = cToken.Token;
Task task = new Task(() =>
{
    for (int i = 1; i < 10; i++)
    {
        if (token.IsCancellationRequested)
        {
            Console.WriteLine("Операция прервана");
            return;
        }
        Console.WriteLine($"Квадрат числа {i} равен {i * i}");
        Thread.Sleep(1000);
    }
}, token
);
task.Start();
if (Console.ReadKey(true).KeyChar == 'y') cToken.Cancel();
Console.WriteLine($"Статус задачи:{task.Status}");
task.Wait();
cToken.Dispose();