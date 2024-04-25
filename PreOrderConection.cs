#region импорт следующих имен пространств .NET:
//---- объекты ОС Windows (Реестр, {Win Api} 
using Microsoft.Win32;
using System;
using System.Collections.Generic;
// Управление Изображениями
using System.ComponentModel;

// --- Process
using System.Diagnostics;
using System.Drawing;
// Ссылка в проекте MSV2010 добовляется ...
using System.Drawing.Text;

// локаль операционной системы
using System.Globalization;
// Управление вводом-выводом
using System.IO;
using System.IO.Compression;
using System.Linq;
// ---- BD Управление БД
using System.Data;              // Содержит типы, независимые от провайдеров, например DataSet и DataTable.
using System.Data.OleDb;        // System.Data.OleDb. Содержит типы OLE DB .NET Data Provider.
// Удаленное управление компьютером
// 
using System.Management;
// Управление сетью
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
//--- для Проверки Сборок
using System.Reflection;
// Импорт библиотек Windows DllImport (управление питанием ОС, ...
using System.Runtime.InteropServices;
// шифрование данных
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;
/// Многопоточность
using System.Threading;
// --- Timer
using System.Timers;
using System.Windows;
using System.Windows.Controls;
//--- WPF
using System.Windows.Media;
using System.Windows.Threading;
// Управление Xml
using System.Xml;
using System.Xml.Linq;

// DataGridExtensions
using System.Windows.Controls.Primitives;
using System.Threading.Tasks;

#endregion

namespace CookOrder
{
    /// <summary>
    ///  Разделяемый класс по файлам (ключевое слово - partial)
    /// </summary>

    public partial class PreOrderConection
    {
        // http://www.spugachev.com/windows8book
        // http://blogs.msdn.com/b/rudevnews/archive/2012/12/06/windows-8-c.aspx
        // professorweb.ru
        // http://metanit.com/
        // http://www.csharpcoderr.com/p/blog-page_19.html перечень примеров
        // http://kbyte.ru/ru/  перечень примеров MSSQl и прочего
        // http://kbss.ru/blog/lang_c_sharp/ - много интересного плюс сбор на терминале (для инвентаризации)



        // Короткие задачи
        // http://cyber-blog.klan-hub.ru/tag/csharp/  - Сервер
        // http://professorweb.ru/my/WPF/base_WPF/level5/5_12.php - Drag End Drop
        // http://www.harding.edu/fmccown/screensaver/screensaver.html - Хранитель Экрана Screen Saver with C#

        // Информация типы данных
        // http://msdn.microsoft.com/ru-ru/library/ms228360(v=vs.90).aspx


        // Защита приложения
        // http://habrahabr.ru/post/97062/ - обзор http://www.youtube.com/watch?v=-Gval9wYWIw
        // Guard защита - http://www.aktiv-company.ru/news/company-editorial-06-09-2011.html
        // http://www.vgrsoft.com/ru/download/ilp
        // http://www.eziriz.com/dotnet_reactor.htm


        // распараллеливанию вычислений в кластере. Так или иначе придется организовывать взаимодействие процессов между собой. 
        // Сейчас широко используется Message Passing Interface. Boost.MPI - одна из реализаций, 
        // MPI.Net http://osl.iu.edu/research/mpi.net/ - другая реализация.


        #region Глобальные параметры (переменные)

        /// <summary>
        /// Имя приложения. Используется для идентификации приложения в ОС.
        /// </summary>
        // public static string NameOP = "Conecto® WorkSpace";
        /// <summary>
        /// Стартовый путь приложения [Application.StartupPath]
        /// В WPF меняем на String appStartPath = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
        /// System.IO.Directory.GetCurrentDirectory()
        /// </summary>
        public static string PStartup = AppDomain.CurrentDomain.BaseDirectory; // AppDomain.CurrentDomain.BaseDirectory
        /// <summary>
        /// Режим отладки в приложения
        /// </summary>
        public static bool DebagApp = true;
        /// <summary>
        /// Режим компиляции приложения Alfa, Beta, Release, Beta App, Release App - режим отадки AppPlay приложений
        /// </summary>
        public static string ReleaseCandidate = "Beta App";
        /// <summary>
        /// Аварийный выход
        /// </summary>
        public static bool STOP = false;
        /// <summary>
        /// Идентификатор исполняемого приложения (исключения допустимы)
        /// </summary>
        public static string IDDir;
        /// <summary>
        /// Путь к файлам приложения по умолчанию. Файлы: библиотеки, изображения, логи, ... 
        /// </summary>
        public static string PutchApp = AppDomain.CurrentDomain.BaseDirectory;
        /// <summary>
        /// Путь к системному логу и для AppPlay (systems.log)
        /// </summary>
        public static string PuthSysLog = AppDomain.CurrentDomain.BaseDirectory + @"\PreOrder.log";
        /// <summary>
        /// Разрешение записи в лог файл (если файл не может быть создан или прочитан ситема запрещает писать в файл) 
        /// при false происходит вывод сообщений на екран.
        /// </summary>
        public static bool LogFile = true;
        /// <summary>
        /// Параметры приложения
        /// </summary>
        public static Dictionary<string, string> aParamApp = new Dictionary<string, string>();




        /// <summary>
        /// // Исключение повторного запуска программы (ture - один раз)
        /// </summary>
        public static bool StartApp { get; set; }
        /// <summary>
        /// Разрешение рабочего стола (основного монитора)
        /// [0] - Top; [1] - Left; [2] - Width; [3] - Height; [4] - Right; [5][6] - Reserve<para></para> 
        /// [7] - Left вертикальной виртуальной черты, отделяемая правую сторону с информационными кнопками от левой 
        /// </summary>
        public static double[] WorkAreaDisplayDefault = new double[8] { 0, 0, 0, 0, 0, 25, 14, 0 }; //{ get; set; }

        /// <summary>
        /// Рабочий стол для основного Окна Primary
        /// </summary>
        public static WorkAreaDisplayOne WorkAreaDisplayOneDef = new WorkAreaDisplayOne();

        /// <summary>
        /// Разрешение рабочего стола (основного монитора)
        /// </summary>
        public struct WorkAreaDisplayOne
        {

            public double Top { get; set; }
            public double Left { get; set; }
            public double Width { get; set; }
            public double Height { get; set; }

            public double Right { get; set; }

            public double IndexWidthError { get; set; }
            public double IndexHeightError { get; set; }

            /// <summary>
            /// Left вертикальной виртуальной черты, отделяемая правую сторону с информационными кнопками от левой 
            /// </summary>
            public double IndexLeft { get; set; }

        }


        /// <summary>
        /// Имя окна системы, активного в данный момент (окна в режиме одного активного окна)
        /// </summary>
        public static string WindowPanelSys_s { get; set; }
        /// <summary>
        /// Базовый клас симетричного ширования
        /// </summary>PutchApp
        private static SymmetricAlgorithm des = null;

        //---------------------------------------------------------------------------------------------------- 



        public string VersijaPO = "a-0-5";                    // Версия ПО (a - альфа версия; первая цифра основные изменения, вторая цифра незначительые изменения)

        public object OSVer = Environment.OSVersion;          // Версия ОС 

        public static Dictionary<string, string> NetworkPC = new Dictionary<string, string>(); // Параметры сети


        public static FileStream XMLConfigFile = null; // Блокировка основного файла кофигурации

        /// <summary>
        /// Время ожидания подключения сети или подключение к серверам, сервисам (после чего выводим сообщение)
        /// </summary>
        public static TimeSpan WaitNetTimeSec = new TimeSpan(0, 0, 35);

        // public static FtpWebResponse NetFTPResponse = null;

        //private static string PutchPack = null;

        /// <summary>
        /// Режим терминала - программа включается без рабочего стола Windows
        /// </summary>
        public static bool TerminalStatus = false;


        /// <summary>
        /// Параметры окна пользователя при авторизации <para></para>
        /// 0 - Системное имя метода класса или метода исполнения изменения интерфейса после авторизации, <para></para>
        /// 1 - Тип авторизации: (-1 - отсутствует, 0 - логин пароль, 1 - пин(карточка), 2 - авто авторизация, 3 - авторизация с носителя),<para></para> 
        /// 2 - Ссылка на доп. Изображение справа,<para></para>
        /// 3 - Ссылка на доп. Изображение сверху, <para></para>
        /// 4 - Текст под картинкой на доп. Изображении справа <para></para>
        /// 5 - Сервер учетных данных (FB, MSSQL, LDAP, WEB server, MySql, UserConecto)<para></para>
        /// 6 - Сервер-IP<para></para>
        /// 7 - Cервер-Alias<para></para>
        /// 8 - Cервер-DopParam 1) Путь к БД если отсутсттвует: Cервер-Alias <para></para>
        /// 9 - Cервер-DopParam-Тип БД: B52-дополнительный индекс к логину (при смене пароля непонятная ситуация)<para></para>
        /// 10- Сервер порт<para></para>
        /// </summary>
        public static string[] Autirize = new string[11] { "", "", "", "", "", "", "", "", "", "", "3055" };


        /// <summary>
        /// Количестов высокотребовательных запросов, запрещает выход из системы
        /// </summary>
        public static int CountQueryHieght = 0;

        #endregion


        #region Логирование и вывод сообщений приложения {v 1.8}

        /// <summary>
        /// Код, защищенный таким образом от неопределённости в плане многопотокового исполнения, называется потокобезопасным. 
        /// Все потоки при записи борются за блокировку объекта
        /// </summary>
        private static Object lockerErr = new Object();

        /// <summary>
        /// Хеш последней ошибки, создан для проверки повтора ошибки через интервал времени. Исключил дублирование ошибок в лог файле, для удобства чтения лога.
        /// </summary>
        public static string HashLastError = "";

        /// <summary>
        /// Количество повтора динаковых ошибок.
        /// </summary>
        public static int CountLastError = 0;

        /// <summary>
        /// Время и дата первой ошибки в повторении
        /// </summary>
        public static string LastTimeError = "";


        /// <summary>
        /// Список параметров ошибок
        /// </summary>
        public class StruErrorDebag
        {
            /// <summary>
            /// Возвращает cообщение об ошибке
            /// </summary>
            public string Message { get; set; }
            /// <summary>
            /// Возвращает код HRES ошибки
            /// </summary>
            public int ErrorCode { get; set; }

            /// <summary>
            /// Возвращает код ошибки Number 
            /// </summary>
            public int ErrorNumber { get; set; }

            /// <summary>
            /// Имя пользователя
            /// </summary>
            public string NameUSERSID { get; set; }

            /// <summary>
            /// Домен пользователя (имя компьютера)
            /// </summary>
            public string DomenUSERSID { get; set; }

            /// <summary>
            /// ID пользователя SID
            /// </summary>
            public string USERSID { get; set; }

            // ===================== Пример полной формы записи
            //private double _seconds;
            //public double Seconds
            //{
            //    get { return _seconds; }
            //    set { _seconds = value; }
            //}

        }


