using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Firebase.Auth;
using приложение.Page;

namespace приложение
{
    public partial class MainPage : ContentPage
    {
        public static string email;
        public MainPage()
        {
            InitializeComponent();
            
        }

        private async void ButtonEntrance_Click(object sender, EventArgs e)
        {
            
            try
            {
                 email = loginEntry.Text; // Получаем email из поля ввода
                string password = passwordEntry.Text; // Получаем пароль из поля ввода
                if(email=="ZG@gmail.com" && password == "123456")
                {
                    await Navigation.PushModalAsync(new Admin_page());
                    return;
                }
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyCmyLnu7iujX8KE1QxV5BEn3kdK8y1LDWM"));
                var authLink = await authProvider.SignInWithEmailAndPasswordAsync(email, password);

                // Если аутентификация прошла успешно, открываем новую страницу (например, Chat)
                await Navigation.PushModalAsync(new Page3());
            }
            catch (FirebaseAuthException ex)
            {
                // Обработка ошибок аутентификации
                if (ex.Reason == AuthErrorReason.WrongPassword || ex.Reason == AuthErrorReason.UnknownEmailAddress)
                {
                    await DisplayAlert("Ошибка", "Неверный логин или пароль", "OK");
                }
                else
                {
                    await DisplayAlert("Ошибка", "Ошибка аутентификации: " + ex.Reason, "OK");
                }
            }
        }

        private void ButtonRegistration_Click(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Page2());
        }
    }
}