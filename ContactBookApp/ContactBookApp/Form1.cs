using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContactBookApp
{
    public partial class Form1 : Form
    {
        private ContactManager _manager = new ContactManager();
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        private void RefreshContactList()
        {
            listContactList.Items.Clear();
            List<Contact> contacts = _manager.getContacts();
            foreach (Contact contact in contacts)
            {
                listContactList.Items.Add(contact.FullName);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        private bool isInputValid()
        //Insert later another condition to check if the input values are correct per field
        {
            bool isPhoneValid = string.IsNullOrWhiteSpace(txtPhone.Text);
            bool isEmailValid = string.IsNullOrWhiteSpace(txtEmail.Text);
            bool isFirstNameValid = string.IsNullOrWhiteSpace(txtFirstName.Text);
            bool isLastNameValid = string.IsNullOrWhiteSpace(txtLastName.Text);

            if (isFirstNameValid && isLastNameValid)
            {
                MessageBox.Show("First name or Last Name is required", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFirstName.Focus();
                return false;
            }
            if (isEmailValid && isPhoneValid)
            {
                MessageBox.Show("Phone or Email is required", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
                return false;
            }
            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                if (txtEmail.Text.Contains("@") == false || txtEmail.Text.Contains(".") == false)
                {
                    MessageBox.Show("Please enter a valid email address (or leave it blank).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    return false;
                }
            }
            return true;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!isInputValid())
            {
                return;
            }
            Contact newContact = new Contact();
            
            newContact.FirstName = txtFirstName.Text;
            newContact.LastName = txtLastName.Text;
            newContact.Email = txtEmail.Text;
            newContact.Phone = txtPhone.Text;
            
            _manager.AddContact(newContact);

            RefreshContactList();
            ClearInputFields();

        }
        private void ClearInputFields()
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
        }

        private void listContactList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectIndex = listContactList.SelectedIndex;
            if (selectIndex<0)
            {
                return;
            }
            List<Contact> contacts = _manager.getContacts();
            Contact selectedContact = contacts[selectIndex];
            txtFirstName.Text = selectedContact.FirstName;
            txtLastName.Text = selectedContact.LastName;
            txtEmail.Text = selectedContact.Email;
            txtPhone.Text= selectedContact.Phone;

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //select the index of the contact to be deleted in the list
            int selectedIndex = listContactList.SelectedIndex;
            if (selectedIndex < 0)
            {
                MessageBox.Show("No contact selected");
                return;
            }
            Contact contactToDelete = _manager.getContacts()[selectedIndex];

            //Prompt user for confirmation before deleting
            DialogResult result = MessageBox.Show(
            $"Are you sure you want to delete {contactToDelete.FullName}'s contact?", 
            "Confirm Delete", 
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                _manager.RemoveContact(contactToDelete);
                ClearInputFields();
                RefreshContactList();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!isInputValid())
            {
                return;
            }
            int selectedIndex = listContactList.SelectedIndex;
            if (selectedIndex < 0)
            {
                MessageBox.Show("No contact selected");
                return;
            }
            Contact contactToUpdate = _manager.getContacts()[selectedIndex];

            //Prompt user for confirmation before deleting
            DialogResult result = MessageBox.Show(
            $"Are you sure you want to update {contactToUpdate.FullName}'s details?",
            "Confirm Update",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                contactToUpdate.FirstName = txtFirstName.Text;
                contactToUpdate.LastName = txtLastName.Text;
                contactToUpdate.Email = txtEmail.Text;
                contactToUpdate.Phone = txtPhone.Text;

                MessageBox.Show("Contact details updated successfully", "Contact Update", MessageBoxButtons.OK, MessageBoxIcon.Information);

                RefreshContactList();
            }
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
