using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testpad
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(134,23);
            int mass = 2000;
            Console.WriteLine("Creating array with "+mass+" random numbers.");
            Random rand = new Random();
            int[] random = new int[mass];
            DateTime start = DateTime.Now;
            for (int i = 0; i < mass; i++) random[i] = rand.Next(mass*-1, mass);
            long test0 = 0;
            for (int i = 0; i < random.Length; i++) test0 += random[i];
            Console.WriteLine("array created in: "+(DateTime.Now-start).ToString("s\\.ffff"));
            Console.WriteLine("Copying the array.");
            int[] test = new int[mass];
            start = DateTime.Now;
            for (int i = 0; i < mass; i++) test[i] = random[i];
            Console.WriteLine("Copy finished: "+(DateTime.Now-start).ToString("s\\.ffff"));
            long test1=0;
            for (int i = 0; i < test.Length; i++) test1 += test[i];
            Console.WriteLine("Bubblesort using \"foreach\": "+bubble1(test, "foreach"));
            for (int i = 0; i < mass; i++)  test[i] = random[i];
            Console.WriteLine("Bubblesort using \"for\": "+bubble1(test,"for"));
            long test2 = 0;
            for (int i = 0; i < test.Length; i++) test2 += test[i];
            Console.WriteLine("Bubblesort using \"while\": " + bubble1(test, "while"));
            for (int i = 0; i < mass; i++) test[i] = random[i];
            Console.WriteLine("Renosort using \"for\": " + bubble1(test, "Reno"));
            for (int i = 0; i < mass; i++) test[i] = random[i];
            long test3 = 0;
            List<int> errors1 = new List<int>();
            
            Console.WriteLine("Reno Multisort: " + bubble1(test, "Reno2"));
            for (int i = 0; i < test.Length; i++) test3 += test[i];
            Console.WriteLine("Original total of all numbers: " + test0);
            Console.WriteLine("Total after bubblesort: " + test1);
            if (test0.Equals(test1)) Console.WriteLine("Bubblesort total matches with original");
            else Console.WriteLine("Bubblesort total does not match original total");
            Console.WriteLine("Total after Renosort: " + test2);
            if (test0.Equals(test2)) Console.WriteLine("Renosort total matches with original.");
            else Console.WriteLine("Renosort total does not atch original");
            Console.WriteLine("Total after Renomultisort: " + test3);
            if (test0.Equals(test3)) Console.WriteLine("Renomultisort total matches original");
            else Console.WriteLine("Renomultisort total does not match original");
            if (doesincrement(test)) Console.WriteLine("Renomultisort result is sorted so numbers increment");
            else Console.WriteLine("Renomultisort does not increment propperly");
            Console.WriteLine("done");
            Console.ReadLine();
        }


        public static bool doesincrement (int[] test)
        {
            bool increments = true;
            for (int i = 0; i < test.Length - 1; i++) if (test[i] > test[i + 1]) increments = false;
            return increments;
        }


        public static string bubble1(int[] intarray,string method)
        {
            switch (method)
            {
                case "foreach":
                    bool changed = false;
                    DateTime start = DateTime.Now;
                    do
                    {
                        changed = false;
                        int i = 1;
                        foreach (int n in intarray)
                        {
                            if (i < intarray.LongLength && n > intarray[i])
                            {
                                int temp = n;
                                intarray[i - 1] = intarray[i];
                                intarray[i] = temp;
                                changed = true;
                            }
                            i++;
                        }
                    }
                    while (changed);
                    TimeSpan end = DateTime.Now - start;
                    return end.ToString("mm\\:ss\\.ffff");
                case "for":
                    start = DateTime.Now;
                    long arraylength = intarray.LongLength;
                    do
                    {
                        changed = false;
                         for (int n = 0; n < arraylength-1; n++)
                        {
                            if (intarray[n] > intarray[n+1])
                            {
                                int temp = intarray[n+1];
                                intarray[n+1] = intarray[n];
                                intarray[n] = temp;
                                changed = true;
                            }
                        }
                    }
                    while (changed);
                    end = DateTime.Now - start;
                    return end.ToString("mm\\:ss\\.ffff");
                case "while":
                    start = DateTime.Now;
                    arraylength = intarray.LongLength;
                    do
                    {
                        changed = false;
                        int n = 0;
                        do
                        {
                            if (intarray[n] > intarray[n + 1])
                            {
                                int temp = intarray[n + 1];
                                intarray[n + 1] = intarray[n];
                                intarray[n] = temp;
                                changed = true;
                            }
                            n++;
                        }
                        while (n < arraylength - 1);
                    }
                    while (changed);
                    end = DateTime.Now - start;
                    return end.ToString("mm\\:ss\\.ffff");
                case "Reno":
                    start = DateTime.Now;
                    arraylength = intarray.LongLength;
                    int last = 0;
                    int current;
                    Console.Write("Sorting: ");
                        for (int i = 0; i < arraylength-1; i++)
                        {
                            for (int n = i+1; n < arraylength; n++)
                            {
                                if (intarray[i]>intarray[n])
                                {
                                    int temp = intarray[n];
                                    intarray[n] = intarray[i];
                                    intarray[i] = temp;
                                }
                            }
                        if ((i * 100) / (intarray.Length - 2) != last || i == arraylength - 2) last = progressbar(i, last, intarray.Length-2);
                        }
                    return (DateTime.Now - start).ToString("mm\\:ss\\.ffff");
                case "Reno2":
                    Console.WriteLine("Starting renosort");
                    start = DateTime.Now;
                    int max = max = ((intarray.Length - 1) * 3) - 1;
                    last = 0;
                    int length = intarray.Length;
                    int depth = (int)Math.Sqrt(length)*3;
                    while (length % depth != 0) depth++;
                    int size;
                    if (intarray.Length % depth != 0) size = (intarray.Length / depth) + 1;
                    else size = intarray.Length / depth;
                    int[,] newarray = new int[depth+1, size];
                   // string[] beforesort = new string[size];
                   // string[] aftersort = new string[size];
                    Console.WriteLine("Created array with depth of " + depth + 1);
                    Console.Write("Sorting: ");
                    for (int y = 0; y < size; y++)
                    {
                        for (int x = 0; x < depth; x++)
                        {
                            if ((y * depth) + x < length) newarray[x, y] = intarray[(y * depth) + x];
                            int temp = (((y * depth) + x) * 100) / length - 1;
                            if ( (((y * depth) + x) * 100) / max != last) last = progressbar((y * depth) + x,last, max); 
                        }
                    }
                    int startingposition = (size*depth)-1;
               /*     for (int y = 0; y < size; y++)
                    {
                        for (int x = 0; x < depth; x++)
                        {
                            if (x < depth - 1) beforesort[y] += newarray[x, y].ToString() + ", ";
                            else beforesort[y] += newarray[x, y].ToString() + ".";
                        }
                    }*/
                    //last = 0;
                    //Console.Write("Step 2 of 3: ");
                    for (int y = 0; y < size; y++)
                    {
                        for (int i = 0; i < depth-1; i++)
                        {
                            for (int n = i + 1; n < depth; n++)
                            {
                                if (newarray[i,y] > newarray[n,y])
                                {
                                    int temp = newarray[n, y];
                                    newarray[n, y] = newarray[i, y];
                                    newarray[i, y] = temp;
                                }
                            }
                            if ((startingposition + ((y*depth)+i))*100 / max != last) last=progressbar(startingposition + ((y * depth) + i), last, max);
                        }
                    }
                    startingposition += (size * depth) - 2;
                 /*   for (int y = 0; y < size; y++)
                    {
                        for (int x = 0; x < depth; x++)
                        {
                            if (x < depth - 1) aftersort[y] += newarray[x, y].ToString() + ", ";
                            else aftersort[y] += newarray[x, y].ToString() + ".";
                        }
                    }*/
                    //last = 0;
                    int lowest;
                    bool skip = false;
                    int test = 0;
                    //Console.Write("Final step: ");
                    for (int i = 0; i < length; i++)
                    {
                        lowest = 0;
                        test = 1;
                        skip = false;
                        while (newarray[depth,lowest]==depth)
                        {
                            lowest++;
                            test++;
                        }
                        for (int y = test; y < size; y++)
                        {
                           // if (newarray[depth, y] > 0 && newarray[newarray[depth, y], y] < newarray[newarray[depth, y] - 1, y]) skip = true;
                            if (newarray[depth, lowest] == depth ||newarray[depth,y]==depth||( y == size - 1 && newarray[depth, y] >= (length - (length / size)))) ;
                            else if (!skip && newarray[newarray[depth, y], y] < newarray[newarray[depth, lowest], lowest]) lowest = y;
                        }
                        intarray[i] = newarray[newarray[depth, lowest], lowest];
                        newarray[depth, lowest]++;
                        if (( startingposition + i)*100 / max != last || i == length-1) last = progressbar(startingposition + i, last, max);
                    }
                    //File.WriteAllLines(Directory.GetCurrentDirectory() + "Before sort.txt", beforesort);
                    //File.WriteAllLines(Directory.GetCurrentDirectory() + "After sort.txt", aftersort);
                    return (DateTime.Now - start).ToString("mm\\:ss\\.ffff");
            }
            return "switch error";
        }
        public static int progressbar (int current,int prev, int max)
        {
            int percentage = (current * 100) / max ;
            if (current == max) Console.WriteLine(100);
            else if (percentage % 5 == 0 && percentage != 100) Console.Write(percentage);
            else  Console.Write("=");
            return percentage;
        }
    }
}
