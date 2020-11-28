using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public interface IUnitOfWork 
    {
        IComicStripRepository comisStripRepository { get; }
        IPublisherRepository publisherRepository { get; }
        IAuthorRepository authorRepository { get; }
    }
}
