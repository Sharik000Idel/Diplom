using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;
using WebApplication1.Models;
using WebApplication1.Models.Data;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        

        public IActionResult Profile()
        {
            var mySessionValue = HttpContext.Session.GetString("UserId");
            if (mySessionValue != null)
            {
                diplomdbContext diplomdbContext = new diplomdbContext();
                Console.WriteLine(diplomdbContext.Roles.Count());
                User user = diplomdbContext.Users.First(x => x.IdUsers == Convert.ToInt32(mySessionValue));
                Role rol=  diplomdbContext.Roles.First(r => r.IdRole == user.IdRole);
                Commenttext commenttext = diplomdbContext.Commenttexts.First(x => x.IdCommentText == user.IdCommentText);
                ViewBag.UserInfo = user;
                ViewBag.UserStatusInfo = rol;
                ViewBag.UserCommentAbout = commenttext;
                return View();
            }
            return RedirectToAction("AuthRegistr");
            
        }

        
        public IActionResult LogOut()
        {
            
                HttpContext.Session.Remove("UserId");
            return RedirectToAction("Index");
        }
        public IActionResult Registration()
        {
            
            return RedirectToAction("Editing");
        }
        


        public async Task<IActionResult> RegistrationForm(
        string name,
        string surname,
        string lastName,
        string Birthday,
        string Email,
        string password,
        int IdRole,
        string commentAbout,
        decimal ecimation = 0.0m)
        {
            diplomdbContext diplomdbContext = new diplomdbContext();
            if (diplomdbContext.Users.Where(e=>e.Email == Email).ToList().Count == 0 )
            {
                int newIdCommentText = diplomdbContext.Commenttexts.ToList().OrderBy(p => p.IdCommentText).Last().IdCommentText + 1;
                Commenttext commenttext = new Commenttext
                {
                    IdCommentText = newIdCommentText,
                    Text = commentAbout
                };
                diplomdbContext.Commenttexts.Add(commenttext);
                User addUser = new User
                {
                    IdUsers = diplomdbContext.Users.ToList().OrderBy(p => p.IdUsers).Last().IdUsers + 1,
                    Name = name,
                    Surname = surname,
                    Lastname = lastName,
                    Birthday = DateOnly.ParseExact(Birthday, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    Email = Email,
                    Login = Email,
                    Password = password,
                    IdRole = IdRole,
                    IdCommentText = newIdCommentText,
                    Estimation = ecimation
                };
                diplomdbContext.Users.Add(addUser);
                diplomdbContext.SaveChanges();
            }
            


            return RedirectToAction("LogIn");
        }





        public IActionResult Editing()
        {
            var mySessionValue = HttpContext.Session.GetString("UserId");
            if (mySessionValue != null)
            {
                diplomdbContext diplomdbContext = new diplomdbContext();
                User user = diplomdbContext.Users.First(x => x.IdUsers == Convert.ToInt32(mySessionValue));
                Role rol = diplomdbContext.Roles.First(r => r.IdRole == user.IdRole);
                Commenttext commenttext = diplomdbContext.Commenttexts.First(x => x.IdCommentText == user.IdCommentText);
                ViewBag.UserInfo = user;
                ViewBag.UserStatusInfo = rol;
                ViewBag.UserCommentAbout = commenttext;
                
            }
            return View();
        }

        public async Task<IActionResult> EditingProfile(
        int id,
        string name,
        string surname,
        string lastName,
        string Birthday,
        string Email,
        string password,
        int IdRole,
        int IdCommentAbout,
        string commentAbout
        )
        {
            diplomdbContext diplomdbContext = new diplomdbContext();

            Commenttext commenttext = diplomdbContext.Commenttexts.First(c => c.IdCommentText == IdCommentAbout);
            commenttext.Text = commentAbout;
            User user = diplomdbContext.Users.First(c => c.IdUsers == id);
            diplomdbContext.Commenttexts.Add(commenttext);


            user.Name = name;
                    user.Surname = surname;
                    user.Lastname = lastName;
                    user.Birthday = DateOnly.ParseExact(Birthday, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    user.Email = Email;
                    user.Login = Email;
                    user.Password = password;
                    user.IdRole = IdRole;

            diplomdbContext.Update(user);
            diplomdbContext.Update(commenttext);
            await diplomdbContext.SaveChangesAsync();




            return RedirectToAction("Profile");
        }

        public IActionResult Mytrips()
        {
            return View();
        }
        public IActionResult Select()
        {
            diplomdbContext diplomdbContext = new diplomdbContext();
            if (TempData["BeginRoute"] != null &&
                TempData["EndRoute"] != null &&
                TempData["date"] != null)
            {

                string BeginRoute = TempData["BeginRoute"].ToString();
                string EndRoute = TempData["EndRoute"].ToString();
                string date = TempData["date"].ToString();
                List<Models.Data.Route> route = diplomdbContext.Routes.Where(r => r.BeginRoute.Trim() == BeginRoute.Trim() && r.EndRoute.Trim() == EndRoute.Trim()).ToList();
                List<User> user = diplomdbContext.Users.Where(x=> route.Select(t=>t.IdUser).Contains(x.IdUsers)).ToList();

                foreach(Models.Data.Route r in route)
                {
                    r.IdUserNavigation.Name = user.First(t=> t.IdUsers == r.IdUser).Name;
                    r.IdUserNavigation.Estimation = user.First(t => t.IdUsers == r.IdUser).Estimation;
                }

                ViewBag.routes = route;
                ViewBag.users = user;

            }

            return View();

        }
        public IActionResult Reviews()
        {
            return View();
        }
        public IActionResult AuthRegistr()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Search(string BeginRoute, string EndRoute, string date)
        {
            
            TempData["BeginRoute"] = BeginRoute;
            TempData["EndRoute"] = EndRoute;
            TempData["date"] = date;
            

            return RedirectToAction("Select");
        }

        public async Task<IActionResult> LogIn(string email, string password)
        {
            diplomdbContext diplomdbContext = new diplomdbContext();
            if (diplomdbContext.Users.FirstOrDefault(u=>u.Email == email && u.Password == password) != null)
            {
                User us = diplomdbContext.Users.First(u => u.Email == email && u.Password == password);
                HttpContext.Session.SetString("UserId", us.IdUsers.ToString());
                
                var mySessionValue = HttpContext.Session.GetString("UserId");
                Console.WriteLine(mySessionValue);
                return RedirectToAction("Profile");
            }
            return RedirectToAction("AuthRegistr");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        

    }
}