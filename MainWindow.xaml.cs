using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Collections.Specialized.BitVector32;
using static System.Net.Mime.MediaTypeNames;

namespace WPFTodoListApp {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }



        private void CreateTask(string input) {
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
            remove.Click += Remove_Button_Click;

            newGrid.Children.Add(task);
            newGrid.Children.Add(remove);

            taskList.Children.Add(newGrid);
        }

        private void Remove_Button_Click(object sender, RoutedEventArgs e) {
            Button button = (Button)sender;

            Grid parentGrid = (Grid)button.Parent;

            taskList.Children.Remove(parentGrid);
        }
        private void Add_Button_Click(object sender, RoutedEventArgs e) {
            CreateTask(input.Text);
        }
    }
}