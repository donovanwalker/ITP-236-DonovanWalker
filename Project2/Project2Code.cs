using System;
using System.Collections.Generic;
using System.Linq;

namespace Project2
{
    public partial class Customer
    {
        public const string StudentName = "Donovan Walker";

        public decimal TotalSales => SalesOrders?.Sum(so => so.OrderTotal) ?? 0;

        public decimal TotalCost => SalesOrders?.Sum(so => so.OrderCost) ?? 0;

        public decimal GrossProfit => TotalSales - TotalCost;

        public int ItemsSold => SalesOrders?.Sum(so => so.SalesOrderParts.Sum(sop => sop.Quantity)) ?? 0;

        public SalesOrder LargestSale => SalesOrders?.OrderByDescending(so => so.OrderTotal).FirstOrDefault();

        public List<CustomerItem> CustomerItems => SalesOrders?
            .SelectMany(so => so.SalesOrderParts)
            .GroupBy(sop => new { sop.Part.PartId, sop.Part.Name })
            .Select(g => new CustomerItem
            {
                PartId = g.Key.PartId,
                PartName = g.Key.Name,
                Quantity = g.Sum(x => x.Quantity),
                ExtendedPrice = g.Sum(x => x.ExtendedPrice),
                UnitsShipped = g.Sum(x => x.UnitsShipped),
                BackOrdered = g.Sum(x => x.Quantity - x.UnitsShipped)
            }).ToList() ?? new List<CustomerItem>();
    }

    public partial class Part
    {
        public int QuantityOnHand => UnitsReceived?.Sum(ur => ur.Quantity) ?? 0
                                      - UnitsSpoiled?.Sum(us => us.Quantity) ?? 0
                                      - SalesOrderParts?.Sum(sop => sop.UnitsShipped) ?? 0;

        public int UnitsSold => SalesOrderParts?.Sum(sop => sop.Quantity) ?? 0;

        public decimal CurrentValue => UnitsReceived?.Sum(ur => ur.ExtendedPrice) ?? 0
                                        - UnitsSpoiled?.Sum(us => us.ExtendedPrice) ?? 0
                                        - SalesOrderParts?.Sum(sop => sop.ExtendedPrice) ?? 0;

        public decimal AmountSold => SalesOrderParts?.Sum(sop => sop.ExtendedPrice) ?? 0;

        public List<Customer> Customers => SalesOrderParts?
            .Select(sop => sop.SalesOrder.Customer)
            .Distinct()
            .ToList() ?? new List<Customer>();
    }

    public partial class SalesOrder
    {
        public int ItemsSold => SalesOrderParts?.Sum(sop => sop.Quantity) ?? 0;

        public int UnitsShipped => SalesOrderParts?.Sum(sop => sop.UnitsShipped) ?? 0;

        public int BackOrdered => ItemsSold - (UnitsShipped ?? 0);

        public decimal OrderTotal => SalesOrderParts?.Sum(sop => sop.ExtendedPrice) ?? 0;

        decimal OrderCost => SalesOrderParts?.Sum(sop => sop.ExtendedCost) ?? 0;
    }
}
