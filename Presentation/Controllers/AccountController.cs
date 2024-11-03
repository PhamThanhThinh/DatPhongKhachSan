using Application.Common.Interfaces;
using Application.Common.Utility;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using Presentation.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Presentation.Controllers
{
  public class AccountController : Controller
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AccountController(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
    {
      _userManager = userManager;
      _unitOfWork = unitOfWork;
      _signInManager = signInManager;
      _roleManager = roleManager;
    }

    //[HttpGet]
    public IActionResult Login(string redirectUrl = null)
    {
      redirectUrl??= Url.Content("/");

      LoginViewModel loginViewModel = new ()
      {
        RedirectUrl = redirectUrl
      };

      return View(loginViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
      if (ModelState.IsValid)
      {
        var ketQua = await _signInManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password, loginViewModel.RememberMe, lockoutOnFailure:false);

        if (ketQua.Succeeded)
        {
          //await _signInManager.SignInAsync(, isPersistent: false);

          if (string.IsNullOrEmpty(loginViewModel.RedirectUrl))
          {
            return RedirectToAction("Index", "Home");
          }
          else
          {
            return LocalRedirect(loginViewModel.RedirectUrl);
          }
        }
        else
        {
          ModelState.AddModelError("", "Không Đăng Nhập Được. Vui Lòng Kiểm Lại Tên Đăng Nhập Và Mật Khẩu!");
        }
      }
      return View(loginViewModel);
    }

    public IActionResult Register(string returnUrl = null)
    {
      returnUrl ??= Url.Content("/");
      if (!_roleManager.RoleExistsAsync(UserRoles.Role_Admin).GetAwaiter().GetResult())
      {
        // admin quản lý trang
        _roleManager.CreateAsync(new IdentityRole(UserRoles.Role_Admin)).Wait();
        // khách hàng
        _roleManager.CreateAsync(new IdentityRole(UserRoles.Role_Customer)).Wait();
      }

      // viết code dùng để show danh sách loại tài khoản
      RegisterViewModel registerViewModel = new()
      {
        RoleList = _roleManager.Roles.Select(r => new SelectListItem
        {
          Text = r.Name,
          //Value = r.Id,
          Value = r.Name
        })
      };

      return View(registerViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
    {
      ApplicationUser applicationUser = new()
      {
        Name = registerViewModel.Name,
        Email = registerViewModel.Email,
        PhoneNumber = registerViewModel.PhoneNumber,
        UserName = registerViewModel.Email,
        CreatedDate = DateTime.Now,

        EmailConfirmed = true,
        NormalizedEmail = registerViewModel.Email.ToLower(),
        //NormalizedEmail = registerViewModel.Email.ToUpper(),

      };

      //var ketQua = _userManager.CreateAsync(applicationUser, registerViewModel.Password).GetAwaiter().GetResult();
      var ketQua = await _userManager.CreateAsync(applicationUser, registerViewModel.Password);

      if (ketQua.Succeeded)
      {
        if (!string.IsNullOrEmpty(registerViewModel.Role))
        {
          await _userManager.AddToRoleAsync(applicationUser, registerViewModel.Role);
        }
        else
        {
          await _userManager.AddToRoleAsync(applicationUser, UserRoles.Role_Customer);
        }

        await _signInManager.SignInAsync(applicationUser, isPersistent: false);

        if (string.IsNullOrEmpty(registerViewModel.RedirectUrl))
        {
          return RedirectToAction("Index", "Home");
        }
        else
        {
          return LocalRedirect(registerViewModel.RedirectUrl);
        }
      }

      foreach(var error in ketQua.Errors)
      {
        ModelState.AddModelError("", error.Description);
      }

      registerViewModel.RoleList = _roleManager.Roles.Select(r => new SelectListItem
      {
        Text = r.Name,
        Value = r.Name
      });

      return View(registerViewModel);
    }

    public async Task<IActionResult> Logout()
    {
      await _signInManager.SignOutAsync();
      return RedirectToAction("Index", "Home");
    }

    public IActionResult ForgotPassword()
    {
      return View();
    }

    // access denied
    public IActionResult AccessDenied()
    {
      return View();
    }

  }
}
