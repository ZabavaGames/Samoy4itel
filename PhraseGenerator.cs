using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyMobileProject1 {

	public enum RuleSet { Random, Slu4ayno, O4epyatka, Lishnee, Soglasn, Proverka, Slovar, 
						Smysl_Zamena, Two_Swap };

	public struct PairString {
		public string Show, Correct;
		}

	public class PhraseGenerator {

		public RusLesson1 RS;
		private static int SourceLength, SourceIndex;
		private string[] Phrases, SourceFiles;

		private static char[] EndSigns = GradesConst.EndSigns;
		private static char[] Ends = GradesConst.Ends;
		private static char[] SpaceAndEnds = GradesConst.SpaceAndEnds;

		private static int CharTableSize;
		private static char[] CharTable, 
            RusCharTable = GradesConst.RusCharTable,
			EngCharTable = GradesConst.EngCharTable;

        private static char[][] CharTable_closest,
            RusCharTable_closest = GradesConst.RusCharTable_closest,
            EngCharTable_closest = GradesConst.EngCharTable_closest;

        private static char[] CharTable_zvon_pairs,
            RusCharTable_zvon_pairs = GradesConst.RusCharTable_zvon_pairs,
            EngCharTable_zvon_pairs = GradesConst.EngCharTable_zvon_pairs;

        private static char[] CharTable_udarn_pairs,
            RusCharTable_udarn_pairs = GradesConst.RusCharTable_udarn_pairs,
            EngCharTable_udarn_pairs = GradesConst.EngCharTable_udarn_pairs;


    // Use this for initialization
        void Start (string source) {
		RS = GameObject.Find ("SceneControl").GetComponent<RusLesson1>();		

	// проверяем язык
		if (RS.LessonLanguage == (int)languages.russian) {
			CharTable = RusCharTable;  // для русской версии
			CharTableSize = RusCharTable.Length;
			CharTable_closest = RusCharTable_closest;
			CharTable_zvon_pairs = RusCharTable_zvon_pairs;
			CharTable_udarn_pairs = RusCharTable_udarn_pairs;
			SourceFiles = GradesConst.SourceFilesRus;
			}
		else if (RS.LessonLanguage == (int)languages.english) {
            CharTable = EngCharTable;  // для русской версии
            CharTableSize = EngCharTable.Length;
            CharTable_closest = EngCharTable_closest;
            CharTable_zvon_pairs = EngCharTable_zvon_pairs;
            CharTable_udarn_pairs = EngCharTable_udarn_pairs;
            SourceFiles = GradesConst.SourceFilesEng;
            }

        Phrases = LoadRandStrings (source, SourceFiles);  // если несколько файлов, то можно случайно выбирать один из них
		SourceLength = Phrases.Length;
		SourceIndex = 0;
	}
	
	public PhraseGenerator (string source) {
		Start (source);
	}

	private string[] LoadRandStrings (string source, string[] filenames) {
		string[] res = { source + SourceFiles[Random4ik (SourceFiles.Length)] }; 
Debug.Log ("Грузим " + res[0]);
		return LoadLstrings (res);
	}

	private string[] LoadLstrings (string[] filenames) {
		string[] result; 
	//	List<string[]> result_list = new List<string[]> ();
		List<string> result_final = new List<string> ();

		foreach (string fname in filenames) {
			result = LoadStringsFromFile (fname);
			for (int j = 0; j < result.Length; j++)
				result_final.Add (result[j]);
			}

		return result_final.ToArray ();
	}

	private string[] LoadStringsFromFile (string filename) {
		string s = filename.Split ('.')[0];
		TextAsset t = Resources.Load (GradesConst.StringsPath + s, typeof (TextAsset)) as TextAsset;
		string[] s1 = t.text.Split (GradesConst.EndOfLine);

		return s1;
	}


	private int Random4ik (int number) {
		return UnityEngine.Random.Range (0, number);
	}

	public PairString GetRandomPhrase (SposobPodgotovki sposob, int rules, int[] output, int errors) {

		string s = GetRandString_ProperLength (sposob, output, errors);

		PairString ts = new PairString ();
		ts.Show = ts.Correct = s;
		char[] arr = s.ToCharArray ();
		char c, cc;
		int k;

		for (int i = 0; i < errors; i++) {
			do {  // подбираем рандомно символ в строке, избегая уже использованных
				k = Random4ik (s.Length);
				c = s[k];
				} while (!isChar (c) || arr [k] != c);

			cc = MakeError (arr, k, rules);
	//		arr [k] = cc;
// Debug.Log ("после замены " + arr[k]);
			}
			s = ts.Show = MakeString (arr);
		return ts;
	}

	private string GetRandString_ProperLength (SposobPodgotovki sposob, int[] output, int errors) {
		string s;
		do {
			if (sposob == SposobPodgotovki.Random4ik)
				s = Phrases [Random4ik (SourceLength)];
			else 
				s = Phrases [SourceIndex++];
			s = s.Trim ();
// обрабатываем фразу на предмет скобок
			s = RemoveBrackers (s);
			} while (WordsCount (s) < errors || !Prokrust (s, output));
		return s;
	}

/*	private string SkobkiMinus (string s) {
		char[] arr = new char[s.Length];
		string result = s;
		bool cutflag = false;

		do {
			cutflag = false;
			for (int i = 0; i < s.Length; i ++) {
	 			if (result[i] == '(') {
Debug.Log (result);
					cutflag = true;
					break;
					}
				}
			if (cutflag) {
				string[] s1 = s.Split ('(');
				result = s1[0];
				string[] s2 = s1[1].Split (')');
				result += s2[1];
				}
			} while (cutflag);
Debug.Log (result);
 		return result;
	} */

	// с киберфорума, удаляет скобки
	private string RemoveBrackers (string s) {
		char[] text_ar = s.ToCharArray();
        bool flag = true;
        string res = string.Empty;
 
        for (int i = 0; i < text_ar.Length; i++)
            {
            if (text_ar[i] == '(') flag = false;
            else if (text_ar[i] == ')') flag = true;
            else if (flag) res += text_ar[i];
            }

		return res;
	}

	private int WordsCount (string s) {
		string[] ss = s.Split (GradesConst.Spacebar);
		int words = 0;
		for (int i = 0; i < ss.Length; i ++) {
			if (ss[i].Length > GradesConst.MinWordLength) 
				words ++;
			}
		return words;
	}

    // проверка, умещается ли фраза в лимиты доски; скопировано с InitNewLesson
	private bool Prokrust (string s, int[] output) {
		string s1 = s;
		if (s.IndexOfAny (Ends) >= 0)
		// убираем всю фигню в коцне
			s1 = s.Split (Ends)[0];

		int raw = output[0], col = output[1];
		int Mcount = raw*col;
		bool flag = true;
		int d, j = 0;
		// разбиваем фразу на слова
		string[] ss = s1.Split (GradesConst.Spacebar);  		

		for (int i = 0; i < ss.Length; i++) {
			ss[i].Trim ();
			d = ss[i].Length;
			// проверяем, умещается ли слово на текущей строке
			// если нет, передвигаем индекс до начала след. строки  
			if ((j%col + d) > col) 
				j += col - j%col;          
			// добавляем слово
			j +=d;
			// и добавляем пробел, если это не конец строки
			if (j%col > 0) 
				j++;
			if (d >= col || j > Mcount/2) {  // j/col >= raw/2 || 
				// туши свет
				flag = false;	
//Debug.Log (raw + " " + col + " " + Mcount);
Debug.Log ("ProkrustФраза не поместилась!   " + s + "   Всего слов " + i + "/" + ss.Length + 
" слово " + ss[i] + " инд " + j + "/" + Mcount/2);
				break;
				}
			}
		return flag;
	}


	private bool isChar (char c) {
		bool flag = false;
		for (int i = 0; i < CharTableSize; i ++)
			if (c == CharTable [i]) {
				flag = true;
				break; 
				} 
		return flag;
	}

	private string MakeString (char[] arr) {
		string s = string.Empty;
		for (int i = 0; i < arr.Length; i++)
			s+= arr[i];
		return s;
	}

	private char MakeError (char[] arr, int k, int rules) {
		char res = GradesConst.ZeroChar;
		RuleSet rule1 = (RuleSet)rules;

		switch (rule1) {	
			case RuleSet.Random: 
				// в этом случае выбираем случайно любое правило
				res = MakeAnotherError (arr, k);
				break;

			case RuleSet.Lishnee:  // ?

			case RuleSet.Two_Swap:
				int q = FindPair (arr, k);
				if (q >= 0)
					res = SwapLetters (arr, k, q);
				else 
					res = MakeAnotherError (arr, k);
				break; 

			case RuleSet.Slovar: // ???
			case RuleSet.Proverka:
				res = GetUdarnPair (arr[k]);
				if (res == arr[k])
					res = MakeAnotherError (arr, k);
				else 
					arr[k] = res;
				break; 

			case RuleSet.Smysl_Zamena:  // ???

			case RuleSet.Soglasn:
				res = GetSoglasnPair (arr[k]);
				if (res == arr[k])
					res = MakeAnotherError (arr, k);
				else arr[k] = res;
				break; 

			case RuleSet.Slu4ayno:
//				arr[k] = res = CharTable [Random4ik (CharTableSize)];
//				break; 

			case RuleSet.O4epyatka:
				char[] c = GetKeyboard_ClosestSymbols (arr[k]);
				arr[k] = res = c [Random4ik (c.Length)];
				break; 

			default:
			break;
		}
		return res;
	}

	private char MakeAnotherError (char[] arr, int k) {
		return MakeError (arr, k, Random4ik (7) + 1);
	}

	private char[] GetKeyboard_ClosestSymbols (char c) {
		char[] arr = CharTable_closest [FindLetterNumber (c)];
		return arr;
	}

	private int FindLetterNumber (char c) {
		int i = 0;
		for (i = 0; i < CharTableSize; i ++)
			if (CharTable[i] == c)
				break; 
		return i;
	}

	private char GetSoglasnPair (char c) {
		char x = CharTable_zvon_pairs [FindLetterNumber (c)];
		return x;
	}

	private char GetUdarnPair (char c) {
		char x = CharTable_udarn_pairs [FindLetterNumber (c)];
		return x;
	}

	private int FindPair (char[] arr, int k) {
		int p, q;
		if (k == 0) 
			p = q = k+1;
		else if (k == (arr.Length - 1)) 
			p = q = k-1;
		else {
			p = k-1; 
			q = k+1;
			}
		int[] randind = { p, q };
		bool c1 = isChar (arr[randind[0]]);
		bool c2 = isChar (arr[randind[1]]);
		if ( c1 && c2) 
			return randind[Random4ik(2)];
		else if (c1) 
			return randind[0];
		else if (c2)
			return randind[1];
		else 
			return -1;
	}

	private char SwapLetters (char[] arr, int k, int q) {
		char c = arr[k];
		arr[k] = arr[q];
		arr[q] = c;
		return arr[k];
	}



	}
}
