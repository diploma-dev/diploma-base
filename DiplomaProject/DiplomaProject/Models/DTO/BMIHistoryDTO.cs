namespace DiplomaProject.Models.DTO
{
    public class BMIHistoryDTO
    {
        public long Id { get; set; }
        public double BMI { get; set; } = default!;
        public DateOnly CheckDate { get; set; } = default!;
        public long UserId { get; set; } = default!;
    }
}
