namespace IdentityService.WebAPI.Controllers.ViewModels.ResponseModel.Login
{
    public record UserResponse(Guid Id,string UserName, string PhoneNumber, DateTime CreationTime);
}
