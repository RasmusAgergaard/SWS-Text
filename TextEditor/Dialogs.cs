﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextEditor
{
    public partial class Form1
    {
        //Search
        private void SearchDialog()
        {
            var margin = 10;
            var searchDialog = new Form { Width = 350, Height = 150, Text = "Search" };
            var textLabel = new Label() { Left = margin, Top = margin + 2, Height = 18, Text = "Search for:", Width = 60};
            var inputBox = new TextBox() { Left = 70, Top = margin, Width = 160 };
            var groupRadioButton = new GroupBox() { Left = margin, Top = 50, Text = "Direction", Width = 200, Height = 50 };
            var rightRadioButton = new RadioButton() { Left = 100, Top = margin * 2, Text = "Forward", Width = 80};
            var leftRadioButton = new RadioButton() { Left = margin, Top = margin * 2, Text = "Backwards", Width = 80};
            var findNextButton = new Button() { Text = "Find Next", Left = 240, Width = 80, Top = margin - 2 };
            var cancelButton = new Button() { Text = "Cancel", Left = 240, Width = 80, Top = margin + 30 };

            //Init
            rightRadioButton.Checked = true;

            //Add controls
            searchDialog.Controls.Add(findNextButton);
            searchDialog.Controls.Add(cancelButton);
            searchDialog.Controls.Add(textLabel);
            searchDialog.Controls.Add(inputBox);
            searchDialog.Controls.Add(groupRadioButton);
            groupRadioButton.Controls.Add(rightRadioButton);
            groupRadioButton.Controls.Add(leftRadioButton);
            searchDialog.Show();
            inputBox.Focus();

            //Send the input to the Search method
            findNextButton.Click += (object sender, EventArgs e) => {

                if (rightRadioButton.Checked == true)
                {
                    SearchNext(inputBox.Text, "right");
                }
                else
                {
                    SearchNext(inputBox.Text, "left");
                }
            };

            //Close the dialog
            cancelButton.Click += (object sender, EventArgs e) => searchDialog.Close();
        }
    }
}
