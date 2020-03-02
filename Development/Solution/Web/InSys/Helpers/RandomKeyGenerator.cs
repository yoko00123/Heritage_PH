using System;
using System.Text;

namespace InSys.Helpers
{
    public class RandomKeyGenerator
    {
        String Key_Letters = null;
        String Key_Numbers = null;
        int Key_Chars;
        char[] LettersArray = null;
        char[] NumbersArray = null;

        protected internal string KeyLetters
        {
            set { Key_Letters = value; }
        }
        protected internal string KeyNumbers
        {
            set { Key_Numbers = value; }
        }
        protected internal int KeyChars
        {
            set { Key_Chars = value; }
        }
        public string Generate()
        {
            int i_key;
            Single Random1;
            Int16 arrIndex;
            StringBuilder sb = new StringBuilder();
            String RandomLetter;

            LettersArray = Key_Letters.ToCharArray();
            NumbersArray = Key_Numbers.ToCharArray();

            for (i_key = 1; i_key <= Key_Chars; i_key++)
            {
                // Randomize();
                Random1 = new Random().Next(99999);  //Rnd();
                arrIndex = -1;
                if (Convert.ToInt32(Random1 * 111) % 2 == 0)
                {
                    while (arrIndex < 0)
                        arrIndex = Convert.ToInt16(LettersArray.GetUpperBound(0) * Random1);
                    RandomLetter = LettersArray[arrIndex].ToString();
                    if (Convert.ToInt32(arrIndex * Random1 * 99) % 2 != 0)
                    {
                        RandomLetter = LettersArray[arrIndex].ToString();
                        RandomLetter = RandomLetter.ToUpper();
                    }
                    sb.Append(RandomLetter);
                }
                else
                {
                    while (arrIndex < 0)
                        arrIndex = Convert.ToInt16(NumbersArray.GetUpperBound(0) * Random1);
                    sb.Append(NumbersArray[arrIndex]);
                }
            }
            return sb.ToString();
        }
    }
}