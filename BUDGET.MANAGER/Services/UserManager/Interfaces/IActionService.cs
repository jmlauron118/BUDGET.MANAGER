using BUDGET.MANAGER.Models.UserManager;

namespace BUDGET.MANAGER.Services.UserManager.Interfaces
{
    public interface IActionService
    {
        Task<List<ActionModel>> GetAllActions();
        Task<List<ActionModel>> GetActionById(int actionId);
        Task<List<ActionModel>> AddAction(ActionModel action);
        Task<List<ActionModel>> ModifyAction(ActionModel action);
        Task<List<ActionModel>> DeleteAction(int actionId);
    }
}
