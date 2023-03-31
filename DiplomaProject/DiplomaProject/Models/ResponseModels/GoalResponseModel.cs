using DiplomaProject.EntityModels.Enums;

namespace DiplomaProject.Models.ResponseModels
{
    public class GoalResponseModel
    {
        public long Id { get; set; }
        public long UserId { get; set; } = default!;
        public string Description { get; set; } = default!;
        public long DurationInDays { get; set; } = default!;
        public long DailyCalorie { get; set; } = default!;
    }
}
