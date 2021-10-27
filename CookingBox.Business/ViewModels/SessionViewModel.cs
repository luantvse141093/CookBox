using System;
namespace CookingBox.Business.ViewModels
{
    public class SessionViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public double? time_from { get; set; }
        public double? time_to { get; set; }
    }
}
