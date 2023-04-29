using Microsoft.Maui.ApplicationModel.Communication;
using SmartLab.Models;
using SmartLab.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SmartLab.ViewModels
{
    public class LoginOrRegisterViewModel : ViewModelBase
	{
        private readonly HttpRequests _httpRequests;   
        public LoginOrRegisterViewModel()
        {
            _httpRequests = new HttpRequests();

            NextCommand = new Command(NextView);
            LoginYandexCommand = new Command(LoginYandex);
        }

        private async void LoginYandex(object obj)
        {
            await AppShell.Current.GoToAsync($"//{nameof(AnalyzesView)}");
        }

        private async void NextView(object obj)
        {
            try
            {
                string response = await _httpRequests.SendCode(UserMail);
                Preferences.Set("UserMail", UserMail);
                await AppShell.Current.GoToAsync($"//{nameof(EmailCodeView)}");
            }
            catch (Exception)
            {
                return;
            }
           
        }

        private string _userMail = string.Empty;
		public string UserMail
        {
			get { return _userMail; }
			set { _userMail = value; OnPropertyChanged(); OnPropertyChanged("NextBtnIsEnabled"); OnPropertyChanged("BtnOpacity"); }
		}

        private bool _nextBtnIsEnabled;
        public bool NextBtnIsEnabled
        {
            get {
                return Regex.IsMatch(UserMail, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase) == false || _userMail == null || _userMail.Trim() == string.Empty ? false : true; 
            }
            set { _nextBtnIsEnabled = value; OnPropertyChanged(); }
        }

        private double _btnOpacity;
        public double BtnOpacity
        {
            get 
            {
                return Regex.IsMatch(UserMail, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase) == false || _userMail == null || _userMail.Trim() == string.Empty ? 0.4 : 1; 
            }
            set { _btnOpacity = value; OnPropertyChanged(); }
        }


        public ICommand NextCommand { get; set; }
        public ICommand LoginYandexCommand { get; set; }
    }
}
