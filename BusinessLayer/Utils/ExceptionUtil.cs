using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Utils
{
    public class ExistException : Exception
    {
        public ExistException(String obj) : base(String.Format("That {0} alreayd exists", obj)) { }
    }

    public class AddException : Exception
    {
        public AddException(String obj) : base(String.Format("Could not add {0}", obj)) { }
    }

    public class UpdateException : Exception
    {
        public UpdateException(String obj) : base(String.Format("Could not update {0}", obj)) { }
    }
}
