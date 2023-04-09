using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;
using WebApplication1.Models;
using WebApplication1.Models.Data;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

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
                return RedirectToAction("LogIn");
            }
            TempData["EmailTrue"] = "";

            
            return RedirectToAction("Editing");
        }





        public IActionResult Editing()
        {
            if (TempData["EmailTrue"] != null)
            {
                ViewData["EmailTrue"] = "Пользователь с таким Email существеут";
            }
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
            diplomdbContext diplomdbContext = new diplomdbContext();
            List<Userroute> userroutes = diplomdbContext.Userroutes.Where(c => c.IdUser == Convert.ToInt32(HttpContext.Session.GetString("UserId"))).ToList();
            foreach (Userroute userroute in userroutes)
            {
                userroute.IdUserNavigation = diplomdbContext.Users.First(f => f.IdUsers == Convert.ToInt32(HttpContext.Session.GetString("UserId")));
                userroute.IdRoutNavigation = diplomdbContext.Routes.First(f => f.IdRout == userroute.IdRout);
                userroute.IdRoutNavigation.IdUserNavigation = diplomdbContext.Users.First(f => userroute.IdRoutNavigation.IdUser == f.IdUsers);
                userroute.IdRoutNavigation.IdUserNavigation.Car = diplomdbContext.Cars.First(f => userroute.IdRoutNavigation.IdUserNavigation.CarId == f.IdCar);
            }
            ViewBag.userroutes = userroutes;
            

            return View();

        }
        
        public IActionResult Select()
        {
            diplomdbContext diplomdbContext = new diplomdbContext();
            if (TempData["BeginRoute"] != null &&
                TempData["EndRoute"] != null)
                
            {
                
                string BeginRoute = TempData["BeginRoute"].ToString();
                string EndRoute = TempData["EndRoute"].ToString();
                DateTime date = TempData["date"]!=null ? Convert.ToDateTime(TempData["date"]) : DateTime.Now;
                List<Models.Data.Route> route = diplomdbContext.Routes.Where(r => r.BeginRoute.Trim() == BeginRoute.Trim() && r.EndRoute.Trim() == EndRoute.Trim()).ToList();
                List<User> user = diplomdbContext.Users.Where(x=> route.Select(t=>t.IdUser).Contains(x.IdUsers)).ToList();

                foreach(Models.Data.Route r in route)
                {
                    r.IdUserNavigation.Name = user.First(t=> t.IdUsers == r.IdUser).Name;
                    r.IdUserNavigation.Estimation = user.First(t => t.IdUsers == r.IdUser).Estimation;
                }
                route = route.OrderBy(p => Math.Abs((date - p.DataTimeStart.Date).TotalSeconds)).ToList();
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

        public IActionResult CarAdd()
        {
            var mySessionValue = HttpContext.Session.GetString("UserId");
            
                diplomdbContext diplomdbContext = new diplomdbContext();
                if (diplomdbContext.Users.First(x => x.IdUsers == Convert.ToInt32(mySessionValue)).CarId != null)
                {
                    Car a = diplomdbContext.Cars.
                        First(p => p.IdCar == diplomdbContext.Users.First(c => c.IdUsers == Convert.ToInt32(mySessionValue)).CarId);
                a.IdCommentCarNavigation= diplomdbContext.Commenttexts.First(x => x.IdCommentText == a.IdCommentCar);
                    ViewBag.Car = a;


                }
            return View();
        }

        public IActionResult NewRoute()
        {
            return View();
        }

        

        public async Task<IActionResult> CreateRoteForm(string BeginRoute, string EndRoute, DateTime date, int cost, int CountPass, string text)
        {
            diplomdbContext diplomdbContext = new diplomdbContext();
            Commenttext commenttext = new Commenttext
            {
                IdCommentText = diplomdbContext.Commenttexts.OrderBy(o => o.IdCommentText).Last().IdCommentText + 1,
                Text = text
            };

            Models.Data.Route route = new Models.Data.Route
            {
                IdRout = diplomdbContext.Routes.OrderBy(o => o.IdRout).Last().IdRout + 1,
                IdUser = Convert.ToInt32(HttpContext.Session.GetString("UserId")),
                BeginRoute = BeginRoute,
                EndRoute = EndRoute,
                DataTimeStart = date,
                Cost = cost,
                CountPassagir = CountPass,
                IdStatusRoute = 1,
                IdCommentText = commenttext.IdCommentText
            };
            diplomdbContext.Commenttexts.Add(commenttext);
            diplomdbContext.Routes.Add(route);
            await diplomdbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> СhangeCarAdd(int id, string Gosnumber, string Namecar,int idtext, string textCar)
        {
            diplomdbContext diplomdbContext = new diplomdbContext();

            int newIdCommentText = diplomdbContext.Commenttexts.ToList().OrderBy(p => p.IdCommentText).Last().IdCommentText + 1;

            Console.WriteLine(diplomdbContext.Cars.Count());
            Commenttext commenttexts = diplomdbContext.Commenttexts.First(t => t.IdCommentText == idtext);
            commenttexts.Text = textCar;
            diplomdbContext.Update(commenttexts);
            Car editCar = diplomdbContext.Cars.First(t => t.IdCar == id);
            editCar.GosNumber = Gosnumber;
            editCar.NameCar = Namecar;
            diplomdbContext.Update(editCar);
            await diplomdbContext.SaveChangesAsync();

            return RedirectToAction("Profile");
        }

        public async Task<IActionResult> NewCarAdd(string Gosnumber, string Namecar , string textCar)
        {
            diplomdbContext diplomdbContext = new diplomdbContext();

            int newIdCommentText = diplomdbContext.Commenttexts.ToList().OrderBy(p => p.IdCommentText).Last().IdCommentText + 1;

            Console.WriteLine(diplomdbContext.Cars.Count());
            diplomdbContext.Commenttexts.Add(new Commenttext
            {
                IdCommentText = newIdCommentText,
                Text = textCar
            });

            Car NewCar = new Car
            {
                IdCar = diplomdbContext.Cars.ToList().OrderBy(p => p.IdCar).Last().IdCar + 1,
                GosNumber = Gosnumber,
                NameCar = Namecar,
                IdCommentCar = newIdCommentText
            };
            diplomdbContext.Cars.Add(NewCar);
            User user = diplomdbContext.Users.First(u => u.IdUsers == Convert.
            ToInt32(HttpContext.Session.GetString("UserId")));
            user.CarId = NewCar.IdCar;
            user.IdRole = 2;
            diplomdbContext.SaveChanges();
            return RedirectToAction("Profile");
        }

        [HttpPost]
        public async Task<IActionResult> Search(string BeginRoute, string EndRoute, string date)
        {
            
            TempData["BeginRoute"] = BeginRoute;
            TempData["EndRoute"] = EndRoute;
            TempData["date"] = date;
            

            return RedirectToAction("Select");
        }

        public IActionResult ViewingRoute(int id)
        {
            //var mySessionValue = HttpContext.Session.GetString("UserId");
            //if (mySessionValue != null)
            //{
            //    //diplomdbContext diplomdbContext = new diplomdbContext();
            //    //Console.WriteLine(diplomdbContext.Roles.Count());
            //    //User user = diplomdbContext.Users.First(x => x.IdUsers == Convert.ToInt32(mySessionValue));
            //    //Role rol = diplomdbContext.Roles.First(r => r.IdRole == user.IdRole);
            //    //Commenttext commenttext = diplomdbContext.Commenttexts.First(x => x.IdCommentText == user.IdCommentText);
            //    //ViewBag.UserInfo = user;
            //    //ViewBag.UserStatusInfo = rol;
            //    //ViewBag.UserCommentAbout = commenttext;
            //    //return View();
            //}
            ////return RedirectToAction("AuthRegistr");

            
            if (id != 0)
            {
                Console.WriteLine("first" + id);
                diplomdbContext diplomdbContext = new diplomdbContext();
                WebApplication1.Models.Data.Route route = diplomdbContext.Routes.First(p => p.IdRout == id);
                Console.WriteLine("second" + id);
                ViewBag.Route = route;
                return View();
            }else
            {
                return RedirectToAction("Select");
            }
            
            
        }

        public async Task<IActionResult> AddInRoutePassager(int id)
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {

            
            diplomdbContext diplomdbContext = new diplomdbContext();
            diplomdbContext.Userroutes.Add(new Userroute
            {
                IdUserroutes = diplomdbContext.Userroutes.OrderBy(o => o.IdUserroutes).Last().IdUserroutes + 1,
                IdRout = id,
                IdUser = Convert.ToInt32(HttpContext.Session.GetString("UserId")),
                StatusUserRouteId = 3
            });
            await diplomdbContext.SaveChangesAsync();
            return RedirectToAction("Select");
            }
            else
            {
                return RedirectToAction("AuthRegistr");
            }
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