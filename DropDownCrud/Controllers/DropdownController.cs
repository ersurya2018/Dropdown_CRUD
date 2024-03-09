using DropDownCrud.DataLayer;
using DropDownCrud.Models;
using DropDownCrud.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DropDownCrud.Controllers
{
    public class DropdownController : Controller
    {
        private readonly ApplicationDbContext _context;
        private object user;

        public DropdownController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetUserList()
        {
            //var data = _context.Users.ToList();
            var data = (from u in _context.Users where u.IsActive==true
                        join cu in _context.Countries on u.CountyID equals cu.CID
                        join s in _context.States on u.StateID equals s.SId
                        join D in _context.Cities on u.CityID equals D.DistID
                        select new UserVM
                        {
                            UserID = u.UID,
                            UserName = u.UserName,
                            CountryName = cu.CountryName,
                            StateName = s.StateName,
                            CityName = D.DistricName,
                            Email = u.Email
                        }).ToList();

            return Json(data);
        }
        public IActionResult GetCountryList()
        {
            var data = _context.Countries.ToList();
            return Json(data);
        }
        public IActionResult GetStateList(int CountryID)
        {
            var data = _context.States.Where(x => x.CountyID == CountryID).ToList();
            return Json(data);
        }
        public IActionResult GetDistricList(int StateID)
        {
            var data = _context.Cities.Where(x => x.StateID == StateID).ToList();
            return Json(data);
        }
        [HttpPost]
        public IActionResult AddUser(User user)
        {
            ResponseModel resModel = new ResponseModel();
            try
            {
                ModelState.Remove("UID");
                if (!ModelState.IsValid)
                {
                    return new JsonResult(user);
                }
                else
                {
                    if(user.UID>0)
                    {
                        _context.Users.Update(user);
                        resModel.Message = "Records has been Updated Succesfully";
                    }
                    else
                    {
                        _context.Users.Add(user);
                        resModel.Message = "Records has been saved Succesfully";
                    }
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return Ok(resModel.Message = ex.Message.ToString());
            }
            return Ok(resModel);
        }
        public IActionResult GetUserDataByID(int UserID)
        {
            var data = (from u in _context.Users where u.IsActive==true
                        join cu in _context.Countries on u.CountyID equals cu.CID
                        join s in _context.States on u.StateID equals s.SId
                        join D in _context.Cities on u.CityID equals D.DistID
                        select new User
                        {
                            UID = u.UID,
                            UserName = u.UserName,
                            CountyID = u.CountyID,
                            StateID = u.StateID,
                            CityID = u.CityID,
                            Email = u.Email,
                            IsActive=u.IsActive
                        }).Where(x => x.UID == UserID).FirstOrDefault();
            return Json(data);
        }
        public IActionResult DeleteUser(int UserID)
        {
            ResponseModel resModel = new ResponseModel();
            try
            {
                var data = _context.Users.Where(x => x.IsActive == true && x.UID == UserID).FirstOrDefault();
                if(data!=null)
                {
                    data.IsActive = false;
                    _context.Update(data);
                    _context.SaveChanges();
                    resModel.Message = "User Deleted";
                }
            }
            catch(Exception ex)
            {

            }
            return Ok(resModel);
        }
    }
}
