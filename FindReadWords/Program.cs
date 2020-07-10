using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindReadWords
{
  class Program
  {
    public static DateTime Time = DateTime.Now;
    public static List<string> WordList = new List<string>();
    public static string RandomWord = "PZLAEEGN".ToLower();
    public static List<string> FoundWords = new List<string>();

    static void Main(string[] args)
    {
      LoadWordList();

      SearchWords();

      int count = 0;
      foreach (var word in FoundWords.OrderByDescending(x => x.Length))
      {
        count++;
        if (count > 5)
        {
          break;
        }
        Console.WriteLine($"{word}\t{word.Length}");
      }
      Console.WriteLine($"\nFound {FoundWords.Count} words");

      Console.Write("\nPress any key...");
      Console.ReadKey();
    }

    private static void Log(string message)
    {
      Console.WriteLine($"{(DateTime.Now - Time)} {message}");
    }

    private static void LoadWordList()
    {
      string[] wordList = Properties.Resources.WordList
        .Replace("\r\n", "\n")
        .Split('\n');
      foreach (string word in wordList)
      {
        WordList.Add(word);
      }
      Log($"{WordList.Count} are loaded");
    }

    /// <summary>
    /// Search for words with the matrix letters of 2 letters.
    /// </summary>
    private static void SearchWords()
    {
      for (int x = 0; x < RandomWord.Length; x++)
      {
        for (int y = 0; y < RandomWord.Length; y++)
        {
          if (x == y) { continue; }
          AddFoundWord(RandomWord[x].ToString() + RandomWord[y].ToString());
        }
      }
      Log($"{FoundWords.Count} words found");
    }

    /// <summary>
    /// Does the found word exists the right letters.
    /// </summary>
    /// <param name="search"></param>
    private static void AddFoundWord(string search)
    {
      foreach (string found in WordList.Where(x => x.Contains(search)))
      {
        if (!FoundWords.Contains(found))
        {
          if (DoesLetterExist(found) && MatchWord(found))
          {
            FoundWords.Add(found);
          }
        }
      }
    }

    /// <summary>
    /// Contains the word the letters?
    /// </summary>
    /// <param name="word"></param>
    /// <returns></returns>
    private static bool DoesLetterExist(string word)
    {
      bool add = true;
      foreach (char letter in word)
      {
        add = add && RandomWord.Contains(letter);
        if (!add) { break; }
      }
      return add;
    }

    /// <summary>
    /// Match the word the letters?
    /// </summary>
    /// <param name="word"></param>
    /// <returns></returns>
    private static bool MatchWord(string word)
    {
      bool add = true;
      string random = RandomWord;

      foreach (char letter in word)
      {
        int pos = random.IndexOf(letter);
        if (pos > 0)
        {
          random = random.Remove(pos, 1);
        }
        else
        {
          add = false;
          break;
        }
      }
      return add;
    }

  }
}
