using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;

namespace WikiApplication
{
    public partial class WikiApplication : Form
    {
        public WikiApplication()
        {
            InitializeComponent();

            Stream traceFiles = File.Create("TraceFile.txt");
            TextWriterTraceListener traceListener = new TextWriterTraceListener(traceFiles);
            Trace.Listeners.Add(traceListener);
            Trace.AutoFlush = true;
            Trace.WriteLine("Trace debug commencing...");
            Trace.WriteLine("");
        }

        // 6.2 Create a global List<T> of type Information called Wiki.
        List<Information> Wiki = new List<Information>();

        // 6.4 Create and initialise a global string array with the six categories as indicated in the Data Structure Matrix.
        // Create a custom method to populate the ComboBox when the Form Load method is called.
        string[] categoryWiki = new string[] { "Array", "Abstract", "Graph", "Hash", "List", "Tree" };

        string defaultFileName = "definitions.bin";





        // 6.9 Create a single custom method that will sort and then display the Name and Category from the wiki information in the list.
        public void displayArray()
        {
            Wiki.Sort();
            listViewOutput.Items.Clear();
            listViewOutput.ForeColor = Color.Black;
            foreach (var information in Wiki)
            {
                listViewOutput.Items.Add(information.displayInfo());
            }
            Trace.WriteLine("Sorted and Displaying");
        }

        private void resetHighlight()
        {
            for (int i = 0; i < Wiki.Count; i++)
            {
                listViewOutput.Items[i].ForeColor = Color.Black;
                listViewOutput.Items[i].BackColor = Color.White;
            }
            Trace.WriteLine("Highlight reset");
        }
        // 6.12 Create a custom method that will clear and reset the TextBboxes, ComboBox and Radio button
        private void clearTexts()
        {
            textBoxName.Clear();
            comboBoxCategory.SelectedIndex = -1;
            radioButtonLinear.Checked = false;
            radioButtonNonLinear.Checked = false;
            textBoxDefinition.Clear();
            textBoxSearch.Clear();
            Trace.WriteLine("Textboxes = Cleared");
        }

        // 6.5 Create a custom ValidName method which will take a parameter string value from the Textbox Name and returns a Boolean after checking for duplicates.
        // Use the built in List<T> method “Exists” to answer this requirement.
        private bool ValidName(String checkThisName)
        {
            if (!Wiki.Exists(dup => dup.GetName() == checkThisName))
            {
                Trace.WriteLine("ValidName = true");
                return true;
            }
            else
            {
                Trace.WriteLine("ValidName = false");
                MessageBox.Show("Invalid name. Cannot have the same name in list twice.");
                return false;
            }
        }

        // 6.6 Create two methods to highlight and return the values from the Radio button GroupBox.
        // The first method must return a string value from the selected radio button (Linear or Non-Linear). 
        // Radio Button (Might not work so delete later if it doesn't)
        private String radioButtonSelected()
        {
            //string value = "";
            if (radioButtonLinear.Checked)
            {
                Trace.WriteLine("RadioButtonLinear = Checked");
                //value = radioButtonLinear.Text;
                return "Linear";
            }
            else
            {
                Trace.WriteLine("RadioButtonNonLinear = Checked");
                //radioButtonNonLinear.Checked = true;
                //value = radioButtonNonLinear.Text;
                return "Non-Linear";
            }
        }

