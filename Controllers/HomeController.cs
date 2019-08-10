using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvcTwitter.Models;
using System.Data.Objects;


namespace mvcTwitter.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {

        public User UsuarioLogueado
        {
            get
            {
                return Session["Usuario"] as User;

            }
            set
            {
                Session["Usuario"] = value;
            }
        }

        public ActionResult Index()
        {
            BDTwitterEntities1 db = new BDTwitterEntities1();
            IEnumerable<Tweet> tweet = null;
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    UsuarioLogueado = db.Users.Where(c => c.UserName == User.Identity.Name).FirstOrDefault();
                    ViewData["tweet"] = tweet;

                    tweet = (from i in db.Tweets
                             orderby i.PostDate descending
                             select i);
                    return View(tweet.ToList());
                }
            }
            catch (Exception) { }
            
            return View(tweet);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Usuario()
        {
            BDTwitterEntities1 bd = new BDTwitterEntities1();

            List<Models.User> usuarios = new List<User>();
            if (User.Identity.IsAuthenticated)
            {
                UsuarioLogueado = bd.Users.Where(c => c.UserName == User.Identity.Name).FirstOrDefault();
                usuarios = bd.get_NotFollowed(Convert.ToInt32(UsuarioLogueado.id)).ToList();
                //ViewData["listaUsers"] = usuarios;
                ViewData["MensageUser"] = "";
            }
            else
            {
                ViewData["MensajeUser"] = "FAVOR INICIE SESION";
            }
            return View(usuarios);
        }

        public ActionResult follow()
        {
            BDTwitterEntities1 bd = new BDTwitterEntities1();
            List<Models.User> usuarios = new List<User>();

            if (User.Identity.IsAuthenticated)
            {
                UsuarioLogueado = bd.Users.Where(c => c.UserName == User.Identity.Name).FirstOrDefault();
                usuarios = bd.getFollowedUsers(UsuarioLogueado.id).ToList();
                //ViewData["listaUsers"] = usuarios;
                ViewData["MensageUser"] = "";
            }
            else
            {
                ViewData["MensajeUser"] = "FAVOR INICIE SESION";
            }


            return View(usuarios);
        }

        public ActionResult PostPage(string name)
        {
            List<Tweet> tweets = new List<Tweet>();
            BDTwitterEntities1 bd = new BDTwitterEntities1();
            var tweet = from t in bd.Tweets where t.User.UserName == name && t.PostDate < DateTime.Now orderby t.PostDate descending select t;
            ViewData["tweet"] = tweet.ToList();
            return View("PostPage", tweet.ToList());

        }


        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Tweet nuevo)
        {
            BDTwitterEntities1 db = new BDTwitterEntities1();
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    //Tweet nuevo = new Tweet();
                    nuevo.PostDate = DateTime.Now;
                    //nuevo.Post = collection["Post"];
                    nuevo.User = db.Users.Where(c => c.UserName == User.Identity.Name).FirstOrDefault();
                    db.AddToTweets(nuevo);
                    db.SaveChanges();

                    List<Tweet> tweets = new List<Tweet>();
                    BDTwitterEntities1 bd = new BDTwitterEntities1();
                    string NombreUsuario = User.Identity.Name;
                    var tweet = from t in bd.Tweets where t.User.UserName == NombreUsuario && t.PostDate < DateTime.Now orderby t.PostDate descending select t;
                    ViewData["tweet"] = tweet.ToList();
                    return View("Feed", tweet.ToList());
                }
                            }
            catch (Exception e) { }

            return View("Index",null);
        }

        public ActionResult Delete(Int32 id)
        {
            BDTwitterEntities1 bd = new BDTwitterEntities1();
            List<Models.User> usuarios = new List<User>();

            if (User.Identity.IsAuthenticated)
            {
                UsuarioLogueado = bd.Users.Where(c => c.UserName == User.Identity.Name).FirstOrDefault();
                bd.sp_deleteFollow(UsuarioLogueado.id, id);

                usuarios = bd.get_NotFollowed(UsuarioLogueado.id).ToList();
                //ViewData["listaUsers"] = usuarios;
                ViewData["MensageUser"] = "";
            }
            else
            {
                ViewData["MensajeUser"] = "FAVOR INICIE SESION";
            }

            return View("Usuario", usuarios);
        }

        public ActionResult Post(string post)
        {
            if (User.Identity.IsAuthenticated)
            {
                BDTwitterEntities1 db = new BDTwitterEntities1();

                var user = db.Users.Where(u => u.UserName == User.Identity.Name).Single();

                Tweet tweet = new Tweet();
                tweet.Post = post;
                tweet.PostDate = DateTime.Now;

                user.Tweets.Add(tweet);

                db.SaveChanges();
            }
            return RedirectToAction("Feed");
        }

        public ActionResult Feed()
        {

            List<Tweet> tweets = new List<Tweet>();
            BDTwitterEntities1 bd = new BDTwitterEntities1();
            string NombreUsuario = User.Identity.Name;
            var tweet = from t in bd.Tweets where t.User.UserName == NombreUsuario && t.PostDate < DateTime.Now orderby t.PostDate descending select t;
            ViewData["tweet"] = tweet.ToList();
            return View("Feed", tweet.ToList());

        }

        public ActionResult seguirUser(int followed)
        {

            BDTwitterEntities1 bd = new BDTwitterEntities1();
            mvcTwitter.Models.User u = new User();

            if (followed != 0)
            {
                bd.spSetFolloweds(UsuarioLogueado.id, followed);
            }

            return RedirectToAction("Usuario");
        }
    }
}
