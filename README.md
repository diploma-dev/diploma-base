#  Проект подбора рационов питания 
> Проект подбора рационов питания - это комплексная система, которая помогает людям составить здоровый и сбалансированный рацион питания, основываясь на их индивидуальных потребностях, предпочтениях и целях.

Описание основных этапов проекта:

1. Консультация. Первый этап - это консультация, в ходе которой пользователь заполняет анкету о своих физических параметрах, образе жизни, предпочтениях в питании и целях (например, похудение, увеличение мышечной массы, поддержание здоровья и т.д.).

2. Анализ данных. На основе полученных данных система проводит анализ и вычисляет оптимальное соотношение белков, жиров и углеводов, необходимых для достижения цели пользователя.

3. Составление рациона. На этом этапе система генерирует индивидуальный рацион питания на основе предпочтений и потребностей пользователя. Рацион может включать в себя различные блюда и рецепты, а также информацию о количестве калорий и питательных веществ в каждом блюде.

4. Поддержка и контроль. После составления рациона питания, система предоставляет пользователю инструменты для контроля за выполнением рациона и помощь в случае возникновения проблем или изменений в целях.

Проект подбора рационов питания может помочь людям улучшить свое здоровье, достичь желаемой формы тела, повысить энергию и работоспособность. Этот проект может быть полезен для тех, кто хочет улучшить свой образ жизни и стать более здоровым и активным.


### Запуск проекта
> Для запуска проекта нужно чтобы у пользователя на его ПК стояли следующие программы: 

1. Visual Studio 2022 + .Net 6
2. PostgreSQL + PgAdmin4

> Процесс запуска
1. Клонирование проекта  [https://github.com/diploma-dev/diploma-base](https://github.com/diploma-dev/diploma-base)
2. В файле DatabaseSecret.cs заполняете следующие поля
3. private string host = "localhost";
4. private string port = "5432";
5. private string username = ""; - Ваш логин от PgAdmin4
6. private string password = ""; - Ваш пароль от PgAdmin4
7. private string database = ""; - Ваше название базы данных
8. private string minPool = "1";    
9. В PackageManagerConsole нужно прописать команду Update-database (эта команда создаст базу данных)
10. Запускайте и развлекайтесь. 


> v1.0 beta
