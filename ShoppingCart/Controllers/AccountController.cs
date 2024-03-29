﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Models;
using ShoppingCart.Models.ViewModels;

namespace ShoppingCart.Controllers
{
    // Controlador para la creación de cuentas y login/out
    public class AccountController : Controller
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // Función para crear un nuevo usuario con Nombre, Email y Contraseña
        public IActionResult CrearUsuario() => View();

        [HttpPost]
        public async Task<IActionResult> CrearUsuario(Usuario usuario) 
        {
            // Creamos el usuario
            AppUser nuevoUsuario = new AppUser { UserName = usuario.UserName, Email = usuario.Email };
            IdentityResult result = await _userManager.CreateAsync(nuevoUsuario, usuario.Password);

            // Si el usuario se ha creado sin problemas redirigimos a la página del Admin
            if (result.Succeeded)
            {
                return Redirect("/admin/productos");
            }

            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(usuario);
        }

        // Función del login del usuario
        public IActionResult Login(string returnUrl) => View(new LoginViewModel { ReturnUrl = returnUrl });

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (ModelState.IsValid)
            { 
                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(loginVM.UserName, loginVM.Password, false, false);
            
                if (result.Succeeded)
                {
                    return Redirect(loginVM.ReturnUrl ?? "/Admin");
                }

                ModelState.AddModelError("", "Usuario o contraseña inválidos");
            }

            return View(loginVM);
        }

        // Función del logout del usuario
        public async Task<RedirectResult> Logout(string returnUrl = "/") 
        {
            await _signInManager.SignOutAsync();

            return Redirect(returnUrl);
        }
    }
}
