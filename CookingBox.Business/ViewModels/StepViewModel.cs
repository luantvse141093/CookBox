using System;
namespace CookingBox.Business.ViewModels
{
    public class StepViewModel
    {
        public int id { get; set; }
        public string description { get; set; }
        public int? repiceId { get; set; }
        public string image { get; set; }
    }
}
