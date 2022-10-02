using Grpc.Core;

namespace GrpcServer.Services
{
    public class CustomerService : Customer.CustomerBase
    {
        private readonly ILogger<CustomerService> _logger;
        public CustomerService(ILogger<CustomerService> logger)
        {
            _logger = logger;
        }

        public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
        {
            CustomerModel output = new CustomerModel();

            if (request.UserId == 1)
            {
                output.FirstName = "Jamie";
                output.LastName = "Smith";
            }
            else if (request.UserId == 2)
            {
                output.FirstName = "Jane";
                output.LastName = "Doe";
            }
            else if (request.UserId == 3)
            {
                output.FirstName = "Grag";
                output.LastName = "Thomas";
            }
            else if (request.UserId == 4)
            {
                output.FirstName = "Yunus";
                output.LastName = "Altıntop";
            }
            else if (request.UserId == 5)
            {
                output.FirstName = "Fatma";
                output.LastName = "Seda";
            }
            else
            {
                output.FirstName = "Seda";
                output.LastName = "Yunus";
            }
            return Task.FromResult(output);
        }

        public override async Task GetNewCustomers(NewCustomerRequest request, IServerStreamWriter<CustomerModel> responseStream, ServerCallContext context)
        {
            List<CustomerModel> customers = new List<CustomerModel>()
            {
                new CustomerModel
                {
                    FirstName = "Tim",
                    LastName = "Corey",
                    EmailAddress = "tim@iamtimcorey.com",
                    Age = 41,
                    IsAlive = true
                },
                new CustomerModel
                {
                    FirstName = "Seda",
                    LastName = "Özyurt",
                    EmailAddress = "fsedaozyurt@gmail.com",
                    Age = 29,
                    IsAlive = true
                },
                new CustomerModel
                {
                    FirstName = "Yunus",
                    LastName = "Altıntop",
                    EmailAddress = "yns.altintop@gmail.com",
                    Age = 29,
                    IsAlive = true
                }
            };
            foreach (var cust in customers)
            {
                await Task.Delay(2000);
                await responseStream.WriteAsync(cust);
            }
        }
    }
}
