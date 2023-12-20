// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Books.Client.Extensions;
using Patrons.Client.Extensions;
using Records.Client.Extensions;

Console.WriteLine("Hello, World!");

var services = new ServiceCollection();
services.AddBooksModule()
    .AddPatronModule()
    .AddBorrowingRecordModule();