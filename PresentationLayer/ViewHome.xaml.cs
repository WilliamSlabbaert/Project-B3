using BusinessLayer;
using DataLayer;
using PresentationLayer.Grids;
using System.Data;
using System.Windows.Controls;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for ViewHome.xaml
    /// </summary>
    public partial class ViewHome : UserControl
    {
        private StripGrid Comicstrips;
        private AuthorGrid Authors;
        private PublisherGrid Publishers;

        public ViewHome()
        {
            InitializeComponent();

            ComicStripManager sm = new ComicStripManager(new UnitOfWork());
            Comicstrips = new StripGrid(StripsGrid, sm.GetAll());
            Comicstrips.SetDeleteButton(Button_DeleteStrips);
            Comicstrips.SetEditButton(Button_EditStrip);

            AuthorManager am = new AuthorManager(new UnitOfWork());
            Authors = new AuthorGrid(AuthorsGrid, am.GetAll());

            PublisherManager pm = new PublisherManager(new UnitOfWork());
            Publishers = new PublisherGrid(PublishersGrid, pm.GetAll());
            Publishers.SetDeleteButton(Button_DeletePublishers);
            Publishers.SetEditButton(Button_EditPublisher);
        }
    }
}
