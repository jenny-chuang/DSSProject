using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaoyuanActivity.Models
{
    public class CodeService
    {
        /// <summary>
        /// 取得DB連線字串
        /// </summary>
        private string GetDBConnectionString()
        {
            return
                System.Configuration.ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString.ToString();
        }

        /// <summary>
        /// 年齡下拉選單
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetAgeData()
        {
            DataTable dt = new DataTable();
            string sql = @" SELECT CODE_ID, CODE_NAME
                            FROM CODE_TABLE
                            WHERE CODE_TYPE = 'AGE'";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);

                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapCodeData(dt);
        }

        public List<SelectListItem> GetCmdActTypeData(string Age_Id)
        {
            DataTable dt = new DataTable();
            string sql = @" EXEC dbo.sp_ACT_PREFER @AgeId , @TYPE ";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@AgeId", Age_Id));
                cmd.Parameters.Add(new SqlParameter("@TYPE", '1'));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);

                sqlAdapter.Fill(dt);
                conn.Close();
            }
             return MapCodeData(dt);
        //    List<SelectListItem> result = new List<SelectListItem>();
        //    foreach (DataRow row in dt.Rows)
        //    {
        //        result.Add(new SelectListItem()
        //        {
        //            Text = row["CODE_NAME"].ToString(),
        //            Value = row["CODE_ID"].ToString()
        //        });
        //    }
        //return result;
        }

        public List<SelectListItem> GetOthActTypeData(string Age_Id)
        {
            DataTable dt = new DataTable();
            string sql = @" EXEC dbo.sp_ACT_PREFER @AgeId , @Type ";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@AgeId", Age_Id));
                cmd.Parameters.Add(new SqlParameter("@Type", '2'));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);

                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return MapCodeData(dt);
            //List<SelectListItem> result = new List<SelectListItem>();
            //foreach (DataRow row in dt.Rows)
            //{
            //    //result.Add(row(0));
            //    result.Add(new SelectListItem()
            //    {
            //        Text = row["CODE_NAME"].ToString(),
            //        Value = row["CODE_ID"].ToString()
            //    });
            //}
            //return result;
        }

        public List<SelectListItem> GetAllActTypeData()
        {
            DataTable dt = new DataTable();
            string sql = @" EXEC dbo.sp_ACT_PREFER @AgeId , @Type ";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@AgeId", string.Empty));
                cmd.Parameters.Add(new SqlParameter("@Type", '2'));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);

                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return MapCodeData(dt);
        }


        public List<Models.Activity> GetTypeActivity(string Act_Type_Id)
        {
            DataTable dt = new DataTable();
            string sql = @" SELECT ROW_NUMBER() OVER(PARTITION BY [TYPE_ID] ORDER BY ID) AS Seq, ACT_NAME, ID, B.CODE_NAME AS ACT_TYPE_NAME
                            FROM ALL_ACT A
	                            INNER JOIN CODE_TABLE B
		                            ON A.TYPE_ID = B.CODE_ID 
                            WHERE [TYPE_ID] = @Act_Type_Id";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@Act_Type_Id", Act_Type_Id));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);

                sqlAdapter.Fill(dt);
                conn.Close();
            }

            return this.MapActkDataToList(dt);
        }

        private List<Models.Activity> MapActkDataToList(DataTable ActData)
        {
            List<Models.Activity> result = new List<Models.Activity>();
            foreach (DataRow row in ActData.Rows)
            {
                result.Add(new Activity()
                {
                    Value = row["Seq"].ToString(),
                    Text = row["ACT_NAME"].ToString(),
                    Id = row["ID"].ToString(),
                    ActType = row ["ACT_TYPE_NAME"].ToString()

                });
            }
            return result;
        }

        /// <summary>
        /// Maping 代碼資料
        /// </summary>
        /// <param name="dt"></param>
        /// <returns>result</returns>
        private List<SelectListItem> MapCodeData(DataTable dt)
        {
            List<SelectListItem> result = new List<SelectListItem>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new SelectListItem()
                {
                    Text = row["CODE_NAME"].ToString(),
                    Value = row["CODE_ID"].ToString()
                });
            }
            return result;
        }
    }
}