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
    /// Логика взаимодействия для MessageAddOrder.xaml
    /// </summary>
    public partial class MessageAddOrder : Window
    {
        System.Windows.Threading.DispatcherTimer CloseAuto;
        public MessageAddOrder(string TextWindows = null, int AutoClose = 0)
        {
            InitializeComponent();

            // Автозакрытие окна
            if (AutoClose == 1)
            {

                CloseAuto = new System.Windows.Threading.DispatcherTimer();
                CloseAuto.Tick += new EventHandler(CloseAutoTick);
                CloseAuto.Interval = new TimeSpan(0, 0, 7);
                CloseAuto.Start();


                //this.CloseAuto.Enabled = true;
            }

            if (TextWindows != null)
            {
                // Определить высоту окна по количеству \n в многострочном тексте
                var TextWindows_a = TextWindows.Split('\n');
                
                this.MessageText.Text = TextWindows != null ? TextWindows : "Сообщение отсутствует!";
                //}
            }
        }

        private void CloseAutoTick(object sender, EventArgs e)
        {
            this.Owner = null;
            this.Close();
        }

        
        private void Close_F_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
    }
}
