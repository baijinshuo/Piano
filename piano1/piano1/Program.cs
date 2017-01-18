using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace piano1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false); ;
            Form2 aForm2 = new Form2();
            aForm2.ShowDialog();
            Application.Run(new Form1());
        }
    }
}
