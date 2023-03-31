using DiplomaProject.EntityModels.Enums;

namespace DiplomaProject.Models.DTO
{
    public class GoalDTO
    {
        public long Id { get; set; }
        public long UserId { get; set; } = default!;
        public string Description { get; set; } = default!;
        public long TargetWeight { get; set; } = default!;
        public long DurationInDays { get; set; } = default!;
        public long DailyCalorie { get; set; } = default!;
    }
}
