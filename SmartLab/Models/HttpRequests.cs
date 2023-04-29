using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SmartLab.ViewModels;
using SmartLab.Core;
using MauiPopup;
using SmartLab.Views;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;

namespace SmartLab.Models
{
    public class HttpRequests
    {
        private AnalyzesViewModel _analyzesVM;
        public async Task<string> SendCode(string UserMail)
        {
            string response;
            using (var client = new HttpClient())
            {
                string url = "https://medic.madskill.ru/api/sendCode";
                var request = new HttpRequestMessage(HttpMethod.Post, url);
                request.Headers.Add("accept", "application/json");
                request.Headers.Add("email", UserMail);

                response = await client.SendAsync(request).Result.Content.ReadAsStringAsync();
            }
            return response;
        }

        public async Task<string> SignIn(string UserMail, string Code)
        {
            string response;
            using (var client = new HttpClient())
            {
                string url = "https://medic.madskill.ru/api/signin";
                var request = new HttpRequestMessage(HttpMethod.Post, url);
                request.Headers.Add("accept", "application/json");
                request.Headers.Add("email", UserMail);
                request.Headers.Add("code", Code);

                response = await client.SendAsync(request).Result.Content.ReadAsStringAsync();
            }
            return response;
        }
        public async Task<string> CreateUser()
        {
            Random random = new Random();
            string response;
            using (var client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(new
                {
                    id = random.Next(),
                    firstname = Preferences.Get("UserName", null),
                    lastname = Preferences.Get("UserLastName", null),
                    middlename = Preferences.Get("UserMiddleName", null),
                    bith = Preferences.Get("Birthday", null),
                    pol = Preferences.Get("Gender", null),
                    image = "1"
                });
                string url = "https://medic.madskill.ru/api/createProfile";
                var request = new HttpRequestMessage(HttpMethod.Post, url);
                request.Headers.Add("accept", "application/json");
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Preferences.Get("UserToken", null));

                response = await client.SendAsync(request).Result.Content.ReadAsStringAsync();
            }
            return response;
        }

        public async void GetNews(AnalyzesViewModel analyzesVM)
        {
            string response;
            using (var client = new HttpClient())
            {
                string url = "https://medic.madskill.ru/api/news";
                response = await client.GetAsync(url).Result.Content.ReadAsStringAsync();
            }
            ObservableCollection<NewsItem> newsItems = JsonConvert.DeserializeObject<ObservableCollection<NewsItem>>(response);
            analyzesVM.NewsCollection = newsItems;
        }
        public async void GetCatalog(AnalyzesViewModel analyzesVM)
        {
            string response;
            using (var client = new HttpClient())
            {
                string url = "https://medic.madskill.ru/api/catalog";
                response = await client.GetAsync(url).Result.Content.ReadAsStringAsync();
            }
            ObservableCollection<CatalogItem> catalogItems = JsonConvert.DeserializeObject<ObservableCollection<CatalogItem>>(response);
            foreach (CatalogItem item in catalogItems)
            {
                item.catalogButtonItem = new CatalogButtonItem()
                {
                    ButtonCommand = new RelayCommand<string>(DisplayPopup),
                    Name = item.name
                };
            };
                
            analyzesVM.CatalogCollection = catalogItems;
            _analyzesVM = analyzesVM;
        }

        private void DisplayPopup(string name)
        {
            CatalogItem catalogItem = _analyzesVM.CatalogCollection.FirstOrDefault(x => x.name == name);
            PopupAction.DisplayPopup(new ProductCardView(catalogItem));
        }
    }
}
