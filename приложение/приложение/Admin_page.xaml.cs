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
using static приложение.Admin_page;

namespace приложение
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Admin_page : ContentPage
    {
        private ObservableCollection<Detail> details;
        public Admin_page()
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
                    details.Add(nestedDetail);
                }
            }
        }

        public class Detail
        {
            public string mail { get; set; }
            public string Detail_name { get; set; }
            public string Car_make { get; set; }
            
        }

    }

}

    
