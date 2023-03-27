using DiplomaProject.EntityModels.Enums;

namespace DiplomaProject.Models.DTO
{
    public class HealthParametrDTO
    {
        public long Id { get; set; }
        public long UserId { get; set; } = default!;
        public Gender Gender { get; set; } = default!;
        public int Age { get; set; } = default!;
        public long Height { get; set; } = default!;
        public long Weight { get; set; } = default!;
        public double BMI { get; set; } = default!;
    }
}
