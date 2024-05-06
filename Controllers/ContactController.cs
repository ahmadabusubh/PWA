using Microsoft.AspNetCore.Mvc;
using PWAX.core;
using PWAX.data;
using System;
using System.Linq;

namespace PWAX.Controllers
{
    public class ContactController : Controller
    {
        private readonly IdataHelper<Contact> _contactService;

        public ContactController(IdataHelper<Contact> contactService)
        {
            _contactService = contactService;
        }

        // GET: Contact
        public IActionResult Index(string sortOrder)
        {
            // تعيين قيم المتغيرات الخاصة بالفرز
            ViewData["IdSortParam"] = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewData["FirstNameSortParam"] = sortOrder == "FirstName" ? "firstname_desc" : "FirstName";
            ViewData["LastNameSortParam"] = sortOrder == "LastName" ? "lastname_desc" : "LastName";
            ViewData["EmailSortParam"] = sortOrder == "Email" ? "email_desc" : "Email";
            ViewData["PhoneNumberSortParam"] = sortOrder == "PhoneNumber" ? "phonenumber_desc" : "PhoneNumber";

            // إعداد الفرز الحسب الحالي
            var contacts = _contactService.GetAllData();
            switch (sortOrder)
            {
                case "id_desc":
                    contacts = contacts.OrderByDescending(c => c.Id).ToList();
                    break;
                case "FirstName":
                    contacts = contacts.OrderBy(c => c.FirstName).ToList();
                    break;
                case "firstname_desc":
                    contacts = contacts.OrderByDescending(c => c.FirstName).ToList();
                    break;
                case "LastName":
                    contacts = contacts.OrderBy(c => c.LastName).ToList();
                    break;
                case "lastname_desc":
                    contacts = contacts.OrderByDescending(c => c.LastName).ToList();
                    break;
                case "Email":
                    contacts = contacts.OrderBy(c => c.Email).ToList();
                    break;
                case "email_desc":
                    contacts = contacts.OrderByDescending(c => c.Email).ToList();
                    break;
                case "PhoneNumber":
                    contacts = contacts.OrderBy(c => c.PhoneNumber).ToList();
                    break;
                case "phonenumber_desc":
                    contacts = contacts.OrderByDescending(c => c.PhoneNumber).ToList();
                    break;
                default:
                    contacts = contacts.OrderBy(c => c.Id).ToList(); // الفرز الافتراضي حسب Id
                    break;
            }

            return View(contacts);
        }

        // GET: Contact/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = _contactService.Find(id.Value);

            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // GET: Contact/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Contact/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,FirstName,LastName,Email,PhoneNumber")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                _contactService.Add(contact);
                return RedirectToAction(nameof(Index));
            }
            return View(contact);
        }

        // GET: Contact/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = _contactService.Find(id.Value);

            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }

        // POST: Contact/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,FirstName,LastName,Email,PhoneNumber")] Contact contact)
        {
            if (id != contact.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _contactService.Edit(contact, id);
                }
                catch (Exception)
                {
                    if (!_contactService.GetAllData().Any(c => c.Id == id))
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
            return View(contact);
        }

        // GET: Contact/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = _contactService.Find(id.Value);

            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // POST: Contact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _contactService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
