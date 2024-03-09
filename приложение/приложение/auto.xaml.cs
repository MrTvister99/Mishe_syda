
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Firebase.Storage;
using System.Threading.Tasks;
using Xamarin.Forms.Database.Models;
using Xamarin.Forms.Xaml;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using Firebase.Auth;
using System.Linq;
using приложение.Page;
using static Google.Rpc.Context.AttributeContext.Types;

namespace приложение
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class auto : ContentPage
    {
        private string mail;
        private ObservableCollection<ImageItem> _images;
        public ObservableCollection<ImageItem> Images
        {
            get { return _images; }
            set
            {
                _images = value;
                OnPropertyChanged();
            }
        }
        public auto()
        {
            InitializeComponent();
            
            Images = new ObservableCollection<ImageItem>();
            BindingContext = this;
            LoadDataIntoListView();

          
        }
        
        private void Order(object sender, EventArgs e)
        {
           // Navigation.PushModalAsync(new order_page());
        }
        public async Task LoadDataIntoListView()
        {
            // Подключение к Firebase Realtime Database
            var firebaseClient = new FirebaseClient("https://coursework-d0ee6-default-rtdb.firebaseio.com");

            
            var autoRef = firebaseClient.Child(Page3.Name);

            
            var minivans = await autoRef.OnceAsync<Details>();

            // Создание списка для хранения данных
            var details_list = new List<Details>();

            // Заполнение списка данными из Firebase
            foreach (var auto in minivans)
            {
                details_list.Add(auto.Object);



            }

            // Привязка списка к ListView в Xamarin.Forms
            myListView.ItemsSource = details_list;
        }

        // Класс для представления данных минивэна
        public class Details
        {
            public string ImgeUrl { get; set; }
          
            public string Название { get; set; }
            public decimal Цена { get; set; }


            public string FormatedPrice => $"Цена: {Цена} руб";
           


        }

        private async void OrderButton_Clicked(object sender, EventArgs e)
        {
          
            var details = new Detail();
            Button button = (Button)sender;
            // string itemName = name.Text;
            string itemName = (string)button.CommandParameter;
            details.Detail_name = itemName;
            details.car_make = Page3.Name;
            button.IsEnabled = false;
            button.Text = "Заказано";
            await Task.Delay(3000);
            button.IsEnabled = true;
            button.Text = "Заказать";
            var firebaseClient = new FirebaseClient("https://coursework-d0ee6-default-rtdb.firebaseio.com/");
            if(MainPage.email!=null)
            {
                string mail1 = MainPage.email.ToString();
                mail = mail1.Replace(".", "");
            }
            else
            {
                string mail1 = Page2.email.ToString();
                mail = mail1.Replace(".", "");
            }

            // Создать новый дочерний узел с указанным ключом
            var child = firebaseClient
                .Child("Details")
                .Child(mail)
                .Child(Guid.NewGuid().ToString());


            // Обновить значение дочернего узла
            await child
                .PutAsync(details);
        }
    }

    public class ImageItem
    {
        // public string ImageUrl { get; set; }
    }
    public class Detail
    {
        public string Detail_name = "Работает";
        public string car_make = "авто";
        
    }

}