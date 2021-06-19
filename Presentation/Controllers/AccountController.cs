using DataAccess.Design_Pattern.UnitOfWork;
using DataAccess.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilities.Convertors;
using Utilities.Genarator;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Presentation.Controllers
{
    public class AccountController : Controller
    {
        #region Constructor
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUnitOfWork _context;

        public AccountController(UserManager<User> userManager,
            SignInManager<User> signInManager,IUnitOfWork context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        #endregion

        #region Register

        [Route("/Register")]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [Route("/Register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {



                if (_context.userRepository.IsExistUserName(model.UserName))
                {
                    ModelState.AddModelError("", "این نام کاربری توسط فرد دیگری انتخاب شده است ");
                    return View(model);
                }
                if (_context.userRepository.IsExistEmail(FixedText.FixEmail(model.Email)))
                {
                    ModelState.AddModelError("", "این ایمیل  توسط فرد دیگری انتخاب شده است");
                    return View(model);
                }
                if (_context.userRepository.IsExistPhoneNumber(FixedText.FixEmail(model.PhoneNumber)))
                {
                    ModelState.AddModelError("PhoneNumber", "شماره تلفن وارد شده توسط فرد دیگری انتخاب شده است  ");
                    return View(model);
                }
                var user = new User()
                {
                    UserName = model.UserName,
                    PhoneNumber = model.PhoneNumber,
                    Email = FixedText.FixEmail(model.Email),
                    EmailConfirmed = true,
                    RegisterDate = DateTime.Now,
                    UserAvatar = "Defult.jpg",
                    IsActive = true,
                    IsDelete = false,
                    ActiveCode = RandomNumberGenerator.GetNumber(),

                };

                var result = await _userManager.CreateAsync(user, model.Password);

                List<string> requestRoles = new List<string>();
                requestRoles.Add("User");

                var reslt = await _userManager.AddToRolesAsync(user, requestRoles);


                if (result.Succeeded)
                {
                    return Redirect("/Login?Register=true");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }
            return View(model);
        }


        #endregion


        #region Login
        [HttpGet]
        [Route("/Login")]

        public IActionResult Login(string returnUrl = null , bool EditProfile = false, bool Register = false, bool recovery = false, bool permission = false)
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("Index", "Home");


            ViewBag.EditProfile = EditProfile;
            ViewBag.permission = permission;
            ViewBag.Register = Register;
            ViewBag.recovery = recovery;

            ViewData["returnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [Route("/Login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("Index", "Home");
            ViewData["returnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);

                if (!user.IsActive)
                {
                    ModelState.AddModelError("", "حساب کاربری شما فعال نمی باشد");
                    return View(model);
                }


                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, true);




                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);

                    ViewBag.IsSuccess = true;
                    return View();
                }

                if (result.IsLockedOut)
                {
                    ViewData["ErrorMessage"] = "اکانت شما به دلیل پنج بار ورود ناموفق به مدت پنج دقیق قفل شده است";
                    return View(model);
                }

                ModelState.AddModelError("", "رمزعبور یا نام کاربری اشتباه است");
            }
            return View(model);
        }



        #endregion

        #region LogOut

        [Route("/LogOut")]

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        #endregion

    }
}
