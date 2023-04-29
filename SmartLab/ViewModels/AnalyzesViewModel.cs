using MauiPopup;
using SmartLab.Core;
using SmartLab.Models;
using SmartLab.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SmartLab.ViewModels
{
    public class AnalyzesViewModel : ViewModelBase
    {
        private readonly HttpRequests _httpRequests;
        public AnalyzesViewModel()
        {
            _httpRequests = new HttpRequests();
            _httpRequests.GetNews(this);
            _httpRequests.GetCatalog(this);

            CarouselItem.Add(new CatalogButtonItem
            {
                Name = "Популярные",
                ButtonCommand = new Command<string>(ButtonCommand1)
            });
            CarouselItem.Add(new CatalogButtonItem
            {
                Name = "Covid",
                ButtonCommand = new Command<string>(ButtonCommand2)
            });
            CarouselItem.Add(new CatalogButtonItem
            {
                Name = "Онкогенетические",
                ButtonCommand = new Command<string>(ButtonCommand3)
            });
            CarouselItem.Add(new CatalogButtonItem
            {
                Name = "ЗОЖ",
                ButtonCommand = new Command<string>(ButtonCommand4)
            });
        }

        private void ButtonCommand4(string obj)
        {
            _httpRequests.GetCatalog(this);
            CatalogCollection = new ObservableCollection<CatalogItem>(CatalogCollection.Where(x => x.category == "ЗОЖ"));
            OnPropertyChanged(nameof(CatalogCollection));
        }

        private void ButtonCommand3(string obj)
        {
            _httpRequests.GetCatalog(this);
            CatalogCollection = new ObservableCollection<CatalogItem>(CatalogCollection.Where(x => x.category == "Онкогенетические"));
            OnPropertyChanged(nameof(CatalogCollection));
        }

        private void ButtonCommand2(string obj)
        {
            _httpRequests.GetCatalog(this);
            CatalogCollection = new ObservableCollection<CatalogItem>(CatalogCollection.Where(x => x.category == "COVID"));
            OnPropertyChanged(nameof(CatalogCollection));
        }

        private void ButtonCommand1(string obj)
        {
            _httpRequests.GetCatalog(this);
            CatalogCollection = new ObservableCollection<CatalogItem>(CatalogCollection.Where(x => x.category == "Популярные"));
            OnPropertyChanged(nameof(CatalogCollection));
        }

        private ObservableCollection<CatalogButtonItem> _carouselItem = new ObservableCollection<CatalogButtonItem>();
        public ObservableCollection<CatalogButtonItem> CarouselItem
        {
            get { return _carouselItem; }
            set { _carouselItem = value; OnPropertyChanged(); }
        }

        private ObservableCollection<NewsItem> _newsCollection;
        public ObservableCollection<NewsItem> NewsCollection
        {
            get { return _newsCollection; }
            set { _newsCollection = value; OnPropertyChanged(); }
        }
        private ObservableCollection<CatalogItem> _catalogCollection;
        public ObservableCollection<CatalogItem> CatalogCollection
        {
            get { return _catalogCollection; }
            set { _catalogCollection = value; OnPropertyChanged(); }
        }

        private int _translationY;

        public int TranslationY
        {
            get { return _translationY; }
            set { _translationY = value; OnPropertyChanged(); }
        }

    }
}
