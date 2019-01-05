using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Enitities;
using Enitities.Model;
using System.Net.Http;
using Newtonsoft.Json;

namespace Web.Controllers
{
    public class GiftsController : Controller
    {
        private readonly DataContext _context;
        private readonly IHttpClientFactory _clientFactory;

        public GiftsController(DataContext context, IHttpClientFactory ClientFactory)
        {
            _context = context;
            _clientFactory = ClientFactory;
           var client = _clientFactory.CreateClient("pallesGB");
        }


        //GET: Gifts
        public async Task<IActionResult> Index()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
            "api/gifts/allgifts");
            var client = _clientFactory.CreateClient("PallesGB");
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var list = await response.Content.ReadAsAsync<List<Gift>>();
                return View(list);
            }

            return View();
           


        }

        [HttpGet]
        [Route("/girlgifts")]
        public async Task<IActionResult> GirlGifts()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
            "api/gifts/girlgifts");
            var client = _clientFactory.CreateClient("PallesGB");
            var response = await client.SendAsync(request);

            var list = await response.Content.ReadAsAsync<List<Gift>>();
            return View(list);


        }

        // GET: Gifts/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gift = await _context.gifts
                .FirstOrDefaultAsync(m => m.GiftNumber == id);
            if (gift == null)
            {
                return NotFound();
            }

            return View(gift);
        }

        // GET: Gifts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Gifts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GiftNumber,Title,Description,CreationDate,BoyGift,GirlGift")] Gift gift)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var baseUrl = "http://localhost:60844/";           
            var client = _clientFactory.CreateClient("PallesGB");  
            var response = await client.PostAsJsonAsync($"{baseUrl}api/gifts/create", gift);
            if (response.IsSuccessStatusCode)
            {
                var giftToSave = JsonConvert.DeserializeObject<Gift>(await response.Content.ReadAsStringAsync());
              
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Gifts/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gift = await _context.gifts.FindAsync(id);
            if (gift == null)
            {
                return NotFound();
            }
            return View(gift);
        }

        // POST: Gifts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("GiftNumber,Title,Description,CreationDate,BoyGift,GirlGift")] Gift gift)
        {
            if (id != gift.GiftNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gift);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GiftExists(gift.GiftNumber))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(gift);
        }

        // GET: Gifts/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gift = await _context.gifts
                .FirstOrDefaultAsync(m => m.GiftNumber == id);
            if (gift == null)
            {
                return NotFound();
            }

            return View(gift);
        }

        // POST: Gifts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var gift = await _context.gifts.FindAsync(id);
            _context.gifts.Remove(gift);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GiftExists(long id)
        {
            return _context.gifts.Any(e => e.GiftNumber == id);
        }
    }
}
