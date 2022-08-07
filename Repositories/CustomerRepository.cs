﻿using Dapper;
using DependencyStore.Models;
using Microsoft.Data.SqlClient;

namespace DependencyStore.Repositories;

public class CustomerRepository
{
    private readonly SqlConnection _connection;

    public CustomerRepository(SqlConnection connection)
    {
        _connection = connection;
    }

    public async Task<Customer?> GetById(string Id)
    {
        const string query = "SELECT [Id], [Name], [Email] FROM CUSTOMER WHERE ID=@id";
        return await _connection.QueryFirstOrDefaultAsync<Customer>(query, new { id = Id });
    }
}
