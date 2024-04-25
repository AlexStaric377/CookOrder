using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading;
using System.Data.Entity.Migrations;

namespace CookOrder
{
    // таблица системных установок параметров 
    public class AdminSetValue
    {
        public int Id { get; set; }
        public string TcpApiHttpServer { get; set; }
        public string PortServer { get; set; }
        public string ValueCmdStroka { get; set; }
        public int StatusOrder { get; set; }
        public int CountOrder { get; set; }
        public string PortTerminal { get; set; }
        public int NumberOrder { get; set; }
        public string TimeEndSmena { get; set; }
        public string TimeEndRegOrder { get; set; }
    }

    // Справочник текстовых оформительных элементов рабочих панелей интерфейса
    public class Admin
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Puth { get; set; }
    }

    // Таблица справочник тороговых точек    
    public class TradingFloor
    {
        public int Id { get; set; }
        public int NaNumberFloor { get; set; }
        public string NameFloor { get; set; }
        public string AdrFloor { get; set; }
        public string IPN { get; set; }
        public string Tel { get; set; }
        public string TelFair { get; set; }
    }

    // Таблица справочник групп товаров
    public class Sprgrup
    {
        public int Id { get; set; }
        public int KodGrup { get; set; }
        public string NameUK { get; set; }
        public string NameEN { get; set; }
        public int IDFotoGrup { get; set; }
        public string ImagePath { get; set; }



    }

    // Таблица справочник видов товаров
    public class Sprvid
    {
        public int Id { get; set; }
        public int Kodvid { get; set; }
        public int KodGrup { get; set; }
        public string NameUK { get; set; }
        public string NameEN { get; set; }
        public int IDFotovid { get; set; }


    }

    // Таблица справочник подвидов товаров
    public class Sprpvid
    {
        public int Id { get; set; }
        public int Kodpvid { get; set; }
        public int KodGrup { get; set; }
        public int Kodvid { get; set; }
        public string NameUK { get; set; }
        public string NameEN { get; set; }
        public int IDFotovid { get; set; }


    }

    // Таблица справочник товаров
    public class Sprtov
    {
        public int Id { get; set; }
        public int KodGrup { get; set; }
        public int Kodvid { get; set; }
        public int Kodpvid { get; set; }
        public int KodTov { get; set; }
        public int Uktved { get; set; }
        public string ArtTov { get; set; }
        public int KodCalc { get; set; }
        public bool PartCalc { get; set; }
        public string NameTovUK { get; set; }
        public string NameTovEN { get; set; }
        public decimal PriceTov { get; set; }
        public decimal OstTov { get; set; }
        public byte[] IDFotoTov { get; set; }
        public int IdConcomtov { get; set; }
        public int IdOptions { get; set; }
        public int IdInfo { get; set; }
        public string Opis { get; set; }
        public string OpisEn { get; set; }
        public string ImagePath { get; set; }
        public bool SaleStop { get; set; }
        public int Timeissue { get; set; }


    }

    // Таблица товаров которые возможно останавливать в продаже. Остановку осуществляет повар.
    public class StopList
    {
        public int Id { get; set; }
        public int KodGrup { get; set; }
        public string NameTovUK { get; set; }
        public decimal PriceTov { get; set; }
        public decimal OstTov { get; set; }
        public int SaleStop { get; set; }
    }
    // справочник сопутствующих товаров
    public class Sprconcomtov
    {
        public int Id { get; set; }
        public int Concomtov { get; set; }
        public int KodGrup { get; set; }
        public int Kodvid { get; set; }
        public int Kodpvid { get; set; }
        public int KodTov { get; set; }
        public string NameUK { get; set; }
        public string NameEN { get; set; }

    }
    // справочник калькуляций товаров
    public class Sprcalculattov
    {
        public int Id { get; set; }
        public int KodTov { get; set; }
        public int KodCalc { get; set; }
        public int CalcTov { get; set; }
        public decimal QuantityTov { get; set; }
        public int Portion { get; set; }
        public string NameCalc { get; set; }

    }

    // Список активных заменённых товаров в калькуляции
    public class Queuereserve
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int KodCalc { get; set; }
        public int IdtovBaz { get; set; }
        public decimal IdtovReplace { get; set; }

    }

    // Таблица справочник взаимозаменяемых составляющих товаров в калькуляции 
    public class CompositionTov
    {
        public int Id { get; set; }
        public int KodCalc { get; set; }
        public int IdtovBaz { get; set; }
        public int IdtovReplace { get; set; }
    }

    // Таблица справочник специальных опций ассортиментных единиц товаров 
    public class OptionsTov
    {
        public int Id { get; set; }
        public int Kodtov { get; set; }
        public int KodOpcii { get; set; }
        public string NameOptionsUk { get; set; }
        public string NameOptionsEn { get; set; }

    }



    // Таблица справочник информации об ассортиментной единице товара
    public class InfoTovSpr
    {
        public int Id { get; set; }
        public int Kodtov { get; set; }
        public string NameInfoUk { get; set; }
        public string NameInfoEn { get; set; }
        public string Weight { get; set; }
        public string Composition { get; set; }
        public string CompositEn { get; set; }
        public string Alergen { get; set; }
        public string AlergenEn { get; set; }
        public string Alarm { get; set; }
        public string AlarmEn { get; set; }
        public int Belki { get; set; }
        public int Giri { get; set; }
        public int Vuglevody { get; set; }
        public int Kkal { get; set; }
    }

    // Таблица справочник фотографий ассортиментной единицы товара
    public class ImagesTov
    {
        public int Id { get; set; }
        public int KodGrup { get; set; }
        public int Kodvid { get; set; }
        public int Kodpvid { get; set; }
        public int Kodtov { get; set; }
        public byte[] FotoGruopTov { get; set; }
        public string ImagePath { get; set; } // путь к изображению  

    }

    // Таблица заголовки документов проданных заказов оперативная однодневка
    public class HeadZakaz
    {
        public int Id { get; set; }
        public int IdOrder { get; set; }
        public int IdFloor { get; set; }
        public int Number { get; set; }
        public decimal SumZakaz { get; set; }
        public decimal SumPayment { get; set; }
        public decimal SumDiscount { get; set; }
        public decimal SumBonusLoyalti { get; set; }
        public int PlaceService { get; set; }
        public int Payment { get; set; }
        public int Status { get; set; }
        public string BankCart { get; set; } // номера карты в формате 1234хххххх5678
        public string NumbTRN { get; set; } //  идентификатор транзакции RRN
        public string TerminalID { get; set; }  // ID терминала
        public string NumbCheck { get; set; } // номер транзакциитанции квитанции  
        public string KodAvtoriz { get; set; } // код авторизации
        public string ShtrihKod { get; set; }
        public DateTime DataTimeBegin { get; set; }
        public DateTime DataTimePrint { get; set; }
        public DateTime DataTimeEnd { get; set; }
        public string IdKartLoyalty { get; set; }
        public int QuantityPozitionTov { get; set; }
    }


    // Таблица заголовки документов проданных заказов накопительная за все время
    public class HeadZakazHistory
    {
        public int Id { get; set; }
        public int IdOrder { get; set; }
        public int IdFloor { get; set; }
        public int Number { get; set; }
        public decimal SumZakaz { get; set; }
        public decimal SumPayment { get; set; }
        public decimal SumDiscount { get; set; }
        public decimal SumBonusLoyalti { get; set; }
        public int PlaceService { get; set; }
        public int Payment { get; set; }
        public int Status { get; set; }
        public string BankCart { get; set; } // номера карты в формате 1234хххххх5678
        public string NumbTRN { get; set; } //  идентификатор транзакции RRN
        public string TerminalID { get; set; }  // ID терминала
        public string NumbCheck { get; set; } // номер транзакциитанции квитанции  
        public string KodAvtoriz { get; set; } // код авторизации
        public string ShtrihKod { get; set; }
        public DateTime DataTimeBegin { get; set; }

        public DateTime DataTimePrint { get; set; }
        public DateTime DataTimeEnd { get; set; }
        public string IdKartLoyalty { get; set; }
        public int QuantityPozitionTov { get; set; }
    }


    // Таблица тело заказа в разрезе ассортиментных единиц товаров  оперативное однодневное 
    public class ContentZakaz
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int Kodtov { get; set; }
        public int TovCalc { get; set; }
        public decimal Quantity { get; set; }
        public string Opcii { get; set; }
        public decimal PriceTov { get; set; }
        public decimal SumTov { get; set; }
        public int Timeissue { get; set; }
        public string NameTovUK { get; set; }
        public int Status { get; set; }


    }

    // Таблица тело заказа в разрезе ассортиментных единиц товаров  накопительное
    public class ContentZakazHistory
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int Kodtov { get; set; }
        public int TovCalc { get; set; }
        public decimal Quantity { get; set; }
        public string Opcii { get; set; }
        public decimal PriceTov { get; set; }
        public decimal SumTov { get; set; }
        public int Timeissue { get; set; }
        public string NameTovUK { get; set; }
        public int Status { get; set; }


    }

    // Таблица предварительного формирования заказ
    public class OrederPreview
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int Idtov { get; set; }
        public decimal Quantity { get; set; }
        public decimal PriceTov { get; set; }
        public decimal SumTov { get; set; }
        public decimal Discount { get; set; }
        public string DeleteTov { get; set; }
        public byte[] ImagePath { get; set; } // путь к изображению  
        public string NameTov { get; set; }
        public string Opcii { get; set; }
    }

    // Таблица очередь сформированных заказов для повара 

    public class Queueoreder
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string VidGiveOut { get; set; }
        public DateTime TimeOut { get; set; }
        public string NameTov { get; set; }
        public decimal Quantity { get; set; }
        public string Status { get; set; }

        public string MemStatus { get; set; }
        public string Line { get; set; }
        public int MemNumber { get; set; }
        public int MemKodStatus { get; set; }
        public int KodTov { get; set; }
    }



    // Таблица очередь заказов для клиента 
    public class Monitorclient
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string VidGiveOut { get; set; }
        public string Status { get; set; }

    }
    // Таблица справочник состояний заказа
    public class SprStatus
    {
        public int Id { get; set; }
        public int KodStatus { get; set; }
        public string NameStatusUk { get; set; }
        public string NameStatusEn { get; set; }
    }
    // Таблица справочник видов оплаты
    public class SprPayment
    {
        public int Id { get; set; }
        public int KodPayment { get; set; }
        public string NamePaymentUk { get; set; }
        public string NamePaymentEn { get; set; }

    }

    // Таблица справочник видов обслуживания
    public class SprVidGiveOut
    {
        public int Id { get; set; }
        public int KodGiveOut { get; set; }
        public string NameGiveOutUk { get; set; }
        public string NameGiveOutEn { get; set; }

    }
    // Таблица часть справочника товаров заданного вида подвида или группы товаров для выбора
    public class SprTovSelect
    {
        public int Id { get; set; }
        public int KodTov { get; set; }
        public decimal PriceTov { get; set; }
        public string Name { get; set; }
        public byte[] ImagePath { get; set; }
    }
    public class ApplicationContext : DbContext
    {

        public static bool DeletedDatabase = false;
        // таблица системных установок параметров 
        public DbSet<AdminSetValue> AdminSetValues { get; set; }
        // Таблица фото оформительных элементов окон
        public DbSet<Admin> Admins { get; set; }

        // Таблица справочник тороговых точек
        public DbSet<TradingFloor> TradingFloors { get; set; }

        // Таблица справочник груп товаров
        public DbSet<Sprgrup> Sprgrups { get; set; }

        // Таблица справочник видов товаров
        public DbSet<Sprvid> Sprvids { get; set; }

        // Таблица справочник подвидов товаров
        public DbSet<Sprpvid> Sprpvids { get; set; }

        // Таблица справочник товаров
        public DbSet<Sprtov> Sprtovs { get; set; }

        // Таблица справочник карт состава товаров  
        public DbSet<CompositionTov> CompositionTovs { get; set; }
        // Таблица справочник специальных опций ассортиментных единиц товаров 
        public DbSet<OptionsTov> OptionsTovs { get; set; }

        // Таблица справочник информации об ассортиментной единице товара
        public DbSet<InfoTovSpr> InfoTovs { get; set; }

        // Таблица справочник фотографий ассортиментной единицы товара
        public DbSet<ImagesTov> ImagesTovs { get; set; }

        // Таблица заголовки документов проданных заказов
        public DbSet<HeadZakaz> HeadZakazs { get; set; }

        // Таблица заголовки документов проданных заказов накопительная за все время
        public DbSet<HeadZakazHistory> HeadZakazHistorys { get; set; }

        // Таблица тело заказа в разрезе ассортиментных единиц товаров 
        public DbSet<ContentZakaz> ContentZakazs { get; set; }

        // Таблица тело заказа в разрезе ассортиментных единиц товаров оперативное однодневное 
        public DbSet<ContentZakazHistory> ContentZakazHistorys { get; set; }
        // Таблица предварительного формирования заказ
        public DbSet<OrederPreview> OrederPreviews { get; set; }

        // справочник сопутствующих товаров
        public DbSet<Sprconcomtov> Sprconcomtovs { get; set; }

        // справочник калькуляций товаров
        public DbSet<Sprcalculattov> Sprcalculattovs { get; set; }

        // Очередь забронированных остатков 
        public DbSet<Queuereserve> Queuereserves { get; set; }

        // Таблица очередь сформированных заказов для повара 
        public DbSet<Queueoreder> Queueoreders { get; set; }

        // Таблица очередь заказов для клиента 
        public DbSet<Monitorclient> Monitorclients { get; set; }

        // Таблица товаров которые возможно останавливать в продаже. Остановку осуществляет повар.
        public DbSet<StopList> StopLists { get; set; }

        // Таблица справочник состояний заказа
        public DbSet<SprStatus> SprStatuss { get; set; }

        // Таблица справочник видов оплаты
        public DbSet<SprPayment> SprPayments { get; set; }

        // Таблица справочник видов  обслуживания
        public DbSet<SprVidGiveOut> SprVidGiveOuts { get; set; }

        // Таблица часть справочника товаров заданного вида подвида или группы товаров для выбора
        public DbSet<SprTovSelect> SprTovSelects { get; set; }
        public class Migrations : DbMigration
        {
            public override void Up()
            {
                CreateTable(
                    "dbo.AdminSetValue", c => new {
                        Id = c.Int(nullable: false, identity: true),
                        TcpApiHttpServer = c.String(),
                        PortServer = c.String(),
                        ValueCmdStroka = c.String(),
                        StatusOrder = c.Int(),
                        CountOrder = c.Int(),
                        PortTerminal = c.String(),
                        NumberOrder = c.Int(nullable: true)
                    })
                    .PrimaryKey(t => t.Id);

            }
        }


        public ApplicationContext()
        {
            string exmesage = "";
            int Open = 1;
            while (Open < 8)
            {
                try
                {

                    ReadConfPreOrder();
                    if (DeletedDatabase == true)
                    {
                        //Database.SetInitializer<ApplicationContext>(null);
                        //Database.Migrate();
                        //var migratorConfig = new ApplicationContext.Migrations();
                        //migratorConfig.Up();
                        //var migratorConfig = new ApplicationContext.Migrations();
                        //var dbMigrator = new DbMigrator(migratorConfig);
                        //dbMigrator.Update();

                        Database.EnsureDeleted();
                        UpdatePreOrder();
                    }

                    Database.EnsureCreated();
                    break;
                }
                catch (Exception ex)
                {
                    exmesage = ex.Message;
                    Thread.Sleep(10000);
                    //PreOrderConection.ErorDebag("возникло исключение:" + Environment.NewLine + " Отсутствует соединение с сервером " + Environment.NewLine + " === Message: " + ex.Message.ToString() + Environment.NewLine + " === Exception: " + ex.ToString(), 2, 0, (PreOrderConection.StruErrorDebag)null);

                    //string TextWindows = "Отсутствует соединение с сервером" + Environment.NewLine + " Проверте включен ли сервер.";
                    //MessageError NewOrder = new MessageError(TextWindows, 1);
                    //NewOrder.Show();
                    //Thread.Sleep(10000);
                    //Environment.Exit(0);
                }
                Open++;
            }
            if (Open > 7)
            {
                PreOrderConection.ErorDebag("возникло исключение:" + Environment.NewLine + " Отсутствует соединение с сервером " + Environment.NewLine + " === Message: " + exmesage + Environment.NewLine + " === Exception: " + exmesage, 2, 0, (PreOrderConection.StruErrorDebag)null);

                string TextWindows = "Отсутствует соединение с сервером" + Environment.NewLine + " Проверте включен ли сервер."
                    + Environment.NewLine + exmesage;
                MessageError NewOrder = new MessageError(TextWindows, 1);
                NewOrder.Show();
                Thread.Sleep(10000);
                Environment.Exit(0);
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            string PuthServer = AppDomain.CurrentDomain.BaseDirectory;
            string DbPuth = @"Host=localhost;Port=5432;Database=d:\PreOrderWin\DbPreOrder;Username=postgres;Password=pgsql";
            //string DbPuth = @"Host=localhost;Port=5432;Database=d:\PreorderWin\DbPreOrder;Username=postgres;Password=pgsql";
            if (File.Exists(PuthServer + "PreOrder.conf"))
            {
                string[] fileLines = File.ReadAllLines(PuthServer + "PreOrder.conf");
                foreach (string x in fileLines)
                {
                    if (x.Contains("=") == true && x.StartsWith("#") == false && x.Contains("$") == false && x.Length != 0)
                    {
                        if (x.Trim().Contains("DbPuth")) DbPuth = x.Substring(x.IndexOf("=") + 1, x.Length - (x.IndexOf("=") + 1));
                        //if (x.Trim().Contains("PortTerminal"))
                        //{
                        //    MainWindow.PortTerminal = "PortTerminal";
                        //    if (x.Trim().Length > x.IndexOf("=") + 1) MainWindow.USBPortTerminal = Convert.ToByte(x.Substring(x.IndexOf("=") + 1, x.Length - (x.IndexOf("=") + 1)));
                        //}
                        //if (x.Trim().Contains("TcpUrlServer")) if (x.Trim().Length > x.IndexOf("=") + 1) MainWindow.TcpUrlServer = x.Substring(x.IndexOf("=") + 1, x.Length - (x.IndexOf("=") + 1)).Trim();
                        //if (x.Trim().Contains("PortUrlServer")) if (x.Trim().Length > x.IndexOf("=") + 1) MainWindow.PortUrlServer = x.Substring(x.IndexOf("=") + 1, x.Length - (x.IndexOf("=") + 1)).Trim();
                        //if (x.Trim().Contains("TimeEndSmena")) if (x.Trim().Length > x.IndexOf("=") + 1) MainWindow.TimeEndSmena = x.Substring(x.IndexOf("=") + 1, x.Length - (x.IndexOf("=") + 1)).Trim();
                        //if (x.Trim().Contains("TimeEndRegOrder")) if (x.Trim().Length > x.IndexOf("=") + 1) MainWindow.TimeEndRegOrder = x.Substring(x.IndexOf("=") + 1, x.Length - (x.IndexOf("=") + 1)).Trim();


                    }
                }
            }
            optionsBuilder.UseNpgsql(DbPuth);
        }
        // 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HeadZakaz>().HasKey(u => new { u.Status, u.Number });
            //modelBuilder.Entity<Sprtov>().HasKey(u => new { u.KodTov });
        }

        public void ReadConfPreOrder()
        {
            string PuthServer = AppDomain.CurrentDomain.BaseDirectory;
            if (File.Exists(PuthServer + "PreOrder.conf"))
            {
                string[] fileLines = File.ReadAllLines(PuthServer + "PreOrder.conf");
                foreach (string x in fileLines)
                {
                    if (x.Contains("=") == true && x.StartsWith("#") == false && x.Contains("$") == false && x.Length != 0)
                    {
                        if (x.Trim().Contains("DeletedDatabase"))
                        {
                            string[] data = x.Split('=');
                            DeletedDatabase = data[1].Trim() == "true" ? true : false;
                        }
                        if (x.Trim().StartsWith("PrintOrderOnOff")) if (x.Trim().Length > x.IndexOf("=") + 1) MainWindow.PrintOrderOnOff = x.Substring(x.IndexOf("=") + 1, x.Length - (x.IndexOf("=") + 1)).Trim() == "true" ? true : false;
                    }
                }
            }
        }

        public void UpdatePreOrder()
        {
            string PuthServer = AppDomain.CurrentDomain.BaseDirectory;
            if (File.Exists(PuthServer + "PreOrder.conf"))
            {
                int Idcount = 0, Idcountout = 0;
                string[] fileLines = File.ReadAllLines(PuthServer + "PreOrder.conf");
                string[] fileoutLines = new string[fileLines.Count()];
                foreach (string x in fileLines)
                {
                    if (x.Contains("=") == true && x.StartsWith("#") == false && x.Contains("$") == false && x.Length != 0)
                    {
                        if (x.Trim().Contains("DeletedDatabase"))
                        {
                            fileLines[Idcount] = "DeletedDatabase = false";
                        }
                        fileoutLines[Idcountout] = fileLines[Idcount];
                        Idcountout++;
                    }
                    Idcount++;
                }
                File.WriteAllLines(PuthServer + "PreOrder.conf", fileoutLines);
            }
        }

    }

    public class TableOrder
    {
        public TableOrder(int Id, string Number, string VidGiveOut, string TimeOut, string NameTov, decimal Quantity, string Status, string MemStatus, string Line, int MemNumber, int MemKodStatus, int KodTov)
        {
            this.Id = Id;
            this.Number = Number;
            this.VidGiveOut = VidGiveOut;
            this.TimeOut = TimeOut;
            this.NameTov = NameTov;
            this.Quantity = Quantity;
            this.Status = Status;
            this.MemStatus = MemStatus;
            this.Line = Line;
            this.MemNumber = MemNumber;
            this.MemKodStatus = MemKodStatus;
            this.KodTov = KodTov;
        }

        public int Id { get; set; }
        public string Number { get; set; }
        public string VidGiveOut { get; set; }
        public string TimeOut { get; set; }
        public string NameTov { get; set; }
        public decimal Quantity { get; set; }
        public string Status { get; set; }
        public string MemStatus { get; set; }
        public string Line { get; set; }
        public int MemNumber { get; set; }
        public int MemKodStatus { get; set; }
        public int KodTov { get; set; }
    }

    public class TableStopList
    {
        public TableStopList(int Id, string NameGrup, string NameTovUK, string Artikul, decimal PriceTov, decimal OstTov, bool SaleStop, int KodTov)
        {
            this.Id = Id;
            this.NameGrup = NameGrup;
            this.NameTovUK = NameTovUK;
            this.Artikul = Artikul;
            this.PriceTov = PriceTov;
            this.OstTov = OstTov;
            this.SaleStop = SaleStop;
            this.KodTov = KodTov;

        }

        public int Id { get; set; }
        public string NameGrup { get; set; }
        public string NameTovUK { get; set; }
        public string Artikul { get; set; }
        public decimal PriceTov { get; set; }
        public decimal OstTov { get; set; }
        public bool SaleStop { get; set; }
        public int KodTov { get; set; }

    }


    public class TableGrup
    {
        public TableGrup(int Id, string Name, int IdImageByte, string ImagePath)
        {
            this.Id = Id;
            this.Name = Name;
            this.IdImageByte = IdImageByte;
            this.ImagePath = ImagePath;


        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int IdImageByte { get; set; }
        public string ImagePath { get; set; }




    }

    public class SprGrupImage
    {
        public SprGrupImage(int Id, string Name, byte[] ImagePath)
        {
            this.Id = Id;
            this.Name = Name;
            this.ImagePath = ImagePath;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] ImagePath { get; set; }

    }

    public class TableTov
    {
        public TableTov(int Id, int KodTov, decimal PriceTov, string ImagePath, string NameTov)
        {
            this.Id = Id;
            this.KodTov = KodTov;
            this.PriceTov = PriceTov;
            this.ImagePath = ImagePath;
            this.NameTov = NameTov;
        }

        public int Id { get; set; }
        public int KodTov { get; set; }
        public decimal PriceTov { get; set; }
        public string ImagePath { get; set; }
        public string NameTov { get; set; }


    }

    public class SprTovImage
    {
        public SprTovImage(int Id, int KodTov, decimal PriceTov, string Name, byte[] ImagePath)
        {
            this.Id = Id;
            this.KodTov = KodTov;
            this.PriceTov = PriceTov;
            this.Name = Name;
            this.ImagePath = ImagePath;
        }

        public int Id { get; set; }
        public int KodTov { get; set; }
        public decimal PriceTov { get; set; }
        public string Name { get; set; }
        public byte[] ImagePath { get; set; }

    }

    // : //INotifyPropertyChanged
    public class TovSelected  //: INotifyPropertyChanged
    {


        public TovSelected(int Id, int KodTov, decimal PriceTov, string Name, byte[] ImagePath)
        {
            this.Id = Id;
            this.KodTov = KodTov;
            this.PriceTov = PriceTov;
            this.Name = Name;
            this.ImagePath = ImagePath;
        }

        public int Id { get; set; }
        public int KodTov { get; set; }
        public decimal PriceTov { get; set; }
        public string Name { get; set; }
        public byte[] ImagePath { get; set; }

    }

    //public class CmdAddOrder
    //{

    //    public string cmd { get; set; }
    //    public string data { get; set; }
    //    public Dictionary<string, Tovar> goods { get; set; }

    //    public string[] SummaryWords { get; set; }


    //}

    public class ListTovar
    {
        public string code { get; set; }
        public decimal qty { get; set; }
        public decimal price { get; set; }
        public decimal amount { get; set; }
    }
}
