using Gimpies_Blazor1.Database.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Gimpies_Blazor1.Components.Pages.Account
{
    public partial class Login
    {
        [CascadingParameter] HttpContext context { get; set; }
        [SupplyParameterFromForm]
        public LoginViewModel Model { get; set; } = new();
        private string? errorMessage;
        private EditContext EditContext { get; set; }

        protected override void OnInitialized()
        {
            EditContext = new EditContext(Model);
        }

        private async Task Authenticate()
        {
            Console.WriteLine($"Username: {Model.Username}, Password: {Model.Password}");
            errorMessage = null; // Reset eventuele eerdere foutmeldingen

            // Valideer input - Controleer eerst of de gebruikersnaam en wachtwoord ingevuld zijn
            if (string.IsNullOrEmpty(Model.Username) || string.IsNullOrEmpty(Model.Password))
            {
                errorMessage = "Gebruikersnaam en wachtwoord zijn vereist.";

                return; // Stop verdergaan als de velden leeg zijn
            }

            // Gebruiker ophalen uit de database
            var userAccount = await dbContext.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Username == Model.Username);

            if (userAccount == null || userAccount.PasswordHashed != Model.Password)
            {
                errorMessage = "Onjuiste gebruikersnaam en/of wachtwoord.";
                return; // Stop verdergaan als gebruiker niet gevonden is of wachtwoord niet klopt
            }

            // Claims aanmaken
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, userAccount.Username),
        new Claim("Role", userAccount.Role.RoleName)
    };

            // Beleid ophalen voor de rol van de gebruiker
            var rolePolicies = await dbContext.UserPolicies
                .Where(p => p.fk_UserRoleID == userAccount.fk_UserRoleID && p.IsEnabled)
                .ToListAsync();

            foreach (var policy in rolePolicies)
            {
                claims.Add(new Claim(policy.PolicyName, "true"));
            }

            // Aanmaken van identity en principal
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

 

            // Navigeren naar de startpagina
            await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            try
            {
                navigationManager.NavigateTo("/", true);
            }
            catch (Exception ex)
            {
                navigationManager.NavigateTo("/", true);
            }


        }
    }
}