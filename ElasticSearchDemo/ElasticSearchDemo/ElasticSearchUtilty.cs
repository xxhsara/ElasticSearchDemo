using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearchDemo
{
    public class ElasticSearchUtilty
    {
        //在kibana中查询 GET /order/_doc/_search
        public static void InsertObject(ElasticClient client)
        {
            var newInfo = new Order
            {
                Id = "001",
                Name = "张三"
            };

            IndexResponse response = client.IndexDocument(newInfo);
            if (response.IsValid)
            {
                Console.WriteLine("对象添加成功");
            }


            //批量新增
            var infoList = new List<Order>
            {
                new Order
                {
                     Id="002",
                     Name="李四"
                },
                 new Order
                {
                     Id="003",
                     Name="王五"
                },
                  new Order
                {
                     Id="004",
                     Name="刘二"
                },
                   new Order
                {
                     Id="005",
                     Name="赵四"
                },
                   new Order
                {
                     Id="006",
                     Name="李丽"
                },
            };

            BulkResponse response1 = client.IndexMany(infoList);

            if (response1.IsValid)
            {
                Console.WriteLine("批量新增成功");
            }
        }


        public static void Search(ElasticClient client)
        {
            var searchAllResponse = client.Search<Order>();
            IReadOnlyCollection<Order> allorderList = searchAllResponse.Documents;
            foreach (var item in allorderList)
            {
                Console.WriteLine($"id:{item.Id},name:{item.Name}");
            }

            Console.WriteLine("限定条件查询");

            var searchResponse = client.Search<Order>(s=>s.From(0).Size(100)
            .Query(q=>q.Match(m=>m.Field(f=>f.Name).Query("李丽"))));
            IReadOnlyCollection<Order> orderList = searchResponse.Documents;
            foreach (var item in orderList)
            {
                Console.WriteLine($"id:{item.Id},name:{item.Name}");
            }
        }

        public static void Update(ElasticClient client)
        {

        }

        public static void Delete(ElasticClient client)
        {
            client.Delete(new DocumentPath<Order>("001"), s =>
            {
                DeleteRequest request = new DeleteRequest("order", "001");
                return request;
            });
            Console.WriteLine("删除成功");

            //删除之后并不能马上生效，需要等待时间
            var searchAllResponse = client.Search<Order>();
            IReadOnlyCollection<Order> allorderList = searchAllResponse.Documents;
            foreach (var item in allorderList)
            {
                Console.WriteLine($"id:{item.Id},name:{item.Name}");
            }
        }
    }
}
