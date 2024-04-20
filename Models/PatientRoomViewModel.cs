using Microsoft.AspNetCore.Mvc.Rendering;

namespace GastroliverWithDb.Models
{
    public class PatientRoomViewModel
    {
        public string PatientName { get; set; }
        public string PatientPhoneNo { get; set; }
        public string PatientEmail { get; set; }
        public int PatientNid { get; set; }
        public string PatientGender { get; set; }
        public int PatientAge { get; set; }

        public int SelectedRoomId { get; set; }
        public List<SelectListItem> Rooms { get; set; }

        public PatientRoomViewModel()
        {
            Rooms = new List<SelectListItem>();
        }
    }
}
