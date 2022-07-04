using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage
{
    public interface IUI
    {
        void ShowMenu();
        string GetString();
        string GetInt();
        void PrintMessage(string message);

    }
}
