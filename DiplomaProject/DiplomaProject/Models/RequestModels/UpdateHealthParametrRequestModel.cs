using DiplomaProject.EntityModels.Enums;

namespace DiplomaProject.Models.RequestModels
{
    public class UpdateHealthParametrRequestModel
    {
        public long UserId { get; set; } = default!;
        public int Age { get; set; } = default!;
        public long Height { get; set; } = default!;
        public long Weight { get; set; } = default!;
    }
}
