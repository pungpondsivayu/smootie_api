namespace Domain.CustomResponse
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Profile { get; set; }
        public string Role { get; set; }
        public int BranchId { get; set; }
        public TokenResponse Token { get; set; }
    }
}
