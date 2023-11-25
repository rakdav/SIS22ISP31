using System.Collections;
using System.Collections.ObjectModel;

ArrayList list = new ArrayList() {"ArrayList",-56109,3.14,"List","Sort void", 0.0309,2.71E-3,
"z",'F'};
Console.WriteLine("Меню");
Console.WriteLine("1 - задача 1");
Console.WriteLine("2 - задача 2");
Console.WriteLine("3 - задача 3");
Console.WriteLine("4 - задача 4");
Console.WriteLine("5 - задача 5");
Console.WriteLine("6 - выход");
int n;
do
{
    Console.Write("Введите число:");
    n=int.Parse(Console.ReadLine()!);
    switch(n)
    {
        case 1: 
            {
                var src = await SumTypes(list);
                int k = 1;
                foreach(var x in src)
                {
                    switch(k)
                    {
                        case 1: Console.WriteLine($"Строк:{x}"); break;
                        case 2: Console.WriteLine($"Целых чисел:{x}"); break;
                        case 3: Console.WriteLine($"Дробных чисел:{x}"); break;
                        case 4: Console.WriteLine($"Символов:{x}"); break;
                        case 5: Console.WriteLine($"Всего:{x}"); break;
                    }
                    k++;
                }
                async Task<IEnumerable<int>> SumTypes(ArrayList list)
                {
                    var collection=new Collection<int>();
                    int countString = 0;
                    int countInt = 0;
                    int countDouble=0;
                    int countChar = 0;
                    var result=await Task.Run(
                        ()=>
                        {
                            for(var i=0;i<list.Count; i++)
                            {
                                if (list[i]!.GetType()==typeof(String)) countString++;
                                if (list[i]!.GetType() == typeof(int)) countInt++;
                                if (list[i]!.GetType() == typeof(double)) countDouble++;
                                if (list[i]!.GetType() == typeof(Char)) countChar++;
                            }
                            collection.Add(countString);
                            collection.Add(countInt);
                            collection.Add(countDouble);
                            collection.Add(countChar); 
                            collection.Add(list.Count);
                            return collection;
                        }
                    );
                    return result;
                }
            } 
            break;
        case 2:
            {
                Console.WriteLine($"{await SumTypes(list):F2}");
                async Task<Double> SumTypes(ArrayList list)
                {

                    var result = await Task.Run(
                        () =>
                        {
                            double sum = 0;
                            for (var i = 0; i < list.Count; i++)
                            {
                                if (list[i]!.GetType() == typeof(int)) sum += (int)list[i]!;
                                if (list[i]!.GetType() == typeof(double)) sum += (double)list[i]!;
                            }
                            return sum;
                        }
                    );
                    return result;
                }
            }
            break;
        case 3:
            {
                var src = await SumTypes(list);
                foreach (var item in src)
                {
                    Console.Write(item+" ");
                }
                Console.WriteLine();
                async Task<ArrayList> SumTypes(ArrayList list)
                {
                    var collection = new ArrayList();
                    var result = await Task.Run(
                        () =>
                        {

                            double sum = 0;
                            for (var i = 0; i < list.Count; i++)
                            {
                                if (list[i]!.GetType() == typeof(string)) collection.Add(list[i]);
                                if (list[i]!.GetType() == typeof(char)) collection.Add(list[i]);
                            }
                            return collection;
                        }
                    );
                    return result;
                }
            } 
            break;
        case 4:
            {
                var src = await SumTypes(1,3,4);
                foreach (var item in src)
                {
                    Console.Write(item + " ");
                }
                Console.WriteLine();
                async Task<ArrayList> SumTypes(params int[] numbers)
                {
                    var collection = new ArrayList(list);
                    var result = await Task.Run(
                        () =>
                        {
                            foreach (int item in numbers)
                            {
                                collection.RemoveAt(item);
                            }
                            return collection;
                        }
                    );
                    return result;
                }
            }
            break;
        case 5:
            {
                Console.Write("Введите индекс:");
                int index=int.Parse(Console.ReadLine()!);
                Console.Write("Введите значение:");
                string val = Console.ReadLine()!;
                var src = await SumTypes(index,val);
                foreach (var item in src)
                {
                    Console.Write(item + " ");
                }
                Console.WriteLine();
                async Task<ArrayList> SumTypes(int index,string val)
                {
                    var collection = new ArrayList(list);
                    var result = await Task.Run(
                        () =>
                        {;
                            if (val.GetType() == typeof(int)) collection.Insert(index, int.Parse(val.ToString()!));
                            if (val.GetType() == typeof(double)) collection.Insert(index, double.Parse(val.ToString()!));
                            else collection.Insert(index, val);
                            return collection;
                        }
                    );
                    return result;
                }
            }
            break;
    }

}
while (n!=6);
