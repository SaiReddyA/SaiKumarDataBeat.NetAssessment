using System;

namespace SaiKumarDataBeat
{
    using System;
    using System.Linq;

    public class MalwareSimulator
    {
        //Behaviour of malwares
        public static int[] Simulate(int[] input)
        {
            int[] result = new int[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                if (ShouldReplace(input, i))
                {
                    result[i] = 0;
                }
                else
                {
                    result[i] = input[i];
                }
            }

            return result;
        }

        //checking the current position to replace with 0
        private static bool ShouldReplace(int[] arr, int position)
        {
            if (position < 0 || position >= arr.Length || arr[position] == 0)
            {
                return false;
            }

            int leftNeighbor = GetNeighbor(arr, position, -1);
            int rightNeighbor = GetNeighbor(arr, position, 1);

            return (leftNeighbor != -1 && arr[position] <= arr[leftNeighbor]) ||
                   (rightNeighbor != -1 && arr[position] <= arr[rightNeighbor]);
        }

        //checking the neighbor
        private static int GetNeighbor(int[] arr, int position, int direction)
        {
            for (int i = position + direction; i >= 0 && i < arr.Length; i += direction)
            {
                if (arr[i] != 0)
                {
                    return i;
                }
            }
            return -1;
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Enter the numbers separated by commas (e.g., 19,2,0,87,1,40,80,77,77,77,77):");
            string inputStr = Console.ReadLine();

            int[] input = inputStr.Split(',').Select(int.Parse).ToArray();
            int[] result = Simulate(input);

            Console.WriteLine("Input array: " + string.Join(", ", input));
            Console.WriteLine("Result array: " + string.Join(", ", result));
        }
    }

}
