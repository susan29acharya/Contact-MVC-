using Contact__MVC_.services;
using Contact__MVC_.Models;
using Microsoft.AspNetCore.Mvc;

namespace Contact__MVC_.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContact _Contacts;
        public ContactController(IContact Contacts)
        {
               _Contacts = Contacts;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddContact(ContactData contacts)
        {
            var response = _Contacts.AddContact(contacts);
            return Json(response);
        }
        [HttpGet]
        public IActionResult FetchingContact()
        {
            var response = _Contacts.GetContacts();
            return Json(response);
        }
        [HttpPost]
        public IActionResult DeleteContact(ContactData contact)
        {
            var response = _Contacts.DeleteContact(contact);
            return Json(response);
        }
        [HttpGet]
        public IActionResult GetContactById(ContactData contact)
        {
            var response = _Contacts.GetContactById(contact);
            return Json(response);
        }
        [HttpPost]
        public IActionResult UpdateContact(ContactData contact)
        {
            var response = _Contacts.UpdateContact(contact);
            return Json(response);
        }
        [HttpPost]
        public IActionResult UploadImage(ContactData contact)
        {
            var response = _Contacts.UploadImage(contact);
            return Json(response);
        }
        public IActionResult Imagefetched(ContactData contact)
        {
            var response = _Contacts.Imagefetched(contact);
            return Json(response);
        }

    }
}
