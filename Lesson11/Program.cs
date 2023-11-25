using System.Net;
using System.Text;

string theBook = "";
GetBook();
Console.WriteLine("Загрузка началась....");
Console.ReadLine();
void GetBook()
{
    WebClient wc=new WebClient();
    wc.DownloadStringCompleted += (s, eArgs) =>
    {
        theBook = eArgs.Result;
        Console.WriteLine(theBook);
        Console.WriteLine("Загрузка завершена.");
        GetStats();
    };
    wc.DownloadStringAsync(new Uri("http://www.gutenberg.org/files/98/98-0.txt"));
}
void GetStats()
{
    string[] words=theBook.Split(new char[]
    {
        ' ','\u000A',',','.',':',';','-','?','!','/'
    },StringSplitOptions.RemoveEmptyEntries);
    //string[] tenMostCommon=FindTenMostCommon(words);
    //string longestWord=FindLongestWord(words);
    string[] tenMostCommon = null;
    string longestWord = String.Empty;
    Parallel.Invoke(
        () => {
            tenMostCommon = FindTenMostCommon(words);
            },
        () =>
        {
            longestWord= FindLongestWord(words);
        }
        );
    StringBuilder bookStats=new StringBuilder("10 самых длинных слов:");
    foreach(string word in tenMostCommon)
    {
        bookStats.AppendLine(word);
    }
    bookStats.AppendFormat($"Самое длинное слово:{longestWord}");
    bookStats.AppendLine();
    Console.WriteLine(bookStats.ToString());
}

string[] FindTenMostCommon(string[] words)
{
    var frequencyOrder = from word in words
                         where word.Length > 6
                         group word by word into g
                         orderby g.Count() descending
                         select g.Key;
    string[] commonWords = (frequencyOrder.Take(10).ToArray());
    return commonWords;
}
string FindLongestWord(string[] words)
{
    return (from w in words orderby w.Length descending select w).FirstOrDefault();
}
