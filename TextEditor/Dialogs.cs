using System;
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
            searchDialog.Show(this);
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

        //Replace
        private void ReplaceDialog()
        {
            var margin = 10;
            var replaceDialog = new Form { Width = 350, Height = 180, Text = "Replace" };
            var searchTextLabel = new Label() { Left = margin, Top = margin + 2, Height = 18, Text = "Search for:", Width = 60 };
            var replaceTextLabel = new Label() { Left = margin, Top = margin + 30, Height = 18, Text = "Replace with:", Width = 60 };
            var searchInputBox = new TextBox() { Left = 70, Top = margin, Width = 160 };
            var replaceInputBox = new TextBox() { Left = 70, Top = margin + 30, Width = 160 };
            var findNextButton = new Button() { Text = "Find Next", Left = 240, Width = 80, Top = margin - 2 };
            var replaceNextButton = new Button() { Text = "Replace", Left = 240, Width = 80, Top = margin - 2 + 30 };
            var replaceAllButton = new Button() { Text = "Replace All", Left = 240, Width = 80, Top = margin - 2 + 60 };
            var cancelButton = new Button() { Text = "Cancel", Left = 240, Width = 80, Top = margin + 90 };

            //Init

            //Add controls
            replaceDialog.Controls.Add(findNextButton);
            replaceDialog.Controls.Add(replaceNextButton);
            replaceDialog.Controls.Add(replaceAllButton);
            replaceDialog.Controls.Add(cancelButton);

            replaceDialog.Controls.Add(searchTextLabel);
            replaceDialog.Controls.Add(replaceTextLabel);
            replaceDialog.Controls.Add(searchInputBox);
            replaceDialog.Controls.Add(replaceInputBox);

            replaceDialog.Show(this);
            searchInputBox.Focus();

            //Find next
            findNextButton.Click += (object sender, EventArgs e) =>
            {
                SearchNext(searchInputBox.Text, "right");
            };

            //Replace
            replaceNextButton.Click += (object sender, EventArgs e) =>
            {
                ReplaceNext(searchInputBox.Text, replaceInputBox.Text);
            };

            //Replace All
            replaceAllButton.Click += (object sender, EventArgs e) =>
            {
                ReplaceAll(searchInputBox.Text, replaceInputBox.Text);
            };

            //Close the dialog
            cancelButton.Click += (object sender, EventArgs e) => replaceDialog.Close();
        }

        //Jump to line
        private void JumpDialog()
        {
            var margin = 10;
            var jumpDialog = new Form { Width = 350, Height = 150, Text = "Search" };
            var textLabel = new Label() { Left = margin, Top = margin + 2, Height = 18, Text = "Jump to line:", Width = 80 };
            var inputBox = new TextBox() { Left = 90, Top = margin, Width = 140 };
            var jumpButton = new Button() { Text = "Jump", Left = 240, Width = 80, Top = margin - 2 };
            var cancelButton = new Button() { Text = "Cancel", Left = 240, Width = 80, Top = margin + 30 };

            //Init

            //Add controls
            jumpDialog.Controls.Add(jumpButton);
            jumpDialog.Controls.Add(cancelButton);
            jumpDialog.Controls.Add(textLabel);
            jumpDialog.Controls.Add(inputBox);
            jumpDialog.Show(this);
            inputBox.Focus();

            //Jump to line
            jumpButton.Click += (object sender, EventArgs e) =>
            {
                int inputNumber = 0;

                if (Int32.TryParse(inputBox.Text, out inputNumber))
                {
                    JumpToLine(inputNumber);
                }
                else
	            {
                    MessageBox.Show("Please enter a number", programName);
                }
            };

            //Close
            cancelButton.Click += (object sender, EventArgs e) => jumpDialog.Close();
        }
    }
}
