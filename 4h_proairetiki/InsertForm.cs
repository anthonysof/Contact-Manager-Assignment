using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;

namespace _4h_proairetiki
{
    public partial class InsertForm : Form
    {
        Contact newContact = new Contact();
        public bool insertflag = false;
        public string temp;
        string ChosenFile;

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            Regex regex = new Regex("^[a-zA-Z0-9]*$");
            validateField(regex, textBoxName);
            
        }

        private void textBoxSurname_TextChanged(object sender, EventArgs e)
        {
            Regex regex = new Regex("^[a-zA-Z0-9]*$");
            validateField(regex, textBoxSurname);
        }

        private void textBoxPhone_TextChanged(object sender, EventArgs e)
        {
            Regex regexItem = new Regex("[0-9]{10}");
            validateField(regexItem, textBoxPhone);
        }

        private void textBoxEmail_TextChanged(object sender, EventArgs e)
        {
            Regex regexItem = new Regex("^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$");
            validateField(regexItem, textBoxEmail);
        }

        private void textBoxAddress_TextChanged(object sender, EventArgs e)
        {
            Regex regexItem = new Regex("[0-10a-zA-Z]");
            validateField(regexItem, textBoxAddress);
        }

        private void validateField(Regex regex, TextBox textbox)
        {
            if (regex.IsMatch(textbox.Text))
                textbox.ForeColor = Color.Green;
            else
            {
                textbox.ForeColor = Color.Red;

            }
            
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            string imagepath = "N/A";
            if ((DateTime.Now - monthCalendar1.SelectionRange.Start).TotalDays < 4745)
            {
                MessageBox.Show("You have to be over 13 years old to use this application.");
                return;
            }
            if (textBoxName.Text == "" || textBoxSurname.Text == "" || textBoxPhone.Text == "" || textBoxAddress.Text == "" || textBoxEmail.Text == "")
            {
                MessageBox.Show("Please fill the necessary fields");
                return;
            }
            if((newContact.Name = textBoxName.Text) != "" &&
            (newContact.Surname = textBoxSurname.Text) != "" &&
            (newContact.Email = textBoxEmail.Text) != "" &&
            (newContact.Phone = textBoxPhone.Text) != "" &&
            (newContact.Address = textBoxAddress.Text) != "")
            {
                newContact.Notes = richTextBoxNotes.Text;
                newContact.Dob = monthCalendar1.SelectionRange.Start.ToString("dd/MM/yyyy");
                
                if (ChosenFile != null)
                {
                    newContact.ProfilePic = Image.FromFile(ChosenFile);
                    imagepath = ChosenFile;
                }

            }
            MessageBox.Show(newContact.Name+newContact.Surname+newContact.Phone+newContact.Email+newContact.Address+newContact.Notes+newContact.Dob);
            using (StreamWriter w = File.AppendText("contacts.txt"))
            {
                w.WriteLine(newContact.Id + "|" + newContact.Name + "|" + newContact.Surname + "|" + newContact.Phone + "|" + newContact.Email + "|" + newContact.Address + "|" + newContact.Dob + "|" + newContact.Notes + "|" + imagepath + "|" + "\n");
                temp = newContact.Id + "|" + newContact.Name + "|" + newContact.Surname + "|" + newContact.Phone + "|" + newContact.Email + "|" + newContact.Address + "|" + newContact.Dob + "|" + newContact.Notes + "|" + "|" + imagepath + "|" + newContact.Id;
            }
            buttonClear_Click(sender, e);
            insertflag = true;
            this.Close();

                
            
        }

        public InsertForm()
        {
            InitializeComponent();
        }

        private void InsertForm_Load(object sender, EventArgs e)
        {
            monthCalendar1.MaxDate = DateTime.Now;
            monthCalendar1.MaxSelectionCount = 1;
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxAddress.Clear();
            textBoxName.Clear();
            textBoxSurname.Clear();
            textBoxPhone.Clear();
            textBoxEmail.Clear();
            richTextBoxNotes.Clear();
            pictureBox1.Image = null;
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Insert an Image";
            openFileDialog1.InitialDirectory = "C:";
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "JPEG Images|*.jpg|PNG Images|*.png|GIF Images|*.gif|BITMAPS|*.bmp";
            if(openFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            ChosenFile = openFileDialog1.FileName;
            pictureBox1.Image = Image.FromFile(ChosenFile);
        }
    }
}
