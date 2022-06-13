using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// 6.1 Create a separate class file to hold the four data items of the Data Structure (use the Data Structure Matrix as a guide).
// Use auto-implemented properties for the fields which must be of type “string”. Save the class as “Information.cs”.

namespace WikiApplication
{
    internal class Information : IComparable
    {
        private string Name;
        private string Category;
        private string Structure;
        private string Definition;

        public object getName { get; internal set; }

        public Information() { }

        public Information (string newName, string newCategory, string newStructure, string newDefinition)
        {
            Name = newName;
            Category = newCategory;
            Structure = newStructure;
            Definition = newDefinition;
        }


        public int CompareTo(object obj)
        {
            Information compare = obj as Information;
            return Name.CompareTo(compare.Name);
        }

        public void setName(string newName)
        {
            Name = newName;
        }


        public string GetName()
        {
            return Name;
        }

        public void setCategory(string newCategory)
        {
            Category = newCategory;
        }

        public string getCategory()
        {
            return Category;
        }

        public void setStructure(string newStructure)
        {
            Structure = newStructure;
        }

        public string getStructure()
        {
            return Structure;
        }

        public void setDefinition(string newDefinition)
        {
            Definition = newDefinition;
        }

        public string getDefinition()
        {
            return Definition;
        }

        public ListViewItem displayInfo()
        {
            ListViewItem lvi = new ListViewItem(GetName());
            lvi.SubItems.Add(getCategory());
            return lvi;


        }
    }
}

