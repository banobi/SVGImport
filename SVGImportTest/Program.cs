using SVGImport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SVGImportTest2
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            SVGFile svg = new SVGFile(@"C:\Temp\Lixo\CamBam\My Plugins\SVGImport\SVGImport\Tests\Rectangle.svg");

            Console.WriteLine("Groups=" + svg.Groups.Count());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ImportSVG());
        }
    }
}
