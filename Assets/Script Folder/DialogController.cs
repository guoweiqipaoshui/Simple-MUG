using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    
    public Image[] Lein;
    public Image LeinInGameImage;
    public GameObject LeinInGame;

    public int index = 1;

    public Text dialogText;
    public TextAsset dialog;
    bool isDialogAllSent = true;

    public List<string> dialogList = new List<string>();
    // Start is called before the first frame update

    private void Awake()
    {
        GetText();
    }

    void Start()
    {
        dialogText.text = "...";
    }

    // Update is called once per frame
    void Update()
    {
        TalkSystem();
    }

    void GetText()
    {
        dialogList.Clear();
        var item = dialog.text.Split('\n');
        foreach(var text in item)
        {
            dialogList.Add(text);
        }
    }

    void TalkSystem()
    {
        if (Input.GetKeyDown(KeyCode.Space) && index == dialogList.Count - 1)
        {
            gameObject.SetActive(false);
            LeinInGame.SetActive(true);
            index = 1;
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space) && isDialogAllSent == true)
        {
            var RImage = Random.Range(0, 3);
            LeinInGameImage.sprite = Lein[RImage].sprite;
            //index++;
            //dialogText.text = dialogList[index];
            StartCoroutine(SetTextUI());
        }
    }

    IEnumerator SetTextUI()
    {
        isDialogAllSent = false;
        dialogText.text = "";
        for(int i = 0; i < dialogList[index].Length; i++)
        {
                dialogText.text += dialogList[index][i];

                yield return new WaitForSeconds(0.1f);
        }
        isDialogAllSent = true;
            index++;
    }
}
