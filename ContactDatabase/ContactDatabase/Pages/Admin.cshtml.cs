using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using EdgeDB;

namespace ContactDatabase.Pages
{
    public class AdminModel : PageModel
    {
        private readonly EdgeDBClient _edgeDbClient;
        public List<Contact> Contacts { get; set; }

        public AdminModel(EdgeDBClient edgeDbClient)
        {
            _edgeDbClient = edgeDbClient;
        }

        public async Task OnGetAsync()
        {
            Contacts = await GetContactsAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var contact = new Contact
            {
                FirstName = Request.Form["FirstName"],
                LastName = Request.Form["LastName"],
                Email = Request.Form["Email"],
                Title = Request.Form["Title"],
                Description = Request.Form["Description"],
                DateOfBirth = DateTime.Parse(Request.Form["DateOfBirth"]),
                MarriageStatus = Request.Form.ContainsKey("MarriageStatus"),
                Username = Request.Form["Username"],
                Role = Request.Form["Role"],
            };

            var passwordHasher = new PasswordHasher<string>();
            contact.Password = passwordHasher.HashPassword(null, Request.Form["Password"]);

            await _edgeDbClient.ExecuteAsync("INSERT Contact { first_name := <str>$firstName, last_name := <str>$lastName, email := <str>$email, title := <str>$title, description := <str>$description, date_of_birth := <datetime>$dateOfBirth, marriage_status := <bool>$marriageStatus, username := <str>$username, password := <str>$password, role := <str>$role }",
                new Dictionary<string, object>
                {
                    { "firstName", contact.FirstName },
                    { "lastName", contact.LastName },
                    { "email", contact.Email },
                    { "title", contact.Title },
                    { "description", contact.Description },
                    { "dateOfBirth", contact.DateOfBirth },
                    { "marriageStatus", contact.MarriageStatus },
                    { "username", contact.Username },
                    { "password", contact.Password },
                    { "role", contact.Role },
                });

            return RedirectToPage();
        }

        private async Task<List<Contact>> GetContactsAsync()
        {
            var result = await _edgeDbClient.QueryAsync<Contact>("SELECT Contact {FirstName := .first_name, LastName := .last_name, Email := .email, Title := .title, Description := .description, DateOfBirth := .date_of_birth, MarriageStatus := .marriage_status }");
            return result.ToList();
        }
    }

    public class Contact
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool MarriageStatus { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}