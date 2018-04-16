using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
using System.IO;


namespace MyMobileProject1 {

public enum SposobPodgotovki { Random4ik, StepByStep, OneFromAll, BlocksFive};

public class RusLesson1 : MonoBehaviour {

	public char[,] Mass_show, Mass_corr;
	private int Mraw, Mcol, Mcount, Lnumber;

	private static string[] Messages;
	private static char[] EndSigns = { '.', '!', '?' };
	private static char[] Ends = { '\0', '\n' };
	private static char[] SpaceAndEnds = { ' ', '"', '\'', '.', '!', '?', '\0', '\n' };
	private const string title = ", задание ";
	private const string EndofPool = "Ты решил все примеры этого уровня. Попробуй более сложный!";
	private const string EndofPool1 = "Ты прошёл этот уровень. Можешь начать сначала!";
	private const string EndofPool2 = "Или попробуй новый уровень сложности! Для этого начни новую игру. Твой прогресс сохранится.";
	private const string EndofPool3 = "Постарайся исправить больше ошибок, чтобы открыть более сложный уровень.";
	private const string HardHelp1 = "Ты выбрал сложный уровень. Он отличается от предыдущих.";
	private const string HardHelp2 = "В этом режиме игры ты увидишь примеры из словаря русских пословиц и поговорок со случайными ошибками в них. Больше никаких повторов - количество примеров бесконечно!";
	private static string[] Helpmsg0 = new string[] { 
		"Безударная гласная!",
		"Замени букву!",
		"Лишняя буква!"
		 };
	private static string[] Helpmsg1 = new string[] { 
//		"Замени букву, чтобы одно слово превратить в другое!",
//		"Проверь ударения и исправь безударную гласную!",
//		"Исправь согласную - звонкую на глухую, или наоборот",
//		"Напиши правильно словарное слово!",
//		"В одном из слов есть лишняя буква. Вычеркни ее!",
//		"Вспомни правило, чтобы писать правильно!"
		"Замени букву!",
		"Безударная гласная!",
		"Звонкая или глухая?",
		"Словарное слово!",
		"Лишняя буква!",
		"Вспомни правило!"
		 };

	private const string StringsPath = "Strings/";
	private const string msgfile = "messages.txt";
	private const string Level1Path = "1Уровень/";
	private const string Level21Path = "2Уровень/1ошибка/";
	private const string Level22Path = "2Уровень/2ошибки/";
	private const string Level3Path = "3Уровень/";
	private const string Level4Path = "4Уровень/";
	private static string[] grade0files_show = new string[] {Level1Path + "0_Ош_Дошколёнок.txt"};  
	private static string[] grade0files_corr = new string[] {Level1Path + "0_В_Дошколёнок.txt"};
	private static string[] grade1files_show = new string[] {Level1Path + "1_Ош_001_зам15.txt", Level1Path + "1_Ош_002_пров15.txt", Level1Path + "1_Ош_003_согл15.txt", Level1Path + "1_Ош_004_слов15.txt", Level1Path + "1_Ош_005_лиш15.txt", Level1Path + "1_Ош_006_прав15.txt"}; 
	private static string[] grade1files_corr = new string[] {Level1Path + "1_В_001_зам15.txt", Level1Path + "1_В_002_пров15.txt", Level1Path + "1_В_003_согл15.txt", Level1Path + "1_В_004_слов15.txt", Level1Path + "1_В_005_лиш15.txt", Level1Path + "1_В_006_прав15.txt"}; 
	private static string[] grade2files_show = new string[] {Level21Path + "2_1_Ош_001_зам20.txt", Level21Path + "2_1_Ош_002_пров10.txt", Level21Path + "2_1_Ош_003_согл10.txt", Level21Path + "2_1_Ош_004_слов10.txt", Level21Path + "2_1_Ош_005_лиш20.txt"}; 
	private static string[] grade2files_corr = new string[] {Level21Path + "2_1_В_001_зам20.txt", Level21Path + "2_1_В_002_пров10.txt", Level21Path + "2_1_В_003_согл10.txt", Level21Path + "2_1_В_004_слов10.txt", Level21Path + "2_1_В_005_лиш20.txt"}; 
	private static string[] grade2_files_show = new string[] {Level22Path + "2_2_Ош_001_зам20.txt", Level22Path + "2_2_Ош_002_пров30.txt", Level22Path + "2_2_Ош_003_согл30.txt", Level22Path + "2_2_Ош_004_слов30.txt", Level22Path + "2_2_Ош_005_лиш20.txt"}; 
	private static string[] grade2_files_corr = new string[] {Level22Path + "2_2_В_001_зам20.txt", Level22Path + "2_2_В_002_пров30.txt", Level22Path + "2_2_В_003_согл30.txt", Level22Path + "2_2_В_004_слов30.txt", Level22Path + "2_2_В_005_лиш20.txt"}; 
	private static string[] grade3files_show = new string[] {Level3Path + "3_2_Ош_001_70", Level3Path + "3_3_Ош_001_60"}; 
	private static string[] grade3files_corr = new string[] {Level3Path + "3_2_В_001_70", Level3Path + "3_3_В_001_60"}; 
	private string[][] sourcefiles_show;
	private string[][] sourcefiles_corr;

	private int Grade;
	private int SessionScore, ErrorsCorrected, SessionPhraseNumber, SessionLimit, LessonRank, 
				SavedPosition, StartSavedPosition, TheGapInFile, TimeBestSec, TimeCapMsec;
	private double SessionTime;
	private bool LessonStarted, SessionStarted, FirstRun, PromotionFlag, PoolEndFlag;
	private SposobPodgotovki SortingMethod;
	private DateTime TimePoint = DateTime.MinValue, TimeStop;

	private GridLayoutGroup LettersGrid;
	private Button[] BtList;
	private Button Helper, Timer;
	private Image HelpImg; 
	private Text DText, TText, LessonTitle;
	private GameObject Dialogue, TimeString;
	private Sprite SpriteRedline1, SpriteRedline2, SpriteEmpty, SpriteBlick, SpriteCheckmark, SpriteHelp;
	private Color HelpColor, HelpBlinkColor, TextColor, TextBlinkColor, ErrorColor;

	private struct Lesson {
		public string[] Lstr_show, Lstr_corr;
		public int Lcount, CurIndex;
		public List<int> Lesson1b;

