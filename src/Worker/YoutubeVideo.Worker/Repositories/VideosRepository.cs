using System.Data;
using System.Data.SQLite;
using YoutubeVideo.Worker.Models;

namespace YoutubeVideo.Worker.Repositories
{
    public class VideosRepository
    {

        private static SQLiteConnection sqliteConnection;

        private static SQLiteConnection DbConnection()
        {
            sqliteConnection = new SQLiteConnection($"Data Source=c:\\temp\\Videos.sqlite; Version=3;");
            sqliteConnection.Open();
            return sqliteConnection;
        }
        public static void CriarBancoSQLite()
        {
            try
            {
                SQLiteConnection.CreateFile(@"c:\\temp\\Videos.sqlite");
                
            }
            catch (Exception ex) 
            {
                throw;
            }
        }
        public static void CriarTabelaSQlite()
        {
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "CREATE TABLE Videos (Id NVARCHAR(255) NOT NULL, Title NVARCHAR(255) NOT NULL, " +
                            "Description NVARCHAR(500) NOT NULL, ChannelTitle NVARCHAR(255) NOT NULL, Author NVARCHAR(255) NOT NULL, Duration NVARCHAR(50) NOT NULL, " +
                            "PublishedAt DATETIME NOT NULL, IsDeleted BIT NOT NULL DEFAULT 0, Link NVARCHAR(255) NOT NULL);";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetVideos()
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Videos";
                    da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    da.Fill(dt);
                    return dt;
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataTable GetVideo(int id)
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Videos Where Id=" + id;
                    da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public static DataTable GetVideoTitle(string title)
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Videos WHERE Title LIKE @Title";
                    cmd.Parameters.AddWithValue("@Title", "%" + title + "%");

                    da = new SQLiteDataAdapter(cmd);
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        public static void Add(Video video)
        {
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Videos (Id, Title, Description, ChannelTitle, Author, Duration, PublishedAt, IsDeleted, Link) " +
                      "VALUES (@Id, @Title, @Description, @ChannelTitle, @Author, @Duration, @PublishedAt, @IsDeleted, @Link);";

                    cmd.Parameters.AddWithValue("@Id", Guid.NewGuid().ToString());
                    cmd.Parameters.AddWithValue("@Title", video.Title);
                    cmd.Parameters.AddWithValue("@Description", video.Description);
                    cmd.Parameters.AddWithValue("@ChannelTitle", video.ChannelTitle);
                    cmd.Parameters.AddWithValue("@Author", video.Author == null ? "-" : video.Author);
                    cmd.Parameters.AddWithValue("@Duration", video.Duration == null ? "-" : video.Duration);
                    cmd.Parameters.AddWithValue("@PublishedAt", video.PublishedAt);
                    cmd.Parameters.AddWithValue("@IsDeleted", video.IsDeleted);
                    cmd.Parameters.AddWithValue("@Link", video.LinkVideo);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void Update(Video video)
        {
            try
            {
                using (var cmd = new SQLiteCommand(DbConnection()))
                {
                    if (video.Id != null)
                    {
                        cmd.CommandText = "UPDATE Videos SET Title=@Title, Description=@Description, ChannelTitle=@ChannelTitle, " +
                                          "Author=@Author, Duration=@Duration, PublishedAt=@PublishedAt, IsDeleted=@IsDeleted, Link=@Link" +
                                          "WHERE Id=@Id";

                        cmd.Parameters.AddWithValue("@Id", video.Id);
                        cmd.Parameters.AddWithValue("@Title", video.Title);
                        cmd.Parameters.AddWithValue("@Description", video.Description);
                        cmd.Parameters.AddWithValue("@ChannelTitle", video.ChannelTitle);
                        cmd.Parameters.AddWithValue("@Author", video.Author);
                        cmd.Parameters.AddWithValue("@Duration", video.Duration);
                        cmd.Parameters.AddWithValue("@PublishedAt", video.PublishedAt);
                        cmd.Parameters.AddWithValue("@IsDeleted", video.IsDeleted);
                        cmd.Parameters.AddWithValue("@Link", video.LinkVideo);

                        cmd.ExecuteNonQuery();
                    }
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void Delete(int Id)
        {
            try
            {
                using (var cmd = new SQLiteCommand(DbConnection()))
                {
                    // Comando para realizar o delete lógico (atualizando a coluna IsDeleted para true)
                    cmd.CommandText = "UPDATE Videos SET IsDeleted = 1 WHERE Id = @Id";

                    // Adicionando o parâmetro Id
                    cmd.Parameters.AddWithValue("@Id", Id);

                    // Executando o comando
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Aqui, é melhor lançar a exceção diretamente ou registrar a exceção para depuração
                throw ex;
            }
        }
    }
}