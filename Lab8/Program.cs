using System.Diagnostics;
//Process
//ProcessModule
//ProcessModuleCollection
//ProcessStartInfo
//ProcessThread
//ProcessThreadCollection
//var runningProcs=from proc in Process.GetProcesses(".")
//                 orderby proc.Id select proc;
//foreach(var p in runningProcs)
//{
//    Console.WriteLine($"->PID: {p.Id}\t Name: {p.ProcessName}");
//}

//try
//{
//    Process theProc = Process.GetProcessById(13184);
//    Console.WriteLine(theProc.ProcessName);
//    Console.WriteLine(theProc.MainWindowTitle);
//    Console.WriteLine(theProc.Container);
//    Console.WriteLine(theProc.StartInfo);
//    Console.WriteLine(theProc.MainModule);
//    Console.WriteLine(theProc.MachineName);
//    Console.WriteLine(theProc.MaxWorkingSet);
//    foreach(ProcessThread pt in theProc.Threads)
//    {
//        Console.WriteLine($"->Thread ID:{pt.Id}\t Start Time:{pt.StartTime.ToShortTimeString()}\t priority:{pt.PriorityLevel}");
//    }
//    foreach (ProcessModule pt in theProc.Modules)
//    {
//        Console.WriteLine($"->Name:{pt.ModuleName}\t Start Time:{pt.FileName}\t priority:{pt.BaseAddress}");
//    }
//}
//catch(Exception ex)
//{

//}

try
{
    //Process proc = Process.Start(@"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe", "www.mail.ru");
    //Console.WriteLine(proc.ProcessName);
    //foreach (var p in Process.GetProcessesByName(proc.ProcessName)) p.Kill();
    //ProcessStartInfo startInfo = new ProcessStartInfo("MsEdge", "www.mail.ru");
    //startInfo.UseShellExecute = true;
    //Process? proc = Process.Start(startInfo);
    //int i = 0;
    //ProcessStartInfo s1=new ProcessStartInfo(@"C:\Users\su\Desktop\gogs.docx");
    //foreach(var verb in s1.Verb)
    //{
    //    Console.WriteLine($"{i++},{verb}");
    //}
    //s1.WindowStyle = ProcessWindowStyle.Maximized;
    //s1.Verb = "Print";
    //s1.UseShellExecute = true;
    //Process.Start(s1);
    AppDomain defaulrAD=AppDomain.CurrentDomain;
    Console.WriteLine(defaulrAD.FriendlyName);
    Console.WriteLine(defaulrAD.Id);
    Console.WriteLine(defaulrAD.IsDefaultAppDomain());
    Console.WriteLine(defaulrAD.BaseDirectory);
    Console.WriteLine(defaulrAD.SetupInformation.ApplicationBase);
    Console.WriteLine(defaulrAD.SetupInformation.TargetFrameworkName);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
