
namespace Eshop.ViewModel.User
{
    public class UserInformationViewModel
    {
        public int userId { get; set; }
        public int followers { get; set; }
        public int following { get; set; }
        public int UserMessages { get; set; }
        public Eshop.DomainClass.User.User User { get; set; }
    }
}
