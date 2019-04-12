using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler_Tests
{
    class Program
    {
        
        static void Main(string[] args)
        {
            // creating a new instance of Program, to avoid creating static test methods.
            new Program().Start();
        }

        // Filepath for the folder that should contain the data files.
        public readonly string FilePath = @"C:\Users\Anders\Desktop\IPC\Data\";
        public readonly string FileBaseName = "DataFile";
        public readonly string FileExtension = ".txt";

        public void Start()
        {
            Console.Write(" 1 - Dead Code Elimination \n 2 - Bubble Sort \n 3 - Generate Datafiles \n => ");
            string input = Console.ReadLine();
            switch (input.ToLower())
            {
                case "1":
                    //Dead Code Elimination
                    Test1(); 
                    break;

                case "2":
                    //BubbleSort Benchmark
                    Test2();
                    break;

                case "3":
                    DataGenerator();
                    break;

                default:
                    Console.WriteLine("Invalid command....");
                    break;
            }

            Console.WriteLine("End....");
            Console.ReadLine();
        }

        void Test1()
        {
            int count = 0;
            Timer t;

            while (count < 10)
            {
                t = new Timer();
                for (int i = 0; i < 10_000_000; i++)
                {
                    double dummy = Multiply(i);
                }
                t.Pause();

                count++;
                Console.WriteLine(t.Check() + "");
            }
        }

        double Multiply(int x)
        {
            x += x * x * x * x * x * x * x * x * x * x * x * x * x * x * x * x * x * x * x * x;

            return x;
        }

        List<int> Test2()
        {
            List<int> dataArray = new List<int>();
            bool dataFileExists = true;
            int fileNr = 1;
            Timer t;

            while (dataFileExists)
            {
                dataArray = FindDataFile(fileNr);
                

                if (dataArray.Count > 0)
                {
                    t = new Timer();
                    dataArray = BubbleSort(dataArray);
                    t.Pause();

                    Console.WriteLine("File nr " + fileNr + "  -  " + t.Check());
                }
                else
                {
                    dataFileExists = false;
                }

                fileNr++;
            }

            return dataArray;
        }

        List<int> FindDataFile(int fileNr)
        {
            // combines all parts of the file path to an absolute path.
            string fileName = FilePath + FileBaseName + fileNr + FileExtension;

            if (File.Exists(fileName))
            {
                List<int> dataArray = new List<int>();
                string[] fileLines = File.ReadAllLines(fileName);

                foreach (string item in fileLines)
                {
                    string s = item.Split(',').First();
                    int nr;

                    if (int.TryParse(s, out nr))
                    {
                        dataArray.Add(nr);
                    }
                    else
                        Console.WriteLine("Error converting " + s + " to an integer.");
                }

                return dataArray;
            }
            else
            {
                return new List<int>();
            }
        }

        List<int> BubbleSort(List<int> A)
        {
            bool swapped = true;
            int temp, length;

            length = A.Count;

            while (swapped)
            {
                swapped = false;

                for (int i = 0; i < length-1; i++)
                {
                    if (A[i] > A[i+1])
                    {
                        temp    = A[i];
                        A[i]    = A[i+1];
                        A[i+1]  = temp;
                        swapped = true;
                    }
                }
            }

            return A;
        }

        void DataGenerator()
        {
            Random rng = new Random();

            // The amount files created.
            int nrOfFiles = 5;

            // The number of integers in the file.
            int listSize = 20_000;

            // The largest number which can be created. Should not be lower than 0.
            int maxNumber = Int32.MaxValue;

            // Repeats for every file that should be created.
            for (int i = 1; i <= nrOfFiles ; i++)
            {
                // combining filepath and base name with a nr descriptor and an extension.
                string fileName = FilePath + FileBaseName + i + FileExtension;

                List<string> data = new List<string>();

                // check and see if file exists.
                if (!File.Exists(fileName))
                {
                    for (int j = 0; j < listSize; j++)
                    {
                        data.Add(rng.Next(0, maxNumber) + ",");
                    }

                    File.AppendAllLines(fileName, data);
                }
                else
                    Console.WriteLine("Error: File exists. Remove files from folder or change FileBaseName");
                
            }
        }
    }

    public class Timer
    {
        private readonly System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch(); 
        public Timer()
        {
            Play();
        }
        public double Check()
        {
            return stopwatch.ElapsedMilliseconds / 1000.0;
        }
        public void Pause()
        {
            stopwatch.Stop();
        }
        public void Play()
        {
            stopwatch.Start();
        }
    }

}
