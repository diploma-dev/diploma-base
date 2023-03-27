namespace DiplomaProject.EntityModels
{
    public class BMIHistoryEntity : BaseEntity
    {
        public double BMI { get; set; } = default!;
        public DateOnly CheckDate { get; set; } = default!; 
        public long UserId { get; set; } = default!;

        public virtual UserEntity User { get; set; } = default!;
    }
}
