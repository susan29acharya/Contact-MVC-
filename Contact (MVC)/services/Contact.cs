using Contact__MVC_.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace Contact__MVC_.services
{
    public class Contact : IContact
    {
        private readonly IConfiguration _configuration;
        public readonly IWebHostEnvironment _webenvironment;
        public Contact(IConfiguration configuration, IWebHostEnvironment webenvironment)
        {
            _configuration = configuration;
            _webenvironment = webenvironment;
        }
        public string constring()
        {
            var response = _configuration.GetConnectionString("dbcs");
            return response;
        }
        public bool AddContact(ContactData contact)
        {
            int res;
            using (SqlConnection conn = new SqlConnection(constring()))
            {
                SqlCommand cmd = new SqlCommand("SP_Contact", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ContactNumber", contact.ContactNumber);
                cmd.Parameters.AddWithValue("@ContactName", contact.ContactName);
                cmd.Parameters.AddWithValue("@flag", "AddContact");

                conn.Open();
                res = cmd.ExecuteNonQuery();
                conn.Close();

                return res > 0;
            };
        }
        public List<ContactData> GetContacts()
        {
            List<ContactData> ContactList = new List<ContactData>();

            using (SqlConnection conn = new SqlConnection(constring()))
            {
                SqlCommand cmd = new SqlCommand("SP_Contact", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@flag", "FetchingContact");

                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable dtable = new DataTable();

                adapter.Fill(dtable);

                foreach (DataRow dr in dtable.Rows)
                {
                    ContactData contact = new ContactData
                    {
                        ContactId = dr["ContactId"].ToString(),
                        ContactName = dr["ContactName"].ToString(),
                        ContactNumber = dr["ContactNumber"].ToString()
                    };
                    ContactList.Add(contact);
                }
                return ContactList;
            };
        }
        public bool DeleteContact(ContactData contact)
        {
            using (SqlConnection conn = new SqlConnection(constring()))
            {
                SqlCommand cmd = new SqlCommand("SP_Contact", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ContactId", contact.ContactId);
                cmd.Parameters.AddWithValue("@flag", "DeleteContact");

                conn.Open();
                int res = cmd.ExecuteNonQuery();
                conn.Close();

                return res > 0;

            }
        }
        public List<ContactData> GetContactById(ContactData contact)
        {
            List<ContactData> ContactList = new List<ContactData>();
            using (SqlConnection conn = new SqlConnection(constring()))
            {
                SqlCommand cmd = new SqlCommand("SP_Contact", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ContactId", contact.ContactId);
                cmd.Parameters.AddWithValue("@flag", "GetContactById");

                conn.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable dtable = new DataTable();

                adapter.Fill(dtable);

                foreach (DataRow dr in dtable.Rows)
                {
                    ContactData contactData = new ContactData
                    {
                        ContactName = dr["ContactName"].ToString(),
                        ContactNumber = dr["ContactNumber"].ToString(),
                    };
                    ContactList.Add(contactData);
                };
                return ContactList;
            }

        }
        public bool UpdateContact(ContactData contact)
        {
            using (SqlConnection conn = new SqlConnection(constring()))
            {
                SqlCommand cmd = new SqlCommand("SP_Contact", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ContactName", contact.ContactName);
                cmd.Parameters.AddWithValue("@ContactNumber", contact.ContactNumber);
                cmd.Parameters.AddWithValue("@ContactId", contact.ContactId);
                cmd.Parameters.AddWithValue("@flag", "UpdateContact");

                conn.Open();
                var res = cmd.ExecuteNonQuery();
                conn.Close();

                return res > 0;
            }
        }
        public bool UploadImage(ContactData contact)
        {
            var filename = Path.GetFileName(contact.formFile.FileName);
            var path = Path.Combine(_webenvironment.WebRootPath, "Images", filename);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                contact.formFile.CopyTo(stream);
            }

            contact.ImagePath = Path.Combine("Images", filename);

            using (SqlConnection conn = new SqlConnection(constring()))
            {
                SqlCommand cmd = new SqlCommand("SP_Contact", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ImagePath", contact.ImagePath);
                cmd.Parameters.AddWithValue("@ContactId", contact.ContactId);
                cmd.Parameters.AddWithValue("@flag", "ImageUpload");
                conn.Open();
                var res = cmd.ExecuteNonQuery();
                conn.Close();

                return res > 0;
            }
        }
        public List<ContactData> Imagefetched(ContactData contact)
        {
            List<ContactData> Imagepaths = new List<ContactData>();
            using (SqlConnection conn = new SqlConnection(constring()))
            {
                SqlCommand cmd = new SqlCommand("SP_Contact", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ContactId", contact.ContactId);
                cmd.Parameters.AddWithValue("@flag", "ImageFetched");

                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable dtable = new DataTable();

                adapter.Fill(dtable);

                foreach (DataRow dr in dtable.Rows)
                {
                    contact.ImagePath = dr["ImagePath"].ToString();
                }
                Imagepaths.Add(contact);
            }
            return Imagepaths;
        }
    }
}
