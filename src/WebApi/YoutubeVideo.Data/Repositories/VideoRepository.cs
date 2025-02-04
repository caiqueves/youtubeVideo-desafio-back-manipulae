using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YoutubeVideo.Domain.Entities;
using YoutubeVideo.Domain.Interfaces;

namespace YoutubeVideo.Data.Repositories
{
    public class VideoRepository : IVideoRepository
    {
        private readonly string _connectionString = "Data Source=c:\\temp\\\\Videos.sqlite;";

        public async Task<IQueryable<Video>> GetAllAsync()
        {
            var videos = new List<Video>();
            using (var connection = new SqliteConnection(_connectionString))
        {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Videos WHERE IsDeleted = 0";
                           
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        videos.Add(new Video
                        {
                            Id = reader.GetString(0),
                            Title = reader.GetString(1),
                            Description = reader.GetString(2),
                            ChannelTitle = reader.GetString(3),
                            Author = reader.GetString(4),
                            Duration = reader.GetString(5),
                            PublishedAt = reader.GetDateTime(6),
                            Link = reader.GetString(7),
                            IsDeleted = reader.GetBoolean(8)
                        });
        }
                }
            }
            return videos.AsQueryable();
        }

        public async Task<Video> GetByIdAsync(string id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Videos WHERE Id = @id AND IsDeleted = 0";
                command.Parameters.AddWithValue("@id", id);

                using (var reader = await command.ExecuteReaderAsync())
        {
                    if (await reader.ReadAsync())
                    {
                        return new Video
                        {
                            Id = reader.GetString(0),
                            Title = reader.GetString(1),
                            Description = reader.GetString(2),
                            ChannelTitle = reader.GetString(3),
                            Author = reader.GetString(4),
                            Duration = reader.GetString(5),
                            PublishedAt = reader.GetDateTime(6),
                            Link = reader.GetString(7),
                            IsDeleted = reader.GetBoolean(8)
                        };
                    }
                }
            }
            return null;
        }

        public async Task AddAsync(Video video)
        {
            using (var connection = new SqliteConnection(_connectionString))
        {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Videos (Id, Title, Description, ChannelTitle, Author, Duration, PublishedAt, Link, IsDeleted) VALUES (@id, @title, @desc, @channel, @author, @duration, @published, @link, @deleted)";
                command.Parameters.AddWithValue("@id", Guid.NewGuid().ToString());
                command.Parameters.AddWithValue("@title", video.Title);
                command.Parameters.AddWithValue("@desc", video.Description);
                command.Parameters.AddWithValue("@channel", video.ChannelTitle);
                command.Parameters.AddWithValue("@author", video.Author);
                command.Parameters.AddWithValue("@duration", video.Duration);
                command.Parameters.AddWithValue("@published", video.PublishedAt);
                command.Parameters.AddWithValue("@link", video.Link);
                command.Parameters.AddWithValue("@deleted", video.IsDeleted);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task UpdateAsync(Video video)
        {
            using (var connection = new SqliteConnection(_connectionString))
        {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = "UPDATE Videos SET Title = @title, Description = @desc, ChannelTitle = @channel, Author = @author, Duration = @duration, PublishedAt = @published, Link = @link, IsDeleted = @deleted WHERE Id = @id";
                command.Parameters.AddWithValue("@id", video.Id);
                command.Parameters.AddWithValue("@title", video.Title);
                command.Parameters.AddWithValue("@desc", video.Description);
                command.Parameters.AddWithValue("@channel", video.ChannelTitle);
                command.Parameters.AddWithValue("@author", video.Author);
                command.Parameters.AddWithValue("@duration", video.Duration);
                command.Parameters.AddWithValue("@published", video.PublishedAt);
                command.Parameters.AddWithValue("@link", video.Link);
                command.Parameters.AddWithValue("@deleted", video.IsDeleted);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteAsync(string id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = "UPDATE Videos SET IsDeleted = 1 WHERE Id = @id";
                command.Parameters.AddWithValue("@id", id);
                await command.ExecuteNonQueryAsync();
            }
        }

    }
}