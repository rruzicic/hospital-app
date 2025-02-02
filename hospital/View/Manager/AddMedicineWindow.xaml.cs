﻿using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace hospital.View
{
    /// <summary>
    /// Interaction logic for AddMedicineWindow.xaml
    /// </summary>
    public partial class AddMedicineWindow : Window
    {
        private readonly MedicineController medicineController;
        private readonly RoomController roomController;
        private readonly List<string> ingridients = new List<string>();
        public AddMedicineWindow()
        {
            DataContext = this;
            InitializeComponent();
            App app = Application.Current as App;
            medicineController = app.medicineController;
            roomController = app.roomController;
            ingridientsField.ItemsSource = app.ingridientsController.FindAll();
            alternativesField.ItemsSource = medicineController.FindAll();
            scheduleBtn.IsEnabled = false;
        }

        private void Add_Medicine_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Validate();
                AddMedicine();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Validate()
        {
            ValidateId();
            ValidateQuantity();
            ValidateIngridients();
        }

        private void ValidateId()
        {
            string id = codeField.Text;
            if (medicineController.FindById(id) != null)
            {
                throw new Exception("Medicine with this id already exists!");
            }
        }

        private void ValidateQuantity()
        {
            int value;
            bool isValid = int.TryParse(quanityField.Text, out value);

            if (!isValid)
            {
                throw new Exception("Quantity should be a number!");
            }

            if (value < 0)
            {
                throw new Exception("Quantity should be positive!");
            }
        }

        private void ValidateIngridients()
        {
            if (GetIngridients().Count == 0)
            {
                throw new Exception("There should be at least one ingredient!");
            }
        }

        private void AddMedicine()
        {
            Medicine medicine = new Medicine(codeField.Text, nameField.Text, GetIngridients(), nameField.Text, int.Parse(quanityField.Text));
            if (!IsMedicineNew(medicine))
            {
                return;
            }

            medicine.Alternatives = GetAlternatives();
            medicineController.Create(medicine);
            AddMedicineAsEquipment();
        }

        private bool IsMedicineNew(Medicine medicine)
        {
            if (medicineController.FindByName(medicine.Name) != null)
            {
                MessageBox.Show("Medicine with this name already exists");
                return false;
            }
            return true;
        }

        private void AddMedicineAsEquipment()
        {
            Equipment equipment = new Equipment(nameField.Text, int.Parse(quanityField.Text));
            roomController.FindRoomByPurpose("warehouse").AddEquipment(equipment);
        }

        private List<Medicine> GetAlternatives()
        {
            List<Medicine> alternatives = new List<Medicine>();
            for (int i = 0; i < alternativesField.SelectedItems.Count; i++)
            {
                alternatives.Add((Medicine)alternativesField.SelectedItems[i]);
            }
            return alternatives;
        }

        private List<string> GetIngridients()
        {
            List<string> ingridients = new List<string>();
            for (int i = 0; i < ingridientsField.SelectedItems.Count; i++)
            {
                ingridients.Add((string)ingridientsField.SelectedItems[i]);
            }
            return ingridients;
        }
        private void Cancel_Medicine_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void IsFormFilled(object sender, TextChangedEventArgs e)
        {
            IsFormFilledValidation();
        }

        private void IsFormFilled(object sender, SelectionChangedEventArgs e)
        {
            IsFormFilledValidation();
        }

        private void IsFormFilledValidation()
        {
            if (nameField.Text != "" && codeField.Text != "" && quanityField.Text != "" && ingridientsField.SelectedItems.Count > 0)
            {
                scheduleBtn.IsEnabled = true;
                return;
            }
            scheduleBtn.IsEnabled = false;
        }

        private void Close_Window(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }
}
