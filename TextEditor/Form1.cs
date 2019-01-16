using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;

namespace TextEditor
{
    public partial class Form1 : Form
    {
        /********** INIT **********/
        string programName = "SWS-Text";
        string copyText = "";
        string fullFilePath = "";
        bool docIsSaved = true;

        public Form1()
        {
            InitializeComponent();
            this.Text = programName;
        }

        /********** CODE **********/


        /********** MENU EVENTS **********/

        //Files
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewFile();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileAs();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Only if the doc is unsaved
            if (docIsSaved == false)
            {
                DoYouWantToSaveChanges();
            }

            //Otherwise just close the program
            else
            {
                ExitProgram();
            }
        }


        //Edit
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CutSelectedText();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopySelectedText();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PasteText();
        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchDialog();
        }

        private void findNextToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReplaceDialog();
        }

        private void goToToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectAllText();
        }

        private void dateAndTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddDateAndTime();
        }

        //View
        private void alwaysOnTopToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (alwaysOnTopToolStripMenuItem.Checked == true)
            {
                AlWaysOnTopOn();
            }
            else
            {
                AlWaysOnTopOff();
            }
        }

        private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (wordWrapToolStripMenuItem.Checked == true)
            {
                WordWrapOn();
            }
            else
            {
                WordWrapOff();
            }
        }

        private void lineNumbersToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {

        }

        //Syntax Highlight
        private void htmlToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {

        }

        //Settings
        private void changeFontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChooseFont();
        }

        private void changeThemeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        //Help
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        /********** OTHER EVENTS **********/

        //Text changed
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            docIsSaved = false;
            this.Text = programName + " *";
        }

        //Form is closing - The form is going to close no matter what
        private void Form1_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            //Only if the doc is unsaved
            if (docIsSaved == false)
            {
                FormClosingDoYouWantToSaveChanges();
            }
        }
    }
}
