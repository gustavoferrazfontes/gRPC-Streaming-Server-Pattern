using Ecommerce;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Server
{
    public class OrderManagementServiceImpl : OrderManagementService.OrderManagementServiceBase
    {
        public async  override Task searchOrders(
            StringValue request, 
            IServerStreamWriter<Order> responseStream, 
            ServerCallContext context)
        {

            var i = 0;
            while (!context.CancellationToken.IsCancellationRequested && i < 10)
            {
                await Task.Delay(500);

                var result = new Order
                {
                    Description = $"order item {i}",
                    Id = i.ToString(),
                    Price = new Random(10).Next() * i,
                    Destination = request.Value
                };
                for (int x = 0; x < i; x++)
                {
                    result.Items.Add("Product 1");
                } 

                i++;
                await responseStream.WriteAsync(result);

            }

        }
    }
}
