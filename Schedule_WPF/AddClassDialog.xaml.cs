﻿using Schedule_WPF.Models;
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

namespace Schedule_WPF
{
    /// <summary>
    /// Interaction logic for AddClassDialog.xaml
    /// </summary>
    public partial class AddClassDialog : Window
    {
        public AddClassDialog()
        {
            InitializeComponent();
            Prof_Text.ItemsSource = (IEnumerable<Professors>)Application.Current.FindResource("Professor_List_View");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // If successfull data entry, communicate data back to MainWindow
            if (allRequiredFields())
            {
                // Get information from input fields
                int term = Int32.Parse(Term_Text.Text);
                int session = Int32.Parse(Session_Text.Text);
                string crn = CRN_Text.Text;
                string dpt = Dept_Text.Text;
                int classNum = Int32.Parse(ClassNum_Text.Text);
                int sectNum = Int32.Parse(Section_Text.Text);
                string name = Name_Text.Text;
                int credits = Int32.Parse(Credits_Text.Text);
                Professors professor = (Professors)Prof_Text.SelectedItem;
                string profname;
                string waitlist = Waitlist_Text.Text;
                string enrolled = Enrolled_Text.Text;
                string maxSeats = MaxSeats_Text.Text.ToString();
                string projSeats = ProjSeats_Text.Text.ToString();
                string startDate = StartDate.Text;
                string endDate = EndDate.Text;
                string crosslist = Crosslist_Text.Text;


                if (professor != null)
                {
                    profname = professor.SRUID;
                }
                else
                {
                    profname = "";
                }

                // Store the information in the appropriate variables inside MainWindow
                Application.Current.Resources["Set_Class_Success"] = true;
                Application.Current.Resources["Set_Class_Term"] = term;
                Application.Current.Resources["Set_Class_Session"] = session;
                Application.Current.Resources["Set_Class_CRN"] = crn;
                Application.Current.Resources["Set_Class_Dept"] = dpt;
                Application.Current.Resources["Set_Class_Number"] = classNum;
                Application.Current.Resources["Set_Class_Section"] = sectNum;
                Application.Current.Resources["Set_Class_Name"] = name;
                Application.Current.Resources["Set_Class_Credits"] = credits;
                Application.Current.Resources["Set_Class_Professor"] = profname;
                Application.Current.Resources["Set_Class_Online"] = (bool)Online_Box.IsChecked;
                Application.Current.Resources["Set_Class_Appointment"] = (bool)Appointment_Box.IsChecked;
                Application.Current.Resources["Set_Class_Appointment2"] = (bool)Appointment2_Box.IsChecked;
                Application.Current.Resources["Set_Class_Enrolled"] = enrolled;
                Application.Current.Resources["Set_Class_Waitlist"] = waitlist;
                Application.Current.Resources["Set_Class_maxSeats"] = Int32.Parse(maxSeats);
                Application.Current.Resources["Set_Class_projSeats"] = Int32.Parse(projSeats);
                Application.Current.Resources["Set_Class_StartDate"] = startDate;
                Application.Current.Resources["Set_Class_EndDate"] = endDate;
                Application.Current.Resources["Set_Class_Crosslist"] = crosslist;

                // Close the window
                this.Close();
            }
        }

