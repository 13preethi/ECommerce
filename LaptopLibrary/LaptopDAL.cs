using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace LaptopLibrary
{
  public  class LaptopDAL
    {
        public bool AddLaptop(Laptop laptop)
        {
            bool operationStatus = false;
            string str = ConfigurationManager.ConnectionStrings["LaptopConnectionString"].ConnectionString;
            SqlConnection cn = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand("[dbo].sp_InsertProduct", cn);
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@p_Brand", laptop.Brand);
                cmd.Parameters.AddWithValue("@p_Processor",laptop.Processor);
                cmd.Parameters.AddWithValue("@p_OperatingSystem", laptop.Operating_System);
                cmd.Parameters.AddWithValue("@p_Price", laptop.Price);

                cn.Open();
                cmd.ExecuteNonQuery();
                operationStatus = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                cn.Close();
                cn.Dispose();

            }
            return operationStatus;

        }
        public bool EditLaptop(Laptop laptop, int Id)
        {
            bool operationStatus = false;
            string str = ConfigurationManager.ConnectionStrings["LaptopConnectionString"].ConnectionString;
            SqlConnection cn = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand("[dbo].[sp_UpdateProduct]", cn);
            try
            {

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@p_Id", Id);
                cmd.Parameters.AddWithValue("@p_Brand", laptop.Brand);
                cmd.Parameters.AddWithValue("@p_Processor", laptop.Processor);
                cmd.Parameters.AddWithValue("@p_OperatingSystem", laptop.Operating_System);
                cmd.Parameters.AddWithValue("@p_Price", laptop.Price);

                cn.Open();
                cmd.ExecuteNonQuery();
                operationStatus = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                cn.Close();
                cn.Dispose();

            }
            return operationStatus;
        }
        public bool RemoveLaptop(int Id)
        {
            bool operationStatus = false;
            string str = ConfigurationManager.ConnectionStrings["LaptopConnectionString"].ConnectionString;
            SqlConnection cn = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand("delete  from laptop where Id= " + Id, cn);
            cn.Open();
            cmd.ExecuteNonQuery();

            operationStatus = true;
            cn.Close();
            cn.Dispose();

            return operationStatus;

        }
        public List<Laptop> GetLaptopList()
        {
            List<Laptop> list = new List<Laptop>();
            string str = ConfigurationManager.ConnectionStrings["LaptopConnectionString"].ConnectionString;
            SqlConnection cn = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand("select * from laptop", cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Laptop laptop = new Laptop();
               laptop.Id = Convert.ToInt32(dr["Id"]);
               laptop.Brand = dr["Brand"].ToString();
                laptop.Processor = dr["Processor"].ToString();
                laptop.Operating_System = dr["Operating_System"].ToString();
                laptop.Price= Convert.ToDouble(dr["Price"]);

                list.Add(laptop);


            }
            cn.Close();
            cn.Dispose();


            return list;
        }
        public Laptop FindLaptop(int Id)
        {

            Laptop laptop = new Laptop();
            string str = ConfigurationManager.ConnectionStrings["LaptopConnectionString"].ConnectionString;
            SqlConnection cn = new SqlConnection(str);

            SqlCommand cmd = new SqlCommand("select * from Laptop where Id= " + Id, cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();

                laptop.Id = Convert.ToInt32(dr["Id"]);
                laptop.Brand = dr["Brand"].ToString();
                laptop.Processor = dr["Processor"].ToString();
                laptop.Operating_System = dr["Operating_System"].ToString();
                laptop.Price = Convert.ToDouble(dr["Price"]);
            }
            cn.Close();
            cn.Dispose();
            return laptop;
        }
    }
}