		public void InitLesson (string[] show, string[] corr) {
			Lstr_show = show;
			Lstr_corr = corr;
			CurIndex = 0;
			Lcount = show.Length;
			Lesson1b = InitLessonB (Lcount);
			}

		public List<int> InitLessonB (int number) {
			List<int> temp = new List<int>();
			for (int i = 0; i < number; i++)
				temp.Add (i);
			return temp;
		}
	}
	private Lesson Lesson1 = new Lesson ();

	public Popup PopupW;
	public bool EscapeSupported;


	// Use this for initialization
	void Start () {

		LessonStarted = SessionStarted = FirstRun = PromotionFlag = PoolEndFlag = false;
		Grade = PlayerPrefs.GetInt (GradesConst.grade);
		LessonRank = PlayerPrefs.GetInt (GradesConst.rank);
//		int lessonN = PlayerPrefs.GetInt (GradesConst.scene);
//		Scene s = SceneManager.GetActiveScene ();
//		if (String.Compare (s.name, Scene1) == 0) {

		if (Application.platform == RuntimePlatform.Android || 
			Application.platform == RuntimePlatform.WindowsPlayer ||
			Application.platform == RuntimePlatform.WindowsEditor)
			EscapeSupported = true;
		else EscapeSupported = false;

		string s1 = PlayerPrefs.GetString (GradesConst.firstrun);
		if (string.Compare (s1, GradesConst.started) == 0) {
			PlayerPrefs.SetString (GradesConst.firstrun, GradesConst.done);
			FirstRun = true;
			}

		sourcefiles_show = new string[][] { grade0files_show, grade1files_show, MergeStringArrays (grade2files_show, grade2_files_show), grade3files_show };
		sourcefiles_corr = new string[][] { grade0files_corr, grade1files_corr, MergeStringArrays (grade2files_corr, grade2_files_corr), grade3files_corr };

	//	LoadStrings_noRanks ();
		LoadStrings_byRanks ();

		StartSession (0, false);
		}

	private void Init () {

		LessonStarted = true;

		Timer = GameObject.Find ("Timer").GetComponent<Button>();
		Helper = GameObject.Find ("Helper").GetComponent<Button>();
		HelpImg = GameObject.Find ("HelpImg").GetComponent<Image>();
		HelpColor = HelpImg.color;
		HelpBlinkColor = new Color (1,1,1,1);
		Dialogue = GameObject.Find ("Dialogue");
		DText = Dialogue.GetComponentInChildren<Text>();
		Dialogue.gameObject.SetActive (false);
		TimeString = GameObject.Find ("TimeString");
		TText = TimeString.GetComponentInChildren<Text>();
		LessonTitle = GameObject.Find ("TitleLabel").GetComponent<Text>();
		SpriteRedline1 = GameObject.Find ("red1").GetComponent<Image>().sprite;
		SpriteRedline2 = GameObject.Find ("red2").GetComponent<Image>().sprite;
		SpriteEmpty = GameObject.Find ("empty").GetComponent<Image>().sprite;
		SpriteBlick = GameObject.Find ("blick").GetComponent<Image>().sprite;
		SpriteCheckmark = GameObject.Find ("checkmark").GetComponent<Image>().sprite;
		SpriteHelp = GameObject.Find ("help?").GetComponent<Image>().sprite;

		GameObject grid = GameObject.Find ("LettersGrid");
		LettersGrid = grid.gameObject.GetComponent <GridLayoutGroup> ();
		TextColor = Color.white;
		TextBlinkColor = Color.gray;
		ErrorColor = Color.red;
		
		InitNumbers ();
	}

	private void InitNumbers () {
		BtList = LettersGrid.GetComponentsInChildren <Button>();

		Mcount = BtList.Length;
		Mraw = GradesConst.RawsOnScreen;   // Ааааа! константа...
		Mcol = Mcount/Mraw;

//		for (int i = 0; i < Mcount; i++)
//			BtList[i].onClick.AddListener (() => CheckLetter (BtList[i], 0));
	}


	public void RestartSession () {
		// load previos state...
		;
		StartSession (StartSavedPosition, true);
	}

	public void NextSession () {
		StartSession (SavedPosition, false);
	}

	private void StartSession (int startpos, bool restart) {
Debug.Log ("session stаrted " + startpos);
		if (PromotionFlag) 
			Reload ();   // если произошло повышение - перезагрузить сцену

		if (!LessonStarted) 
			Init ();  	// типа конструктор

	// инициируем переменные для подсчета очков и т.д.
		SessionScore = ErrorsCorrected = SessionPhraseNumber = 0;
		TimeBestSec = GradesConst.BestTimePerPhrase [LessonRank] / 1000;
Debug.Log ("time best is " + TimeBestSec);
		TimeCapMsec = GradesConst.TimeCapPerPhrase;
		SessionLimit = GradesConst.StringsPerLevel[Grade];
		SessionTime = 0.0;
	//	ShowTimer (0f);
		TText.text = "0:0";
		ShowTitle (0, 0);

		// для харда - автогенерация примеров на текущую сессию (если не повтор); 
		// в остальных уровнях все грузится на старте сцены
		if (LessonRank >= (int)ranks.hard && !restart) {
			GenerateStrings ();   
			if (!PlayerPrefs.HasKey (GradesConst.hard_help)) {
				PlayerPrefs.SetInt (GradesConst.hard_help, 1);
				PopupW.HelpWindow (HardHelp1, HardHelp2, null);
				}
			}

		// save state for reload
		SavedPosition = StartSavedPosition = startpos;

		if (FirstRun || Grade == GradesConst.MinGrade) {
		// окно с хелпом для первого запуска (два!)
			PopupW.HelpWindow (string.Empty, string.Empty, PopupW.HelpWindow2);
			DrawLesson ();
			}
		else if (!restart && GetSession (LessonRank)) {
		// считали сохраненную сессию
UnityEngine.Debug.Log ("Load session");
			DrawLesson ();
			}
		else {
		// начали новую сессию
			DrawLesson ();
			}
	}

	private void ShowTitle (int k, int m) {
		LessonTitle.text = GradesConst.GradeStringsRus [Grade] + title + k + "/" + m;
	}

