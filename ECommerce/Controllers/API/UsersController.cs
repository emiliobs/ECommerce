using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ECommerce.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json.Linq;
using ECommerce.Models.API;

namespace ECommerce.Controllers.API
{
    [RoutePrefix("api/Users")]
    public class UsersController : ApiController
    {
        private ECommerceContext db = new ECommerceContext();

        //Método Login:
        [HttpPost]
        [Route("Login")]
        public IHttpActionResult Login(JObject form)
        {
            //deshabilito las tablas virtuales:
            db.Configuration.ProxyCreationEnabled = false;
            string email = string.Empty;
            string password = string.Empty;
            dynamic jsonObject = form;

            try
            {
                email = jsonObject.Email.Value;
                password = jsonObject.Password.Value;
            }
            catch
            {
                return this.BadRequest("Incorrect call");
            }

            var userContext = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));
            var userASP = userManager.Find(email, password);

            if (userASP == null)
            {
                return this.BadRequest("User or password wrong");
            }

            var user = db.Users.Where(u => u.UserName == email).Include(u => u.Company).Include(u => u.City).Include(u => u.Department).FirstOrDefault();

            if (user == null)
            {
                return this.BadRequest("User not found");
            }

            var userResponse = new UserResponse
            {
                Address = user.Address,
                CityId = user.CityId,
                CityName = user.City.Name,
                //Company = user.Company,
                DepartmentId = user.DepartmentId,
                DepartmentName = user.Department.Name,
                FirstName = user.FirstName,
                IsAdmin = userManager.IsInRole(userASP.Id, "Admin"),
                IsCustomer = userManager.IsInRole(userASP.Id, "Admin"),
                IsSupplier = userManager.IsInRole(userASP.Id, "Admin"),
                IsUser = userManager.IsInRole(userASP.Id, "User"),
                LastName = user.LastName,
                Phone = user.Phone,
                Photo = user.Photo,
                UserId = user.UserId,
                UserName = user.UserName,
            };

            return this.Ok(userResponse);

        }

        // GET: api/Users
        public IQueryable<User> GetUsers()
        {
            db.Configuration.ProxyCreationEnabled = false;


            return db.Users;
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> GetUser(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            User user = await db.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.UserId)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Users
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(user);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> DeleteUser(int id)
        {
            User user = await db.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            await db.SaveChangesAsync();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.UserId == id) > 0;
        }
    }
}