using Microsoft.Extensions.DependencyInjection;
using Books.Client.Extensions;
using Patrons.Client.Extensions;
using Records.Client.Extensions;

Console.WriteLine("Hello, World!");

var services = new ServiceCollection();
services.AddBooksModule()
    .AddPatronModule()
    .AddBorrowingRecordModule();