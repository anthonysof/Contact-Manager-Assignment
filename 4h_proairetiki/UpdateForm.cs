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
    public partial class UpdateForm : Form
    {
        Contact targetContact;
        public UpdateForm(Contact targetContact)
        {
            this.targetContact = targetContact;
            InitializeComponent();
        }
        string ChosenFile = "N/A";
        private void UpdateForm_Load(object sender, EventArgs e)
        {
            label10.Hide();
            updateTextBoxes();
        }

        private void updateTextBoxes()
        {
            label1.Text = targetContact.Name;
            label2.Text = targetContact.Surname;

            textBoxName.Text = targetContact.Name;
            textBoxSurname.Text = targetContact.Surname;
            textBoxPhone.Text = targetContact.Phone;
            textBoxEmail.Text = targetContact.Email;
            textBoxAddress.Text = targetContact.Address;
            textBoxDob.Text = targetContact.Dob;
            richTextBox1.Text = targetContact.Notes;
            pictureBox1.Image = targetContact.ProfilePic;
        }
        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

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

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            string[] finaltext;
            string[] temp;
            if (ChosenFile != "N/A")
                targetContact.ProfilePic = Image.FromFile(ChosenFile);
            using (var streamReader = File.OpenText("contacts.txt"))
            {
                var lines = streamReader.ReadToEnd().Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                string[] finaltextarr = new string[lines.Length];
                if(lines.Length > 0)
                {
                    int i = 0;
                    foreach(var line in lines)
                    {
                        finaltextarr[i] = line;
                        temp = line.Split('|');
                        if (temp[0] != targetContact.Id.ToString())
                        {
                            i++;
                            continue;
                        }
                        temp[1] = textBoxName.Text;
                        temp[2] = textBoxSurname.Text;
                        temp[3] = textBoxPhone.Text;
                        temp[4] = textBoxEmail.Text;
                        temp[5] = textBoxAddress.Text;
                        temp[6] = textBoxDob.Text;
                        temp[7] = richTextBox1.Text;
                        
                        if (ChosenFile != "N/A")
                        {
                            temp[8] = ChosenFile;
                            targetContact.ProfilePic = Image.FromFile(temp[8]);
                        }
                            
                            
                        targetContact.Name = temp[1];
                        targetContact.Surname = temp[2];
                        targetContact.Phone = temp[3];
                        targetContact.Email = temp[4];
                        targetContact.Address = temp[5];
                        targetContact.Dob = temp[6];
                        targetContact.Notes = temp[7];
                        finaltextarr[i] = string.Join("|", temp);
                        i++;
                        
                    }
                }
                finaltext = finaltextarr;
            }
            File.WriteAllLines("contacts.txt", finaltext);
            updateTextBoxes();
            label10.Show();
        }

        private void buttonChange_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Insert an Image";
            openFileDialog1.InitialDirectory = "C:";
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "JPEG Images|*.jpg|PNG Images|*.png|GIF Images|*.gif|BITMAPS|*.bmp";
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            ChosenFile = openFileDialog1.FileName;
            pictureBox1.Image = Image.FromFile(ChosenFile);
        }
    }
}
