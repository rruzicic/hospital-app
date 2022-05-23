﻿using Controller;
using hospital.Controller;
using hospital.Model;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace hospital.View.Manager
{
    /// <summary>
    /// Interaction logic for DoctorReviewWindow.xaml
    /// </summary>
    public partial class DoctorReviewWindow : Window
    {
        private DoctorController doctorController;
        private PollController pollController;
        public DoctorReviewWindow()
        {
            InitializeComponent();
            App app = Application.Current as App;
            pollController = app.pollBlueprintController;
            doctorController = app.doctorController;
            doctorsComboBox.ItemsSource = doctorController.GetDoctors();
        }

        private void Doctor_Changed(object sender, SelectionChangedEventArgs e)
        {
            FillFinalGrade();
            FillCategories();
            FillFirstCategoryQuestion();
            FillSecondCategoryQuestion();
            FillThirdCategoryQuestion();
        }

        private void FillFinalGrade() {
            finalAverage.Content = pollController.CalculateDoctorFinalGrade(((Doctor)doctorsComboBox.SelectedItem).Username);
        }
        private void FillCategories() {
            category1.Content = pollController.GetDoctorPollBlueprint().Categories[0].Name + pollController.CalculateCategoryGrade(((Doctor)doctorsComboBox.SelectedItem).Username, pollController.GetDoctorPollBlueprint().Categories[0].Id);
            category2.Content = pollController.GetDoctorPollBlueprint().Categories[1].Name + pollController.CalculateCategoryGrade(((Doctor)doctorsComboBox.SelectedItem).Username, pollController.GetDoctorPollBlueprint().Categories[1].Id); ;
            category3.Content = pollController.GetDoctorPollBlueprint().Categories[2].Name + pollController.CalculateCategoryGrade(((Doctor)doctorsComboBox.SelectedItem).Username, pollController.GetDoctorPollBlueprint().Categories[2].Id); ;
        }

        private void FillFirstCategoryQuestion() {
            PollCategory category = pollController.GetDoctorPollBlueprint().Categories[0];
            category1Q1.Text = pollController.GetDoctorPollBlueprint().Categories[0].PollQuestions[0].Question;
            q1.Content = pollController.CalculateQuestionGrade(((Doctor)doctorsComboBox.SelectedItem).Username, category.Id, category.PollQuestions[0].Id) + "/5";
            category1Q2.Text = pollController.GetDoctorPollBlueprint().Categories[0].PollQuestions[1].Question;
            q2.Content = pollController.CalculateQuestionGrade(((Doctor)doctorsComboBox.SelectedItem).Username, category.Id, category.PollQuestions[1].Id) + "/5";
            category1Q3.Text = pollController.GetDoctorPollBlueprint().Categories[0].PollQuestions[2].Question;
            q3.Content = pollController.CalculateQuestionGrade(((Doctor)doctorsComboBox.SelectedItem).Username, category.Id, category.PollQuestions[2].Id) + "/5";
            FillGrades(category, grades1);
            FillGrades(category, grades2);
            FillGrades(category, grades3);
        }
        private void FillSecondCategoryQuestion()
        {
            PollCategory category = pollController.GetDoctorPollBlueprint().Categories[1];

            category2Q1.Text = pollController.GetDoctorPollBlueprint().Categories[1].PollQuestions[0].Question;
            q4.Content = pollController.CalculateQuestionGrade(((Doctor)doctorsComboBox.SelectedItem).Username, category.Id, category.PollQuestions[0].Id) + "/5";
            category2Q2.Text = pollController.GetDoctorPollBlueprint().Categories[1].PollQuestions[1].Question;
            q5.Content = pollController.CalculateQuestionGrade(((Doctor)doctorsComboBox.SelectedItem).Username, category.Id, category.PollQuestions[1].Id) + "/5";
            category2Q3.Text = pollController.GetDoctorPollBlueprint().Categories[1].PollQuestions[2].Question;
            q6.Content = pollController.CalculateQuestionGrade(((Doctor)doctorsComboBox.SelectedItem).Username, category.Id, category.PollQuestions[2].Id) + "/5";
            FillGrades(category, grades4);
            FillGrades(category, grades5);
            FillGrades(category, grades6);
        }
        private void FillThirdCategoryQuestion()
        {
            PollCategory category = pollController.GetDoctorPollBlueprint().Categories[2];

            category3Q1.Text = pollController.GetDoctorPollBlueprint().Categories[2].PollQuestions[0].Question;
            q7.Content = pollController.CalculateQuestionGrade(((Doctor)doctorsComboBox.SelectedItem).Username, category.Id, category.PollQuestions[0].Id) + "/5";
            category3Q2.Text = pollController.GetDoctorPollBlueprint().Categories[2].PollQuestions[1].Question;
            q8.Content = pollController.CalculateQuestionGrade(((Doctor)doctorsComboBox.SelectedItem).Username, category.Id, category.PollQuestions[1].Id) + "/5";
            category3Q3.Text = pollController.GetDoctorPollBlueprint().Categories[2].PollQuestions[2].Question;
            q9.Content = pollController.CalculateQuestionGrade(((Doctor)doctorsComboBox.SelectedItem).Username, category.Id, category.PollQuestions[2].Id) + "/5";
            FillGrades(category, grades7);
            FillGrades(category, grades8);
            FillGrades(category, grades9);
        }

        private void FillGrades(PollCategory category, TextBlock grades) {
            grades.Text = "1|" + pollController.CountEachGrade(((Doctor)doctorsComboBox.SelectedItem).Username, category.Id, category.PollQuestions[0].Id)[0]
                +"   2|" + pollController.CountEachGrade(((Doctor)doctorsComboBox.SelectedItem).Username, category.Id, category.PollQuestions[0].Id)[1] + 
                "    3|" + pollController.CountEachGrade(((Doctor)doctorsComboBox.SelectedItem).Username, category.Id, category.PollQuestions[0].Id)[2] + 
                "    4|" + pollController.CountEachGrade(((Doctor)doctorsComboBox.SelectedItem).Username, category.Id, category.PollQuestions[0].Id)[3] +
                 "     5|" + pollController.CountEachGrade(((Doctor)doctorsComboBox.SelectedItem).Username, category.Id, category.PollQuestions[0].Id)[4];
        }

    }
}
