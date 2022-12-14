// See https://aka.ms/new-console-template for more information
using Grpc.Core;
using Grpc.Net.Client;
using GrpcServer;
using System.ComponentModel.DataAnnotations;

//var input = new HelloRequest { Name = "Seda" };

//var channel = GrpcChannel.ForAddress("https://localhost:7057");

//var client = new Greeter.GreeterClient(channel);

//var reply = await client.SayHelloAsync(input);

//Console.WriteLine(reply.Message);


var channel = GrpcChannel.ForAddress("https://localhost:7057");
var customerClient = new Customer.CustomerClient(channel);

var clientRequested = new CustomerLookupModel { UserId = 6 };

var customer = await customerClient.GetCustomerInfoAsync(clientRequested);

Console.WriteLine($"{customer.FirstName} {customer.LastName}");

Console.WriteLine();
Console.WriteLine("New Customer List");
Console.WriteLine();

using (var call = customerClient.GetNewCustomers(new NewCustomerRequest()))
{
	while (await call.ResponseStream.MoveNext())
	{
		var currentCustomer = call.ResponseStream.Current;
        Console.WriteLine($"{currentCustomer.FirstName} {currentCustomer.LastName} : {currentCustomer.EmailAddress}");
    }
}


Console.ReadKey();