	// грузим примеры в соответствии со старой системой прогрессии игрока
/*	private void LoadStrings_noRanks () {
		string[] filenames1, filenames2; 

		Messages = LoadStringsFromFile (msgfile);

		SortingMethod = SposobPodgotovki.Random4ik;
		SavedPosition = 0;

		if (Grade == GradesConst.MinGrade) {
			LessonRank = 0; 
			filenames1 = grade0files_show;
			filenames2 = grade0files_corr;
			}
//		else if (lessonN == 1) {
//			LessonRank = 1;
//			filenames1 = new string[] {"ruslesson1_show.txt"}; 
//			filenames2 = new string[] {"ruslesson1_corr.txt"};
//			}
//		else if (lessonN == 2) {
//			LessonRank = 2;
//			filenames1 = new string[] {"ruslesson2o_show.txt", "ruslesson2s_show.txt"}; 
//			filenames2 = new string[] {"ruslesson2o_corr.txt", "ruslesson2s_corr.txt"};
//			}
		else if (Grade < (int)grades.gramotei) {
			LessonRank = 1;
			SortingMethod = SposobPodgotovki.OneFromAll;
			if (PlayerPrefs.HasKey (GradesConst.gd1sp))
				SavedPosition = PlayerPrefs.GetInt (GradesConst.gd1sp);
			filenames1 = grade1files_show;
			filenames2 = grade1files_corr;
			}
		else if (Grade < (int)grades.otlichnik) {
			LessonRank = 2;
			SortingMethod = SposobPodgotovki.OneFromAll;
			if (PlayerPrefs.HasKey (GradesConst.gd2sp))
				SavedPosition = PlayerPrefs.GetInt (GradesConst.gd2sp);
			filenames1 = grade2files_show;
			filenames2 = grade2files_corr;
			}
		else if (Grade < (int)grades.vypusknik) { // || Grade == (int)grades.akademik) {
			LessonRank = 2;
		//	SortingMethod = SposobPodgotovki.OneFromAll;
		//	if (PlayerPrefs.HasKey (GradesConst.gd2sp))
		//		SavedPosition = PlayerPrefs.GetInt (GradesConst.gd2sp);
			SortingMethod = SposobPodgotovki.Random4ik;
			filenames1 = grade2_files_show;
			filenames2 = grade2_files_corr;
			}
		else if (Grade < (int)grades.uchitel) {
			LessonRank = 3;
			if (PlayerPrefs.HasKey (GradesConst.gd3sp))
				SavedPosition = PlayerPrefs.GetInt (GradesConst.gd3sp);
			filenames1 = MergeStringArrays (grade1files_show, 
							MergeStringArrays (grade2files_show, grade2_files_show));
			filenames2 = MergeStringArrays (grade1files_corr,
 							MergeStringArrays (grade2files_corr, grade2_files_corr));
			}
		else if (Grade == GradesConst.MaxGrade) {
			LessonRank = 10;
			// здесь должно быть что-то особенное?..
			filenames1 = new string[] {"TOPSECRET!"}; 
			filenames2 = new string[] {"TOPSECRET!"};
			}
		else {
			LessonRank = 10;
			// 
			filenames1 = new string[] {"TOPSECRET!"}; 
			filenames2 = new string[] {"TOPSECRET!"};
			}

		string[] show;
		string[] corr;
		if (LessonRank < 10) {
			show = LoadLstrings (filenames1);
			corr = LoadLstrings (filenames2);
//			Lesson1 = new Lesson ();
			Lesson1.InitLesson (show, corr);
			}
	//	else
	//		GenerateStrings ();   // автогенерация примеров

	}
*/

	// новая система с уровнями сложности, в зав-ти от которых грузим примеры из разных файлов
	private void LoadStrings_byRanks () {
		string[] filenames1, filenames2; 

		Messages = LoadStringsFromFile (msgfile);

		SortingMethod = SposobPodgotovki.Random4ik;
//		SavedPosition = 0;

		if (LessonRank == (int)ranks.zero && Grade != GradesConst.MinGrade)
			LessonRank = (int)ranks.light;

		if (Grade == GradesConst.MinGrade || LessonRank == (int)ranks.zero) {
			SortingMethod = SposobPodgotovki.StepByStep;
//			filenames1 = grade0files_show;
//			filenames2 = grade0files_corr;
			}
		else if (LessonRank == (int)ranks.light) {
			SortingMethod = SposobPodgotovki.BlocksFive;
//			filenames1 = grade1files_show;
//			filenames2 = grade1files_corr;
			}
		else if (LessonRank == (int)ranks.medium) {
			SortingMethod = SposobPodgotovki.OneFromAll;
//			filenames1 = grade2files_show;
//			filenames2 = grade2files_corr;
			}
		else if (LessonRank == (int)ranks.advanced) {
			SortingMethod = SposobPodgotovki.OneFromAll;
//			filenames1 = grade2_files_show;
//			filenames2 = grade2_files_corr;
			}
		else if (Grade == GradesConst.MaxGrade || LessonRank >= (int)ranks.hard) {
			// раньше был рандом, но для харда он и так рандом...
			SortingMethod = SposobPodgotovki.StepByStep;
			// здесь должно быть что-то особенное?..
			filenames1 = new string[] {"TOPSECRET!"}; 
			filenames2 = new string[] {"TOPSECRET!"};
			}
		else {
			filenames1 = new string[] {"TOPSECRET!"}; 
			filenames2 = new string[] {"TOPSECRET!"};
			}

		// прогружаем строки из файлов
		if (LessonRank < (int)ranks.hard) {
			filenames1 = sourcefiles_show[LessonRank];
			filenames2 = sourcefiles_corr[LessonRank];
			string[] show = LoadLstrings (filenames1);
			string[] corr = LoadLstrings (filenames2);
			Lesson1.InitLesson (show, corr);
		//	TheGapInFile = Lesson1.Lcount / filenames1.Length;
		//  больше не нужен
			}
	//	else
	//		GenerateStrings ();   // автогенерация примеров
    // перенес в сессию

		// для легкого уровня нужно разбиение по блокам-пятеркам
		if (SortingMethod == SposobPodgotovki.BlocksFive) {
			Lesson1 = PrepareBlocksFive (Lesson1, LessonRank);
			SortingMethod = SposobPodgotovki.StepByStep;
			}
		else if (SortingMethod == SposobPodgotovki.OneFromAll) {
			Lesson1 = PrepareFivesCircle (Lesson1, LessonRank);
			SortingMethod = SposobPodgotovki.StepByStep;
			}

	}

