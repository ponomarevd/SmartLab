using SmartLab.Core;
using SmartLab.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLab.ViewModels
{
    public class OnBoardViewModel : ViewModelBase
    {
        public OnBoardViewModel() 
        {
            OnBoardItems.Add(new OnBoardItem
            {
                Title = "Анализ",
                Description = "Экспресс сбор и получение проб",
                ImageName = "onboardfirstimage.png",
                ButtonText = "Пропустить",
                ButtonCommand = new Command<string>(ExecuteButtonCommand)
            });
            OnBoardItems.Add(new OnBoardItem
            {
                Title = "Уведомления",
                Description = "Вы быстро узнаете о результатах",
                ImageName = "onboardsecondimage.png",
                ButtonText = "Пропустить",
                ButtonCommand = new Command<string>(ExecuteButtonCommand)
            });
            OnBoardItems.Add(new OnBoardItem
            {
                Title = "Мониторинг",
                Description = "Наши врачи всегда наблюдают за вашими показателями здоровья",
                ImageName = "onboardthirdimage.png",
                ButtonText = "Завершить",
                ButtonCommand = new Command<string>(ExecuteButtonCommand)
            });
        }

        private async void ExecuteButtonCommand(string obj)
        {
            await AppShell.Current.GoToAsync($"//{nameof(LoginOrRegisterView)}");
        }

        private ObservableCollection<OnBoardItem> _onBoardItems = new ObservableCollection<OnBoardItem>();
        public ObservableCollection<OnBoardItem> OnBoardItems
        {
            get { return _onBoardItems; }
            set { _onBoardItems = value; OnPropertyChanged(); }
        }

        private int _position;
        public int Position
        {
            get { return _position; }
            set { _position = value; }
        }


    }
}
