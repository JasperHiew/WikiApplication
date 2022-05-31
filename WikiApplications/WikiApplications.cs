using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics; // Trace
using System.IO;

// Jasper Hiew
// 03/05/2022
// An advanced Wiki application program
//
namespace WikiApplications
{
    public partial class WikiApplications : Form
    {
        public WikiApplications()
        {
            InitializeComponent();
        }

        // 6.2 Create a global List<T> of type Information called Wiki.
        List<Information> Wiki = new List<Information>();

        public void displayArray()
        {
            listViewOutput.Items.Clear();
            foreach (Information info in Wiki)
            {
                // Use the subItems from your WikiDataSearch
                //Items.Add(information.displayInfo());
                //.Items;
            }
        }

        // 6.12 Create a custom method that will clear and reset the TextBboxes, ComboBox and Radio button
        public void clearTexts()
        {
            textBoxName.Clear();
            comboBoxCategory.Items.Clear(); // Unselects one of the options (Might not work if so delete)
            textBoxDefinition.Clear();
            textBoxSearch.Clear();
        }
        // Radio Button (Might not work so delete later if it doesn't)
        private String getSelected()
        {
            if(radioButtonLinear.Checked)
            {
                return "Linear";
            } 
            else if (radioButtonNonLinear.Checked) 
            {
                return "Non Linear";
            }
            return "None";
        }
        

        // 6.3 Create a button method to ADD a new item to the list. Use a TextBox for the Name input, ComboBox for the Category,
        // Radio group for the Structure and Multiline TextBox for the Definition.
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            //Wiki.Add(textBoxName.Text);
            //Wiki.Add(radioButtonLinear.Checked);
            //Wiki.Add(textBoxDefinition.Text);
            Information newInfo = new Information();
            newInfo.setName(textBoxName.Text);
            newInfo.setCategory(comboBoxCategory.Text.ToString());
            //newInfo.setStructure(radioButtonLinear.Checked.ToString() || radioButtonNonLinear.Checked.ToString());
            newInfo.setDefinition(textBoxDefinition.Text);
            Wiki.Add(newInfo);
            clearTexts();
            displayArray();
        }

        //6.7 Create a button method that will delete the currently selected record in the ListView.
        //Ensure the user has the option to backout of this action by using a dialog box.
        // Display an updated version of the sorted list at the end of this process.
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listViewOutput.SelectedIndices[0] == -1)
            {

            } 
            else
            {

            }
            //foreach (ListViewItem eachItem in listViewOutput.SelectedItems)
            //{
            //    listViewOutput.Items.Remove(eachItem);
            //}

            //listViewOutput.SelectedItems[0].Remove();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            //Wiki[listViewOutput.SelectedIndex] = textBoxName.Text;
        }

        // 6.10 Create a button method that will use the builtin binary search to find a Data Structure name.
        // If the record is found the associated details will populate the appropriate input controls and highlight the name in the ListView.
        // At the end of the search process the search input TextBox must be cleared.
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            Information item = new Information();
            item.setName(textBoxSearch.Text);
            int found = Wiki.BinarySearch(item);
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {   
            if (File.Exists("Wiki.dat"))
            { 
                using (var stream = File.Open("Wiki.dat", FileMode.Open))
                {
                    using (var reader = new BinaryReader(stream, Encoding.UTF8, false))
                    {
                        Wiki.Clear();
                        while (stream.Position < stream.Length)
                        {
                            Information info = new Information();
                            info.setName(reader.ReadString());
                            info.setCategory(reader.ReadString()); // Comboboxes probably don't work
                            info.setStructure(reader.ReadString()); // Maybe the groupbox doesn't work in this case
                            info.setDefinition(reader.ReadString());
                            Wiki.Add(info);
                        }
                    }
                }
                displayArray();
            }
        }
        // 6.8 Create a button method that will save the edited record of the currently selected item in the ListView.
        // All the changes in the input controls will be written back to the list.
        // Display an updated version of the sorted list at the end of this process
        private void buttonSave_Click(object sender, EventArgs e)
        {
            using (var stream = File.Open("Wiki.dat", FileMode.Create))
            {
                using (var writer = new BinaryWriter(stream, Encoding.UTF8, false))
                {
                    foreach (var x in Wiki)
                    {
                        writer.Write(x.getName());
                        writer.Write(x.getCategory()); // Comboboxes probably don't work
                        writer.Write(x.getStructure()); // Maybe the groupbox doesn't work in this case
                        writer.Write(x.getDefinition());
                    }
                }
            }
        }

        //6.5 Create a custom ValidName method which will take a parameter string value from the Textbox Name and returns a Boolean after checking for duplicates.
        // Use the built in List<T> method “Exists” to answer this requirement.
        private bool ValidName(string checkThisName)
        {
            if (Wiki.Exists(duplicate => duplicate.Equals(checkThisName)))
                return false;
            else
                return true;



            //bool duplicate = false;
            ////for (int i = 0; i < row; i++)
            ////{
            ////    if (string.Equals(WikiList[i, 0], textBoxName.Text)
            ////        && string.Equals(WikiList[i, 1], groupBoxStructure.)
            ////}

            //return duplicate;
        }

        private void WikiApplications_Load(object sender, EventArgs e)
        {
            listViewOutput.View = View.Details;

            radioButtonLinear.Checked = true;
        }
    }
}
