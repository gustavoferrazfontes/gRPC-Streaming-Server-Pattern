using Ecommerce;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Core.Utils;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OrderManagementClient
{
    class Program
    {
         static void Main(string[] args)
        {
            var channel = new Channel("192.168.0.5", 5001, ChannelCredentials.Insecure);
            var client = new OrderManagementService.OrderManagementServiceClient(channel);

            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));

            var value = new StringValue { Value = "OrderId" };
            using var streamCall = client.searchOrders(value);

            try
            {
                 streamCall.ResponseStream.ForEachAsync( item  =>
                     Task.Run(() => Console.WriteLine($"Id:{item.Id} | Price: {item.Price} | Description: {item.Description} | ProductCount:{item.Items.Count}")));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            Console.ReadKey();

        }
    }
}
