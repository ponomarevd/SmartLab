using SmartLab.Models;
using SmartLab.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SmartLab.ViewModels
{
    public class EmailCodeViewModel : ViewModelBase
    {
        private readonly HttpRequests _httpRequests;
        IDispatcherTimer timer;
        public EmailCodeViewModel()
        {
            _httpRequests = new HttpRequests();

            timer = Application.Current.Dispatcher.CreateTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += (s, e) => timerCallback();
            timer.Start();

            BackCommand = new Command(ToBackView);
            SendAgainCommand = new Command(SendCodeAgain);
        }

        private async void SendCodeAgain(object obj)
        {
            try
            {
                await _httpRequests.SendCode(Preferences.Get("UserMail", null));

                timer = Application.Current.Dispatcher.CreateTimer();
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += (s, e) => timerCallback();
                timer.Start();

                _btnSendIsVisible = false;
                OnPropertyChanged("BtnSendIsVisible");

                _timeIsVisible = true;
                OnPropertyChanged("TimeIsVisible");
            }
            catch (Exception)
            {
                return;
            }
        }

        private async void ToBackView(object obj)
        {
            await AppShell.Current.GoToAsync($"//{nameof(LoginOrRegisterView)}");
        }

        private void timerCallback()
        {
            _value--;
            if (Value <= 0)
            {
                timer.Stop();

                _value = 60;
                OnPropertyChanged("Value");

                _btnSendIsVisible = true;
                OnPropertyChanged("BtnSendIsVisible");

                _timeIsVisible = false;
                OnPropertyChanged("TimeIsVisible");
            }
            OnPropertyChanged("Value");
            OnPropertyChanged("TimeToNewCode");
        }

        private int _value = 60;
        public int Value
        {
            get { return _value; }
            set { _value = value; OnPropertyChanged(); }
        }


        private string _field1;
		public string Field1
		{
			get { return _field1; }
			set { _field1 = value; OnPropertyChanged(); }
		}

        private string _field2;
        public string Field2
        {
            get { return _field2; }
            set { _field2 = value; OnPropertyChanged(); }
        }

        private string _field3;
        public string Field3
        {
            get { return _field3; }
            set { _field3 = value; OnPropertyChanged(); }
        }

        private string _field4;
        public string Field4
        {
            get { return _field4; }
            set { _field4 = value; OnPropertyChanged(); }
        }

        private string _timeToNewCode;
        public string TimeToNewCode
        {
            get { return "Отправить код повторно можно будет через " + _value + " секунд"; }
            set { _timeToNewCode = value; OnPropertyChanged(); }
        }

        private bool _timeIsVisible = true;
        public bool TimeIsVisible
        {
            get { return _timeIsVisible; }
            set { _timeIsVisible = value; OnPropertyChanged(); }
        }

        private bool _btnSendIsVisible;
        public bool BtnSendIsVisible
        {
            get { return _btnSendIsVisible; }
            set { _btnSendIsVisible = value; OnPropertyChanged(); }
        }

        public ICommand BackCommand { get; set; }
        public ICommand SendAgainCommand { get; set; }
    }
}
