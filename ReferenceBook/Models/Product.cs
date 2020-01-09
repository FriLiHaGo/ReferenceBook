namespace ReferenceBook
{
    /// <summary>
    /// Товар из справочника
    /// </summary>
    public class Product : NotifyPropertyChange
    {
        public int Id { get; set; }

        private string vendorCode;
        /// <summary>
        /// Артикул 1
        /// </summary>
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
        /// <summary>
        /// Производитель 1
        /// </summary>
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
        /// <summary>
        /// Артикул 2
        /// </summary>
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
        /// <summary>
        /// Производитель 2
        /// </summary>
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
        /// <summary>
        /// Доверие
        /// </summary>
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
}
