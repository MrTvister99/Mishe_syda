using System;
using Xamarin.Forms;

namespace приложение.Page // Замените на ваше актуальное пространство имен
{
    public partial class Page1 : ContentPage
    {
        DatePicker datePicker;
        Label selectedDateLabel;
        StackLayout buttonStackLayout;
        Label selectedTimeLabel; // Инициализация переменной selectedTimeLabel

        public Page1()
        {
            datePicker = new DatePicker
            {
                Format = "dd.MM.yyyy",
                MinimumDate = DateTime.Today.AddDays(0),
                MaximumDate = DateTime.Today.AddDays(20),
                Date = DateTime.Today
            };

            selectedDateLabel = new Label
            {
                Text = $"Выбранная дата: {datePicker.Date.ToString("dd.MM.yyyy")}",
                HorizontalOptions = LayoutOptions.CenterAndExpand // Установка горизонтального выравнивания по центру
            };

            buttonStackLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                IsVisible = false
            };

            var button1 = new Button
            {
                Text = "9:00",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            var button2 = new Button
            {
                Text = "12:00",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            var button3 = new Button
            {
                Text = "16:00",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            var button4 = new Button
            {
                Text = "18:00",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            selectedTimeLabel = new Label // Инициализация selectedTimeLabel
            {
                Text = "Выбранное время:",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            var grid = new Grid
            {
                RowDefinitions = new RowDefinitionCollection
                {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
                }
            };

            grid.Children.Add(datePicker, 0, 0);
            grid.Children.Add(selectedDateLabel, 0, 1);
            grid.Children.Add(buttonStackLayout, 0, 2);
            grid.Children.Add(selectedTimeLabel, 0, 3); // Добавление selectedTimeLabel в Grid

            Content = grid;

            var timeLabel = new Label
            {
                Text = "Выбор времени",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            buttonStackLayout.Children.Add(timeLabel);
            buttonStackLayout.Children.Add(button1);
            buttonStackLayout.Children.Add(button2);
            buttonStackLayout.Children.Add(button3);
            buttonStackLayout.Children.Add(button4);

            datePicker.DateSelected += (sender, e) =>
            {
                selectedDateLabel.Text = $"Выбранная дата: {datePicker.Date.ToString("dd.MM.yyyy")}";
                selectedTimeLabel.Text = "Выбранное время:"; // Сброс выбранного времени
                buttonStackLayout.IsVisible = true;
            };

            button1.Clicked += async (sender, e) =>
            {
                var result = await DisplayAlert("Выбранное время", "9:00", "ОК", "Отмена");
                if (result)
                {
                    selectedTimeLabel.Text = "Выбранное время: 9:00"; // Обновление выбранного времени
                }
            };

            button2.Clicked += async (sender, e) =>
            {
                var result = await DisplayAlert("Выбранное время", "12:00", "ОК", "Отмена");
                if (result)
                {
                    selectedTimeLabel.Text = "Выбранное время: 12:00";
                }
            };

            button3.Clicked += async (sender, e) =>
            {
                var result = await DisplayAlert("Выбранное время", "16:00", "ОК", "Отмена");
                if (result)
                {
                    selectedTimeLabel.Text = "Выбранное время: 16:00";
                }
            };

            button4.Clicked += async (sender, e) =>
            {
                var result = await DisplayAlert("Выбранное время", "18:00", "ОК", "Отмена");
                if (result)
                {
                    selectedTimeLabel.Text = "Выбранное время: 18:00";
                }
            };
        }
    }
}
