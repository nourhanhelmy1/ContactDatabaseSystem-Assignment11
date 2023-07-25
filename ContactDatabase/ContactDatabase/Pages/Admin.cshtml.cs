using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using EdgeDB;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

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

        [BindProperty]
        public Contact NewContact { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var passwordHasher = new PasswordHasher<string>();
            NewContact.Password = passwordHasher.HashPassword(null, NewContact.Password);

            await _edgeDbClient.ExecuteAsync("INSERT Contact { first_name := <str>$first_name, last_name := <str>$last_name, email := <str>$email, title := <str>$title, description := <str>$description, date_of_birth := <datetime>date_of_birth, marriage_status := <bool>marriage_status, username := <str>$username, password := <str>$password, role := <str>$role }",
                new Dictionary<string, object>
                {
                    { "first_name", NewContact.FirstName },
                    { "last_name", NewContact.LastName },
                    { "email", NewContact.Email },
                    { "title", NewContact.Title },
                    { "description", NewContact.Description },
                    { "date_of_birth", NewContact.DateOfBirth },
                    { "marriage_status", NewContact.MarriageStatus },
                    { "username", NewContact.Username },
                    { "password", NewContact.Password },
                    { "role", NewContact.Role },
                });

            return RedirectToPage();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostUpdateAsync([FromBody] JsonElement data)
        {
            var serializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var contact = JsonSerializer.Deserialize<Contact>(data.GetRawText(), serializerOptions);

            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                }

                return BadRequest("Invalid data");
            }

            var query = "UPDATE Contact FILTER .username = <str>$Username SET { first_name := <str>$FirstName, last_name := <str>$LastName, email := <str>$Email, title := <str>$Title, description := <str>$Description, date_of_birth := <datetime>$DateOfBirth, marriage_status := <bool>$MarriageStatus, role := <str>$Role }";
            await _edgeDbClient.ExecuteAsync(query, new Dictionary<string, object?>
            {
                    { "first_name", NewContact.FirstName },
                    { "last_name", NewContact.LastName },
                    { "email", NewContact.Email },
                    { "title", NewContact.Title },
                    { "description", NewContact.Description },
                    { "date_of_birth", NewContact.DateOfBirth },
                    { "marriage_status", NewContact.MarriageStatus },
                    { "username", NewContact.Username },
                    { "password", NewContact.Password },
                    { "role", NewContact.Role },
                });

            return RedirectToPage();
        }

        private async Task<List<Contact>> GetContactsAsync()
        {
            var result = await _edgeDbClient.QueryAsync<Contact>("SELECT Contact {FirstName := .first_name, LastName := .last_name, Email := .email, Title := .title, Description := .description, DateOfBirth := .date_of_birth, MarriageStatus := .marriage_status, Username := .username }");
            return result.ToList();
        }
    }

    public class Contact
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        public bool MarriageStatus { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
