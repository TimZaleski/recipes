using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using recipes.Models;

namespace recipes.Repositories
{
    public class RecipeRepository
    {

        private SqlConnection con;
        private void connection()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(connectionString);

        }
        //To Add Recipe
        public bool AddRecipe(Recipe obj)
        {
            connection();
            SqlCommand com = new SqlCommand("AddNewRecipe", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Name", obj.Name);
            com.Parameters.AddWithValue("@City", obj.Description);

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
        //To view recipe     
        public List<Recipe> GetAllRecipes()
        {
            connection();
            List<Recipe> RecipeList = new List<Recipe>();


            SqlCommand com = new SqlCommand("GetRecipes", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);
            con.Close();  
            foreach (DataRow dr in dt.Rows)
            {
                RecipeList.Add(
                    new Recipe
                    {
                        id = new Guid(dr["id"].ToString()),
                        Name = Convert.ToString(dr["name"]),
                        Description = Convert.ToString(dr["description"])
                    }
                    );
            }

            return RecipeList;
        }
        //To Update Recipe 
        public bool UpdateRecipe(Recipe obj)
        {
            connection();
            SqlCommand com = new SqlCommand("UpdateRecipe", con);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Id", obj.id);
            com.Parameters.AddWithValue("@Name", obj.Name);
            com.Parameters.AddWithValue("@Description", obj.Description);
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
        //To delete Recipe    
        public bool DeleteRecipe(Guid Id)
        {

            connection();
            SqlCommand com = new SqlCommand("DeleteRecipeById", con);

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
