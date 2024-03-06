using APIAssignmet.Data;
using APIAssignmet.Model;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection.PortableExecutable;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace APIAssignmet.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DapperDBContext context;

        public CategoryRepository(DapperDBContext dapperDBContext)
        {
            this.context = dapperDBContext;

        }

        public IEnumerable<dynamic> Get()
        {
           
                SqlCommand cmd;
                SqlDataReader reader;
                string connetionString = "Server=ATISL402; Database=MyData; user Id=UserD; password=12345;Encrypt=false;MultipleActiveResultSets=True";
                string sql = "select distinct AP_L1Category, count(*) as [Count] from exported_data group by AP_L1Category order by AP_L1Category";
                string sql2 = "  select AP_L1Category,  STRING_AGG( CAST(AP_L2Category as nvarchar(MAX)), ', ') as CommonCategory from (select DISTINCT AP_L1Category, AP_L2Category from exported_data ) as distinctvalue group by AP_L1Category;";
                SqlConnection cnn = new SqlConnection(connetionString);
                cnn.Open();
                cmd = new SqlCommand(sql2, cnn);
                reader = cmd.ExecuteReader();
                var dict = new Dictionary<string, string>();
                while (reader.Read())
                {
                    string column = reader.GetString(0);
                    string value = reader.GetString(1);

                    dict.Add(column, value);
                }

                foreach (string key in dict.Keys)
                {
                    Console.WriteLine(key + "=" + dict[key]);
                }




            //var dict1 = new Dictionary<string, dynamic>();
            //foreach (string key in dict1.Keys)
            //{
            //    string query = "select distinct(AP_L2Category) as l from exported_data where AP_L1Category='" + key +"'";
            //    cmd = new SqlCommand(query, cnn);
            //    reader= cmd.ExecuteReader();
            //    List<string> l2 = new List<string>();
            //    while (reader.Read())
            //    {
            //        l2.Add(reader.GetString(0));

            //        //List<string> list1 = DeserializeList(serializeList);

            //        //dict.Add(column, value);
            //    }
            //    dict1.Add(key,l2);
            //}

            //foreach (string key in dict1.Keys)
            //{
            //    Console.WriteLine(key + "=" + dict1[key]);
            //}

            return cnn.Query(sql);
        }
    

        public IEnumerable<dynamic> GetAll(int type, decimal GreaterThan, decimal LessThan)
        {
            string query = "  select distinct AP_L1Category, count(*) as count FROM exported_data group by AP_L1Category ";
            string query1 = "  select distinct AP_L1Category FROM exported_data";
            string query2 = "  select count(*) FROM exported_data group by AP_L1Category ";
            //var query = "select AP_L1Category , AP_L2Category , AP_L3Category , AP_L4Category  FROM exported_data ";
            //string query = "SELECT d.PO_BASE_SPEND,d.AP_L1Category,d.AP_L2Category,d.AP_L3Category,d.AP_L4Category,e.Location FROM exported_data as d JOIN Location_Info as e ON d.PURCHASING_LOCATION_ID = e.Id";


            //string query1 = "SELECT d.AP_L1Category,d.AP_L2Category,d.AP_L3Category,d.AP_L4Category, e.Country, count(d.PO_BASE_SPEND) as SPENDCOUNT, sum(d.PO_BASE_SPEND) as SPENDSUM FROM exported_data as d JOIN Location_Info as e ON d.PURCHASING_LOCATION_ID = e.Id where d.PO_BASE_SPEND between '"+ GreaterThan + "' and '"+ LessThan + "' group by d.AP_L1Category, d.AP_L2Category, d.AP_L3Category, d.AP_L4Category, e.Country";
            //string query2 = "SELECT d.AP_L1Category,d.AP_L2Category,d.AP_L3Category, e.Country, count(d.PO_BASE_SPEND) as SPENDCOUNT, sum(d.PO_BASE_SPEND) as SPENDSUM FROM exported_data as d JOIN Location_Info as e ON d.PURCHASING_LOCATION_ID = e.Id where d.PO_BASE_SPEND between '" + GreaterThan + "' and '" + LessThan + "' group by d.AP_L1Category, d.AP_L2Category, d.AP_L3Category, e.Country";
            //string query3 = "SELECT d.AP_L1Category,d.AP_L2Category, e.Country, count(d.PO_BASE_SPEND) as SPENDCOUNT, sum(d.PO_BASE_SPEND) as SPENDSUM FROM exported_data as d JOIN Location_Info as e ON d.PURCHASING_LOCATION_ID = e.Id where d.PO_BASE_SPEND between '" + GreaterThan + "' and '" + LessThan + "' group by d.AP_L1Category, d.AP_L2Category,  e.Country";
            //string query4 = "SELECT d.AP_L1Category, e.Country, count(d.PO_BASE_SPEND) as SPENDCOUNT, sum(d.PO_BASE_SPEND) as SPENDSUM FROM exported_data as d JOIN Location_Info as e ON d.PURCHASING_LOCATION_ID = e.Id where d.PO_BASE_SPEND between '" + GreaterThan + "' and '" + LessThan + "' group by d.AP_L1Category, e.Country";


            using (var connection = this.context.CreateConnection())
            {





                //switch(type)
                //{
                //    case 1:
                //        var catlist1 = connection.Query(query1);
                //        return catlist1.ToList();
                //        break;
                //    case 2:
                //        var catlist2 = connection.Query(query2);
                //        return catlist2.ToList();
                //        break;
                //    case 3:
                //        var catlist3 = connection.Query(query3);
                //        return catlist3.ToList();
                //        break;
                //    case 4:
                //        var catlist4 = connection.Query(query4);
                //        return catlist4.ToList();
                //        break;
                //}

                //Dictionary<string, int> dt2 = new Dictionary<string, int>();
                //var catlist1 = connection.Query(query).ToList();
                //var catlist2 = connection.Query(query).ToList();

                //Console.WriteLine(catlist1.ToString);
                //foreach( var cat in catlist1)
                //{
                //    Console.WriteLine(cat);
                //}





                var catlist = connection.Query(query).ToList();


               

              

                 


                    Dictionary<int, dynamic> dt = new Dictionary<int, dynamic>();


                    int i = 0;
                    foreach (var cat in catlist)
                    {
                        dt.Add(i, cat);
                        i++;
                        //Console.WriteLine(cat);

                    }


                    foreach (var cat in dt)
                    {
                        Console.WriteLine(cat);
                    }


                    //return catlist.ToList();

                    return catlist;

                }
            }
        }
}