using MauiSwipeCardViewSample.Models;
using System.Collections.ObjectModel;
using System.Net.Http.Json;

namespace MauiSwipeCardViewSample
{
    public partial class MainPage : ContentPage
    {
        private HttpClient _httpClient = new();

        public ObservableCollection<Monkey> Monkeys { get; set; } = new();

        public MainPage()
        {
            InitializeComponent();

            BindingContext = this;
        }

        protected async override void OnNavigatedTo(NavigatedToEventArgs args)
        {
            base.OnNavigatedTo(args);

            var response = await _httpClient.GetAsync("https://www.montemagno.com/monkeys.json");
            if (response.IsSuccessStatusCode)
            {
                Monkeys.Clear();

                var resultMonkeys = await response.Content.ReadFromJsonAsync(MonkeyContext.Default.ListMonkey);

                if (resultMonkeys is not null)
                {
                    foreach (var monkey in resultMonkeys)
                    {
                        Monkeys.Add(monkey);
                    }
                }
            }
        }
    }

}
