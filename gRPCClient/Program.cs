// See https://aka.ms/new-console-template for more information


using gRPC;
using Grpc.Core;
using Grpc.Net.Client;

// var input = new HelloRequest {Name = "Arkida"};
var channel = GrpcChannel.ForAddress("http://localhost:5105");
// var client = new Greeter.GreeterClient(channel);
// var reply = await client.SayHelloAsync(input);
//
// Console.WriteLine(reply.Message);

var clientRequested = new CustomerLookupModel{UserId = 1};
var customerClient = new Customer.CustomerClient(channel);
var customer = await customerClient.GetCustomerInfoAsync(clientRequested);

Console.WriteLine($"{customer.FirstName} {customer.LastName}");

Console.WriteLine();
Console.WriteLine("New CustomerRequest");
Console.WriteLine();

using (var call = customerClient.GetNewCustomers(new NewCustomerRequest()))
{
    while (await call.ResponseStream.MoveNext())
    {
        var currentCustomer = call.ResponseStream.Current;
        
        Console.WriteLine($"{ currentCustomer.FirstName } { currentCustomer.LastName }: { currentCustomer.EmailAddress }");
    }
}

Console.ReadLine();