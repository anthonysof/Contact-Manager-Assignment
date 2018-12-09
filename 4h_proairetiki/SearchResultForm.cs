using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _4h_proairetiki
{
    public partial class SearchResultForm : Form
    {
        List<Contact> foundContacts;
        public SearchResultForm(List<Contact> foundContacts)
        {
            this.foundContacts = foundContacts;
            InitializeComponent();
        }
        private void SearchResultForm_Load(object sender, EventArgs e)
        {
            string[] temp = new string[8];
            listView1.View = View.Details;
            foreach (var contact in foundContacts)
            {
                temp[0] = contact.Name;
                temp[1] = contact.Surname;
                temp[2] = contact.Phone;
                temp[3] = contact.Email;
                temp[4] = contact.Address;
                temp[5] = contact.Dob;
                temp[6] = contact.Notes;
                temp[7] = contact.Id.ToString();

                ListViewItem item = new ListViewItem(temp);
                listView1.Items.Add(item);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                return;
            ListViewItem item = listView1.SelectedItems[0];
            int id = int.Parse(item.SubItems[7].Text);
            Contact temp = foundContacts.Find(i => i.Id == id);
            if (temp.ProfilePic != null)
                pictureBox1.Image = temp.ProfilePic;
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
