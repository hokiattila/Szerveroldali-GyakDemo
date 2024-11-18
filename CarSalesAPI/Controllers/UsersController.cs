using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarSalesAPI.Models;

namespace CarSalesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly CarSalesContext _context;

        public UsersController(CarSalesContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/{username}
        [HttpGet("{username}")]
        public async Task<ActionResult<User>> GetUser(string username)
        {
            // Keresés a username alapján
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }


        // PUT: api/Users/{username}
        [HttpPut("{username}")]
        public async Task<IActionResult> PutUser(string username, User user)
        {
            // Ellenőrizzük, hogy az URL-ben megadott username egyezik-e a kérésben lévő user.Username mezővel
            if (username != user.Username)
            {
                return BadRequest("A megadott username nem egyezik a felhasználó Username mezőjével.");
            }

            // Jelöljük a felhasználót módosítottnak
            _context.Entry(user).State = EntityState.Modified;

            try
            {
                // Mentjük a változásokat az adatbázisban
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Ellenőrizzük, hogy létezik-e a felhasználó a megadott username alapján
                if (!UserExists(username))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // Sikeres frissítés esetén NoContent válasz
            return NoContent();
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            // Új felhasználó hozzáadása
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Visszatérünk a létrehozott felhasználó adataival
            return CreatedAtAction(nameof(GetUser), new { username = user.Username }, user);
        }



        // DELETE: api/Users/{username}
        [HttpDelete("{username}")]
        public async Task<IActionResult> DeleteUser(string username)
        {
            // Keresés a username alapján
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

            // Ha a felhasználó nem található, akkor NotFound választ adunk vissza
            if (user == null)
            {
                return NotFound();
            }

            // A felhasználó törlése
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            // Sikeres törlés esetén NoContent választ
            return NoContent();
        }


        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.Username == id);
        }
    }
}
