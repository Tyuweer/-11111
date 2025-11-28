using ModelLogic;
<<<<<<< HEAD
using Ninject;
using PresenterLayer;
=======
>>>>>>> b2720782dba29053f1d983004746f08cc76aba74
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
<<<<<<< HEAD
=======
using Ninject;
>>>>>>> b2720782dba29053f1d983004746f08cc76aba74

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
<<<<<<< HEAD
            var mainForm = kernel.Get<Form1>();
            var logic = kernel.Get<IGenreOperations>();

            // СОЗДАЕМ PRESENTER и связываем его с View и Model
            var presenter = new BookPresenter(mainForm, logic);
=======

            var mainForm = kernel.Get<Form1>();
>>>>>>> b2720782dba29053f1d983004746f08cc76aba74

            Application.Run(mainForm);
        }
    }
}
