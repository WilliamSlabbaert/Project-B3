using BusinessLayer;
using DataLayer.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;

namespace DataLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private String connectionString;
        private SqlConnection connection;

        public UnitOfWork(String environment = "development")
        {
            setConnectionString(environment);
            connection = new SqlConnection(connectionString);

            Comicstrips = new ComicStripRepository(connection);
            ComicstripBundles = new ComicstripBundleRepository(connection);
            Publishers = new PublisherRepository(connection);
            Authors = new AuthorRepository(connection);
        }

        public void setConnectionString(String environment)
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false);
            var configuration = builder.Build();
            if (environment.ToLower() == "development")
                connectionString = configuration.GetConnectionString("Development").ToString();
            else
                connectionString = configuration.GetConnectionString("Production").ToString();
        }

        public IComicStripRepository Comicstrips { get; private set; }

        public IComicstripBundleRepository ComicstripBundles { get; private set; }

        public IPublisherRepository Publishers { get; private set; }

        public IAuthorRepository Authors { get; private set; }

    }
}
