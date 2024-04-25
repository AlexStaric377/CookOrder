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
using System.Collections.ObjectModel;
/// Многопоточность
using System.Threading;
using System.Drawing.Printing;
using System.Drawing;

namespace CookOrder
{
    
    /// <summary>
    /// Логика взаимодействия для ListOrderCook.xaml
    /// </summary>
    public partial class ListOrderCook : Window
    {
        public static  List<TableOrder> TableGridOrder = new List<TableOrder>(1);
        public static double ScreenHeight = 0.0;
        public static double SetHeigtCurent = 0.0;
        public static double ScreenWidth = 0.0;
        public static double TableOrdersRowHeight = 0.0;

        public static string ExMessage = "", TextKvitancii = "", TextKvitancii0 = "", TextKvitancii1 = "", TextKvitancii11 = "", TextKvitancii12 = "", TextKvitancii2 = "", TextKvitancii3 = "", TextKvitancii4 = "";
        public static int SizeFont = 11, IdHeadZakaz = 0, CashBank = 1, NewMemNumber=0;
        public static float x = 0.0F, y = 0.0F, StrAddZagolov = 0.0F, StrAdd = 0.0F, StrAddItg = 0.0F, StrAddDisc = 0.0F, StrAddBank = 0.0F;
        public static decimal SumaOrder = 0, SumDiscountBasket =0, SumSkidka =0, SumaFinish =0;
        public static string NumberBankKart = "", NumberTranzakcii = "", InvoiceNum = "", KodAvtor = "", TerminalID  = "" , DiscountBarCod="";
        public static bool PrintOnOff = true;

        public ListOrderCook()
        {
            InitializeComponent();

            // 1920х1080
            ScreenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            ScreenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
 
            if (1370 <= ScreenWidth) TableOrders.Width = ScreenWidth;
            else
            {
                var WidthColumn = 1370- ScreenWidth;
                TableOrders.Columns[3].Width = 600 - WidthColumn-10;
            }

            MainWindow.CountHeadZakaz = MainWindow.db.HeadZakazs.Count();
            PrintOnOff = MainWindow.PrintOrderOnOff == true ? true : false;

            if (MainWindow.StatusOrder1 == 1) CheckBoxNewOrder.IsChecked = true; else CheckBoxNewOrder.IsChecked = false;
            if (MainWindow.StatusOrder2 == 2) CheckBoxGotuetcya.IsChecked = true; else CheckBoxGotuetcya.IsChecked = false;
            if (MainWindow.StatusOrder3 == 3) CheckBoxGotovo.IsChecked = true; else CheckBoxGotovo.IsChecked = false;

            RunUpdateStackOrder();
            if(MainWindow.CountHeadZakaz>0 && MainWindow.MemUpdateTimeOut == true) RunTimeOutOrder();
        }

        private void Users_Loaded(object sender, RoutedEventArgs e)
        {

            LoadTableOrder(MainWindow.StatusOrder1, MainWindow.StatusOrder2, MainWindow.StatusOrder3);

        }

        private void Users_MouseUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            if (TableOrders.SelectedItem != null && MainWindow.StatusClose == true) /*&& MainWindow.MemUpdateStatus != TableOrders.SelectedIndex*/
            {
                // Зафиксировали выбранную запись Далее идетобработка
                MainWindow.MemUpdateStatus = TableOrders.SelectedIndex;
                TableOrder path = TableOrders.SelectedItem as TableOrder;
                MainWindow.NumberOrder = path.MemNumber;
                MainWindow.MemStatusKod = path.MemKodStatus;
                MainWindow.IdTovContentOrder = path.KodTov;
                if (MainWindow.MemStatusKod < 4 || MainWindow.MemStatusKod == 5)
                {
                    MainWindow.StatusClose = true;
                    PanelStatus WinTake = new PanelStatus();
                    WinTake.Owner = this;
                    WinTake.Show();

                }

            }
            else MainWindow.StatusClose = true;
        }


        private void Exit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Click_NewOrder(object sender, RoutedEventArgs e)
        {
            NewOrderCheckBox();
        }

        private void ClickCheckBox_NewOrder(object sender, RoutedEventArgs e)
        {
            CheckBoxNewOrder.IsChecked = CheckBoxNewOrder.IsChecked == false ? true : false;
            NewOrderCheckBox();
        }

        public void NewOrderCheckBox()
        { 
             if (CheckBoxNewOrder.IsChecked == false && CheckBoxGotuetcya.IsChecked == false && CheckBoxGotovo.IsChecked == false) CheckBoxNewOrder.IsChecked = true;
                if (CheckBoxNewOrder.IsChecked == false) MainWindow.StatusOrder1 = 0; else MainWindow.StatusOrder1 = 1;
            LoadTableOrder(MainWindow.StatusOrder1, MainWindow.StatusOrder2, MainWindow.StatusOrder3);
      
        }

