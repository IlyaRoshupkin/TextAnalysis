using System;
using System.Collections.Generic;

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var result = new Dictionary<string, string>();
            if (text.Count > 0 && text[0].Count > 1)
            {
                if(text[0].Count<3)
                    result = Get2Gramms(text);
                else if(text[0].Count)
            }
                
                
            return result;
        }

        private static Dictionary<string, string> Get2Gramms(List<List<string>> text)
        {
            Dictionary<string, string> bgramms = new Dictionary<string, string>();
            
            Dictionary<string, Dictionary<string,int>> checkingSecWords = new Dictionary<string, Dictionary<string, int> >();
            
            // inside sentence
            for (int i = 0; i < text.Count; i++)
            {
                // work with each word
                for (int j = 0; j < text[i].Count - 1; j++)
                {
                    string word = text[i][j];
                    string nextWord = text[i][j + 1];
                    if (checkingSecWords.ContainsKey(word))
                    {
                        if (checkingSecWords[word].ContainsKey(nextWord))
                        {
                            checkingSecWords[word][nextWord]++;
                        }
                        else
                        {
                            checkingSecWords[word].Add(nextWord, 1);
                        }
                    }
                    else
                    {
                        checkingSecWords.Add(word, new Dictionary<string, int>());
                        checkingSecWords[word].Add(nextWord, 1);
                    }            
                }
            }
            GetMostFreq(checkingSecWords, bgramms);
            
            return bgramms;
        }

        private static void GetMostFreq(Dictionary<string, Dictionary<string, int>> checkingSecWords, Dictionary<string,string> bgramms)
        {
            int amount = checkingSecWords.Keys.Count;
            string[] firstWords = new string[amount];
            checkingSecWords.Keys.CopyTo(firstWords, 0);
            for (int i = 0; i < firstWords.Length; i++)
            {
                int amoutSecWords = checkingSecWords[firstWords[i]].Keys.Count;
                string[] secWords = new string[amoutSecWords];
                int max = 0;
                for(int j = 0; j < secWords.Length; j++)
                {
                    checkingSecWords[firstWords[i]].Keys.CopyTo(secWords, 0);
                    if(checkingSecWords[firstWords[i]][secWords[j]] > max)
                    {
                        max = checkingSecWords[firstWords[i]][secWords[j]];
                        bgramms[firstWords[i]] = secWords[j];
                    }
                }
            }
        }
    }
}