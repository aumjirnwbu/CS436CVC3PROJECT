using CS436CVC3PROJECT.Data;
using CS436CVC3PROJECT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CS436CVC3PROJECT.Controllers
{
    public class InventoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InventoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Index action to list all inventory items
        public async Task<IActionResult> Index()
        {
            return View(await _context.Inventorys.ToListAsync());
        }

        // GET: Inventory/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Inventory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductName,Description,Price,Stock,Sale")] Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inventory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(inventory);
        }

        // GET: Inventory/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventorys.FindAsync(id);
            if (inventory == null)
            {
                return NotFound();
            }
            return View(inventory);
        }

        // POST: Inventory/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductName,Description,Price,Stock,Sale")] Inventory inventory)
        {
            if (id != inventory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inventory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventoryExists(inventory.Id))
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
            return View(inventory);
        }

        // GET: Inventory/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventorys
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        // POST: Inventory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inventory = await _context.Inventorys.FindAsync(id);
            if (inventory == null)
            {
                return NotFound();
            }

            _context.Inventorys.Remove(inventory);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Read(int id)
        {
            var email = await _context.EmailMessages.FindAsync(id);
            if (email == null)
            {
                return NotFound();
            }

            // เปลี่ยนสถานะเป็น "อ่านแล้ว"
            email.IsRead = true;
            _context.Update(email);
            await _context.SaveChangesAsync();

            return View(email);
        }


        private bool InventoryExists(int id)
        {
            return _context.Inventorys.Any(e => e.Id == id);
        }

        public IActionResult Inbox()
        {
            // คัดกรองอีเมลที่ส่งมาหาผู้ใช้งาน (ใช้ `Recipient` หรือ `ReceiverEmail`)
            var emails = _context.EmailMessages
                                 .Where(e => e.Recipient == User.Identity.Name) // ใช้ฟิลด์ที่เก็บอีเมลของผู้รับ
                                 .ToList();

            return View(emails); // ส่ง List ของ EmailMessage ไปยัง View
        }

        // Action สำหรับการดูรายละเอียด (อ่านข้อความ)
        // Action สำหรับการดูรายละเอียด (อ่านข้อความ)
        // In EmailController.cs
        public IActionResult ReadEmail(int id)
        {
            var email = _context.EmailMessages
                                .FirstOrDefault(e => e.Id == id && e.Recipient == User.Identity.Name);
            if (email == null)
            {
                return NotFound();
            }

            // Change the status to 'Read'
            email.IsRead = true;
            _context.Update(email);
            _context.SaveChanges();

            return View("ReadEmail", email); // Show the ReadEmail view
        }



    }
}
