namespace RealEstate.Areas.Admin.Controllers
{
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.Owin.Security;
    using RealEstate.App_Start;
    using RealEstate.Models.Identity;
    using RealEstate.ViewModels.Identity;
    using RealEstate.CustomAttributes;
    using RealEstate.ViewModels;
    using RealEstate.Infrastructure;
    using RealEstate.Areas.Admin.Controllers.Shared;

    public class ManageAccountController : BaseAdminController
    {
        private readonly AccountService _accountService;
        private readonly RoleService _roleService;

        private IAuthenticationManager _authenticationManager => HttpContext.GetOwinContext().Authentication;

        public ManageAccountController()
        {
            _accountService = new AccountService(new UserStore<Account>(_context));
            _roleService = new RoleService(new RoleStore<IdentityRole>(_context));
        }

        [HttpGet, GetModelState]
        public async Task<ActionResult> UserProfile(string tab)
        {
            Account user = await _accountService.FindByIdAsync(User.Identity.GetUserId());

            ViewBag.Tab = tab;
            return View(_converter.CreateProfileViewModels(user));
        }

        [HttpPost, PostModelState(nameof(UserProfile))]
        public async Task<ActionResult> ChangeInformation(ProfileViewModels model)
        {
            Account user = await _accountService.FindByIdAsync(User.Identity.GetUserId());

            user.FullName = model.Fullname;
            user.Email = model.EmailAddress;
            user.PhoneNumber = model.PhoneNumber;

            await _accountService.UpdateAsync(user);
            return RedirectToAction($"{nameof(UserProfile)}");
        }

        [HttpPost, PostModelState(nameof(UserProfile))]
        public async Task<JsonResult> ChangePassword(ChangePasswordViewModels user)
        {
            IdentityResult result = await _accountService
                .ChangePasswordAsync(User.Identity.GetUserId(), user.OldPassword, user.Password);

            if (result.Succeeded)
            {
                return Json(new
                {
                    isSuccess = true
                });
            }
            else
            {
                result.Errors.ForEach(x => AddErrors(x));
                return Json(new
                {
                    isSuccess = false,
                    error = ModelState.FirstOrDefault(x => x.Value.Errors.Any())
                            .Value.Errors.Select(y => y.ErrorMessage).FirstOrDefault()
                });
            }
        }

        [HttpPost, PostModelState(nameof(UserProfile))]
        public async Task<ActionResult> ChangeAvatar(ICollection<_ImageCropper> avatar)
        {
            Account user = await _accountService.FindByIdAsync(User.Identity.GetUserId());
            user.Avatar = SaveImage(avatar.Safe().FirstOrDefault()).ImagePath;

            await _accountService.UpdateAsync(user);
            return RedirectToAction($"{nameof(UserProfile)}");
        }

        [HttpGet, AllowAnonymous]
        [GetModelState]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [HttpPost, AllowAnonymous]
        [PostModelState, ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(AccountViewModels account, string returnUrl)
        {
            Account user = await _accountService.FindAsync(account.Username, account.Password);
            if (user == null)
            {
                AddErrors("Invalid username or password");
                return View(account);
            }
            else
            {
                ClaimsIdentity claimsIdentity = await _accountService
                    .CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

                _authenticationManager.SignOut();
                _authenticationManager.SignIn(new AuthenticationProperties
                {
                    IsPersistent = account.IsRemember
                }, claimsIdentity);

                return RedirectToLocal(returnUrl);
            }
        }

        [HttpGet]
        public ActionResult Logout()
        {
            _authenticationManager.SignOut();
            return RedirectToAction("Login");
        }
    }
}