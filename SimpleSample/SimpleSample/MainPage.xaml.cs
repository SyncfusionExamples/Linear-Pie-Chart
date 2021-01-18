using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SimpleSample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            viewModel.Data.Add(new PriceData() { Component = "Hard Disk", Price = 80 });
        }
    }
    public class ViewModel : INotifyPropertyChanged
    {

        public ObservableCollection<PriceData> Data1 { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<PriceData> data;
        public ObservableCollection<PriceData> Data
        {
            get { return data; }
            set
            {
                data = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Data"));
            }
        }

        public ViewModel()
        {
            Data = new ObservableCollection<PriceData>()
            {
                 new PriceData() {Component = "Hard Disk", Price = 80 },
                 new PriceData() {Component = "Scanner", Price = 140  },
                 new PriceData() {Component = "Monitor", Price = 150  },
                 new PriceData() {Component = "Printer", Price = 180  },
            };

            Data1 = new ObservableCollection<PriceData>()
            {
                 new PriceData() {Component = "Hard Disk", Price = 80 },
            };
        }

    }

    public class PriceData
    {
        public string Component { get; set; }

        public double Price { get; set; }
    }
}
