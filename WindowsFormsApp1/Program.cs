// Program.cs — теперь просто!
using System;
using System.Windows.Forms;
using ModelLogic;
using DataAccessLayer;

namespace WindowsFormsApp1
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // MVC: View сам создаёт Model (Controller внутри формы)
            var form = new Form1();
            Application.Run(form);
        }
    }
}