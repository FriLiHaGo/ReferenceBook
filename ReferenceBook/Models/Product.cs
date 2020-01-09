using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReferenceBook
{
    public class Product : NotifyPropertyChange
    {
        public int Id { get; set; }

        private string vendorCode;
        public string VendorCode
        {
            get => vendorCode;
            set
            {
                vendorCode = value;
                OnPropertyChanged();
            }
        }

        private string brand;
        public string Brand
        {
            get => brand;
            set
            {
                brand = value;
                OnPropertyChanged();
            }
        }

        private string analogueVendorCode;
        public string AnalogueVendorCode
        {
            get => analogueVendorCode;
            set
            {
                analogueVendorCode = value;
                OnPropertyChanged();
            }
        }

        private string analogueBrand;
        public string AnalogueBrand
        {
            get => analogueBrand;
            set
            {
                analogueBrand = value;
                OnPropertyChanged();
            }
        }

        private int trust;
        public int Trust
        {
            get => trust;
            set
            {
                trust = value;
                OnPropertyChanged();
            }
        }
    }

    public class ReferenceBookContext : DbContext
    {
        public ReferenceBookContext() :
            base("DataBase")
        { }

        public DbSet<Product> Products { get; set; }
    }
}
