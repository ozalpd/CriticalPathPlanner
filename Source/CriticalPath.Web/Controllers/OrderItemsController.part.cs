using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Threading.Tasks;
using CriticalPath.Data;
using CriticalPath.Web.Models;
using System.Net;
using System.Web.Mvc;

namespace CriticalPath.Web.Controllers
{
    public partial class OrderItemsController 
    {
        partial void SetViewBags(OrderItem orderItem)
        {
            //TODO: Optimize query
            var queryPuchaseOrderId = DataContext.PuchaseOrders;
            //int puchaseOrderId = orderItem == null ? 0 : orderItem.PuchaseOrderId;
            //ViewBag.PuchaseOrderId = new SelectList(queryPuchaseOrderId, "Id", "Title", puchaseOrderId);
            //TODO: Optimize query
            var queryProductId = DataContext.Products;
            int productId = orderItem == null ? 0 : orderItem.ProductId;
            ViewBag.ProductId = new SelectList(queryProductId, "Id", "Title", productId);
        }


        //public new class QueryParameters : BaseController.QueryParameters
        //{

        //}

        //Purpose: To set default property values for newly created OrderItem entity
        //partial void SetDefaults(OrderItem orderItem) { }
    }
}
