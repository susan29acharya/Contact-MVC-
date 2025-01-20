using Contact__MVC_.Models;
using System.Data;
using System.Data.SqlClient;
namespace Contact__MVC_.services
{
    public class Contact : IContact
    {
        private readonly IConfiguration _configuration;

        public object Contact_Number { get; private set; }

        public Contact(IConfiguration configuration)
        {
            _configuration = configuration;
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

            using(SqlConnection conn = new SqlConnection(constring()))
            {
                SqlCommand cmd = new SqlCommand("SP_Contact", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@flag", "FetchingContact");

                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable dtable = new DataTable();

                adapter.Fill(dtable);

                foreach(DataRow dr in dtable.Rows)
                {
                    ContactData contact = new ContactData
                    { ContactId = dr["ContactId"].ToString(),
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
            using(SqlConnection conn = new SqlConnection(constring()))
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
    }
}