	private string[] LoadLstrings (string[] filenames) {
		string[] result; 
	//	List<string[]> result_list = new List<string[]> ();
		List<string> result_final = new List<string> ();

		foreach (string fname in filenames) {
			result = LoadStringsFromFile (fname);
			for (int j = 0; j < result.Length; j++)
				result_final.Add (result[j]);
//			result_list.Add (result);
			}
//		for (int i = 0; i < result_list.Count; i++) {
//			result = result_list[i];
//			for (int j = 0; j < result_list[i].Length; j++)
//				result_final.Add (result[j]);
//			}

		return result_final.ToArray ();
	}

	private string[] LoadStringsFromFile (string filename) {
		string s = filename.Split ('.')[0];
		TextAsset t = Resources.Load (StringsPath + s, typeof (TextAsset)) as TextAsset;
		string[] s1 = t.text.Split ('\n');

		return s1;
	}

	// слепить строковыe массивы в один
	private string[] MergeStringArrays (string[] a1, string[] a2) {
		int size = a1.Length + a2.Length;
		string[] aa = new string[size];
		a1.CopyTo (aa, 0);
		a2.CopyTo (aa, a1.Length);
		return aa;
	}

	// подбираем случайный комплект строк для текущего задания, по параметрам
	private void GenerateStrings () {
		PairString ps = new PairString ();
		ps.Correct = ps.Show = string.Empty;
		string[] show, corr;
		List<string> result1 = new List<string> ();
		List<string> result2 = new List<string> ();

		// нужно ли делать членом основного класса, или оставить здесь? 
		// (экземпляр создается при каждом вызове ф-ии)
		PhraseGenerator PGen = new PhraseGenerator (Level4Path);

		var limit = GradesConst.StringsPerLevel [Grade];
		var errors = GradesConst.ErrorsPerLevel [Grade];
		// не меньше 2-х ошибок для генератора
		errors = Math.Max (GradesConst.MinGenericErrors, errors);
		int[] output = { Mraw, Mcol };
		// в цикле набираем нужное кол-во примеров в пул
		for (int i = 0; i < limit; i++) {
			// генерим рандомный пример с заданным кол-вом ошибок
			ps = PGen.GetRandomPhrase (SposobPodgotovki.Random4ik, (int)RuleSet.Random, output, errors);
			result1.Add (ps.Show);
			result2.Add (ps.Correct);
			}
		show = result1.ToArray ();
		corr = result2.ToArray ();

		Lesson1.InitLesson (show, corr);
	}


//  главная функция, подбирает строку и выводит ее на экран
	public void DrawLesson () {

		Mass_corr = new char[Mraw, Mcol];    // инициируем массив нулями (каждый раз, типа очистка)
		Mass_show = new char[Mraw, Mcol];       
		//  выбираем случайную фразу; здесь должен быть хитрый алгоритм выбора...
		//  var 1
		//	Lnumber = Random4ik (Lesson1.Lcount);   
		//  var 2
		//	Lnumber = GetNextPhrase (Lesson1.Lcount);
		//  а вот и он - алгоритм
		if (SortingMethod == SposobPodgotovki.Random4ik)		
			Lnumber = RandPhraseCut (Lesson1);
//		else if (SortingMethod == SposobPodgotovki.OneFromAll)
//			Lnumber = GetOnePhrase_FromAllFiles (Lesson1);
//		else if (SortingMethod == SposobPodgotovki.BlocksFive)
//			Lnumber = GetPhrase_fromBlock (Lesson1);
//		else if (SortingMethod == SposobPodgotovki.StepByStep)
		else
			Lnumber = GetNextPhrase (Lesson1.Lcount);

		string show = Lesson1.Lstr_show[Lnumber];
		string corr = Lesson1.Lstr_corr[Lnumber];
		bool flag = InitNewLesson (Mass_show, show);	// в 1й пишем выбранную фразу
		if (!flag) {  // если не влезает, корректируем
			Podgonka (LettersGrid, show, corr);  // на самом деле, пока не работает...
			InitNewLesson (Mass_show, show);
			}	
		InitNewLesson (Mass_corr, corr); // а во 2й записываем корректный вариант

		Text tt;		
		Button b;
		Image img;
		// вывод на экран
		for (int i = 0; i < Mraw; i++)
			for (int j = 0; j < Mcol; j++) {
				b = BtList[i*Mcol + j];
				tt = b.GetComponentInChildren<Text>(); 
				tt.color = TextColor;
				tt.text = Mass_show[i, j].ToString ();       // записываем текст в кнопки
				if ((img = b.GetComponentInChildren<Image>()) != null) {
					img.sprite = SpriteEmpty;     // убираем красные черточки
				//	img.color.a = 0f;		// делаем прозрачным
					}
				}
		
		//  заглавная буква
	//	SetFirstLetter ();

		SessionPhraseNumber++;
		// запускаем счетчик времени
		TimePoint = DateTime.Now;
		HelpImg.sprite = SpriteHelp;
		HelpImg.color = HelpColor;

		// в названии пишем вол-во фраз сделано/всего
		ShowTitle (SessionPhraseNumber, GradesConst.StringsPerLevel [Grade]);
		// для новичков - подсказки
		if (LessonRank == (int)ranks.zero) //  для дошколенка
			ShowDialogueString (Helpmsg0 [SessionPhraseNumber-1], GradesConst.TimeToShowHelp);
		else if (LessonRank == (int)ranks.light) {
			int files = sourcefiles_show [LessonRank].Length;
			const int five = GradesConst.Categories; 
			if (Grade <= (int)grades.gramotei && Lnumber < files * five) {  
			// по следам ф-ии PrepBlocksFive
				int x = Lnumber / five;
				int y = x % files;
				ShowDialogueString (Helpmsg1 [y], GradesConst.TimeToShowHelp);
				}
			}

		// и сразу сохраняем сессию (при выходе тоже сохраняем)
		SessionStarted = true;
		SaveSession (LessonRank);

	}

