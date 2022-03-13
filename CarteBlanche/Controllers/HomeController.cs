using CarteBlanche.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;


namespace CarteBlanche.Controllers
{
    public class HomeController : Controller
    {
        public static int userid;        //global variable containing user's unique id
         

        public ActionResult Index()     // Main Home Page
        {
            return View();
        } 




        [HttpGet]
        public ActionResult Register()      //Register get method
        {
            return View();
        }



        [HttpPost]
        public ActionResult Register(User u)       //Register Post Method
        {
            using (CarteBlancheContext db = new CarteBlancheContext())      //Opening, closing DB
            {
                db.Users.Add(u);        //Adding user in User db
                db.SaveChanges();

            }
           // ViewBag.register = "Registered Successfully!";
            return RedirectToAction("Login", "Home");
        }



        [HttpGet]
            public ActionResult Login()            //Login GET Method
            {
                return View();
            }


        [HttpPost]
        public ActionResult Login(User u)           //Login POST Method
        {
            try
            {
                using (CarteBlancheContext db = new CarteBlancheContext())
                {
                    
                    foreach (User x in db.Users)
                    {
                        if (x.Name.Equals(u.Name) && x.Pwd.Equals(u.Pwd))   //Authentication
                        {
                            userid = x.Id;
                            HttpContext.Session.SetString("id", x.Id.ToString());   //Session Creation
                            HttpContext.Session.SetString("name", x.Name);
                            HttpContext.Session.SetString("pwd", x.Pwd);

                            ViewBag.login = "LogIn Successfully!";


                            



                            return RedirectToAction("TodoList", "Home");
                        }
                        

                    }
                    
                    return RedirectToAction("Register", "Home");
                }

            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorPage", "Home");
            }



        }


        public ActionResult TodoList()              // View Todo list after login
        {
            List<Models.Task> todotasks;
            if (HttpContext.Session.GetString("id") == null)    //check session doesnot exist
            {
                return RedirectToAction("Login", "Home");
            }
            else                                                  // if session exsits
            {
                try
                {

                    using (CarteBlancheContext db = new CarteBlancheContext())
                    {

                        todotasks = db.Tasks.Where(x => x.Id == userid).ToList(); //Show tasks of specific user
                        

                    }
                    return View(todotasks);

                }
                catch (Exception ex)
                {
                    return RedirectToAction("ErrorPage", "Home");
                }

                
            }
            return View(todotasks);


        }

        [HttpPost]
        public ActionResult TodoList(int Id, String Title, String Label, String Priority)   
        {
            Models.Task temp = new Models.Task();
            temp.Id = userid;
            temp.Title = Title;
            temp.Label = Label;
            temp.Priority = Priority;

            
            List<Models.Task> todotasks;

            try
            {
                using (CarteBlancheContext db = new CarteBlancheContext())
                {
                    db.Tasks.Add(temp);
                    db.SaveChanges();
                    todotasks = db.Tasks.Where(x => x.Id==userid).ToList();

                }
                return View(todotasks);

            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorPage", "Home");
            }
        }



        public ActionResult delete(int id)              //delete task
        {
            if (id == 0)                
            {
                return RedirectToAction("TodoList", "Home");
            }
            try
            {
                using (CarteBlancheContext db = new CarteBlancheContext())
                {
                    Models.Task temp = db.Tasks.Find(id);           //find specific record using pkid
                    if(temp != null)
                    {
                        db.Tasks.Remove(temp);
                        db.SaveChanges();
                        return RedirectToAction("TodoList", "Home");
                    }
                    else
                    {
                        return RedirectToAction("ErrorPage", "Home");
                    }
                   
                }

            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorPage", "Home");
            }
           



        }

        public ActionResult edit(int id)                //edit task
        {
            if (id == 0)
            {
                return RedirectToAction("ErrorPage", "Home");
            }
            try
            {
                using (CarteBlancheContext db = new CarteBlancheContext())
                {
                    Models.Task temp = db.Tasks.Find(id);
                    if (temp != null)
                    {
                        
                        return View(temp);
                    }
                    else
                    {
                        return RedirectToAction("ErrorPage", "Home");
                    }

                }

            }
            catch(Exception ex)
            {
                return RedirectToAction("ErrorPage", "Home");
            }



            
        }

       [HttpPost]
       public ActionResult saveupdate(Models.Task temp)     //Saves updated/edited fields into database
        {
            try
            {
                if (temp != null)
                {
                    using (CarteBlancheContext db = new CarteBlancheContext())
                    {
                        db.Tasks.Update(temp);
                        db.SaveChanges();
                        return RedirectToAction("TodoList", "Home");

                    }
                }
                else
                {
                    return RedirectToAction("ErrorPage", "Home");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorPage", "Home");
            }
            
            
        }


        public ActionResult Logout()            //expires session
        {
            HttpContext.Session.Clear();
            return View("Login", "Home");
        }



        public ActionResult ErrorPage()         //shows if action is ambiguous
        {
            return View();
        }
        public ActionResult Contact()           //Contact details view
            {
                return View();
            }


        }
    }
