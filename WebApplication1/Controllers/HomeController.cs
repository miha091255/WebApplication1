using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        AppDbContext db = new AppDbContext();

        public ActionResult Index()
        {

            IEnumerable<Test> tests = db.Test;
            IEnumerable<Theory> artilces = db.Theory;

            ViewBag.Tests = tests;
            ViewBag.Artilces = artilces;

            ViewBag.Title = "Удаленное обучение";

            return View();
        }

        public ActionResult Article(int id)
        {
            ViewBag.Message = "Теория";
            using (AppDbContext db = new AppDbContext())
            {
                Theory t = db.Theory.Find(id);
                ViewBag.Article = t;
            }
            return View();
        }

        public ActionResult Test(int id)
        {
            ViewBag.Message = "тестирование";

            try
            {
                Test t = db.Test.Find(id);
                if (t == null)
                {
                    return RedirectToAction("NotFound");
                }
                List<int> list = GetQuestList(t.list);
                List<Quest> quests = new List<Quest>();
                for (int i=0; i < list.Count; i++)
                {
                    quests.Add(db.Quests.Find(list[i]));
                }

                ViewBag.Quests = quests;
                return View();

            }
            catch (Exception)
            {
                //NotFound();
                return RedirectToAction("NotFound");
            }

        }

        public ActionResult NotFound()
        {
            ViewBag.Message = "Не удалось найти элемент";

            return View();
        }

        public List<int> GetQuestList(string Ids)
        {
            List<int> list = new List<int>();

            string[] quests = Ids.Split(',');

            foreach (var q in quests)
            {
                list.Add(Int32.Parse(q.Trim()));
            }

            return list;
        }

        public ActionResult Exercises()
        {
            if ((Session["Status"] == null)||(Session["Status"] == "Студент"))
            {
                return View("NoPermission");
            }

            IEnumerable<Test> tests = db.Test;
            IEnumerable<Theory> artilces = db.Theory;

            ViewBag.Tests = tests;
            ViewBag.Theories = artilces;

            ViewBag.Title = "Редактирование заданий";
            return View();
        }

        [HttpGet]
        public ActionResult AddTheory()
        {
            if ((Session["Status"] == null) || (Session["Status"] == "Студент"))
            {
                return View("NoPermission");
            }
            Theory model = new Theory();
            ViewBag.Title = "Добавление лекционного текста";
            return View(model);
        }

        [HttpPost]
        public ActionResult AddTheory(Theory model)
        {
            if ((Session["Status"] == null) || (Session["Status"] == "Студент"))
            {
                return View("NoPermission");
            }
            if (ModelState.IsValid)
            {
                if (model.name ==null || model.content == null)
                {
                    ModelState.AddModelError("", "Заполнены не все поля!");
                    return View(model);
                }
                using (AppDbContext db = new AppDbContext())
                {
                    db.Theory.Add(new Theory { name = model.name, content = model.content });
                    db.SaveChanges();
                    ModelState.AddModelError("", "Добавлен новый элемент в лекционные материалы");
                }
            }
            else
            {
                ModelState.AddModelError("", "Введены неверные данные");
            }
            return View(model);
        }
    }
}