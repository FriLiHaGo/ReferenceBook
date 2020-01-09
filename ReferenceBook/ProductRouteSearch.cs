using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ReferenceBook
{
    /// <summary>
    /// Параметры поиска для <see cref="ProductNode"/>
    /// </summary>
    public class ProductRouteSearch : IRouteSearchParameter<ProductNode>
    {
        public bool Equals(ProductNode nodeA, ProductNode nodeB)
        {
            return VendorCodeCompare(nodeA.VendorCode, nodeB.VendorCode) && BrandCompare(nodeA.Brand, nodeB.Brand);
        }

        public bool IsSearchConitue(ProductNode nodeA, ProductNode nodeB)
        {
            using (var db = new ReferenceBookContext())
            {
                var product = db.Products.ToList().FirstOrDefault(p =>
                    VendorCodeCompare(p.VendorCode, nodeA.VendorCode) &&
                    BrandCompare(p.Brand, nodeA.Brand) &&
                    VendorCodeCompare(p.AnalogueVendorCode, nodeB.VendorCode) &&
                    BrandCompare(p.AnalogueBrand,nodeB.Brand));

                return product?.Trust != 0 ? true : false;
            }
        }

        public IEnumerable<ProductNode> GetNodes(ProductNode node)
        {
            using(var db = new ReferenceBookContext())
            {
                return db.Products
                    .Where(p => p.VendorCode == node.VendorCode && p.Brand == node.Brand)
                    .Select(p => new ProductNode() 
                    {
                        VendorCode = p.AnalogueVendorCode,
                        Brand = p.AnalogueBrand
                    })
                    .ToList();
            }
        }

        #region Private

        private bool VendorCodeCompare(string a, string b)
        {
            var pattern = @"\W";
            var regex = new Regex(pattern);
            a = regex.Replace(a, "");
            b = regex.Replace(b, "");
            return string.Compare(a, b, true) == 0 ? true : false;
        }

        private bool BrandCompare(string a, string b)
        {
            a = a.Trim();
            b = b.Trim();
            return string.Compare(a, b, true) == 0 ? true : false;
        }

        #endregion
    }
}
