using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage
{
    public abstract class Vehicle
    {
        public abstract string Name { get; }
        public string Color { get; set; }

        public string LicenseNumber { get; set; }

        public uint AmountWheels { get; set; }

        public uint CyliderVolume { get; set; }

        public Vehicle(string color, string licenseNumber, uint amountWheels, uint cyliderVolume)
        {
            Color = color;
            LicenseNumber = licenseNumber;
            AmountWheels = amountWheels;
            CyliderVolume = cyliderVolume;
        }

        public override string ToString()
        {
            return base.ToString() + $"Vehicle type: {Name}\n Color: {Color}\n Registration Number: {LicenseNumber}\n Amount of Wheels: {AmountWheels}\n Amount of Cylinder Volume: V{CyliderVolume}\n";
        }
    }
}
