using DiplomaProject.EntityModels.Enums;

namespace DiplomaProject.EntityModels
{
    public class GoalTemplateEntity : BaseEntity
    {
        public GoalType GoalType { get; set; } = default!;
        public string Description { get; set; } = default!;
    }
}
