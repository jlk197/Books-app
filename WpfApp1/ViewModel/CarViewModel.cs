using Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.ViewModel
{
    class CarViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private ICar car;
        public CarViewModel(ICar car)
        {
            this.car = car;
        }

        public int Id
        {
            get { return car.Id; }
            set
            {
                car.Id = value;
                RaisePropertyChanged(nameof(Id));
            }
        }
        public string Name
        {
            get { return car.Name; }
            set
            {
                car.Name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }

        public int ProducerId
        {
            get { return car.ProducerId; }
            set
            {
                car.ProducerId = value;
                RaisePropertyChanged(nameof(ProducerId));
            }
        }

        public int Engine
        {
            get { return car.Engine; }
            set
            {
                car.Engine = value;
                RaisePropertyChanged(nameof(Engine));
            }
        }

        public int Mileage
        {
            get { return car.Mileage; }
            set
            {
                car.Mileage = value;
                RaisePropertyChanged(nameof(Mileage));
            }
        }
        public int ProdYear
        {
            get { return car.ProdYear; }
            set
            {
                car.ProdYear = value;
                RaisePropertyChanged(nameof(ProdYear));
            }
        }
        public TransmissionType Transmission
        {
            get { return car.Transmission; }
            set
            {
                car.Transmission = value;
                RaisePropertyChanged(nameof(Transmission));
            }
        }

        private void RaisePropertyChanged(string propertyName) {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
