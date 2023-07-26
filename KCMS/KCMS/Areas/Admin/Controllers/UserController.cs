using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Dao;
using Model.EF;
using KCMS.Common;
namespace KCMS.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index()
        {
            var dao = new UserDao();
            List<User> userList = dao.GetUserList();
            return View(userList);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var encrypPass = Encryptor.GetMD5Hash(user.Password);
                user.Password = encrypPass;
                long res = dao.Insert(user);
                if(res > 0)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm không thành công !");
                }             
            }
            return View("Index");
        }

        public ActionResult Edit(int id)
        {
            var dao = new UserDao();
            var res = dao.GetUserById(id);
            return View(res);
        }
    }
}