using CarsDB;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KlechammerKardas.Books.UI.ViewModel 
{
    class CarListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private ObservableCollection<CarViewModel> cars;
        public ObservableCollection<CarViewModel> Cars { 
            get { return cars; }
            set { cars = value; RaisePropertyChanged(nameof(Cars)); }
        }
        public CarListViewModel() {
            cars = new ObservableCollection<CarViewModel>();
            IParking parking = new CarsDB.Parking();
            foreach (var car in parking.GetAllCars()) { 
                cars.Add(new CarViewModel(car));
            }

            addNewCarCommand = new RelayCommand(_ => AddNewCar());
        }

        public void AddNewCar() {
            CarViewModel cvm = new CarViewModel(new CarsDB.Car());
            cars.Add(cvm);
            SelectedCar = cvm;
        }

        private RelayCommand addNewCarCommand;
        public ICommand AddNewCarCommand
        {
            get => addNewCarCommand;
        }

        private CarViewModel selectedCar;
        public CarViewModel SelectedCar
        {
            get { return selectedCar; }
            set
            {
                selectedCar = value;
                RaisePropertyChanged(nameof(SelectedCar));
            }
        }
    }
}
