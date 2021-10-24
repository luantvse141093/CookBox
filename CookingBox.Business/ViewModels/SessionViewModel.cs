using System;
namespace CookingBox.Business.ViewModels
{
    public class SessionViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public DateTime? time_from { get; set; }
        public DateTime? time_to { get; set; }
    }
}
