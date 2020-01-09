using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReferenceBook
{
    internal class ProductVM : NotifyPropertyChange
    {
        public ProductVM(Product product)
        {
            Product = product;
        }

        private Product product;
        public Product Product
        {
            get => product;
            set
            {
                product = value;
                OnPropertyChanged();
            }
        }
    }
}
