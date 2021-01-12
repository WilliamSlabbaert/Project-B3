using BusinessLayer.DomainManagers;
using DataLayer;
using PresentationLayer.Forms;
using PresentationLayer.Grids;
using System.Windows.Controls;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for ViewDeliveries.xaml
    /// </summary>
    public partial class ViewDeliveries : UserControl
    {
        private DeliveryGrid Deliveries;

        public ViewDeliveries()
        {
            InitializeComponent();

            DeliveryManager dm = new DeliveryManager(new UnitOfWork());
            Deliveries = new DeliveryGrid(DeliveriesGrid, dm.GetAll());
            Deliveries.SetDeleteButton(Button_DeleteDeliveries);
            Deliveries.SetEditButton(Button_EditDelivery);

            new DeliveryAddForm(Input_DeliverySupplier, Input_DeliveryItem, Input_DeliveryItemQuantity, Input_DeliveryItems, Button_AddItem, Button_CreateDelivery);
        }
    }
}
