using ModelLogic;
using Ninject;
using PresenterLayer;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            var mainForm = kernel.Get<Form1>();
            var model = kernel.Get<IModel>();

            // СОЗДАЕМ PRESENTER и связываем его с View и Model
            var presenter = new BookPresenter(mainForm, kernel.Get<IModel>());

            Application.Run(mainForm);
        }
    }
}
