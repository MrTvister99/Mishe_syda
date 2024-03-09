using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Auth;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace приложение.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page2 : ContentPage
    {
        private const string FirebaseUrl = "https://coursework-d0ee6-default-rtdb.firebaseio.com/";

        private readonly FirebaseClient firebase;
        public static string email;

        public Page2()
        {
            InitializeComponent();
            ButtenAccount_registration.IsEnabled = false;
            firebase = new FirebaseClient(FirebaseUrl);
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        private async void ButtenAccount_registration_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(loginEntry.Text) || string.IsNullOrEmpty(passwordEntry.Text) || string.IsNullOrEmpty(EnterpasswordEntry.Text))
            {
                await DisplayAlert("Ошибка регистрации", "Заполните все поля", "OK");
                return;
            }

            email = loginEntry.Text;
            string password = passwordEntry.Text;
            string confirmPassword = EnterpasswordEntry.Text;

            if (email.Length < 6) 
            {
                await DisplayAlert("Ошибка регистрации", "Недопустимый адрес электронной почты", "OK");
                return;
            }

            if (password.Length < 6)
            {
                await DisplayAlert("Ошибка регистрации", "Пароль не должен быть короче 6 символов, попробуйте другой пароль", "OK");
                await DisplayAlert("Ошибка регистрации", "Пароль не должен быть короче 6 символов, попробуйте другой пароль", "OK");
                return;
            }

            if (password == confirmPassword)
            {
                string apiKey = "AIzaSyCmyLnu7iujX8KE1QxV5BEn3kdK8y1LDWM"; // ключ API Firebase

                FirebaseAuthProvider firebaseAuthProvider = new FirebaseAuthProvider(new FirebaseConfig(apiKey));



                try
                {
                    FirebaseAuthLink authLink = await firebaseAuthProvider.CreateUserWithEmailAndPasswordAsync(email, password);


                  
                    Navigation.PushModalAsync(new Page3()); // Переход на новую страницу с использованием NavigationPage

                }
                catch (Exception ex)
                {
                    await DisplayAlert("Ошибка регистрации", ex.Message, "OK");
                }
            }
            else
            {
                await DisplayAlert("Ошибка регистрации", "Пароли не совпадают", "OK");
            }
        }

        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            ButtenAccount_registration.IsEnabled = e.Value; // устанавливаем значение прямо из CheckedChangedEventArgs
        }

        private void ButtonRefund_Click(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new MainPage()); // Возвращаемся на предыдущую страницу
        }
    }
}