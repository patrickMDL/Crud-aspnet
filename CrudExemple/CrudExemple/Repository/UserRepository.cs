﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using Npgsql;
using CrudExemple.Models;

namespace CrudExemple.Repository
{
    public class UserRepository : IRepository<Users>
    {
        private string connectionString;
        public UserRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetValue<string>("DBInfo:ConnectionString");
        }

        internal IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection(connectionString);
            }
        }

        public void Add(Users item)
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            dbConnection.Execute("INSERT INTO Users (name,email,phone,address,password) VALUES(@Name,@Email,@Phone,@Address,@Password)", item);

        }

        public IEnumerable<Users> FindAll()
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            return dbConnection.Query<Users>("SELECT * FROM users");
        }

        public Users FindByID(int id)
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            return dbConnection.Query<Users>("SELECT * FROM Users WHERE id = @Id", new { Id = id }).FirstOrDefault();
        }

        public void Remove(int id)
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            dbConnection.Execute("DELETE FROM Users WHERE Id=@Id", new { Id = id });
        }

        public void Update(Users item)
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            dbConnection.Query("UPDATE Users SET name = @Name,   email= @Email, phone  = @Phone, address= @Address, password= @Password WHERE id = @Id", item);
        }
    }
}
