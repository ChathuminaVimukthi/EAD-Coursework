using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EADCourseworkTwo.model
{
    class Contact
    {
        private string id;
        private string contactName;
        private string email;
        private string userId;

        public string Id { get => id; set => id = value; }
        public string ContactName { get => contactName; set => contactName = value; }
        public string Email { get => email; set => email = value; }
        public string UserId { get => userId; set => userId = value; }
    }
}
