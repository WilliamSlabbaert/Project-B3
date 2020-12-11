using System;

namespace DataLayer.Utils
{
    public class UnknownException : Exception
    {
        public UnknownException() : base(String.Format("There was an unknown issue")) { }
    }
    public class QueryException : Exception
    {
        public QueryException() : base(String.Format("There was an issue while querying")) { }
    }
    public class InsertException : Exception
    {
        public InsertException() : base(String.Format("There was an issue while inserting")) { }
    }
}