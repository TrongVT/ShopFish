using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShopFish.Areas.Admin.Models;
using WebShopFish.Common;

namespace WebShopFish.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Admin/Login/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();
                var res = dao.Login(model.Username,EncryptMD5.MahoaMD5(model.Password));
                if (res==2)
                {
                    var username = dao.getByUserID(model.Username);
                    var userSession = new UserLogin();

                    userSession.USerName = username.UserName;
                    userSession.UserID = username.Id;

                    Session.Add(CommonConstrant.UserSession,userSession);
                    return RedirectToAction("Index","Home");
                }
                else if(res==0){
                    ModelState.AddModelError("", "Tài khoản không tồn tại");
                }
                else if (res == 1)
                {
                    ModelState.AddModelError("", "Tài khoảng đã bị khóa");
                }
                else if (res == 3)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng nhập thất bại");
                }
            }
            return View("Index");
           

        }
	}
}