        /// <summary>
        /// Логирование и вывод сообщений приложения<para></para>
        /// </summary>
        /// <param name="Message">Message: Текст сообщения.</param><para></para>
        /// <param name="TypeError">TypeError: Тип сообщений: <para></para> 0 - исключение;<para></para> 1 - отладка;<para></para> 
        /// 2 - исключение с выводом сообщения на экран;<para></para> 3 - информация.</param><para></para>
        /// <param name="Image">Image: Вывод изображений в информационном окне TypeError = 2.</param><para></para>
        /// <param name="StruErrorDebag">StruErrorDebag: Сообщение ошибки в виде структуры - для табличных логов (разработка).</param><para></para>
        public static void ErorDebag(string Message, int TypeError = 0, int Image = 0, StruErrorDebag StructuraError = null)
        {
            // Не выводить сообщения при отключенной отладке
            // Откладка отключается при настройки логирования системы в администрировании
            if (!DebagApp && TypeError == 1)
            {
                return;
            }

            Encoding win1251 = Encoding.GetEncoding("windows-1251");

            // Кнопки окаобщения (по умолчанию информирование) 
            var ImegLink = System.Windows.MessageBoxImage.Information;
            switch (Image)
            {
                case 0:
                case 1:
                    ImegLink = System.Windows.MessageBoxImage.Information;
                    break;
                case 2:
                    ImegLink = System.Windows.MessageBoxImage.Stop;
                    break;
                default:
                    ImegLink = System.Windows.MessageBoxImage.Information;
                    break;
            }


            if (LogFile)
            {
                // запись в лог код; время; пользователь; тип сообщения; сообщения
                //string HeadLog = String.Format("Id; {0}; {1}; {2}; {3}; {4}", "Дата", "Имя пользователя", "Домен пользователя", "Тип сообщения", "Сообщение");


                string text = Environment.NewLine;
                string textall = "";
                switch (TypeError)
                {
                    case 0:
                    case 2:
                        text = text + "101;";
                        break;
                    case 1:
                        text = text + "103;";
                        break;
                    case 3:
                        text = text + "104;";
                        break;

                }
                // Здесь очень опасно вставлять не опереденное время!!! Синхронизировать но как?
                DateTime dateTime = DateTime.Now;

                // Определение структуры
                if (StructuraError == null)
                {
                    text = string.Format(text + " {0}; {1}; {2}; --- ", dateTime.ToString("dd.MM.yyyy HH:mm:ss"), "NoName", "NoDomen");
                }
                else
                {
                    text = string.Format(text + " {0}; {1}; {2}; --- ", dateTime.ToString("dd.MM.yyyy HH:mm:ss"), StructuraError.NameUSERSID, StructuraError.DomenUSERSID);
                }

                switch (TypeError)
                {
                    case 0:
                    case 2:
                        text = text + "Ошибка" + ": ;";
                        break;
                    case 1:
                        text = text + "Отладка" + ": ;";
                        break;
                    case 3:
                        text = text + "Информация" + ": ;";
                        break;

                }



                text += Message;

                // Блокировка
                lock (lockerErr)
                {
                    // Проверка структуры
                    // textall = (!(File.Exists(PuthSysLog)) ? HeadLog + Environment.NewLine : "") + text;


                    // Проверка структуры
                    if (File.Exists(PuthSysLog))
                    {
                        textall = text;

                        // Проверка размера файла
                        System.IO.FileInfo file = new System.IO.FileInfo(PuthSysLog);
                        if (file.Length > 1048576)
                        {
                            // очитсить содержимое
                            File.WriteAllText(PuthSysLog, "");
                            textall = text; //HeadLog + Environment.NewLine +
                        }
                        // File.(PuthSysLog);

                    }
                    else
                    {
                        textall = text; //HeadLog + Environment.NewLine +


                    }




                    // Отслеживание ошибок в досупе к файлу лога в многопотоковой среде 
                    try
                    {
                        using (StreamWriter FileSysLog = new StreamWriter(PuthSysLog, true, win1251))
                        {
                            FileSysLog.WriteLine(textall);
                            FileSysLog.Close();
                        }
                    }
                    catch //(Exception ex)
                    {
                        // Отследить ошибки
                        LogFile = false;
                        // 1. Запись в БД локальную или центральную
                        // Пробуем записать в локальный лог 
                        string LocalLog = AppDomain.CurrentDomain.BaseDirectory + "local_preorder.log";
                        textall = text; //(!(File.Exists(LocalLog)) ? HeadLog + Environment.NewLine : "") +

                        try
                        {
                            using (StreamWriter FileSysLog = new StreamWriter(LocalLog, true, win1251))
                            {
                                FileSysLog.WriteLine(textall);
                                FileSysLog.Close();
                            }
                            // Лог сохранен
                            LogFile = true;
                        }
                        catch //(Exception ex)
                        {


                        }
                    }
                }
                if (TypeError == 2 || !LogFile)
                {

                    // 1. Запись в БД локальную или центральную


                    // 2. Изменить верстку окна ошибок

                    //System.Windows.MessageBox.Show(Message, "", System.Windows.MessageBoxButton.OK, ImegLink);
                }
            }
            else
            {
                // 1. Запись в БД локальную или центральную

                //System.Windows.MessageBox.Show(Message, "", System.Windows.MessageBoxButton.OK, ImegLink);
                // MessageBox.Show("Данные отладки: " + IpVAr[0] + "{}" + IpVAr[1], "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

        }

        #endregion



        #region Вернуть ссылку на окно по запросу WPF C# {ListWindowMain}
        /// <summary>
        /// Вернуть ссылку на окно по запросу WPF C# {ListWindowMain}
        /// </summary>
        /// <param name="NameWindow"></param>
        /// <returns></returns>
        public static System.Windows.Window ListWindowMain(string NameWindow)
        {

            foreach (System.Windows.Window openWindow in System.Windows.Application.Current.Windows)
            {

                // System.Windows.MessageBox.Show(openWindow.Name.ToString(), "«Open Windows»");
                if (openWindow.Name == NameWindow)
                {
                    // Пример ссылки на объект
                    //System.Windows.Controls.Image g = (System.Windows.Controls.Image)LogicalTreeHelper.FindLogicalNode(openWindow, "ConectInternet");
                    //System.Windows.MessageBox.Show( g.Source.ToString(), "«Open Windows»");


                    // Пример кода
                    // ownedWindow.Hide();
                    // ownedWindow.Close();
                    return openWindow;
                }

            }
            return null;
            #region Варианты ссылок
            // -------------- Еще вариант

            //MainWindow FonWait = (MainWindow)App.Current.MainWindow;
            //foreach (System.Windows.Window ownedWindow in FonWait.OwnedWindows)
            //{
            //    if (ownedWindow.Name == NameWindow)
            //    {
            //        // Пример кода
            //        // ownedWindow.Hide();
            //        // ownedWindow.Close();
            //        return ownedWindow;
            //    }

            //}


            // -------------- Еще вариант
            //StringBuilder sb = new StringBuilder();

            //foreach (System.Windows.Window openWindow in System.Windows.Application.Current.Windows)
            //{

            //sb.AppendLine(openWindow.Title);

            //}

            //System.Windows.MessageBox.Show(sb.ToString(), "«Open Windows»");
            #endregion

        }
        #endregion



        #region Функции для управления директориями и файлами
        /// <summary>
        /// Проверка дириктории (Typefunc - 0 - создание , 1 - удаление , 2 - очистка от всех файлов, изменение)<para></para>
        /// Управление каталогами по заданому пути path;
        /// </summary>
        /// <param name="path"></param>
        /// <param name="Typefunc"></param>
        /// <returns></returns> 
        public static bool DIR_(string path, int Typefunc = 0)
        {
            // 
            if (!(Directory.Exists(path)))
            {

                switch (Typefunc)
                {
                    default:
                        // Создание директории
                        try
                        {
                            Directory.CreateDirectory(path);
                        }
                        catch (Exception error)
                        {
                            //C:\Users\ADM-o.zhakulin\Documents\Conecto\Export
                            ErorDebag("Ошибка проверки директории: " + path + ";  Сообщение системы: " + error.Message);
                            // Дополнительная логика проверки создания директории
                            if (CheckDirectory(path))
                            {
                                return true;
                            }


                            return false;
                        }

                        break;
                }


            }
            else
            {
                switch (Typefunc)
                {
                    case 1:
                    case 2:
                        // Асинхронное удаление каталога
                        StructCache_FileDelete ArgumentsTh = new StructCache_FileDelete
                        {
                            dir = path,
                            CreateDir = Typefunc == 2 ? true : false
                        };

                        Thread thStartDelDir = new Thread(FileDelete_Cache);
                        thStartDelDir.SetApartmentState(ApartmentState.STA);
                        thStartDelDir.IsBackground = true; // Фоновый поток
                        thStartDelDir.Priority = ThreadPriority.Lowest;
                        thStartDelDir.Start(ArgumentsTh);

                        break;
                        //default:
                        //    // Директория есть
                        //    break;
                }
            }
            return true;
        }

        #region Создаем уникальную директорию
        /// <summary>
        ///  Создать уникальную директорию по пути<para></para>
        ///  по умолчанию: Старт приложения ... \Temp\
        ///  return - { Путь к созданной директории, имя  каталога уникальное значение }
        /// </summary>
        /// <returns></returns>
        public string[] DIR_CreateUnic(string PuthDirUnic = "")
        {

            PuthDirUnic = PuthDirUnic == "" ? PreOrderConection.PStartup + @"\Temp\" : PuthDirUnic;
            string PatchTmpUnic = "";
            bool dircr = true;
            string valuenewkey = "";
            while (dircr)
            {
                Random rnd = new Random(DateTime.Now.Millisecond);

                valuenewkey = rnd.Next(10000000).ToString();

                if (!Directory.Exists(PuthDirUnic + valuenewkey)) //
                {
                    //SystemConecto.ErorDebag("========================  " + fi.Name + " / " + valuekey + "/" + valuenewkey, 1);

                    PatchTmpUnic = PuthDirUnic + valuenewkey;
                    Directory.CreateDirectory(PatchTmpUnic);
                    dircr = false;
                }

            }
            return new string[2] { PatchTmpUnic, valuenewkey };
        }
        #endregion

        /// <summary>
        /// Проверка файла (Typefunc - создание = 0, удаление, изменение, 
        /// 4 - создать но вернуть результат проверки, 5-проверка без создания, 6-блокировка для безопасности)
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <param name="Typefunc"></param>
        /// <param name="LinkFile"></param>
        /// <returns></returns>
        public static bool File_(string path, int Typefunc = 0)
        {
            /// Проверка файла (Typefunc - создание = 0, удаление, изменение, 
            /// 4 - создать но вернуть результат проверки, 5-проверка без создания)
            if (!(File.Exists(path)))
            {
                try
                {
                    if (Typefunc < 5)
                    {
                        using (FileStream NewFile = File.Create(path))
                        {
                            NewFile.Close();
                            if (Typefunc == 4)
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }

                }
                catch (Exception error)
                {
                    ErorDebag("Ошибка проверки файла: " + path + ";  Сообщение системы: " + error.Message);
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Блокировка записи файла:
        /// </summary>
        /// <param name="path"></param>
        /// <param name="LinkFile"></param>
        /// <returns></returns>
        public static bool File_Block(string path, ref FileStream LinkFile)
        {
            /// Проверка файла  6-блокировка для безопасности
            if ((File.Exists(path)))
            {
                try
                {

                    LinkFile = File.Open(path, FileMode.Open, FileAccess.Write, FileShare.None); // FileStream
                    // LinkFile = NewFile;
                    //using (FileStream NewFile = File.Open(path, FileMode.Open, FileAccess.Write, FileShare.None))
                    //{
                    // Файл блокируется внутри конструкции using 

                    // ErorDebag("Хай", 2);
                    //}
                    // LinkFile = new StreamWriter(path, true);

                }
                catch (Exception error)
                {
                    ErorDebag("Ошибка блокировки записи файла: " + path + ";  Сообщение системы: " + error.Message);
                    return false;
                }
            }

            return true;
        }


        #region Удаление директории с файлами

        /// <summary>
        /// Структура передаваемых данных для потока удаления файлов
        /// </summary>
        public struct StructCache_FileDelete
        {
            public string dir { get; set; }
            public bool CreateDir { get; set; }

        }
        /// <summary>
        /// Асинхронное удаление с поддержкой кеша
        /// </summary>
        /// <param name="ThreadObj"></param>
        public static void FileDelete_Cache(object ThreadObj)
        {

            PreOrderConection.StructCache_FileDelete arguments = (PreOrderConection.StructCache_FileDelete)ThreadObj;
            try
            {
                Thread.Sleep(2000);
                Directory.Delete(arguments.dir, true); //true - если директория не пуста (удалит и файлы и папки)
                // Создать директорию заново
                if (arguments.CreateDir)    // arguments.CreateDir != null && 
                {
                    DIR_(arguments.dir);
                }
            }
            catch (Exception error)
            {
                ErorDebag(error.Message);
            }
        }

        #endregion

        #region  Резерв и разработки private

        // Удаление файла сносителя локально асинхронно
        // try
        //{
        //    // Перенести как свойство в Compress - Так как функция асинхронна
        //    // File.Delete(PutchApp + @"tmp\" + NameFile + ".gz");
        //}
        //catch
        //{

        //}

        private void Write(string path, int Typefunc = 0)
        {
            // Резервная процедура

        }
        #endregion

        #endregion



        #region Проверка или установка системных шрифтов (Regular)
        public static bool IsFontInstalled(string NameFont = "Arial")
        {
            //get
            //{
            bool result;
            using (InstalledFontCollection installedFontCollection = new InstalledFontCollection())
            {
                // System.Windows.Media.FontFamily
                System.Drawing.FontFamily[] fontFamilies = installedFontCollection.Families;
                System.Drawing.FontFamily ff = fontFamilies.FirstOrDefault(f => f.Name == NameFont);
                result = ff != null; //&& ff.IsStyleAvailable(FontStyle.Regular)
            }
            return result;
            //}
        }

        #endregion




        #region Управление в ОС Windows Выключением Компьютера

        //[DllImport("user32.dll")]
        //public static extern int ExitWindowsEx(int uFlags, int dwReason);

        //импортируем API функцию InitiateSystemShutdown
        [DllImport("advapi32.dll", EntryPoint = "InitiateSystemShutdownEx")]
        static extern int InitiateSystemShutdown(string lpMachineName, string lpMessage, int dwTimeout, bool bForceAppsClosed, bool bRebootAfterShutdown);
        //импортируем API функцию AdjustTokenPrivileges
        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool AdjustTokenPrivileges(IntPtr htok, bool disall,
        ref TokPriv1Luid newst, int len, IntPtr prev, IntPtr relen);
        //импортируем API функцию GetCurrentProcess
        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern IntPtr GetCurrentProcess();
        //импортируем API функцию OpenProcessToken
        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool OpenProcessToken(IntPtr h, int acc, ref IntPtr phtok);
        //импортируем API функцию LookupPrivilegeValue
        [DllImport("advapi32.dll", SetLastError = true)]
        internal static extern bool LookupPrivilegeValue(string host, string name, ref long pluid);
        //импортируем API функцию LockWorkStation
        [DllImport("user32.dll", EntryPoint = "LockWorkStation")]
        static extern bool LockWorkStation();




        //объявляем структуру TokPriv1Luid для работы с привилегиями
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct TokPriv1Luid
        {
            public int Count;
            public long Luid;
            public int Attr;
        }

        //объявляем необходимые, для API функций, константые значения, согласно MSDN
        internal const int SE_PRIVILEGE_ENABLED = 0x00000002;
        internal const int TOKEN_QUERY = 0x00000008;
        internal const int TOKEN_ADJUST_PRIVILEGES = 0x00000020;
        internal const string SE_SHUTDOWN_NAME = "SeShutdownPrivilege";

        //функция SetPriv для повышения привилегий процесса
        private static void SetPriv()
        {
            TokPriv1Luid tkp; //экземпляр структуры TokPriv1Luid 
            IntPtr htok = IntPtr.Zero;
            //открываем "интерфейс" доступа для своего процесса
            if (OpenProcessToken(GetCurrentProcess(), TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, ref htok))
            {
                //заполняем поля структуры
                tkp.Count = 1;
                tkp.Attr = SE_PRIVILEGE_ENABLED;
                tkp.Luid = 0;
                //получаем системный идентификатор необходимой нам привилегии
                LookupPrivilegeValue(null, SE_SHUTDOWN_NAME, ref tkp.Luid);
                //повышем привилигеию своему процессу
                AdjustTokenPrivileges(htok, false, ref tkp, 0, IntPtr.Zero, IntPtr.Zero);
            }
        }
        /// <summary>
        /// публичный метод для перезагрузки/выключения машины
        /// (true, false)   //мягкая перезагрузка
        /// (true, true)    //жесткая перезагрузка
        /// (false, false)  //мягкое выключение
        /// (false, true)   //жесткое выключение
        /// </summary>
        /// <param name="RebootSh"></param>
        /// <param name="ForceOff">форсировать выключение</param>
        /// <returns></returns>
        public static int ShutdownPC(bool RebootSh, bool ForceOff)
        {
            SetPriv(); //получаем привилегию для своего приложения
            //вызываем функцию InitiateSystemShutdown, передавая ей необходимые параметры
            return InitiateSystemShutdown(null, null, 0, ForceOff, RebootSh);
            // 1 - Если lpMachineName является NULL или пустую строку, функция отключается на локальном компьютере.
            // 2 - Сообщение, которое будет отображаться в диалоговом окне завершения работы. Этот параметр может быть NULL, если сообщение не требуется.
            // 3 - Если dwTimeout равна нулю, компьютер выключается без отображения диалогового окна, а отключение не может быть остановлен AbortSystemShutdown .
            // 4 - 

            // Чтобы выключить удаленный компьютер, вызывающий поток должен иметь SE_REMOTE_SHUTDOWN_NAME привилегии на удаленном компьютере.
            // если возвращает - ERROR_NOT_READY - В этом случае приложение должно подождать некоторое время и повторите вызов.
        }

        /// <summary>
        /// Блокировка компьютера с помощью вывода приглашения ввести пароль пользователя ОС Windows (публичный метод для блокировки операционной системы)
        /// </summary>
        /// <returns></returns>
        public static int Lock()
        {
            if (LockWorkStation())
                return 1;
            else
                return 0;
        }

        /// <summary>
        /// Тестовое выключение компьютера с помощью (WMI - System.Management)
        /// Возможно удаленное выключение компьютера
        /// </summary>
        public void ShutDownComputer()
        {
            ManagementBaseObject outParameters = null;
            ManagementClass sysOS = new ManagementClass("Win32_OperatingSystem");
            sysOS.Get();
            // enables required security privilege.
            sysOS.Scope.Options.EnablePrivileges = true;
            // get our in parameters
            ManagementBaseObject inParameters = sysOS.GetMethodParameters("Win32Shutdown");

            //0 = Log off the network.
            //1 = Shut down the system.
            //2 = Perform a full reboot of the system.
            //4 = Force any applications to quit instead of prompting the user to close them.
            //8 = Shut down the system and, if possible, turn the computer off.

            inParameters["Flags"] = "1";
            inParameters["Reserved"] = "0";
            foreach (ManagementObject manObj in sysOS.GetInstances())
            {
                outParameters = manObj.InvokeMethod("Win32Shutdown", inParameters, null);
            }
        }


        #endregion

        #region Управление сканированием группы портов в сети
        /// <summary>
        ///  Управление сканированием группы портов в сети
        /// </summary>
        /// <param name="ips"></param>
        /// <param name="StartPort"></param>
        /// <param name="EndPort"></param>
        public static Dictionary<int, string> NetScan(IPAddress ips, int StartPort, int EndPort = 0)
        {

            /// Таблица портов http://ru.wikipedia.org/wiki/%D0%A1%D0%BF%D0%B8%D1%81%D0%BE%D0%BA_%D0%BF%D0%BE%D1%80%D1%82%D0%BE%D0%B2_TCP_%D0%B8_UDP

            Dictionary<int, string> InfoPortNet = new Dictionary<int, string>();
            TcpClient TcpScan = new TcpClient();

            // ------------- Один порт
            if (EndPort == 0)
            {

                //ErorDebag();
                // string host = "localhost";

                // int port = 6900;

                // IPAddress addr = (IPAddress)Dns.GetHostAddresses(host)[0];

                try
                {
                    // Проверка локальных портов на локальных адресах 
                    // TcpListener tcpList = new TcpListener(ips, StartPort);
                    // tcpList.Start();
                    // Попытка подключения
                    //TcpScan.Connect(ips, StartPort);
                    //InfoPortNet[StartPort] = "True";

                    // Проверка
                    if (!TcpScan.Connected)
                    {
                        // Попытка подключения
                        TcpScan.ConnectAsync(ips, StartPort);
                        InfoPortNet[StartPort] = "True";
                        // Если не сработало исключение, мы можем сказать, что порт открыт
                        TcpScan.Close();
                    }
                    else
                    {
                        InfoPortNet[StartPort] = "True";
                    }

                }

                catch (SocketException sx)
                {

                    ErorDebag(sx.ToString());
                    // Catch exception here if port is blocked
                    InfoPortNet[StartPort] = sx.ToString();
                }
            }
            else
            {
                // ----------------------- Много портов  
                // Проганяем через порты между портом начала и портом окончания
                for (int CurrPort = StartPort; CurrPort <= EndPort; CurrPort++)
                {

                    try
                    {
                        // Проверка
                        if (!TcpScan.Connected)
                        {
                            // Попытка подключения
                            TcpScan.ConnectAsync(ips, CurrPort);
                            InfoPortNet[CurrPort] = "True";
                            // Если не сработало исключение, мы можем сказать, что порт открыт
                            TcpScan.Close();
                        }
                        else
                        {
                            InfoPortNet[CurrPort] = "True";

                        }

                    }
                    catch (SocketException sx)
                    {
                        // Сработало исключение, порт для нас закрыт
                        ErorDebag(sx.ToString());
                        // Catch exception here if port is blocked
                        InfoPortNet[StartPort] = sx.ToString();
                    }

                }


            }
            return InfoPortNet;

        }
        #endregion

        //Режимы выключения программы и компьютера

        #region Осмотр сети для Администратора
        /// <summary>
        ///  Осмотр сети (в администрировании кнопка Осмотреть сеть)
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static Dictionary<string, string> ViewNet(int Type = 0)
        {
            // Возвращвет масив данных: Параметр - данные
            Dictionary<string, string> InfoViewNet = new Dictionary<string, string>();
            Dictionary<int, string> IpPrtNet_ml = new Dictionary<int, string>();        // Отсканированные порты
            IPAddress IpAdrr_ml = IPAddress.Parse("127.0.0.1");                         // Айпи Адресс для методов

            // Переопределить масив (очистить)
            InfoPingNet = new Dictionary<string, string>();
            locker_a = new int[2] { 0, 0 };

            // Имя пользователя System.Environment.UserName

            // Проверяем сеть Сеть делится на два типа: Инфраструктура всей сети и сети системы
            // Сеть системы работает с определнными портами:
            //      - порт программы
            // 3050 - Сервер БД Feirb. Для точной проверки запрос на алиас БД 
            // Вся сеть отвечает на порты:
            var IPDevice = IP_DeviceCcurent();
            foreach (KeyValuePair<string, string> dani in IPDevice)
            {

                // Чтения адаптера ID который подключен к сети
                if (NetworkPC[dani.Key + "_STATUS"] == "Up")
                {
                    // ErorDebag("Отладка: Сети " + NetworkPC[dani.Key + "_Description"] + " | " + NetworkPC[dani.Key + "_IP"] + " | " + NetworkPC[dani.Key + "_MAC-ADRESS"] + " | " + NetworkPC[dani.Key + "_TypeInterf"]);
                    // Проходим по адресам локальной группы
                    IpAdrr_ml = IPAddress.Parse(NetworkPC[dani.Key + "_IP"]);
                    var IpSplit = IpAdrr_ml.ToString();
                    var IpSplit_a = IpSplit.Split('.');
                    // Подсети и VPN
                    var IPGoup_a = new String[6] { "1", "2", "3", "100", "23", "123" };
                    if (Array.IndexOf(IPGoup_a, IpSplit_a[2]) < 0)
                    {
                        Array.Resize(ref IPGoup_a, IPGoup_a.Length + 1);
                        IPGoup_a[6] = IpSplit_a[2];
                    }



                    foreach (string DaniGr in IPGoup_a)
                    {
                        // Проверяем группу адресов в кторой находится компьютер
                        if (DaniGr == IpSplit_a[2])
                        {
                            // Смотрим сеть. Маркируем потоки в масиве
                            Thread[] threads = new Thread[256];
                            for (int i = 1; i < 256; i++)
                            {
                                // Формирование адресса
                                IpSplit = IpSplit_a[0] + "." + IpSplit_a[1] + "." + DaniGr + "." + i.ToString();
                                RenderInfo Arguments = new RenderInfo() { argument1 = IpSplit, argument2 = i.ToString(), argument3 = NetworkPC[dani.Key + "_TypeInterf"] };
                                Thread th_ip = new Thread(PingNet_Th);
                                threads[i] = th_ip;
                                threads[i].SetApartmentState(ApartmentState.STA);
                                threads[i].IsBackground = true; // Фоновый поток
                                threads[i].Start(Arguments);
                                //Ждем окончания последнего потока
                                if (i == 255)
                                {
                                    threads[i].Join();
                                    // System.Windows.Forms.MessageBox.Show("Отладка: " + locker_a[0].ToString() + " | " + locker_a[1].ToString());

                                    // Отследить выполнение потоков (пробую заменить while for -ом)
                                    for (int iEnd = 0; iEnd < 2; iEnd++)
                                    {
                                        if (locker_a[0] == locker_a[1])
                                        {
                                            // Потоки завершины
                                            foreach (KeyValuePair<string, string> DeviceDani in InfoPingNet)
                                            {
                                                // System.Windows.Forms.MessageBox.Show("Отладка: " + DeviceDani);
                                                // ErorDebag("Память: " + NetworkPC[dani.Key + "_Description"] + " | " + DeviceDani);
                                                InfoViewNet[DeviceDani.Key] = DeviceDani.Value;
                                            }


                                            iEnd = 2;
                                        }
                                        else
                                        {
                                            // Продолжить
                                            iEnd = 0;
                                        }

                                    }
                                }

                                // IpPrtNet_ml = NetScan(IpAdrr_ml, 3050);
                                //foreach (KeyValuePair<int, string> daniPort in IpPrtNet_ml)
                                //{
                                //   if (daniPort.Value == "True")
                                //   {
                                //        ErorDebag("Отладка: " + daniPort.Key + " | Есть сервер");
                                //   }
                                //}

                            }
                        }
                    }

                }

            }
            return InfoViewNet;
        }
        #endregion

        #region Управление потоками -Thread глобально в системе v1.2


        /// <summary>
        /// Объект кторый  можно передать потоку в многопотоковой среде например структуру данных
        /// </summary>
        public delegate void ParameterizedThreadStart(object ThreadObj);
        /// <summary>
        /// Структура данных для многопотоковой среды (передача аргументов)
        /// </summary>
        struct RenderInfo
        {
            public string argument1 { get; set; }
            public string argument2 { get; set; }
            public string argument3 { get; set; }
        }
        //public static IPAddress ips_s { get; set; } // Айпи адресс кторый запрашивают в многопотоковой среде
        /// <summary>
        /// Код, защищенный таким образом от неопределённости в плане многопотокового исполнения, называется потокобезопасным. 
        /// Все потоки при записи борются за блокировку объекта
        /// </summary>
        static object locker1 = new object();
        static object locker2 = new object();
        /// <summary>
        /// Отслеживание выполнения потоков locker[0] -количество зарегистрировавшихся потоков, 
        /// locker[1] - количество потоков кторые завершили выполнятся
        /// </summary>
        public static int[] locker_a = new int[2] { 0, 0 };
        //static int[] locker_a { get; set; }

        // Пример использования
        // RenderInfo Arguments = new RenderInfo() { argument1 = IpSplit, argument2 = i.ToString(), argument3 = NetworkPC[dani.Key + "_TypeInterf"] };
        // Thread th_ip = new Thread(PingNet_Th);
        // threads[i] = th_ip;
        // threads[i].SetApartmentState(ApartmentState.STA);
        // threads[i].IsBackground = true; // Фоновый поток
        // threads[i].Start(Arguments);
        #endregion

        #region IPPing Device -Thread
        /// <summary>
        /// Масив для IP адрессов сети в многопоточной среде {IP адресс} {Время ответа}
        /// </summary>
        public static Dictionary<string, string> InfoPingNet = new Dictionary<string, string>();

        /// <summary>
        /// Эхо запрос в сети в многопотоковой среде
        /// отсутствуют параметры и результат вывода
        /// </summary>
        private static void PingNet_Th(object ThreadObj)
        {
            // Отслеживание ошибок в птоке для многопотоковой среде 
            try
            {
                // Разбор аргументов
                RenderInfo arguments = (RenderInfo)ThreadObj;
                var ips_s = IPAddress.Parse(arguments.argument1);   // Айпи адресс
                var ThreadNumber = arguments.argument2;             // Номер потока
                var TypeInterf = arguments.argument3;               // Тип интерфейса, сетевого адаптера (Ppp - VPN)
                // Регистрация потока
                lock (locker1)
                {
                    locker_a[0] = locker_a[0] + 1;
                }


                var timeout = TypeInterf == "Ppp" ? 3000 : 290;      // Доступ к другим сетям больше затрачивает времени по доступу
                var buffer = new byte[] { 0, 0, 0, 0 };

                // создаем и отправляем ICMP request
                var ping = new Ping();
                // TTL (время жизни) IP-пакетов
                // у разных операционных систем TTL по умолчанию в пределе от 32 до 128, так например у Linux-систем ttl по умолчанию равно 64, а у Windows - 128, но значение это четное
                var reply = ping.Send(ips_s, timeout, buffer, new PingOptions { Ttl = 128 });

                // если ответ успешен
                if (reply.Status == IPStatus.Success)
                {
                    lock (locker2)
                    {
                        // ErorDebag("Отладка потока: поток " + ThreadNumber +" " + reply.Address + " | " + reply.RoundtripTime + "|" + ConvertIpToMAC(ips_s));
                        InfoPingNet[reply.Address.ToString() + "_IP"] = reply.RoundtripTime.ToString();
                        InfoPingNet[reply.Address.ToString() + "_MAC"] = ConvertIpToMAC(ips_s);
                        // NetBios имя компьютера (замедляет работу, может заменить на протокол ConectoNet)
                        string NameHost_ = "";
                        try
                        {
                            var DnsName = Dns.GetHostEntry(ips_s);
                            NameHost_ = DnsName.HostName;
                        }
                        catch  //(SocketException ExDns) ускоряет код
                        {
                            // Частое исключение System.Net.Sockets.SocketException (0x80004005): Этот хост неизвестен
                            // ErorDebag("Отладка: " +ExDns.ToString());
                        }

                        InfoPingNet[reply.Address.ToString() + "_NETBIOS"] = NameHost_;
                        //Пробуем определить поддерживает ли объект SNMP если нет то FALSE
                        //SNMPObject ob = new SNMPObject("1.3.6.1.2.1.1.5.0");
                        //try
                        //{

                        //    string agent = ob.getSimpleValue(new SNMPAgent(IPClassAddres.IPAddres));

                        //}
                        //catch { }
                        // Завершение потока
                        locker_a[1] = locker_a[1] + 1;
                    }
                }
                else
                {
                    // Закончилось не удачей (конец времени или ...)
                    lock (locker2)
                    {
                        locker_a[1] = locker_a[1] + 1;
                    }
                }

            }
            catch (Exception ex)
            {
                // Отследить ошибки
                lock (locker1)
                {
                    ErorDebag(ex.ToString());
                }
            }


        }
        #endregion

        #region IPPing Device -Return {Изменил под функцию, проверить в соответствии с  IPPing Device -Thread}
        /// <summary>
        /// Пинг устройства в сети
        /// </summary>
        /// <param name="ips"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        private static bool PingNet(IPAddress ips, int Type = 0)
        {
            // Отслеживание ошибок 
            try
            {
                // Dictionary<string, long> InfoPingNet_ = new Dictionary<string, long>();
                var timeout = 9220;
                var buffer = new byte[] { 0, 0, 0, 0 };

                // создаем и отправляем ICMP request
                var ping = new Ping();
                var reply = ping.Send(ips, timeout, buffer, new PingOptions { Ttl = 128 });

                // если ответ успешен
                if (reply.Status == IPStatus.Success)
                {
                    //ErorDebag("Отладка: " + reply.Address + " | " + reply.RoundtripTime);
                    return true;
                }
                else
                {
                    ErorDebag(reply.Address + " | " + reply.Status, 1);
                }
                // return InfoPingNet_; Dictionary<string, long>
            }
            catch (Exception ex)
            {
                // Отследить ошибки
                ErorDebag(ex.ToString());
            }
            return false;

        }
        #endregion

        /// <summary>
        /// проверить подключение к Интернету 
        /// </summary>
        public class InternetAvailability
        {
            [System.Runtime.InteropServices.DllImport("wininet.dll")]
            private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

            //Creating a function that uses the API function...
            /// <summary>
            /// Функция проверки через Windows API
            /// </summary>
            /// <returns></returns>
            public static bool IsConnectedToInternet()
            {
                int Desc;
                return InternetGetConnectedState(out Desc, 0);
            }

        }



        #region MAC Device -Return
        // Управление сетевыми запросами (Для определения MAC-адресса удаленной машины)
        // Расположение C:\Windows\System32 (C:\Windows\winsxs\x86_microsoft-windows-t..-platform-libraries_31bf3856ad364e35_6.1.7600.16385_none_ea474108fca1c083)
        [DllImport("iphlpapi.dll", ExactSpelling = true)]
        public static extern int SendARP(int DestIP, int SrcIP, [Out] byte[] pMacAddr, ref int PhyAddrLen);
        /// <summary>
        /// Трансформация Ip адресса в MAC адресс с таблицы сетевых адаптеров
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static string ConvertIpToMAC(IPAddress ip, int Type = 0)
        {
            byte[] ab = new byte[6];
            int len = ab.Length;
            int r = SendARP(ip.GetHashCode(), 0, ab, ref len);
            // BitConverter.ToString(ab, 0, 6); // конвертирование в формат 00-00-00-00-00
            //ab.ToString()
            return BitConverter.ToString(ab, 0, 6); ;
            // System.Net.NetworkInformation.PhysicalAddress();
        }
        #endregion

        #region Обзор сетевых интерфейсов и их свойств {v 1.2}

        #region Определение настроек сетевого подключения Структура: 0 - имя компьютера NETBIOS; 1 - _IP (айпи адрес адаптера) {v 1.2};
        /// <summary>
        ///  Определение настроек сетевого подключения <para></para>
        ///  Структура: 0 - имя компьютера NETBIOS; <para></para>
        ///  1 - _IP (айпи адрес адаптера); <para></para>
        ///  3 -_SCHLUZ (шлюз группы сети); <para></para>
        ///  4 - _MAC-ADRESS (вытаскиваем и показываем MAC-адрес (физический адрес адаптера));<para></para>
        ///  5 - _Description (Описание адаптера);<para></para>
        /// NoLoopback: - включения в список адаптеров Loopback (127.0.0.1)
        /// <returns></returns>
        /// </summary>
        public static Dictionary<string, string> IP_DeviceCcurent(bool NoLoopback = false)
        {
            //(только для одного адаптера, обновил для всех адаптеров)

            // для работы нужно импортировать пространство имен System.Net
            // using System.Net;
            // using System.Net.NetworkInformation;
            // using System.Net.Sockets;
            Dictionary<string, string> IPDevice = new Dictionary<string, string>();

            // Получаем список сетевых интерфейсов. интерфейсы могут быть физические (ethernet,
            // wifi и т.п.), и программные/виртуальные, к примеру, VPN-интерфейсы.
            // Если наберете в командной строке «cmd» команду «ipconfig /all», то сетевой
            // интерфейс там будет показан адаптером.

            var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (var networkInterface in networkInterfaces)
            {
                // у каждого интерфейса выбираем его IP-адреса, если они у него есть
                foreach (var address in networkInterface.GetIPProperties().UnicastAddresses)
                {
                    // Есть еще возможность проверить тип интерейса
                    // для каждого интерфейса Ethernet
                    // if (networkInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet)


                    NoLoopback = IPAddress.IsLoopback(address.Address) && !NoLoopback ? false : true; // (IPAddress.IsLoopback(address.Address) && NoLoopback ? true : true);

                    //мне нужны только IPv4-адреса, также исключаю loopback-адреса (127.0.0.1), (т.к. и так понятно, что он в любом случае есть.)
                    if (address.Address.AddressFamily == AddressFamily.InterNetwork && NoLoopback)
                    {
                        //MessageBox.Show("IP-" + address.Address.ToString());
                        IPDevice[networkInterface.Id] = "";
                        // Структура: 0 - имя компьютера NETBIOS; 1 - _IP (айпи адрес адаптера);
                        // 3 -_SCHLUZ (шлюз группы сети); 4 - _MAC-ADRESS (вытаскиваем и показываем MAC-адрес (физический адрес адаптера));
                        // 5 - _Description (Описание адаптера);
                        // получаем хост Запись в память лишняя и так читаем с памяти
                        // NetworkPC[networkInterface.Name+"_HOST"] = Dns.GetHostName();
                        // 1 - айпи адрес
                        NetworkPC[networkInterface.Id + "_IP"] = address.Address.ToString();
                        // 2 - Тип интерфейса (резерв)
                        NetworkPC[networkInterface.Id + "_TypeInterf"] = networkInterface.NetworkInterfaceType.ToString();

                        // 3 - шлюз
                        foreach (var SCHLU_address in networkInterface.GetIPProperties().GatewayAddresses)
                        {
                            NetworkPC[networkInterface.Id + "_SCHLUZ"] = SCHLU_address.Address.ToString();
                        }

                        //foreach (var SCHLU_address in networkInterface.GetIPProperties().GatewayAddresses)
                        //{
                        //    NetworkPC[networkInterface.Id + "_SCHLUZ"] = SCHLU_address.ToString();
                        //    break;
                        //}
                        // 4 - вытаскиваем и показываем MAC-адрес (физический адрес адаптера)
                        var MACaddress = networkInterface.GetPhysicalAddress().ToString();

                        // int MACaddress_ = int.Parse(MACaddress); // кодировка в цифру
                        // BitConverter.ToString(Encoding.Unicode.GetBytes(MACaddress), 0, 6); // кодировка в байты
                        // BitConverter.ToString(networkInterface.GetPhysicalAddress().GetAddressBytes(), 0, 6) // конвертирование в формат 00-00-00-00-00

                        NetworkPC[networkInterface.Id + "_MAC-ADRESS"] = !string.IsNullOrWhiteSpace(MACaddress) ? MACaddress : "";
                        // 5 - Описание адаптера
                        NetworkPC[networkInterface.Id + "_Description"] = networkInterface.Description.ToString();


                        // 6 - состояние соединения
                        NetworkPC[networkInterface.Id + "_STATUS"] = networkInterface.OperationalStatus.ToString();
                        // 7 - Тип адреса Статика, DHCP
                        NetworkPC[networkInterface.Id + "_DCHP"] = networkInterface.GetIPProperties().IsDynamicDnsEnabled.ToString();
                        // 8 - скорость соединения



                    }
                }
            }


            //// получаем хост
            //IPDevice[0] = Dns.GetHostName();

            //// получаем IP-адрес хоста
            //IPAddress[] ips = Dns.GetHostAddresses(IPDevice[0]);
            //foreach (IPAddress ip in ips)
            //{
            //    IPDevice[1] = ip.ToString();
            //}
            //string[] aIPGroup = IPDevice[1].Split('.');
            ///// Получаем пред-последний елемент
            //// int length = aIPGroup.Length - 1;
            //IPDevice[2] = aIPGroup[aIPGroup.Length - 1];

            // Не рекомендовано - System.Net.Dns.GetHostByName(myHost).AddressList[0].ToString();

            // получение IP по dns-имени
            //var hostEntry = Dns.GetHostEntry(«www.yandex.ru»);
            //foreach (var address in hostEntry.AddressList)
            //   Console.WriteLine(address);

            //// обратная операция
            //var hostEntry = Dns.GetHostEntry(«212.48.193.37″);
            //Console.WriteLine(hostEntry.HostName); 

            return IPDevice;
        }
        #endregion

        #region Логический метод анализирующий работу сетевых адаптеров
        /// <summary>
        /// Логический метод анализирующий работу сетевых адаптеров 
        /// (адаптеров которые включенны в панели управления, - состояние включенный! Выключенные адаптеры отсутствуют в проверке)
        /// Результат 0- количество адаптеров , 1- включенных адаптеров, <para></para>
        /// 2 - количество выключенных адаптеров  WiFi<para></para>
        /// 3 - количество выключенных адаптеров Ppp <para></para>
        /// 4 - количество выключенных адаптеров Ethernet<para></para>
        /// 5 - количество выключенных других адаптеров<para></para>
        /// <returns>returns: цифровой масив</returns><para></para>
        /// </summary>
        public static int[] NetOff()
        {

            int[] NetOff_a = new int[6] { 0, 0, 0, 0, 0, 0 };

            // Перебор настроек сетевых адаптеров
            var IPDevice = IP_DeviceCcurent();
            foreach (KeyValuePair<string, string> dani in IPDevice)
            {
                // Отладка
                // ErorDebag(NetworkPC[dani.Key + "_TypeInterf"]);
                NetOff_a[0] = NetOff_a[0] + 1;
                // Чтения адаптера ID который подключен к сети
                if (NetworkPC[dani.Key + "_STATUS"] == "Up")
                {
                    NetOff_a[1] = NetOff_a[1] + 1;

                }
                else
                {
                    switch (NetworkPC[dani.Key + "_TypeInterf"])
                    {
                        case "Wireless80211":
                            NetOff_a[2] = NetOff_a[2] + 1;

                            break;
                        case "Ppp":
                            NetOff_a[3] = NetOff_a[3] + 1;

                            break;
                        case "Ethernet":
                            NetOff_a[4] = NetOff_a[4] + 1;

                            break;
                        default:
                            NetOff_a[5] = NetOff_a[4] + 1;

                            break;

                    }
                }

            }
            // ОТладка
            // ErorDebag(NetOff_a[4].ToString());
            return NetOff_a;
        }
        #endregion

        #region Проверка физического соединения со шлюзами локальной сети
        /// <summary>
        /// Проверка физического соединения со шлюзами локальной сети
        /// </summary>
        /// <returns>0-нет соединения, 1-есть соединение, 2-отсутствует шлюз при подключенном адапторе</returns>
        public static int NetGetwai()
        {
            int Conect = 0;
            int Conect_ = 0;
            var IPDevice = IP_DeviceCcurent();
            foreach (KeyValuePair<string, string> dani in IPDevice)
            {
                // Отладка
                // ErorDebag(NetworkPC[dani.Key + "_TypeInterf"]);

                // Чтения адаптера ID который подключен к сети
                if (NetworkPC[dani.Key + "_STATUS"] == "Up")
                {

                    // Проверка шлюза (шлюз может отсутствовать)
                    if (NetworkPC.ContainsKey(dani.Key + "_SCHLUZ"))
                    {
                        ErorDebag(NetworkPC[dani.Key + "_SCHLUZ"].Length.ToString());
                        if (NetworkPC[dani.Key + "_SCHLUZ"].ToString() == "0.0.0.0")
                        {
                            // 2-отсутствует шлюз при подключенном адапторе (широковещательное вещание работает)
                            // Возможно подключение через широковещательный шлюз проверяется пингом на сайт google.com
                            Conect_ = 2;
                        }
                        else
                        {
                            if (PingNet(IPAddress.Parse(NetworkPC[dani.Key + "_SCHLUZ"])))
                            {
                                Conect = 1;
                                break;
                            }
                        }
                    }

                }


            }
            // ОТладка
            // ErorDebag();
            // Проверка результатов, если выключенны все адаптеры, но есть включенный адаптер без шлюза сообщить об этом
            Conect = Conect == 0 ? Conect_ : Conect;
            return Conect;
        }
        #endregion

        #region Событие которое возникает при смене свойств сетевых адаптеров

        // При обнаружении смены настроек адаптеров или установки новых в режиме он лайн
        // Событие перенастраивает все сервера и службы системы 

        #endregion

        #endregion

        #region Удаленное подключение к Компьютеру

        public static void ConectToPC()
        {

            ConnectionOptions options = new ConnectionOptions
            {
                Username = "Администратор",
                Password = ""
            };
            //        connection.Authority = "ntlmdomain:";

            ManagementScope scope =
                new ManagementScope("\\\\192.168.1.231\\root\\cimv2", options);
            scope.Connect();

            ObjectQuery query = new ObjectQuery(
                "SELECT * FROM Win32_OperatingSystem");
            ManagementObjectSearcher searcher =
                new ManagementObjectSearcher(scope, query);

            ManagementObjectCollection queryCollection = searcher.Get();
            foreach (ManagementObject m in queryCollection)
            {
                //System.Windows.Forms.MessageBox.Show("Отладка: " + m["csname"].ToString() + " | " + m["WindowsDirectory"].ToString() +
                //    " | " + m["Caption"].ToString() + " | " + m["Manufacturer"].ToString());
                // Display the remote computer information

            }
            Console.ReadLine();

            // Рабочие станции под управлением Windows XP Professional и Vista, не подключенные к домену, по умолчанию не позволяют 
            // локальному администратору аутентифицироваться под собственным именем по сети. Вместо этого, используется политика 
            //"ForceGuest", которая означает, что все удаленные подключения производятся с правами гостевой учетной записи.
            // Однако, как уже было сказано, для сканирования требуются права администратора. Поэтому на каждом удаленном компьютере
            // требуется произвести настройку политики безопасности: "Пуск - Выполнить - secpol.msc - OK - Локальные политики - 
            //Параметры безопасности - Сетевой доступ: модель совместного доступа и безопасности..." - если она имеет значение "Гостевая", 
            // переключите на "Обычная" 

        }


        #endregion

        #region Wake-on-LAN Удаленного компьютора


        public class WOLClass : UdpClient
        {
            // ------------------ Start Class
            public WOLClass()
                : base()
            { }
            //Установим broadcast для отправки сообщений
            public void SetClientToBrodcastMode()
            {
                if (this.Active)
                    this.Client.SetSocketOption(SocketOptionLevel.Socket,
                                              SocketOptionName.Broadcast, 0);
            }
            //---------------- End Class
        }

        /// <summary>
        /// Включить компьютер в сети 
        /// MAC адрес должен выглядеть следующим образом: 013FA04912, а не 00-00-00-00-00
        /// </summary>
        /// <param name="MAC_ADDRESS"></param>
        public static void WakeUP(string MAC_ADDRESS)
        {
            WOLClass client = new WOLClass();
            client.Connect(new IPAddress(0xffffffff), 0x2fff); //Используем порт = 12287
            // Широковешательный способ
            client.SetClientToBrodcastMode();
            // На определенный адрес ... (сделать)

            int counter = 0;
            //буффер для отправки
            byte[] bytes = new byte[1024];
            //Первые 6 бит 0xFF
            for (int y = 0; y < 6; y++)
                bytes[counter++] = 0xFF;
            //Повторим MAC адрес 16 раз
            for (int y = 0; y < 16; y++)
            {
                int i = 0;
                for (int z = 0; z < 6; z++)
                {
                    bytes[counter++] = byte.Parse(MAC_ADDRESS.Substring(i, 2), NumberStyles.HexNumber);
                    i += 2;
                }
            }

            //Отправим магический пакет
            int reterned_value = client.Send(bytes, 1024);
        }



        // Описание задачи
        /*
         *      Управляемый компьютер находится в дежурном режиме (англ. stand-by) и выдаёт питание на сетевой адаптер. 
         * Сетевой адаптер находится в режиме пониженного энергопотребления, просматривая все пакеты, приходящие на его MAC-адрес, 
         * и ничего не отвечая на них. Если одним из пакетов окажется magic packet, сетевой адаптер выдаст сигнал на включение питания компьютера.
         * Magic packet — это специальная последовательность байтов, которую для нормального прохождения по локальным сетям можно вставить в пакеты UDP или IPX.
         * Есть два способа послать широковещательно и через маршрутизатор, запрещающий широковещательные пакеты, можно послать пакет по какому-то определённому адресу.
         * Правило построение, в начале пакета идет так называемая цепочка синхронизации: 6 байт, равных 0xFF. Затем — MAC-адрес сетевой платы, повторённый 16 раз.
         * 
         * На компьюторе необходимо установка в BIOS параметра Wake After Power Fail («пробуждаться после пропадания питания») в значение On («Вкл.»).
        */
        #endregion

        // Проверка работоспособности соедининий (с БД, сервером Conecto, Интернет и пр.) -Thread

        // Окно Wait в отдельном потоке -Thread


        #region Прочее

        // Частичная копия объекта
        public PreOrderConection ShallowCopy()
        {
            return (PreOrderConection)this.MemberwiseClone();
        }


        public void CompressUtility()
        {
            // Сжатие данных
        }
        public void ExtractUtility()
        {
            // Извлечение сжатых данных

        }


        #endregion

        #region Заметки на полях
        //        //Обновляем DataGrid
        //if (addDataGrid != null)
        //{
        //    string[] s = { IPClassAddres.IPAddres, host, pingReply.RoundtripTime.ToString(),string.Format("{0}",SNMP) };
        //    addDataGrid(s);
        //}
        #endregion

        // ---------------- New 

        #region Выполнить задачу в отдельном потоке -Thread
        ///// <summary>
        ///// Структура данных для многопотоковой среды (передача аргументов)
        ///// </summary>
        //struct RenderInfo
        //{
        //    public string argument1 { get; set; }
        //    public int    argument2 { get; set; }
        //    public string argument3 { get; set; }
        //}


        //public static void StartNewThread()
        //{
        //    // Передача параметров в виде структуры в другой поток
        //    // RenderInfo Arguments03 = new RenderInfo() { argument1 = SystemConecto.NetworkPC["1" + "_IP"] };
        //    // Thread thStartTimer03 = new Thread(StartWork);
        //    // thStartTimer03.SetApartmentState(ApartmentState.STA);
        //    // thStartTimer03.IsBackground = true; // Фоновый поток
        //    // thStartTimer03.Start(Arguments03);

        //}

        //public static void StartWork(object ThreadObj)
        //{
        //    
        // Разбор аргументов
        //RenderInfo arguments = (RenderInfo)ThreadObj;
        //var ips_s = IPAddress.Parse(arguments.argument1);   // Айпи адресс

        // Тело потока

        //}

        #endregion




        // ---------------------------------


        #region Расчитать размер рабочего стола экранна устройства - № 1 [WorkAreaDisplay]
        /// <summary>
        /// Расчитать размер рабочего стола экранна устройства - № 1 (View_DeviceSize)
        /// </summary>
        /// <returns></returns>
        public static double[] WorkAreaDisplay(Window MainWindow)
        {


            // === Возникает вопрос как работать с несколькими мониторами (это может быть платная функция)

            // Индекс погрешности 5 - ширины 6 - высоты public static
            double[] SizeDWArea = new double[8] { 0, 0, 0, 0, 0, 25, 14, 0 };

            // Бордер Окна MainWindow в низу по высоте 7 px
            // Рабочего стола ([0] - Top; [1] - Left; [2] - Width; [3] - Height; [4] - Right;
            // ConectoWorkSpace_InW.WindowState = WindowState.Maximized;
            WorkAreaDisplayOneDef.Top = SizeDWArea[0] = (MainWindow.WindowState == WindowState.Maximized ? 0 : SystemParameters.WorkArea.Top); // - 
                                                                                                                                               //(MainWindow.ResizeMode == ResizeMode.NoResize && MainWindow.WindowStyle == WindowStyle.None ? 0 : 7);
            WorkAreaDisplayOneDef.Left = SizeDWArea[1] = (MainWindow.WindowState == WindowState.Maximized ? 0 : SystemParameters.WorkArea.Left); // -
                                                                                                                                                 // (MainWindow.ResizeMode == ResizeMode.NoResize && MainWindow.WindowStyle == WindowStyle.None ? 0 : 7);
            WorkAreaDisplayOneDef.Width = SizeDWArea[2] = (MainWindow.WindowState == WindowState.Maximized ? SystemParameters.PrimaryScreenWidth : SystemParameters.WorkArea.Width) +
                            (MainWindow.ResizeMode == ResizeMode.NoResize ? 0 : SizeDWArea[5]);
            WorkAreaDisplayOneDef.Height = SizeDWArea[3] = (MainWindow.WindowState == WindowState.Maximized ? SystemParameters.PrimaryScreenHeight : SystemParameters.WorkArea.Height) +
                            (MainWindow.ResizeMode == ResizeMode.NoResize ? 0 : SizeDWArea[6]);
            WorkAreaDisplayOneDef.Right = SizeDWArea[4] = MainWindow.WindowState == WindowState.Maximized ? 0 : SystemParameters.WorkArea.Right;
            // [7] - Left вертикальной виртуальной черты, отделяемая правую сторону с информационными кнопками от левой 
            WorkAreaDisplayOneDef.IndexLeft = SizeDWArea[7] = SizeDWArea[2] - (277 + 5 + 10);

            WorkAreaDisplayOneDef.IndexHeightError = SizeDWArea[5];
            WorkAreaDisplayOneDef.IndexWidthError = SizeDWArea[6];

            // Отладка
            //SystemConecto.ErorDebag(SystemParameters.WorkArea.X.ToString() + "/" + SystemParameters.WorkArea.Y.ToString(), 1);
            //SystemConecto.ErorDebag(SystemParameters.WorkArea.Bottom.ToString() + "/" + SystemParameters.PrimaryScreenHeight.ToString(), 1);

            // Запись в систему оно же промежуточное значение
            // SizeDWAreaDef_aD = SizeDWArea;


            // Ширина 1361 1360, Высота 739 768 
            // Отладка
            // SystemConecto.ErorDebag(string.Format("Высота {0}, Слева {1}, Ширина {2}, Высота {3}", SystemParameters.WorkArea.Top, SystemParameters.WorkArea.Left,
            //    SystemParameters.WorkArea.Width,
            //    SystemParameters.WorkArea.Height), 1);



            return SizeDWArea;
        }
        #endregion

        #region Короткий путь для ОС Windows. Сокращение имен директорий и файлов к формату 8.3.

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern int GetShortPathName([MarshalAs(UnmanagedType.LPTStr)] string path, [MarshalAs(UnmanagedType.LPTStr)]
StringBuilder shortPath, int shortPathLength);


        /// <summary>
        /// Короткий путь для ОС Windows (исправление ошибки пробела в пути указанного в ручную),
        /// альтернатива var Puth_ = Path.Combine(@"D:\!Project\SDK ZGuard\", "ZGuard.dll");
        /// ссылка using System.IO;
        /// Подходит для приложений выпоняемых из командной строки.
        /// Не рекомендуется использовать!
        /// </summary>
        /// <param name="path">Путь</param>
        /// <returns>Короткий путь</returns>
        public static string GetShortPathName(string path)
        {
            // Определить версию ОС

            // Использую функцию Windows GetShortPathName из kernel32.dll
            System.Text.StringBuilder sb = new System.Text.StringBuilder(250); // Размер пути
            int res = GetShortPathName(path, sb, sb.Capacity);
            // ========== Ошибка формирования ....  (Разработка - RAB)
            // return (res > 0 && res < sb.Capacity) ? sb.ToString() : null;
            return sb.ToString();
        }

        #endregion

        #region Серийный номер материнской партии в Windows

        private void SNBoardDevice()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher
           ("SELECT Product, SerialNumber FROM Win32_BaseBoard");

            ManagementObjectCollection information = searcher.Get();
            foreach (ManagementObject obj in information)
            {
                foreach (PropertyData data in obj.Properties)
                {
                    MessageBox.Show(string.Format("{0} = {1}", data.Name, data.Value));
                }
                // listBoxControl1.Items.Add(string.Format("{0} = {1}", data.Name, data.Value));              
            }

        }

        #endregion


        #region Определение версии ОС [version 1.4]

        /// <summary>
        /// Код версии Windows знает или нет системный код об исключениях в ОС
        /// Environment.Is64BitOperatingSystem - Определяет разрядность ОС
        /// </summary>
        public static int OSWMI = 0;

        /// <summary>
        /// Определение разрядности ОС зарезервировано для разработок в разных средах Windows Mono и прочих.<para></para> 
        /// Возвращает также как и  Environment.Is64BitOperatingSystem
        /// 
        /// </summary>
        public static bool OS64Bit = false;

        public static int WMIOS()
        {

            // Определяет разрядность ОС
            try
            {

                OS64Bit = Environment.Is64BitOperatingSystem;

                // Так можно определить какой тип исполняемой среды (по умолчанию 32 bit IntPtr.Size == 4)
                //if (IntPtr.Size == 8)
                //{
                // Только для 64bit
                //}
            }
            catch
            {

            }

            string Version = "";

            string[] Version_ = Environment.OSVersion.Version.ToString().Split('.');

            int MaxLen = Version_.Length > 3 ? 3 : Version_.Length;
            for (int i = 0; i < MaxLen; i++)
            {
                Version = Version + (Version == "" ? "" : ".") + Version_[i];
            }

            #region Альтернатива Environment.OSVersion.Version с помощью WMI (Если исключение то WMI выключен)
            // ======== Нюанс состоит в том, что нужно проверять состояние службы WMI для получения результатов
            //string winos = "Select Name, Version from Win32_OperatingSystem";
            //ManagementObjectSearcher mos =
            //    new ManagementObjectSearcher(winos);

            //// Отладка разных свойст системы ОС Windows Память, Сетевая группа, Сервис Пак ОС Windows
            //// MessageBox.Show(string.Format("[Свойство {0}]", mos.ToString()));

            //// Из запроса берем только первую запись
            //foreach (ManagementObject mo in mos.Get())
            //{
            //    Version = mo["Version"].ToString();
            //}
            //// Отладка елементов
            ////foreach (PropertyData data in mo.Properties)
            ////{
            ////    MessageBox.Show(string.Format("[Свойство {0} = {1}]", data.Name, data.Value));
            ////}
            #endregion
            switch (Version)
            {
                case "6.1.7600":
                    // Windows 7 Ultimate 32-bit ломаная

                    return 10;
                case "6.1.7601":
                    // Windows 7 начальная    
                    // break;    


                    return 1;
                case "6.0.6002":
                    // Windows Vista Home Premium  
                    // break;    
                    return 2;
                case "5.1.2600":
                    // Windows XP Home SP3
                    // break;    
                    return 3;
                case "6.2.9200":

                    // Windows 8
                    return 4;
            }
            ErorDebag("Имя ОС: " + Version, PreOrderConection.ReleaseCandidate == "Release" ? 0 : 2);


            // Environment.GetEnvironmentVariable("NameVirable") - считывает значение переменной  в среде в виде текста.

            // Поиск класса не работает из dll в основном теле приложения!
            //Type SherchClass = Type.GetType("ConectoWorkSpace.AppStart");
            //if (SherchClass != null)
            //{
            //    //System.Reflection.MethodInfo loadAppEvents = typeof(ConectoWorkSpace.KaraokeServer).GetMethod(NameMetod, new Type[] { }); // typeof(void)
            //    System.Reflection.MethodInfo loadAppEvents = SherchClass.GetMethod("ErorDebag", new Type[] { typeof(string), typeof(int) });
            //    ;
            //    if (loadAppEvents != null)
            //    {
            //        // SystemConecto.ErorDebag("LoadAppEvents_" + IdApp, 2);
            //        loadAppEvents.Invoke(new object(), new object[] { "Имя ОС: " + Version, SystemConecto.ReleaseCandidate == "Release" ? 0 : 2 });
            //        // ErorDebag("Имя ОС: " + Version, SystemConecto.ReleaseCandidate == "Release" ? 0 : 2);
            //    }

            //}

            return -1;

        }
        #endregion





        #region Поиск устройства в списке Windows

        /// <summary>
        /// Поиск устройства в списке Устройств Windows (Адмнистрирование устройств - Панель управления)
        /// </summary>
        /// <param name="NameDevice">Имя устройства</param>
        public static void ListDeviceDriverName(string NameDevice)
        {

            int NumberDivece = 0;

            // Модемы
            //string query = "SELECT * FROM Win32_POTSModem";
            // COM-PORT - SerialPort
            //string query = "SELECT * FROM Win32_SerialPort";
            // Список всех подключаемых устройств по Plag and Play - Win32_PnPEntity - Условие {WHERE ConfigManagerErrorCode = 0}
            // работает без ошибок 
            string query = "SELECT * FROM Win32_PnPEntity";

            string[] ModemObjects = new string[250];
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            foreach (ManagementObject obj in searcher.Get())
            {
                // Win32_POTSModem
                // MessageBox.Show(obj["Name"].ToString() + "(" + obj["AttachedTo"].ToString() + ")");
                // Win32_PnPEntity
                //var test =obj["Availability"].ToString();
                //string source = "MY INFORMATION TO CHECK";            
                //if(source.IndexOf("information", StringComparison.OrdinalIgnoreCase) >= 0)
                if (obj["Name"].ToString().IndexOf("Z397") >= 0)
                {
                    // Присутствует код ConfigManagerErrorCode 28 - Устройство обнаруженно но не установленно
                    // Описание http://msdn.microsoft.com/en-us/library/windows/desktop/aa394353%28v=vs.85%29.aspx

                    // Установка драйвера
                    // Появляется еще одно устройство USB Serial Port Поставщик устройства FTDI

                    // Результат полсе установки
                    // Устройство Контроллер Z397-Guard USB<->485
                    // Устройство Порт Z397-Guard USB<->485 [Serial port] (COM 29)
                    // Присутствует код ConfigManagerErrorCode 0 - Ошибок нету


                    NumberDivece++;
                    // MessageBox.Show("Устройство №" + NumberDivece  + ". " + obj["Name"].ToString() + " / " + obj["ConfigManagerErrorCode"].ToString());
                    // MessageBox.Show(string.Format("{0} = {1}", obj["Name"].ToString(), obj["Value"].ToString()));
                    foreach (PropertyData data in obj.Properties)
                    {

                        // MessageBox.Show(string.Format("[Свойство {0} = {1}]", data.Name, data.Value, NumberDivece));
                    }
                }
                else
                {
                    //Отсутствует
                }
                if (obj["Description"].ToString().IndexOf("Guard") >= 0)
                {
                    //Присутствует   
                    // MessageBox.Show("/" + obj["Description"].ToString());
                }
                else
                {
                    //Отсутствует
                }






            }

            // objectQuery = new ObjectQuery("SELECT * FROM Win32_PnPEntity WHERE ConfigManagerErrorCode = 0");

            //ManagementObjectSearcher comPortSearcher = new ManagementObjectSearcher(connectionScope, objectQuery);

            //using (comPortSearcher)

            //{



        }

        #endregion

        #region Поиск семных носителей
        /// <summary>
        /// Поиск семных носителей и сбор короткой дополнительной информации
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string[]> ListDeviceRemovable()
        {
            Dictionary<string, string[]> List = new Dictionary<string, string[]>();
            // 1. Приложение ищит на всех дисках папку Conecto в папке Conecto\pack - обязательно наличие файла pack.xml)
            DriveInfo[] DriveList = DriveInfo.GetDrives();
            // string[] DriveList = Environment.GetLogicalDrives(); // Вариант не очень информативный
            //for (int i = 0; i < DriveList.Length; i++)
            // d.DriveType - тип устройства, проверить все портативные носители - if (drive.DriveType == DriveType.Removable)

            foreach (DriveInfo d in DriveList)
            {
                if (d.DriveType == DriveType.Removable)
                {
                    // Проверка готовности устройства
                    if (d.IsReady == true)
                    {
                        List.Add(d.Name, new string[2] { d.RootDirectory.ToString(), d.AvailableFreeSpace.ToString() });
                    }
                }
            }
            return List;
        }

        #endregion

        #region Удаление временных файлов (можно выполнять в потоке)
        //        Чистка файлов в фоне!!!! 
        //Для ускорения работы системы. Файлы удляются потом.
        //Не хватает списка файлов которые можно удалить.
        //Чтобы не удалить файлы которые нужны в данный момент,
        //предлагаю сделать открытый xml файл, в котором отмечать то, что можно удалить.
        /// <summary>
        /// Чистка файлов в фоне.  Для ускорения работы системы. Файлы удляются потом.
        /// Поток
        /// </summary>
        public static void DeliteFileTmp()
        {


        }

        #endregion

        #region Определения старта приложения в Windows

        /// <summary>
        /// Определения старта приложения в Windows
        /// [true загрузка с винчестера; false - из сети]
        /// </summary>
        /// <returns>true загрузка с винчетера; false - из сети</returns>
        public static bool IsLocalStartProgram()
        {
            DirectoryInfo dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            return (from d in DriveInfo.GetDrives()
                    where string.Compare(dir.Root.FullName, d.Name, StringComparison.OrdinalIgnoreCase) == 0
                    select (d.DriveType != DriveType.Network)
                    ).FirstOrDefault();
        }

        #endregion

        #region Выключение экрана монитора для экономии електроэнергии

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="hMsg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        private static extern int SendMessage(int hWnd, int hMsg, int wParam, int lParam);

        /// <summary>
        /// Handle - активного окна в Windows
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        public static void MonitorPOWER()
        {
            int WM_SYSCOMMAND = 0x0112;
            int SC_MONITORPOWER = 0xF170;
            var handleActive = GetForegroundWindow();

            //MessageBox.Show(string.Format("{0} = {1}", NameOP, allProc_.ToString()));

            SendMessage(handleActive.ToInt32(), WM_SYSCOMMAND, SC_MONITORPOWER, 2); //this.Handle.ToInt32()

        }




        #endregion



        #region Запуск и обслуживание серверов, а также их служб (Состояние)

        /// <summary>
        /// Служба запускается в отдельном потоке
        /// </summary>
        public static void StartStatusServer()
        {
            // Передача параметров в виде структуры в другой поток
            RenderInfo Arguments03 = new RenderInfo() { };
            //Thread thStartTimer03 = new Thread();
            //thStartTimer03.SetApartmentState(ApartmentState.STA);
            //thStartTimer03.IsBackground = true; // Фоновый поток
            //thStartTimer03.Start(Arguments03);

        }


        #endregion

        #region Создание каталогов по заданому пути v.1.2
        /// <summary>
        /// Создание каталогов по заданому пути аналог DIR_ - она быстрее
        /// позваляет определить ошибку когда в пути есть каталог имя которого совпадает с файлом
        /// </summary>
        /// <param name="dir_"></param>
        public static bool CheckDirectory(string dir_)
        {
            string[] dir = dir_.Split('\\');

            string dirchek = dir[0]; // диск начало пути

            for (int ind = 1; ind < dir.Length; ind++)
            {
                dirchek = dirchek + @"\" + dir[ind];
                // Определение ошибки совпадения названия файла и Директории
                if (File_(dirchek, 5))
                {
                    ErorDebag("Нельзя создать директорию, так как по указанному пути: " + dirchek.ToString() + " ; уже есть файл с таким именем!");
                    return false;
                }
                if (!DIR_(dirchek))
                {
                    return false;
                }
            }

            //string[] dirs = Directory.GetDirectories(dir_); // список всех txt файлов в директории C:\temp
            //for (int i = 0; i < dirs.Length; i++)
            //{
            //    //рекурсивный вызов сканирования для подпапок
            //    ScanFileDirectory(dirs[i]);  //, ref DTCursorZapit
            //}
            return true;
        }

        #endregion


        #region Разработка кодирования символов здесь часть кода еще в запись в CSV и в неизвестном класе
        /// <summary>
        /// Перекодировка UTF8 to 1251
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private string UTF8ToWin1251(string source)
        {

            Encoding utf8 = Encoding.GetEncoding("utf-8");
            Encoding win1251 = Encoding.GetEncoding("windows-1251");

            byte[] utf8Bytes = utf8.GetBytes(source);
            byte[] win1251Bytes = Encoding.Convert(utf8, win1251, utf8Bytes);
            source = win1251.GetString(win1251Bytes);
            return source;

        }

        private string Win1251ToUTF8(string source)
        {

            Encoding utf8 = Encoding.GetEncoding("utf-8");
            Encoding win1251 = Encoding.GetEncoding("windows-1251");

            byte[] utf8Bytes = win1251.GetBytes(source);
            byte[] win1251Bytes = Encoding.Convert(win1251, utf8, utf8Bytes);
            source = win1251.GetString(win1251Bytes);
            return source;

        }

        /// <summary>
        /// Перокодировка UTF8 CP1251 (UTF-16LE)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string UTF8ToCP1251(string value)
        {
            byte[]
                ba = Encoding.GetEncoding(1251).GetBytes(value);

            char[]
                ca = new char[Encoding.UTF8.GetDecoder().GetCharCount(ba, 0, ba.Length)];

            Encoding.UTF8.GetDecoder().GetChars(ba, 0, ba.Length, ca, 0);

            return (new string(ca));
        }

        /// <summary>
        /// Перокодировка Unicode CP1251
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string UnicodeToCP1251(string value)
        {
            byte[]
                ba = Encoding.GetEncoding(1251).GetBytes(value);

            char[]
                ca = new char[Encoding.Unicode.GetDecoder().GetCharCount(ba, 0, ba.Length)];

            Encoding.Unicode.GetDecoder().GetChars(ba, 0, ba.Length, ca, 0);

            return (new string(ca));
        }
        #endregion


        #region Проверка записи в реестре
        /// <summary>
        /// Управление записями реестра Windows<para></para>
        /// 0 - По умолчанию проверка системной ветки<para></para>
        /// 1 - Проверка ветки LocalMachine HKLM<para></para>
        /// 2 - Поиск по уже найденой ветке RegistryKey<para></para>
        /// <param name="TypeChek">Тип управления:</param>
        /// <returns>RegistryKey - Можно управлять с помощью команд:<para></para>
        /// GetValue("MyApp") - чтения параметра<para></para>
        /// SetValue("MyApp", Application.ExecutablePath.ToString()) - запись значения<para></para>
        /// DeleteValue("MyApp", false) - удаление </returns>        
        /// </summary>
        public static RegistryKey SerchWinRegistr(int TypeChek = 0, RegistryKey CurrentKey = null, string prOpenSubKey = null, string prOpenSubKey2 = null, string prOpenSubKey3 = null, string prOpenSubKey4 = null, string prOpenSubKey5 = null, string prOpenSubKey6 = null, string prOpenSubKey7 = null)
        {
            RegistryKey ChekReg = CurrentKey;

            switch (TypeChek)
            {
                case 0:
                    // Проверка системной ветки


                    break;
                case 1:

                    ChekReg = Registry.LocalMachine.OpenSubKey(prOpenSubKey);
                    if (ChekReg == null || prOpenSubKey2 == null)
                    {
                        // Запись Ошибки 
                        return ChekReg;
                    }
                    RegistryKey FoundKey2 = ChekReg.OpenSubKey(prOpenSubKey2);
                    if (FoundKey2 == null || prOpenSubKey3 == null)
                    {
                        // Запись Ошибки 

                        return FoundKey2;
                    }
                    RegistryKey FoundKey3 = FoundKey2.OpenSubKey(prOpenSubKey3);
                    if (FoundKey3 == null || prOpenSubKey4 == null)
                    {
                        // Запись Ошибки

                        return FoundKey3;
                    }
                    RegistryKey FoundKey4 = FoundKey3.OpenSubKey(prOpenSubKey4);
                    if (FoundKey4 == null || prOpenSubKey5 == null)
                    {
                        // Запись Ошибки 

                        return FoundKey4;
                    }
                    RegistryKey FoundKey5 = FoundKey4.OpenSubKey(prOpenSubKey5);
                    if (FoundKey5 == null || prOpenSubKey6 == null)
                    {
                        // Запись Ошибки 

                        return FoundKey5;
                    }
                    RegistryKey FoundKey6 = FoundKey5.OpenSubKey(prOpenSubKey6);
                    if (FoundKey6 == null || prOpenSubKey7 == null)
                    {
                        // Запись Ошибки 

                        return FoundKey6;
                    }
                    RegistryKey FoundKey7 = FoundKey6.OpenSubKey(prOpenSubKey7);
                    //if (FoundKey7 == null || prOpenSubKey8 == null)
                    //{
                    // Запись Ошибки 

                    return FoundKey7;
                //}

                //break;
                // Registry.CurrentUser.CreateSubKey(@"System\Alt-Tab\App");
                //// Чтение
                //if (rkApp.GetValue("MyApp") == null)
                //   // Установка
                //    rkApp.SetValue("MyApp", Application.ExecutablePath.ToString());
                //// Удаление
                //    rkApp.DeleteValue("MyApp", false);

                case 2:
                    // Поиск по уже найденой ветке RegistryKey

                    ChekReg = ChekReg.OpenSubKey(prOpenSubKey);
                    if (ChekReg == null || prOpenSubKey2 == null)
                    {
                        // Запись Ошибки 
                        return ChekReg;
                    }
                    RegistryKey FoundKey2_ = ChekReg.OpenSubKey(prOpenSubKey2);
                    if (FoundKey2_ == null || prOpenSubKey3 == null)
                    {
                        // Запись Ошибки 

                        return FoundKey2_;
                    }
                    RegistryKey FoundKey3_ = FoundKey2_.OpenSubKey(prOpenSubKey3);
                    if (FoundKey3_ == null || prOpenSubKey4 == null)
                    {
                        // Запись Ошибки

                        return FoundKey3_;
                    }
                    RegistryKey FoundKey4_ = FoundKey3_.OpenSubKey(prOpenSubKey4);
                    if (FoundKey4_ == null || prOpenSubKey5 == null)
                    {
                        // Запись Ошибки 

                        return FoundKey4_;
                    }
                    RegistryKey FoundKey5_ = FoundKey4_.OpenSubKey(prOpenSubKey5);
                    if (FoundKey5_ == null || prOpenSubKey6 == null)
                    {
                        // Запись Ошибки 

                        return FoundKey5_;
                    }
                    RegistryKey FoundKey6_ = FoundKey5_.OpenSubKey(prOpenSubKey6);
                    if (FoundKey6_ == null || prOpenSubKey7 == null)
                    {
                        // Запись Ошибки 

                        return FoundKey6_;
                    }
                    RegistryKey FoundKey7_ = FoundKey6_.OpenSubKey(prOpenSubKey7);
                    //if (FoundKey7 == null || prOpenSubKey8 == null)
                    //{
                    // Запись Ошибки 

                    return FoundKey7_;
                    //}

                    //break;

            }

            return ChekReg;
        }



        #endregion


        #region Чтение файла CSV с носителя разделител по умолчанию {,}

        //"Source", "Target"
        //"Red", "Красный"
        //"Orange","Оранжевый"
        //"Yellow", "Желтый"
        //"Green", "Зеленый"
        //"Blue", "Синий"
        //"Violet", "Фиолетовый"

        /// <summary>
        /// Чтение файла CSV с носителя разделител по умолчанию {,}
        /// </summary>
        /// <param name="NameCSVFile"></param>
        /// <param name="Putch"></param>
        /// <param name="NameTable"></param>
        /// <returns></returns>     
        public static DataTable ReadCSVDefault(string NameCSVFile, string Putch, string NameTable = "ReadCSV")
        {
            // Определяем Таблицу
            DataTable AllRowsCSV = new DataTable(NameTable);

            //Определяем подключение
            OleDbConnection StrCon = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Putch + ";Extended Properties=text"); // HDR=No;FMT=Delimited"""
            try
            {
                //Строка для выборки данных
                string Select1 = string.Format("SELECT * FROM [{0}]", NameCSVFile);
                //Создание объекта Command
                using (OleDbCommand comand1 = new OleDbCommand(Select1, StrCon))
                {
                    //Определяем объект Adapter для взаимодействия с источником данных
                    OleDbDataAdapter adapter1 = new OleDbDataAdapter(comand1);

                    //Определяем объект DataSet (кеш не используем)
                    // DataSet AllTables = new DataSet();

                    //Открываем подключение
                    StrCon.Open();

                    //Заполняем DataSet таблицей из источника данных
                    adapter1.Fill(AllRowsCSV); //AllTables
                    //Заполняем обект datagridview для отображения данных на форме
                    //dataGridView1.DataSource = AllTables.Tables[0];
                }

            }
            catch (Exception ex)
            {
                // ошибки
                PreOrderConection.ErorDebag("File : " + Environment.NewLine +
                    " === Message: " + ex.Message.ToString() + Environment.NewLine +
                    " === Exception: " + ex.ToString(), 1);
            }
            finally
            {
                if (StrCon != null)
                    StrCon.Close();
            }
            return AllRowsCSV;
        }


        #endregion


        #region Управление масивами:  Byte - Изменение размера, изменения данных в бинарном масиве UTF8


        /// <summary>
        /// Дозапись в масив (испытания) Кодировка по умолчанию UTF8
        /// </summary>
        /// <param name="WriteArrray">Масив в который пишем</param>
        /// <param name="CopyDataArray">Данные кторые записываем</param>
        public static void WriteinArrayByte(ref byte[] WriteArrray, string CopyDataArray)
        {
            byte[] _bytes = Encoding.UTF8.GetBytes(CopyDataArray);

            var LenStart2_ = WriteArrray.Length;
            Array.Resize(ref WriteArrray, WriteArrray.Length + _bytes.Length);
            _bytes.CopyTo(WriteArrray, LenStart2_);
        }
        /// <summary>
        /// Дозапись в масив (испытания) Кодировка по умолчанию UTF8
        /// </summary>
        /// <param name="WriteArrray">Масив в который пишем</param>
        /// <param name="_bytes">Данные кторые записываем из масива по ссылке</param>
        public static void WriteinArrayByte(ref byte[] WriteArrray, ref byte[] _bytes)
        {

            //byte[] _bytes = Encoding.UTF8.GetBytes(CopyDataArray);

            var LenStart2_ = WriteArrray.Length;
            Array.Resize(ref WriteArrray, WriteArrray.Length + _bytes.Length);
            _bytes.CopyTo(WriteArrray, LenStart2_);

        }
        /// <summary>
        /// Дозапись в масив (испытания) Кодировка по умолчанию UTF8
        /// </summary>
        /// <param name="WriteArrray">Масив в который пишем</param>
        /// <param name="_bytes">Данные кторые записываем из масива копия</param>
        public static void WriteinArrayByte(ref byte[] WriteArrray, byte[] _bytes)
        {

            //byte[] _bytes = Encoding.UTF8.GetBytes(CopyDataArray);

            var LenStart2_ = WriteArrray.Length;
            Array.Resize(ref WriteArrray, WriteArrray.Length + _bytes.Length);
            _bytes.CopyTo(WriteArrray, LenStart2_);

        }

        #endregion

    }

    // Тестовые процедуры (пока не пригодились)

    #region Выгрузка таблицы в файл
    /// <summary>
    /// Расширение для базового класса DataTable
    /// </summary>
    public static class DataTableExtensions
    {
        /// <summary>
        /// Экспорт таблицы в файл
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="filePath"></param>
        /// <param name="refTitleTable"> Таблица полей - класс ConnectFXML  (string[] TitleCSV - старый параметр)</param>
        /// <param name="TypeFile"></param>
        public static void WriteToFile(this DataTable dataTable, string filePath, DataTable refTitleTable, string TypeFile = "CSV utf8")
        {
            StringBuilder fileContent = new StringBuilder();

            // С рашифровкой заголовка TitleCSV.Count()
            if (refTitleTable.Rows.Count > 0 && refTitleTable.Columns.Count > 0)
            {
                // Совпадения порядка
                //var ind = 0;
                for (int ind = 0; ind < refTitleTable.Columns.Count; ind++)
                {
                    fileContent.Append(refTitleTable.Rows[0][ind].ToString() + ";"); //col.ToString()
                }
                //foreach (var col in dataTable.Columns)
                //{
                //    fileContent.Append( ( ind < TitleCSV.Length ? TitleCSV[ind] : col.ToString() ) + ";"); //col.ToString()
                //    ind++;
                //}
            }
            else
            {
                foreach (var col in dataTable.Columns)
                {
                    fileContent.Append(col.ToString() + ";");
                }
            }


            // Последний символ удаляем
            fileContent.Remove(fileContent.Length - 1, 1);
            fileContent.AppendLine();
            // Альтернатива
            // fileContent.Replace(";", System.Environment.NewLine, fileContent.Length - 1, 1);



            foreach (DataRow dr in dataTable.Rows)
            {

                foreach (var column in dr.ItemArray)
                {

                    //fileContent.Append("\"" + column.ToString() + "\";");
                    string ReplaceSpec = column.ToString().Replace(";", "&#160"); // замена символа пробелом
                    fileContent.Append(ReplaceSpec + ";");
                }

                // Последний символ удаляем
                fileContent.Remove(fileContent.Length - 1, 1);
                fileContent.AppendLine();
                // Альтернатива
                // fileContent.Replace(";", System.Environment.NewLine, fileContent.Length - 1, 1);
            }

            // Определим на первое время запись только одного файла
            // Для многих
            // for(){}
            if (TypeFile == "CSV utf8")
            {
                System.IO.File.WriteAllText(filePath.Replace(".csv", " utf.csv"), fileContent.ToString());
            }
            if (TypeFile == "CSV WIN1251")
            {
                Encoding win1251 = Encoding.GetEncoding("windows-1251");

                System.IO.File.WriteAllText(filePath.Replace(".csv", " win1251.csv"), fileContent.ToString(), win1251);

            }





        }

    }
    #endregion



}


