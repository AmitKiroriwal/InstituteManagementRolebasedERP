using InstituteManagement_Models;

namespace InstituteManagement.Models.Interfaces
{
    public interface IUserAccountConfirm
    {
        public Task<UserAccountConfirmations> AddPendingConfirmation(UserAccountConfirmations user);
        public Task<UserAccountConfirmations> ConfirmAccount(int Id);

        public Task<IEnumerable<UserAccountConfirmations>> GetPendingConfirmation();
        public Task<IEnumerable<UserAccountConfirmations>> GetConfirmedAccount();

    }
}
