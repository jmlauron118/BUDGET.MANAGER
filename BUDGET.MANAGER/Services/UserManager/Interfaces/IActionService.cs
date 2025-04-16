using BUDGET.MANAGER.Models.UserManager;

namespace BUDGET.MANAGER.Services.UserManager.Interfaces
{
    public interface IActionService
    {
        Task<List<ActionModel>> GetAllActions(int status);
        Task<List<ActionModel>> GetActionById(int actionId);
        Task<List<ActionModel>> AddAction(ActionModel action);
        Task<List<ActionModel>> ModifyAction(ActionModel action);
    }
}
