namespace LibrarySystem.Models
{
    public class BookLoan
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int BookId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public ApplicationUser User { get; set; }
        public Book Book { get; set; }
    }
}
