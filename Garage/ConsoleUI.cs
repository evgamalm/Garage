using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage
{
    
    public class ConsoleUI //: IUI
    {


        public string AskForString(string prompt)
        {
            bool running = false;
            string name;

            do
            {
                Print($"{prompt}");
                name = prompt;

                if (string.IsNullOrWhiteSpace(name))
                {
                    Print($"You must enter an valid {prompt}");
                }
                else
                {
                    running = true;
                }

            } while (!running);
            return name;
        }

        public string ReadString() => Console.ReadLine();
        public uint AskForUInt()
        {
            var input = ReadString();
            if (uint.TryParse(input, out uint result))
                return result;
            else
                throw new ArgumentException("Wrong number. Needs to be 'Uint'. "); ;
        }
        public uint ReadUInt()
        {
            var input = ReadString();
            if (uint.TryParse(input, out uint result))
                return result;
            else
                throw new ArgumentException("Wrong input. Please try again. ");
        }
        public string GetInt()
        {
            throw new NotImplementedException();
        }

        public void WaitForUser()
        {
            Print("Press something to continue. ");
            ReadString();

        }

        public string GetString()
        {
            throw new NotImplementedException();
        }
        public void PrintMessage(string message)
        {
            throw new NotImplementedException();
        }
        public void Print(string printedtext) => Console.WriteLine(printedtext);
        public void Sleep() => Thread.Sleep(1000);
        public void ColorRed() => Console.ForegroundColor = ConsoleColor.Red;
        public void ShowMenu()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("*******WELCOME TO THE GARAGE*******");
            Console.WriteLine("Type '0' to quit the program. ");
            Console.WriteLine("Type '1' to generate a new garage. ");
            Console.WriteLine("Type '2' to list all parked Vehicles ");
            Console.WriteLine("Type '3' to list different vehicles parked in the garage ");
            Console.WriteLine("Type '4' in the console to park your Vehicle ");
            Console.WriteLine("Type '5' to unpark your vehicle from the garage ");
            Console.WriteLine("Type '6' to find the specific vehicle you are looking for ");
            Console.ResetColor();

        }
    }
 
}