	//  попытка сделать первую букву (заглавную) больше других, чтобы не налезала
/*	private void SetFirstLetter () {
		Button b1 = GetButton_ByName (GradesConst.Button20);
		Button b2 = GetButton_ByName (GradesConst.Button120);
		Text t2 = b2.GetComponentInChildren<Text>();
		Text t1 = b1.GetComponentInChildren<Text>();
		t2.text = t1.text;
		t1.text = String.Empty;
		t2.color = TextColor;
		Image img;
		if ((img = b2.GetComponentInChildren<Image>()) != null)
			img.sprite = SpriteEmpty; 
	}
*/

//  забиваем в массив символы так, чтобы это корректно отображалось в поле ввода
	private bool InitNewLesson (char[,] mass, string s) {   
		string s1 = s;
		int ind;
		if ((ind = s.IndexOfAny (Ends)) >= 0)
	 	// убираем всю фигню в конце фразы
			s1 = s.Split (Ends)[0] + s[ind].ToString ();  

		bool flag = true;
		char[] cc;
		int d, j = 0;
		// разбиваем фразу на слова
		string[] ss = s1.Split (' ');  		

		for (int i = 0; i < ss.Length; i++) {
			ss[i].Trim ();
			d = ss[i].Length;
			cc = ss[i].ToCharArray ();
			// проверяем, умещается ли слово на текущей строке
			// если нет, передвигаем индекс до начала след. строки  
			if ((j%Mcol + d) > Mcol) 
				j += Mcol - j%Mcol;          
			// посимвольно копируем слово в массив
			for (int z = 0; z < cc.Length; z++)   
				flag = AddSymbol (mass, j++, cc[z]);
			// и добавляем пробел, если это не конец строки
			if (j%Mcol > 0)        				
				flag = AddSymbol (mass, j++, ' ');
			// проверка на пределы
			if (d >= Mcol || j > Mcount/2) {  // j/Mcol >= Mraw/2 || 
				// туши свет
				flag = false;				
Debug.Log ("InitNewLessonФраза не поместилась!   " + s + "   Всего слов " + d + " инд " + j + "/" + Mcount/2);
				break;
				}
			}
		return flag;
	}

	private void Podgonka (GridLayoutGroup grid, string s1, string s2) {
//		if (!PhraseFit_OnScreen (mass, s))
	// меняем размер поля по длине фразы; на будущее...
		Button Bt1 = BtList [0];
		int fitsize = 101;
		if (grid != null) {
			for (int i = Mcount; i < fitsize; i ++) {
			//	Button b = Instantiate (Bt1);
				;	
				}
			InitNumbers ();
			}
	}

/*	private bool PhraseFit_OnScreen (char[,] mass, string s) {
		bool flag = true;

		return flag;
	} */

	private int Random4ik (int number) {
		return UnityEngine.Random.Range (0, number);
	}

	// случайно выбираем фразу из пула примеров, но так, чтобы использовать все фразы по одному
	// разу, прежде чем зайти на новый круг; для этого создаем "обрезанный" дубликат пула LessonB
	private int RandPhraseCut (Lesson L1) {
		List<int> L1b = L1.Lesson1b; 
		int i = L1b.Count;
		if (i > 0) {
			int r = Random4ik (i);
			int k = L1b [r];
			L1b.RemoveAt(r);
			L1.CurIndex = k;
			return k;
			}
		else {
			PoolEndFlag = true;
			ShowDialogueString (EndofPool, GradesConst.TimeToShowHelp);
			L1.Lesson1b = L1.InitLessonB (L1.Lcount);	
			return RandPhraseCut (L1);
			}
	}

	private int GetNextPhrase (int number) {
		int ind = SavedPosition;
	//	SavedPosition = (SavedPosition < number-1) ? SavedPosition+1 : 0;
	// альтернативка
	//	SavedPosition = (SavedPosition++) % (number-1);
		if (SavedPosition < number - 1)
			SavedPosition ++;
		else {
			SavedPosition = 0;
			PoolEndFlag = true;
			ShowDialogueString (EndofPool, GradesConst.TimeToShowHelp);
		}
		return ind;
	}

	// выбираем по одной фразе из каждого отдельного файла с примерами, чтобы в задании было по
	// одной фразе каждого типа; поскольку все фразы уже свалены в один пул Lesson, то берем
	// фразы из пула через интервал (работает только для файлов с одинаковым кол-вом примеров);
	// промежуток (TheGapInFile) высчитывается на этапе загрузки файлов.
	// Также сохраняем позицию в пуле, чтобы после запуска новой сессии или выхода в главное меню
	// продолжить с этого же места
/*	private int GetOnePhrase_FromAllFiles (Lesson L1) {
		int sp = SavedPosition;
		if (SavedPosition + TheGapInFile < L1.Lcount)
			SavedPosition += TheGapInFile;
		else SavedPosition = TheGapInFile - (L1.Lcount -1 - SavedPosition);

		return sp;
	} */

	// вариант 2, пересорт исходного массива
	// выбираем по 1й фразе из каждого файла, 5 файлов по кругу
	// пересоздаем массив в упорядоченном виде, при возвращении надо его присвоить взад
	private Lesson PrepareFivesCircle (Lesson L1, int lesson_rank) {
		Lesson L2 = new Lesson ();

		string[] show = new string[L1.Lcount],
				 corr = new string[L1.Lcount];
	//	const int five = GradesConst.Categories; // по кол-ву категорий (5)
		int files = sourcefiles_show [lesson_rank].Length;
		int fifteen = L1.Lcount / files;
		int index = 0, jndex;

		for (int i = 0; i < fifteen; i++) { 
			for (int k = 0; k < files; k++) {     // берем по фразе с каждого файла
				jndex = fifteen * k + i;
				show[index] = L1.Lstr_show[jndex];
				corr[index] = L1.Lstr_corr[jndex];
				index++;
				}		
			}
		L2.InitLesson (show, corr);
		return L2;
	}

