using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using SaiKumarDataBeatAPI.Models;
using Microsoft.Extensions.Configuration;

namespace SaiKumarDataBeatAPI.Repository
{

    public interface DataBeatresults
    {
        public int InsetArticles(Articals articals);
        public int InsetSearchresult(ViewResponce searchresult);
    }
    public class DatabaseService : DataBeatresults
    {

        private readonly IConfiguration _configuration;
    
        public DatabaseService(IConfiguration configuration)
        {
            _configuration = configuration;
        
        }
        public  int InsetArticles( Articals articals)
        {
            SqlConnection _sqlConnection = new SqlConnection(_configuration.GetConnectionString("connection"));
            int lastinsertedidentity;
            if(_sqlConnection.State== System.Data.ConnectionState.Closed) { _sqlConnection.Open(); }
            using (_sqlConnection)
            {
                    string insertSql = "INSERT INTO Articles(Articaldata_id, journal, eissn, publication_date, article_type, author_display, abstract_data, title_display, score, SearchResult_ID) " + " " +
                                       " OUTPUT INSERTED.Id " +" " +
                                       " VALUES (@Articaldata_id, @journal, @eissn, @publication_date, @article_type, @author_display, @abstract_data, @title_display, @score, @SearchResult_ID)";

                    using (SqlCommand command = new SqlCommand(insertSql, _sqlConnection))
                    {
                        command.Parameters.AddWithValue("@Articaldata_id", articals.Id);
                        command.Parameters.AddWithValue("@journal", articals.journal);
                        command.Parameters.AddWithValue("@eissn", articals.eissn);
                        command.Parameters.AddWithValue("@publication_date", articals.publication_date);
                        command.Parameters.AddWithValue("@article_type", articals.article_type);
                        command.Parameters.AddWithValue("@author_display", string.Join(",", articals.author_display));
                        command.Parameters.AddWithValue("@abstract_data", string.Join(",", articals.abstract_data));
                        command.Parameters.AddWithValue("@title_display", articals.title_display);
                        command.Parameters.AddWithValue("@score", articals.score);
                        command.Parameters.AddWithValue("@SearchResult_ID", articals.SearchResult_ID);

                    lastinsertedidentity= Convert.ToInt32(command.ExecuteScalar());
                    }
            }

            if (_sqlConnection.State == System.Data.ConnectionState.Open) { _sqlConnection.Close(); }
            return lastinsertedidentity;
        }
        public int InsetSearchresult(ViewResponce searchresult)
        {
            SqlConnection _sqlConnection = new SqlConnection(_configuration.GetConnectionString("connection"));
            int lastinsertedidentity;
            if (_sqlConnection.State == System.Data.ConnectionState.Closed) { _sqlConnection.Open(); }
            using (_sqlConnection)
            {
               string insertSql = "INSERT INTO SearchResult(numFound, start, maxScore)  " + " "+
                                   " OUTPUT INSERTED.Id " + " " +
                                   "VALUES (@numFound, @start, @maxScore)";

                using (SqlCommand command = new SqlCommand(insertSql, _sqlConnection))
                {
                    command.Parameters.AddWithValue("@numFound", searchresult.numFound);
                    command.Parameters.AddWithValue("@start", searchresult.start);
                    command.Parameters.AddWithValue("@maxScore", searchresult.maxScore);
                    lastinsertedidentity = Convert.ToInt32(command.ExecuteScalar());
                }

            }
            if (_sqlConnection.State == System.Data.ConnectionState.Open) { _sqlConnection.Close(); }
            return lastinsertedidentity;
        }
    }
}
