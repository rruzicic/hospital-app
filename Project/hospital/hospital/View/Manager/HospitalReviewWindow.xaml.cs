﻿using hospital.Controller;
using hospital.Model;
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
    /// Interaction logic for HospitalReviewWindow.xaml
    /// </summary>
    public partial class HospitalReviewWindow : Window
    {
        private PollController pollController;
        public HospitalReviewWindow()
        {
            InitializeComponent();
            App app = Application.Current as App;
            pollController = app.pollBlueprintController;
            FillFinalGrade();
            FillCategories();
            FillFirstCategoryQuestion();
            FillSecondCategoryQuestion();
            FillThirdCategoryQuestion();
        }

        private void FillFinalGrade()
        {
            finalAverage.Content = pollController.CalculateHospitalFinalGrade();
        }
        private void FillCategories()
        {
            category1.Content = pollController.GetHospitalPollBlueprint().Categories[0].Name + pollController.CalculateHospitalCategoryGrade(pollController.GetDoctorPollBlueprint().Categories[0].Id);
            category2.Content = pollController.GetHospitalPollBlueprint().Categories[1].Name + pollController.CalculateHospitalCategoryGrade(pollController.GetDoctorPollBlueprint().Categories[1].Id); ;
            category3.Content = pollController.GetHospitalPollBlueprint().Categories[2].Name + pollController.CalculateHospitalCategoryGrade(pollController.GetDoctorPollBlueprint().Categories[2].Id); ;
        }

        private void FillFirstCategoryQuestion()
        {
            PollCategory category = pollController.GetHospitalPollBlueprint().Categories[0];
            category1Q1.Text = pollController.GetHospitalPollBlueprint().Categories[0].PollQuestions[0].Question;
            q1.Content = pollController.CalculateHospitalQuestionGrade(category.Id, category.PollQuestions[0].Id) + "/5";
            category1Q2.Text = pollController.GetHospitalPollBlueprint().Categories[0].PollQuestions[1].Question;
            q2.Content = pollController.CalculateHospitalQuestionGrade(category.Id, category.PollQuestions[1].Id) + "/5";
            category1Q3.Text = pollController.GetHospitalPollBlueprint().Categories[0].PollQuestions[2].Question;
            q3.Content = pollController.CalculateHospitalQuestionGrade(category.Id, category.PollQuestions[2].Id) + "/5";
            FillGrades(category, 0, grades1);
            FillGrades(category, 1, grades2);
            FillGrades(category, 2, grades3);
        }
        private void FillSecondCategoryQuestion()
        {
            PollCategory category = pollController.GetHospitalPollBlueprint().Categories[1];

            category2Q1.Text = pollController.GetHospitalPollBlueprint().Categories[1].PollQuestions[0].Question;
            q4.Content = pollController.CalculateHospitalQuestionGrade(category.Id, category.PollQuestions[0].Id) + "/5";
            category2Q2.Text = pollController.GetHospitalPollBlueprint().Categories[1].PollQuestions[1].Question;
            q5.Content = pollController.CalculateHospitalQuestionGrade(category.Id, category.PollQuestions[1].Id) + "/5";
            category2Q3.Text = pollController.GetHospitalPollBlueprint().Categories[1].PollQuestions[2].Question;
            q6.Content = pollController.CalculateHospitalQuestionGrade(category.Id, category.PollQuestions[2].Id) + "/5";
            FillGrades(category, 0, grades4);
            FillGrades(category, 1, grades5);
            FillGrades(category, 2, grades6);
        }
        private void FillThirdCategoryQuestion()
        {
            PollCategory category = pollController.GetHospitalPollBlueprint().Categories[2];

            category3Q1.Text = pollController.GetHospitalPollBlueprint().Categories[2].PollQuestions[0].Question;
            q7.Content = pollController.CalculateHospitalQuestionGrade(category.Id, category.PollQuestions[0].Id) + "/5";
            category3Q2.Text = pollController.GetHospitalPollBlueprint().Categories[2].PollQuestions[1].Question;
            q8.Content = pollController.CalculateHospitalQuestionGrade(category.Id, category.PollQuestions[1].Id) + "/5";
            category3Q3.Text = pollController.GetHospitalPollBlueprint().Categories[2].PollQuestions[2].Question;
            q9.Content = pollController.CalculateHospitalQuestionGrade(category.Id, category.PollQuestions[2].Id) + "/5";
            FillGrades(category, 0, grades7);
            FillGrades(category, 1, grades8);
            FillGrades(category, 2, grades9);
        }

        private void FillGrades(PollCategory category, int question, TextBlock grades)
        {
            int[] gradesCount = pollController.CountEachHospitalGrade(category.Id, category.PollQuestions[question].Id);
            grades.Text = "1|" + gradesCount[0] +
                "   2|" + gradesCount[1] +
                "    3|" + gradesCount[2] +
                "    4|" + gradesCount[3] +
                "     5|" + gradesCount[4];
        }
    }
}
