using DiplomaProject.EntityModels.Enums;

namespace DiplomaProject.Models.RequestModels
{
    public class CreateGoalRequestModel
    {
        public long UserId { get; set; } = default!;
        public long TargetWeight { get; set; } = default!;
        public ActivityType ActivityType { get; set; } = default!;
    }
}
