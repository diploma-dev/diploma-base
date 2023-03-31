using DiplomaProject.EntityModels.Enums;

namespace DiplomaProject.Models.ResponseModels
{
    public class BMIResponseModel
    {
        public double BMI { get; set; } = default!;
        public string Status { get; set; } = default!;
    }
}
