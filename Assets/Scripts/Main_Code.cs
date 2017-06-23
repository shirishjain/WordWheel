using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class Main_Code : MonoBehaviour {

private Dictionary<string, List<string>> anagramDictionary = new Dictionary<string, List<string>>();

    private string previousScene = "Start_page";
    private List<string> final_alphabets_list;// = new List<string>();
    private List<string> question_list;// = new List<string>();
    private Dictionary<int, List<string>> currentQuestion_LengthDictionary;// = new Dictionary<int, List<string>>();

public Text inputText,displayText, displayAnswerText, someMoreWordsHeading;
public Button answer, next;
public GameObject textParent;
public Text centerText;
int numberOfCorrectAns;
   public void CreateAnagramDictionary()
    {
        TextAsset textAssest = Resources.Load("Words") as TextAsset;
        System.IO.StreamReader DictionaryFileReader;
        DictionaryFileReader = new System.IO.StreamReader(new System.IO.MemoryStream(textAssest.bytes));
        int count = 0;
        while (!DictionaryFileReader.EndOfStream)
        {
            string presentWord = (string)DictionaryFileReader.ReadLine();
                        
                if (presentWord != "")
                {
                    if ( presentWord.Length > 1 && presentWord.Length <= 9)
                    {                                            
                        char[] presentWordArray = presentWord.ToCharArray();
                        System.Array.Sort(presentWordArray);
                        string key = new string(presentWordArray);
                                                                       
                        if (anagramDictionary.ContainsKey(key))
                        {
                            anagramDictionary[key].Add(presentWord); 
                        }
                        else
                        {
                            anagramDictionary.Add(key, new List<string>() { presentWord });
                        }
                        count++;
                    }
                }
        }
    }


   void GenerateQuestion()
    {
        someMoreWordsHeading.gameObject.SetActive(false);
        numberOfCorrectAns = 0;
        int count = 0;
        while (count < 15)
        {
            count = 0;
            question_list = new List<string>();
            final_alphabets_list = new List<string>();
            currentQuestion_LengthDictionary = new Dictionary<int, List<string>>();

            List<string> vowels = new List<string> {"a", "e", "i", "o", "u" };
            List<string> consonants = new List<string>() { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "n", "p", "q", "r", "s", "t", "v", "w", "x", "y", "z" };
            // List<string> alphabets = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

            int totalNoOfAlphabets = 9;
            int noOfVowels = Random.Range(3,5);
            int noOfConsonants = totalNoOfAlphabets - noOfVowels;

            for (int i = 0; i < noOfVowels; i++) {
                string s = vowels[Random.Range(0, vowels.Count)];
                final_alphabets_list.Add(s);
                vowels.Remove(s);
            }
        
            for (int i = 0; i < noOfConsonants; i++)
            {
                string s = consonants[Random.Range(0, consonants.Count)];
                final_alphabets_list.Add(s);
                consonants.Remove(s);
            }

            /*for (int i = 0; i < final_alphabets_list.Count; i++) {
                Debug.Log("Alphabet selected: " + final_alphabets_list[i]);
            }*/

            string mid_letter = final_alphabets_list[Random.Range(0,final_alphabets_list.Count)];
            final_alphabets_list.Remove(mid_letter);
            question_list.Add(mid_letter);
            for (int i=0;i<final_alphabets_list.Count;i++)
            {
                question_list.Add(final_alphabets_list[i]);
            }
            
                    
            //Debug.Log("mid_letter : " + mid_letter);

            for (int i = 0; i < (1 << totalNoOfAlphabets-1); i++)
            {
                string str = mid_letter;
                for (int j = 0; j < totalNoOfAlphabets-1; j++)
                {
                    if ((i & (1 << j)) == 1<<j)
                    {
                        str += final_alphabets_list[j];
                    }
                }
                char[] str_array = str.ToCharArray();
                System.Array.Sort(str_array);
                string keyToBeFound= new string(str_array);

                int length_key = keyToBeFound.Length;
                length_key = keyToBeFound.Length;
                if (anagramDictionary.ContainsKey(keyToBeFound))
                {
                     if (!currentQuestion_LengthDictionary.ContainsKey(length_key))
                     {
                        currentQuestion_LengthDictionary.Add(length_key, new List<string>());
                     }
                    for (int x = 0; x < anagramDictionary[keyToBeFound].Count; x++)
                    {
                        currentQuestion_LengthDictionary[length_key].Add(anagramDictionary[keyToBeFound][x]);
                        Debug.Log("word added : " + anagramDictionary[keyToBeFound][x] );
                        count++;
                    }
                }
                str = "";
            }
           
        }
        Debug.Log("no of words made : "+ count);

    }

   void StartGame()
   {
       WheelWord.instance.DrawWheelWord(question_list);
   }

    // Use this for initialization
   void Start () {
        numberOfCorrectAns = 0;
        CreateAnagramDictionary();
        GenerateQuestion();
        StartGame();
	}

    public void OnAnimationComplete(AbstractGoTween tween)

    {
        inputText.text = "";
        inputText.color = new Color(0, 0, 0);

    }

    public void OnEnterPressed()
    {

        centerText.color = new Color(1, 1, 1, 1);
        centerText.gameObject.SetActive(true);

        for (int i = 0; i < 8; i++)
        {
            textParent.transform.GetChild(i ).GetComponent<Text>().color = new Color(0, 0, 0, 1);
            textParent.transform.GetChild(i).GetComponent<Text>().gameObject.SetActive(true);
        }

        string typedAnswer = inputText.text;
        Debug.Log("Typed ans : " + typedAnswer);
        typedAnswer = typedAnswer.ToLower();
        if (!currentQuestion_LengthDictionary.ContainsKey(typedAnswer.Length))
        {
            Debug.Log("InCorrect");
            inputText.color = new Color(0.78039f, 0.42745f, 0.21176f);
            Go.to(inputText.gameObject.transform, 0.5f, new GoTweenConfig().shake(new Vector3(0, 0, 20), GoShakeType.Eulers)).setOnCompleteHandler(OnAnimationComplete);

            //inputText.text = "";
        }
        else
        {
            List<string> answersOfThisLength = currentQuestion_LengthDictionary[typedAnswer.Length];
            int i;
            for (i = 0; i < answersOfThisLength.Count; i++)
            {
                if (typedAnswer.Equals(answersOfThisLength[i]))
                {
                    Debug.Log("Answer is correct");
                    inputText.text = "";
                    displayText.text += typedAnswer.ToUpper() + "  ";
                    answersOfThisLength.Remove(typedAnswer);
                    currentQuestion_LengthDictionary[typedAnswer.Length] = answersOfThisLength;
                    numberOfCorrectAns++;
                    if(numberOfCorrectAns == 5) {
                        //Make answer button inactive
                        OnAnswerPressed();
                    }
                }
                
            }
            if (i == answersOfThisLength.Count)
            {
                Debug.Log("Answer is incorrect");
                inputText.color = new Color(0.78039f, 0.42745f, 0.21176f);
                Go.to(inputText.gameObject.transform, 0.5f, new GoTweenConfig().shake(new Vector3(0, 0, 20), GoShakeType.Eulers)).setOnCompleteHandler(OnAnimationComplete);
                //inputText.text = "";

            }
        }
    }

    public void OnNumpadPressed(string key)
    {
        if (answer.IsActive())
        {
            inputText.text += question_list[int.Parse(key)].ToUpper();
            if (int.Parse(key) != 0)
            {
                textParent.transform.GetChild(int.Parse(key) - 1).GetComponent<Text>().color = new Color(1, 1, 1, 1);
                textParent.transform.GetChild(int.Parse(key) - 1).GetComponent<Text>().gameObject.SetActive(false);

            }
            else
            {
                centerText.color = new Color(0.99215f, 0.02352f, 0.02352f, 1);
                centerText.gameObject.SetActive(false);
            }

            //Debug.Log(question_list[int.Parse(key)]);
        }
    }

    public void OnDelPressed()
    {
        if (inputText.text.Length != 0)
        {
            string lastChar = inputText.text.Substring(inputText.text.Length - 1, 1).ToLower();
            Debug.Log(lastChar);
            for(int i = 0; i < 9; i++)
            {
                if (lastChar.Equals(question_list[i]))
                {
                    Debug.Log(lastChar);

                    if (i != 0)
                    {
                        textParent.transform.GetChild(i - 1).GetComponent<Text>().color = new Color(0, 0, 0, 1);
                        textParent.transform.GetChild(i - 1).GetComponent<Text>().gameObject.SetActive(true);

                    }
                    else
                    {
                        centerText.color = new Color(1, 1, 1, 1);
                        centerText.gameObject.SetActive(true);
                    }
                }
            }
            inputText.text = inputText.text.Substring(0, inputText.text.Length - 1);

        }
    }

    public void OnAnswerPressed()
    {
        inputText.gameObject.SetActive(false);
        someMoreWordsHeading.gameObject.SetActive(true);
        centerText.gameObject.SetActive(true);

        centerText.color = new Color(1, 1, 1, 1);

        for(int i = 0; i < 8; i++)
        {
            textParent.transform.GetChild(i).GetComponent<Text>().color = new Color(0, 0, 0, 1);
            textParent.transform.GetChild(i).GetComponent<Text>().gameObject.SetActive(true);
        }


        //someMoreWordsHeading.color = new Color(0f, 0f, 1f, 1f);

        List<string> answersList = new List<string>();
       int lengthAns = 9;
        while (answersList.Count != 8)
        {
            if (currentQuestion_LengthDictionary.ContainsKey(lengthAns))
            {
                answersList.Add(currentQuestion_LengthDictionary[lengthAns][0]);
                Debug.Log(answersList[answersList.Count - 1]);
                currentQuestion_LengthDictionary[lengthAns].RemoveAt(0);
                if (currentQuestion_LengthDictionary[lengthAns].Count == 0)
                {
                    currentQuestion_LengthDictionary.Remove(lengthAns);
                }
            }
                lengthAns = (lengthAns + 9) % 10; 

        }
        /*for(int i = 3; i <= 9; i++)
        {
            if (currentQuestion_LengthDictionary.ContainsKey(i))
            {
                for(int j=0;j< currentQuestion_LengthDictionary[i].Count; j++)
                {
                    answersList.Add(currentQuestion_LengthDictionary[i][j]);
                }
            }
        }*/
        for(int i = 0; i < 8; i ++)
        {
            
            displayAnswerText.text +=answersList[i].ToUpper() + "  ";
            //answersList.RemoveAt(index);
        }
        answer.gameObject.SetActive(false);
        //answer disable
    }

    public void OnNextPressed()
    {
        inputText.gameObject.SetActive(true);
        answer.gameObject.SetActive(true);
        displayText.text = "";
        inputText.text = "";
        displayAnswerText.text = "";    
        GenerateQuestion();
        StartGame();

        centerText.gameObject.SetActive(true);
        for (int i = 0; i < 8; i++)
        {
            textParent.transform.GetChild(i).GetComponent<Text>().gameObject.SetActive(true);
        }
    }    

    public void OnBackPressed()
    {
        Application.LoadLevel(this.previousScene);

    }
}