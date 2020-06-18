using System;
using System.Collections.Generic;
using System.Linq;

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var result = new Dictionary<string, string>();

            if (text.Count > 0)
            {
                for (int i = 0; i < text.Count; i++)
                {
                    if(text[i].Count > 1)
                    {
                        if (text[i].Count < 3)
                            result = Get2Gramms(text,result);
                        else 
                        {
                            result = Get2Gramms(text,result);
                            result = Get3Gramms(text,result);
                        }
                    }
                }   
            }
            return result;
        }

        private static Dictionary<string, string> Get3Gramms(List<List<string>> text, Dictionary<string,string> result)
        {
            Dictionary<string, Dictionary<string, int>> possible3Gramms = new Dictionary<string, Dictionary<string, int>>();

            for(int i = 0; i < text.Count; i++)
            {
                for (int j = 0; j < text[i].Count - 2; j++)
                {
                    string threeGrammsKey = text[i][j] + " " + text[i][j + 1];
                    string nextWord = text[i][j + 2];
                    AddWords(threeGrammsKey, nextWord, possible3Gramms);
                }
            }
            GetMostFreq(possible3Gramms, result);
            return result;
        }

        private static Dictionary<string, string> Get2Gramms(List<List<string>> text, Dictionary<string, string> result)
        {
            Dictionary<string, Dictionary<string,int>> checkingSecWords = new Dictionary<string, Dictionary<string, int> >();
            
            // inside sentence
            for (int i = 0; i < text.Count; i++)
            {
                // work with each word
                for (int j = 0; j < text[i].Count - 1; j++)
                {
                    string word = text[i][j];
                    string nextWord = text[i][j + 1];

                    AddWords(word, nextWord, checkingSecWords);
                }
            }
        GetMostFreq(checkingSecWords, result);
            
            return result;
        }

        private static void AddWords(string word, string nextWord, Dictionary<string, Dictionary<string, int>> checkingSecWords)
        {
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

        private static void GetMostFreq(Dictionary<string, Dictionary<string, int>> checkingSecWords, Dictionary<string,string> result)
        {
           
                int amount = checkingSecWords.Keys.Count;
                string[] firstWords = new string[amount];
                checkingSecWords.Keys.CopyTo(firstWords, 0);
                for (int i = 0; i < firstWords.Length; i++)
                {
                    int amoutSecWords = checkingSecWords[firstWords[i]].Keys.Count;
                    string[] secWords = new string[amoutSecWords];
                    checkingSecWords[firstWords[i]].Keys.CopyTo(secWords, 0);
                    int max = 0;
                    for (int j = 0; j < secWords.Length; j++)
                    {
                        if (checkingSecWords[firstWords[i]][secWords[j]] > max)
                        {
                            max = checkingSecWords[firstWords[i]][secWords[j]];
                            if (result.ContainsKey(firstWords[i]))
                                result[firstWords[i]] = secWords[j];
                            else
                                result.Add(firstWords[i], secWords[j]);
                        }
                        else if (checkingSecWords[firstWords[i]][secWords[j]] == max)
                        {
                            result[firstWords[i]] = (string.CompareOrdinal(secWords[j], result[firstWords[i]]) < 0 ? secWords[j] : result[firstWords[i]]);
                        }
                    }
                }
        }
    }
}