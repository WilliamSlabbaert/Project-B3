using BusinessLayer;
using Microsoft.Data.SqlClient;
using System;

namespace DataLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private String connectionString;
        private SqlConnection connection;
        public UnitOfWork(String connect_string = "test")
        {
            setConnectionString(connect_string);
            connection = new SqlConnection(connectionString);

            publisherRepository = new PublisherRepository(connection);
            authorRepository = new AuthorRepository(connection);
            comisStripRepository = new ComicStripRepository(connection);
        }
        public void setConnectionString(String connect_string)
        {
            if (connect_string.ToLower() == "test")
                connectionString = @"Data Source=WILLIAM-SLABBAE\SQLEXPRESS;Initial Catalog=ComicsTest;Integrated Security=True";
            else
                connectionString = "production server";
        }
        public IComicStripRepository comisStripRepository { get; private set; }

        public IPublisherRepository publisherRepository { get; private set; }

        public IAuthorRepository authorRepository { get; private set; }

    }
}
