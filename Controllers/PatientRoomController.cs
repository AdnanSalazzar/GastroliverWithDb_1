using GastroliverWithDb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GastroliverWithDb.Controllers
{
    public class PatientRoomController : Controller
    {
        private readonly MtechGastroLiverContext _context;

        public PatientRoomController(MtechGastroLiverContext context)
        {
            _context = context;
        }


        // GET: Patient/Create
        public IActionResult Create()
        {
            // Retrieve all rooms from the database
            var rooms = _context.TblRooms.ToList();

            // Create an instance of the ViewModel
            var viewModel = new PatientRoomViewModel
            {
                // Populate the list of rooms for the dropdown menu
                Rooms = rooms.Select(r => new SelectListItem
                {
                    Value = r.RoomId.ToString(),
                    Text = r.RoomNo
                }).ToList()
            };

            return View(viewModel);
        }

        // POST: Patient/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PatientRoomViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Create a new patient object and populate its properties
                var patient = new TblPatient
                {
                    Name = viewModel.PatientName,
                    PhoneNo = viewModel.PatientPhoneNo,
                    Email = viewModel.PatientEmail,
                    Nid=viewModel.PatientNid,
                    Gender = viewModel.PatientGender,
                    Age = viewModel.PatientAge,

                    RoomId = viewModel.SelectedRoomId 
                };

                // Add the patient to the database and save changes
                _context.TblPatients.Add(patient);
                _context.SaveChanges();


                // Redirect to a success page or action
                //return RedirectToAction("Index", "Home" , viewModel); // Redirect to the home page for example
                return RedirectToAction("ReciptFunction", "PatientRoom", new { name = viewModel.PatientName, phone = viewModel.PatientPhoneNo  , email = viewModel.PatientEmail});
            }

            // If ModelState is not valid, return to the create view with the ViewModel
            return View(viewModel);
        }


        // Another IActionResult method to handle redirection and pass data
        public async Task<IActionResult> ReciptFunction(string name, string phone , string email)
        {
            // You can do whatever you want with the passed data here
            ViewData["PatientName"] = name;
            ViewData["PatientPhoneNo"] = phone;
            ViewData["PatientEmail"] = email;

            var patientInfo = await _context.TblPatients
                     .Include(p => p.Room) // Include the Room navigation property
                     .FirstOrDefaultAsync(p => p.Name == name && p.PhoneNo == phone && p.Email == email);



            if (patientInfo == null)
            {
                // Handle the case where no patient information is found
                return RedirectToAction("PatientNotFound");
            }

            // Return the view or perform any other action
            return View(patientInfo);
        }


        // Action to handle case where patient information is not found
        public IActionResult PatientNotFound()
        {
            return View();
        }

    }



}