        private void Gotuetcya_Click(object sender, RoutedEventArgs e)
        {
            GotuetcyaCheckBox();
        }
        
        private void ClickCheckBox_Gotuetcya(object sender, RoutedEventArgs e)
        {
            CheckBoxGotuetcya.IsChecked = CheckBoxGotuetcya.IsChecked == true ? false : true ;
            GotuetcyaCheckBox();
        }

        public void GotuetcyaCheckBox()
        { 
           if (CheckBoxNewOrder.IsChecked == false && CheckBoxGotuetcya.IsChecked == false && CheckBoxGotovo.IsChecked == false) CheckBoxGotuetcya.IsChecked = true;
            if (CheckBoxGotuetcya.IsChecked == false) MainWindow.StatusOrder2 = 0; else MainWindow.StatusOrder2 = 2;
            LoadTableOrder(MainWindow.StatusOrder1, MainWindow.StatusOrder2, MainWindow.StatusOrder3);
          
        }
     

        private void Gotovo_Click(object sender, RoutedEventArgs e)
        {
            GotovoCheckBox();
        }

        private void ClickCheckBox_Gotovo(object sender, RoutedEventArgs e)
        {
            CheckBoxGotovo.IsChecked = CheckBoxGotovo.IsChecked == false ? true : false;
            GotovoCheckBox();
        }

        public void GotovoCheckBox()
        { 

             if (CheckBoxNewOrder.IsChecked == false && CheckBoxGotuetcya.IsChecked == false && CheckBoxGotovo.IsChecked == false) CheckBoxGotovo.IsChecked = true;
                if (CheckBoxGotovo.IsChecked == false) MainWindow.StatusOrder3 = 0; else MainWindow.StatusOrder3 = 3;
            LoadTableOrder(MainWindow.StatusOrder1, MainWindow.StatusOrder2, MainWindow.StatusOrder3);
   
        
        }

