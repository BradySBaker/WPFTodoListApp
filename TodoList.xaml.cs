using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPFTodoListApp
{
    public partial class TodoList : UserControl
    {
        public string todoFilePath = Path.Combine(Directory.GetCurrentDirectory(), "todoData.txt");
        public TodoList()
        {
            InitializeComponent();
            RetrieveFileData();
        }


        private void RetrieveFileData() {
            if (File.Exists(todoFilePath)) {
                try {
                    using (StreamReader reader = new StreamReader(todoFilePath)) {
                        string line;
                        while ((line = reader.ReadLine()) != null) {
                            CreateTask(line.Substring(1), (int)line[0] - '0');
                        }
                    }
                }
                catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                }
                return;
            }
            try {
                using (File.Create(todoFilePath)){};
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        private void CreateTask(string input, int statusIndex = -1) {

            Grid newGrid = new Grid();
            newGrid.Margin = new Thickness(0, 0, 0, 10);

            TextBlock task = new TextBlock();
            task.Text = input;
            task.HorizontalAlignment = HorizontalAlignment.Left;
            task.VerticalAlignment = VerticalAlignment.Center;
            task.FontSize = 20;
            task.TextWrapping = TextWrapping.Wrap;
            task.Width = 230;

            ComboBox statusSelector = new ComboBox();
            statusSelector.Items.Add(new ComboBoxItem() { Content = "NotStarted" });
            statusSelector.Items.Add(new ComboBoxItem() { Content = "InProgress" });
            statusSelector.Items.Add(new ComboBoxItem() { Content = "Completed" });
            statusSelector.Margin = new Thickness(190, 0, 0, 0);
            statusSelector.Width = 80;
            statusSelector.MaxHeight = 30;
            statusSelector.VerticalAlignment = VerticalAlignment.Center;
            statusSelector.SelectedIndex = statusIndex == -1 ? 0 : statusIndex;
            statusSelector.SelectionChanged += TaskStatus_Changed;

            Button remove = new Button();
            remove.Content = "Remove";
            remove.HorizontalAlignment = HorizontalAlignment.Right;
            remove.VerticalAlignment = VerticalAlignment.Center;
            remove.Background = new SolidColorBrush(Colors.Red);
            remove.Foreground = new SolidColorBrush(Colors.White);
            remove.FontWeight = FontWeights.Bold;
            remove.Click += RemoveButton_Click;
            remove.MaxHeight = 30;

            newGrid.Children.Add(task);
            newGrid.Children.Add(statusSelector);
            newGrid.Children.Add(remove);

            taskList.Children.Add(newGrid);
            if (statusIndex != -1) {
                return;
            }
            try {
                using (StreamWriter writer = new StreamWriter(todoFilePath, true)) {
                    writer.WriteLine("0" + task.Text);
                }
            } catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            Grid parentGrid = (Grid)button.Parent;

            TextBlock task = (TextBlock)parentGrid.Children[0];
            RemoveTaskFromFile(task.Text);

            taskList.Children.Remove(parentGrid);
        }

        private void RemoveTaskFromFile(string input) {
            try {
                string[] lines = File.ReadAllLines(todoFilePath);
                string[] updatedLines = lines.Where(line => line.Substring(1) != input).ToArray();
                File.WriteAllLines(todoFilePath, updatedLines);
                MessageBox.Show("Task Removed");
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
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

        private void TaskStatus_Changed(object sender, SelectionChangedEventArgs e) {
            if (sender is ComboBox comboBox) {
                int selectedIndex = comboBox.SelectedIndex;
                string[] lines = File.ReadAllLines(todoFilePath);
                Grid grid = (Grid)comboBox.Parent;
                TextBlock task = (TextBlock)grid.Children[0];

                for (int i = 0; i < lines.Length; i++) {
                    if (lines[i].Substring(1) == task.Text) {
                        lines[i] = selectedIndex + lines[i].Substring(1);
                        break;
                    }
                }

                File.WriteAllLines(todoFilePath, lines);
            }
        }
    }
}
