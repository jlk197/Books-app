using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.ViewModel
{
    class WindowViewModel
    {
        private BooksListViewModel booksView;
        public BooksListViewModel BooksView
        {
            get { return booksView; }
            set { booksView = value; }
        }

        private ProducerListViewModel producerView;
        public ProducerListViewModel ProducerView
        {
            get { return producerView; }
            set { producerView = value; }
        }

        public WindowViewModel()
        {
            producerView = new ProducerListViewModel();
            booksView = new BooksListViewModel(producerView.Producers);
        }
    }
}