        private void StopList_Click(object sender, RoutedEventArgs e)
        {
            PanelStopList WinTake = new PanelStopList();
            WinTake.Show();
 
        }

        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            DataGridRow row = e.Row;
            row.Height = 100;
        }

        // запуск потока слежения за пасивностью клиента
        public static void RunTimeOutOrder()
        {
            MainWindow.RenderInfo Arguments01 = new MainWindow.RenderInfo();
            Thread thread = new Thread(ThreadTimeOutOrder);
            thread.SetApartmentState(ApartmentState.STA);
            thread.IsBackground = true; // Фоновый поток
            thread.Start(Arguments01);

        }
        // Обновление времени выполнения заказа
        public static void ThreadTimeOutOrder(object ThreadObj)
        {
            MainWindow.MemUpdateTimeOut = false;
            while (0 == 0)
            { 
                Thread.Sleep(30000);
                while (MainWindow.MemUpdateTableOrder == false || MainWindow.StatusUpdate == false) { Thread.Sleep(1000); }
                var ContentOrder = MainWindow.db.ContentZakazs.ToList().OrderBy(row => row.Number);
                foreach (ContentZakaz zap in ContentOrder)
                {
                    if (zap.Timeissue > 0)zap.Timeissue -= 30;
                }
                MainWindow.db.SaveChanges();
                
                while (MainWindow.MemUpdateTableOrder == false || MainWindow.StatusUpdate == false) { Thread.Sleep(1000); }
                System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                {

                    ListOrderCook Cook_Link = MainWindow.LinkMainWindow("ListOrderCook");
                    if (Cook_Link != null)Cook_Link.LoadTableOrder(MainWindow.StatusOrder1, MainWindow.StatusOrder2, MainWindow.StatusOrder3);
 
                }));
     
            }
        }

            // запуск потока слежения за пасивностью клиента
        public static void RunUpdateStackOrder()
        {
            MainWindow.RenderInfo Arguments01 = new MainWindow.RenderInfo();
            Thread thread = new Thread(ThreadUpdateStackOrder);
            thread.SetApartmentState(ApartmentState.STA);
            thread.IsBackground = true; // Фоновый поток
            thread.Start(Arguments01);
        }


        public static void ThreadUpdateStackOrder(object ThreadObj)
        {
            while (0 == 0)
            {
                Thread.Sleep(13000);
                ApplicationContext dbUpdate = new ApplicationContext();
                MainWindow.MemCount = dbUpdate.HeadZakazs.Count();
                NewMemNumber = 0;
                if (MainWindow.CountHeadZakaz < MainWindow.MemCount)
                {
                    //NewMemNumber = dbUpdate.HeadZakazs.Max(s => s.IdOrder);
                    var AddNewOrder = dbUpdate.HeadZakazs.Where(row => row.Status == 1);
                    foreach (HeadZakaz pole in AddNewOrder)
                    {

                        NewMemNumber = pole.IdOrder;
                    }
                        
                    MainWindow.CountHeadZakaz = MainWindow.MemCount;
                    System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
                    {

                        // Дозапись полученных заказов в историю заказов
                        if (dbUpdate.HeadZakazs.Where(row => row.Status == 4 || row.Status == 5).Count() > 0)
                        {
                            ApplicationContext dbIdContent = new ApplicationContext();
                            ApplicationContext dbIdHead = new ApplicationContext();
                            int NewId = dbIdHead.HeadZakazHistorys.Count() > 0 ? dbIdHead.HeadZakazHistorys.Max(p => p.Id) + 1 : 1;
                            int IdContent = dbIdHead.ContentZakazHistorys.Count() > 0 ? dbIdHead.ContentZakazHistorys.Max(p => p.Id) + 1 : 1;
                            //ApplicationContext HeadAdd = new ApplicationContext();
                            var StatusOrderAll = dbUpdate.HeadZakazs.Where(row => row.Status == 4);
                            foreach (HeadZakaz pole in StatusOrderAll)
                            {

                                HeadZakazHistory StrHead = new HeadZakazHistory();
                                StrHead.Id = NewId;
                                StrHead.IdOrder = pole.IdOrder;
                                StrHead.IdFloor = pole.IdFloor;
                                StrHead.Number = pole.Number;
                                StrHead.SumZakaz = pole.SumZakaz;
                                StrHead.SumPayment = pole.SumPayment;
                                StrHead.SumDiscount = pole.SumDiscount;
                                StrHead.PlaceService = pole.PlaceService;
                                StrHead.Payment = pole.Payment;
                                StrHead.Status = pole.Status;
                                StrHead.BankCart = pole.BankCart;
                                StrHead.NumbTRN = pole.NumbTRN;
                                StrHead.NumbCheck = pole.NumbCheck;
                                StrHead.KodAvtoriz = pole.KodAvtoriz;
                                StrHead.ShtrihKod = pole.ShtrihKod;
                                StrHead.DataTimeBegin = pole.DataTimeBegin;
                                StrHead.DataTimePrint = pole.DataTimePrint;
                                StrHead.DataTimeEnd = DateTime.Now;
                                StrHead.IdKartLoyalty = pole.IdKartLoyalty;
                                StrHead.QuantityPozitionTov = pole.QuantityPozitionTov;

                                // Дозапись заказ сновым статусом
                                dbUpdate.HeadZakazHistorys.Add(StrHead);
                                NewId++;
                                int MemNumber = pole.Number;


                                ContentZakazHistory StrContent = new ContentZakazHistory();
                                var ContentOrder = dbIdContent.ContentZakazs.Where(p => p.Number == MemNumber);
                                foreach (ContentZakaz zap in ContentOrder)
                                {

                                    StrContent.Id = IdContent;
                                    StrContent.Number = zap.Number;
                                    StrContent.Kodtov = zap.Kodtov;
                                    StrContent.TovCalc = zap.TovCalc;
                                    StrContent.Quantity = zap.Quantity;
                                    StrContent.Opcii = zap.Opcii;
                                    StrContent.PriceTov = zap.PriceTov;
                                    StrContent.SumTov = zap.SumTov;
                                    StrContent.Timeissue = zap.Timeissue;
                                    StrContent.NameTovUK = zap.NameTovUK;

                                    // Дозапись заказ сновым статусом
                                    dbIdContent.ContentZakazHistorys.Add(StrContent);
                                    IdContent++;
                                }
                                
                              
                                //// Удаление тела заказа
                                ContentZakaz content = dbIdContent.ContentZakazs.Where(c => c.Number == MemNumber).FirstOrDefault();
                                if (content != null)dbIdContent.ContentZakazs.Remove(content);
                                                                
                                dbIdContent.SaveChanges();
                            }
                            // Удаление заголовка заказа
                                //HeadZakaz order = dbUpdate.HeadZakazs.Where(p => p.Status == 4).FirstOrDefault();
                                dbUpdate.HeadZakazs.RemoveRange(dbUpdate.HeadZakazs.Where(p => p.Status == 4 || p.Status == 5));

                            dbUpdate.SaveChanges();
                            //dbIdContent.SaveChanges();

                        }

                        ListOrderCook Cook_Link = MainWindow.LinkMainWindow("ListOrderCook");
                        //Cook_Link.Close();
                        Cook_Link.LoadTableOrder(MainWindow.StatusOrder1, MainWindow.StatusOrder2, MainWindow.StatusOrder3);
                        //ListOrderCook Cook_link = new ListOrderCook();
                        //Cook_link.Show();
                        string TextWindows = "Надійшло нове замовлення № " + NewMemNumber.ToString();
                        MessageAddOrder NewOrder = new MessageAddOrder(TextWindows, 1);
                        //NewOrder.Owner = Cook_Link;
                        NewOrder.Show();


                    }));

                    // печать квитанции
                    if (PrintOnOff == true) PrintOrder();
                }
                
            }
        }


        // печать квитанции по заказу.
        public static void PrintOrder()
        {

            TextKvitancii = "";
            TextKvitancii0 = "";
            TextKvitancii1 = "";
            TextKvitancii11 = "";
            TextKvitancii2 = "";
            TextKvitancii12 = "";
            TextKvitancii3 = "";
            TextKvitancii4 = "";
            StrAddZagolov = 0.0F;
            StrAdd = 0.0F;
            StrAddItg = 0.0F;
            StrAddBank = 0.0F;
            StrAddDisc = 0.0F;

            // Процедура построения тела чека
            LoadNewOreder();

            // создаем объект для печати.
            PrintDocument printDocument = new PrintDocument();
            //// Устанавливаем обработчик собтия печати
            printDocument.PrintPage += PrintPageHandler;
            //// Объект диалога настройки печати
            //// количество страниц, размер бумаги ...
            ////PrintDialog printDialog = new PrintDialog();
            //// печать документа
            printDocument.Print();



        }

        public static void PrintPageHandler(object sender, PrintPageEventArgs e)
        {
            float x = 0.0F;
            float y0 = 0.0F;
            //SizeFont = 10;
            //e.Graphics.DrawString(TextKvitancii, new Font("serif", SizeFont), System.Drawing.Brushes.Black, x, y);
            //float y0 = 115.0F + StrAddZagolov;
            SizeFont = 14;
            e.Graphics.DrawString(TextKvitancii0, new Font("Courier", SizeFont), System.Drawing.Brushes.Black, x, y0);
            //float y01 = 145.0F + StrAddZagolov;
            float y01 = 30.0F + StrAddZagolov;
            SizeFont = 10;
            e.Graphics.DrawString(TextKvitancii1, new Font("Courier", SizeFont), System.Drawing.Brushes.Black, x, y01);
            //float y4 = 145.0F + StrAddZagolov + StrAdd;Lucida Console
            //e.Graphics.DrawString(TextKvitancii11, new Font("Lucida Console", SizeFont), System.Drawing.Brushes.Black, x, y4);
            //float y5 = 145.0F + StrAddZagolov + StrAdd + StrAddItg;
            //if (CashBank == 2)
            //{
            //    e.Graphics.DrawString(TextKvitancii12, new Font("Lucida Console", SizeFont), System.Drawing.Brushes.Black, x, y5);
            //}
            //float y1 = 155.0F + StrAddZagolov + StrAdd + StrAddItg + StrAddBank;ArialseriffeArial
            float y1 = 45.0F + StrAddZagolov + StrAdd + StrAddItg + StrAddBank; //30

            SizeFont = 14;
            e.Graphics.DrawString(TextKvitancii2, new Font("Courier", SizeFont), System.Drawing.Brushes.Black, x, y1);
            if (CashBank == 2) StrAddBank -= 15.0F;
            float y2 = 90.0F + StrAddZagolov + StrAdd + StrAddItg + StrAddBank;
            SizeFont = 9;
            e.Graphics.DrawString(TextKvitancii3, new Font("Courier", SizeFont), System.Drawing.Brushes.Black, x, y2);
            float y3 = 120.0F + StrAddZagolov + StrAdd + StrAddItg + StrAddDisc + StrAddBank;
            SizeFont = 10;
            e.Graphics.DrawString(TextKvitancii4, new Font("Courier", SizeFont), System.Drawing.Brushes.Black, x, y3);
        }


        // Запись готового заказа в таблицу заголовка заказов и тело заказа
        public static void LoadNewOreder()
        {
 
            ApplicationContext dbIdHead = new ApplicationContext();
            var NewHeadZakaz = dbIdHead.HeadZakazs.Where(row => row.IdOrder == NewMemNumber).FirstOrDefault();
 
            SumaOrder = NewHeadZakaz.SumZakaz;
            SumDiscountBasket = NewHeadZakaz.SumDiscount;
            SumSkidka = NewHeadZakaz.SumBonusLoyalti;
            SumaFinish = NewHeadZakaz.SumPayment;
            CashBank = NewHeadZakaz.Payment;

            NumberBankKart = NewHeadZakaz.BankCart;
            NumberTranzakcii = NewHeadZakaz.NumbTRN;
            InvoiceNum = NewHeadZakaz.NumbCheck;
            KodAvtor = NewHeadZakaz.KodAvtoriz;
            TerminalID  = NewHeadZakaz.TerminalID;
            DiscountBarCod = NewHeadZakaz.IdKartLoyalty;

            string StrKvitancii = "";
            //ApplicationContext db = new ApplicationContext();
            //int IdTradingFloor = 0;
            //// загрузка описания точки торговли
            //var TradFloor = dbIdHead.TradingFloors.ToList(); // Связать с кодом торговой точки которая работает в этой базе при настройках БД БекОфисе
            //foreach (TradingFloor Stroka in TradFloor)
            //{
            //    IdTradingFloor = Stroka.Id;
            //    string StrokaNameFloor = Stroka.NameFloor.Trim();
            //    int len = ((32 - StrokaNameFloor.Length) / 2) + StrokaNameFloor.Length;
            //    if (StrokaNameFloor.Trim().Length <= 32) TextKvitancii = StrokaNameFloor.Trim() + " \n";
            //    else
            //    {
            //        StrKvitancii = StrokaNameFloor.Substring(0, 32);
            //        int LastSpace = StrKvitancii.LastIndexOf(" ");
            //        TextKvitancii = StrokaNameFloor.Substring(0, LastSpace) + " \n";
            //        string OstStroka = StrokaNameFloor.Substring(LastSpace + 1, StrokaNameFloor.Length - (LastSpace + 1));
            //        len = ((32 - OstStroka.Length) / 2) + OstStroka.Length;
            //        TextKvitancii += OstStroka.PadLeft(len).PadRight(32) + " \n";
            //        StrAddZagolov += 15.0F;
            //    }


            //    len = ((32 - ("ПН " + Stroka.IPN).Length) / 2) + ("ПН " + Stroka.IPN).Length;
            //    TextKvitancii += ("ПН " + Stroka.IPN).PadLeft(len).PadRight(32) + "\n";

            //    len = ((32 - Stroka.AdrFloor.Length) / 2) + Stroka.AdrFloor.Length;
            //    if (Stroka.AdrFloor.Length <= 32) TextKvitancii += Stroka.AdrFloor.PadLeft(len).PadRight(32) + "\n ";
            //    else
            //    {
            //        StrKvitancii = Stroka.AdrFloor.Substring(0, 32);
            //        int LastSpace = StrKvitancii.LastIndexOf(" ");
            //        TextKvitancii += Stroka.AdrFloor.Substring(0, LastSpace) + " \n";
            //        string OstStroka = Stroka.AdrFloor.Substring(LastSpace + 1, Stroka.AdrFloor.Length - (LastSpace + 1));
            //        len = ((32 - OstStroka.Length) / 2) + OstStroka.Length;
            //        TextKvitancii += OstStroka.PadLeft(len).PadRight(32) + " \n";
            //        StrAddZagolov += 15.0F;
            //    }
            //    len = ((32 - ("Телефон закладу:" + Stroka.Tel).Length) / 2) + ("Телефон закладу:" + Stroka.Tel).Length;
            //    if (("Телефон закладу:" + Stroka.Tel).Length <= 32) TextKvitancii += ("Телефон закладу:" + Stroka.Tel).PadLeft(len).PadRight(32) + "\n";
            //    else
            //    {
            //        TextKvitancii += "Телефон закладу:" + Stroka.Tel.Substring(0, (32 - ("Телефон закладу:").Length)) + " \n";
            //        TextKvitancii += Stroka.Tel.Substring(("Телефон закладу:").Length + 1, Stroka.Tel.Length - (("Телефон закладу:").Length + 1)) + " \n";
            //        StrAddZagolov += 15.0F;
            //    }
            //    len = ((32 - ("Телефон гарячої лінії:").Length) / 2) + ("Телефон гарячої лінії:").Length;
            //    TextKvitancii += ("Телефон гарячої лінії:").PadLeft(len).PadRight(32) + "\n";
            //    if (Stroka.TelFair.Trim().Length > 0)
            //    {
            //        TextKvitancii += Stroka.TelFair.PadLeft((32 - Stroka.TelFair.Length) / 2).PadRight((32 - Stroka.TelFair.Length) / 2) + "\n";
            //        StrAddZagolov += 15.0F;
            //    }
            //    //else PaymentOrderWin.StrAddZagolov -= 15.0F;
            //    TextKvitancii += "Грошова одиниця: грн. ";
 

            //}

            TextKvitancii0 += "Замовлення: № " + NewMemNumber.ToString() + "\n ";           

            // Добавляем товар в тело чека
            string TovName = "";
            ContentZakaz StrTovAdd = new ContentZakaz();
            foreach (ContentZakaz ListOrder in dbIdHead.ContentZakazs.Where(row => row.Number == NewMemNumber).ToList<ContentZakaz>())
            {
                // Разбиваем название товара на несколько строк
                StrKvitancii = "";
                string Quant = ListOrder.Quantity.ToString();
                TovName = ListOrder.NameTovUK;
                TextKvitancii1 += Quant + " ";
                if (TovName.Length < 20)
                {
                    TextKvitancii1 += TovName.PadRight(20) +  "\n"; //ListOrder.PriceTov.ToString().PadLeft(6) + ListOrder.SumTov.ToString().PadLeft(6) +
                    StrAdd += 15.0F;
                }
                else
                {
                    int LengthName = TovName.Length, ind = 0, StartSimvol = 0, LengthSimvol = 20;

                    for (ind = 1; ind <= 4; ind++)
                    {
                        StrKvitancii += TovName.Substring(StartSimvol, LengthSimvol) + ((LengthSimvol == 20 && ind < 4) ? "\n" : "");
                        if (LengthSimvol == 20 && ind < 4)
                        {
                            TextKvitancii1 += StrKvitancii;
                            StrKvitancii = "";
                            StrAdd += 15.0F;
                        }

                        if (LengthName - 20 <= 0)
                        {
                            LengthSimvol = 0;
                            break;
                        }

                        else
                        {
                            LengthName -= 20;
                            StartSimvol += 20;
                            LengthSimvol = ((20 - LengthName) <= 20 && (20 - LengthName) > 0) ? LengthName : 20;

                        }
                    }

                    TextKvitancii1 += StrKvitancii.PadRight(20) + "\n"; //+ ListOrder.PriceTov.ToString().PadLeft(6) + ListOrder.SumTov.ToString().PadLeft(6) + 
                    StrAdd += 15.0F;

                }

             
            }

            //TextKvitancii11 += "Разом:".PadRight(26) + SumaOrder.ToString().PadLeft(6) + "\n";
            //StrAddItg += 15.0F;
            //if (SumDiscountBasket != 0 || SumSkidka != 0)
            //{

            //    TextKvitancii11 += "Сплатити бонусами:".PadRight(26) + SumDiscountBasket.ToString().PadLeft(6) + "\n";
            //    TextKvitancii11 += "Знижка:".PadRight(26) + SumSkidka.ToString().PadLeft(6) + "\n";
            //    TextKvitancii11 += "До сплати:".PadRight(26) + SumaFinish.ToString().PadLeft(6) + "\n";
            //    StrAddItg += 45.0F;
            //}

            StrAddBank = 0;
            if (CashBank == 1)
            {

                TextKvitancii2 += "Заплатити на касі\n";
                TextKvitancii2 += "готівкою чи карткою.\n";
            }
            else
            {

                //TextKvitancii12 += "№ Б.карти: " + NumberBankKart.ToString() + "\n";
                //TextKvitancii12 += "№ Транзакції RRN:" + NumberTranzakcii.ToString() + "\n";
                //TextKvitancii12 += "№ чека у терміналі:" + InvoiceNum + "\n";
                //TextKvitancii12 += "№ Код авторизації:" + KodAvtor + "\n";
                //TextKvitancii12 += "Термінал:" + TerminalID + "\n";
                //StrAddBank = 75.0F;
                TextKvitancii2 += " Чек сплачено.\n";

            }

            IdHeadZakaz = dbIdHead.HeadZakazs.Count() + 1;

            TextKvitancii3 += "_______________________\n";
            string IdHead = IdHeadZakaz.ToString();
            char pad = '0';
            TextKvitancii3 += IdHead.PadLeft(6, pad) + "  " + DateTime.Now.ToString("dd.MM.yyyy") + " " + DateTime.Now.ToString("HH.mm") + "\n ";

            //if (DiscountBarCod.Length != 0 || SumDiscountBasket != 0)

            //{
            //    TextKvitancii3 += "       Номер карти лояльності: \n ";
            //    TextKvitancii3 += "                 " + DiscountBarCod + " \n ";
            //    StrAddDisc = 30.0F;
            //}

            //TextKvitancii3 += "               Дякуємо за візит\n ";
            TextKvitancii4 += "Службова Квітанція\n ";
        }



        public void LoadTableOrder(int StatusOrder1 = 0, int StatusOrder2 = 0, int StatusOrder3 = 0)
        {

           

            int Service = 0, MemStatus = 0, MemNumber = 0, MemKodTov = 0, MemTime = 0, minutes = 0, seconds = 0, OpciyaCol=0, OpciyaPaper = 0, OpciyaMayonez = 0;
            string TimeVidachi = "", VidObslugi = "", NameTov = "", NameStatus = "", MemPayment = "", NameStatusPlus = "", NameStatusTov = "", MemOpcii="";

            List<TableOrder> TableGridOrder = new List<TableOrder>(1);
            //TableOrders.ItemsSource = TableGridOrder;

            MainWindow.MemUpdateTableOrder = false;
            var HeadOrderAll = MainWindow.db.HeadZakazs.ToList().OrderBy(row => row.Number).OrderByDescending(row => row.Status);
            foreach (HeadZakaz pole in HeadOrderAll)  
            {

                if (((StatusOrder1 == pole.Status && StatusOrder2 == 0 && StatusOrder3 == 0) || (StatusOrder2 == pole.Status && StatusOrder1 == 0 && StatusOrder3 == 0) ||
                    (StatusOrder3 == pole.Status && StatusOrder1 == 0 && StatusOrder2 == 0) ||
                    ((StatusOrder2 == pole.Status || StatusOrder1 == pole.Status) && StatusOrder1 != 0 && StatusOrder2 != 0 && StatusOrder3 == 0) ||
                    ((StatusOrder3 == pole.Status || StatusOrder1 == pole.Status) && StatusOrder1 != 0 && StatusOrder3 != 0 && StatusOrder2 == 0) ||
                    ((StatusOrder2 == pole.Status || StatusOrder3 == pole.Status) && StatusOrder2 != 0 && StatusOrder3 != 0 && StatusOrder1 == 0) ||
                    ((StatusOrder2 == pole.Status || StatusOrder3 == pole.Status || StatusOrder1 == pole.Status) && StatusOrder2 != 0 && StatusOrder3 != 0 && StatusOrder1 != 0) ||
                    (StatusOrder2 == 0 && StatusOrder3 == 0 && StatusOrder1 == 0))) 
                {
                    TableOrders.RowHeight = 75;
                    Service = pole.PlaceService;
                    MemStatus = pole.Status;
                    MemNumber = pole.IdOrder;
                    MemPayment = pole.Payment == 1 || pole.Payment == 0 ? " оплата на касі" : " сплачено за безготівку";
                    int Countvid = MainWindow.db.SprVidGiveOuts.Where(s => s.KodGiveOut == Service).Count();
                    if (Countvid != 0)
                    { 
                        var StrGiveOut = MainWindow.db.SprVidGiveOuts.Where(s => s.KodGiveOut == Service);
                        foreach (SprVidGiveOut strzap in StrGiveOut) { VidObslugi = strzap.NameGiveOutUk; }
                    }

                    int CountStatus = MainWindow.db.SprStatuss.Where(r => r.KodStatus == MemStatus).Count();
                    if (CountStatus != 0)
                    { 
                        var StrSprStatus = MainWindow.db.SprStatuss.Where(r => r.KodStatus == MemStatus);
                        foreach (SprStatus strzap in StrSprStatus) { NameStatusPlus = strzap.NameStatusUk + MemPayment; NameStatus = strzap.NameStatusUk; }
                    }
                       
                    int CountContentZakaz = MainWindow.db.ContentZakazs.Where(p => p.Number == MemNumber).Count();
                    if (CountContentZakaz != 0)
                    { 
                        var ContentOrder = MainWindow.db.ContentZakazs.Where(p => p.Number == MemNumber);
                        ApplicationContext dbtov = new ApplicationContext();
                        foreach (ContentZakaz zap in ContentOrder)
                        {
                            NameTov = zap.NameTovUK;
                            MemKodTov = zap.Kodtov;
                            MemOpcii = zap.Opcii;
  
                            OpciyaCol = Convert.ToInt16(MemOpcii.Substring(0, MemOpcii.IndexOf(";")));
                            ApplicationContext dbOpcii = new ApplicationContext();
                            if (OpciyaCol != 0)
                            {
                                int CountOpcii = dbOpcii.OptionsTovs.Where(p => p.Id == OpciyaCol).Count();
                                if (CountOpcii != 0)
                                {
                                     var OptionsStr = dbOpcii.OptionsTovs.Where(p => p.Id == OpciyaCol).FirstOrDefault();
                                    NameTov +=  " -" + OptionsStr.NameOptionsUk + ";";
                                }

                           
                            }
                            MemOpcii = MemOpcii.Substring(MemOpcii.IndexOf(";") + 1, MemOpcii.Length - (MemOpcii.IndexOf(";") + 1));
                            OpciyaPaper = Convert.ToInt16(MemOpcii.Substring(0, MemOpcii.IndexOf(";")));
                            if (OpciyaPaper != 0)
                            {
                                int CountOpcii = dbOpcii.OptionsTovs.Where(p => p.Id == OpciyaPaper).Count();
                                if (CountOpcii != 0)
                                { 
                                    var OptionsStr = dbOpcii.OptionsTovs.Where(p => p.Id == OpciyaPaper).FirstOrDefault();
                                    NameTov += " -" + OptionsStr.NameOptionsUk + ";";                     
                                }
    
                            }
                            MemOpcii = MemOpcii.Substring(MemOpcii.IndexOf(";") + 1, MemOpcii.Length - (MemOpcii.IndexOf(";") + 1));
                            OpciyaMayonez = Convert.ToInt16(MemOpcii.Substring(0, MemOpcii.IndexOf(";")));
                            if (OpciyaMayonez != 0)
                            {

                                int CountOpcii = dbOpcii.OptionsTovs.Where(p => p.Id == OpciyaMayonez).Count();
                                if (CountOpcii != 0)
                                {
                                    var OptionsStr = dbOpcii.OptionsTovs.Where(p => p.Id == OpciyaMayonez).FirstOrDefault();
                                    NameTov += " -" + OptionsStr.NameOptionsUk + ";";                       
                                }

  
                            }
                            //int TovCalcOn = dbOpcii.Sprcalculattovs.Where(p => p.KodTov == MemKodTov).Count();
                            //int[] KolTovCalc = new int[TovCalcOn];
                            //if (TovCalcOn != 0)
                            //{

                            //    var TovCalc = dbOpcii.Sprcalculattovs.Where(p => p.KodTov == MemKodTov);
                            //    foreach (Sprcalculattov stroka in TovCalc)
                            //    {
                            //        try
                            //        {
                            //            var StrNameTov = dbtov.Sprtovs.Where(c => c.KodTov == stroka.CalcTov).FirstOrDefault();
                            //            NameTov +=  "- " + StrNameTov.NameTovUK;

                            //            ApplicationContext dbtovBaz = new ApplicationContext();
                            //            decimal KodTovBun = 0;
                            //            int CountTovCalc = dbtovBaz.Queuereserves.Where(p => p.IdtovBaz == stroka.CalcTov && p.Number == MemNumber).Count();
                            //            if (CountTovCalc != 0)
                            //            {
                            //                var ActTovCalc = dbtovBaz.Queuereserves.Where(p => p.IdtovBaz == stroka.CalcTov && p.Number == MemNumber).FirstOrDefault();
                            //                if (ActTovCalc.IdtovReplace != 0)
                            //                {
                            //                    KodTovBun = ActTovCalc.IdtovReplace;
                            //                }
                            //                else
                            //                {
                            //                    KodTovBun = ActTovCalc.IdtovBaz;
                            //                }
                            //                var NameTovCalc = dbtov.Sprtovs.Where(c => c.KodTov == KodTovBun).FirstOrDefault();
                            //                NameTov += "- " + NameTovCalc.NameTovUK;

                            //            }
                            //                //TableOrders.RowHeight +=  25;
                            //        }
                            //        catch (Exception ex)
                            //        {
                            //            PreOrderConection.ErorDebag("возникло исключение:" + Environment.NewLine + " Отсутствует соединение с сервером " + Environment.NewLine + " === Message: " + ex.Message.ToString() + Environment.NewLine + " === Exception: " + ex.ToString(), 2, 0, (PreOrderConection.StruErrorDebag)null);

                            //            string TextWindows = "Отсутствует соединение с сервером" + Environment.NewLine + " Проверте включен ли сервер.";
                            //            MessageError NewOrder = new MessageError(TextWindows, 1);
                            //            NewOrder.Show();
                            //            Thread.Sleep(10000);
                            //            Environment.Exit(0);
                            //        }

                            //    }
                            //    TableOrdersRowHeight = TableOrders.RowHeight;
                            //}
                       
                            MemTime = MemTime < zap.Timeissue ? zap.Timeissue : MemTime;
                            minutes = MemTime / 60;
                            seconds = MemTime-(minutes * 60);
                            TimeVidachi = "0:" + Convert.ToString(minutes) + ":" + Convert.ToString(seconds);
                            NameStatusTov = "";
                            if (zap.Status == 3)
                            {
                                //var StrStatus = MainWindow.db.SprStatuss.Where(r => r.KodStatus == zap.Status);
                                //foreach (SprStatus strzap in StrStatus) { NameStatusTov = strzap.NameStatusUk; }
                                NameStatusTov = "виготовлено";
                                NameStatusPlus = "виготовлено" + MemPayment;
                            }

                            if (CountContentZakaz == 1) TableGridOrder.Add(new TableOrder(zap.Id, zap.Number.ToString(), VidObslugi, TimeVidachi, NameTov, zap.Quantity, NameStatusPlus, NameStatus, "1", zap.Number, MemStatus, MemKodTov));
                            else TableGridOrder.Add(new TableOrder(zap.Id, "", "", "", NameTov, zap.Quantity, NameStatusTov, NameStatus, "0", zap.Number, MemStatus, MemKodTov));

                            CountContentZakaz--;
                        }
                    
                    
                    }
                    


                }
                
            }
           
            TableOrders.ItemsSource = TableGridOrder;
            MainWindow.MemUpdateTableOrder = true;


        }
      

        private void OnManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }


        //DateTime TimeNow = DateTime.Now; // .ToString("yyyyMMddHHmmss")
        //string Time = TimeNow.ToString("mm:ss");
        ////if (dataGridView1.CurrentRow != null)
        ////    {
        ////        var result = MessageBox.Show("Вы действительно хотите удалить товар? \"" + dataGridView1[2, dataGridView1.CurrentRow.Index].FormattedValue.ToString() + "\"? ", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        ////        if (result == DialogResult.Yes)
        ////        {
        ////            dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
        ////            this.tovarTableAdapter.Update(this.ARM_Comp2DataSet.Tovar);
        //}
        //}
    }
}
