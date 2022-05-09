﻿using System;
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

namespace Schedule_WPF.Models
{
    /// <summary>
    /// Interaction logic for EditProfessorDialog.xaml
    /// </summary>
    public partial class EditProfessorDialog : Window
    {
        Professors targetProfessor = null;
        string originalSRUID = "";

        ProfessorList professors = (ProfessorList)Application.Current.FindResource("Professor_List_View");

        public EditProfessorDialog(Professors prof)
        {
            InitializeComponent();
            targetProfessor = prof;
            if (targetProfessor != null)
            {
                originalSRUID = targetProfessor.SRUID;
                FirstName.Text = targetProfessor.FirstName;
                LastName.Text = targetProfessor.LastName;
                ID.Text = targetProfessor.SRUID;
                colorPicker.SelectedColor = targetProfessor.profRGB.colorBrush;
                Classes.Text = targetProfessor.MaxClasses.ToString();
                Prep.Text = targetProfessor.MaxPrep.ToString();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (allRequiredFields() && targetProfessor != null)
            {
                targetProfessor.FirstName = FirstName.Text;
                targetProfessor.LastName = LastName.Text;
                targetProfessor.SRUID = ID.Text;
                targetProfessor.profRGB = new RGB_Color(colorPicker.SelectedColor.ToString());
                targetProfessor.MaxClasses = Int32.Parse(Classes.Text);
                targetProfessor.MaxPrep = Int32.Parse(Prep.Text);
                Close();
            }
        }

        private bool allRequiredFields()
        {
            bool success = true;
            // First Name
            if (FirstName.Text == "")
            {
                FirstName_Required.Visibility = Visibility.Visible;
                success = false;
            }
            else
            {
                FirstName_Required.Visibility = Visibility.Hidden;
            }
            // Last Name
            if (LastName.Text == "")
            {
                LastName_Required.Visibility = Visibility.Visible;
                success = false;
            }
            else
            {
                LastName_Required.Visibility = Visibility.Hidden;
            }
            string newname = LastName.Text.ToUpper() + ", " + FirstName.Text.ToUpper();
            string currname = targetProfessor.LastName.ToUpper() + ", " + targetProfessor.FirstName.ToUpper();
            for (int i = 0; i < professors.Count; i++)
            {

                if (newname == professors[i].LastName.ToUpper() + ", " + professors[i].FirstName.ToUpper() && newname != currname)
                {
                    FName_Duplicate.Visibility = Visibility.Visible;
                    LName_Duplicate.Visibility = Visibility.Visible;
                    success = false;
                    break;

                }


            }
                // SRU ID
            if (ID.Text == "")
            {
                ID_Required.Visibility = Visibility.Visible;
                ID_Invalid.Visibility = Visibility.Hidden;
                success = false;
            }
            else
            {
                if (ID.Text.Length != 9 || ID.Text.Substring(0, 2) != "A0")
                {
                    ID_Invalid.Visibility = Visibility.Visible;
                    ID_Required.Visibility = Visibility.Hidden;
                    success = false;
                }
                else
                {
                    for (int i = 0; i < professors.Count; i++)
                    {

                        if (ID.Text == professors[i].SRUID && ID.Text != originalSRUID)
                        {
                            ID_Duplicate.Visibility = Visibility.Visible;
                            ID_Invalid.Visibility = Visibility.Hidden;
                            ID_Required.Visibility = Visibility.Hidden;
                            success = false;
                            break;

                        }
                        else
                        {
                            ID_Duplicate.Visibility = Visibility.Hidden;
                            ID_Invalid.Visibility = Visibility.Hidden;
                            ID_Required.Visibility = Visibility.Hidden;
                        }


                    }
                }
            }
            // Color
            if (colorPicker.SelectedColor.ToString() == "")
            {
                Color_Required.Visibility = Visibility.Visible;
                Color_Invalid.Visibility = Visibility.Hidden;
                success = false;
            }
            else
            {
                RGB_Color tempColor = new RGB_Color(colorPicker.SelectedColor.ToString());
                if (isColorTaken(tempColor) && colorPicker.SelectedColor != targetProfessor.profRGB.colorBrush)
                {
                    Color_Invalid.Visibility = Visibility.Visible;
                    Color_Required.Visibility = Visibility.Hidden;
                    success = false;
                }
                else
                {
                    Color_Invalid.Visibility = Visibility.Hidden;
                    Color_Required.Visibility = Visibility.Hidden;
                }
            }
            // Max Classes/Prep
            if (Classes.Text == "" || Prep.Text == "")
            {
                Max_Required.Visibility = Visibility.Visible;
                Max_Invalid.Visibility = Visibility.Hidden;
                success = false;
            }
            else
            {
                int classParse, prepParse;
                if (!Int32.TryParse(Classes.Text, out classParse) || !Int32.TryParse(Prep.Text, out prepParse))
                {
                    Max_Invalid.Visibility = Visibility.Visible;
                    Max_Required.Visibility = Visibility.Hidden;
                    success = false;
                }
                else
                {
                    Max_Invalid.Visibility = Visibility.Hidden;
                    Max_Required.Visibility = Visibility.Hidden;
                }
            }
            return success;
        }

        public bool isColorTaken(RGB_Color color)
        {
            for (int i = 0; i < professors.Count; i++)
            {
                if (professors[i].SRUID != ID.Text && withinColorRange(color, professors[i].profRGB))
                {
                    return true;
                }
            }
            return false;
        }
        public bool withinColorRange(RGB_Color c1, RGB_Color c2)
        {
            int threshold = 40;
            if (Math.Abs(c1.R - c2.R) <= threshold && Math.Abs(c1.G - c2.G) <= threshold && Math.Abs(c1.B - c2.B) <= threshold)
            {
                return true;
            }
            return false;
        }
    }
}
