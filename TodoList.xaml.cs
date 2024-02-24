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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFTodoListApp
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class TodoList : UserControl
    {
        public TodoList()
        {
            InitializeComponent();
        }


        private void CreateTask(string input)
        {
            Grid newGrid = new Grid();
            newGrid.Margin = new Thickness(0, 0, 0, 10);

            TextBlock task = new TextBlock();
            task.Text = input;
            task.VerticalAlignment = VerticalAlignment.Center;
            task.FontSize = 20;

            Button remove = new Button();
            remove.Content = "Remove";
            remove.HorizontalAlignment = HorizontalAlignment.Right;
            remove.Background = new SolidColorBrush(Colors.Red);
            remove.Foreground = new SolidColorBrush(Colors.White);
            remove.FontWeight = FontWeights.Bold;
            remove.Click += RemoveButton_Click;

            newGrid.Children.Add(task);
            newGrid.Children.Add(remove);

            taskList.Children.Add(newGrid);
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            Grid parentGrid = (Grid)button.Parent;

            taskList.Children.Remove(parentGrid);
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            CreateTask(input.Text);
        }

        private void SignoutButton_Clicked(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            window.Content = new LoginView();
        }
    }
}
