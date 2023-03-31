using DiplomaProject.EntityModels.Enums;
using DiplomaProject.Models.Contracts;

namespace DiplomaProject.Services
{
    public interface ICalorieService
    {
        long CalculateDaysToReachTargetWeight(double currentWeight, double targetWeight, long dailyCalorieDeficit);
        CalorieInfoModel CalculateDailyCalorieDeficit(double weight, double height, int age, Gender gender, ActivityType activityType);
    }

    public class CalorieService : ICalorieService
    {
        public long CalculateDaysToReachTargetWeight(double currentWeight, double targetWeight, long dailyCalorieDeficit)
        {
            double weightToLose = currentWeight - targetWeight;
            long weeksToReachTargetWeight = (long)Math.Ceiling((weightToLose * 7700) / dailyCalorieDeficit);

            return weeksToReachTargetWeight;
        }

        public CalorieInfoModel CalculateDailyCalorieDeficit(double weight, double height, int age, Gender gender, ActivityType activityType)
        {
            double bmr = 0;
            double dailyCalorieNeeds = 0;
            double dailyCalorieDeficit = 0;
            double targetWeightLossPerWeek = 0.5;

            switch (gender)
            {
                case Gender.Male:
                    bmr = 88.362 + (13.397 * weight) + (4.799 * height) - (5.677 * age);
                    break;
                case Gender.Female:
                    bmr = 447.593 + (9.247 * weight) + (3.098 * height) - (4.330 * age);
                    break;
            }

            switch (activityType)
            {
                case ActivityType.Active:
                    dailyCalorieNeeds = bmr * 1.9;
                    break;
                case ActivityType.ModeratelyActive:
                    dailyCalorieNeeds = bmr * 1.725;
                    break;
                case ActivityType.LowActivity:
                    dailyCalorieNeeds = bmr * 1.55;
                    break;
                case ActivityType.Sedentary:
                    dailyCalorieNeeds = bmr * 1.375;
                    break;
                default:
                    dailyCalorieNeeds = bmr * 1.2;
                    break;
            }

            dailyCalorieDeficit = (targetWeightLossPerWeek * 7700) / 7;

            return new CalorieInfoModel()
            {
                DailyCalorieDeficit = (long)dailyCalorieDeficit,
                DailyCalorie = (long)(dailyCalorieNeeds - dailyCalorieDeficit)
            };
        }
    }
}
