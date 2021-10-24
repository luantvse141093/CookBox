using System;
namespace CookingBox.Business.ViewModels
{
    public class TasteDetailViewModel
    {
        public int id { get; set; }
        public string taste_name { get; set; }
        public int? taste_level { get; set; }
        public int? taste_id { get; set; }
    }
}
