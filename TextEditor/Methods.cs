using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextEditor
{
    public partial class Form1
    {
        /********** METHODS **********/

        //Drop down item clicked
        private void fileToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            this.UpdateStatus(e.ClickedItem);
        }

        private void OpenFile()
        {
            if (docIsSaved == false)
            {
                //Check to see if user want to save
                DialogResult dialogResult = MessageBox.Show("Do you want to save your changes?", "TextEditor", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    SaveFile();
                }
            }

            //Open a new file
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //Read content from file and save it in a string
                string fileContent = File.ReadAllText(openFileDialog.FileName);

                //Set textBox to the file content
                textBox1.Text = fileContent;
            }
            else
            {
                MessageBox.Show("Something went wrong");
            }

            //Set status as saved
            docIsSaved = true;
        }

        private void NewFile()
        {
            if (docIsSaved == false)
            {
                //Check to see if user want to save
                DialogResult dialogResult = MessageBox.Show("Do you want to save your changes?", "TextEditor", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    SaveFile();
                }
            }

            //Reset text box
            textBox1.Text = "";

            //Set status
            docIsSaved = true;
        }

        private void ChooseFont()
        {
            FontDialog fontDialog = new FontDialog();
            if (fontDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Font = fontDialog.Font;
            }
        }

        private void SaveFile()
        {
            if (fullFilePath == "")
            {
                SaveFileAs();
            }
            else
            {
                QuickSaveFile();
            }
        }

        private void QuickSaveFile()
        {
            //Save the file using the file path
            File.WriteAllText(fullFilePath, textBox1.Text);

            //Set status
            docIsSaved = true;
            this.Text = programName;
        }

        private void SaveFileAs()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Txt file|*.txt";
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //Update the file path
                fullFilePath = saveFileDialog.FileName;

                QuickSaveFile();
            }
        }

        private void SaveFileAndQuit()
        {
            SaveFile();
            ExitProgram();
        }

        private void PrintFile()
        {
            PrintDialog printDialog = new PrintDialog();

            if (printDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                PrintDocument printDocument = new PrintDocument();
                printDocument.PrintPage += new PrintPageEventHandler(printDocument_printPage);
                printDocument.Print();
            }
        }

        private void printDocument_printPage(object sender, PrintPageEventArgs ev)
        {
            ev.Graphics.DrawString(textBox1.Text, new Font("Arial", 10), Brushes.Black,
                      ev.MarginBounds.Left, 0, new StringFormat());
        }

        private void UpdateStatus(ToolStripItem item)
        {
            if (item != null)
            {
                string msg = String.Format("{0} selected", item.Text);
                this.statusStrip1.Items[0].Text = msg;
            }
        }

        private void AlWaysOnTopOn()
        {
            this.TopMost = true;
        }

        private void AlWaysOnTopOff()
        {
            this.TopMost = false;
        }

        private void WordWrapOn()
        {
            textBox1.WordWrap = true;
        }

        private void WordWrapOff()
        {
            textBox1.WordWrap = false;
        }

        private void SelectAllText()
        {
            textBox1.SelectAll();
        }

        private void AddDateAndTime()
        {
            string currentDateTime = DateTime.Now.ToString();
            var selectionIndex = textBox1.SelectionStart;
            textBox1.Text = textBox1.Text.Insert(selectionIndex, currentDateTime);

            //Move selection index to after the pasted line
            textBox1.SelectionStart = selectionIndex + currentDateTime.Length;
        }

        private void CutSelectedText()
        {
            int selectionLenght = textBox1.SelectionLength;
            int selectionStart = textBox1.SelectionStart;

            //Save the selected text
            copyText = textBox1.SelectedText;

            //Remove the selected text
            textBox1.Text = textBox1.Text.Remove(textBox1.SelectionStart, selectionLenght);

            //Set selection index
            textBox1.SelectionStart = selectionStart;
        }

        private void CopySelectedText()
        {
            //Save selected text
            copyText = textBox1.SelectedText;
        }

        private void PasteText()
        {
            var selectionIndex = textBox1.SelectionStart;
            textBox1.Text = textBox1.Text.Insert(selectionIndex, copyText);

            //Move selection index to after the pasted line
            textBox1.SelectionStart = selectionIndex + copyText.Length;
        }

        private void SearchNext(string search, string direction)
        {
            int positionOfResult;

            if (direction == "left")
            {
                positionOfResult = textBox1.Text.LastIndexOf(search, textBox1.SelectionStart);
            }
            else
            {
                positionOfResult = textBox1.Text.IndexOf(search, textBox1.SelectionStart + textBox1.SelectionLength);
            }


            if (positionOfResult != -1)
            {
                textBox1.SelectionStart = positionOfResult;
                textBox1.SelectionLength = search.Length;
                textBox1.HideSelection = false;
            }
            else
            {
                MessageBox.Show("'" + search + "'" + " was not found", programName);
            }
        }

        private void ReplaceNext(string search, string replace)
        {
            //If the right text is selected, replace it
            if (textBox1.SelectionLength == search.Length && search == textBox1.SelectedText)
            {
                textBox1.SelectedText = replace;
            }

            //Find the next string part
            int positionOfResult = textBox1.Text.IndexOf(search, textBox1.SelectionStart + textBox1.SelectionLength);

            if (positionOfResult != -1)
            {
                textBox1.SelectionStart = positionOfResult;
                textBox1.SelectionLength = search.Length;
                textBox1.HideSelection = false;
            }
            else
            {
                MessageBox.Show("'" + search + "'" + " was not found", programName);
            }
        }

        private void ReplaceAll(string search, string replace)
        {
            var replacedText = textBox1.Text.Replace(search, replace);
            textBox1.Text = replacedText;
        }

        private void JumpToLine(int lineNumber, Form dialogToClose)
        {
            //Count number of lines
            var lines = textBox1.Lines.Count();
            lines -= String.IsNullOrWhiteSpace(textBox1.Lines.Last()) ? 1 : 0;

            //If the entered number is within the possible
            if (lineNumber <= lines && lineNumber != 0)
            {
                //Move the cursor
                int skipChars = textBox1.GetFirstCharIndexFromLine(lineNumber - 1);
                textBox1.Select(skipChars, 0);
                textBox1.Focus();
                textBox1.ScrollToCaret();

                //Close the dialog
                dialogToClose.Close();
            }
            else
            {
                MessageBox.Show("Line number is larger that the number of lines in the file", programName);
            }
        }

        private void SyntaxHighlight()
        {
            // getting keywords/functions
            string keywords = @"\b(public|private|partial|static|namespace|class|using|void|foreach|in)\b";
            MatchCollection keywordMatches = Regex.Matches(textBox1.Text, keywords);

            // getting types/classes from the text 
            string types = @"\b(Console)\b";
            MatchCollection typeMatches = Regex.Matches(textBox1.Text, types);

            // getting comments (inline or multiline)
            string comments = @"(\/\/.+?$|\/\*.+?\*\/)";
            MatchCollection commentMatches = Regex.Matches(textBox1.Text, comments, RegexOptions.Multiline);

            // getting strings
            string strings = "\".+?\"";
            MatchCollection stringMatches = Regex.Matches(textBox1.Text, strings);

            // saving the original caret position + forecolor
            int originalIndex = textBox1.SelectionStart;
            int originalLength = textBox1.SelectionLength;
            Color originalColor = Color.Black;

            // MANDATORY - focuses a label before highlighting (avoids blinking)
            //titleLabel.Focus();

            // removes any previous highlighting (so modified words won't remain highlighted)
            textBox1.SelectionStart = 0;
            textBox1.SelectionLength = textBox1.Text.Length;
            textBox1.SelectionColor = originalColor;

            // scanning...
            foreach (Match m in keywordMatches)
            {
                textBox1.SelectionStart = m.Index;
                textBox1.SelectionLength = m.Length;
                textBox1.SelectionColor = Color.Blue;
            }

            foreach (Match m in typeMatches)
            {
                textBox1.SelectionStart = m.Index;
                textBox1.SelectionLength = m.Length;
                textBox1.SelectionColor = Color.DarkCyan;
            }

            foreach (Match m in commentMatches)
            {
                textBox1.SelectionStart = m.Index;
                textBox1.SelectionLength = m.Length;
                textBox1.SelectionColor = Color.Green;
            }

            foreach (Match m in stringMatches)
            {
                textBox1.SelectionStart = m.Index;
                textBox1.SelectionLength = m.Length;
                textBox1.SelectionColor = Color.Brown;
            }

            // restoring the original colors, for further writing
            textBox1.SelectionStart = originalIndex;
            textBox1.SelectionLength = originalLength;
            textBox1.SelectionColor = originalColor;

            // giving back the focus
            textBox1.Focus();
        }

        private void DoYouWantToSaveChanges()
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to save your changes?", "TextEditor", MessageBoxButtons.YesNoCancel);

            if (dialogResult == DialogResult.Yes)
            {
                SaveFileAndQuit();
            }

            if (dialogResult == DialogResult.No)
            {
                ExitProgram();
            }

            if (dialogResult == DialogResult.Cancel)
            {

            }
        }

        private void FormClosingDoYouWantToSaveChanges()
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to save your changes?", "TextEditor", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                SaveFileAndQuit();
            }
        }

        private void ExitProgram()
        {
            this.Close();
        }
    }
}
