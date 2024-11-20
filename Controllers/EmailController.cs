using CS436CVC3PROJECT.Data;
using CS436CVC3PROJECT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CS436CVC3PROJECT.Controllers
{
    public class EmailController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmailController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Email/Compose
        public IActionResult Compose()
        {
            return View();
        }

        // POST: Email/Send
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Send(EmailMessage email)
        {
            if (ModelState.IsValid)
            {
                email.SentDate = DateTime.Now;
                email.IsRead = false;  // Set as unread when the email is sent

                // Save the email to the database
                _context.EmailMessages.Add(email);
                await _context.SaveChangesAsync();

                // Redirect back to inbox
                return RedirectToAction(nameof(Inbox), "Email");
            }
            return View("Compose", email);  // Return to compose view if there's a validation error
        }

        public async Task<IActionResult> Inbox()
        {
            var emails = await _context.EmailMessages.ToListAsync(); // Retrieve all email messages
            return View(emails);  // Ensure it's returning the Inbox view
        }
        public async Task<IActionResult> Read(int id)
        {
            var email = await _context.EmailMessages.FindAsync(id);

            if (email == null)
            {
                return NotFound(); // If the email with the given id does not exist
            }

            // Mark the email as "read" or perform any other necessary logic
            email.IsRead = true;
            _context.Update(email);
            await _context.SaveChangesAsync();

            return View(email);  // Return the "Read" view with the email data
        }

        public IActionResult Edit(int id)
        {
            var email = _context.EmailMessages.FirstOrDefault(e => e.Id == id);
            if (email == null)
            {
                return NotFound();
            }
            return View(email);
        }

        // POST: Email/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Sender,Subject,Recipient,Body,IsRead")] EmailMessage email)
        {
            if (id != email.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(email); // หากข้อมูลไม่ถูกต้อง กลับไปที่หน้า Edit
            }

            try
            {
                // ตรวจสอบว่า email มีการอัปเดตหรือไม่
                _context.Update(email);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmailMessageExists(email.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Inbox)); // กลับไปยังหน้า Inbox
        }

        private bool EmailMessageExists(int id)
        {
            return _context.EmailMessages.Any(e => e.Id == id);
        }

        public IActionResult Delete(int id)
        {
            var email = _context.EmailMessages.FirstOrDefault(e => e.Id == id);
            if (email == null)
            {
                return NotFound();
            }
            return View(email);
        }

        // POST: Email/Delete/5 (ลบอีเมลโดยตรง)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var email = _context.EmailMessages.Find(id);
            if (email != null)
            {
                _context.EmailMessages.Remove(email);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Inbox));  // กลับไปที่หน้า Inbox หลังจากลบอีเมล
        }
    }

}
