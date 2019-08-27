﻿using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace FiiiPay.Enterprise.Data
{
    public abstract class BaseDataAccess
    {
        private static readonly string _fiiipayConStr = ConfigurationManager.ConnectionStrings["fiiienterprise"].ConnectionString;
        private static readonly string _fiiiRates = ConfigurationManager.ConnectionStrings["fiiiRates"].ConnectionString;

        /// <summary>
        /// Gets the size of the page.
        /// </summary>
        /// <value>
        /// The size of the page.
        /// </value>
        protected int PageSize => Convert.ToInt32(ConfigurationManager.AppSettings.Get("PageSize") ?? "10");

        /// <summary>
        /// Reads the connection to FiiiRates.
        /// </summary>
        /// <returns></returns>
        public virtual IDbConnection ReadConnectionToFiiiRates()
        {
            var sqlConnection = new SqlConnection(_fiiiRates);
            sqlConnection.Open();

            return sqlConnection;
        }

        /// <summary>
        /// Reads the connection.
        /// </summary>
        /// <returns></returns>
        public virtual IDbConnection ReadConnection()
        {
            var sqlConnection = new SqlConnection(_fiiipayConStr);
            sqlConnection.Open();

            return sqlConnection;
        }

        /// <summary>
        /// Writes the connection.
        /// </summary>
        /// <returns></returns>
        public virtual IDbConnection WriteConnection()
        {
            var sqlConnection = new SqlConnection(_fiiipayConStr);
            sqlConnection.Open();

            return sqlConnection;
        }

        /// <summary>
        /// Reads the connection.
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IDbConnection> ReadConnectionAsync()
        {
            var sqlConnection = new SqlConnection(_fiiipayConStr);
            await sqlConnection.OpenAsync();

            return sqlConnection;
        }

        /// <summary>
        /// Writes the connection.
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IDbConnection> WriteConnectionAsync()
        {
            var sqlConnection = new SqlConnection(_fiiipayConStr);
            await sqlConnection.OpenAsync();

            return sqlConnection;
        }
    }
}
