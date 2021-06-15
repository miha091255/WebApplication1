using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class UserController : Controller
    {
        [HttpPost]
        public ActionResult Authorize(UserModel model)
        {
            if (ModelState.IsValid)
            {
                string status = null;
                UserModel user = null;
                using (AppDbContext db = new AppDbContext())
                {
                    user = db.Students.FirstOrDefault(u => u.login == model.login && u.pass == model.pass);
                    status = "Студент";
                    if (user == null)
                    {
                        user = db.Teachers.FirstOrDefault(u => u.login == model.login && u.pass == model.pass);
                        status = "Учитель";
                    }
                    if (user == null)
                    {
                        user = db.Admins.FirstOrDefault(u => u.login == model.login && u.pass == model.pass);
                        status = "Администратор";
                    }                       
                }
                if (user != null)
                {
                    Session["Login"] = user.login;
                    Session["Name"] = user.name;
                    Session["Surname"] = user.surname;
                    Session["Status"] = status;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                } 
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Authorize()
        {
            UserModel model = null;
            return View(model);
        }

        public ActionResult Prophile()
        {
            ViewBag.Title = "Профиль пользователя " + Session["Login"];
            if (Session["Login"] == null)
            {
                ViewBag.Message = "Информация о данном пользователе не найдена. Обратитесь к Администратору";
                return View();
            } 
            return View();                      
        }  
        public ActionResult AllUsers()
        {
            if (Session["Status"] != "Администратор")
            {
                return View("NoPermission");
            }
            AppDbContext db = new AppDbContext();
            IEnumerable<Admin> admins = db.Admins;
            IEnumerable<Teacher> teachers = db.Teachers;
            IEnumerable<Student> students = db.Students;
            ViewBag.Admins = admins;
            ViewBag.Teachers = teachers;
            ViewBag.Students = students;
            ViewBag.Title = "Пользователи";
            return View();
        }
        [HttpGet]
        public ActionResult AddUser()
        {
            if (Session["Status"] != "Администратор")
            {
                return View("NoPermission");
            }
            UserModel model = null;
            return View(model);
        }
        [HttpPost]
        public ActionResult AddUser(UserModel model)
        {
            if (Session["Status"] != "Администратор")
            {
                return View("NoPermission");
            }
            if (ModelState.IsValid)
            {
                UserModel user = null;
                using (AppDbContext db = new AppDbContext())
                {
                    user = db.Students.FirstOrDefault(u => u.login == model.login && u.pass == model.pass);
                    if (user == null)
                        user = db.Teachers.FirstOrDefault(u => u.login == model.login && u.pass == model.pass);
                    if (user == null)
                        user = db.Admins.FirstOrDefault(u => u.login == model.login && u.pass == model.pass);
                    if (user == null)
                    {
                        if (Request.Params["role"] == "Admin")
                            db.Admins.Add(new Admin { name=model.name, surname=model.surname, login=model.login, pass=model.pass});
                        if (Request.Params["role"] == "Teacher")
                            db.Teachers.Add(new Teacher { name = model.name, surname = model.surname, login = model.login, pass = model.pass });
                        if (Request.Params["role"] == "Student")
                            db.Students.Add(new Student { name = model.name, surname = model.surname, login = model.login, pass = model.pass });
                        db.SaveChanges();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Пользователь с таким ником уже существует");
                    }
                }
            }            
            return View(model);
        }
        public ActionResult DeleteStudent(int id)
        {
            if (Session["Status"] != "Администратор")
            {
                return View("NoPermission");
            }
            Student user = null;
            using (AppDbContext db = new AppDbContext())
            {
                user = db.Students.FirstOrDefault(u => u.id == id);
                if (user != null)
                {
                    if (user.login == Session["Login"])
                    {
                        ViewBag.Message = "Нельзя удалить свою учетную запись, находясь в сети";
                        return RedirectToAction("AllUsers", "User");
                    }
                    else
                    {
                        db.Students.Remove(user);
                        db.SaveChanges();
                        return RedirectToAction("AllUsers", "User");
                    }
                }
                else
                {
                    ViewBag.Message = "Данный пользователь не найден в базе данных";
                    return RedirectToAction("AllUsers", "User");
                }
            }
        }
        public ActionResult DeleteTeacher(int id)
        {
            if (Session["Status"] != "Администратор")
            {
                return View("NoPermission");
            }
            Teacher user = null;
            using (AppDbContext db = new AppDbContext())
            {
                user = db.Teachers.FirstOrDefault(u => u.id == id);
                if (user != null)
                {
                    if (user.login == Session["Login"])
                    {
                        ViewBag.Message = "Нельзя удалить свою учетную запись, находясь в сети";
                        return RedirectToAction("AllUsers", "User");
                    }
                    else
                    {
                        db.Teachers.Remove(user);
                        db.SaveChanges();
                        return RedirectToAction("AllUsers", "User");
                    }
                }
                else
                {
                    ViewBag.Message = "Данный пользователь не найден в базе данных";
                    return RedirectToAction("AllUsers", "User");
                }
            }
        }
        public ActionResult DeleteAdmin(int id)
        {
            if (Session["Status"] != "Администратор")
            {
                return View("NoPermission");
            }
            Admin user = null;
            using (AppDbContext db = new AppDbContext())
            {
                user = db.Admins.FirstOrDefault(u => u.id == id);
                if (user != null)
                {
                    if (user.login == Session["Login"])
                    {
                        ViewBag.Message = "Нельзя удалить свою учетную запись, находясь в сети";
                        return RedirectToAction("AllUsers", "User");
                    }
                    else
                    {
                        db.Admins.Remove(user);
                        db.SaveChanges();
                        return RedirectToAction("AllUsers", "User");
                    }
                }
                else
                {
                    ViewBag.Message = "Данный пользователь не найден в базе данных";
                    return RedirectToAction("AllUsers", "User");
                }
            }
        }
    }
}