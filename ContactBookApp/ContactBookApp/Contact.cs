using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBookApp
{
    public class Contact
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        // This is a read-only property. It has no 'set'
        // because it's calculated from other properties.
        // This is a great example of encapsulation.
       public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }   
        }


    }
}
