namespace MedicalRecords.Domain.Requests.User
{
    public class SignUpRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserNickname { get; set; }
    }
}
