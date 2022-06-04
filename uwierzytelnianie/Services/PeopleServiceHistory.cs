using uwierzytelnianie.Interfaces;
using Microsoft.Data.SqlClient;
using uwierzytelnianie.Data;
using uwierzytelnianie.Models;

namespace uwierzytelnianie.Services
{
    public class PeopleServiceHistory : IPeopleService
    {
        private readonly ApplicationDbContext _context;
        public PeopleServiceHistory(ApplicationDbContext context)
        {
            _context = context;
        }
        public IQueryable<People> GetAllEntries()
        {
            if(_context.People.Count() > 20)
            {
                return _context.People.OrderByDescending(c => c.Date).Take(20);
            }
            return _context.People.OrderByDescending(c => c.Date);
        }
        public void AddEntry(string name, string lastName, string id)
        {
            DateTime actualdata = DateTime.Now;
            string tmp = actualdata.ToString("MM/dd/yyyy HH:mm:ss");
            string sqlQuery = "INSERT INTO People(FirstName,LastName,Date,UserId) VALUES('" + name + "', '" + lastName + "', '" + tmp + "', '" + id + "')";
            string sqlLink = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=aspnet-uwierzytelnianie-53bc9b9d-9d6a-45d4-8429-2a2761773502;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(sqlLink);
            con.Open();
            SqlCommand sc = new SqlCommand(sqlQuery, con);
            sc.ExecuteNonQuery();
            con.Close();
        }
        public IQueryable<People> DeletePeople(int id)
        {
            People people = _context.People.FirstOrDefault(p => p.Id == id);
            if(people != null)
            {
                _context.People.Remove(people);
                _context.SaveChanges();
            }
            return _context.People;
        }
    }
}