        // The second method must send an integer index which will highlight an appropriate radio button.
        private void radioButtonHighlight(int radio)
        {
            if (radio == 0)
            {
                Trace.WriteLine("RadioButton = Checked");
                radioButtonLinear.Checked = true;
            }
            else
            {
                Trace.WriteLine("RadioButton = Not Checked");
                radioButtonNonLinear.Checked = true;
            }
        }
        // 6.4 Create and initialise a global string array with the six categories as indicated in the Data Structure Matrix.
        // Create a custom method to populate the ComboBox when the Form Load method is called.
        private void fillComboBox()
        {
            for (int i = 0; i < categoryWiki.Length; i++)
            {
                categoryWiki[i].ToString();
            }
            comboBoxCategory.Items.AddRange(categoryWiki);
        }
        // 6.11 Create a ListView event so a user can select a Data Structure Name from the list of Names and
        // the associated information will be displayed in the related text boxes combo box and radio button.
        private void listViewOutput_Click(object sender, EventArgs e)
        {
            int currentWiki = listViewOutput.SelectedIndices[0];
            Trace.WriteLine("Current Index: " + currentWiki + " is clicked");
            textBoxName.Text = Wiki[currentWiki].GetName();
            comboBoxCategory.Text = Wiki[currentWiki].getCategory();
            if (Wiki[currentWiki].getStructure() == "Linear")
            {
                radioButtonHighlight(0);
                Trace.WriteLine("RadioButtonLinear = Checked");
            }
            else
            {
                radioButtonHighlight(1);
                Trace.WriteLine("RadioButtonNonLinear = Checked");
            }
            textBoxDefinition.Text = Wiki[currentWiki].getDefinition();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBoxName.Text) &&
                !string.IsNullOrWhiteSpace(textBoxDefinition.Text))
            {
                var dialogResult = MessageBox.Show("Add new wiki record?", "Add wiki record", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.OK)
                {
                    if (ValidName(textBoxName.Text))
                    {
                        resetHighlight();
                        Information newInfo = new Information();
                        newInfo.setName(textBoxName.Text);
                        newInfo.setCategory(comboBoxCategory.Text);
                        newInfo.setStructure(radioButtonSelected());
                        newInfo.setDefinition(textBoxDefinition.Text);
                        Wiki.Add(newInfo);

                        clearTexts();
                        displayArray();

                    }
                }
                else
                {
                    MessageBox.Show("Record NOT added.");
                }
            }
            else
            {
                MessageBox.Show("Please fill out the necessary boxes on the left-side to add a record.");
            }
        }

        // 6.7 Create a button method that will delete the currently selected record in the ListView.
        // Ensure the user has the option to backout of this action by using a dialog box. Display an updated version of the sorted list at the end of this process.
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            resetHighlight();
            try
            {
                int deleteWiki = listViewOutput.SelectedIndices[0];
                Trace.WriteLine("deleteWiki(Current Item): " + deleteWiki);
                if (deleteWiki >= 0)
                {
                    DialogResult dialogResult = MessageBox.Show("Would you like to delete this wiki record",
                           "Click 'Yes' to proceed with the deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        Wiki.RemoveAt(deleteWiki);

                        clearTexts();
                        displayArray();


                    }
                    else
                    {
                        MessageBox.Show("Wiki record has NOT been deleted.");
                    }
                }
            } 
            catch (Exception)
            {
                MessageBox.Show("Please select wiki record to delete.");
                clearTexts();
            }
        }

        // 6.8 Create a button method that will save the edited record of the currently selected item in the ListView.
        // All the changes in the input controls will be written back to the list. Display an updated version of the sorted list at the end of this process.
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            try
            {
                resetHighlight();
                int editWiki = listViewOutput.SelectedIndices[0];
                if (editWiki >= 0)
                {
                    var dialogResult = MessageBox.Show("Update current wiki record?", "Edit wiki record",
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                    if (dialogResult == DialogResult.OK)
                    {
                        if (ValidName(textBoxName.Text))
                        {
                            Wiki[editWiki].setName(textBoxName.Text);
                            Wiki[editWiki].setCategory(comboBoxCategory.Text);
                            Wiki[editWiki].setStructure(radioButtonSelected());
                            Wiki[editWiki].setDefinition(textBoxDefinition.Text);


                            clearTexts();
                            displayArray();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Current wiki record has NOT been updated.");
                    }
                }
            } 
            catch (Exception)
            {
                MessageBox.Show("Select a wiki record from the list to edit.");
                clearTexts();
            }
        }

        // 6.10 Create a button method that will use the builtin binary search to find a Data Structure name.
        // the record is found the associated details will populate the appropriate input controls and highlight the name in the ListView.
        // At the end of the search process the search input TextBox must be cleared.
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            resetHighlight();

            if (!string.IsNullOrEmpty(textBoxSearch.Text))
            {
                Information found = new Information();
                found.setName(textBoxSearch.Text);
                int binarySearch = Wiki.BinarySearch(found);

                if (binarySearch >= 0)
                {
                    listViewOutput.Focus();
                    listViewOutput.Items[binarySearch].BackColor = Color.Blue;
                    listViewOutput.Items[binarySearch].ForeColor = Color.White;

                    MessageBox.Show(textBoxSearch.Text + " Found.");
                    clearTexts();
                } 
                else
                {
                    MessageBox.Show(textBoxSearch.Text + " was not found.");
                    clearTexts();
                }
            }
            else
            {
                MessageBox.Show("Please input a name to search for in the current wiki records.");
            }
        }

        // 6.14 Create two buttons for the manual open and save option; this must use a dialog box to select a file or rename a saved file.
        // All Wiki data is stored/retrieved using a binary file format.
        private void buttonOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Application.StartupPath;
            openFileDialog.Filter = "BIN Files|*.bin";
            openFileDialog.Title = "Select a Bin File";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                openRecord(openFileDialog.FileName);
                displayArray();
            }
            Trace.WriteLine("Opening bin file");
        }

        public void openRecord (string openFileName)
        {
            try
            {
                using (Stream stream = File.Open(openFileName, FileMode.Open))
                {
                    using (var reader = new BinaryReader(stream, Encoding.UTF8, false)) 
                    {
                        Wiki.Clear();
                        while (stream.Position < stream.Length)
                        {
                            Information openWiki = new Information();
                            openWiki.setName(reader.ReadString());
                            openWiki.setCategory(reader.ReadString());
                            openWiki.setStructure(reader.ReadString());
                            openWiki.setDefinition(reader.ReadString());
                            Wiki.Add(openWiki);
                        }
                    }
                }
            }
            catch (IOException e)
            {
                MessageBox.Show("Error. Can't open file");
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "bin file|*.bin";
            saveFileDialog.Title = "Save a bin File";
            saveFileDialog.InitialDirectory = Application.StartupPath;
            saveFileDialog.DefaultExt = "bin";
            saveFileDialog.ShowDialog();

            string fileName = saveFileDialog.FileName;
            if (saveFileDialog.FileName != "")
            {
                saveRecord(fileName);
            }
            else
            {
                saveRecord(defaultFileName);
            }
            Trace.WriteLine("Save bin file");
        }

        private void saveRecord(string saveFileName)
        {
            try
            {
                using (var stream = File.Open(saveFileName, FileMode.Create))
                {
                    using (var writer = new BinaryWriter(stream, Encoding.UTF8, false))
                    {
                        foreach (var x in Wiki)
                        {
                            writer.Write(x.GetName());
                            writer.Write(x.getCategory());
                            writer.Write(x.getStructure());
                            writer.Write(x.getDefinition());
                        }
                    }
                }
            } 
            catch (IOException e)
            {
                MessageBox.Show("Error. File NOT Saved.");
            }
        }

        private void WikiApplication_Load(object sender, EventArgs e)
        {
            fillComboBox();

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Application.StartupPath;
            openFileDialog.Filter = "BIN Files|*.bin";
            openFileDialog.Title = "Select a BIN File";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (var stream = File.Open(openFileDialog.FileName, FileMode.Open))
                {
                    using (var reader = new BinaryReader(stream, Encoding.UTF8, false))
                    {
                        Wiki.Clear();
                        while (stream.Position < stream.Length)
                        {
                            Information readWiki = new Information();
                            readWiki.setName(reader.ReadString());
                            readWiki.setCategory(reader.ReadString());
                            readWiki.setStructure(reader.ReadString());
                            readWiki.setDefinition(reader.ReadString());
                            Wiki.Add(readWiki);
                        }
                    }
                }
                Trace.WriteLine("Wiki application records opening");
                displayArray();
            }

        }

        // 6.13 Create a double click event on the Name TextBox to clear the TextBboxes, ComboBox and Radio button.
        private void textBoxName_DoubleClick(object sender, EventArgs e)
        {
            clearTexts();
        }

        // 6.15 The Wiki application will save data when the form closes. 
        private void WikiApplication_FormClosing(object sender, FormClosingEventArgs e)
        {
            saveRecord(defaultFileName);
        }
    }
}
