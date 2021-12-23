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

namespace DemoExam
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            update();
        }
        public void update()
        {
            var List = App.DB.prod.ToList();
            var cb = combobox.SelectedIndex;
            if(poiskbox != null)
            {
                var poisk = poiskbox.Text;
                List = App.DB.prod.Where(p => p.name.StartsWith(poisk)).ToList();
            }
            if(cb == 0)
            {
                List = App.DB.prod.Where(p => p.id_category == 1).ToList();
            }
            if (cb == 1)
            {
                List = App.DB.prod.Where(p => p.id_category == 2).ToList();
            }
            listview.ItemsSource = List;
        }

        private void combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            update();
        }

        private void poiskbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            update();
        }

        private void del_Click(object sender, RoutedEventArgs e)
        {
            Button button1 = (Button)sender;
            prod tb = (prod)button1.DataContext;
            App.DB.prod.Remove(tb);
            App.DB.SaveChanges();
            update();
        }

        private void change_Click(object sender, RoutedEventArgs e)
        {
            Button button1 = (Button)sender;
            var tb = (prod)button1.DataContext;
            textbox1.Text = tb.name;
            textbox2.Text = tb.data;
            textbox3.Text = tb.id_category.ToString();
            App.Inser_table = tb;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            prod tb = new prod {name = textbox1.Text, data=textbox2.Text, id_category = Convert.ToInt32(textbox3.Text)};
            App.DB.prod.Add(tb);
            App.DB.SaveChanges();
            update();
        }

        private void up_Click(object sender, RoutedEventArgs e)
        {
            update();
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            var inser = App.DB.prod.Where(prod => prod.id == App.Inser_table.id).FirstOrDefault();
            inser.name = textbox1.Text;
            inser.data = textbox2.Text;
            inser.id_category = Convert.ToInt32(textbox3.Text);
            App.DB.SaveChanges();
            update();

        }
    }
}
