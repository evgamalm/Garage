using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage
{
    public class Handler
    {
        private Garage<Vehicle> garage;
        private uint capacity;
        private readonly ConsoleUI ui;



        public Handler(uint capacity, ConsoleUI ui)
        {
            garage = new Garage<Vehicle>(capacity);
            this.ui = ui;
        }

        private IEnumerable<Vehicle> GetDummyVehicles()
        {
            return new List<Vehicle>()
            {
                new Car("White", "ABC123", 4,34, FuelType.Diesel),
                new Bus("Red", "KLM187", 6, 8, 20),
                new Motorcycle("Black", "JKL987", 2, 4, 1.1),
                new Airplane("White", "HUM372", 6, 18, 4),
                new Boat("Green", "LKJ764", 0, 12, 3.4),
                new Car("Black", "DFE321", 4, 6, FuelType.electric)
            };
        }

        internal void SeedVehicles()
        {
            Console.Clear();
            var vehicles = GetDummyVehicles();

            foreach (var vehicle in vehicles)
            {
                garage.Park(vehicle);
            }
        }

        public void AddVehicle()
        {
            Console.WriteLine("Park your vehicle: ");
            try
            {
                if (garage is null)
                    throw new InvalidOperationException("Garage is not available. ");

                Console.WriteLine("Please enter your Vehicle Type ");
                var vehicleType = Console.ReadLine();

                Vehicle? vehicle = null;


                switch (vehicleType)
                {
                    case "Bus":
                        var (BusColour, busRegistrationNumber, nrwheels, busCylinderVolume) = GetCommons("Bus");
                        vehicle = new Bus(BusColour, busRegistrationNumber, nrwheels, busCylinderVolume, amountNumber());

                        break;
                    case "Boat":
                        var (BoatColour, BoatRegistrationnumber, BoatNrWheels, BoatCylinderVolume) = GetCommons("Boat");
                        vehicle = new Boat(BoatColour, BoatRegistrationnumber, BoatNrWheels, BoatCylinderVolume, VehicleLength());
                        break;

                    case "Motorcycle":
                        var (MotorCycleColour, MotorCycleRegistrationNumber, MotorCycleWheels, MotorCycleCylinderVolume) = GetCommons("Motorcycle");
                        vehicle = new Motorcycle(MotorCycleColour, MotorCycleRegistrationNumber, MotorCycleWheels, MotorCycleCylinderVolume, VehicleLength());
                        break;


                    case "Airplane":

                        var (AirplaneColour, AirplaneRegistrationNumber, AirplaneWheels, AirplaneCylinderVolume) = GetCommons("Airplane");
                        vehicle = new Airplane(AirplaneColour, AirplaneRegistrationNumber, AirplaneWheels, AirplaneCylinderVolume, amountUint());
                        break;


                    case "Car":

                        var (CarColour, CarRegistrationNumber, CarAmountOfWheels, CarCylinderVolume) = GetCommons("Car");

                        ConsoleKeyInfo input;
                        FuelType fuelType;
                        VehicleFuelType(out input, out fuelType);

                        switch (input.Key)
                        {
                            case ConsoleKey.D1:
                            case ConsoleKey.NumPad1:
                                fuelType = FuelType.Gasoline;
                                break;
                            case ConsoleKey.D2:
                            case ConsoleKey.NumPad2:
                                fuelType = FuelType.Diesel;
                                break;
                            case ConsoleKey.D3:
                            case ConsoleKey.NumPad3:
                                fuelType = FuelType.electric;
                                break;
                            default:
                                throw new ArgumentException("Wrong type. Please, re-consider your input.");
                                break;

                        }
                        vehicle = new Car(CarColour, CarRegistrationNumber, CarAmountOfWheels, CarCylinderVolume, fuelType);
                        break;
                }
                if (!garage.AddVehicle(vehicle))
                    Console.WriteLine("Garage is full. Cannot park vehicle. ");
            }
            catch
            {
                throw new ArgumentException("Wrong input. Please Try again: ");
            }

        }

        private static void VehicleFuelType(out ConsoleKeyInfo input, out FuelType fuelType)
        {
            Console.WriteLine($"Please Select: \n 1. Gasoline \n 2. Diesel \n 3. Electric ");
            input = Console.ReadKey();
            fuelType = FuelType.Gasoline;
        }

        public uint amountUint()
        {
            ui.Print("Enter total amount of engines: ");
            var totalAmount = ui.ReadString();
            UInt32.TryParse(totalAmount, out uint totalAmountResult);
            return totalAmountResult;
        }

        public uint amountNumber()
        {
            ui.Print($"Enter your total seats for your: ");
            var amountNumber = Console.ReadLine();
            UInt32.TryParse(amountNumber, out UInt32 amountNumberResult);
            return amountNumberResult;
        }
        private static double VehicleLength()
        {
            Console.WriteLine("Enter your Length for your Boat: ");
            var VehicleLength = Console.ReadLine();
            double.TryParse(VehicleLength, out double VehicleLengthResult);
            return VehicleLengthResult;
        }

        private (string vehicleColor, string vehicleRegistrationNumber, uint nrwheels, uint CylinderVolume) GetCommons(string vehicleType)
        {
            ui.Print($"Please Enter your colour for your {vehicleType}: ");
            string vehicleColour = ReadInput();

            ui.Print($"Please enter your registration number for your {vehicleType}");
            string vehicleRegistrationNumber = ReadInput();

            ui.Print($"Please, enter amount of wheels your {vehicleType} have ");
            string amount = ReadInput();
            uint amountResult = ParseStringToUInt(amount);

            ui.Print($"Please, enter amount of Cylinder volume the {vehicleType} has ");
            string CylinderVolume = ReadInput();
            uint CylinderVolumeResult = ParseStringToUInt(CylinderVolume);

            return (vehicleColour, vehicleRegistrationNumber, amountResult, CylinderVolumeResult);

        }

        private uint ParseStringToUInt(string amount)
        {
            UInt32.TryParse(amount, out uint amountResult);
            return amountResult;
        }

        private static string ReadInput()
        {
            return Console.ReadLine()!;
        }

        private Vehicle? SearchVehicleRegNr()
        {
            Console.WriteLine("Enter your Registration Number to search: ");
            var registrationNumber = Console.ReadLine();
            var vehicle = garage?.FirstOrDefault(v => v.LicenseNumber.ToUpper().Equals(registrationNumber.ToUpper()));

            return vehicle;
        }

        public void DeleteVehicle()
        {
            try
            {
                if (garage is null)
                    throw new ArgumentNullException("Garage not created. ");

                Console.WriteLine("UnPark your vehicle with Registraton number: ");
                var vehicle = SearchVehicleRegNr();

                if (vehicle != null)
                {
                    garage.removeVehicle(vehicle);
                    ui.Print($"You have successfully unparked your vehicle: {vehicle.LicenseNumber} ");
                }
                else
                {
                    ui.Print("Couldn't park the Vehicle. The following Registration number doesn't exist. ");

                }
            }

            catch (InvalidOperationException)
            {
                Console.WriteLine("invalid. Please, try again. ");
            }
        }

        public void ListAllVehicles()
        {
            ui.Print("Print all Vehicles in garage ");

            try
            {
                if (garage is null)
                    throw new InvalidOperationException("Failed. try again!");

                foreach (Vehicle v in garage)
                    ui.Print(v.ToString());
            }


            catch (InvalidOperationException)
            {
                ui.Print(" Invalid, Please try again ");
            }
        }

        public void CreateGarage()
        {
            ui.Print("Please, enter to create your new garage: ");
            var capacity = ui.ReadUInt();
            garage = new Garage<Vehicle>(capacity);
            ui.Print("Garage has been created! ");
            ui.Print("The old one has been deleted. ");
        }

        

        public void ParkVehicles(Vehicle vehicle)
        {
            ui.Print("Please, enter to park your vehicle: ");
            Console.ReadLine();

            if (garage.Park(vehicle))
            {
                ui.Print($"You have been successfully parked your vehicle;  {vehicle.LicenseNumber}");
            }
            else
            {
                ui.Print($" Garage is full. {vehicle.LicenseNumber}");
            }

        }
        public List<Vehicle> SearchVehicles(uint amountWheels, string colour = "", string vehicleType = "")
        {
            IEnumerable<Vehicle> query = garage;
            ArgumentNullException.ThrowIfNull(colour);

            if (!string.IsNullOrWhiteSpace(colour))
            {
                query = query.Where(v => v.Color.ToLower() == colour.ToLower());
                foreach (var vehicle in query)
                {
                    Console.WriteLine("Before:" + garage.Count());
                    Console.WriteLine(vehicle.Color);
                }
            }
            else if (amountWheels > 1)
            {
                query = query.Where(v => v.AmountWheels == amountWheels);

                foreach (var vehicle in query)
                {
                    Console.WriteLine(vehicle);
                }
            }
            else if (vehicleType == "Car")
            {
                query = query.Where(v => v.LicenseNumber == vehicleType);
                foreach (var vehicle in query)
                {
                    Console.WriteLine(vehicle);
                }
            }
            return query.ToList();
        }

        public void ListTypesInVehicles()
        {

            try
            {
                ui.Print("Please, enter the desired type to print it: ");

                if (garage is null)
                    throw new InvalidOperationException("Garage has not been created. Please re-consider your input. ");
                string colour;
                string amountWheels;
                string vehicleType;
                string pressed;


                ui.Print("1./ Black Cars");
                ui.Print("2./ All Vehicles with 4 or more Wheels");
                ui.Print("3./ All Airplanes: ");
                pressed = Console.ReadLine();
                uint.TryParse(pressed, out uint pressedResult);

                if (pressedResult == 1)
                {
                    foreach (var vehicle in garage)
                    {
                        if (vehicle.GetType().Name == "Car")
                        {
                            Console.WriteLine(vehicle);
                        }
                        else
                            Console.WriteLine("Wrong type, Try again");

                    }
                }

                else if (pressedResult == 2)
                {
                    foreach (var vehicle in garage)
                    {
                        if (vehicle.AmountWheels > 4)
                        {
                            Console.WriteLine(vehicle.AmountWheels);
                        }
                    }
                }

                else if (pressedResult == 3)
                {
                    foreach (var vehicle in garage)
                    {
                        if (vehicle.Name == "Airplane")
                        {
                            ui.Print(vehicle.Name);
                        }

                    }
                }
                else
                    ui.Print("Wrong. Please re-enter 1 - 3. ");
                return;
            }
            catch
            {
                throw new ArgumentException();
            }
            
        }

        public void UnParkVehicle(string registerNumber)
        {
            ui.Print("Please, enter your desired vehicle to unpark it: ");

            if (garage.UnPark(registerNumber))
            {
                Console.WriteLine($"You have been successfully parked your vehicle. {registerNumber}");
            }
            else
            {
                Console.WriteLine($"Unable to park vehicle: {registerNumber}");
            }
        }
        public void Exit()
        {
            ui.Print("Exiting program");
            ui.Print("3...2.....1....");
            Environment.Exit(0);
        }
    }
}
