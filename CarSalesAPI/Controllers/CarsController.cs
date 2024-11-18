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
    public class CarsController : ControllerBase
    {
        private readonly CarSalesContext _context;


        public CarsController(CarSalesContext context)
        {
            _context = context;
        }

        // GET: api/Cars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars([FromQuery] int offset = 0, [FromQuery] int limit = 10)
        {
            if (offset < 0 || limit <= 0)
            {
                return BadRequest("Offset must be non-negative and limit must be greater than zero.");
            }

            var cars = await _context.Cars
                .Skip(offset)
                .Take(limit)
                .ToListAsync();

            return Ok(cars);
        }


        // GET: api/Cars/{vin}
        [HttpGet("{vin}")]
        public async Task<ActionResult<Car>> GetCar(string vin)
        {
            // A VIN mező alapján keresünk a Cars táblában
            var car = await _context.Cars.FirstOrDefaultAsync(c => c.Vin == vin);

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }

        // PUT: api/Cars/{vin}
        [HttpPut("{vin}")]
        public async Task<IActionResult> PutCar(string vin, Car car)
        {
            // Ellenőrizzük, hogy az útvonalban megadott VIN egyezik-e a kérésben lévő autó VIN mezőjével
            if (vin != car.Vin)
            {
                return BadRequest("A megadott VIN nem egyezik az autó VIN mezőjével.");
            }

            // Jelöljük az autót módosítottnak
            _context.Entry(car).State = EntityState.Modified;

            try
            {
                // Mentjük a változásokat az adatbázisban
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Ellenőrizzük, hogy létezik-e az autó a megadott VIN alapján
                if (!CarExists(vin))
                {
                    return NotFound();
                }
                else
                {
                    // Ha más hiba történt, továbbadjuk a kivételt
                    throw;
                }
            }

            // Sikeres frissítés esetén NoContent válasz
            return NoContent();
        }

        // POST: api/Cars
        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCar), new { vin = car.Vin }, car);
        }

        // DELETE: api/Cars/{vin}
        [HttpDelete("{vin}")]
        public async Task<IActionResult> DeleteCar(string vin)
        {
            var car = await _context.Cars.FirstOrDefaultAsync(c => c.Vin == vin);
            if (car == null)
            {
                return NotFound();
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarExists(string id)
        {
            return _context.Cars.Any(e => e.Vin == id);
        }
    }
}
