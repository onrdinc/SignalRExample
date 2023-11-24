using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalR.API.Contexts;
using SignalR.API.Hubs;
using SignalR.API.Models;

namespace SignalR.API.Services
{
    public class ProductService
    {
        private readonly DataContext _context;
        private readonly IHubContext<MyHub> _hubContext;

        public ProductService(DataContext context, IHubContext<MyHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public async void AddData()
        {
            try
            {
                Random rand = new Random();
                string[] list = { "Altın", "Dolar", "Euro", "Pound"};

                while (true)
                {
                    int sayi = rand.Next(1, 10);
                    string selectedProduct = list[rand.Next(list.Length)];
                    decimal randomPrice = rand.Next(1, 10);
                    DateTime randomDate = DateTime.Today.AddDays(rand.Next(0, 5));

                    var existingProduct =  _context.Products.Where(k=>k.ProductName == selectedProduct && k.Date == randomDate).FirstOrDefault();
                    if (existingProduct != null)
                    {
                        if (sayi % 2 == 0)
                        {
                            existingProduct.Price += randomPrice;
                            _context.Update(existingProduct);
                             _context.SaveChanges();
                        }
                        else
                        {
                            existingProduct.Price -= randomPrice;
                            _context.Update(existingProduct);
                             _context.SaveChanges();
                        }


                    }
                    else
                    {
                        Product procuct = new Product();
                        procuct.Price = randomPrice;
                        procuct.Date = randomDate;
                        procuct.ProductName = selectedProduct;
                        procuct.OpenPrice = randomPrice;
                         _context.Add(procuct);
                         _context.SaveChanges();
                    }
                    await _hubContext.Clients.All.SendAsync("GetAllProduct", GetAll());

                    Thread.Sleep(1000);

                }


            }
            catch (Exception ex)
            {
                throw await Task.FromResult(ex);

            }
        }

        public async Task<List<Product>> GetAll()
        {
            try
            {
                List<Product> mr = new List<Product>();
                var date = DateTime.Now.Date;
                var products =  _context.Products.Where(k => k.Date == date).Select(l => new Product
                {
                    Id = l.Id,
                    ProductName = l.ProductName,
                    Price = l.Price,
                    Date = l.Date,
                    OpenPrice = l.OpenPrice,
                }).ToList();

                mr = products;
                return await Task.FromResult(mr);

            }
            catch (Exception ex)
            {

                throw await Task.FromResult(ex);
            }
        }
    }
}
