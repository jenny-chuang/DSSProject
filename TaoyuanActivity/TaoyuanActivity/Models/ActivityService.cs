using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaoyuanActivity.Models
{
    public class ActivityService
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
        /// 撈取活動場地資料
        /// </summary>
        /// <returns></returns>
        public List<Models.Activity> GetSiteData(string Ping_Id, string In_Out_Id)
        {
            DataTable dt = new DataTable();
            string sql = @" IF PARSE(@PingId AS INT) = 50
                            BEGIN
	                            SELECT A.PUB_PRI, A.NAME + '-' + B.D_NAME AS NAME, A.ADDRESS, B.PHONE, B.D_TYPE, B.PING
	                            FROM ACT_SITE A
		                            INNER JOIN ACT_SITE_D B
			                            ON A.ID = B.SITE_ID
	                            WHERE (B.PING <= 50 OR B.PING IS NULL) AND B.IN_OUT = @InOutId
                            END
                            ELSE IF PARSE(@PingId AS INT) = 100
                            BEGIN
	                            SELECT A.PUB_PRI, A.NAME + '-' + B.D_NAME AS NAME, A.ADDRESS, B.PHONE, B.D_TYPE, B.PING
	                            FROM ACT_SITE A
		                            INNER JOIN ACT_SITE_D B
			                            ON A.ID = B.SITE_ID
	                            WHERE ((B.PING BETWEEN 51 AND 100) OR B.PING IS NULL) AND B.IN_OUT = @InOutId
                            END
                            ELSE IF PARSE(@PingId AS INT) = 500
                            BEGIN
	                            SELECT A.PUB_PRI, A.NAME + '-' + B.D_NAME AS NAME, A.ADDRESS, B.PHONE, B.D_TYPE, B.PING
	                            FROM ACT_SITE A
		                            INNER JOIN ACT_SITE_D B
			                            ON A.ID = B.SITE_ID
	                            WHERE ((B.PING BETWEEN 101 AND 500) OR B.PING IS NULL) AND B.IN_OUT = @InOutId
                            END
                            ELSE IF PARSE(@PingId AS INT) = 501
                            BEGIN
	                            SELECT A.PUB_PRI, A.NAME + '-' + B.D_NAME AS NAME, A.ADDRESS, B.PHONE, B.D_TYPE, B.PING
	                            FROM ACT_SITE A
		                            INNER JOIN ACT_SITE_D B
			                            ON A.ID = B.SITE_ID
	                            WHERE (B.PING >= 501 OR B.PING IS NULL) AND B.IN_OUT = @InOutId
                            END";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@PingId", Ping_Id));
                cmd.Parameters.Add(new SqlParameter("@InOutId", In_Out_Id));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);

                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapCodeData(dt);
        }

        /// <summary>
        /// Maping 代碼資料
        /// </summary>
        /// <param name="dt"></param>
        /// <returns>result</returns>
        private List<Models.Activity> MapCodeData(DataTable dt)
        {
            List<Models.Activity> result = new List<Models.Activity>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new Activity()
                {
                    SitePubPri = row["PUB_PRI"].ToString(),
                    SiteName = row["NAME"].ToString(),
                    SiteAddressName = row["ADDRESS"].ToString(),
                    SitePhone = row["PHONE"].ToString(),
                    SiteDType = row["D_TYPE"].ToString(),
                    SitePing = row["PING"].ToString(),
                });
            }
            return result;
        }

        public List<Sum> GetActDataById(string actId, string weight)
        {
            //TaoyuanActivity.Models.Activity results = null;
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                string sql = @" SELECT ACT_ID, RES_COM, LOC_FEA, LOC_ECO, CUL_COM, SECURITY, UNAFF_WEATHER, COST, B.ACT_NAME, C.CODE_NAME
                                FROM VALUE_FUNC A 
	                                INNER JOIN ALL_ACT B ON A.ACT_ID = B.ID
	                                INNER JOIN CODE_TABLE C ON B.TYPE_ID = C.CODE_ID
                                WHERE ACT_ID IN (" + actId + ")";
                string sql2 = @" SELECT 
		                                A.x.value('(/n)[1]', 'varchar(16)') AS RES_COM,
		                                A.x.value('(/n)[2]', 'varchar(16)') AS LOC_FEA,
		                                A.x.value('(/n)[3]', 'varchar(16)') AS LOC_ECO,
		                                A.x.value('(/n)[4]', 'varchar(16)') AS CUL_COM,
		                                A.x.value('(/n)[5]', 'varchar(16)') AS [SECURITY],
		                                A.x.value('(/n)[6]', 'varchar(16)') AS UNAFF_WEATHER,
		                                A.x.value('(/n)[7]', 'varchar(16)') AS COST
                                FROM 
                                (
	                                select convert(xml, '<n>' + replace(aaa, ',','</n><n>') + '</n>' ) as x 
	                                FROM (select '" + weight + "' as aaa) C )A";
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                SqlCommand cmd2 = new SqlCommand(sql2, conn);
                SqlDataAdapter sqlAdapter2 = new SqlDataAdapter(cmd2);
                sqlAdapter.Fill(dt);
                sqlAdapter2.Fill(dt2);
                conn.Close();
            }
            var count = new List<Sum>();
            var results = new List<Sum>();
            
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                float sum = 0;
                for (var j = 1; j < 8; j++)
                {

                    float act = float.Parse(dt.Rows[i][j].ToString());
                    float wgt = float.Parse(dt2.Rows[0][j-1].ToString());
                    sum += act * wgt;

                }
                count.Add(new Sum() {
                    Id = dt.Rows[i][0].ToString(),
                    SumNumber = sum,
                    ActName = dt.Rows[i][8].ToString(),
                    ActType = dt.Rows[i][9].ToString()
                });
            }
            results = count.OrderByDescending(item => item.SumNumber).ToList();
            int a = 1;
            foreach(var item in results)
            {
                item.Id = a.ToString();
                a++;
            }
            return results;
        }
    


            
        
    }
}