using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using PagedList;
namespace Model.DAO
{
    public class UserDAO
    {
        ShopFishDbContext db=null;
        public UserDAO()
        {
            db = new ShopFishDbContext();
        }
        public long Insert(User entity)
        {
            db.Users.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }
        //Cập nhật người dùng
        public bool Update(User u)
        {
            try
            {
                var user = db.Users.Find(u.Id);
                if (!string.IsNullOrEmpty(u.Password))
                {
                    user.Password = u.Password;
                }

                user.Name = u.Name;
                user.Address = u.Address;
                user.Email = u.Email;
                user.Phone = u.Phone;
                user.ModifiedBy = u.ModifiedBy;
                user.ModifiedDate = DateTime.Now;
                user.Status = u.Status;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
            
        }
        //Lấy danh sách user
        public IEnumerable<User> getListUser(string searchString, int page, int sizePage)
        {
            IQueryable<User> model = db.Users;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.UserName.Contains(searchString) || x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x=>x.CreateDate).ToPagedList(page,sizePage);
        }

        //Đăng nhập user
        public int Login(string user, string pass)
        {
            var res = db.Users.SingleOrDefault(x=>x.UserName==user);
            if (res ==null)
            {
                return 0;
            }
            else
            {
                if (res.Status == false)
                {
                    return 1;
                }
                else
                    if (res.Password == pass)
                    {
                        return 2;
                    }
                    else
                    {
                        return 3;
                    }
            }
        }
        //get by id user
        public User getByUserID(string username)
        {
            return db.Users.SingleOrDefault(x=>x.UserName==username);
        }
        public User viewDetails(int id)
        {
            return db.Users.Find(id);
        }
        //Xóa User
        public bool Delete(int id)
        {
            try
            {
                var user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }
    }
}
