using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactEase
{
    public class Contact
        {
        public int ContactID { get; set; }
        public int UserID { get; set; }
        public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public bool IsFavorite { get; set; }
        public string FotoPath { get; internal set; }
    }
}
