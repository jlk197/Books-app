using Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.ViewModel
{
    class ProducerViewModel : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        public event PropertyChangedEventHandler? PropertyChanged;


        private IProducer producer;
        public IProducer Producer => producer;
        public ProducerViewModel(IProducer producer)
        {
            this.producer = producer;
            this.isChanged = false;
        }

        public int Id
        {
            get { return producer.Id; }
            set
            {
                producer.Id = value;
                RaisePropertyChanged(nameof(Id));
            }
        }
        [Required(ErrorMessage = "Nazwa musi zostać nadana")]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "Długość nazwy <1, 30>")]
        public string Name
        {
            get { return producer.Name; }
            set
            {
                producer.Name = value;
                this.isChanged = true;
                RaisePropertyChanged(nameof(Name));
            }
        }

        [Required(ErrorMessage = "Kraj musi zostać podany")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Długość nazwy kraju <1, 20>")]
        public string Country
        {
            get { return producer.Country; }
            set
            {
                producer.Country = value;
                this.isChanged = true;
                RaisePropertyChanged(nameof(Country));
            }
        }

        [Required]
        [Range(1900, 2025, ErrorMessage = "Rok założenia <1900, 2025>")]
        public int EstablishmentYear
        {
            get { return producer.EstablishmentYear; }
            set
            {
                producer.EstablishmentYear = value;
                this.isChanged = true;
                RaisePropertyChanged(nameof(EstablishmentYear));
            }
        }

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
            if (propertyName != nameof(HasErrors))
            {
                Validate();
            }
        }



        #region INotifyDataErrorInfo implementation
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public bool HasErrors => errorsCollection.Count > 0;

        public IEnumerable GetErrors(string? propertyName)
        {
            if (string.IsNullOrEmpty(propertyName) || !errorsCollection.ContainsKey(propertyName))
            {
                return null;
            }
            return errorsCollection[propertyName];
        }

        private Dictionary<string, ICollection<string>>? errorsCollection = new Dictionary<string, ICollection<string>>();
        protected void RaiseErrorChanged(string propertyName)
        {
            if (ErrorsChanged != null)
            {
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
                RaisePropertyChanged(nameof(HasErrors));
            }
        }

        public void Validate()
        {
            var validationContext = new ValidationContext(this, null, null);
            var validationResults = new List<ValidationResult>();

            Validator.TryValidateObject(this, validationContext, validationResults, true);

            foreach (var kv in errorsCollection.ToList())
            {
                if (validationResults.All(r => r.MemberNames.All(m => m != kv.Key)))
                {
                    errorsCollection.Remove(kv.Key);
                    RaiseErrorChanged(kv.Key);
                }
            }
            var q = from result in validationResults
                    from member in result.MemberNames
                    group result by member into gr
                    select gr;


            foreach (var prop in q)
            {
                var messages = prop.Select(m => m.ErrorMessage).ToList();

                if (errorsCollection.ContainsKey(prop.Key))
                {
                    errorsCollection.Remove(prop.Key);
                }
                errorsCollection.Add(prop.Key, messages);
                RaiseErrorChanged(prop.Key);
            }
        }
        #endregion

        private bool isChanged;
        public bool IsChanged
        {
            get { return isChanged; }
            set
            {
                isChanged = value;
                RaisePropertyChanged(nameof(isChanged));
            }
        }
    }
}