        private bool allRequiredFields()
        {
            bool success = true;
            int tmp;


            //Class Term
            if (Term_Text.Text == null)
            {
                Term_Required.Visibility = Visibility.Visible;
                Term_Invalid.Visibility = Visibility.Hidden;

                success = false;
            }
            else if (!Int32.TryParse(Term_Text.Text, out tmp))
            {
                Term_Required.Visibility = Visibility.Hidden;
                Term_Invalid.Visibility = Visibility.Visible;
                success = false;
            }
            else
            {
                if (Int32.Parse(Term_Text.Text) > 0)
                {
                    Term_Invalid.Visibility = Visibility.Hidden;
                    Session_Required.Visibility = Visibility.Hidden;
                }
                else
                {
                    Term_Invalid.Visibility = Visibility.Visible;
                    Term_Required.Visibility = Visibility.Hidden;
                    success = false;
                }
            }

            //Class Session
            if (Session_Text.Text == "")
            {
                Session_Required.Visibility = Visibility.Visible;
                Session_Invalid.Visibility = Visibility.Hidden;

                success = false;
            }
            else if (!Int32.TryParse(Session_Text.Text, out tmp))
            {
                Session_Required.Visibility = Visibility.Hidden;
                Session_Invalid.Visibility = Visibility.Visible;
                success = false;
            }
            else
            {
                if (tmp > 0)
                {
                    Session_Invalid.Visibility = Visibility.Hidden;
                    Session_Required.Visibility = Visibility.Hidden;
                }
                else
                {
                    Session_Invalid.Visibility = Visibility.Visible;
                    Session_Required.Visibility = Visibility.Hidden;
                    success = false;
                }
            }


            // Class CRN
            if (CRN_Text.Text == "")
            {
                CRN_Required.Visibility = Visibility.Visible;
                CRN_Invalid.Visibility = Visibility.Hidden;
                success = false;
            }
            else if (!Int32.TryParse(CRN_Text.Text, out tmp))
            {
                CRN_Text.Text = CRN_Text.Text.ToUpper();
                if (CRN_Text.Text != "NEW")
                {
                    CRN_Invalid.Visibility = Visibility.Visible;
                    CRN_Required.Visibility = Visibility.Hidden;
                    success = false;
                }
            }
            else
            {
                CRN_Invalid.Visibility = Visibility.Hidden;
                CRN_Required.Visibility = Visibility.Hidden;
            }
            // Department Name
            if (Dept_Text.Text == "")
            {
                Dept_Required.Visibility = Visibility.Visible;
                Dept_Invalid.Visibility = Visibility.Hidden;
                success = false;
            }
            else
            {
                if (Dept_Text.Text.Length > 4)
                {
                    Dept_Required.Visibility = Visibility.Hidden;
                    Dept_Invalid.Visibility = Visibility.Visible;
                    success = false;
                }
                else
                {
                    Dept_Required.Visibility = Visibility.Hidden;
                    Dept_Invalid.Visibility = Visibility.Hidden;
                    Dept_Text.Text = Dept_Text.Text.ToUpper();
                }
            }
            // Class Number
            if (ClassNum_Text.Text == "")
            {
                Number_Required.Visibility = Visibility.Visible;
                Number_Invalid.Visibility = Visibility.Hidden;

                success = false;
            }
            else if (!Int32.TryParse(ClassNum_Text.Text, out tmp))
            {
                Number_Invalid.Visibility = Visibility.Visible;
                Number_Required.Visibility = Visibility.Hidden;
                success = false;
            }
            else
            {
                if (tmp > 99 && tmp < 1000)
                {
                    Number_Invalid.Visibility = Visibility.Hidden;
                    Number_Required.Visibility = Visibility.Hidden;
                }
                else
                {
                    Number_Invalid.Visibility = Visibility.Visible;
                    Number_Required.Visibility = Visibility.Hidden;
                    success = false;
                }
            }

            ////// Class Section
            if (Section_Text.Text == "")
            {
                Section_Required.Visibility = Visibility.Visible;
                Section_Invalid.Visibility = Visibility.Hidden;
                success = false;
            }
            else if (!Int32.TryParse(Section_Text.Text, out tmp))
            {
                Section_Invalid.Visibility = Visibility.Visible;
                Section_Required.Visibility = Visibility.Hidden;
                success = false;
            }
            else
            {
                int sectionNum = Int32.Parse(Section_Text.Text);
                if (sectionNum >= 0 && sectionNum < 100)
                {
                    Section_Invalid.Visibility = Visibility.Hidden;
                    Section_Required.Visibility = Visibility.Hidden;
                }
                else
                {
                    Section_Invalid.Visibility = Visibility.Visible;
                    Section_Required.Visibility = Visibility.Hidden;
                    success = false;
                }
            }
            // Class Name
            if (Name_Text.Text == "")
            {
                Name_Required.Visibility = Visibility.Visible;
                success = false;
            }
            else
            {
                Name_Required.Visibility = Visibility.Hidden;
            }

            // Professor
            if (Prof_Text.Text == "")
            {
                Prof_Required.Visibility = Visibility.Visible;
                success = false;
            }
            else
            {
                Prof_Required.Visibility = Visibility.Hidden;
            }

            // Class Credits
            if (Credits_Text.Text == "")
            {
                Credits_Required.Visibility = Visibility.Visible;
                Credits_Invalid.Visibility = Visibility.Hidden;
                success = false;
            }
            else if (!Int32.TryParse(Credits_Text.Text, out tmp))
            {
                Credits_Invalid.Visibility = Visibility.Visible;
                Credits_Required.Visibility = Visibility.Hidden;
                success = false;
            }
            else
            {
                if (tmp > 0 && tmp < 15)
                {
                    Credits_Invalid.Visibility = Visibility.Hidden;
                    Credits_Required.Visibility = Visibility.Hidden;
                }
                else
                {
                    Credits_Invalid.Visibility = Visibility.Visible;
                    Credits_Required.Visibility = Visibility.Hidden;
                    success = false;
                }
            }

            // Waitlist
            if (Waitlist_Text.Text == "")
            {
                Waitlist_Required.Visibility = Visibility.Visible;
                Waitlist_Invalid.Visibility = Visibility.Hidden;
                success = false;
            }
            else if (!Int32.TryParse(Waitlist_Text.Text, out tmp))
            {
                Waitlist_Required.Visibility = Visibility.Visible;
                Waitlist_Invalid.Visibility = Visibility.Hidden;
                success = false;
            }
            else
            {
                if (tmp >= 0)
                {
                    Waitlist_Required.Visibility = Visibility.Hidden;
                    Waitlist_Invalid.Visibility = Visibility.Hidden;
                }
                else
                {
                    Waitlist_Required.Visibility = Visibility.Visible;
                    Waitlist_Invalid.Visibility = Visibility.Hidden;
                    success = false;
                }
            }

            // Enrolled
            if (Enrolled_Text.Text == "")
            {
                Enrolled_Required.Visibility = Visibility.Visible;
                Enrolled_Invalid.Visibility = Visibility.Hidden;
                success = false;
            }
            else if (!Int32.TryParse(Enrolled_Text.Text, out tmp))
            {
                Enrolled_Required.Visibility = Visibility.Visible;
                Enrolled_Invalid.Visibility = Visibility.Hidden;
                success = false;
            }
            else
            {
                if (tmp >= 0)
                {
                    Enrolled_Required.Visibility = Visibility.Hidden;
                    Enrolled_Invalid.Visibility = Visibility.Hidden;
                }
                else
                {
                    Enrolled_Required.Visibility = Visibility.Visible;
                    Enrolled_Invalid.Visibility = Visibility.Hidden;
                    success = false;
                }
            }

            // Max Seats
            if (MaxSeats_Text.Text == "")
            {
                MaxSeats_Required.Visibility = Visibility.Visible;
                MaxSeats_Invalid.Visibility = Visibility.Hidden;
                success = false;
            }
            else if (!Int32.TryParse(MaxSeats_Text.Text, out tmp))
            {
                MaxSeats_Required.Visibility = Visibility.Visible;
                MaxSeats_Invalid.Visibility = Visibility.Hidden;
                success = false;
            }
            else
            {
                if (tmp >= 0)
                {
                    MaxSeats_Required.Visibility = Visibility.Hidden;
                    MaxSeats_Invalid.Visibility = Visibility.Hidden;
                }
                else
                {
                    MaxSeats_Required.Visibility = Visibility.Visible;
                    MaxSeats_Invalid.Visibility = Visibility.Hidden;
                    success = false;
                }
            }

            // Proj Seats
            if (ProjSeats_Text.Text == "")
            {
                ProjSeats_Required.Visibility = Visibility.Visible;
                ProjSeats_Invalid.Visibility = Visibility.Hidden;
                success = false;
            }
            else if (!Int32.TryParse(ProjSeats_Text.Text, out tmp))
            {
                ProjSeats_Required.Visibility = Visibility.Visible;
                ProjSeats_Invalid.Visibility = Visibility.Hidden;
                success = false;
            }
            else
            {
                if (tmp >= 0)
                {
                    ProjSeats_Required.Visibility = Visibility.Hidden;
                    ProjSeats_Invalid.Visibility = Visibility.Hidden;
                }
                else
                {
                    ProjSeats_Required.Visibility = Visibility.Visible;
                    ProjSeats_Invalid.Visibility = Visibility.Hidden;
                    success = false;
                }
            }



            return success;
        }


