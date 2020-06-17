using System.Collections.Generic;
using System.Text;
using System;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        public static List<List<string>> ParseSentences(string text)
        {
            var sentencesList = new List<List<string>>();
            string[] textArr = DivideText(text);
            foreach (string sentence in textArr)
            {
                var listWords = new List<string>();
                var builderWord = new StringBuilder();
                foreach (char letter in sentence)
                {
                    if (CheckLetter(letter))
                        builderWord.Append(letter.ToString().ToLower());
                    else
                    {
                        ReturnListOfWords(builderWord, listWords);
                        builderWord.Clear();
                    }
                }
                ReturnListOfWords(builderWord, listWords);
                if (listWords.Count != 0) sentencesList.Add(listWords);
            }
            return sentencesList;
        }

        public static string[] DivideText(string text)
        {
            char[] separators = ".!?;:()\"".ToCharArray();
            return text.Split(separators);
        }

        public static List<string> ReturnListOfWords(StringBuilder strBrWithWord, List<string> listWords)
        {
            if (strBrWithWord.Length != 0)
            {
                listWords.Add(strBrWithWord.ToString().ToLower());
            }
            return listWords;
        }

        public static bool CheckLetter(char letter)
        {
            return Char.IsLetter(letter) || letter == '\'';
        }
    }
}