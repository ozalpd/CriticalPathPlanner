using CP.i8n;
using CriticalPath.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CriticalPath.Web.Models
{
    public class ProductEditVM : ProductDTO
    {
        public ProductEditVM() { }
        public ProductEditVM(Product entity) : base(entity) { }

        protected override void Constructing(Product entity)
        {
            base.Constructing(entity);
            foreach (var item in entity.Suppliers)
            {
                Suppliers.Add(new SupplierDTO(item));
            }
        }

        [Display(ResourceType = typeof(EntityStrings), Name = "Suppliers")]
        public virtual ICollection<SupplierDTO> Suppliers
        {
            set { _suppliers = value; }
            get
            {
                if (_suppliers == null)
                    _suppliers = new List<SupplierDTO>();
                return _suppliers;
            }

        }
        ICollection<SupplierDTO> _suppliers;

        public int[] SuppliersSelected { get; set; }
    }
}