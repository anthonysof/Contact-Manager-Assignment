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
using Newtonsoft.Json;

namespace _4h_proairetiki
{
    public partial class Form1 : Form
    {
        public static List<Contact> contactList;
        InsertForm insertform;
        public Form1()
        {
            InitializeComponent();
        }

        private void updateListView()
        {
            contactList.Clear();
            string[] temp = new string[8];

            using (var streamReader = File.OpenText("contacts.txt"))
            {

                var lines = streamReader.ReadToEnd().Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (lines.Length > 0)
                {
                    foreach (var line in lines)
                    {
                        temp = line.Split('|');
                        Contact newContact = new Contact();
                        newContact.Name = temp[1];
                        newContact.Surname = temp[2];
                        newContact.Phone = temp[3];
                        newContact.Email = temp[4];
                        newContact.Address = temp[5];
                        newContact.Dob = temp[6];
                        newContact.Notes = temp[7];
                        if (temp[8] != "N/A")
                            newContact.ProfilePic = Image.FromFile(temp[8]);
                        if(!contactList.Contains(newContact))
                            contactList.Add(newContact);
                        ListViewItem item = new ListViewItem(temp);
                        listView1.Items.Add(item);
                    }
                }
            }
            //Contact.Count = contactList[contactList.Count - 1].Id;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            listView1.View = View.Details;
            if (!File.Exists("contacts.txt"))
                File.Create("contacts.txt");

            contactList = new List<Contact>();
            updateListView();
            buttonUpdate.Hide();
            buttonDelete.Hide();
            

        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            insertform = new InsertForm();
            insertform.ShowDialog();
            Contact newContact = new Contact();
            string[] temp = new string[8];
            if(insertform.insertflag)
            {
                temp = insertform.temp.Split('|');
                ListViewItem item = new ListViewItem(temp);
                listView1.Items.Add(item);
                newContact.Name = temp[1];
                newContact.Surname = temp[2];
                newContact.Phone = temp[3];
                newContact.Email = temp[4];
                newContact.Address = temp[5];
                newContact.Dob = temp[6];
                newContact.Notes = temp[7];
                contactList.Add(newContact);
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            SearchForm searchform = new SearchForm();
            searchform.ShowDialog();
        }
        Contact selectedContact;
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                return;
            ListViewItem item = listView1.SelectedItems[0];
            int id = int.Parse(item.SubItems[0].Text);
            label1.Text = id.ToString();
            selectedContact = contactList.Find(i => i.Id == id);
            if (selectedContact.ProfilePic != null)
                pictureBox1.Image = selectedContact.ProfilePic;
            buttonUpdate.Show();
            buttonDelete.Show();

        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            UpdateForm updateform = new UpdateForm(selectedContact);
            updateform.ShowDialog();
            listView1.Items.Clear();
            updateListView();
        }
    }
}
