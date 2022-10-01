﻿using Controller;
using hospital.Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace hospital.View
{
    /// <summary>
    /// Interaction logic for EditMedicineWindow.xaml
    /// </summary>
    public partial class EditMedicineWindow : Window
    {
        private readonly MedicineController medicineController;
        private readonly IngridientsController ingridientsController;

        public EditMedicineWindow()
        {
            InitializeComponent();
            App app = Application.Current as App;
            medicineController = app.medicineController;
            ingridientsController = app.ingridientsController;
            codeField.IsEnabled = false;
        }

        public void FillForm()
        {
            Medicine medicine = DataContext as Medicine;
            if (medicine == null)
            {
                return;
            }

            alternativesField.ItemsSource = medicineController.FindAll();
            ingridientsField.ItemsSource = ingridientsController.FindAll();
            SetAllSelectedIngridients(medicine.Ingridients);
            SetAllSelectedAlternatives(medicine.Alternatives);
        }

        private void SetAllSelectedIngridients(List<string> igridients)
        {
            ingridientsField.SelectedItems.Clear();
            foreach (string ingridient in igridients)
            {
                ingridientsField.SelectedItems.Add(ingridient);

            }
        }

        private void SetAllSelectedAlternatives(List<Medicine> alternatives)
        {
            alternativesField.SelectedItems.Clear();
            foreach (Medicine alternative in alternatives)
            {
                alternativesField.SelectedItems.Add(GetMedicineOriginalReference(alternative));
            }
        }

        private Medicine GetMedicineOriginalReference(Medicine medicine)
        {
            ObservableCollection<Medicine> medicines = medicineController.FindAll();
            foreach (Medicine med in medicines)
            {
                if (med.Id == medicine.Id)
                {
                    return med;
                }
            }
            return null;
        }

        private void Cancel_Medicine_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Close_Window(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }

        private void Edit_Medicine_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Validate();
                EditMedicine();
                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void EditMedicine()
        {
            Medicine newMedicine = new Medicine(codeField.Text, nameField.Text, GetIngridients(), nameField.Text,
                int.Parse(quanityField.Text))
            {
                Alternatives = GetAlternatives()
            };
            medicineController.UpdateById(codeField.Text, newMedicine);
        }

        private void Validate()
        {
            CheckIfEditable();
            ValidateQuantity();
            ValidateIngridients();
        }

        private void CheckIfEditable()
        {
            if (medicineController.FindById(codeField.Text).Status.Equals("approved"))
            {
                throw new Exception("Editing an approved medicine is not allowed!");
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

        private List<string> GetIngridients()
        {
            List<string> ingridients = new List<string>();
            for (int i = 0; i < ingridientsField.SelectedItems.Count; i++)
            {
                ingridients.Add((string)ingridientsField.SelectedItems[i]);
            }

            return ingridients;
        }

        private List<Medicine> GetAlternatives()
        {
            List<Medicine> alternatives = new List<Medicine>();
            for (int i = 0; i < alternativesField.SelectedItems.Count; i++)
            {
                alternatives.Add((Medicine)alternativesField.SelectedItems[i]);
                Console.WriteLine(alternatives.Count);
            }
            return alternatives;
        }

        private void FormFilled(object sender, TextChangedEventArgs e)
        {
            FormFilled();
        }

        private void FormFilled(object sender, SelectionChangedEventArgs e)
        {
            FormFilled();
        }

        private void FormFilled()
        {
            if (nameField.Text != "" && ingridientsField.SelectedItems.Count != 0 || quanityField.Text != "")
            {
                confirmBtn.IsEnabled = true;
                return;
            }
            confirmBtn.IsEnabled = false;
        }
    }
}
