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
using System.Windows.Shapes;

namespace CookOrder
{
    /// <summary>
    /// Логика взаимодействия для PanelKeyBoard.xaml
    /// </summary>
    public partial class PanelKeyBoard : Window
    {
        public string MemTovOst = "";
        public PanelKeyBoard()
        {
            InitializeComponent();
            MemOstTov.Text = Convert.ToString(MainWindow.MemOstTov);
            MemTov.Text = MainWindow.MemNameTov + " Ціна: " + MainWindow.MemPriceTov.ToString();
        }

        private void Close_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            Close();
        }

        private void One_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (MemTovOst.Length < 4) MemOstTov.Text = MemTovOst += "1";
            else MemOstTov.Text = MemTovOst;
        }

        private void To_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (MemTovOst.Length < 4) MemOstTov.Text = MemTovOst += "2";
            else MemOstTov.Text = MemTovOst;
        }

        private void Three_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (MemTovOst.Length < 4) MemOstTov.Text = MemTovOst += "3";
            else MemOstTov.Text = MemTovOst;
        }

        private void Four_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (MemTovOst.Length < 4) MemOstTov.Text = MemTovOst += "4";
            else MemOstTov.Text = MemTovOst;
        }

        private void Five_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (MemTovOst.Length < 4) MemOstTov.Text = MemTovOst += "5";
            else MemOstTov.Text = MemTovOst;
        }

        private void Six_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (MemTovOst.Length < 4) MemOstTov.Text = MemTovOst += "6";
            else MemOstTov.Text = MemTovOst;
        }

        private void Eight_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (MemTovOst.Length < 4) MemOstTov.Text = MemTovOst += "8";
            else MemOstTov.Text = MemTovOst;
        }

        private void Seven_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (MemTovOst.Length < 4) MemOstTov.Text = MemTovOst += "7";
            else MemOstTov.Text = MemTovOst;
        }

        private void Nine_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (MemTovOst.Length < 4) MemOstTov.Text = MemTovOst += "9";
            else MemOstTov.Text = MemTovOst;
        }

        private void Zero_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (MemTovOst.Length < 4) MemOstTov.Text = MemTovOst += "0";
            else MemOstTov.Text = MemTovOst;
        }



        private void BackSpace_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (MemTovOst.Length <= 1) MemOstTov.Text = "";
            else MemOstTov.Text = MemTovOst = MemTovOst.Substring(0, MemTovOst.Length - 1);
        }

        private void Del_MouseLeftuttonDown(object sender, MouseButtonEventArgs e)
        {
            MemOstTov.Text = "";
            MemTovOst = "";
        }



        private void Ok_MouseLefBtuttonDown(object sender, MouseButtonEventArgs e)
        {

            if (MemTovOst.Length == 0 && MemOstTov.Text.Length == 0) MemOstTov.Text = "0";
            MainWindow.MemOstTov = Convert.ToDecimal(MemOstTov.Text);

            var StrSprtov = MainWindow.db.Sprtovs.Where(r => r.KodTov == MainWindow.MemKodTov);
            foreach (Sprtov strzap in StrSprtov)
            {
                strzap.OstTov = MainWindow.MemOstTov;
            }
            ApplicationContext dbUpdate = new ApplicationContext();
            dbUpdate.SaveChanges();
            string StrokaStatus = MainWindow.MemOstTov.ToString();
            PreOrderConection.ErorDebag("Изменение остатка товара. Остаток товара: " + StrokaStatus, 3);

            PanelStopList MainWindow_Link = MainWindow.LinkMainWindow("PanelStopList");
            MainWindow_Link.LoadedStopList();
            
            Close();
        }
    }
}
