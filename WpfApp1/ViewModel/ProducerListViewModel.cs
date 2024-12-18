using Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace WpfApp1.ViewModel
{
    class ProducerListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private ListCollectionView view;

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private ObservableCollection<ProducerViewModel> producers;
        public ObservableCollection<ProducerViewModel> Producers
        {
            get { return producers; }
            set { producers = value; RaisePropertyChanged(nameof(Producers)); }
        }
        private IDaoMock daoMock;
        public ProducerListViewModel()
        {

            string libraryName = ConfigurationManager.AppSettings["library_name"];
            daoMock = new BLC.BLC(libraryName).DAO;
            producers = new ObservableCollection<ProducerViewModel>();
            foreach (var p in daoMock.GetAllProducers())
            {
                producers.Add(new ProducerViewModel(p));
            }
            view = (ListCollectionView)CollectionViewSource.GetDefaultView(producers);
            addNewProducerCommand = new RelayCommand(_ => AddNewProducer(), _ => CanAddProducer());
            saveProducerCommand = new RelayCommand(_ => SaveProducer(), _ => CanSaveChanges());
            filterDataCommand = new RelayCommand(_ => FilterData());
        }

        private void AddNewProducer()
        {
            ProducerViewModel cvm = new ProducerViewModel(daoMock.CreateNewProducent());
            cvm.IsChanged = true;
            EditedProducer = cvm;
        }

        private RelayCommand addNewProducerCommand;
        public ICommand AddNewProducerCommand
        {
            get => addNewProducerCommand;
        }

        private ProducerViewModel selectedProducer;
        public ProducerViewModel SelectedProducer
        {
            get { return selectedProducer; }
            set
            {
                selectedProducer = value;
                if (CanAddProducer())
                {
                    EditedProducer = selectedProducer;
                }
                RaisePropertyChanged(nameof(SelectedProducer));
            }
        }

        private ProducerViewModel editedProducer;
        public ProducerViewModel EditedProducer
        {
            get { return editedProducer; }
            set
            {
                editedProducer = value;
                RaisePropertyChanged(nameof(EditedProducer));
            }
        }
      
        private RelayCommand saveProducerCommand;
        public ICommand SaveProducerCommand { get => saveProducerCommand; }

        private void SaveProducer()
        {
            if (!EditedProducer.HasErrors)
            {
                if (EditedProducer.Id == 0)
                {
                    producers.Add(EditedProducer);
                    daoMock.AddProducer(EditedProducer.Producer);
                }
                daoMock.SaveChanges();
                EditedProducer.IsChanged = false;
                EditedProducer = null;
            }
        }

        private bool CanAddProducer()
        {
            if (EditedProducer == null || !EditedProducer.IsChanged)
            {
                return true;
            }
            return false;
        }

        private bool CanSaveChanges()
        {
            if (EditedProducer == null || !EditedProducer.IsChanged)
            {
                return false;
            }
            else
            {
                return !EditedProducer.HasErrors;
            }
        }
        private String filter;
        public String Filter
        {
            get { return filter; }
            set
            {
                filter = value;
                RaisePropertyChanged(nameof(Filter));
            }
        }

        private RelayCommand filterDataCommand;
        public RelayCommand FilterDataCommand { get => filterDataCommand; }
        public void FilterData()
        {
            if (string.IsNullOrEmpty(filter))
            {
                view.Filter = null;
            }
            else
            {
                view.Filter = c => ((ProducerViewModel)c).Name.Contains(filter, StringComparison.OrdinalIgnoreCase);
            }
        }
    }
}
