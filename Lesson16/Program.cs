//int[] numbers=new int[10];
//Random random = new Random();
//for (int i = 0; i < numbers.Length; i++)
//{
//    numbers[i] = random.Next(10, 100);
//    Console.Write(numbers[i]+" ");
//}
//Console.WriteLine();
//var squares = from c in numbers.AsParallel()
//              select Square(c);
//foreach (var square in squares)
//    Console.Write(square+" ");
//Console.WriteLine();
//var squaresLinq=numbers.AsParallel().Select(x => Square(x));
//foreach (var square in squaresLinq)
//    Console.Write(square + " ");
//Console.WriteLine();
//(from c in numbers.AsParallel() select Square(c)).ForAll(Console.WriteLine);
//int Square(int n) => n * n;
using System.Collections;

ArrayList list = new ArrayList() { 2, 3.4, 0.6E1,'r', 1, 5, 7, 8 };
int[] mas=list.AsParallel().OfType<int>().ToArray();
var res=mas.AsParallel().AsOrdered().Select(x => Func(x)).ToArray();
foreach(int i in res)
    Console.Write(i+" ");
Console.WriteLine();
double Func(int n)
{
    if (n != 0)
        return Math.Pow(1 + (1.0 / n), n);
    else throw new ArithmeticException("Деление на ноль");
}