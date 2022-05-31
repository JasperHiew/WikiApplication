using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// 6.1 Create a separate class file to hold the four data items of the Data Structure (use the Data Structure Matrix as a guide).
// Use auto-implemented properties for the fields which must be of type “string”. Save the class as “Information.cs”.
namespace WikiApplications
{
    class Information
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Structure { get; set; }
        public string Definition { get; set; }

        public void setName (string newName)
        {
            Name = newName;
        }

        public string getName()
        {
            return Name;
        }
        
        public void setCategory (string newCategory)
        {
            Category = newCategory;
        }
        
        public string getCategory ()
        {
            return Category;
        }

        public void setStructure (string newStructure)
        {
            Structure = newStructure;   
        }

        public string getStructure()
        {
            return Structure;
        }
        
        public void setDefinition (string newDefinition)
        {
            Definition = newDefinition; 
        }

        public string getDefinition()
        {
            return Definition;
        }

        public string displayInfo()
        {
            return getName();         
        }
    }
}
