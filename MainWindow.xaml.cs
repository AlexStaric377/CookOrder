using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CookOrder
{
    
    
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static int NumberOrder = 0, CheckBoxOrder = 0, MemStatusKod = 0, CountHeadZakaz = 0, MemCount = 0, MemKodTov = 0, IdTovContentOrder = 0,
                          PanelBoard = 0, MemStopDBConect = 0, ByBracket = 1, StatusOrder1 = 1,  StatusOrder2 = 2,  StatusOrder3=3, MemUpdateStatus = 0;
        public static string MemNameTov = "";
        public static decimal MemOstTov = 0, MemPriceTov = 0;
        public static bool MemSaleStop = false, MemUpdateTableOrder = true, MemUpdateTimeOut = true, StatusUpdate= true, GotovOrder =false, StatusClose=true, PrintOrderOnOff= false;
        public static ApplicationContext db = new ApplicationContext();


        /// Структура данных для многопотоковой среды (передача аргументов)
        /// </summary>
        public struct RenderInfo
        {
            public string argument1 { get; set; }
            public string argument2 { get; set; }
            public string argument3 { get; set; }
            public string argument4 { get; set; }
            public string argument5 { get; set; }
        }

        #region Вернуть ссылку на главное окно по запросу WPF C# {LinkMainWindow}
        /// <summary>
        /// Вернуть ссылку на окно по запросу WPF C# {ListWindowMain}MainWindow
        /// </summary>
        /// <param name="NameWindow"> Имя главного окна Рабочий стол или Панель</param>
        /// <returns></returns>
        public static dynamic LinkMainWindow(string NameWindow)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.Title == NameWindow)
                    return (dynamic)window;
            }
            return null;
        }
        #endregion



        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, MouseButtonEventArgs e)
        {

            ListOrderCook Cook_link = new ListOrderCook();
            Cook_link.Show();
            //Close();
        }

        private void ExitPreOrder(object sender, MouseButtonEventArgs e)
        {
            Environment.Exit(0);
        }


    }
}
