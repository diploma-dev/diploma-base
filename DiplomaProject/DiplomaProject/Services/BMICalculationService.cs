using DiplomaProject.EntityModels.Enums;

namespace DiplomaProject.Services
{
    public interface IBMICalculationService
    {
        double CalculateBMI(long weight, long height, int age, Gender gender);
        string GetBMIStatus(double bmi);
    }

    public class BMICalculationService : IBMICalculationService
    {
        public double CalculateBMI(long weight, long height, int age, Gender gender)
        {
            double bmi = (weight / Math.Pow((double)height / 100, 2));

            if (gender == Gender.Male)
            {
                bmi += 0.5;

                if (age > 50)
                {
                    bmi += 0.7;
                }
            }
            else if (gender == Gender.Female)
            {
                bmi -= 0.5;

                if (age > 50)
                {
                    bmi += 0.7;
                }
            }

            return bmi;
        }

        public string GetBMIStatus(double bmi)
        {
            if (bmi < 18.5)
            {
                return BMIStatus.Underweight.ToString();
            }
            else if (bmi < 25)
            {
                return BMIStatus.NormalWeight.ToString();
            }
            else if (bmi < 30)
            {
                return BMIStatus.Overweight.ToString();
            }
            else
            {
                return BMIStatus.Obese.ToString();
            }
        }
    }
}
