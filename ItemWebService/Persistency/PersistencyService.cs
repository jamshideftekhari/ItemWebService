using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ItemLibrary;

namespace ItemWebService.Persistency
{
    public class PersistencyService
    {
        private const string Get_All = "select * From Items";
       // private const string ConnectionString = @"Data Source=DESKTOP-EP0VCU9;Initial Catalog = Items;Integrated Security=TRUE;";
        private const string ConnectionString = @"Server=tcp:jaefserver.database.windows.net,1433;Initial Catalog=HotelDataBaseF2018;
                                                   Persist Security Info=False;User ID=jaef;Password=JAM2003eft;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public IEnumerable<Item> Get()
        {
            List<Item> items = new List<Item>();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = Get_All;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Item item = new Item();
                                item.Id = reader.GetInt32(0);
                                item.Name = reader.GetString(1);
                                item.Quality = reader.GetString(2);
                                item.Quantity = reader.GetDouble(3);
                                items.Add(item);
                            }
                        }
                    }
                }
            }
            return items;
        }

        public Item Get(int Id)
        {
            Item item = new Item();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "select * from Items where Id = @Param";
                        cmd.Parameters.AddWithValue("@param", Id);
                        //cmd.ExecuteNonQuery();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                
                                item.Id = reader.GetInt32(0);
                                item.Name = reader.GetString(1);
                                item.Quality = reader.GetString(2);
                                item.Quantity = reader.GetDouble(3);
                                
                            }
                        }
                    }
                }
            }

            return item;

        }
        public void PostItems(Item item)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "insert into Items values (@Param1,@Param2,@Param3)";
                        cmd.Parameters.AddWithValue("@param1", item.Name);
                        cmd.Parameters.AddWithValue("@Param2", item.Quality);
                        cmd.Parameters.AddWithValue("@Param3", item.Quantity);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public void PutItems( int id,Item item)
        {
            Item oldItem = Get(id);

            if (oldItem != null)
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    if (conn.State == ConnectionState.Open)
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = "UPDATE Items SET Name=@Param1, Quality=@Param2, Quantity=@Param3 WHERE Id=@id";
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.Parameters.AddWithValue("@param1", item.Name);
                            cmd.Parameters.AddWithValue("@Param2", item.Quality);
                            cmd.Parameters.AddWithValue("@Param3", item.Quantity);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }

            }
        }
    }
}
