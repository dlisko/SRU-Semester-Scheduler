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
    /// Interaction logic for AddClassRoomDialog.xaml
    /// </summary>
    public partial class AddClassRoomDialog : Window
    {
        public AddClassRoomDialog()
        {
            InitializeComponent();
        }

        private void SubmitData(object sender, RoutedEventArgs e)
        {
            if (allRequiredFields())
            {
                string building = Building_Text.Text.ToString();
                int roomNum = Int32.Parse(Number_Text.Text);
                int seats = Int32.Parse(Seats_Text.Text);
                string notes;
                

                if (Notes_Text.Text == null)
                {
                    notes = "";
                }
                else
                {
                    notes = Notes_Text.Text.ToString();
                }
                Application.Current.Resources["Set_ClassRoom_Bldg"] = building;
                Application.Current.Resources["Set_ClassRoom_Num"] = roomNum;
                Application.Current.Resources["Set_ClassRoom_Seats"] = seats;
                Application.Current.Resources["Set_ClassRoom_Notes"] = notes;
                Application.Current.Resources["Set_ClassRoom_Success"] = true;
                this.Close();
            }

        }

        private bool allRequiredFields()
        {
            bool success = true;
            int tmp;
            // Building name
            if (Building_Text.Text == "")
            {
                Building_Required.Visibility = Visibility.Visible;
                Building_Invalid.Visibility = Visibility.Hidden;
                success = false;
            }
            else
            {
                if (Building_Text.Text.Contains(" ") || Building_Text.Text.Length > 4)
                {
                    Building_Required.Visibility = Visibility.Hidden;
                    Building_Invalid.Visibility = Visibility.Visible;
                    success = false;
                }
                else
                {
                    Building_Invalid.Visibility = Visibility.Hidden;
                    Building_Required.Visibility = Visibility.Hidden;
                    Building_Text.Text = Building_Text.Text.ToUpper();
                }
            }
            // Room Number
            if (Number_Text.Text == "")
            {
                Number_Required.Visibility = Visibility.Visible;
                Number_Invalid.Visibility = Visibility.Hidden;
                success = false;
            }
            else if (!Int32.TryParse(Number_Text.Text, out tmp))
            {
                Number_Invalid.Visibility = Visibility.Visible;
                Number_Required.Visibility = Visibility.Hidden;
                success = false;
            }
            else
            {
                Number_Invalid.Visibility = Visibility.Hidden;
                Number_Required.Visibility = Visibility.Hidden;
            }
            ClassRoomList classrooms = (ClassRoomList)System.Windows.Application.Current.FindResource("ClassRoom_List_View");
            string bldgID, tempRoomLabel, inputRoomLabel;
            int roomID;

            inputRoomLabel = (Building_Text.Text + "-" + Number_Text.Text);
            for (int n = 0; n < classrooms.Count; n++)
            {
                //cycles through each room in list to find the one that user originally clicked on

                bldgID = classrooms[n].Location;
                roomID = classrooms[n].RoomNum;

                //this is the indexed room
                tempRoomLabel = (bldgID + "-" + roomID);


                //This is if the new building and room num already exist
                if (tempRoomLabel == inputRoomLabel)
                {
                    Number_Duplicate.Visibility = Visibility.Visible;
                    Building_Duplicate.Visibility = Visibility.Visible;
                    success = false;
                }
            }

            // Seats
            if (Seats_Text.Text != "")
            {
                if (!Int32.TryParse(Seats_Text.Text, out tmp))
                {
                    Seats_Invalid.Visibility = Visibility.Visible;
                    success = false;
                }
                else
                {
                    Seats_Invalid.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                Seats_Text.Text = "0";
            }

            return success;
        }
    }
}
