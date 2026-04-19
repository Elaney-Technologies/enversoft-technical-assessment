using System;
using System.Linq; // Needed for Intersect(), Except(), and ToArray()

namespace ConsoleApplication1
{
    class Program
    {
        public static void Main(string[] args)
        {
            // Declare and initialize the first integer array
            int[] list1 = new int[] { 1, 2, 3, 4, 5 };

            // Declare and initialize the second integer array
            int[] list2 = new int[] { 3, 4, 5, 6, 7 };

            // Find common elements that appear in both list1 and list2
            // Intersect() compares both arrays and returns matching values
            int[] list3 = list1.Intersect(list2).ToArray();
            Console.WriteLine(string.Join(" ", list3));

            // Find elements that are in list1 but NOT in list2
            // Except() removes values found in list2 from list1
            int[] list4 = list1.Except(list2).ToArray();
            Console.WriteLine(string.Join(" ", list4));

            // Find elements that are in list2 but NOT in list1
            // Except() removes values found in list1 from list2
            int[] list5 = list2.Except(list1).ToArray();
            Console.WriteLine(string.Join(" ", list5));

            // Keep the console window open until Enter is pressed
            Console.WriteLine("Press <ENTER> to continue");
            Console.ReadLine();
        }
    }
}