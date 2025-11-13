using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBookApp
{
    public class ContactManager
    {
        //Data storage
        private List<Contact> _contacts;

        //Constructor
        public ContactManager()
        {
            _contacts = new List<Contact>();
        }
        public void AddContact(Contact contact)
        {
            if (contact != null)
            {
                _contacts.Add(contact);
            }
        }
        public void RemoveContact(Contact contact)
        {
            if (contact != null)
            {
                _contacts.Remove(contact);
            }
        }
        public List<Contact>getContacts()
        {
            return _contacts;
        }
    }
}
