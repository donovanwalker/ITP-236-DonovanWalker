using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace Week11Part
{
    public class Parts
    {
        /// <summary>
        /// Retrieves parts of a specified type, including related data.
        /// </summary>
        /// <param name="partType">The type of parts to retrieve.</param>
        /// <returns>A list of parts with the specified type.</returns>
        public static List<Part> GetParts(Program.PartType partType)
        {
            using (var context = new SalesModel())
            {
                return context.Parts
                    .Include(p => p.SalesOrderParts.Select(sop => sop.SalesOrder)) // Include SalesOrderParts and SalesOrder
                    .Include(p => p.SalesOrderParts.Select(sop => sop.SalesOrder.Customer)) // Include Customer
                    .Where(p => p.PartTypeId == (int)partType)
                    .ToList();
            }
        }

        /// <summary>
        /// Retrieves parts that have been spoiled.
        /// </summary>
        /// <returns>A list of spoiled parts.</returns>
        public static List<Part> GetSpoiledParts()
        {
            using (var context = new SalesModel())
            {
                // Assuming there's a property like IsSpoiled in your Part entity.  If not, adjust the where clause.
                return context.Parts
                    .Include(p => p.SalesOrderParts.Select(sop => sop.SalesOrder))  // Include SalesOrderParts
                    .Include(p => p.SalesOrderParts.Select(sop => sop.SalesOrder.Customer)) // Include Customer
                    .Where(p => p.UnitsSpoiled.Any(us => us.Quantity > 0)) // Example:  Adapt this condition to your actual "spoiled" criteria
                    .ToList();
            }
        }

        /// <summary>
        /// Calculates the total units shipped for a part.
        /// </summary>
        /// <param name="part">The part for which to calculate shipped units.</param>
        /// <returns>The total number of units shipped.</returns>
        public static int GetUnitsShipped(Part part)
        {
            using (var context = new SalesModel())
            {
                 context.Parts.Attach(part);
                 return part.SalesOrderParts.Sum(sop => sop.UnitsShipped);
            }

        }

        /// <summary>
        /// Retrieves parts that are backordered.
        /// </summary>
        /// <returns>A list of backordered parts.</returns>
        public static List<Part> GetBackorderedParts()
        {
            using (var context = new SalesModel())
            {
                return context.Parts
                    .Include(p => p.SalesOrderParts.Select(sop => sop.SalesOrder)) // Include SalesOrderParts and SalesOrder
                    .Include(p => p.SalesOrderParts.Select(sop => sop.SalesOrder.Customer))// Include Customer
                    .Where(p => p.SalesOrderParts.Any(sop => sop.Quantity > sop.UnitsShipped))
                    .ToList();
            }
        }

        /// <summary>
        /// Retrieves parts that have been sold more than a specified quantity.
        /// </summary>
        /// <param name="quantitySold">The minimum quantity sold.</param>
        /// <returns>A list of parts that meet the quantity sold criteria.</returns>
        public static List<Part> GetPopularParts(int quantitySold)
        {
            using (var context = new SalesModel())
            {
                return context.Parts
                    .Include(p => p.SalesOrderParts.Select(sop => sop.SalesOrder))  // Include SalesOrderParts
                    .Include(p => p.SalesOrderParts.Select(sop => sop.SalesOrder.Customer))// Include Customer
                    .Where(p => p.SalesOrderParts.Sum(sop => sop.Quantity) > quantitySold)
                    .ToList();
            }
        }

        /// <summary>
        /// Retrieves the best-selling parts based on total dollar sales, sorted descending.
        /// </summary>
        /// <param name="amount">The minimum total dollar sales.</param>
        /// <returns>A list of best-selling parts.</returns>
        public static List<Part> GetBestSellingParts(decimal amount)
        {
            using (var context = new SalesModel())
            {
                return context.Parts
                     .Include(p => p.SalesOrderParts.Select(sop => sop.SalesOrder)) // Include SalesOrderParts and SalesOrder
                    .Include(p => p.SalesOrderParts.Select(sop => sop.SalesOrder.Customer))// Include Customer
                    .Where(p => p.SalesOrderParts.Sum(sop => sop.ExtendedPrice) >= amount)
                    .OrderByDescending(p => p.SalesOrderParts.Sum(sop => sop.ExtendedPrice))
                    .ToList();
            }
        }

        /// <summary>
        /// Retrieves the best-selling parts based on total units sales, sorted descending.
        /// </summary>
        /// <param name="quantity">The minimum total units sales.</param>
        /// <returns>A list of best-selling parts.</returns>
        public static List<Part> GetBestSellingParts(int quantity)
        {
            using (var context = new SalesModel())
            {
                return context.Parts
                    .Include(p => p.SalesOrderParts.Select(sop => sop.SalesOrder))  // Include SalesOrderParts
                    .Include(p => p.SalesOrderParts.Select(sop => sop.SalesOrder.Customer))// Include Customer
                    .Where(p => p.SalesOrderParts.Sum(sop => sop.Quantity) >= quantity)
                    .OrderByDescending(p => p.SalesOrderParts.Sum(sop => sop.Quantity))
                    .ToList();
            }
        }
    }
}
