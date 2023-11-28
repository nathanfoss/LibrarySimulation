using Microsoft.Extensions.DependencyInjection;
using Books.Client.Extensions;

Console.WriteLine("Hello, World!");

var services = new ServiceCollection();
services.AddBooksModule();