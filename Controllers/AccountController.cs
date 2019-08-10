﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using mvcTwitter.Models;

namespace mvcTwitter.Controllers
{

    [HandleError]
    public class AccountController : Controller
    {

        public User UsuarioLogueado
        {
            get
            {
                return Session["Usuario"] as User;

            }
            set
            {
                Session["Usuario"] = value;
            }
        }

        public IFormsAuthenticationService FormsService { get; set; }
        public IMembershipService MembershipService { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            if (FormsService == null) { FormsService = new FormsAuthenticationService(); }
            if (MembershipService == null) { MembershipService = new AccountMembershipService(); }

            base.Initialize(requestContext);
        }

        // **************************************
        // URL: /Account/LogOn
        // **************************************

        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
           
            if (ModelState.IsValid)
            {

                if (validarUser(model.UserName, model.Password))
                //    if (MembershipService.ValidateUser(model.UserName, model.Password))
                {
                    FormsService.SignIn(model.UserName, model.RememberMe);
                    if (!String.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    //}
                }
                else
                {
                    ModelState.AddModelError("", "Nombre de usuarios o password incorrecto.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public bool validarUser(string nombre, string pass)
        {


            BDTwitterEntities1 db = new BDTwitterEntities1();
            //Models.User[] usuarios = db.Users.Select("Username='" + nombre + "' ' and password='" + pass + "'").Single();
            Models.User u = new Models.User();
            u = (from c in db.Users where c.UserName == nombre && c.Password == pass select c).SingleOrDefault();

            if (u != null)
            {
                UsuarioLogueado = u;
                return true;
                // ModelState.AddModelError("", "Nombre de usuarios o password incorrecto.");
            }

            // ViewData["tweet"] = tweet;
            //   Session["User"] = ;
            return false;
        }



        // **************************************
        // URL: /Account/LogOff
        // **************************************

        public ActionResult LogOff()
        {
            FormsService.SignOut();

            return RedirectToAction("Index", "Home");
        }

        // **************************************
        // URL: /Account/Register
        // **************************************

        public ActionResult Register()
        {
            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                //   MembershipCreateStatus createStatus = MembershipService.CreateUser(model.UserName, model.Password, model.Email);

                //  if (createStatus == MembershipCreateStatus.Success)
                // {
                BDTwitterEntities1 db = new BDTwitterEntities1();

                User u = new User();
                u = (from c in db.Users where model.UserName == c.UserName select c).SingleOrDefault();

                if (u == null)
                {
                    u = new Models.User();
                    string password;
                    password = model.Password;

                    if (password.Length > 50)
                        password = password.Substring(0, 50);

                    u.UserName = model.UserName;
                    u.Password = model.Password;
                    db.AddToUsers(u);
                    db.SaveChanges();
                }

                FormsService.SignIn(model.UserName, false /* createPersistentCookie */);
                return RedirectToAction("Index", "Home");
                //}
                //else
                //{
                //    ModelState.AddModelError("", AccountValidation.ErrorCodeToString(createStatus));
                //}
            }

            // If we got this far, something failed, redisplay form
            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            return View(model);
        }

        // **************************************
        // URL: /Account/ChangePassword
        // **************************************

        [Authorize]
        public ActionResult ChangePassword()
        {
            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                if (MembershipService.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword))
                {
                    BDTwitterEntities1 db = new BDTwitterEntities1();

                    User u = new User();
                    u = (from c in db.Users where User.Identity.Name == c.UserName select c).SingleOrDefault();

                    if (u != null)
                    {
                        u.Password = model.NewPassword;
                        db.SaveChanges();
                    }

                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "Password Invalida");
                }
            }

            // If we got this far, something failed, redisplay form
            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            return View(model);
        }

        // **************************************
        // URL: /Account/ChangePasswordSuccess
        // **************************************

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

    }
}
