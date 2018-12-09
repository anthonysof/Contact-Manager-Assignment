using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _4h_proairetiki
{
    public partial class SearchForm : Form
    {
        //List<Contact> contactList;
        public SearchForm()
        {
            InitializeComponent();
        }
        private List<Contact> searchForContact(List<Contact> ContactList, List<string> parameters)
        {
            List<Contact> foundContacts = new List<Contact>();
            string[] temp = { "Name", "Surname", "Phone", "Email", "Address", "Dob" };
            foreach (var item in ContactList)
            {
                bool found = false;
                int count = 0;
                foreach (var param in parameters)
                {
                    if (param == "")
                        continue;
                    foreach (var propname in temp)
                    {
                        string temp2 = Contact.GetPropValue(item, propname).ToString();
                        if (param.ToLower() == temp2.ToLower())
                        {
                            found = true;
                            break;
                        }
                        else
                        {
                            found = false;
                            continue;
                        }
                        
                    }
                    if(!found)
                    {
                        count++;
                        break;
                    }
                        
                    if (count > 1)
                    {
                        found = false;
                        break;
                    }
                }
                if (found)
                    foundContacts.Add(item);
            }
            return foundContacts;
        }

        private void SearchForm_Load(object sender, EventArgs e)
        {
           
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            List<string> searchParams = new List<string>();
            searchParams.Add(textBoxName.Text);
            searchParams.Add(textBoxSurname.Text);
            searchParams.Add(textBoxPhone.Text);
            searchParams.Add(textBoxEmail.Text);
            searchParams.Add(textBoxAddress.Text);
            searchParams.Add(textBoxDOB.Text);
            List<Contact> foundContacts = searchForContact(Form1.contactList, searchParams);
            MessageBox.Show(foundContacts.Count.ToString());
            if(foundContacts.Count == 0)
            {
                MessageBox.Show("No contacts found");
                return;
            }
            SearchResultForm searchresults = new SearchResultForm(foundContacts);
            searchresults.ShowDialog();
            buttonClear_Click(sender, e);
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxName.Clear();
            textBoxSurname.Clear();
            textBoxPhone.Clear();
            textBoxEmail.Clear();
            textBoxAddress.Clear();
            textBoxDOB.Clear();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
