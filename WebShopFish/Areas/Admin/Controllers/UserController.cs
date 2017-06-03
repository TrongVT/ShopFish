using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShopFish.Common;

namespace WebShopFish.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        public int page = 1;
        public int size = 10;
        //
        // GET: /Admin/User/
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(User u)
        {
            var dao=new UserDAO();
            var matkhauMD5 = EncryptMD5.MahoaMD5(u.Password);
            u.Password = matkhauMD5;
            long id = dao.Insert(u);
            if (ModelState.IsValid)
            {
                if (id > 0)
                {
                    return RedirectToAction("Index","User");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm không thành công");
                }
            }
            else
            {
                ModelState.AddModelError("","Thêm không thành công");
            }
            return View("Index");
        }
        //Lấy danh sách user
        public ActionResult Index(string searchString, int page=1, int size=3)
        {
            var dao = new UserDAO();
            var model = dao.getListUser(searchString,page,size);
            ViewBag.searchString = searchString;
            return View(model);
        }
        //Cập nhật danh sách người dùng
        public ActionResult Edit(int id)
        {
            var user = new UserDAO().viewDetails(id);
            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(User u)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var dao = new UserDAO();
                    if (!string.IsNullOrEmpty(u.Password))
                    {
                        var matkhauMD5 = EncryptMD5.MahoaMD5(u.Password);
                        u.Password = matkhauMD5;
                    }
                    var user = dao.Update(u);
                    if (user)
                    {
                        return RedirectToAction("Index", "User");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Thêm thất bại rồi");
                    }
                }
                return RedirectToAction("Edit");
            }
            catch
            {
                return View("Edit");
            }
        }
        //Xóa user
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new UserDAO().Delete(id);
            return View("Index");
        }
	}
}