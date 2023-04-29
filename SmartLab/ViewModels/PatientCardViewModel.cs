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
    public class PatientCardViewModel : ViewModelBase
    {
        private readonly HttpRequests _httpRequests;
        public PatientCardViewModel()
        {
            _httpRequests = new HttpRequests();

            SkipCommand = new Command(BtnSkip);
            CreateCommand = new Command(CreatePatientCard);
        }

        private async void CreatePatientCard(object obj)
        {
            try
            {
                Preferences.Set("UserName", UserName);
                Preferences.Set("UserLastName", UserLastName);
                Preferences.Set("UserMiddleName", UserMiddleName);
                Preferences.Set("Birthday", Birthday);
                Preferences.Set("Gender", Gender);

                await _httpRequests.CreateUser();

                await AppShell.Current.GoToAsync($"//{nameof(AnalyzesView)}");
            }
            catch (Exception)
            {
                return;
            }
            
        }

        private async void BtnSkip(object obj)
        {
            await AppShell.Current.GoToAsync($"//{nameof(AnalyzesView)}");
        }

        public ICommand SkipCommand { get; set; }
        public ICommand CreateCommand { get; set; }

        private string _userName = string.Empty;
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; OnPropertyChanged(); OnPropertyChanged("NextBtnIsEnabled"); OnPropertyChanged("BtnOpacity"); }
        }

        private string _userMiddleName = string.Empty;
        public string UserMiddleName
        {
            get { return _userMiddleName; }
            set { _userMiddleName = value; OnPropertyChanged(); OnPropertyChanged("NextBtnIsEnabled"); OnPropertyChanged("BtnOpacity"); }
        }

        private string _userLastName = string.Empty;
        public string UserLastName
        {
            get { return _userLastName; }
            set { _userLastName = value; OnPropertyChanged(); OnPropertyChanged("NextBtnIsEnabled"); OnPropertyChanged("BtnOpacity"); }
        }

        private string _birthday = string.Empty;
        public string Birthday
        {
            get { return _birthday; }
            set { _birthday = value; OnPropertyChanged(); OnPropertyChanged("NextBtnIsEnabled"); OnPropertyChanged("BtnOpacity"); }
        }

        private string _gender = string.Empty;
        public string Gender
        {
            get { return _gender; }
            set { _gender = value; OnPropertyChanged(); OnPropertyChanged("NextBtnIsEnabled"); OnPropertyChanged("BtnOpacity"); }
        }

        private bool _nextBtnIsEnabled;
        public bool NextBtnIsEnabled
        {
            get
            {
                return _userName.Trim() == string.Empty || _userName == null
                    || _userLastName.Trim() == string.Empty || _userLastName == null
                    || _userMiddleName.Trim() == string.Empty || _userMiddleName == null
                    || _birthday.Trim() == string.Empty || _birthday == null
                    || _gender.Trim() == string.Empty || _gender == null
                    ? false : true;
            }
            set { _nextBtnIsEnabled = value; OnPropertyChanged(); }
        }

        private double _btnOpacity;
        public double BtnOpacity
        {
            get { return _userName.Trim() == string.Empty || _userName == null
                    || _userLastName.Trim() == string.Empty || _userLastName == null
                    || _userMiddleName.Trim() == string.Empty || _userMiddleName == null
                    || _birthday.Trim() == string.Empty || _birthday == null
                    || _gender.Trim() == string.Empty || _gender == null ? 0.4 : 1; }
            set { _btnOpacity = value; OnPropertyChanged(); }
        }
    }
}
