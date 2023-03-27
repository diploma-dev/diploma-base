using DiplomaProject.EntityModels.Enums;

namespace DiplomaProject.Models.RequestModels
{
    public class UploadHealthParametrRequestModel
    {
        public long UserId { get; set; } = default!;
        public Gender Gender { get; set; } = Gender.Unknow;
        public int Age { get; set; } = default!;
        public long Height { get; set; } = default!;
        public long Weight { get; set; } = default!;
    }
}
