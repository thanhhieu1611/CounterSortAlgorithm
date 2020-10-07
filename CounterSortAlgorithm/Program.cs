using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CounterSortAlgorithm
{
    class Program
    {
        #region MAIN
        static void Main()
        {
            //Number of arrays
            int myArrayNumber = 1000;

            //Number of integer in each array
            int myIntQuantity = 100000;

            //Pick max and min value of an Integer to prevent a new empty array from out of memory
            //since range = max-min is too big
            //If the range is so small, it will be more likely create same set of random number
            int myIntMax = 5000000;
            int myIntMin = -5000000;

            //Use Try-Catch to catch unusual errors
            try
            {
                checkSortedArrays(createRandomArrayList(myArrayNumber, myIntQuantity, myIntMax, myIntMin));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }

        #endregion

        #region FUNCTIONS
        //Counter Sort array
        static int[] counterSortArray(int[] intArray)
        {
            int[] rangeData = findMinMaxValues(intArray);

            //Check to see if all the elements in the provided array have the same value
            if (rangeData[0] == rangeData[1])
            {
                Console.WriteLine("Your array has been sorted with all same values!");
            }
            else
            {
                //Build an empty array
                int[] emptyArray = createEmptyArray(rangeData[0], rangeData[1]);

                //Count occurence of values in given array
                emptyArray = countOccurences(intArray, emptyArray, rangeData[0]);

                //Re-arrange the value in sorted order
                intArray = rearrangeArray(intArray, emptyArray, rangeData[0]);
            }
            return intArray;
        }

        //Find min value and max values of the provided array
        public static int[] findMinMaxValues(int[] myArray)
        {
            int min = myArray[0];
            int max = myArray[0];
            int[] temp = new int[2];
            int len = myArray.Length;
            for (var i = 1; i < len; i++)
            {
                if (myArray[i] < min)
                {
                    min = myArray[i];
                }
                else if (myArray[i] > max)
                {
                    max = myArray[i];
                }
            }

            temp[0] = min;
            temp[1] = max;

            return temp;
        }

        //Build an empty array
        static int[] createEmptyArray(int min, int max)
        {
            int len = max - min + 1;
            int[] temp = new int[len];
            //And numerical array will have default value of 0 for its elements
            //We don't need to loop through the array and then add 0 for its elements
            return temp;
        }

        //Count the occurence of each value
        static int[] countOccurences(int[] myArray, int[] emptyArray, int min)
        {
            int len = myArray.Length;
            for (var i = 0; i < len; i++)
            {
                emptyArray[myArray[i] - min] += 1;
            }
            return emptyArray;
        }

        //Re-arrange the value in sorted order
        static int[] rearrangeArray(int[] givenArray, int[] emptyArray, int min)
        {
            int len = emptyArray.Length;
            int indexCounter = 0;
            for (var i = 0; i < len; i++)
            {
                if (emptyArray[i] > 0)
                {
                    int counter = emptyArray[i];
                    int temp = i + min;
                    while (counter > 0)
                    {
                        givenArray[indexCounter] = temp;
                        indexCounter++;
                        counter--;
                    }
                }
            }
            return givenArray;
        }

        //return the sorted array
        static void printArray(int[] givenArray)
        {
            foreach (var i in givenArray)
            {
                
                Console.WriteLine(i);
            }
            Console.WriteLine("");
        }

        //return the list of array
        static void printListArray(List<int[]> arrList)
        {
            int len = arrList.Count;
            for (int i = 0; i < len; i++)
            {
                printArray(arrList[i]);
            }
            Console.WriteLine("");
        }

        //Create a list of arrays, with a number of random integers, which is in range of intMin and intMax
        static List<int[]> createRandomArrayList(int arrayNumber, int intQuantity, int intMax, int intMin)
        {

            List<int[]> arrList = new List<int[]>();
            for (var i = 0; i < arrayNumber; i++)
            {
                //create an array of 100,000 number, which is in range intMax and intMin
                //then add to its list
                arrList.Add(createRandomArray(intQuantity, intMax, intMin));
                
                if(i == arrayNumber - 1)
                {
                    Console.WriteLine("Print random arrays");
                    printListArray(arrList);
                }

                //Counter Sort arrayTemp
                arrList[i] = counterSortArray(arrList[i]);
            }

            Console.WriteLine("Print a sorted list array");
            printListArray(arrList);
            return arrList;
        }

        //create an array of 100,000000 number
        //If the range of the random value is small, 
        //it may return the same set of array for this function
        static int[] createRandomArray(int intQuantity, int intMax, int intMin)
        {
            //create an empty array with intQuantity elements
            int[] arrayTemp = new int[intQuantity];
            int intTemp;

            //Declare a random object
            var rand = new Random();

            for (var i = 0; i < intQuantity; i++)
            {
                //create a random integer, which need to be in a suitable range
                //which will prevent a new empty array from out of range (memory)
                //when create new empty array with length = max-min 
                //max, min are created by a random number.
                intTemp = rand.Next(intMin, intMax);

                //add to the temp array
                arrayTemp[i] = intTemp;
            }

            return arrayTemp;
        }

        //check the sorted arrays in array list
        static void checkSortedArrays(List<int[]> arrList)
        {
            foreach (int[] arrayElement in arrList)
            {
                int len = arrayElement.Length;
                for(int i = 1; i < len; i++)
                {
                    if(arrayElement[i] < arrayElement[i - 1])
                    {
                        Console.WriteLine("Sorting not correctly!");
                        return;
                    }
                }
                Console.WriteLine("");
            }
            Console.WriteLine("");
        }

        #endregion


    }
}
