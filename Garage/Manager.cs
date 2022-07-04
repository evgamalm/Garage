using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage
{
    internal class Manager
    {
        private readonly ConsoleUI ui;
        private Handler handler;

        public Manager()
        {
            ui = new ConsoleUI();
        }

        internal void Start()
        {
            ConsoleUI ui = new();

            
            uint capacity;
            ui.Print("How long is your garage?");
            var size = ui.AskForUInt();
            
            handler = new Handler(size, ui);

            

            ShowMainMeny();

        }
        private void ShowMainMeny()
        {
            bool running = true;

            while (running!)
            {
                ui.ShowMenu();
                string option;
                option = ui.ReadString();
                UInt32.TryParse(option, out uint result);

                switch (option)
                {
                    case "0":
                        handler.Exit();
                        ui.Sleep();
                        break;
                    case "1":
                        handler.CreateGarage();
                        break;
                    case "2":
                        handler.SeedVehicles();
                        handler.ListAllVehicles();
                        break;
                    case "3":
                        handler.ListTypesInVehicles();
                        break;
                    case "4":
                        handler.AddVehicle();
                        break;
                    case "5":
                        handler.DeleteVehicle();
                        break;
                    case "6":
                        handler.ListTypesInVehicles();
                        break;
                    default:
                        ui.ColorRed();
                        ui.Print("Wrong input. Pleaase re-consider your input again.");
                        Console.ResetColor();
                        break;

                }


            }
        }
    }
}
