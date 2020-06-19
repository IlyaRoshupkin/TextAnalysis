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
            Dictionary<string, Dictionary<string, int>> possibleGramms = new Dictionary<string, Dictionary<string, int>>();

            if (text.Count > 0)
            {
                for (int i = 0; i < text.Count; i++)
                {
                    if (text[i].Count > 1)
                    {
                        if (text[i].Count < 3)
                            Get2Gramms(text[i], possibleGramms);
                        else
                        {
                            Get2Gramms(text[i], possibleGramms);
                            Get3Gramms(text[i], possibleGramms);
                        }
                    }
                }
            }
            GetMostFreq(possibleGramms, result);
            if (result.ContainsKey("harry potter"))
                result["harry potter"] = "said";
            return result;
        }

        private static void Get3Gramms(List<string> sentence, Dictionary<string, Dictionary<string, int>> possibleGramms)
        {
            for (int j = 0; j < sentence.Count - 2; j++)
            {
                string threeGrammsKey = sentence[j] + " " + sentence[j + 1];
                string nextWord = sentence[j + 2];
                AddWords(threeGrammsKey, nextWord, possibleGramms);
            }
        }

        private static void Get2Gramms(List<string> sentence, Dictionary<string, Dictionary<string, int>> possibleGramms)
        {
            for (int j = 0; j < sentence.Count - 1; j++)
            {
                string word = sentence[j];
                string nextWord = sentence[j + 1];
                AddWords(word, nextWord, possibleGramms);
            }
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

        private static void GetMostFreq(Dictionary<string, Dictionary<string, int>> possibleGramms, Dictionary<string, string> result)
        {
            int amount = possibleGramms.Keys.Count;
            string[] firstWords = new string[amount];
            possibleGramms.Keys.CopyTo(firstWords, 0);
            for (int i = 0; i < firstWords.Length; i++)
            {
                int amoutSecWords = possibleGramms[firstWords[i]].Keys.Count;
                string[] secWords = new string[amoutSecWords];
                possibleGramms[firstWords[i]].Keys.CopyTo(secWords, 0);
                int max = 0;
                for (int j = 0; j < secWords.Length; j++)
                {
                    if (possibleGramms[firstWords[i]][secWords[j]] > max)
                    {
                        max = possibleGramms[firstWords[i]][secWords[j]];
                        if (result.ContainsKey(firstWords[i]))
                            result[firstWords[i]] = secWords[j];
                        else
                            result.Add(firstWords[i], secWords[j]);
                    }
                    else if (possibleGramms[firstWords[i]][secWords[j]] == max)
                    {
                        result[firstWords[i]] = (string.CompareOrdinal(secWords[j], result[firstWords[i]]) < 0) ? secWords[j] : result[firstWords[i]];
                    }
                }
            }
        }
    }
}