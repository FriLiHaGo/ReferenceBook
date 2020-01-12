namespace ReferenceBook
{
    /// <summary>
    /// Товар - узел графа
    /// </summary>
    public class ProductNode
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public ProductNode()
        {

        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="vendorCode"></param>
        /// <param name="brand"></param>
        public ProductNode(string vendorCode, string brand)
        {
            VendorCode = vendorCode;
            Brand = brand;
        }

        private string vendorCode;
        /// <summary>
        /// Артикул
        /// </summary>
        public string VendorCode
        {
            get => vendorCode;
            set
            {
                vendorCode = value.Trim();
            }
        }

        private string brand;
        /// <summary>
        /// Произdодитель
        /// </summary>
        public string Brand
        {
            get => brand;
            set
            {
                brand = value.Trim();
            }
        }

        public override string ToString()
        {
            return string.Format("{0} / {1}", VendorCode, Brand);
        }
    }
}
