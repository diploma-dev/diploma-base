using DiplomaProject.EntityModels.Enums;

namespace DiplomaProject.EntityModels
{
    public class GoalEntity : BaseEntity
    {
        public long UserId { get; set; } = default!;
        public string Description { get; set; } = default!;
        public long TargetWeight { get; set; } = default!;
        public long DurationInDays { get; set; } = default!;
        public long DailyCalorie { get; set; } = default!;

        public virtual UserEntity User { get; set; } = default!;
    }
}
