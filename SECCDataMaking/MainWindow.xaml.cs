using System;
using System.Collections.Generic;
using System.IO;
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

namespace SECCDataMaking
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int BlockSize = 20;
        private const int Cnt = 16;
        private int[,] data = new int[Cnt, Cnt];
        private Label[,] btns = new Label[Cnt, Cnt];
        private string text = "";

        public MainWindow()
        {
            InitializeComponent();

            for (int i = 0; i < Cnt; ++i)
            {
                StackPanel p = new StackPanel();
                p.Width = Cnt * BlockSize;
                p.Height = 20;
                p.Orientation = Orientation.Horizontal;
                for (int j = 0; j < Cnt; ++j)
                {
                    Label btn = new Label();
                    btn.Width = BlockSize;
                    btn.Height = BlockSize;
                    p.Children.Add(btn);
                    btn.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                    btns[j, i] = btn;
                }
                st.Children.Add(p);
            }
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            Label btn = (Label)sender;
            KeyValuePair<int, int> a = (KeyValuePair<int, int>)btn.Tag;
        }

        private void panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                var pos = e.GetPosition(st);
                Console.WriteLine(pos);
                int x = (int)Math.Floor(pos.X / BlockSize);
                int y = (int)Math.Floor(pos.Y / BlockSize);
                if (x >= Cnt || y >= Cnt)
                    return;
                data[x, y] = 1;

                btns[x, y].Background = new SolidColorBrush(Color.FromRgb(255, 255, 0));
                Console.WriteLine($"{x},{y}");
            }
        }

        private bool fir = true;

        private int pow(int n, int p)
        {
            if (n == 0)
                return n;
            int r = 1;
            for (int i = 0; i < p; ++i)
            {
                r *= n;
            }
            return r;
        }

        private void ssclick(object sender, RoutedEventArgs e)
        {
            string res = "";
            for (int i = 0; i < Cnt; ++i)
            {
                for (int j = 0; j < Cnt; ++j)
                {
                    res += data[j, i];
                    Console.Write(data[j, i]);
                }
            }
            result.Text = res;
        }

        private void rBtnClicked(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < Cnt; ++i)
            {
                for (int j = 0; j < Cnt; ++j)
                {
                    data[j, i] = 0;
                    btns[j, i].Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                }
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "data.txt");
            FileStream f = File.Open(path, FileMode.Create);
            byte[] bytes = Encoding.ASCII.GetBytes(text);
            f.Write(bytes, 0, bytes.Length);
            f.Close();
            //FileStream file = new FileStream(@"F:\Data\data.txt", FileAccess.Write);
        }
    }
}