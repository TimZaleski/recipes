using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using recipes.Models;

namespace recipes.Repositories
{
    public class IngredientRepository
    {

        private SqlConnection con;
        private void connection()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(connectionString);

        }
        //To Add Ingredient
        public bool AddIngredient(Ingredient obj)
        {
            connection();
            SqlCommand com = new SqlCommand("AddNewIngredient", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@RecipeId", obj.recipeId);
            com.Parameters.AddWithValue("@Name", obj.Name);
            com.Parameters.AddWithValue("@Quantity", obj.Quantity);

            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }


        }
        //To view ingredients     
        public List<Ingredient> GetAllIngredients()
        {
            connection();
            List<Ingredient> IngredientList = new List<Ingredient>();


            SqlCommand com = new SqlCommand("GetIngredients", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {
                IngredientList.Add(
                    new Ingredient
                    {
                        id = new Guid(dr["id"].ToString()),
                        recipeId = new Guid(dr["recipeId"].ToString()),
                        Name = Convert.ToString(dr["name"]),
                        Quantity = Convert.ToString(dr["quantity"])
                    }
                    );
            }

            return IngredientList;
        }
        //To Update Ingredient 
        public bool UpdateIngredient(Ingredient obj)
        {
            connection();
            SqlCommand com = new SqlCommand("UpdateIngredient", con);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Id", obj.id);
            com.Parameters.AddWithValue("@Name", obj.Name);
            com.Parameters.AddWithValue("@Quantity", obj.Quantity);
            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //To delete Ingredient    
        public bool DeleteIngredient(Guid Id)
        {

            connection();
            SqlCommand com = new SqlCommand("DeleteIngredientById", con);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Id", Id);

            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
