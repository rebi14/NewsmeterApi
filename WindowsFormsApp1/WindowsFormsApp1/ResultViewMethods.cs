using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.Net;
using System.IO;

namespace WindowsFormsApp1
{
    public class ResultViewMethods
    {
        DataBaseConnection Connection = new DataBaseConnection();

        public bool InsertNews(ResultView resultView)
        {
            try
            {

                using (SqlConnection con = Connection._Connection)
                {
                    con.Open();

                    foreach (var item in resultView.Results)
                    {

                        string saveResult = @"INSERT into News(Country,DomainName,GroupId,ImageUrl,IsPopularWebsite,Lang,NewsId,OnHomePage,PublishDate,RetweetCount,Spot,Title,Topics,Url)
                         VALUES (@Country,@DomainName,@GroupId,@ImageUrl,@IsPopularWebsite,@Lang,@NewsId,@OnHomePage,@PublishDate,@RetweetCount,@Spot,@Title,@Topics,@Url)";


                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = con;

                        cmd.Parameters.Add("@Country", SqlDbType.NVarChar).Value = item.Country;
                        cmd.Parameters.Add("@DomainName", SqlDbType.NVarChar).Value = item.DomainName;
                        cmd.Parameters.Add("@GroupId", SqlDbType.NVarChar).Value = item.GroupId;
                        cmd.Parameters.Add("@ImageUrl", SqlDbType.NVarChar).Value = item.ImageUrl;
                        cmd.Parameters.Add("@IsPopularWebsite", SqlDbType.Bit).Value = item.IsPopularWebsite;
                        cmd.Parameters.Add("@Lang", SqlDbType.NVarChar).Value = item.Lang;
                        cmd.Parameters.Add("@NewsId", SqlDbType.NVarChar).Value = item.NewsId;
                        cmd.Parameters.Add("@OnHomePage", SqlDbType.Bit).Value = item.OnHomePage;
                        cmd.Parameters.Add("@PublishDate", SqlDbType.VarChar).Value = item.PublishDate;
                        cmd.Parameters.Add("@RetweetCount", SqlDbType.Int).Value = item.RetweetCount;
                        cmd.Parameters.Add("@Spot", SqlDbType.NVarChar).Value = item.Spot;
                        cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = item.Title;
                        cmd.Parameters.Add("@Topics", SqlDbType.NVarChar).Value = item.Topics;
                        cmd.Parameters.Add("@Url", SqlDbType.NVarChar).Value = item.Url;
                        cmd.CommandText = saveResult;

                        int rowCount = cmd.ExecuteNonQuery();
                    }

                    con.Close();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return false;
        }

        public void insertByEfx(ResultView resultView)
        {
            using (var db = new NewsEntities4())
            {
                foreach (var item in resultView.Results)
                {
                   
                 db.News.Add(item);
               
                }
                db.SaveChanges();

            }





        }
        public static ResultView Post(string searchAll, string searchAny, string searchExclude, string start, string end, decimal maxResult, long MaxId, long MinId, string lang)
        {

            try
            {


                String url = String.Format("https://api.newsmeter.com/api/news?searchAll={0}&searchAny={1}%&searchExclude={2}%&startDate={3}&endDate={4}&maxResult={5}&maxNewsId={6}&minNewsId={7}&lang={8}", searchAll, searchAny, searchExclude, start, end, maxResult, MaxId, MinId, lang);

                //string url = "https://api.newsmeter.com/api/news?1=1";

                //StringBuilder sb = new StringBuilder(url);
                //if (!String.IsNullOrWhiteSpace(searchAll))
                //{
                //    sb.Append("&searchAll=" + searchAll);
                //}

                //if (MaxId > 0)
                //{
                //    sb.Append("&maxNewsId=" + MaxId);
                //}
                //url = sb.ToString();

                ResultView res = new ResultView();
                WebRequest req = WebRequest.Create(url);
                req.Method = "GET";
                req.Headers.Add("token", "445271f4-b117-4e2a-a3a3-7effdd2c475e");
                req.ContentType = "application/json; charset=utf-8";
                WebResponse resp = req.GetResponse();
                Stream stream = resp.GetResponseStream();
                StreamReader re = new StreamReader(stream);
                string json = re.ReadToEnd();
                res = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultView>(json);
                //var dd= Newtonsoft.Json.JsonConvert.SerializeObject(list);//yukardaki işlemin tam tersi
                return res;
            }
            catch (WebException e)
            {

                throw e;


            }


        }
    }
}
