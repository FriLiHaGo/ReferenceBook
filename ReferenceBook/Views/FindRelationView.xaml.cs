using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ReferenceBook
{
    public partial class FindRelationView : Window
    {
        public FindRelationView()
        {
            InitializeComponent();
        }

        public Dictionary<int, ICollection<Product>> Relations { get; private set; } = new Dictionary<int, ICollection<Product>>();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ProductNode productA, productB;
            using(var db = new ReferenceBookContext())
            {
                productA = new ProductNode()
                {
                    VendorCode = VendorCode.Text,
                    Brand = Brand.Text
                };
                productA.Analogues = db.Products.Where(p => p.VendorCode == VendorCode.Text && p.Brand == Brand.Text).ToList();

                productB = new ProductNode()
                {
                    VendorCode = AnalogueVendorCode.Text,
                    Brand = AnalogueBrand.Text
                };
            }


        }

        public void FindReletaion(ProductNode productA, ProductNode productB)
        {
            using(var db = new ReferenceBookContext())
            {
                foreach(var analogue in productA.Analogues)
                {
                    var analogueNode = CreateProductNode(analogue);
                }
            }
        }

        private void FindReletaionInternal(ProductNode productA, ProductNode productB, int deepCount, ICollection<ProductNode> current)
        {

        }

        private ProductNode CreateProductNode(Product product)
        {
            ProductNode productNode;
            using (var db = new ReferenceBookContext())
            {
                productNode = new ProductNode()
                {
                    VendorCode = product.VendorCode,
                    Brand = product.Brand
                };
                productNode.Analogues = db.Products.Where(p => p.VendorCode == VendorCode.Text && p.Brand == Brand.Text).ToList();
            }
            return productNode;
        }
    }
}
