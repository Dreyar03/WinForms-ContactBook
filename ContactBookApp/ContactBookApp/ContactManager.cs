using Newtonsoft.Json;       // For converting objects to text
using System;
using System.Collections.Generic;
using System.IO;             // For file handling (Reading/Writing)
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBookApp
{
    public class ContactManager
    {
        //Data storage
        private List<Contact> _contacts;

        // Define the file name. It will be created next to your .exe
        private const string FilePath = "contacts.json";

        //Constructor
        public ContactManager()
        {
            _contacts = new List<Contact>();
            LoadContacts();
        }
        public void AddContact(Contact contact)
        {
            if (contact != null)
            {
                _contacts.Add(contact);
            }
            SaveContacts();
        }
        public void RemoveContact(Contact contact)
        {
            if (contact != null)
            {
                _contacts.Remove(contact);
            }
            SaveContacts();
        }
        public List<Contact>getContacts()
        {
            return _contacts;
        }
        public void SaveContacts()
        {
            try {
                //Convert the list to a pretty text format(JSON)
                string json = JsonConvert.SerializeObject(_contacts, Formatting.Indented);
                File.WriteAllText(FilePath, json);   //Write that text to the file
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving contacts: " + ex.Message);
            }
        }

        // This reads the file and converts it back into a List.
        private void LoadContacts()
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    // Read the text from the file
                    string json = File.ReadAllText(FilePath);

                    // Convert text back into a List<Contact>
                    _contacts = JsonConvert.DeserializeObject<List<Contact>>(json);
                }
                else
                {
                    // If file doesn't exist, start with an empty list
                    _contacts = new List<Contact>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading contacts: " + ex.Message);
                _contacts = new List<Contact>();
            }
        }
    }
}
