﻿using Controller;
using Model;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace hospital.View.UserControls
{
    /// <summary>
    /// Interaction logic for OrderEquipmentUserControl.xaml
    /// </summary>
    public partial class OrderEquipmentUserControl : UserControl
    {
        private readonly OrderController _orderController;
        private Notifier Notifier { get; set; }
        public OrderEquipmentUserControl()
        {
            InitializeComponent();
            App app = Application.Current as App;
            _orderController = app.orderController;
            Notifier = GetNotifier();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (isValidate())
            {
                Equipment newEquipment = new Equipment(txtType.Text, int.Parse(txtQuantity.Text));
                Notifier.ShowSuccess("Order successfully");
                _orderController.Create(new Order(DateTime.Now, newEquipment));
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
            txtQuantity.Text = "";
            txtType.Text = "";
            errQuantity.Text = "";
            errType.Text = "";
        }

        private void time_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.Source == txtType)
            {
                errType.Text = "";
            }

            if (e.Source == txtQuantity)
            {
                if (!txtQuantity.Text.Contains("-"))
                {
                    errQuantity.Text = "";
                }
                else
                {
                    errQuantity.Text = "Quantity can't be negative";
                }

                if (txtQuantity.Text.Any(char.IsLetter))
                {
                    errQuantity.Text = "Must be number";
                }
            }
        }

        private bool isValidate()
        {
            bool[] isCorrected = new bool[2];

            for (int i = 0; i < 2; i++)
            {
                isCorrected[i] = true;
            }

            if (txtType.Text.Trim().Equals(""))
            {
                errType.Text = "Must be filled";
                isCorrected[0] = false;
            }
            else
            {
                errType.Text = "";
                isCorrected[0] = true;
            }

            if (txtQuantity.Text.Trim().Equals(""))
            {
                errQuantity.Text = "Must be filled";
                isCorrected[1] = false;
            }
            else if (txtQuantity.Text.Contains("-"))
            {
                errQuantity.Text = "Quantity can't be negative";
                isCorrected[1] = false;
            }
            else if (txtQuantity.Text.Any(char.IsLetter))
            {
                errQuantity.Text = "Must be number";
                isCorrected[1] = false;
            }
            else
            {
                errQuantity.Text = "";
                isCorrected[1] = true;
            }
            return (isCorrected[0] && isCorrected[1]);
        }

        private Notifier GetNotifier()
        {
            return new Notifier(cfg =>
            {
                cfg.PositionProvider = new WindowPositionProvider(
                    parentWindow: Application.Current.MainWindow,
                    corner: Corner.TopRight,
                    offsetX: 10,
                    offsetY: 10);

                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    notificationLifetime: TimeSpan.FromSeconds(3),
                    maximumNotificationCount: MaximumNotificationCount.FromCount(5));

                cfg.Dispatcher = Application.Current.Dispatcher;
            });
        }
    }
}
