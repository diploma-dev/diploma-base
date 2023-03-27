using DiplomaProject.EntityModels.Enums;

namespace DiplomaProject.EntityModels
{
    public class HealthParametrEntity : BaseEntity
    {
        public long UserId { get; set; } = default!;
        public Gender Gender { get; set; } = default!;
        public int Age { get; set; } = default!;
        public long Height { get; set; } = default!;
        public long Weight { get; set; } = default!;
        public double BMI { get; set; } = default!;

        //In future we can add extra params like sugar in blood and etc.

        public virtual UserEntity User { get; set; } = default!;
    }
}
