using Soz.SQLConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Soz.SQLGraphic
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>


        static List<int> UserIdList = new List<int>();
        static List<int> OrderIdList = new List<int>();

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginPage());
        }
    }
}
