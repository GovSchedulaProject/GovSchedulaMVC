using GovSchedulaWeb.Models.Data.GovSchedulaDBContext;
using GovSchedulaWeb.Models.Data.ViewModels;

namespace GovScedulaTrial.Models.Data.Services
{
    public class AccountService
    {
        private GovSchedulaDbContext _context;

        public AccountService(GovSchedulaDbContext context)
        {
            _context = context;
        }

        //Add user
        public bool SignUp(SignUpViewModel model)
        {
            Userprofile userprofile = new Userprofile()
            {
                UserName = model.Username,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password)
            };
            _context.Userprofiles.Add(userprofile);
            _context.SaveChanges();
            return true;
        }
        //Get users
        public List<SignUpViewModel> GetUserprofiles()
        {
            List<Userprofile> userprofiles = _context.Userprofiles.ToList();

            if(userprofiles == null)
            {
                return new List<SignUpViewModel>();
            }

            List<SignUpViewModel> model = userprofiles.Select(x => new SignUpViewModel
            {
                UserId = x.UserId,
                Username = x.UserName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                Password = x.Password
            }).ToList();

            return model;
        }
        // Get a user
        public SignUpViewModel GetUserprofile(int UserId)
        {
            Userprofile userprofile = _context.Userprofiles
                                               .Where(x => x.UserId == UserId)
                                               .FirstOrDefault();

            if (userprofile == null)
            {
                return new SignUpViewModel();
            }

            SignUpViewModel model = new SignUpViewModel()
            {
                UserId = userprofile.UserId,
                Username = userprofile.UserName,
                Email = userprofile.Email,
                PhoneNumber = userprofile.PhoneNumber,
                Password = userprofile.Password
            };

            return model;
        }

        public SignUpViewModel LogIn(string Email, string Password)
        {
            Userprofile userprofile = _context.Userprofiles
                                               .Where(x => x.Email == Email)
                                               .SingleOrDefault();
            if (userprofile == null || !BCrypt.Net.BCrypt.Verify(Password, userprofile.Password) == true)
            {
                throw new Exception("Username or password is incorrect");
            }

            return new SignUpViewModel
            {
                UserId = userprofile.UserId,
                Username = userprofile.UserName,
                Email = userprofile.Email,
                PhoneNumber = userprofile.PhoneNumber
            };    
        }
    }
}
