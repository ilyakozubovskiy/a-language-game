using System;
using System.Globalization;
using System.Text;

namespace LanguageGame
{
    public static class Translator
    {
        /// <summary>
        /// Translates from English to Pig Latin. Pig Latin obeys a few simple following rules:
        /// - if word starts with vowel sounds, the vowel is left alone, and most commonly 'yay' is added to the end;
        /// - if word starts with consonant sounds or consonant clusters, all letters before the initial vowel are
        ///   placed at the end of the word sequence. Then, "ay" is added.
        /// Note: If a word begins with a capital letter, then its translation also begins with a capital letter,
        /// if it starts with a lowercase letter, then its translation will also begin with a lowercase letter.
        /// </summary>
        /// <param name="phrase">Source phrase.</param>
        /// <returns>Phrase in Pig Latin.</returns>
        /// <exception cref="ArgumentException">Thrown if phrase is null or empty.</exception>
        /// <example>
        /// "apple" -> "appleyay"
        /// "Eat" -> "Eatyay"
        /// "explain" -> "explainyay"
        /// "Smile" -> "Ilesmay"
        /// "Glove" -> "Oveglay".
        /// </example>
        public static string TranslateToPigLatin(string phrase)
        {
            if (string.IsNullOrWhiteSpace(phrase))
            {
                throw new ArgumentException("Source string cannot be null or empty or whitespace.");
            }

            StringBuilder result = new StringBuilder(phrase);

            for (int i = 0; i < result.Length; i++)
            {
                if (!char.IsLetter(result[i]))
                {
                    continue;
                }

                int beginIndex = i;
                int endIndex = FindSeparatorIndex(ref result, beginIndex);

                string word = result.ToString(beginIndex, endIndex - beginIndex).ToLower(CultureInfo.CurrentCulture);
                int vowelIndex = FindVowelIndex(word);

                char firstLetter = result[beginIndex];
                result.Remove(beginIndex, word.Length);
                
                if (vowelIndex == 0)
                {
                    result.Insert(beginIndex, word + "yay");
                    beginIndex += word.Length + 3;
                }
                else if (vowelIndex > 0)
                {
                    result.Insert(beginIndex, word[vowelIndex..word.Length] + word[..vowelIndex] + "ay");
                    beginIndex += word.Length + 2;
                }

                if (char.IsUpper(firstLetter))
                {
                    result[i] = char.ToUpper(result[i], CultureInfo.InvariantCulture);
                }

                i = beginIndex;
            }

            return result.ToString();
        }

        private static int FindSeparatorIndex(ref StringBuilder source, int index)
        {
            char[] separators = new char[] { ' ', '-', '?', '.', ',', '!' };
            for (int i = index; i < source.Length; i++)
            {
                for (int j = 0; j < separators.Length; j++)
                {
                    if (source[i] == separators[j])
                    {
                        return i;
                    }
                }
            }

            return source.Length;
        }

        private static int FindVowelIndex(string source)
        {
            char[] vowels = new char[] { 'a', 'e', 'i', 'o', 'u' };
            for (int i = 0; i < source.Length; i++)
            {
                for (int j = 0; j < vowels.Length; j++)
                {
                    if (source[i] == vowels[j])
                    {
                        return i;
                    }
                }
            }

            return source.Length;
        }
    }
}
