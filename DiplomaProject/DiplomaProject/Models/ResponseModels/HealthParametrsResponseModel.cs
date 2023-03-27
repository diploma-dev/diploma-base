using DiplomaProject.EntityModels.Enums;

namespace DiplomaProject.Models.ResponseModels
{
    public class HealthParametrsResponseModel
    {
        public Gender Gender { get; set; } = default!;
        public int Age { get; set; } = default!;
        public long Height { get; set; } = default!;
        public long Weight { get; set; } = default!;
        public double BMI { get; set; } = default!;
    }
}