	// работаем с блоками по 5 фраз из одного файла, 5 файлов по кругу, с подсказками
	// пересоздаем массив в упорядоченном виде, при возвращении надо его присвоить взад
	private Lesson PrepareBlocksFive (Lesson L1, int lesson_rank) {
		Lesson L2 = new Lesson ();

		string[] show = new string[L1.Lcount],
				 corr = new string[L1.Lcount];
		const int five = GradesConst.Categories; // по кол-ву категорий (5), ну или просто так
		int files = sourcefiles_show [lesson_rank].Length;
		int fifteen = L1.Lcount / files;
		int index = 0, jndex;

		for (int i = 0; i < fifteen; i += five) {  // берем блоками по пять
			for (int k = 0; k < files; k++) {     // берем по блоку с каждого файла
				for (int j = 0; j < five; j++) {  // берем блок из 5 фраз
					jndex = fifteen * k + i + j;
					show[index] = L1.Lstr_show[jndex];
					corr[index] = L1.Lstr_corr[jndex];
					index++;
					}
				}		
			}
		L2.InitLesson (show, corr);
		return L2;
	}


/*
	private void SaveFilePosition (int savpos) {
		string sp = string.Empty;
		if (LessonRank == 1) sp = GradesConst.gd1sp;
		else if (LessonRank == 2) sp = GradesConst.gd2sp;
		else if (LessonRank == 3) sp = GradesConst.gd3sp;
		if (sp != string.Empty)
			PlayerPrefs.SetInt (sp, savpos - 1);  // отматываем на 1 назад, чтобы вернуться на текущую фразу
Debug.Log (" savepos = " + savpos);
	}

	private int GetFilePosition (int lesson_rank) {
		int sp = 0;
		if (lesson_rank == (int)ranks.light && PlayerPrefs.HasKey (GradesConst.gd1sp))
			sp = PlayerPrefs.GetInt (GradesConst.gd1sp);
		else if (lesson_rank == (int)ranks.medium && PlayerPrefs.HasKey (GradesConst.gd2sp))
			sp = PlayerPrefs.GetInt (GradesConst.gd2sp);
		else if (lesson_rank == (int)ranks.advanced && PlayerPrefs.HasKey (GradesConst.gd3sp))
			sp = PlayerPrefs.GetInt (GradesConst.gd3sp);
Debug.Log (" loadsp = " + sp);
		return sp;
	}
*/

	// Update is called once per frame
	void Update () {
		if (SessionStarted) {
// перенес
		//	double thelp = GetTimer (TimeStop);
		//	if (thelp > GradesConst.HidePhraseTimer)   // спрятать реплику от предыдущего задания спустя 3 сек
		//		ShowDialogueString (null);
			double tsm = GetTimer (TimePoint); 
			ShowTimer (tsm);
		}

		if (EscapeSupported)  // ловим аппаратную кнопку
			if (Input.GetKeyDown (KeyCode.Escape)) {
				if (PopupW.MoreHelpWindow.gameObject.activeInHierarchy)
					PopupW.MoreHelpClose.onClick.Invoke ();
				else if (PopupW.MyPromWindow.gameObject.activeInHierarchy)
					PopupW.PromClose.onClick.Invoke ();
				else if (PopupW.MyEndSessionWindow.gameObject.activeInHierarchy)
					PopupW.EndSessionClose.onClick.Invoke ();
				else if (PopupW.MyHelpWindow.gameObject.activeInHierarchy)
					PopupW.HelpClose.onClick.Invoke ();
				else 
					Quit ();
				}
	}


	public bool CheckLetterIncorrect (Button b) {
		bool flag = false;
		// считываем кнопку, которую нужно проверить
		int num = GetBtNumber_ByName (b.gameObject.name);
		char c1 = GetSymbol (Mass_corr, num), 
			 c2 = GetSymbol (Mass_show, num);
		if (c1 != c2)     // сверяем символы в массивах
			flag = true;
		return flag;
	}
	
//  вторая главная ф-ия, проверяет и исправляет букву в выбранной кнопке
	private bool CorrectLetter (Button b, int dir) {
		if (CheckLetterIncorrect (b)) {
			int num = GetBtNumber_ByName (b.gameObject.name);
		 	int raw = num/Mcol;
			int col = num%Mcol;
			if (raw <= 0) {
				// Ooups!
				return false;
				}
			Button b2 = GetButton_ByNumber ((raw-1)*Mcol + col); // находим кнопку на 1 строку выше
			char c1 = GetSymbol (Mass_corr, num), 
				 c2 = GetSymbol (Mass_show, num);
			CorrectLetter_OnScreen (b, b2, c1, dir);     // исправляем на экране
			ReplaceSymbol (Mass_show, num, c1);   //  исправляем

			//  если фраза полностью корректна, выдаем бонус и следующее задание!
			if (PhraseCorrect (Mass_corr, Mass_show)) {
			//	Debug.Log ("Все верно!");
				
			//  останавливаем счетчик времени
				TimeStop = DateTime.Now;
				double tsm = GetTimer (TimePoint); 
				if (tsm > TimeCapMsec) tsm = TimeCapMsec;  // ограничение до 30сек
				SessionTime += tsm;
				ErrorsCorrected++;
				HelpImg.sprite = SpriteCheckmark;
				HelpImg.color = HelpBlinkColor;

			//  вызываем окно с результатом
				StartCoroutine (DisplayResult (tsm));
				}
			return true;
		}
		return false;
	}


	private double GetTimer (DateTime curtime) {
		TimeSpan ts = DateTime.Now - curtime; 
		return ts.TotalMilliseconds;
	}

	private void ShowTimer (double tsm) {
		int t = (int)Math.Round (tsm/1000, 0);  // переводим в сек. и округляем
		int min, sec;
		min = t/60;
		sec = t%60;
		TText.text = min + ":" + sec;
	}

	private void ShowDialogueString (string msg, int time) {
		StartCoroutine (DS_show (msg, time));
	}

	private IEnumerator DS_show (string msg, int time) {
		while (Dialogue.activeSelf && DText.text != msg)
			yield return null;
		if (msg != null) {
			Dialogue.SetActive (true);
			DText.text = msg;
			int wait = (time == 0) ? (GradesConst.HidePhraseTimer / 1000) : time;
			yield return new WaitForSeconds (wait);
		}	
		Dialogue.SetActive (false);
	}


	// результат одной сессии
	private IEnumerator DisplayResult (double tsm) {

		string msg = Messages [Random4ik (Messages.Length)];
	//	PopupW.EndOfTurn_Popup (msg, tsm);
	
		// пауза в 1сек, чтобы успели увидеть зачеркивание
		if (tsm > 0) {  // если фразу скипнули, то -1 (не показываем)
			if (Random4ik (5) == 2) {  // счастливый номер, 1 из 5
				ShowDialogueString (msg, 0);
				}
			yield return new WaitForSeconds (1.5f);
			}

		if (SessionPhraseNumber >= SessionLimit) {  // конец сессии, тушим свет, сливаем воду
			SessionScore = CalculateScore (SessionPhraseNumber, ErrorsCorrected, SessionTime);
			SessionStarted = FirstRun = false;
			int stars = 0;  // раздаем звезды в зав-ти от рейтинга
			if (SessionScore > GradesConst.SessionScore[3]) 	 stars = 3;
			else if (SessionScore > GradesConst.SessionScore[2]) stars = 2;
			else if (SessionScore > GradesConst.SessionScore[1]) stars = 1;
			else if (ErrorsCorrected >= SessionPhraseNumber)     stars = 1;

		//	PopupW.EndOfSession_Popup (SessionScore);
			if (stars > 0)
				ShowDialogueString (msg, 0);  // финальное поздравление

			int totals = PlayerPrefs.GetInt (GradesConst.stars);
			totals += stars;
			PlayerPrefs.SetInt (GradesConst.stars, totals);

			Grade = PlayerPromotion (totals, Grade);
			PlayerPrefs.SetInt (GradesConst.grade, Grade);
			
			ClearSavedSession (LessonRank);

			if (PoolEndFlag) {
				PoolEndFlag = false;
				if (LessonRank > (int)ranks.zero && LessonRank < (int)ranks.hard)
					ShowLevelEndInfo ();
				}

			PopupW.EndSessionWindow (string.Empty, string.Empty, stars, totals);
			// после чего либо выход в меню, либо nextsession (), либо restartsesion ()
			}
		else 
			DrawLesson ();
	}

