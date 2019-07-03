using System;

namespace LargeNumberMultiplication
{
    class Program
    {
        static void Main(string[] args)
        {
            string num1 = "4154";
            string num2 = "51454";
            
            string num3 = "654154154151454545415415454";
            string num4 = "63516561563156316545145146514654";

            Console.WriteLine(num1 + " x " + num2 + " = ");
            Console.WriteLine("Result: " + multiply(num1, num2));

            Console.WriteLine();

            Console.WriteLine(num3 + " x " + num4 + " = ");
            Console.WriteLine("Result: " + multiply(num3, num4));
        }

        static string multiply(string a, string b)
        {
            int[] first = convertToIntArray(a);
            int[] second = convertToIntArray(b);

            int[] resultArray = new int[first.Length + second.Length + 1];
            int factor = 0;

            for(int i = second.Length - 1; i >= 0; i--)
            {
                int[] temp_result = multiplayArrayBy(first, second[i], factor); //Fix to add zeros
                resultArray = addArrays(resultArray, temp_result);
                factor++;
            }

            return convertToString(resultArray);
        }

        static int[] addArrays(int[] one, int[] two)
        {
            int[] result = new int[Math.Max(one.Length, two.Length) + 1];

            int[] larger;
            int[] smaller;

            if (one.Length > two.Length)
            {
                larger = one;
                smaller = two;
            }
            else
            {
                larger = two;
                smaller = one;
            }

            int smallerPosition = smaller.Length - 1;
            int resultPosition = result.Length - 1;
            for(int i = larger.Length - 1; i >= 0; i--)
            {
                if(smallerPosition < 0)
                {
                    result[resultPosition] += larger[i];
                }
                else
                {
                    result[resultPosition] += smaller[smallerPosition] + larger[i];
                    if (result[resultPosition] > 9)
                    {
                        result[resultPosition - 1] += 1;
                        result[resultPosition] = result[resultPosition] % 10;
                    }
                }

                resultPosition--;
                smallerPosition--;
            }
            return result;
        }

        static int[] multiplayArrayBy(int[] array, int value, int factor)
        {
            int[] result = new int[array.Length + 1];
            for (int i = array.Length - 1; i >= 0; i--)
            {
                result[i + 1] += array[i] * value;
                if(result[i + 1] > 9)
                {
                    result[i] = ((result[i + 1] - (result[i + 1] % 10)) / 10);
                    result[i + 1] = result[i + 1] % 10;
                }
            }

            if(factor > 0)
            {
                int[] newResult = new int[result.Length + factor];
                for(int i = 0; i < result.Length; i++)
                {
                    newResult[i] = result[i];
                }
                result = newResult;
            }

            return result;
        }

        static int[] convertToIntArray(string s)
        {
            char[] array = s.ToCharArray();
            int[] result = new int[array.Length];
            for(int i = 0; i < array.Length; i++)
            {
                result[i] = Int32.Parse(array[i].ToString());
            }
            return result;
        }

        static string convertToString(int[] array)
        {
            string result = "";
            bool clipped = false;
            for(int i = 0; i < array.Length; i++)
            {
                if (!clipped)
                {
                    if (array[i] != 0)
                    {
                        clipped = true;
                        result += array[i];
                    }
                }
                else
                {
                    result += array[i];
                }
            }
            return result;
        }


        static void printArray(int[] array)
        {
            foreach(int i in array)
            {
                Console.Write(i);
            }
            Console.WriteLine();
        }
    }
}
