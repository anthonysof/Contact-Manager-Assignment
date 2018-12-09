using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Drawing;

namespace _4h_proairetiki
{
    public class Contact
    {
        private string name, surname, phone, email, dob, address, notes;
        private Image profilePic;
        private static int count = 0;
        private int id;
        Regex regexItem;

        public Contact()
        {
            id = Count++;
        }
        public string Name {
            get => name;
            set
            {
                regexItem = new Regex("[a-zA-Z0-9]*");
                if (regexItem.IsMatch(value))
                    name = value;
                else
                    System.Windows.Forms.MessageBox.Show("No special characters in names!");
            }
        }
        public string Surname
        {
            get => surname;
            set
            {
                regexItem = new Regex("[a-zA-Z0-9]*");
                if (regexItem.IsMatch(value))
                    surname = value;
                else
                    System.Windows.Forms.MessageBox.Show("No special characters in names!");
            }
        }
        public string Phone { get => phone;
            set
            {
                regexItem = new Regex("[0-9]{10}");
                if (!regexItem.IsMatch(value))
                    System.Windows.Forms.MessageBox.Show("Input 10 Digits only");
                else
                    phone = value;
            }
        }
        public string Email { get => email;
            set
            {
                regexItem = new Regex("^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$");
                if (regexItem.IsMatch(value))
                    email = value;
                else
                    System.Windows.Forms.MessageBox.Show("Invalid email format.");
            }
        }
        public string Dob { get => dob; set => dob = value; }
        public string Address { get => address; set => address = value; }
        public string Notes { get => notes; set => notes = value; }
        public Image ProfilePic { get => profilePic; set => profilePic = value; }
        public int Id { get => id; set => id = Count; }
        public static int Count { get => count; set => count = value; }

        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
    }
}
