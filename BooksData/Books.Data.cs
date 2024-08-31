using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Data
{
    public class BooksData
    {
        private readonly string ConnectionString;
        public BooksData()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
        }

        public DataTable GetGenres()
        {
            try
            {
                var connection = new SqlConnection(ConnectionString);
                var command = new SqlCommand("spGetGenres", connection);
                command.CommandType = CommandType.StoredProcedure;

                var da = new SqlDataAdapter(command);
                var ds = new DataSet();
                da.Fill(ds);

                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error to get genres: {ex.Message}");
            }
        }
        public int CreateBook(
            string bookTitle,
            string bookSynopsis,
            string bookImage,
            string bookIsbn,
            double bookPrice,
            List<int> SelectedGenresIds,
            bool bookStatus,
            DateTime bookCreatedAt,
            DateTime bookLastUpdated)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    var command = new SqlCommand("spCreateBook", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter() { ParameterName = "book_id", DbType = DbType.Int32, Value = 2 });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "book_name", DbType = DbType.String, Value = bookTitle });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "book_synopsis", DbType = DbType.String, Value = bookSynopsis });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "book_image", DbType = DbType.String, Value = bookImage });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "book_isbn", DbType = DbType.String, Value = bookIsbn });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "book_price", DbType = DbType.Double, Value = bookPrice });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "book_status", DbType = DbType.Boolean, Value = bookStatus });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "book_created_at", DbType = DbType.DateTime, Value = bookCreatedAt });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "book_last_updated", DbType = DbType.DateTime, Value = bookLastUpdated });
                    command.ExecuteNonQuery();

                    SqlCommand IDToInsertCommand = connection.CreateCommand();
                    IDToInsertCommand.CommandText = "SELECT MAX(book_id) FROM books";
                    object result = IDToInsertCommand.ExecuteScalar();
                    int IDToInsert = Convert.ToInt32(result);

                    for (int i = 0; i < SelectedGenresIds.Count; i++)
                    {
                        var command2 = new SqlCommand("spCreateBookHasGenre", connection);
                        command2.CommandType = CommandType.StoredProcedure;
                        command2.Parameters.Add(new SqlParameter() { ParameterName = "book_id", DbType = DbType.Int32, Value = IDToInsert });
                        command2.Parameters.Add(new SqlParameter() { ParameterName = "genre_id", DbType = DbType.Int32, Value = SelectedGenresIds[i] });
                        command2.ExecuteNonQuery();
                    }

                    connection.Close();
                    return IDToInsert;

                }
                var script = $"alert('The book {bookTitle} was added successfully')";

            }
            catch (Exception ex)
            {
                var script = $"alert('The book {bookTitle} was added successfully')";
                throw new ApplicationException($"Error to add book, id: : {ex.Message}");
            }
        }

        public void UpdateBook(
            int bookId,
            string bookTitle,
            string bookSynopsis,
            string bookImage,
            string bookIsbn,
            double bookPrice,
            List<string> SelectedGenresNames,
            List<int> SelectedGenresIds,
            bool bookStatus,
            DateTime bookLastUpdated)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    var command = new SqlCommand("spUpdateBook", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter() { ParameterName = "book_id", DbType = DbType.Int32, Value = bookId });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "book_name", DbType = DbType.String, Value = bookTitle });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "book_synopsis", DbType = DbType.String, Value = bookSynopsis });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "book_image", DbType = DbType.String, Value = bookImage });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "book_isbn", DbType = DbType.String, Value = bookIsbn });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "book_price", DbType = DbType.Double, Value = bookPrice });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "book_status", DbType = DbType.Boolean, Value = bookStatus });
                    command.Parameters.Add(new SqlParameter() { ParameterName = "book_last_updated", DbType = DbType.DateTime, Value = bookLastUpdated });
                    command.ExecuteNonQuery();

                    var command3 = new SqlCommand("spResetBookHasGenre", connection);
                    command3.CommandType = CommandType.StoredProcedure;
                    command3.Parameters.Add(new SqlParameter() { ParameterName = "book_id", DbType = DbType.Int32, Value = bookId });
                    command3.ExecuteNonQuery();

                    for (int i = 0; i < SelectedGenresIds.Count; i++)
                    {
                        var command2 = new SqlCommand("spCreateBookHasGenre", connection);
                        command2.CommandType = CommandType.StoredProcedure;
                        command2.Parameters.Add(new SqlParameter() { ParameterName = "book_id", DbType = DbType.Int32, Value = bookId });
                        command2.Parameters.Add(new SqlParameter() { ParameterName = "genre_id", DbType = DbType.Int32, Value = SelectedGenresIds[i] });
                        command2.ExecuteNonQuery();
                    }

                    connection.Close();

                }
                var script = $"alert('The book {bookTitle} was updated successfully')";
            }
            catch (Exception ex)
            {
                var script = $"alert('The book {bookTitle} was updated successfully')";
                throw new ApplicationException($"Error to add book, id: : {ex.Message}");
            }
        }

        public DataTable GetBooks()
        {
            try
            {
                var connection = new SqlConnection(ConnectionString);
                var command = new SqlCommand("spGetBooks", connection);
                command.CommandType = CommandType.StoredProcedure;

                var da = new SqlDataAdapter(command);
                var ds = new DataSet();
                da.Fill(ds);

                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error to get books: {ex.Message}");
            }
        }
        public DataTable GetSelectedBookCategories(int bookId)
        {
            try
            {
                var connection = new SqlConnection(ConnectionString);
                var command = new SqlCommand("spGetSelectedBookCategories", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter() { ParameterName = "book_id", DbType = DbType.Int32, Value = bookId });

                var da = new SqlDataAdapter(command);
                var ds = new DataSet();
                da.Fill(ds);

                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error to get genres: {ex.Message}");
            }
        }
    }
}
