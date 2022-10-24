using Grpc.Core;

namespace gRPC.Services;

public class CustomerService: Customer.CustomerBase
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
            output.FirstName = "Arkida";
            output.LastName = "Kupe";
        }
        else if (request.UserId == 2)
        {
            output.FirstName = "Futura";
            output.LastName = "Solutions";
        }
        else
        {
            output.FirstName = "John";
            output.LastName = "Smith";
        }

        return Task.FromResult(output);
    }
    
    public override async Task GetNewCustomers(NewCustomerRequest request, IServerStreamWriter<CustomerModel> responseStream, ServerCallContext context)

    {
        List<CustomerModel> customers = new List<CustomerModel>
        {
            new CustomerModel
            {
                FirstName = "Test1",
                LastName = "Test12",
                Age = 23,
                EmailAddress = "test1@gmail.com"
            },
            new CustomerModel
            {
                FirstName = "Test2",
                LastName = "Test22",
                Age = 23,
                EmailAddress = "test2@gmail.com"
            },
            new CustomerModel
            {
                FirstName = "Test3",
                LastName = "Test32",
                Age = 23,
                EmailAddress = "test3@gmail.com"
            },
        };
    
        foreach (var cust in customers)
        {
            await responseStream.WriteAsync(cust);
        }
    }
}