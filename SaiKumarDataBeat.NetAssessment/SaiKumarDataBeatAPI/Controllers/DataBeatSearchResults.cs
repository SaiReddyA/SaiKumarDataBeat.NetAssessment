using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SaiKumarDataBeatAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Net.Http;
using System.Text.Json;
using SaiKumarDataBeatAPI.Models;

namespace SaiKumarDataBeatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataBeatSearchResults : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private DataBeatresults _dataBeatresults;
       // private SqlConnection _sqlConnection;

        public DataBeatSearchResults(IConfiguration configuration, DataBeatresults dataBeatresults)
        {
            _configuration = configuration;
            _dataBeatresults = dataBeatresults;
           // _sqlConnection = new SqlConnection(_configuration.GetConnectionString("connection"));
        }

        [HttpGet]
        [Route("GetDataBeatApiResults")]
        public async Task<ActionResult> Insertdata()
        
        {
            
            HttpClient client = new HttpClient();
            string apiUrl = "https://api.plos.org/search?q=title:DNA";
            HttpResponseMessage response = await client.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                // Read the content of the HTTP response
                string responseData = await response.Content.ReadAsStringAsync();
                Jsondata data = JsonSerializer.Deserialize<Jsondata>(responseData);
              
                Articals results = JsonSerializer.Deserialize<Articals>(responseData);
                int identity = 1;  //_dataBeatresults.InsetSearchresult(_sqlConnection, data.ViewResponce);
                foreach (var item in data.ViewResponce.docs)
                {
                    item.SearchResult_ID = identity;
                    _dataBeatresults.InsetArticles(item);
                }
                return Ok(data);
            }
            else
            {
                // Handle API request failure
                return BadRequest("API request failed.");
            }
           // return Ok(response);

        }






    }
}
