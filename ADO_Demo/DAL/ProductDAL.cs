using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using ADO_Demo.Model;
using System.Reflection;

namespace ADO_Demo.DAL
{
    public class ProductDAL
    {
        private string _connectionstring;
        public ProductDAL(IConfiguration configuration)
        {
            _connectionstring = configuration.GetConnectionString("Default");
        }
        public SqlConnection GetConnection()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = _connectionstring;
            return conn;
        }

        public int AddData(ProductModel p)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            int record = 0;
            try
            {
                conn = GetConnection();
                cmd = new SqlCommand("insertentry", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@pname", System.Data.SqlDbType.NVarChar).Value = p.Name;
                cmd.Parameters.Add("@pprice", System.Data.SqlDbType.Float).Value = p.Price;
                cmd.Parameters.Add("@pqty", System.Data.SqlDbType.Int).Value = p.Qty;
                conn.Open();
                record = cmd.ExecuteNonQuery();
                Console.WriteLine("Entry Added.");
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();

            }
            return record;
        }
        public ProductModel Search(int id)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            ProductModel model = null;
            try
            {
                conn = GetConnection();
                conn.Open();
                cmd = new SqlCommand("select * from Product where id=@pid", conn);
                cmd.Parameters.Add("@pid", System.Data.SqlDbType.Int).Value = id;
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        model = new ProductModel();
                        model.ID = Convert.ToInt32(rdr["Id"]);
                        model.Name = rdr["Name"].ToString();
                        model.Price = Convert.ToSingle(rdr["Price"]);
                        model.Qty = Convert.ToInt32(rdr["Qty"]);
                        break;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return model;
        }

        public List<ProductModel> Search(string name)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            List<ProductModel> models = null;
            try
            {
                conn = GetConnection();
                conn.Open();
                cmd = new SqlCommand("Select * from Product where name=@pname", conn);
                cmd.Parameters.AddWithValue("@pname", name);
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    ProductModel model = null;
                    models = new List<ProductModel>();
                    while(rdr.Read())
                    {
                        model = new ProductModel();
                        model.ID = Convert.ToInt32(rdr["Id"]);
                        model.Name = rdr["Name"].ToString();
                        model.Price = Convert.ToSingle(rdr["Price"]);
                        model.Qty = Convert.ToInt32(rdr["Qty"]);
                        models.Add(model);
                    }
                }
            }
            catch(SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn?.Close();
            }
            return models;
        }

        public int Delete(int id)
        {
            int rows = 0;
            SqlConnection conn = null;
            SqlCommand cmd = null;
            ProductModel model = null;
            try
            {
                conn = GetConnection();
                conn.Open();
                cmd = new SqlCommand("delete from Product where id=@pid", conn);
                cmd.Parameters.Add("@pid",System.Data.SqlDbType.Int).Value = id;
                rows = cmd.ExecuteNonQuery();
                
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }
        public int Update(int id)
        {
            int rows = 0;
            SqlConnection conn = null;
            SqlCommand cmd = null;
            ProductModel model = null;
        }
    }
}