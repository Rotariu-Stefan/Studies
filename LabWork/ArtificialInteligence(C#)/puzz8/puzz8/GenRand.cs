using System;
using System.Collections.Generic;

namespace puzz8
{
    public class GenRand
    {
        int m_newRandomNumber = 0;
        List<int> RemainingNumbers;

        public GenRand( int min, int max )
        {

            int range = max - min;
            int[] NArr = new int[range + 1];

            for (int count = 0; count < NArr.Length; count++)
                NArr[count] = min + count;


            RemainingNumbers = new List<int>();
            RemainingNumbers.AddRange(NArr);
        }

        public int NewRandomNumber(Random rn)
        {
            if (RemainingNumbers.Count != 0)
            {

                int index = rn.Next(0, RemainingNumbers.Count);
                m_newRandomNumber = RemainingNumbers[index];

                RemainingNumbers.RemoveAt(index);

                return m_newRandomNumber;
            }
            else
                Console.WriteLine("No more numbers!!");
            return -1;
        }
    }
}


