using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCDclock
{
    class Program
    {
        static int[,] numbers = {
            { 1, 3, 0, 3, 1}, //0
            { 0, 2, 0, 2, 0}, //1
            { 1, 2, 1, 1, 1}, //2
            { 1, 2, 1, 2, 1}, //3
            { 0, 3, 1, 2, 0}, //4
            { 1, 1, 1, 2, 1}, //5
            { 1, 1, 1, 3, 1}, //6
            { 1, 2, 0, 2, 0}, //7
            { 1, 3, 1, 3, 1}, //8
            { 1, 3, 1, 2, 1}, //9
            { 1, 3, 1, 3, 0}, //A
            { 1, 3, 1, 1, 0}, //P
            { 0, 0, 0, 0, 0}};//:
        static int line = 0;
        static string[] ORDER = { "Horizontal", "Vertical", "Horizontal", "Vertical", "Horizontal", "Ready"};
        StringBuilder[] result;

        static void Main(string[] args)
        {
            Program lcd = new Program();            
            lcd.readInArguments(args);
            //Console.ReadLine();
        }

        public string horizontal(int number, int size)
        {
            switch (number)
            {
                case 0:
                    return " " + repeat(" ",size) + " " + " ";
                case 1:
                    return " " + repeat("-",size) + " " + " ";
                default:
                    return "Error";
            }
        }
        public string vertical(int number, int size)
        {
            switch (number)
            {
                case 0:
                    return " " + repeat("-",size) + " " + " ";
                case 1:
                    return "|" + repeat(" ",size) + " " + " ";
                case 2:
                    return " " + repeat(" ",size) + "|" + " ";
                case 3:
                    return "|" + repeat(" ",size) + "|" + " ";
                default:
                    return "Error";
            }
        }
        public string repeat(string sample, int size)
        {
            string replacement = "";
            for(int i =0; i < size; i++)
            {
                replacement += sample;
            }
            return replacement;
        }
        public int parseIntNumber(string timeSample)
        {
            switch (timeSample)
            {
                case "A":
                    return 10;
                case "P":
                    return 11;
                case ":":
                    return 12;
                default:
                    return int.Parse(timeSample); 
            }
        }

        public void printNumberHorizontal(string time, int ord, int size, int line)
        {
            for (int i = 0; i < time.Length; i++)
            {
                int number = parseIntNumber(time.Substring(i,1));
                string temp = horizontal(numbers[number, ord], size);
                result[line] = new StringBuilder();
                result[line].Append(temp);
                Console.Write(result[line]);
            }
        }

        public void printNumberVertical(string time, int ord, int size, int line)
        {
            for (int i = 0; i < time.Length; i++)
            {
                int number = parseIntNumber(time.Substring(i, 1));
                string temp = vertical(numbers[number, ord], size);
                result[line] = new StringBuilder();
                result[line].Append(temp);
                Console.Write(result[line]);
            }
        }

        public void printClock(string time, int size)
        {
            for (int ord = 0; ord < ORDER.Length; ord++)
            {
                switch (ORDER[ord])
                {
                    case "Horizontal":
                        printNumberHorizontal(time, ord, size, line);
                        Console.Write("\n");
                        line++;
                        break;
                    case "Vertical":
                        for (int i = 0; i < size; i++)
                        {
                            printNumberVertical(time, ord, size, line);
                            Console.Write("\n");
                            line++;
                        }
                        break;
                    case "Ready":
                        return;
                }
            }
        }
        public void readInArguments(string[] args)
        {
            string time = string.Format("{0:HH:mm}", DateTime.Now);
            int size = 2;
            int hours = int.Parse(time.Substring(0, 2));
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].Equals("-s"))
                {
                    if (int.Parse(args[i + 1]) < 1 || int.Parse(args[i + 1]) > 5)
                    {
                        Console.WriteLine("Please enter a size between 1 and 5, the default sized clock is now displayed");
                    }
                    else
                    {
                        size = int.Parse(args[i + 1]);
                    }
                }
                else if (args[i].Equals("-12"))
                {
                    if (hours >= 12)
                    {
                        hours = hours - 12;
                        if (hours < 10)
                        {
                            time = "P" + 0 + hours + time.Substring(2, 3);
                        }
                        else
                        {
                            time = "P" + hours + time.Substring(2, 3);
                        }
                    }
                    else
                    {
                        time = "A" + time;
                    }
                }
            }
            result = new StringBuilder[3 + 2 * size];
            printClock(time, size);
        }

    }
}


