using Dashboard.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using System.IO;
using Newtonsoft.Json;
using Dashboard.DataDB;
using System.Configuration.Assemblies;
using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data;

namespace Dashboard.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            
        }

        public  IActionResult Index()
        {
            //string file = "D:jsondata.json";
            //  string file = System.IO.File.ReadAllText(@"D:\jsondata.json");
            // JsonData data = JsonConvert.DeserializeObject<JsonData>(file);
            //  List<Root> roots= new List<Root>();
            //  var data = JsonConvert.DeserializeObject<List<Root>>(file);
            // string s = System.Configuration.ConfigurationManager

            // PostData();
            GetData();
            return View();
        }

        [HttpPost]
        public void PostData()
        {
            SqlConnection con = new SqlConnection("Server=LAPTOP-I3LC4FIG;Database=TestDB;Integrated Security=True;TrustServerCertificate=True;");

            string file = System.IO.File.ReadAllText(@"D:\jsondata.json");
            var data = JsonConvert.DeserializeObject<List<Root>>(file);
            int? i = 0;
            
            try
            {
                foreach (var r in data)
                {
                    // string query = "INSERT INTO JsonData(end_year, intensity, sector, topic, insight, url, region, start_year, impact, added, publicshed, country, relevance, pestle, source, title, likelihood) values ('" + r.end_year + "','" + r.intensity + "', '" + r.sector + "', '" + r.topic + "', '" + r.insight + "', '" + r.url + "', '" + r.region + "', '" + r.start_year + "', '" + r.impact + "', '" + r.added + "', '" + r.published + "', '" + r.country + "', '" + r.relevance + "', '" + r.pestle + "', '" + r.source + "', '" + r.title + "', '" + r.likelihood + "')";
                    string query = "INSERT INTO JsonData(end_year, intensity, sector, topic, insight, url, region, start_year, impact, added, publicshed, country, relevance, pestle, source, title, likelihood) values (@end_year, @intensity, @sector, @topic, @insight, @url, @region, @start_year, @impact, @added, @published, @country, @relevance, @pestle, @source, @title, @likelihood)";

                    i = r.intensity;
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@end_year", r.end_year);
                    if (r.intensity == null)
                    {
                        cmd.Parameters.AddWithValue("@intensity", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@intensity", r.intensity);
                    }

                    cmd.Parameters.AddWithValue("@sector", r.sector);
                    cmd.Parameters.AddWithValue("@topic", r.topic);
                    cmd.Parameters.AddWithValue("@insight", r.insight);
                    cmd.Parameters.AddWithValue("@url", r.url);
                    cmd.Parameters.AddWithValue("@region", r.region);
                    cmd.Parameters.AddWithValue("@start_year", r.start_year);
                    cmd.Parameters.AddWithValue("@impact", r.impact);
                    cmd.Parameters.AddWithValue("@added", r.added);
                    cmd.Parameters.AddWithValue("@published", r.published);
                    cmd.Parameters.AddWithValue("@country", r.country);
                    if (r.relevance == null)
                    {
                        cmd.Parameters.AddWithValue("@relevance", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@relevance", r.relevance);
                    }
                    cmd.Parameters.AddWithValue("@pestle", r.pestle);
                    cmd.Parameters.AddWithValue("@source", r.source);
                    cmd.Parameters.AddWithValue("@title", r.title);
                    if (r.likelihood == null)
                    {
                        cmd.Parameters.AddWithValue("@likelihood", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@likelihood", r.likelihood);
                    }
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch(Exception e)
            {
               // System.Diagnostics.Debug.WriteLine("caught exception");
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
        }

        public void GetData()
        {
            List<Root> list = new List<Root>();
            SqlConnection con = new SqlConnection("Server=LAPTOP-I3LC4FIG;Database=TestDB;Integrated Security=True;TrustServerCertificate=True;");
            string query = "SELECT * FROM JsonData";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter sdr = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sdr.Fill(dt);
            int i = 0;
            foreach(DataRow dr in dt.Rows)
            {
                Root rt = new Root();
                rt.title = dr["title"].ToString();
                rt.source = dr["source"].ToString();
                rt.country = dr["country"].ToString();
                rt.published = dr["publicshed"].ToString();
                rt.added = dr["added"].ToString();
                rt.start_year = dr["start_year"].ToString();
                rt.end_year = dr["end_year"].ToString();
                rt.topic = dr["topic"].ToString();
                rt.url = dr["url"].ToString();
                rt.sector = dr["sector"].ToString();
                rt.insight = dr["insight"].ToString();
                rt.impact = dr["impact"].ToString();
                rt.pestle = dr["pestle"].ToString();
                if (dr["intensity"] is DBNull)
                {
                    rt.intensity = 0;
                }
                else
                {
                    rt.intensity = Convert.ToInt32(dr["intensity"]);
                }
                rt.relevance = dr["relevance"] is DBNull ? 0 : Convert.ToInt32(dr["relevance"]);
              //  rt.relevance = Convert.ToInt32(dr["relevance"]);
                rt.likelihood = dr["likelihood"] is DBNull ? 0 : Convert.ToInt32(dr["likelihood"]);
               // rt.likelihood = Convert.ToInt32(dr["likelihood"]);

                list.Add(rt);
                System.Diagnostics.Debug.WriteLine("Added value from sector " + list[i++].sector);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}