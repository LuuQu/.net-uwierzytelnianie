using uwierzytelnianie.Models;

namespace uwierzytelnianie.Interfaces
{
    public interface IPeopleService
    {
        public IQueryable<People> GetAllEntries();
        public void AddEntry(string name, string lastName, string id);
        public IQueryable<People> DeletePeople(int id);
    }
}
