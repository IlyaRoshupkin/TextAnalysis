using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;

namespace TextAnalysis
{
    static class TextGeneratorTask
    {
        public static string ContinuePhrase(
            Dictionary<string, string> nextWords,
            string phraseBeginning,
            int wordsCount)
        {
            if (nextWords.Count < 1 || wordsCount == 0) return phraseBeginning;
            string text = "";
            if (wordsCount == 1)
                text = phraseBeginning + AddOneWord(phraseBeginning, nextWords);
            else if (wordsCount > 1)
                text = AddSeveralWords(phraseBeginning, nextWords, wordsCount);
            return text;
        }

        private static string AddSeveralWords(string phraseBeginning, Dictionary<string, string> nextWords, int wordsCount)
        {
            string phrase = phraseBeginning;
            string word;
            for (int i = 0; i < wordsCount; i++)
            {
                word = AddOneWord(phrase, nextWords);
                phrase += word;
            }   
            return phrase;
        }

        private static string AddOneWord(string phrase, Dictionary<string, string> nextWords)
        {
            string[] phraseWords = phrase.Split(' ');
            int length = phraseWords.Length;
            if(phraseWords.Length > 1)
            {
                if(nextWords.ContainsKey(phraseWords[length-2]+" "+phraseWords[length-1]))
                    return " " + nextWords[phraseWords[phraseWords.Length - 2] + " " + phraseWords[phraseWords.Length - 1]];
                else if (nextWords.ContainsKey(phraseWords[length-1]))
                    return " " + nextWords[phraseWords[length - 1]];
            }
            else if(phraseWords.Length == 1)
            {
                if (nextWords.ContainsKey(phraseWords[0]))
                return " " + nextWords[phraseWords[0]];
            }
            return null;
        }
    }
}