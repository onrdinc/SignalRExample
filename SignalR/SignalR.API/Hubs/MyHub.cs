using Microsoft.AspNetCore.SignalR;
using SignalR.API.Services;

namespace SignalR.API.Hubs
{
    public class MyHub : Hub
    {
        private readonly ProductService _service;

        public MyHub(ProductService service)
        {
            _service = service;
        }

        public async Task GetData()
        {
            await Clients.All.SendAsync("GetAllProduct", _service.GetAll());
        }
    }
}
