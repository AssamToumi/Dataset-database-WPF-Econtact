using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Econtact
{
    class contactClass
    {
        //Getter Setter Properties
        //Acts as Data Carrier in our application
        public int ContactID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }

        static string myconnstrng=ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
    
        //Selecting Data from Database 
    public DataTable Select()
    {
        //Step 1: Database Connection 
        SqlConnection conn = new SqlConnection(myconnstrng);
        DataTable dt = new DataTable();
        try
        {
                //Step 2: Writing SQL Query 
            string sql = "SELECT * FROM tbl_contact";
                //Creating cmd using sql and conn
            SqlCommand cmd = new SqlCommand(sql, conn);
                //Creating SQL DataAdapter using cmd
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            conn.Open();
            adapter.Fill(dt);
        }
        catch (Exception)
            {
           
        }
        finally
        {
            conn.Close();
        }
        return dt;
    }
        //Inserting Datainto Database 
    public bool Insert(contactClass c)
    {
            //Creating a default return type and setting its value to false 
        bool isSuccess = false;
            //Step 1: Connect Database 
        SqlConnection conn = new SqlConnection(myconnstrng);
            try
        {
            //Step 2: Create a SQL Query to insert Data 
            string sql = "INSERT INTO tbl_contact (FirstName, LastName, ContactNo, Address, Gender) VALUES (@FirstName, @LastName, @ContactNo, @Address, @Gender)";
            //Creating SQL Command using sql and conn 
                SqlCommand cmd = new SqlCommand(sql,conn);
                //Create Parameters to add data 
            cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
            cmd.Parameters.AddWithValue("@LastName", c.LastName);
            cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
            cmd.Parameters.AddWithValue("@Address", c.Address);
            cmd.Parameters.AddWithValue("@Gender", c.Gender);
                //Connection Open here 
            conn.Open();
            int rows = cmd.ExecuteNonQuery();
                //If the query runs successfully then the value of rows will be greater than zero else its value will be 0
            if (rows>0)
            {
                isSuccess = true;
            }
            else
            {
                isSuccess = false;
            }
        }
        catch (Exception)
            {

        }
        finally
        {
            conn.Close();
        }
        return isSuccess;
        }
        //Method to update data in database from our application 
public bool Update(contactClass c)
{
            //Create a default return type and set its default value to false 
    bool isSuccess = false;
        SqlConnection conn = new SqlConnection(myconnstrng);
    try
    {
                //SQL to update data in our Database
            string sql = "UPDATE tbl_contact SET FirstName=@FirstName, LastName=@LastName, ContactNo=@ContactNo, Address=@Address, Gender=@Gender WHRER Contact=@ContactID";
           
                //Creating SQL Command 
            SqlCommand cmd = new SqlCommand(sql, conn);
                //Create Parameters to add value 
            cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
            cmd.Parameters.AddWithValue("@LastName", c.LastName);
            cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
            cmd.Parameters.AddWithValue("@Adress", c.Address);
            cmd.Parameters.AddWithValue("@Gender", c.Gender);
            cmd.Parameters.AddWithValue("@ContactID", c.ContactID);
            //Open Database Connection 
            conn.Open();
            int rows = cmd.ExecuteNonQuery();
                //if the query runs succfully then the value of rows will be 0
            if (rows>0)
            {
                isSuccess = true;
            }
            else
            {
                isSuccess = false;
            }
        }
    catch (Exception)
            {

    }
    finally
    {
            conn.Close();
    }
        return isSuccess;
    }
        //Method to Delete Data from Database
 public bool Delete(contactClass c)
    {
            //Create a default return value and set its value to false 
        bool isSuccess = false;
            //Create SQL Connection
        SqlConnection conn = new SqlConnection(myconnstrng);
        try
        {
                //SQL To Delete Data 
            string sql = "DELETE FROM tbl_contact WHERE ContactID=@ContactID";
           //Creating SQL Command
                SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@ContactID", c.ContactID);
            //Open Connection
                conn.Open();
            int rows = cmd.ExecuteNonQuery();
                //if the query run secessfully then the value of rows is greater than 0 else its value is 0
            if (rows>0)
            {
                isSuccess = true;
            }
            else
            {
                isSuccess = false;
            }
        }
        catch (Exception)
            {
          
        }
        finally
        {
//Close Connection 
            conn.Close();
        }
            return isSuccess;
    }
    }
}