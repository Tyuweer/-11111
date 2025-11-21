using ModelLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ninject;

namespace WindowsFormsApp1
{
    /// <summary>
    /// Главный класс приложения Windows Forms
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            IKernel kernel = new StandardKernel(new SimpleConfigModule());
            var logic = kernel.Get<IGenreOperations>();

            var mainForm = kernel.Get<Form1>();

            Application.Run(mainForm);
        }
    }
}
