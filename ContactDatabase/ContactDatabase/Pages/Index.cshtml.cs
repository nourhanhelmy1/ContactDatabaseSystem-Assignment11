using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using EdgeDB;

namespace ContactDatabase.Pages
{
    public class IndexModel : PageModel
    {
        private readonly EdgeDBClient _edgeDbClient;

        public IndexModel(EdgeDBClient edgeDbClient)
        {
            _edgeDbClient = edgeDbClient;
        }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; private set; }
        public bool HasError => !string.IsNullOrEmpty(ErrorMessage);

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await GetUserByUsernameAsync(Username);

            if (user == null)
            {
                ErrorMessage = "Invalid Username";
                return Page();
            }

            var passwordHasher = new PasswordHasher<string>();
            var passwordVerification = passwordHasher.VerifyHashedPassword(null, user.Password, Password);

            if (passwordVerification != PasswordVerificationResult.Success)
            {
                ErrorMessage = "Incorrect Password";
                return Page();
            }

            if (user.Role == "User")
            {
                return RedirectToPage("/User");
            }
            else
            {
                return RedirectToPage("/Admin");
            }

        }

        private async Task<Contact> GetUserByUsernameAsync(string username)
        {
            var result = await _edgeDbClient.QueryAsync<Contact>(
                "SELECT Contact {FirstName := .first_name, LastName := .last_name, Email := .email, Title := .title, Description := .description, DateOfBirth := .date_of_birth, MarriageStatus := .marriage_status, Role := .role, Password := .password} FILTER .username = <str>$username",
                new Dictionary<string, object> { { "username", username } });

            var user = result.FirstOrDefault();

            return user;
        }
    }
}