        private void Add_Prof_Click(object sender, RoutedEventArgs e)
        {
            AddProfessorDialog addProfDialog = new AddProfessorDialog();
            addProfDialog.Owner = this;
            addProfDialog.ShowDialog();

            ProfessorList professors = (ProfessorList)Application.Current.Resources["Professor_List_View"];

            if ((bool)Application.Current.Resources["Set_Prof_Success"])
            {
                // Get data about new professor
                string fName = (string)Application.Current.Resources["Set_Prof_FN"];
                string lName = (string)Application.Current.Resources["Set_Prof_LN"];
                string id = (string)Application.Current.Resources["Set_Prof_ID"];
                string colorString = (string)Application.Current.Resources["Set_Prof_Color"];
                // Create new professor object and add to professor list
                Professors prof = new Professors(fName, lName, id);
                prof.profRGB = new RGB_Color(colorString);
                professors.Add(prof);
                // Set the new professor as the comboBox selected value
                Prof_Text.SelectedIndex = (professors.Count - 1);
                // Reset Success flag for the addProfessorDialog
                Application.Current.Resources["Set_Prof_Success"] = false;
            }
        }

        private void CorrectDates(object sender, SelectionChangedEventArgs e)
        {
            var test = EndDate.Text.ToString().CompareTo(StartDate.Text.ToString());

            if (EndDate.Text == null)
            {
                EndDate.SelectedDate = StartDate.SelectedDate.Value.AddDays(30);
                Application.Current.Resources["Set_Class_EndDate"] = EndDate.Text;
            }
            else if ((test == 0) || (test == -1))
            {
                EndDate.SelectedDate = StartDate.SelectedDate.Value.AddDays(30);
                EndDate_Invalid.Visibility = Visibility.Visible;
                Application.Current.Resources["Set_Class_EndDate"] = EndDate.Text;
            }
            else
            {
                EndDate_Invalid.Visibility = Visibility.Hidden;
            }
        }
    }
}
