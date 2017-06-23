using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public string gameBoardScene = "Game_page1";
    public Button NewGameButton, InstructionButton, AboutButton, backButton;
    public Text textBox, textBoxAbout, createdBy;
    // Use this for initialization
    void Start () {
        backButton.gameObject.SetActive(false);
        textBox.gameObject.SetActive(false);
        textBoxAbout.gameObject.SetActive(false);
        createdBy.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void NewGameClicked()
    {
        Application.LoadLevel(this.gameBoardScene);
    }

    public void OnClickInstructions()
    {
        textBox.gameObject.SetActive(true);
        backButton.gameObject.SetActive(true);

        textBoxAbout.gameObject.SetActive(false);
        NewGameButton.gameObject.SetActive(false);
        InstructionButton.gameObject.SetActive(false);
        AboutButton.gameObject.SetActive(false);

        textBox.text = "Form words using the letters provided in the wheel. Using center letter is mandatory.";

    }

    public void OnClickAboutUs()
    {
        textBoxAbout.gameObject.SetActive(true);
        createdBy.gameObject.SetActive(true);
        backButton.gameObject.SetActive(true);

        textBox.gameObject.SetActive(false);
        NewGameButton.gameObject.SetActive(false);
        InstructionButton.gameObject.SetActive(false);
        AboutButton.gameObject.SetActive(false);

        createdBy.text = "Created By :-";
        int rand = Random.Range(0,2);
        if(rand==0)
        textBoxAbout.text = "Paramjeet Desai\nShirish Jain\nLabdhi Shah\n(June 2017)";
        else
        textBoxAbout.text = "Shirish Jain\nParamjeet Desai\nLabdhi Shah\n(June 2017)";

    }

    public void OnBackPressed()
    {
        textBoxAbout.gameObject.SetActive(false);
        textBox.gameObject.SetActive(false);
        backButton.gameObject.SetActive(false);
        createdBy.gameObject.SetActive(false);

        NewGameButton.gameObject.SetActive(true);
        InstructionButton.gameObject.SetActive(true);
        AboutButton.gameObject.SetActive(true);
    }
}