	private void ShowLevelEndInfo () {
//		if ((LessonRank == (int)ranks.light && Grade >= (int)grades.shkolnik) ||
//			(LessonRank == (int)ranks.medium && Grade >= (int)grades.gramotei) ||
//			(LessonRank == (int)ranks.advanced && Grade >= (int)grades.otlichnik))
		if (Grade >= GradesConst.RankGradeReq[LessonRank])
			{
			PopupW.HelpWindow (EndofPool1, EndofPool2, null);
			}
		else 
			PopupW.HelpWindow (EndofPool1, EndofPool3, null);
	}

	// ф-ия подсчитывает рейтинг игрока в зависимости от кол-ва заданий и времени
	private int CalculateScore (int attempts, int successful, double time) {
		int errors = attempts - successful;
		int dovesok = errors * TimeBestSec;
		int Ttime = (int)Math.Round (time/1000, 0) + dovesok;
		int k1 = 100 * attempts * TimeBestSec / Ttime;
		int k2 = 100 * successful / attempts;
UnityEngine.Debug.Log (Ttime + " " + k1 + " " + k2);
		int score = (k1 + k2)/2;
		
		return score;  // как-то так...
	}

	public int PlayerPromotion (int totals, int grade) {
		// здесь смотрим, сколько звезд нужно на уровень выше
		if (Grade < GradesConst.MaxGrade && totals >= GradesConst.StarsToPromote[grade+1]) {  
			grade ++;
			string titul = GradesConst.GradeStringsRus[grade];
			PromotionFlag = true;
		//	Action prom = Reload;
			PopupW.PromotionWindow (string.Empty, string.Empty, titul, null);
			}
		return grade;
	}

	private void Reload () {
Debug.Log ("перезагрузка!!");
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}


//  добавляем один символ в наш массив
	private bool AddSymbol (char[,] arr, int j, char c) { 
		bool flag = false;
		int raw = (j/Mcol)*2 + 1;       // это нужно, чтобы пропускать четные строки в массиве, они 
									   //	будут нужны для исправлений
		int	col = j%Mcol;
		if (raw < Mraw && col < Mcol) {
			arr[raw, col] = c;
			flag = true;
			}
		return flag;
	}

//  простая замена символа в массиве
	private bool ReplaceSymbol (char[,] arr, int j, char c) { 
		bool flag = false;
		int raw = j/Mcol;
		int col = j%Mcol;
		if (raw < Mraw && col < Mcol) {
			arr[raw, col] = c;
			flag = true;
			}
		return flag;
	}

//  считываем символ из массива по заданному номеру кнопки
	private char GetSymbol (char[,] arr, int i) {
		int raw = i/Mcol;
		int	col = i%Mcol;
		if (raw < Mraw && col < Mcol)
			return arr[raw, col];
		else return '\0';
	}

	private void CorrectLetter_OnScreen (Button b1, Button b2, char letter, int dir ) {
		// заглавную букву подставляем
	//	if (String.Compare (b1.name, GradesConst.Button20) == 0)
	//		b1 = GetButton_ByName (GradesConst.Button120);

		Image img = b1.GetComponentInChildren<Image>();  // мы берем картинку из объекта BtBackground
		if (img != null) {
			if (dir == 0)
				img.sprite = SpriteRedline1;   // зачеркиваем букву красной линией 
			else if (dir == 1)
				img.sprite = SpriteRedline2;   
			else img.sprite = SpriteEmpty;   
			//	img.color.a = 255;			// делаем непрозрачным
			}
		//	Text t = b1.GetComponentInChildren<Text>();
		//	t.font = null;
		//	t.color = Color.red;
		Text t2 = b2.GetComponentInChildren<Text>();
		t2.color = ErrorColor;
		t2.text = letter.ToString();  //  подставляем правильную букву
	}

	// проверяем отсутствие ошибок во фразе
	private bool PhraseCorrect (char[,] mass_corr, char[,] mass_show) {
	// выбираем только нечетные строки (в них отображаются буквы)
		for (int i = 1; i < Mraw; i += 2)  
			for (int j = 0; j < Mcol; j++) {
				if (mass_corr[i, j] != mass_show [i, j])
					return false;
			}
		return true;
	}

/*	private bool CheckLetterYes (Button b) {
		ActiveCheck = true;
		CheckLetter (b);
		ActiveCheck = false;
		return CheckResult;
	} */

	public bool SwipeButton (Button b, int direction) {
		if (b != null && CheckLetterIncorrect (b)) {
			CorrectLetter (b, direction);
			return true;
		}
		else return false;
	}

	private int GetBtNumber_ByName (string s) {
		string s1 = string.Empty;
		foreach (char ch in s.ToCharArray()) 
			if(Char.IsNumber(ch))
				s1 += ch.ToString();
		return Convert.ToInt32 (s1);
	}

	private Button GetButton_ByNumber (int n) {
		string s1 = GradesConst.LetterButtonName[0] + n + GradesConst.LetterButtonName[1];
		return GameObject.Find(s1).GetComponent<Button>();
	}

	private Button GetButton_ByName (string s) {
		return GameObject.Find(s).GetComponent<Button>();
	}

	public bool ButtonIsVisible (Button b) {
		if (b != null) {
			int n = GetBtNumber_ByName (b.name);
			int raw = n / Mcol;
			if (raw%2 != 0) { // нечетные строки видны
				char c = GetSymbol (Mass_show, n); 
				int ind = c.ToString().IndexOfAny (SpaceAndEnds);
				if (ind < 0) 
			//	if (!char.IsWhiteSpace (c) && c != '\0' && c != ' ')  // в строке не пустой символ
					return true;
				}
			}
		return false;
	}


