// See https://aka.ms/new-console-template for more information
using ElasticSearchDemo;
using Nest;

Console.WriteLine("Hello, World!");

ConnectionSettings setting = new ConnectionSettings(new Uri("http://192.168.10.100:9200/")).DefaultIndex("order");//在ES中索引相当于数据库

ElasticClient client = new ElasticClient(setting);

//ElasticSearchUtilty.InsertObject(client);

ElasticSearchUtilty.Search(client);

ElasticSearchUtilty.Delete(client);

Console.ReadLine();


