//Проблема
//int x = 0;
//for (int i = 0; i < 5; i++)
//{
//    Thread thread = new Thread(Print);
//    thread.Name = $"Поток:{i}";
//    thread.Start();
//}
//void Print()
//{
//    for (int i = 1;i<7;i++)
//    {
//        Console.WriteLine($"{Thread.CurrentThread.Name}:{x}");
//        x++;
//        Thread.Sleep(1000);
//    }
//}

//синхронизация lock
//int x=0;
//object locker = new();
//for (int i = 0; i < 5; i++)
//{
//    Thread thread = new Thread(Print);
//    thread.Name = $"Поток:{i}";
//    thread.Start();
//}
//void Print()
//{
//    lock (locker)
//    {
//        for (int i = 1; i < 7; i++)
//        {
//            Console.WriteLine($"{Thread.CurrentThread.Name}:{x}");
//            x++;
//            Thread.Sleep(1000);
//        }
//    }
//}

//синхронизация monitor
//int x = 0;
//object locker = new();
//for (int i = 0; i < 5; i++)
//{
//    Thread thread = new Thread(Print);
//    thread.Name = $"Поток:{i}";
//    thread.Start();
//}
//void Print()
//{
//    bool acquiredLock=false;
//    try
//    {
//        Monitor.Enter(locker,ref acquiredLock);
//        for (int i = 1; i < 7; i++)
//        {
//            Console.WriteLine($"{Thread.CurrentThread.Name}:{x}");
//            x++;
//            Thread.Sleep(1000);
//        }
//    }
//    finally
//    {
//        if(acquiredLock) Monitor.Exit(locker);
//    }
//}

//синхронизация AutoResetEvent
//int x = 0;
//AutoResetEvent wait=new AutoResetEvent(true);

//for (int i = 0; i < 5; i++)
//{
//    Thread thread = new Thread(Print);
//    thread.Name = $"Поток:{i}";
//    thread.Start();
//}
//void Print()
//{
//    wait.WaitOne();
//        for (int i = 1; i < 7; i++)
//        {
//            Console.WriteLine($"{Thread.CurrentThread.Name}:{x}");
//            x++;
//            Thread.Sleep(1000);
//        }
//    wait.Set();
//}

//синхронизация мьютекс
//int x = 0;
//Mutex mutex = new();

//for (int i = 0; i < 5; i++)
//{
//    Thread thread = new Thread(Print);
//    thread.Name = $"Поток:{i}";
//    thread.Start();
//}
//void Print()
//{
//    mutex.WaitOne();
//    for (int i = 1; i < 7; i++)
//    {
//        Console.WriteLine($"{Thread.CurrentThread.Name}:{x}");
//        x++;
//        Thread.Sleep(1000);
//    }
//    mutex.ReleaseMutex();
//}

//Semaphore
for (int i = 0; i < 8; i++)
{
    Reader reader = new Reader(i);
}
class Reader
{
    Random random;
    static Semaphore sem = new Semaphore(3, 3);
    Thread myThread;
    int count = 3;
    public Reader(int i)
    {
        random = new Random();
        myThread = new Thread(Read);
        myThread.Name = $"Читатель: {i}";
        myThread.Start();
    }
    void Read()
    {
        while (count>0) 
        {
            sem.WaitOne();
            Console.WriteLine($"{Thread.CurrentThread.Name} входит в библиотеку");
            Console.WriteLine($"{Thread.CurrentThread.Name} читает");
            Thread.Sleep( random.Next(5000));
            Console.WriteLine($"{Thread.CurrentThread.Name} выходит из библиотеки");
            sem.Release();
            count--;
            Thread.Sleep(1000);
        }
    }
}
