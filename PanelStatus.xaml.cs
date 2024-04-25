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
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading;


namespace CookOrder
{
    /// <summary>
    /// Логика взаимодействия для PanelStatus.xaml
    /// </summary>
    public partial class PanelStatus : Window
    {
        public PanelStatus()
        {
            InitializeComponent();
            MainWindow.StatusUpdate = false;
            this.Left = ListOrderCook.ScreenWidth / 2 - 175;
            this.Top = 100;//this.Height/3-50;
            foreach (SprStatus pole in MainWindow.db.SprStatuss.ToList<SprStatus>())
            {
                if (pole.KodStatus == 2) LabelGotuetcya.Content = pole.NameStatusUk;
                if (pole.KodStatus == 3) LabelGotovo.Content = pole.NameStatusUk;
                if (pole.KodStatus == 4) LabelVidano.Content = pole.NameStatusUk;
                if (pole.KodStatus == 5) LabelOtmena.Content = pole.NameStatusUk;

            }
            Gotuetcya.Visibility = Visibility.Visible;
            Gotovo.Visibility = Visibility.Visible;
            Vidano.Visibility = Visibility.Visible;
            Otmena.Visibility = Visibility.Visible;


            if (MainWindow.MemStatusKod == 2) Gotuetcya.Visibility = Visibility.Collapsed;
            if (MainWindow.MemStatusKod == 3)
            {
                Gotuetcya.Visibility = Visibility.Collapsed;
                Gotovo.Visibility = Visibility.Collapsed;
            }
            TextStatus.Text = "   Вказати статус " + Environment.NewLine + " замовлення № " + MainWindow.NumberOrder.ToString();

        }

        private void Gotuetcya_Click(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            UploadStatusOrder(2);
        }

        private void Gotovo_Click(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            UploadStatusOrder(3);
        }

        private void Vidano_Click(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            UploadStatusOrder(4);
        }

        private void Otmena_Click(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            UploadStatusOrder(5);


        }

  
        public void UploadStatusOrder(int status)
        {

            this.Close();
            Status.IsEnabled = false;
            MainWindow.StatusUpdate = false;
            var ContentOrder = MainWindow.db.ContentZakazs.Where(p => p.Number == MainWindow.NumberOrder && p.Kodtov == MainWindow.IdTovContentOrder).FirstOrDefault();
            ContentOrder.Status = status;
            //  Сохранить изменения
            MainWindow.db.SaveChanges();
            MainWindow.GotovOrder = true;
            if (status == 3)
            { 
                var Content = MainWindow.db.ContentZakazs.Where(p => p.Number == MainWindow.NumberOrder);
                foreach (ContentZakaz zap in Content)
                {
                    //if (zap.Status != 3) MainWindow.GotovOrder = false;
                    zap.Status = 3;
                }
            }
            //if (status == 5)
            //{
            //    ListOrderCook.NewMemNumber = MainWindow.NumberOrder;
            //    ListOrderCook.PrintOrder();
            //    return;
            //}
            if (MainWindow.GotovOrder == true)
            { 
                var HeadUpOrder = MainWindow.db.HeadZakazs.Where(p => p.IdOrder == MainWindow.NumberOrder).FirstOrDefault();
                if (MainWindow.db.HeadZakazs.Where(p => p.IdOrder == MainWindow.NumberOrder).FirstOrDefault() != null)
                {
                    HeadZakaz StrHead = new HeadZakaz();
                    //  заказ меняем на новый статус  
                    StrHead.Id = MainWindow.db.HeadZakazs.Max(p => p.Id) + 1;
                    StrHead.IdOrder = HeadUpOrder.IdOrder;
                    StrHead.IdFloor = HeadUpOrder.IdFloor;
                    StrHead.Number = HeadUpOrder.Number;
                    StrHead.SumZakaz = HeadUpOrder.SumZakaz;
                    StrHead.SumPayment = HeadUpOrder.SumPayment;
                    StrHead.SumDiscount = HeadUpOrder.SumDiscount;
                    StrHead.PlaceService = HeadUpOrder.PlaceService;
                    StrHead.Status = status;
                    StrHead.Payment = HeadUpOrder.Payment;
                    StrHead.BankCart = HeadUpOrder.BankCart;
                    StrHead.NumbTRN = HeadUpOrder.NumbTRN;
                    StrHead.NumbCheck = HeadUpOrder.NumbCheck;
                    StrHead.KodAvtoriz = HeadUpOrder.KodAvtoriz;
                    StrHead.ShtrihKod = HeadUpOrder.ShtrihKod;
                    StrHead.DataTimeBegin = HeadUpOrder.DataTimeBegin;
                    StrHead.DataTimePrint = HeadUpOrder.DataTimePrint;
                    StrHead.DataTimeEnd = DateTime.Now;
                    StrHead.IdKartLoyalty = HeadUpOrder.IdKartLoyalty;

                    // Удаление заголовка заказа
                    var Strhead = MainWindow.db.HeadZakazs.Where(p => p.IdOrder == MainWindow.NumberOrder).FirstOrDefault();
                    MainWindow.db.HeadZakazs.Remove((HeadZakaz)Strhead);
                    MainWindow.db.SaveChanges();
                    // Дозапись заказ сновым статусом
                    MainWindow.db.HeadZakazs.Add(StrHead);
                    //  Сохранить изменения
                    MainWindow.db.SaveChanges();
                    // Заказ отменен
                    if (status == 5)
                    { 
                         string StrokaStatus = HeadUpOrder.IdOrder.ToString()+" Время: "+ StrHead.DataTimeEnd.ToString();
                        PreOrderConection.ErorDebag("Изменение статуса заказа. Заказ отменен: " + StrokaStatus, 3);
                    }
               
                }
            }
            var ListAdmin = MainWindow.db.AdminSetValues.Where(p => p.Id == 1).FirstOrDefault();
            ListAdmin.StatusOrder = ListAdmin.StatusOrder == 0 ? 1 : 0;
            MainWindow.db.SaveChanges();
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                
                ListOrderCook Cook_Link = MainWindow.LinkMainWindow("ListOrderCook");
                if(Cook_Link != null) Cook_Link.LoadTableOrder(MainWindow.StatusOrder1, MainWindow.StatusOrder2, MainWindow.StatusOrder3);
            }));
            MainWindow.MemUpdateTableOrder = true;
            MainWindow.StatusUpdate = true;
            Status.IsEnabled = true;


        }

        private void ImageClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            this.Close();
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {

                ListOrderCook Cook_Link = MainWindow.LinkMainWindow("ListOrderCook");
                if (Cook_Link != null) Cook_Link.LoadTableOrder(MainWindow.StatusOrder1, MainWindow.StatusOrder2, MainWindow.StatusOrder3);
            }));
            //MainWindow.StatusClose = false;
            MainWindow.MemUpdateTableOrder = true;
            MainWindow.StatusUpdate = true;
            //Close();
        }

    }
}
