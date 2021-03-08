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
        public IngredientRepository(IDbConnection db)
        {
            con = (SqlConnection)db;
        }
        private SqlConnection con;
        //To Add Ingredient
        public bool AddIngredient(Ingredient obj)
        {
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

        //To view a recipe's ingredients     
        public List<Ingredient> GetIngredientsByRecipeId(Guid recipeId)
        {
            List<Ingredient> IngredientList = new List<Ingredient>();


            SqlCommand com = new SqlCommand("GetIngredientsByRecipeId", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@RecipeId", recipeId);
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
