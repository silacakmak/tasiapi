using System.Data.Entity;
using tasiapi.Interfaces;
using tasiapi.Models;

namespace tasiapi.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly TasinmazDbContext _context;

        public AuthRepository(TasinmazDbContext context)
        {
            _context = context;
        }
        public User Login( string userName, string password)
        {
            var user =  _context.User.FirstOrDefault(x => x.UserName == userName);
            if (user == null)
            {
                return null;
            }
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt)){
                return null;

            }
            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] userpasswordHash, byte[] userpasswordSalt)
        {
            using (var hmac=new System.Security.Cryptography.HMACSHA512(userpasswordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i = 0;i<computedHash.Length;i++)
                {
                    if (computedHash[i] != userpasswordHash[i])
                    {
                        return false; 
                    }
                }
                return true;
            }
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password,out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
             _context.User.Add(user);
             _context.SaveChanges();
            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac=new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;                
                passwordHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public bool UserExists(string userName)
        {
          if( _context.User.Any(x => x.UserName == userName))
            {
                return true;
            }
          return false;
        }
    }
}
