using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Firebase.Storage;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xamarin.Forms.Database.Models;
using Xamarin.Forms.Xaml;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using Firebase.Auth;
using System.Linq;
using System.Net.Mail;
using приложение.Page;
using Google.Cloud.Firestore;

namespace приложение
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class korzina : ContentPage
    {
        private ObservableCollection<Detail> details;
        public korzina()
        {
            InitializeComponent();
            details = new ObservableCollection<Detail>();
            detailsListView.ItemsSource = details;
            FetchDataFromFirebase();

        }
        private async void FetchDataFromFirebase()
        {
            var firebaseClient = new FirebaseClient("https://coursework-d0ee6-default-rtdb.firebaseio.com/");

            // Получить данные из Firebase
            var items = await firebaseClient
                .Child("Details")
                .OnceAsync<Detail>();

            // Очистить текущие данные
            details.Clear();

            // Добавить полученные данные в ObservableCollection
            foreach (var item in items)
            {
                var detail = item.Object;
                detail.mail = item.Key;



                var nestedItems = await firebaseClient
                    .Child("Details")
                    .Child(detail.mail)
                    .OnceAsync<Detail>();
                int atIndex = detail.mail.IndexOf('@');
                if (atIndex >= 0)
                {
                    if (detail.mail.Contains("ru") && atIndex < detail.mail.IndexOf("ru"))
                    {
                        detail.mail = detail.mail.Replace("ru", ".ru");
                    }
                    else if (detail.mail.Contains("com") && atIndex < detail.mail.IndexOf("com"))
                    {
                        detail.mail = detail.mail.Replace("com", ".com");
                    }
                }
                foreach (var nestedItem in nestedItems)
                {
                    var nestedDetail = nestedItem.Object;
                    nestedDetail.mail = detail.mail;
                    if (detail.mail == MainPage.email || detail.mail == Page2.email)
                    {
                        details.Add(nestedDetail);
                    }
                }
            }
        }

        public class Detail
        {
            public string mail { get; set; }
            public string Detail_name { get; set; }
            public string Car_make { get; set; }

        }

        private async void Delete_Click(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var item = (Detail)imageButton.BindingContext;
            int Index = item.mail.IndexOf('@');
            bool answer = await DisplayAlert("Подтверждение", "Вы уверены, что хотите отменить этот заказ?", "Да", "Нет");
            if (item.mail.Contains("ru") && Index < item.mail.IndexOf("ru"))
            {
                item.mail = item.mail.Replace("ru", ".ru");
            }
            else if (item.mail.Contains("com") && Index < item.mail.IndexOf("com"))
            {
                item.mail = item.mail.Replace("com", ".com");
            }

            var firebaseClient = new FirebaseClient("https://coursework-d0ee6-default-rtdb.firebaseio.com/");
                var toDeleteItem = (from detail in details
                                    where detail.mail == item.mail && detail.Detail_name == item.Detail_name && detail.Car_make == item.Car_make
                                    select detail).FirstOrDefault();

                if (toDeleteItem != null)
                {
                    int atIndex = toDeleteItem.mail.IndexOf('@');
                    if (toDeleteItem.mail.Contains("ru") && atIndex < toDeleteItem.mail.IndexOf("ru"))
                    {
                        toDeleteItem.mail = toDeleteItem.mail.Replace(".ru", "ru");
                    }
                    else if (toDeleteItem.mail.Contains("com") && atIndex < toDeleteItem.mail.IndexOf("com"))
                    {
                        toDeleteItem.mail = toDeleteItem.mail.Replace(".com", "com");
                    }
                var detailsSnapshot = await firebaseClient
            .Child("Details")
            .Child(toDeleteItem.mail.Replace(".", ""))
            .OnceAsync<Detail>();

                foreach (var detailSnapshot in detailsSnapshot)
                {
                    var detail = detailSnapshot.Object;
                    if (detail.Detail_name == item.Detail_name && detail.Car_make == item.Car_make)
                    {
                        // Удаляем деталь из Firebase
                        await firebaseClient
                            .Child("Details")
                            .Child(toDeleteItem.mail.Replace(".", ""))
                            .Child(detailSnapshot.Key)
                            .DeleteAsync();

                        // Удаляем деталь из локального списка
                        details.Remove(detail);

                        // Обновляем источник данных ListView
                        detailsListView.ItemsSource = null;
                        detailsListView.ItemsSource = details;

                        // Выходим из цикла, так как деталь удалена
                        break;
                    }
                }




               

                    
                    details.Remove(toDeleteItem);

                    
                    detailsListView.ItemsSource = null; 
                    detailsListView.ItemsSource = details; 
                }
            }
        }
    }
