using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordListFilter
{
  /* The game "Letters" has a limited length.
   * Replacing for Dutch:
   *   i and j => y.
   *   ' or - => nothing.
   * Output:
   *   Word list.
   *   Count the letter frequency.
   */
  class Program
  {
    public static SortedDictionary<char, int> Letters = new SortedDictionary<char, int>();

    static void Main(string[] args)
    {
      const byte MaxLength = 8;
      const string InputPath = @"C:\Users\chi\Google Drive\words_dutch_gz.txt";
      const string OutputPath = "WordList (nl-NL).txt";

      int WordCount = 0;
      string word;

      using (StreamWriter stream = new StreamWriter(OutputPath))
      {
        using (StreamReader input = new StreamReader(InputPath))
        {
          while (!input.EndOfStream)
          {
            word = input.ReadLine().ToLower();

            // Exclude words with a Q or X
            if (!(word.Contains("q") || word.Contains("x")))
            {
              // Replacements
              word = word.Replace("ij", "y");
              word = word.Replace("-", "");
              word = word.Replace("'", "");

              if (word.Length > 1 && word.Length <= MaxLength)
              {
                CountLetters(word);
                stream.WriteLine(word);
                WordCount++;
              }
            }
          }
        }
      }

      Console.WriteLine($"Number of words: {WordCount}\n");

      Console.WriteLine($"| Letter | Count | Relative");
      Console.WriteLine($"| ---- | ---: | ---:");
      foreach (var item in Letters)
      {
        Console.WriteLine($"| {item.Key} | {item.Value} | {(int)((item.Value / Letters['j']) + 0.50)}");
      }

      Console.Write("\nPress any key...");
      Console.ReadKey();
    }

    private static void CountLetters(string word)
    {
      foreach (char letter in word)
      {
        if (!Letters.ContainsKey(letter))
        {
          Letters.Add(letter, 1);
        }
        else
        {
          Letters[letter]++;
        }
      }
    }
  }
}
