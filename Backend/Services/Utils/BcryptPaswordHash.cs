using Core.IUtils;

namespace Services.Utils
{
    public class BcryptPaswordHash : IPasswordHash
    {
        private int salt = 12;

        public string Hash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, salt);
        }

        public bool VerifyHash(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
