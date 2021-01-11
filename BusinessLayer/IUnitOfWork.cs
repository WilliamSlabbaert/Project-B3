using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public interface IUnitOfWork 
    {
        IComicStripRepository Comicstrips { get; }
        IComicstripBundleRepository ComicstripBundles { get; }
        IPublisherRepository Publishers { get; }
        IAuthorRepository Authors { get; }
    }
}
