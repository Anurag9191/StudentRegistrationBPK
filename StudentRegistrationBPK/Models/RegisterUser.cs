namespace StudentRegistrationBPK.Models
{
    public class RegisterUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? Dob { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Phone { get; set; }
        public string? ParentName { get; set; }
        public int? Maths { get; set; }
        public int? Science { get; set; }
        public int? Hindi { get; set; }
        public int? English { get; set; }
        public int? SocialScience { get; set; }
    }
}
