using SalesApp.DomainLayer.Model.Transactions;
using SalesApp.Infrastructure.Operations;
using System.Runtime.CompilerServices;

namespace SalesApp.Infrastructure
{
    internal class Program
    {
        static void Main(string[] args)
        {   
            SellingDB sellingdb = new SellingDB();
            Selling selling = new Selling();
            selling = sellingdb.GetSellingById(1);
            Console.WriteLine("id da venda: " + selling.Id + " valor total: " + selling.TotalValue);
            foreach(SellingItem sellingItem in selling.SellingItems)
            {
                Console.WriteLine("preco: " + sellingItem.Price);
                Console.WriteLine("quantidade" + sellingItem.Quantity);
            }

            Selling selling2 = new Selling(1);
            SellingItem item = new SellingItem();
            item.Price = 12.00m;
            item.Quantity = 8;
            item.ProductId = 1;
            item.CompanyId = 1;
            selling2.SaleStartDate = DateTime.Now;
            selling2.SaleEndDate = DateTime.Now;
            selling2.AddProduct(item);
            selling2.TotalValue = selling2.CalculateTotalValue();
            sellingdb.AddSelling(selling2);
            selling = sellingdb.GetSellingById(2);
            Console.WriteLine("id da venda: " + selling.Id);
            foreach (SellingItem sellingItem in selling.SellingItems)
            {
                Console.WriteLine("preco: " + sellingItem.Price);
                Console.WriteLine("quantidade" + sellingItem.Quantity);
            }
            Console.WriteLine(selling.TotalValue);
            Console.WriteLine("data antiga: " + selling.SaleEndDate);
            selling.SaleEndDate = DateTime.Now;
            sellingdb.UpdateSelling(selling);
            selling = sellingdb.GetSellingById(2);
            Console.WriteLine("nova data: " + selling.SaleEndDate);

        }
            
    }
}
