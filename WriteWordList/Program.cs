using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WriteWordList
{
  class Program
  {
    public static List<string> WordList = new List<string>();
    static void Main(string[] args)
    {
      WordList.Add("One");
      WordList.Add("Two");
      WordList.Add("Three");

      using (StreamWriter stream = new StreamWriter("WordList.txt"))
      {
        foreach (var item in WordList)
        {
          stream.WriteLine(item);
        }
      }


      Console.Write("\nPress any key...");
      Console.ReadKey();
    }
  }
}
