namespace Cook_Eat.models
{
    public class UserAccount
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsBusiness { get; set; }
    }
}