using Microsoft.AspNetCore.Mvc.RazorPages;
using EdgeDB;

namespace ContactDatabase.Pages
{
    public class UserModel : PageModel
    {
        private readonly EdgeDBClient _edgeDbClient;
        public List<Contact> Contacts { get; set; }

        public UserModel(EdgeDBClient edgeDbClient)
        {
            _edgeDbClient = edgeDbClient;
        }

        public async Task OnGetAsync()
        {
            Contacts = await GetContactsAsync();
        }

        private async Task<List<Contact>> GetContactsAsync()
        {
            var result = await _edgeDbClient.QueryAsync<Contact>(
                "SELECT Contact {FirstName := .first_name, LastName := .last_name, Email := .email, Title := .title, Description := .description, DateOfBirth := .date_of_birth, MarriageStatus := .marriage_status }");
            return result.ToList();
        }
    }
}