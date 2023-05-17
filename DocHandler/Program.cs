﻿using System.IO;
namespace DocHandler
{
    internal class Program
    {
        static void Main(string[] args)
        {
          List<string> doc_texts = new DocumentHandler(@"..\.\Files").GetDocTexts();
          foreach(string doc in doc_texts)
          {
            Console.WriteLine(doc);
          }
        }
        
    }
}