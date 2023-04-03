using DiplomaProject.EntityModels;
using DiplomaProject.EntityModels.Enums;

namespace DiplomaProject.DataSeeding
{
    public static class GoalDescriptionInitialData
    {
        public static GoalTemplateEntity[] GoalTemplatesInitialData = new GoalTemplateEntity[]
        {
            new GoalTemplateEntity { Id = 1, GoalType = GoalType.GainWeight, Description = "Путь к мощному телу: Увеличение массы до {targetWeight} кг и достижение желаемой формы тела в течение {durationInDays} дней."},
            new GoalTemplateEntity { Id = 2, GoalType = GoalType.GainWeight, Description = "Преображение тела: Увеличение массы до {targetWeight} кг и достижение идеальной формы тела в течение {durationInDays} дней."},
            new GoalTemplateEntity { Id = 3, GoalType = GoalType.GainWeight, Description = "Сильное тело, сильный дух: Увеличение массы до {targetWeight} кг и достижение желаемой формы тела в течение {durationInDays} дней."},
            new GoalTemplateEntity { Id = 4, GoalType = GoalType.GainWeight, Description = "Новый облик: Увеличение массы до {targetWeight} кг и достижение новой формы тела в течение {durationInDays} дней."},
            new GoalTemplateEntity { Id = 5, GoalType = GoalType.GainWeight, Description = "Путь к более крепкому телу: Увеличение массы до {targetWeight} кг и достижение более крепкой формы тела в течение {durationInDays} дней."},
            new GoalTemplateEntity { Id = 6, GoalType = GoalType.GainWeight, Description = "Преображение тела через дисциплину: Увеличение массы до {targetWeight} кг и достижение желаемой формы тела в течение {durationInDays} дней."},
            new GoalTemplateEntity { Id = 7, GoalType = GoalType.GainWeight, Description = "Новый образ жизни: Увеличение массы до {targetWeight} кг и достижение новой формы тела в течение {durationInDays} дней через изменение образа жизни."},
            new GoalTemplateEntity { Id = 8, GoalType = GoalType.GainWeight, Description = "Путь к мощному телу и разуму: Увеличение массы до {targetWeight} кг и достижение желаемой формы тела в течение {durationInDays} дней, благодаря улучшению физической и умственной дисциплины."},
            new GoalTemplateEntity { Id = 9, GoalType = GoalType.GainWeight, Description = "Новый уровень фитнеса: Увеличение массы до {targetWeight} кг и достижение нового уровня фитнеса в течение {durationInDays} дней."},
            new GoalTemplateEntity { Id = 10, GoalType = GoalType.GainWeight, Description = "Путь к здоровому телу: Увеличение массы до {targetWeight} кг и достижение здоровой формы тела в течение {durationInDays} дней."},

            new GoalTemplateEntity { Id = 11, GoalType = GoalType.LoseWeight , Description = "Новое тело, новый я: Достигнуть желаемого веса тела {targetWeight} кг и преобразить себя за {durationInDays} дней."},
            new GoalTemplateEntity { Id = 12, GoalType = GoalType.LoseWeight , Description = "Путь к стройности: Достигнуть желаемого веса тела {targetWeight} кг и получить стройную фигуру за {durationInDays} дней."},
            new GoalTemplateEntity { Id = 13, GoalType = GoalType.LoseWeight , Description = "Путь к здоровой фигуре: Достигнуть желаемого веса тела {targetWeight} кг и получить здоровую фигуру за {durationInDays} дней."},
            new GoalTemplateEntity { Id = 14, GoalType = GoalType.LoseWeight , Description = "Путь к уверенности: Достигнуть желаемого веса тела {targetWeight} кг и стать более уверенным в себе за {durationInDays} дней.\r\n"},
            new GoalTemplateEntity { Id = 15, GoalType = GoalType.LoseWeight , Description = "Новый образ жизни: Достигнуть желаемого веса тела {targetWeight} кг и изменить свой образ жизни за {durationInDays} дней."},
            new GoalTemplateEntity { Id = 16, GoalType = GoalType.LoseWeight , Description = "Путь к здоровому образу жизни: Достигнуть желаемого веса тела {targetWeight} кг и привести свой образ жизни к здоровому уровню за {durationInDays} дней."},
            new GoalTemplateEntity { Id = 17, GoalType = GoalType.LoseWeight , Description = "Путь к энергичности: Достигнуть желаемого веса тела {targetWeight} кг и получить больше энергии и жизненной силы за {durationInDays} дней."},
            new GoalTemplateEntity { Id = 18, GoalType = GoalType.LoseWeight , Description = "Новый стиль жизни: Достигнуть желаемого веса тела {targetWeight} кг и создать новый стиль жизни за {durationInDays} дней."},
            new GoalTemplateEntity { Id = 19, GoalType = GoalType.LoseWeight , Description = "Путь к красоте: Достигнуть желаемого веса тела {targetWeight} кг и стать более привлекательным за {durationInDays} дней."},
            new GoalTemplateEntity { Id = 20, GoalType = GoalType.LoseWeight , Description = "Путь к долголетию: Достигнуть желаемого веса тела {targetWeight} кг и повысить свои шансы на долгую и здоровую жизнь за {durationInDays} дней."},
        };
    }
}
