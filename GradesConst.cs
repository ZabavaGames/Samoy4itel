namespace MyMobileProject1 {

	public enum grades { beforeschool, novice, shkolnik, gramotei, otlichnik, vypusknik, uchitel, master, professor, akademik, volshebnik, bog };
	public enum ranks { zero, light, medium, advanced, hard };

	public static class GradesConst {

		public const int MinGrade = (int)grades.beforeschool;
		public const int MaxGrade = (int)grades.volshebnik;
		public static string[] GradeStrings = {"beforeschool", "novice", "shkolnik", "gramotei", 
			"otlichnik", "vypusknik", "uchitel", "master", "professor", "akademik", "volshebnik",
			"bog"};
		public static string[] GradeStringsRus = {"Дошколёнок", "Новичок", "Школьник", "Грамотей",
			"Отличник", "Выпускник", "Учитель", "Мастер", "Профессор", "Академик", "Волшебник", 
			"<нет>"};

		public static int[] StarsToPromote =  {0, 1, 7, 20, 30, 50, 80, 120, 180, 240, 320 };
//		public static int[] StringsPerLevel = {3, 5, 5, 10, 10, 15, 15, 20,  20,  25,  30 };
//		public static int[] ErrorsPerLevel =  {1, 1, 1, 1,  1,  2,  2,  2,   3,   3,   4 };
		public static int[] StringsPerLevel = {3, 5, 5, 5, 10, 10, 10, 15,  15,  20,  20 };
		public static int[] ErrorsPerLevel =  {1, 1, 1, 1,  2,  2,  2,  3,   3,   3,   4 };
		public static int[] RankGradeReq =  {(int)grades.beforeschool, (int)grades.shkolnik, (int)grades.gramotei, (int)grades.otlichnik};
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

		public static string[] LetterButtonName = { "ButtonL (", ")" };
	//	public const string Button20 = "ButtonL (20)";
	//	public const string Button120 = "ButtonL (120)";

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
							Scene1 = "ruslesson", 
							Scene2 = "ruslesson2";


	}

}
