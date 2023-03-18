using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Lab3
{
    public partial class MainWindow : Window
    {
        WebBrowser webB; // открытая страница
        Button tabBtn; // открытая вкладка
        List<WebPage> webPages = new List<WebPage>(); // список страниц
        List<string> webhistory = new List<string>(); // история
        List<string> webNotes = new List<string>(); // закладки

        public MainWindow()
        {
            InitializeComponent();

            LastSession();
        }

        // Создает страницу
        void AddPage(string path)
        {
            WebPage page = new WebPage();
            page.WebBrowser.Source = new Uri(path);
            page_webB.Content = page.Page;
            page.Tag = webPages.Count.ToString();
            webB = page.WebBrowser;

            webPages.Add(page);
        }

        // Создает вкладку
        void AddTab(string name)
        {
            Button button = new Button()
            {
                Width = 100,
                Content = name,
                Tag = webPages.Count.ToString(),
                Margin = new Thickness(2, 0, 0, 0),
            };

            button.Click += (sender, e) =>
            {
                if (sender as Button == tabBtn) return;

                foreach(WebPage page in webPages)
                {
                    if(page.Tag == (sender as Button).Tag.ToString())
                    {
                        page_webB.Content = page.Page;
                        webB = page.WebBrowser;
                        tabBtn = sender as Button;

                        textBox.Text = webB.Source.OriginalString;
                        webhistory.Add(webB.Source.OriginalString);
                    }
                }
            };

            tabBtn = button;
            textBox.Text = "https://www.google.com/";

            tabs.Children.Insert(tabs.Children.Count - 1, button);
        }

        // Последний сеанс
        void LastSession()
        {
            MessageBoxResult result = MessageBox.Show("Восстановить последний сеанс?", "Восстановить?", MessageBoxButton.YesNo);

            if(result == MessageBoxResult.Yes) 
            {
                List<WebPage> pages = SaveList<List<WebPage>>.ReadList("page.json");
                foreach(var elem in pages)
                {
                    AddTab(elem.uri);
                    AddPage(elem.uri);
                }
            }
            else
            {
                AddTab("Google");
                AddPage("https://www.google.com/");
            }
        }

        // загружаем данные поле загрузки окна
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            webhistory = SaveList<List<string>>.ReadList("history.json");
            webNotes = SaveList<List<string>>.ReadList("notes.json");
        }

        // Вперед
        private void forwardBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (webB.CanGoForward == false) return;
                
                webB.GoForward();
                tabBtn.Content = textBox.Text = webB.Source.OriginalString;
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }

        // Назад
        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (webB.CanGoBack == false) return;
                
                webB.GoBack();
                tabBtn.Content = textBox.Text = webB.Source.OriginalString;
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }

        // Поиск
        private void searchBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (textBox.Text == "") return;

                int uri = textBox.Text.IndexOf("https://");

                textBox.Text = (uri == -1 ? "https://" : "") + textBox.Text;

                webB.Source = new Uri(textBox.Text);

                tabBtn.Content = textBox.Text;

                webhistory.Add(textBox.Text);
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }

        // открываем вкладку после нажатии на кнопку "+"
        private void addTabBtn_Click(object sender, RoutedEventArgs e)
        {
            AddTab("Google");
            AddPage("https://www.google.com/");
        }

        // сохраняет данные при закрытии окна
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveList<List<WebPage>>.WriteList(webPages, "page.json");
            SaveList<List<string>>.WriteList(webhistory, "history.json");
            SaveList<List<string>>.WriteList(webNotes, "notes.json");
        }

        // добавляем в избранное
        private void addNotesBtn_Click(object sender, RoutedEventArgs e)
        {
            string note = "";
            if(webNotes == null) webNotes= new List<string>();

            foreach (var elem in webNotes)
            {
                if (elem == webB.Source.OriginalString)
                {
                    note = elem;
                }
            }
            if (note != "")
            {
                webNotes.Remove(note);
                return;
            }
            webNotes.Add(webB.Source.OriginalString);
        }

        // открывает список истории
        private void historyBtn_Click(object sender, RoutedEventArgs e)
        {
            string history = "";
            foreach(var elem in webhistory)
            {
                history += elem;
                history += "\n";
            }
            MessageBox.Show(history, "История");
        }

        // открывает список закладки
        private void noteBtn_Click(object sender, RoutedEventArgs e)
        {
            string note = "";
            foreach (var elem in webNotes)
            {
                note += elem;
                note += "\n";
            }
            MessageBox.Show(note, "Зкаладки");
        }
    }
}
