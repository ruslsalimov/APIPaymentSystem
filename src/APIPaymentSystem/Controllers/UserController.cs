using APIPaymentSystem.Models;
using APIPaymentSystem.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIPaymentSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Produces("application/json")]
    public class UserController : Controller
    {
        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;
        ListErrors listErrors = new ListErrors();

        public UserController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreateAccount([FromBody] NewUser newUser)
        {
            listErrors.Errors = new List<Error>();
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    UserName = newUser.Name,
                    Email = newUser.Email
                };
                IdentityResult result = await userManager.CreateAsync(user, newUser.Password);

                if (result.Succeeded)
                    return Ok(new { status = "Account was created successfully" });
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        listErrors.Errors.Add(new Error() { Status = "400", Title = error.Description });
                    }
                    return BadRequest(listErrors.Errors);
                }
            }
            else
            {
                listErrors.Errors.Add(new Error() { Status = "400", Title = "invalid model" });
                return BadRequest(listErrors.Errors);
            }
        }

        [HttpPost]
        public async Task<ActionResult<string>> Login(LoginUser details)
        {
            listErrors.Errors = new List<Error>();
            if (ModelState.IsValid)
            {
                AppUser user = await userManager.FindByEmailAsync(details.Email);
                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(user, details.Password, false, false);
                    if (result.Succeeded)
                        return Ok(new { status = "Authorization was successful" });
                    else
                    {
                        listErrors.Errors.Add(new Error() { Status = "400", Title = "Invalid user or password" });
                        return BadRequest(listErrors.Errors);
                    }
                }
                else
                {
                    listErrors.Errors.Add(new Error() { Status = "400", Title = "Invalid login or password" });
                    return BadRequest(listErrors.Errors);
                }
            }
            else
            {
                listErrors.Errors.Add(new Error() { Status = "400", Title = "invalid model" });
                return BadRequest(listErrors.Errors);
            }
        }
    }
}
