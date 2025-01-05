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
    class BookViewModel : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        public event PropertyChangedEventHandler? PropertyChanged;


        private IBook book;
        public IBook Book => book;
        public BookViewModel(IBook book, ProducerViewModel? producer)
        {
            this.book = book;
            this.isChanged = false;
            if (producer != null) this.producer = producer;
        }

        public int Id
        {
            get { return book.Id; }
            set
            {
                book.Id = value;
                RaisePropertyChanged(nameof(Id));
            }
        }
        [Required(ErrorMessage = "Nazwa musi zostać nadana")]
        [StringLength(30, MinimumLength = 1, ErrorMessage ="Długość nazwy <1, 30>")]
        public string Title
        {
            get { return book.Title; }
            set
            {
                book.Title = value;
                this.isChanged = true;
                RaisePropertyChanged(nameof(Title));
            }
        }

        private ProducerViewModel producer;
        [Required]
        public ProducerViewModel Producer
        {
            get { return producer; }
            set
            {
                if (producer != value)
                {
                    producer = value;
                    this.book.Producer = producer.Producer;
                    this.isChanged = true;
                    RaisePropertyChanged(nameof(Producer));
                }
            }
        }

        [Required]
        [Range(1850, 2025, ErrorMessage = "Rok wydania <1850, 2025>")]
        public int PublicationYear
        {
            get { return book.PublicationYear; }
            set
            {
                book.PublicationYear = value; 
                this.isChanged = true;
                RaisePropertyChanged(nameof(PublicationYear));
            }
        }
        public GenreType Genre
        {
            get { return book.Genre; }
            set
            {
                book.Genre = value; 
                this.isChanged = true;
                RaisePropertyChanged(nameof(Genre));
            }
        }


        private void RaisePropertyChanged(string propertyName) {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
            if (propertyName != nameof(HasErrors)) {
                Validate();
            }
        }

       

        #region INotifyDataErrorInfo implementation
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public bool HasErrors => errorsCollection.Count>0;

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
            if(ErrorsChanged != null)
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

            foreach (var kv in errorsCollection.ToList()) { 
                if(validationResults.All(r => r.MemberNames.All(m => m!= kv.Key)))
                {
                    errorsCollection.Remove(kv.Key);
                    RaiseErrorChanged(kv.Key);
                }
            }
            var q = from result in validationResults
                    from member in result.MemberNames
                    group result by member into gr
                    select gr;


            foreach (var prop in q) { 
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
        public bool IsChanged {
            get { return isChanged; }
            set {
                isChanged = value;
                RaisePropertyChanged(nameof(isChanged));
            }
        }
    }
}
