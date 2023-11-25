Thread currentThread = Thread.CurrentThread;

//свойства
//name
Console.WriteLine($"Name:{currentThread.Name}");
currentThread.Name = "Main";
Console.WriteLine($"Name:{currentThread.Name}");
//ExecutionContext-контекст, выпонения потока
Console.WriteLine(currentThread.ExecutionContext);
//IsAlive - работает или на работает
Console.WriteLine(currentThread.IsAlive);
//IsBackground - работает или на работает
Console.WriteLine(currentThread.IsBackground);
//ManagedThreadId - id потока
Console.WriteLine(currentThread.ManagedThreadId);
//Priority - id потока
//Lowest
//BelowNormal
//Normal
//AboveNormal
//Highest
Console.WriteLine(currentThread.Priority);
currentThread.Priority = ThreadPriority.Highest;
Console.WriteLine(currentThread.Priority);
//ThreadState - состояние потока
Console.WriteLine(currentThread.ThreadState);

//методы
//GetDomainID()
Console.WriteLine(Thread.GetDomainID());
//Sleep() - задержка потока
//Thread.Sleep(5000);
//Interrupt()
//Join()
//Start() - запуск потока

//Создание потока
Print();
Thread myThread = new Thread(Print);
myThread.Name = "Fone";
myThread.Start();
Thread thread2= new Thread(new ParameterizedThreadStart(PrintParam));
thread2.Start(9);
void Print()
{
    for (int i = 0;i<5;i++)
    {
        Console.WriteLine($"{Thread.CurrentThread.Name}:{i} +  ");
        Thread.Sleep(1000);
    }
}
void PrintParam(object obj)
{
    for (int i = 0; i < Convert.ToInt32(obj); i++)
    {
        Console.WriteLine($"{Thread.CurrentThread.Name}:{i} +  ");
        Thread.Sleep(1000);
    }
}
