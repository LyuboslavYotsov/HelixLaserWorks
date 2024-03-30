namespace HelixLaserWorks.Core.Models.Review
{
    public class ReviewViewModel
    {
        public int Id { get; set; }

        public string UserEmail { get; set; } = string.Empty;

        public string Comment { get; set; } = string.Empty;

        public int Rating { get; set; }
    }
}
