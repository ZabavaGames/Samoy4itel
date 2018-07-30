namespace MyMobileProject1 {

// енамы общего назначения
	public enum grades { beforeschool, novice, shkolnik, gramotei, otlichnik, vypusknik, uchitel, 
						master, professor, akademik, volshebnik, bog };
	public enum ranks { zero, light, medium, advanced, hard };
	public enum languages { russian, english, france, german, italian, spanish, portugal, polsky, 
							arabian, hindi, chinese, japan };
    public enum SposobPodgotovki { Random4ik, StepByStep, OneFromAll, BlocksFive };

    public static class GradesConst {

		public const int MinGrade = (int)grades.beforeschool;
		public const int MaxGrade = (int)grades.volshebnik;
		public static string[] langs = {"РУС", "ENG", "FRA", "GER", "ITA", "ESP", "POR", "POL",
										 "ARA", "HIN", "CHI", "JAP" };

		public static string[][] GradeStrings = {
			new string[] {"Дошколёнок", "Новичок", "Школьник", "Грамотей", "Отличник", 
			"Выпускник", "Учитель", "Мастер", "Профессор", "Академик", "Волшебник", "<нет>"},
			new string[] {"Preschooler", "Novice", "Schoolboy", "Scholar", "Honor pupil",
            "Graduate", "Teacher", "Master", "Professor", "Academician", "Wizard", "god"}
			};

		public static string[] SourceFilesRus = new string[] //{"dal1000-1.txt", "dal1000-2.txt",
			{"Берсеньева-1.txt"};//, "Берсеньева-2.txt", "sysoev.txt"};
        public static string[] SourceFilesEng = new string[] {"engproverbs.txt"};

        //  важные константы	
        public static int[] StarsToPromote =  {0, 1, 7, 20, 30, 50, 80, 120, 180, 240, 320 };
//		public static int[] StringsPerLevel = {3, 5, 5, 10, 10, 15, 15, 20,  20,  25,  30 };
//		public static int[] ErrorsPerLevel =  {1, 1, 1, 1,  1,  2,  2,  2,   3,   3,   4 };
		public static int[] StringsPerLevel = {3, 5, 5, 5, 10, 10, 10, 15,  15,  20,  20 };
		public static int[] ErrorsPerLevel =  {1, 1, 1, 1,  2,  2,  2,  3,   3,   3,   4 };
		public static int[] RankGradeReq =  {(int)grades.beforeschool, (int)grades.shkolnik, 
											(int)grades.gramotei, (int)grades.otlichnik};
		public static int[] SessionScore = {0, 45, 60, 85}; // percents
		public const int RawsOnScreen = 6; // строки, ограничение экрана
		public const int MinWordLength = 2; // слово состоит из минимум Н символов
		public const int MinGenericErrors = 2;
		public const int Categories = 5;  // кол-во категорий ошибок в готовых примерах
		public const int HidePhraseTimer = 3000; // ms
		public const int ReplikaShowTimer = 9000; // ms
		public const int TimeCapPerPhrase = 30000; // ms
		public static int[] BestTimePerPhrase = { 5000, 5000, 6000, 7000, 8000 }; // ms

		public const int TimeToShowHelp = 5;  // в секундах
		public const float BlinkTime = 1f; // длительность мигания подсказки
		public const int ChanceToShowGrats = 5;  // 1 из 5
		public const float PhraseDoneDelay = 1.5f;  // сколько показывать исправленный пример

		public static string[] LetterButtonName = { "ButtonL (", ")" };
	//	public const string Button20 = "ButtonL (20)";
	//	public const string Button120 = "ButtonL (120)";

//  ключи для настроек в PlayerPref
		public const string ruslesson = "ruslesson";
		public const string ruslesson2 = "ruslesson2";
		public const string firstrun = "firstrun";
		public const string started = "started";
		public const string done = "done";
		public const string enabled = "enabled";
		public const string disabled = "disabled";
		public const string ads = "ads";
		public const string grade = "grade";
		public const string stars = "stars";
		public const string scene = "scene";
		public const string rank = "rank";
		public const string date = "date";
		public const string lang = "lang";
	//	public const string gd1sp = "grade1savpos";
	//	public const string gd2sp = "grade2savpos";
	//	public const string gd3sp = "grade3savpos";
		public const string save_rank = "save_rank";
		public const string save_errors_corrected = "save_errors_corrected";
		public const string save_phrase_number = "save_phrase_number";
		public const string save_session_limit = "save_session_limit";
		public const string save_session_time = "save_session_time";
		public const string saved_session = "saved_session";
		public const string save_position = "save_position";
		public const string prev_save_position = "prev_save_position";
		public const string hard_help = "hard_help";
		public const string score = "score";
		public const string undecised = "undecide";
		public const string fivestars = "fivestars";

		public const string Scene0 = "start", 
							Scene1 = "ruslesson",    // на самом деле сцены пока что ничем не отличаются
							Scene2 = "englesson";   // скрипты там и там одни и те же

		public const string ApplicationIdRus = "com.ZabavaGames.Samoy4itel";
        public const string ApplicationIdEng = "com.ZabavaGames.SelfTeacher";
        public static string[] PlayMarketUrl = {"https://play.google.com/store/apps/details?id=com.ZabavaGames.Samoy4itel",
                                                                    "https://play.google.com/store/apps/details?id=com.ZabavaGames.SelfTeacher" };
        public static string[] PlayMarketLink = {"market://details?id=com.ZabavaGames.Samoy4itel",
                                                                      "market://details?id=com.ZabavaGames.SelfTeacher" };
        //	public const string PlayMarketLink = "market://details?id=" + Application.productName;
        public const string MailTo = "zabava.games.studio@gmail.com";
		public static string[] MailSubject = { "О приложении Сам себе учитель", "About application Self-teacher" };

//  элементы интерфейса, главного меню и др.

	public static string[] TitleString = {"Сам себе учитель", "Self-teacher"};
	public static string[] HelloWorld = {"Привет!", "Hello!"};
	public static string[] LevelString = {"Твой уровень: ", "Your level: "};
	public static string[] PlayString = {"Играть!", "Play!"};
	public static string[] ControlString = {"Управление", "Control"};
	public static string[] ExitString = {"Выйти", "Quit"};
	public static string[] ScoreString = {"Оцени нас!", "Rate us!"};
	public static string[] EmailString = {"Напиши нам!", "Email us!"};
	public static string[][] Reklama = {
		new string[] {"Рекламу можно отключить, приобретя пакет \"Премиум\" в разделе Покупки.", 
		"Рекламу можно отключить, приобретя пакет \"Премиум\" в разделе Покупки.",
		"Рекламу можно отключить, приобретя пакет \"Премиум\" в разделе Покупки."},
		new string[] {"Advertising can be turned off. To do that, buy a Premium package.",
        "Advertising can be turned off. To do that, buy a Premium package.",
        "Advertising can be turned off. To do that, buy a Premium package."}
		};
	public static string[][] Help = {
		new string[] {"Если ты не закончил задание и вышел, то сможешь потом продолжить.",
		"Ты можешь повторить задание, если оно тебе понравилось.",
		"Если ты не можешь найти ошибку, воспользуйся подсказкой!",
		"Когда ищешь ошибку, не торопись. Правильный ответ важнее, чем потраченное время!",
		"Непонятный пример можно пропустить, но это ухудшит твой результат. :-(",
		"Задания кажутся слишком лёгкими? Попробуй более сложный уровень!",
		"На сложном уровне тебя ждут случайные примеры - всегда новые!",
		"Если тебе нравится игра, поставь нам оценку в Play Market. :-)",
		"Велик и могуч русский язык! А ты его хорошо знаешь?" },
		new string[] {"If you do not finish the task and leave, you can continue later.", 
		"You can repeat the task if you liked it.", 
		"If you can not find the error, use the hint!", 
		"When looking for a mistake, don't hurry. The correct answer is more important, than the time spent!",
        "A difficult task can be skipped, but this will worsen your score. :-(", 
		"Tasks seem too easy? Try a more difficult level!", 
		"At a hard level, you are waiting for random examples - always new!", 
		"If you like the game, give us a score on the Play Store. :-)", 
		"Russian language is great and powerful! Do you know him well?"}
		 };

	public static string[] Rules = {"Правила", "Rules"};
	public static string[] Trophy = {"Достижения", "Achievements"};
	public static string[] Basket = {"Покупки", "Purchases"};
	public static string[] Tools = {"Настройки", "Settings"};
	public static string[] Nazad = {"Назад", "Back"};

	public static string[] RulesText = {"В этой игре тебе нужно исправлять грамматические ошибки, зачёркивая пальцем неправильные буквы в словах. \nЧем быстрее ты это делаешь, тем лучше! \n\nЗа выполнение задания, состоящего из нескольких фраз, ты получаешь звёзды. Собирая звёзды, ты повышаешь свой уровень в игре. \n\nС каждым новым уровнем задания становятся всё интереснее! \nПопробуй достичь максимального уровня и заработать звание Волшебника!", 
										"In this game you need to correct grammatical errors by crossing out the wrong letters in words with your finger. \nThe faster you do it, the better! \n\nFor performing the task, consisting of several phrases, you receive stars. By collecting stars, you raise your level in the game. \n\nTasks become more interesting with each new level! \nTry to reach the maximum level and earn the title of the Wizard!"}; 
	public static string[] Close = {"Закрыть", "Close"};

	public static string[] NextLevel = {"Следующий уровень: ", "Next level: "};
	public static string[] Star_S = {"Собрано звезд: ", "Stars collected: "};
	public static string[] Stars_L = {"Осталось собрать звезд: ", "Stars to collect: "};
	public static string[] AdsInfo = {"Ты можешь посмотреть рекламный ролик, чтобы получить звезду!", 
										"You can watch the commercial to get a star!"};
	public static string[] AdsText = {"Смотреть видео", "Watch promo"};

	public static string[] TableText = {"Игровое поле: ", "Playing field: "};
	public static string[] Table1Text = {"Школьная доска", "School board"};
	public static string[] StyleText = {"Стиль зачёркивания: ", "Stitching style: "};
	public static string[] Style1Text = {"Простая указка", "Simple pointer"};
	public static string[] FontText = {"Выбор шрифта: ", "Font selection: "};
	
	public static string[] ADPurchased = {"Ты уже купил пакет \"Премиум\". Спасибо за твою поддержку!", 
										"You already bought a package \"Premium\". Thanks for your support!"};
	public static string[] ADnotPurchased = {"Купить пакет \"Премиум\" для того, чтобы отключить рекламу в игре и получать все будущие обновления бесплатно!", 
										"Buy the package \"Premium\" in order to disable advertising in the game and receive all future updates for free!"};
	public static string[] AllLvPurchased = {"Ты не можешь купить максимальный уровень в игре! Но ты можешь достичь его сам!", 
										"You can not buy the maximum level in the game! But you can reach it yourself!"};
	public static string[] LvnotPurchased = {"Ты можешь мгновенно повысить свой уровень. Ты автоматически получишь необходимое для этого количество звёзд.", 
										"You can instantly increase your level. You will automatically receive the required number of stars for this."};
	public static string[] PremiumPrice = {"Премиум-пакет\n99 руб.", "Premium package\n$1,5"};
	public static string[] LvUnlockPrice = {"Повышение уровня\n45 руб.", "Raise level\n$0,99"};

	public static string[] Helpach0 = {"Выбери уровень сложности. Начни с легкого!", 
										"Choose your difficulty level. Start with an easy one!"};
	public static string[] Helpach1 = {"Ты открыл более сложный уровень. Попробуй сыграть на нём!", 
										"You have opened more difficult level. Give it a go!"};
	public static string[] Helpach2 = {"Получи еще несколько звёзд, чтобы открыть самый сложный уровень!", 
										"Get a few more stars to open the most difficult level!"};
	public static string[] Helpach3 = {"Выбери уровень сложности. Какой тебе нравится?", 
										"Choose your difficulty level. Which one do you like?"};
    public static string[] Helpach4 = {"В этой версии программы доступен только один режим. Попробуй его!",
                                        "In this version of app only one level is available. Try it!"};
    public static string[] Difficulty = {"Сложность", "Difficulty"};
	public static string[] Novice = {"Легкий", "Light"};
	public static string[] Student = {"Средний", "Medium"};
	public static string[] Master = {"Продвинутый", "Intermediate"};
	public static string[] Expert = {"Сложный", "Hard"};

	public static string[] ScoreText = {"Ну как, тебе понравилось наше приложение? Скажи честно! Если хочешь поставить нам оценку в Play Маркет, нажми \"Да!\"", 
										"Well, did you like our application? Tell us honestly! If you want to rate us on the Play Store, click \"Yes!\""};
	public static string[] OptionYes = {"Да, я хочу поставить оценку!", "Yes, I want to rate!"};
	public static string[] OptionNo = {"Нет, и больше не предлагайте это.", "No, and do not ask me again."};
	public static string[] OptionLater = {"Я еще не решил...", "I have not decided yet..."};

        //  сообщения и константы для сеанса руслессон

        public static string[][] Messages = {
        new string[] {"Поздравляю! Ты исправил все ошибки!",
        "Молодец! Ты справился! Так держать!",
        "Отличная работа! Все ошибки исправлены!",
        "Ты справился со всеми ошибками! Здорово!",
        "Отлично! Ты всё исправил!",
        "Прекрасная работа! Ты всё исправил!",
        "Здорово! Теперь все буквы на своих местах!",
        "Прекрасно! Все буквы на месте!",
        "Все буквы на своих местах! Прекрасная работа!",
        "Так держать! Всё верно!",
        "Ты справился! Так держать!"},
        new string[] {"Congratulations! You fixed all the mistakes!",
        "Well done! You did it! Keep it up!",
        "Great work! All bugs fixed!",
        "You coped with all the mistakes! Great!",
        "Excellent! You fixed everything!",
        "Great job! You fixed it all!",
        "Great! Now all the letters are in their places!",
        "Perfect! All the letters are in place!",
        "All the letters are in their place! Great job!",
        "Keep it up! That's right!",
        "You did it! Keep it up!"}
        };

        public static string[] Zadanie = { ", задание ", ", task " };
        public static string[] EndofPool = {"Ты решил все примеры этого уровня. Попробуй более сложный!",
                                        "You solved all the examples for this level. Try something more complicated!"};
        public static string[] EndofPool1 = {"Ты прошёл этот уровень. Можешь начать сначала!",
                                        "You passed this level. You can start again!"};
        public static string[] EndofPool2 = {"Или попробуй новый уровень сложности! Для этого начни новую игру. Твой прогресс сохранится.",
                                        "Or try a new level of difficulty! To do this, start a new game. Your progress will remain."};
        public static string[] EndofPool3 = {"Постарайся исправить больше ошибок, чтобы открыть более сложный уровень.",
                                        "Try to correct more errors to open a more difficult level."};
        public static string[] HardHelp1 = {"Ты выбрал сложный уровень. Он отличается от предыдущих.",
                                        "You chose a hard level. It differs from the previous ones."};
        public static string[] HardHelp2 = {"В этом режиме игры ты увидишь примеры из словаря русских пословиц и поговорок со случайными ошибками в них. Больше никаких повторов - количество примеров бесконечно!",
                                        "In this game mode you will see examples from the dictionary of proverbs and sayings with random errors in them. No more repetitions - the number of examples is infinite!"};
        public static string[][] Helpmsg0 = {
        new string[] {"Безударная гласная!", "Замени букву!", "Лишняя буква!"},
        new string[] {"Impudent vowel!", "Change the letter!", "Remove an extra letter!"}
        };
        public static string[][] Helpmsg1 = { 
//		"Замени букву, чтобы одно слово превратить в другое!",
//		"Проверь ударения и исправь безударную гласную!",
//		"Исправь согласную - звонкую на глухую, или наоборот",
//		"Напиши правильно словарное слово!",
//		"В одном из слов есть лишняя буква. Вычеркни ее!",
//		"Вспомни правило, чтобы писать правильно!"
		new string[] {"Замени букву!", "Безударная гласная!", "Звонкая или глухая?", "Словарное слово!", "Лишняя буква!", "Вспомни правило!"},
        new string[] {"Change the letter!", "Impudent vowel!", "Ringing or deaf?", "Word dictionary!", "Remove an extra letter!", "Remember the rule!"}
         };

        //  сообщения во всплывающих окнах
        public static string[] YourTime = { "Ваше время: ", "Your time: " };
        public static string[] YourResult = { "Ваш результат: ", "Your result: " };
        public static string[] Percents = { " процентов.", " percentage." };
        public static string[] Seconds = { " c.", " s." };
        public static string[] Congratulations = {"Поздравляем! Ты успешно выполнил все задания!",
                            "Congratulations! You have successfully completed all the tasks!"};
        public static string[] HelpPhrase1 = {"Добро пожаловать! Глупенький Двойкин наделал ошибок. Исправь их волшебной ручкой!",
                            "Welcome! Silly botcher made a lot of mistakes. Fix them with a magic pen!"};
        public static string[] HelpPhrase2 = {"Просто зачеркни неправильную букву пальцем. Попробуй! Желаем удачи!",
                            "Just cross out the wrong letter with your finger. Try it! Good luck!"};
        public static string[] EndPhrase1 = {"Прекрасно! Ты справился с заданием! Получи награду!",
                            "Perfect! You coped with the task! Get your reward!"};
        public static string[] EndPhrase2 = {"Собирай звезды, чтобы повысить свой уровень. У тебя всего звезд: ",
                            "Collect stars to raise your level. You have stars: "};
        public static string[] EndPhrase3 = {"К сожалению, твой результат не позволил тебе получить ни одной звезды. Не расстраивайся! Попытайся еще раз!",
                            "Unfortunately, your result did not allow you to get a single star. Do not worry! Try again!"};
        public static string[] PromPhrase1 = {"Великолепно! Ты достиг нового уровня знаний!",
                            "Great! You have reached a new level of knowledge!"};
        public static string[] PromPhrase2 = {"Продолжай получать достижения и не забудь поделиться со своими друзьями!",
                            "Keep getting achievements and do not forget to share it with your friends!"};
        public static string[] HelpText1 = {"Исправление",
                            "Correction"};
        public static string[] HelpText2 = {"Подсказка подсвечивает слова, в которых есть ошибки",
                            "Tooltip highlights words that have errors"};
        public static string[] HelpText3 = { "Таймер", "Timer" };
        public static string[] HelpText4 = { "Пропустить пример и перейти к следующему", "Skip the example and go to the next one" };
        public static string[] HelpText5 = { "Выход в меню", "Exit to the menu" };
        public static string[] HelpText6 = { "Твой помощник. Он дает ценные советы!", "Your assistant. He gives valuable advice!" };
        public static string[] NewLevel = { "Получен_новый_уровень!", "New_level_gained!" };
        public static string[] Nazvanie = { "Сам_себе_учитель", "Self-teacher" };

        public const string StringsPath = "Strings/";
        public const string Msgfile = "messages.txt";
        public const string Level1Path = "1Уровень/";
        public const string Level21Path = "2Уровень/1ошибка/";
        public const string Level22Path = "2Уровень/2ошибки/";
        public const string Level3Path = "3Уровень/";
        public const string Level4Path = "4Уровень/";
        public static string[] grade0files_show = new string[] { Level1Path + "0_Ош_Дошколёнок.txt" };
        public static string[] grade0files_corr = new string[] { Level1Path + "0_В_Дошколёнок.txt" };
        public static string[] grade1files_show = new string[] { Level1Path + "1_Ош_001_зам15.txt", Level1Path + "1_Ош_002_пров15.txt", Level1Path + "1_Ош_003_согл15.txt", Level1Path + "1_Ош_004_слов15.txt", Level1Path + "1_Ош_005_лиш15.txt", Level1Path + "1_Ош_006_прав15.txt" };
        public static string[] grade1files_corr = new string[] { Level1Path + "1_В_001_зам15.txt", Level1Path + "1_В_002_пров15.txt", Level1Path + "1_В_003_согл15.txt", Level1Path + "1_В_004_слов15.txt", Level1Path + "1_В_005_лиш15.txt", Level1Path + "1_В_006_прав15.txt" };
        public static string[] grade2files_show = new string[] { Level21Path + "2_1_Ош_001_зам20.txt", Level21Path + "2_1_Ош_002_пров10.txt", Level21Path + "2_1_Ош_003_согл10.txt", Level21Path + "2_1_Ош_004_слов10.txt", Level21Path + "2_1_Ош_005_лиш20.txt" };
        public static string[] grade2files_corr = new string[] { Level21Path + "2_1_В_001_зам20.txt", Level21Path + "2_1_В_002_пров10.txt", Level21Path + "2_1_В_003_согл10.txt", Level21Path + "2_1_В_004_слов10.txt", Level21Path + "2_1_В_005_лиш20.txt" };
        public static string[] grade2_files_show = new string[] { Level22Path + "2_2_Ош_001_зам20.txt", Level22Path + "2_2_Ош_002_пров30.txt", Level22Path + "2_2_Ош_003_согл30.txt", Level22Path + "2_2_Ош_004_слов30.txt", Level22Path + "2_2_Ош_005_лиш20.txt" };
        public static string[] grade2_files_corr = new string[] { Level22Path + "2_2_В_001_зам20.txt", Level22Path + "2_2_В_002_пров30.txt", Level22Path + "2_2_В_003_согл30.txt", Level22Path + "2_2_В_004_слов30.txt", Level22Path + "2_2_В_005_лиш20.txt" };
        public static string[] grade3files_show = new string[] { Level3Path + "3_2_Ош_001_70", Level3Path + "3_3_Ош_001_60" };
        public static string[] grade3files_corr = new string[] { Level3Path + "3_2_В_001_70", Level3Path + "3_3_В_001_60" };

    public const char ZeroChar = '\0', EndOfLine = '\n', Spacebar = ' ', Point = '.', 
		TwoDots = ':', Slash = '/';
	public const string DoubleZeroes = "0:0";
	public static char[] EndSigns = { '.', '!', '?' };
	public static char[] Ends = { ZeroChar, EndOfLine };
	public static char[] SpaceAndEnds = { ' ', '"', '\'', '.', '!', '?', '\0', '\n' };

        public static char[] 
        RusCharTable = {'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж',
                                     'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т',
                                'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я' },
        EngCharTable = {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n',
                                        'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

        public static char[][] 
            RusCharTable_closest = new char[][] {
            new char[] {'к', 'е', 'в', 'п', 'с', 'м'}, new char[] {'л', 'д', 'ь', 'ю', '_'},
            new char[] {'у', 'к', 'ы', 'а', 'ч', 'с'}, new char[] {'7', '8', 'н', 'ш', 'р', 'о'},
            new char[] {'щ', 'з', 'л', 'ж', 'б', 'ю'}, new char[] {'5', '6', 'к', 'н', 'а', 'п'},
            new char[] {'1', 'е', 'ё'}, new char[] {'з', 'х', 'д', 'э', 'ю', '_'},
            new char[] {'0', '-', 'щ', 'х', 'д', 'ж'}, new char[] {'п', 'р', 'м', 'т', '_'},
            new char[] {'1', '2', 'ц', 'ф'}, new char[] {'4', '5', 'у', 'е', 'в', 'а'},
            new char[] {'ш', 'щ', 'о', 'д', 'ь', 'б'}, new char[] {'а', 'п', 'с', 'и', '_'},
            new char[] {'6', '7', 'е', 'г', 'п', 'р'}, new char[] {'г', 'ш', 'р', 'л', 'т', 'ь'},
            new char[] {'е', 'н', 'а', 'р', 'м', 'и'}, new char[] {'н', 'г', 'п', 'о', 'и', 'т'},
            new char[] {'в', 'а', 'ч', 'м', '_'}, new char[] {'р', 'о', 'и', 'ь', '_'},
            new char[] {'3', '4', 'ц', 'к', 'ы', 'в'}, new char[] {'й', 'ц', 'ы', 'я'},
            new char[] {'-', '=', 'з', 'ъ', 'ж', 'э'}, new char[] {'2', '3', 'й', 'у', 'ф', 'ы'},
            new char[] {'ы', 'в', 'я', 'с', '\\', '_'}, new char[] {'8', '9', 'г', 'щ', 'о', 'л'},
            new char[] {'9', '0', 'ш', 'з', 'л', 'д'}, new char[] {'=', '\\', 'х', 'э', 'ь'},
            new char[] {'ц', 'у', 'ф', 'в', 'я', 'ч'}, new char[] {'о', 'л', 'т', 'б', '_', 'ъ'},
            new char[] {'х', 'ъ', 'ж', '_'}, new char[] {'д', 'ж', 'б', '_'},
            new char[] {'ф', 'ы', 'ч', '\\'} },
            EngCharTable_closest = new char[][] {
            new char[] {'q', 'w', 's', 'z'}, new char[] {'g', 'h', 'v', 'n', '_'},
            new char[] {'d', 'f', 'x', 'v', '_'}, new char[] {'e', 'r', 's', 'f', 'x', 'c'},
            new char[] {'3', '4', 'w', 'r', 's', 'd'}, new char[] {'r', 't', 'd', 'g', 'c', 'v'},
            new char[] {'t', 'y', 'f', 'h', 'v', 'b'}, new char[] {'y', 'u', 'g', 'j', 'b', 'n'},
            new char[] {'8', '9', 'u', 'o', 'j', 'k'}, new char[] {'u', 'i', 'h', 'k', 'n', 'm'},
            new char[] {'i', 'o', 'j', 'l', 'm'}, new char[] {'o', 'p', 'k'},
            new char[] {'j', 'k', 'n', '_'}, new char[] {'h', 'j', 'b', 'm', '_'},
            new char[] {'9', '0', 'i', 'p', 'k', 'l'}, new char[] {'0', '-', 'o', '[', 'l'},
            new char[] {'1', '2', 'w', 'a', 's'}, new char[] {'4', '5', 'e', 't', 'd', 'f'},
            new char[] {'w', 'e', 'a', 'd', 'z', 'x'}, new char[] {'5', '6', 'r', 'y', 'f', 'g'},
            new char[] {'7', '8', 'y', 'i', 'h', 'j'}, new char[] {'f', 'g', 'c', 'b', '_'},
            new char[] {'2', '3', 'q', 'e', 'a', 's'}, new char[] {'s', 'd', 'z', 'c', '\\', '_'},
            new char[] {'6', '7', 't', 'u', 'g', 'h'}, new char[] {'a', 's', 'x', '\\'},
            };

        public static char[] 
            RusCharTable_zvon_pairs = {
            'я', 'п', 'ф', 'к', 'т', 'э', 'о', 'ш', 'с', 'ы', 'й', 'г', 'л', 'м', 'н', 'ё', 'б',
            'р', 'з', 'д', 'ю', 'в', 'х', 'ц', 'ч', 'ж', 'щ', 'ь', 'и', 'ъ', 'е', 'у', 'а' },
            EngCharTable_zvon_pairs = {
             'a', 'p', 'c', 't', 'e', 'v', 'k', 'h', 'i', 'w', 'g', 'r', 'n', 'm',
            'o', 'b', 'q', 'l', 'z', 'd', 'u', 'f', 'j', 'x', 'y', 's'   };

        public static char[] 
            RusCharTable_udarn_pairs = {
            'о', 'б', 'в', 'г', 'д', 'и', 'о', 'ж', 'з', 'е', 'й', 'к', 'л', 'м', 'н', 'а', 'п',
            'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'е', 'ю', 'я' },
            EngCharTable_udarn_pairs = {
            'o', 'b', 'c', 'd', 'i', 'f', 'g', 'h', 'y', 'j', 'k', 'l', 'm', 'n',
            'a', 'p', 'q', 'r', 's', 't', 'a', 'v', 'w', 'x', 'i', 'z'   };


    }
}