	// пропустить текущую фразу и перейти к следующей в задании
	public void SkipCurrent () {
		SessionTime += TimeCapMsec;
	//  вызываем окно с результатом
		StartCoroutine (DisplayResult (-1));	
	}

	// сохранить сессию, чтобы потом продолжить с прерванного места; 
	// отдельный слот для разных уровней сложности
	private void SaveSession (int lesson_rank) {
		string lr = lesson_rank.ToString ();
		PlayerPrefs.SetInt (lr + GradesConst.saved_session, 1);
		PlayerPrefs.SetInt (lr + GradesConst.save_rank, LessonRank);
		PlayerPrefs.SetInt (lr + GradesConst.save_position, SavedPosition);
		PlayerPrefs.SetInt (lr + GradesConst.prev_save_position, StartSavedPosition);
		PlayerPrefs.SetInt (lr + GradesConst.save_errors_corrected, ErrorsCorrected);
		PlayerPrefs.SetInt (lr + GradesConst.save_phrase_number, SessionPhraseNumber);
	//	PlayerPrefs.SetInt (lr + GradesConst.save_session_limit, SessionLimit);
		PlayerPrefs.SetFloat (lr + GradesConst.save_session_time, (float)SessionTime);
	//	if (LessonRank < (int)ranks.hard)		
	//		SaveFilePosition (SavedPosition);
	}

	private void ClearSavedSession (int lesson_rank) {
		string lr = lesson_rank.ToString ();
		PlayerPrefs.SetInt (lr + GradesConst.saved_session, 0);
	}

	// загрузить сессию, если зашли на тот же уровень сложности
	private bool GetSession (int lesson_rank) {
		bool state = false;
		string lr = lesson_rank.ToString ();
		if (PlayerPrefs.GetInt (lr + GradesConst.saved_session) == 1 
		 && PlayerPrefs.GetInt (lr + GradesConst.save_rank) == lesson_rank) {

			SavedPosition = PlayerPrefs.GetInt (lr + GradesConst.save_position);
			SavedPosition --;  // потому что в GetNextPhrase стоит ++;
		// а еще надо учесть, что она идет по кругу, и не может быть меньше 0...
			if (SavedPosition < 0) SavedPosition = 0;
			StartSavedPosition = PlayerPrefs.GetInt (lr + GradesConst.prev_save_position);

			SessionPhraseNumber = PlayerPrefs.GetInt (lr + GradesConst.save_phrase_number);
			SessionPhraseNumber --;  // потому что в drawlesson стоит ++
		//  лимит не нужен, т.к. он зависит от текущего уровня игрока, который может измениться
		//	SessionLimit = PlayerPrefs.GetInt (lr + GradesConst.save_session_limit);
			SessionTime = (double)PlayerPrefs.GetFloat (lr + GradesConst.save_session_time);
		//	SavedPosition = GetFilePosition (lesson_rank);
			state = true;
		}
		return state;
	}

	public void Quit () {
	//  конец урока, подсчет очков
		;
	//  если прервали, сохраняем сессию
		if (SessionStarted)		
			SaveSession (LessonRank);

	// освобождаем ресурсы (...), вызываем стартовое меню
		string s = PlayerPrefs.GetString (GradesConst.firstrun);
		if (string.Compare (s, GradesConst.started) == 0)
			PlayerPrefs.SetString (GradesConst.firstrun, GradesConst.done);

		SceneManager.LoadScene (GradesConst.Scene0);
	}

	// по кнопке (?) в интерфейсе
	public void GetSomeHelp () {
		BlinkAllWords (GradesConst.BlinkTime);
	}

	// все слова с ошибками блинкают
	private void BlinkAllWords (float seconds) {
		for (int i = 0; i < Mcount; i++) {
				if (GetSymbol (Mass_show, i) != GetSymbol (Mass_corr, i)) {
					int ind1 = GetWord_FirstIndex (Mass_show, i);
					int ind2 = GetWord_LastIndex (Mass_show, i);

					BlinkWord (ind1, ind2, seconds);
					}
		}
	}

// находим в массиве индекс первой буквы слова, в котором есть буква с индексом i
// (способ поиска 1)
	private int GetWord_FirstIndex (char[,] s, int i) {
		int k; 
		for (k = i; k > 0; k--) {
			char c = GetSymbol (s, k);
			int ind = c.ToString().IndexOfAny (SpaceAndEnds);
			if (ind >= 0) 
					break;
			}
		return k+1;
	}

// находим в массиве индекс последней буквы слова, в котором есть буква с индексом i
// (способ поиска 2)
	private int GetWord_LastIndex (char[,] s, int i) {
		int k;
		for (k = i; k < Mcount; k++)
				if (GetSymbol (s, k) == ' ') 
					break;
		return k;
	}

	private string GetWord (int start, int length) {
		string s = string.Empty;
		for (int i = start; i < length; i++) {
			char c = GetSymbol (Mass_show, i);
			s += c;
			}
		return s;
	}

	private void BlinkWord (int start, int length, float seconds) {
		for (int i = start; i < length; i++) {
		//	char c = GetSymbol (Mass_show, i);
			Button b = GetButton_ByNumber (i);
			if (b != null) {
				Text t = b.GetComponentInChildren<Text>();
				if (t != null)
					StartCoroutine (BlinkLetter (t, seconds));
//					StartCoroutine (BlinkButton (b, seconds));
				}
		}
	}

	private IEnumerator BlinkLetter (Text t, float seconds) {
		Color oldc = TextColor, 
			  newc = TextBlinkColor;
	//	newc.a = 0xFF;
	//	t.color = newc;
		for (float i = 0; i < 255; i+=10) {
			float r = 0.5f + i/255/2, a = 0.5f + i/255/2;
			newc = new Color (r, r, r, a);
			t.color = newc;
			yield return new WaitForSeconds (seconds/25);
			}
		t.color = oldc;
	}

	private IEnumerator BlinkButton (Button b, float seconds) {
		Image img = b.GetComponentInChildren<Image>();
		if (img != null)
				img.sprite = SpriteBlick;
		yield return new WaitForSeconds (seconds);
		img.sprite = SpriteEmpty;
	}

  }
}
