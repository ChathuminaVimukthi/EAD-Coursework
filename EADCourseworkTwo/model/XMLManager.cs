using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace EADCourseworkTwo.model
{
    class XMLManager
    {
        private static string path = @"C:\Users\kavin\Downloads\Cw2_w1628067_WadumesthriChathuminaVimukthi\EADCourseworkTwo\EADCourseworkTwo\Resources\User.xml";
    
        public static void writeUsersToXml(User user)
        {
            UserModel userModel = new UserModel();
            List<User> userList = userModel.getAllUsers();
            using (var writer = new FileStream(path, FileMode.Create))
            {
                XmlSerializer ser = new XmlSerializer(typeof(List<User>),
                    new XmlRootAttribute("user_list"));
                ser.Serialize(writer, userList);
                ser.Serialize(Console.Out, userList);

            }
        }
        
        public static List<User> readUserFromXml()
        {
            List<User> userList;
            using (var reader = new StreamReader(path))
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(List<User>),
                    new XmlRootAttribute("user_list"));
                return userList = (List<User>)deserializer.Deserialize(reader);
            }
        }

        public static Boolean validateUserFromXml(string userName, string password)
        {
            List<User> userList = new List<User>();
            foreach (User obj in readUserFromXml())
            {
                if (obj.UserName.Equals(userName))
                {
                    if (PasswordHandler.Validate(password, obj.Password))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }
    }
}
