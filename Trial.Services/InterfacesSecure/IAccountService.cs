using Trial.DomainLogic.ResponsesSec;
using Trial.DomainLogic.TrialResponse;

namespace Trial.Services.InterfacesSecure;

public interface IAccountService
{
    Task<ActionResponse<TokenDTO>> LoginAsync(LoginDTO modelo);

    Task<ActionResponse<bool>> RecoverPasswordAsync(EmailDTO modelo, string frontUrl);

    Task<ActionResponse<bool>> ResetPasswordAsync(ResetPasswordDTO modelo);

    Task<ActionResponse<bool>> ChangePasswordAsync(ChangePasswordDTO modelo, string UserName);

    Task<ActionResponse<bool>> ConfirmEmailAsync(string userId, string token);
}