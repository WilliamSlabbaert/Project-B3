using BusinessLayer.DomainManagers;
using DataLayer;
using PresentationLayer.Forms;
using PresentationLayer.Grids;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for ViewOrders.xaml
    /// </summary>
    public partial class ViewOrders : UserControl
    {
        private OrderGrid Orders;

        public ViewOrders()
        {
            InitializeComponent();

            OrderManager om = new OrderManager(new UnitOfWork());
            Orders = new OrderGrid(OrdersGrid, om.GetAll());
            Orders.SetDeleteButton(Button_DeleteOrders);
            Orders.SetEditButton(Button_EditOrder);

            new OrderAddForm(Input_OrderFirstname, Input_OrderLasttname, Input_OrderEmail, Input_OrderPhone, Input_OrderItem, Input_OrderItemQuantity, Input_OrderItems, Button_AddOrderItem, Button_CreateOrder);
        }
    }
}
