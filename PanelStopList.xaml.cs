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
    /// Логика взаимодействия для PanelStopList.xaml
    /// </summary>
    public partial class PanelStopList : Window
    {
        public PanelStopList()
        {
            InitializeComponent();

            if (1460 <= ListOrderCook.ScreenWidth) GridStopList.Width = ListOrderCook.ScreenWidth;
            else
            {
                var WidthColumn = 1460 - ListOrderCook.ScreenWidth;
                GridStopList.Columns[2].Width = 600 - (WidthColumn-70) - 10;
                GridStopList.Columns[3].Width = 200 - 70;
            }

 
            LoadedStopList();
            MainWindow.MemUpdateTableOrder = false;
        }

        public void OnChecked(object sender, RoutedEventArgs e)
        {

            if (GridStopList.SelectedItem != null)
            {
                TableStopList path = GridStopList.SelectedItem as TableStopList;
                if (MainWindow.MemSaleStop != path.SaleStop)
                {
                    MainWindow.MemSaleStop = path.SaleStop;
                    MainWindow.ByBracket = 2;
                    //int MemNewId = MainWindow.db.Sprtovs.Max(p => p.Id) + 1;
                    var StrSprtov = MainWindow.db.Sprtovs.Where(r => r.KodTov == path.KodTov);
                    foreach (Sprtov strzap in StrSprtov)
                    {
                        strzap.SaleStop = path.SaleStop;
                    }
                    MainWindow.db.SaveChanges();
            
                    Window MainWindow_Link = MainWindow.LinkMainWindow("PanelKeyBoard");
                    if (MainWindow_Link != null) MainWindow_Link.Close();
                    string StrokaStatus = MainWindow.MemSaleStop == false ? "В продаже" :"Изъять из продажи" ;
                    PreOrderConection.ErorDebag("Изменение статуса товара. Товар: "+ StrokaStatus, 3);
                    //MainWindow.MemUpdateTableOrder = true;
                }
            }
            ImageClose.Focus();

        }

        public void LoadedStopList()
        {

            string MemNameGrup = "", Art = "";
            List<TableStopList> TableGridStopList = new List<TableStopList>(1);

            foreach (Sprtov pole in MainWindow.db.Sprtovs.ToList<Sprtov>().OrderBy(row => row.KodTov).OrderBy(row => row.KodGrup))
            {

                var StrSprgrup = MainWindow.db.Sprgrups.Where(r => r.KodGrup == pole.KodGrup);
                foreach (Sprgrup strzap in StrSprgrup) { MemNameGrup = strzap.NameUK; }
                if (pole.ArtTov == null) Art = "";
                else Art = pole.ArtTov;

                TableGridStopList.Add(new TableStopList(pole.KodTov, MemNameGrup, pole.NameTovUK, Art, pole.PriceTov, pole.OstTov, pole.SaleStop, pole.KodTov));

            }
            GridStopList.ItemsSource = TableGridStopList;
        }

        private void ImageClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.MemUpdateTableOrder = true;
            //ListOrderCook WinTake = new ListOrderCook();
            //WinTake.Owner = this;
            //WinTake.Show();
            this.Close();
        }

        private void Users_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Users_MouseUp(object sender, MouseButtonEventArgs e)
        {

           
            if (GridStopList.SelectedItem != null)  // MainWindow.PanelBoard == 0 && 
            {
                TableStopList path = GridStopList.SelectedItem as TableStopList;
                if (MainWindow.MemKodTov == path.KodTov) return;
                MainWindow.MemKodTov = path.KodTov;
                MainWindow.MemOstTov = path.OstTov;
                MainWindow.MemNameTov = path.NameTovUK;
                MainWindow.MemPriceTov = path.PriceTov;
                MainWindow.MemSaleStop = path.SaleStop;
                MainWindow.ByBracket = 1;
                PanelKeyBoard WinTake = new PanelKeyBoard();
                WinTake.Owner = this;
                WinTake.Show();

            }

        }

        private void ByBracket_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            if (GridStopList.SelectedItem != null)
            {
                TableStopList path = GridStopList.SelectedItem as TableStopList;
                if ((MainWindow.MemSaleStop != path.SaleStop && MainWindow.MemKodTov == path.KodTov) || (MainWindow.ByBracket == 0 && MainWindow.MemKodTov != path.KodTov))  //
                {

                    MainWindow.ByBracket = 0;
                    //int MemNewId = MainWindow.db.Sprtovs.Max(p => p.Id) + 1;
                    var StrSprtov = MainWindow.db.Sprtovs.Where(r => r.KodTov == path.KodTov);
                    foreach (Sprtov strzap in StrSprtov)
                    {
                        strzap.SaleStop = path.SaleStop;
                    }
                    MainWindow.db.SaveChanges();
                }
                MainWindow.MemSaleStop = path.SaleStop;
                //MainWindow.MemUpdateTableOrder = true;
                string StrokaStatus = MainWindow.MemSaleStop == false ? "В продаже" : "Изъять из продажи";
                PreOrderConection.ErorDebag("Изменение статуса товара. Товар: " + StrokaStatus, 3);

            }
        }

        private void Users_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (GridStopList.SelectedItem != null)  // MainWindow.PanelBoard == 0 && 
            {
                TableStopList path = GridStopList.SelectedItem as TableStopList;
                if (MainWindow.MemKodTov == path.KodTov) return;
                MainWindow.MemKodTov = path.KodTov;
                MainWindow.MemOstTov = path.OstTov;
                MainWindow.MemNameTov = path.NameTovUK;
                MainWindow.MemPriceTov = path.PriceTov;
                PanelKeyBoard WinTake = new PanelKeyBoard();
                WinTake.Owner = this;
                WinTake.Show();

            }
        }
    }
}
