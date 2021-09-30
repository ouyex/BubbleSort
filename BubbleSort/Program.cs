using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BubbleSort
{
    class Program
    {
        static int sortPasses = 0;
        static System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();

        static string sortedArrayString = "";
        static string unsortedArrayString = "";
        
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Title = "Bubble Sort (WARNING)";
            Console.WriteLine("WARNING: This program uses the bubble sort algorithm.");
            Console.WriteLine("Although bubble sort is great to visualize and understand, it is extrememly slow at scale.");
            Console.WriteLine("Values that are too large will take longer than the lifetime of the universe to calculate.");
            Console.WriteLine("Array sizes of less than 100,000 are recommended.");
            Console.WriteLine();
            Console.WriteLine("Big O Complexity of O(n^2) in worst case scenarios and O(n) in best case scenarios.");
            Console.WriteLine();
            Console.WriteLine("Press any key if you understand the above message.");
            Console.ReadKey();
            Console.ForegroundColor = ConsoleColor.Gray;

            Console.Clear();
            Console.Title = "Bubble Sort";

            int inputLength = 0;

            while (inputLength <= 1)
            {
                Console.WriteLine("Please input array size:");
                
                try
                {
                    inputLength = int.Parse(Console.ReadLine());

                    if (inputLength <= 1)
                    {
                        Console.Clear();
                        Console.WriteLine("Please input a size of at least 2.");
                    }
                }
                catch (FormatException)
                {
                    Console.Clear();
                    Console.WriteLine("Please only input numbers.");
                }
                catch (ArgumentNullException)
                {
                    Console.Clear();
                    Console.WriteLine("Please enter a number.");
                }
            }

            if (inputLength > 200000)
            {
                string savedTitle = Console.Title;
                Console.Title = "Bubble Sort (WARNING)";
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("WARING: You have entered a value higher than 200,000 (" + inputLength + ").");
                Console.WriteLine("Values this high may take an extremely long amount of time,");
                Console.WriteLine("please refer to the README included with this program for time references.");
                Console.WriteLine();
                Console.WriteLine("[] Press \"Y\" to continue.");
                Console.WriteLine("[] Press any other key to cancel and restart.");

                if (Console.ReadKey().KeyChar != 'y')
                {
                    Restart();
                    return;
                }

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Title = savedTitle;
            }

            Console.Clear();
            Console.WriteLine("Entered size: " + inputLength);

            int[] unsortedArray = new int[inputLength];
            int[] sortedArray = {};

            Console.WriteLine();
            Console.WriteLine("This program randomly generates values to hold in the array, please enter the range for these values.");
            Console.WriteLine();
            Console.WriteLine("Minimum value: ");
            int minVal = int.Parse(Console.ReadLine());
            Console.WriteLine("Maximum value: ");
            int maxVal = int.Parse(Console.ReadLine());

            Console.WriteLine();
            Console.WriteLine("Populating Array...");
            FillArray(ref unsortedArray, minVal, maxVal);
            Console.Clear();
            Console.WriteLine("Sorting Array...");

            watch.Start();
            BubbleSort(unsortedArray, ref sortedArray);
            watch.Stop();

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Sorted " + sortedArray.Length + " items in " + watch.ElapsedMilliseconds + "ms with " + sortPasses + " total passes.");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();

            // Get sorted array indicies
            while (true)
            {
                Console.WriteLine("__________");
                Console.WriteLine("[] Enter a number to get the value of that index (max index: " + (sortedArray.Length - 1) + ").");
                Console.WriteLine();
                Console.WriteLine("[] Enter \"force_fetch\" to manually fetch strings of all arrays and store it in memory.");
                Console.WriteLine("[] Enter \"force_unfetch\" to manually unfetch all array strings (will NOT free memory, used for debug only).");
                Console.WriteLine();
                Console.WriteLine("[] Enter \"print_sort\" to print the sorted array.");
                Console.WriteLine("[] Enter \"print_unsort\" to print the unsorted array.");
                Console.WriteLine();
                Console.WriteLine("[] Enter \"write_sort\" to output the sorted array to a file (placed next to exe).");
                Console.WriteLine("[] Enter \"write_unsort\" to output the unsorted array to a file (placed next to exe).");
                Console.WriteLine();
                Console.WriteLine("[] Enter \"o\" to open data location.");
                Console.WriteLine("[] Enter \"r\" to restart.");
                Console.WriteLine("[] Enter \"q\" to quit.");
                Console.WriteLine("__________");

                int input = 0;

                try
                {
                    string strInput = Console.ReadLine();
                    Console.Clear();

                    switch (strInput)
                    {
                        case "force_fetch":
                            ArrayToString(sortedArray, true);
                            ArrayToString(unsortedArray, false);
                            Console.Clear();
                            Console.WriteLine("Array strings fetched.");
                            break;
                        case "force_unfetch":
                            sortedArrayString = "";
                            unsortedArrayString = "";
                            Console.Clear();
                            Console.WriteLine("Array strings unfetched.");
                            continue;
                        case "print_sort":
                            Console.SetCursorPosition(0, 0);
                            Console.WriteLine(ArrayToString(sortedArray, true));
                            Console.WriteLine();
                            Console.WriteLine("Press enter to clear.");
                            Console.WriteLine();
                            continue;
                        case "print_unsort":
                            Console.WriteLine(ArrayToString(unsortedArray, false));
                            Console.WriteLine();
                            Console.WriteLine("Press enter to clear.");
                            Console.WriteLine();
                            continue;
                        case "write_sort":
                            Console.Clear();
                            WriteArray(sortedArray, "sorted.txt", true);
                            Console.Clear();
                            Console.WriteLine("Finished writing to sorted.txt");
                            continue;
                        case "write_unsort":
                            Console.Clear();
                            WriteArray(unsortedArray, "unsorted.txt", false);
                            Console.Clear();
                            Console.WriteLine("Finished writing to unsorted.txt");
                            continue;
                        case "o":
                            ProcessStartInfo startInfo = new ProcessStartInfo
                            {
                                Arguments = Directory.GetCurrentDirectory(),
                                FileName = "explorer.exe",
                            };

                            Process.Start(startInfo);
                            continue;
                        case "r":
                            Restart();
                            return;
                        case "q":
                            return;
                        case "":
                            continue;
                        default:
                            input = int.Parse(strInput);
                            if (input < sortedArray.Length && input >= 0)
                                Console.WriteLine("Array index at [" + input + "] is " + sortedArray[input]);
                            else
                                Console.WriteLine("Please enter a valid number within the array range.");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please enter a valid option.");
                    continue;
                };

            }
        }

        static void Restart()
        {
            var filePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            System.Diagnostics.Process.Start(filePath);
        }

        static void BubbleSort(int[] _unsortedArray, ref int[] storeLocation)
        {
            string savedTitle = Console.Title;
            int[] workingArray = new int[_unsortedArray.Length];
            _unsortedArray.CopyTo(workingArray, 0);
            bool isSorted = false;

            int maxSorted = 0;
            sortPasses = 0;
            
            var timeStart = watch.ElapsedMilliseconds;

            int millisecondsBetweenTitleUpdates = 400;

            Console.Clear();

            while (!isSorted)
            {
                // Check if array is sorted
                for (int i = 0; i < workingArray.Length - 1; i++)
                {
                    if (i > maxSorted)
                    {
                        maxSorted = i;
                    }

                    if (workingArray[i] > workingArray[i + 1])
                    {
                        isSorted = false;
                        break;
                    }
                    else
                    {
                        isSorted = true;
                    }
                }

                if (watch.ElapsedMilliseconds - timeStart > millisecondsBetweenTitleUpdates)
                {
                    timeStart = watch.ElapsedMilliseconds;
                    string percetageComplete = (sortPasses / (float)workingArray.Length * 100).ToString();

                    if (percetageComplete.Length >= 2)
                    {
                        string percetageCompleteString = percetageComplete.Substring(0, percetageComplete.IndexOf('.') + 2) + "%";
                        Console.SetCursorPosition(0, 0);
                        Console.WriteLine("Bubble sorting " + workingArray.Length + " values.");
                        Console.WriteLine();
                        Console.WriteLine("Estimated Percentage: " + percetageCompleteString);
                        Console.WriteLine("Elapsed Time: " + watch.ElapsedMilliseconds / 1000 + " seconds");
                        Console.WriteLine("Elapsed Passes: " + sortPasses);

                        Console.Title = ("Bubble Sort: " + percetageCompleteString);
                    }
                }


                // Sort
                for (int i = 0; i < workingArray.Length - 1; i++)
                {
                    if (workingArray[i] > workingArray[i + 1])
                    {
                        int small = workingArray[i + 1];
                        int big = workingArray[i];

                        workingArray[i] = small;
                        workingArray[i + 1] = big;
                    }
                }

                sortPasses++;
            }

            Array.Resize(ref storeLocation, workingArray.Length);
            workingArray.CopyTo(storeLocation, 0);

            Console.Title = savedTitle;
        }

        static string ArrayToString(int[] array, bool usingSortedArray)
        {
            if (usingSortedArray && sortedArrayString != "")
                return sortedArrayString;
            else if (!usingSortedArray && unsortedArrayString != "")
                return unsortedArrayString;
            else
            {
                Console.Clear();
            }

            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            int timeBetweenTitleUpdates = 100;
            var timeStart = watch.ElapsedMilliseconds;

            var returnString = new StringBuilder();

            returnString.Append("{");

            Console.Clear();

            for (int i = 0; i < array.Length; i++)
            {
                returnString.Append(array[i]);

                if (watch.ElapsedMilliseconds - timeStart > timeBetweenTitleUpdates)
                {
                    Console.SetCursorPosition(0, 0);

                    if (usingSortedArray)
                        Console.WriteLine("Fetching sorted array string for the first time, this may take a while...");
                    else
                        Console.WriteLine("Fetching unsorted array string for the first time, this may take a while...");

                    string percentage = (i / (float)array.Length * 100).ToString();
                    
                    if (percentage.Length >= 2)
                        Console.WriteLine(percentage.Substring(0, 2) + "%");
                    
                    timeStart = watch.ElapsedMilliseconds;
                }

                if (i != array.Length - 1)
                    returnString.Append(", ");
            }

            returnString.Append("}");

            if (usingSortedArray)
                sortedArrayString = returnString.ToString();
            else if (!usingSortedArray)
                unsortedArrayString = returnString.ToString();

            return returnString.ToString();
        }

        static void FillArray(ref int[] array, int min, int max)
        {
            Random rand = new Random();

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rand.Next(min, max);
            }
        }

        static void WriteArray(int[] array, string fileName, bool usingSortedArray)
        {
            File.WriteAllText(fileName, ArrayToString(array, usingSortedArray));

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                Arguments = Directory.GetCurrentDirectory() + "\\" + fileName,
                FileName = "explorer.exe",
            };

            Process.Start(startInfo);
        }
    }
}
