using Autofac;
using NetModule2_1;
using NetModule2_1.BAL;
using NetModule2_1.DefaultBusinessLogic;
using NetModule2_1.LiteDb;
using Newtonsoft.Json;

var builder = new ContainerBuilder();
builder.RegisterModule<BusinessModuleLogic>();
builder.RegisterModule<LiteDbModule>();
var container = builder.Build();

DataSetup.Setup();

var cartService = container.Resolve<ICartService>();
var beforeFirst = cartService.GetItemsList("First");
var beforeSecond = cartService.GetItemsList("Second");

Console.WriteLine("Before:");
Console.WriteLine(JsonConvert.SerializeObject(beforeFirst));
Console.WriteLine(JsonConvert.SerializeObject(beforeSecond));

cartService.RemoveItem("First", 1);
cartService.AddItem("First", new Item { Id = 1, Image = null, Name = "Teacup", Price = 5, Quantity = 10 });
cartService.AddItem("Second", new Item { Id = 3, Image = null, Name = "Just dish", Price = 5, Quantity = 3 });

var afterFirst = cartService.GetItemsList("First");
var afterSecond = cartService.GetItemsList("Second");

Console.WriteLine("After:");
Console.WriteLine(JsonConvert.SerializeObject(afterFirst));
Console.WriteLine(JsonConvert.SerializeObject(afterSecond));