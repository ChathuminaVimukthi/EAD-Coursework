using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EADCourseworkTwo.model
{
    public class Contact
    {
        private int id;
        private string contactName;
        private int contactNumber;
        private string email;
        private int userId;

        public int Id { get => id; set => id = value; }
        public string ContactName { get => contactName; set => contactName = value; }
        public string Email { get => email; set => email = value; }
        public int UserId { get => userId; set => userId = value; }
        public int ContactNumber { get => contactNumber; set => contactNumber = value; }
    }
}
