using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Enitities;
using Enitities.Model;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiftsController : ControllerBase
    {
        private readonly DataContext _context;

        public GiftsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Gifts
        [HttpGet]
        [Route("allgifts")]
        public IEnumerable<Gift> Getgifts()
        {
            return _context.gifts;
        }

        [HttpGet]
        [Route("GirlGifts")]
        public IEnumerable<Gift> GetGirlgifts()
        {
            return _context.gifts.Where(x => x.GirlGift == true).ToList(); ;
        }

        [HttpGet]
        [Route("BoyGifts")]
        public IEnumerable<Gift> GetBoygifts()
        {
            return _context.gifts.Where(x => x.BoyGift == true).ToList(); ;
        }

        // GET: api/Gifts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGift([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var gift = await _context.gifts.FindAsync(id);

            if (gift == null)
            {
                return NotFound();
            }

            return Ok(gift);
        }

        // PUT: api/Gifts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGift([FromRoute] long id, [FromBody] Gift gift)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gift.GiftNumber)
            {
                return BadRequest();
            }

            _context.Entry(gift).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GiftExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Gifts
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateGift([FromBody] Gift gift)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.gifts.Add(gift);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGift", new { id = gift.GiftNumber }, gift);
        }

        // DELETE: api/Gifts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGift([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var gift = await _context.gifts.FindAsync(id);
            if (gift == null)
            {
                return NotFound();
            }

            _context.gifts.Remove(gift);
            await _context.SaveChangesAsync();

            return Ok(gift);
        }

        private bool GiftExists(long id)
        {
            return _context.gifts.Any(e => e.GiftNumber == id);
        }
    }
}