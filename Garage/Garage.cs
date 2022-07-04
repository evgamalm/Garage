using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage
{
    class Garage<T> : IEnumerable<T> where T : Vehicle
    {

        private T[] vehicles;
        public Garage(uint capacity)
        {
            
            vehicles = new T[capacity];
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in vehicles)
            {
                if (item != null)
                    yield return item;

            }
            yield break;
        }
        internal bool AddVehicle(T newVehicle)
        {
            bool full = vehicles.All(v => v != null);
            if (full) return false;

            var firstNullElementIndex = vehicles.ToList().IndexOf(vehicles.First(v => v is null));

            vehicles[firstNullElementIndex] = newVehicle;
            return true;

        }

        internal void removeVehicle(T vehicle)
        {
            var vehicleIndex = vehicles.ToList().IndexOf(vehicle);

            if (vehicleIndex != -1)
            {
                vehicles[vehicleIndex] = null;
            }
        }

        public bool UnPark(string registerNumber)
        {
            for (int i = 0; i < vehicles.Length; i++)
            {
                if (vehicles[i].LicenseNumber == registerNumber)
                {
                    vehicles[i] = default(T);
                    return true;
                }
            }
            return false;
        }

        public bool Park(T vehicle)
        {
            for (int i = 0; i < vehicles.Length; i++)
            {
                if (vehicles[i] is null)
                {
                    vehicles[i] = vehicle;
                    return true;

                }
            }
            return false;

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
