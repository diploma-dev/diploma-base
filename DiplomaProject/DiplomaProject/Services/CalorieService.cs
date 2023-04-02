using DiplomaProject.EntityModels.Enums;
using DiplomaProject.Models.Contracts;

namespace DiplomaProject.Services
{
    public interface ICalorieService
    {
        long CalculateDaysToReachTargetWeight(long currentWeight, long targetWeight, long extraDailyCalorie);
        LoseCalorieInfoModel CalculateDailyCalorieDeficit(long weight, long height, int age, Gender gender, ActivityType activityType);
        GainCalorieInfoModel CalculateDailyCalorieProficit(long weight, long height, int age, Gender gender, ActivityType activityType);
    }

    public class CalorieService : ICalorieService
    {
        public long CalculateDaysToReachTargetWeight(long currentWeight, long targetWeight, long extraDailyCalorie)
        {
            long daysToReachTargetWeight = 0;

            if(targetWeight < currentWeight)
            {
                double weightToLose = currentWeight - targetWeight;
                daysToReachTargetWeight = (long)Math.Ceiling((weightToLose * 7700) / extraDailyCalorie);
            }
            else if (targetWeight > currentWeight)
            {
                long weigthToGain = targetWeight - currentWeight;
                daysToReachTargetWeight = (weigthToGain * 7700) / extraDailyCalorie;
            }

            return daysToReachTargetWeight;
        }

        public LoseCalorieInfoModel CalculateDailyCalorieDeficit(long weight, long height, int age, Gender gender, ActivityType activityType)
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
                case ActivityType.ExtraActive:
                    dailyCalorieNeeds = bmr * 1.9;
                    break;
                case ActivityType.VeryActive:
                    dailyCalorieNeeds = bmr * 1.725;
                    break;
                case ActivityType.ModeratelyActive:
                    dailyCalorieNeeds = bmr * 1.55;
                    break;
                case ActivityType.LightlyActive:
                    dailyCalorieNeeds = bmr * 1.375;
                    break;
                case ActivityType.Sedentary:
                    dailyCalorieNeeds = bmr * 1.2;
                    break;
                default:
                    dailyCalorieNeeds = bmr * 1.2;
                    break;
            }

            dailyCalorieDeficit = (targetWeightLossPerWeek * 7700) / 7;

            return new LoseCalorieInfoModel()
            {
                DailyCalorieDeficit = (long)dailyCalorieDeficit,
                DailyCalorie = (long)(dailyCalorieNeeds - dailyCalorieDeficit)
            };
        }

        public GainCalorieInfoModel CalculateDailyCalorieProficit(long weight, long height, int age, Gender gender, ActivityType activityType)
        {
            double bmr = 0;
            double dailyCalorieNeeds = 0;
            double dailyCalorieProficit = 0;
            long dailyExtraCalorie = 400;

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
                case ActivityType.ExtraActive:
                    dailyCalorieNeeds = bmr * 1.9;
                    break;
                case ActivityType.VeryActive:
                    dailyCalorieNeeds = bmr * 1.725;
                    break;
                case ActivityType.ModeratelyActive:
                    dailyCalorieNeeds = bmr * 1.55;
                    break;
                case ActivityType.LightlyActive:
                    dailyCalorieNeeds = bmr * 1.375;
                    break;
                case ActivityType.Sedentary:
                    dailyCalorieNeeds = bmr * 1.2;
                    break;
                default:
                    dailyCalorieNeeds = bmr * 1.2;
                    break;
            }

            dailyCalorieProficit = dailyCalorieNeeds + dailyExtraCalorie;

            return new GainCalorieInfoModel()
            {
                DailyCalorieProficit = dailyExtraCalorie,
                DailyCalorie = (long)dailyCalorieProficit
            };
        }
    }
}
