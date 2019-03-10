using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopDialogue : MonoBehaviour
{
  [SerializeField] TextMeshProUGUI textBox;
  [TextArea(15, 20)] [SerializeField] List<string> TextList;
  [SerializeField] string talkingText = "";
  [SerializeField] GameObject shopMenu;
  [SerializeField] GameObject talkPanel;
  bool endOfTalk = false;
  private string currentText;
  // Start is called before the first frame update
  void Start()
  {
    StartCoroutine("StartTalking");
    shopMenu.SetActive(false);
  }

  private void CheckDialogue()
  {
    if (Input.GetButtonDown("Fire1"))
    {
      talkingText = currentText;
      // StopCoroutine("StartTalking");
    }

  }
  private IEnumerator StartTalking()
  {
    for (int i = 0; i < TextList.Count; i++)
    {

      talkingText = "";
      currentText = TextList[i];
      if (talkingText.Length != currentText.Length)
      {
        for (int j = 0; j < currentText.Length; j++)
        {

          talkingText = talkingText + currentText[j];
          yield return new WaitForSeconds(.04f);
          if (talkingText.Length == currentText.Length)
          {
            print("TEXT DONE");
            yield return new WaitForSeconds(2f);
            talkingText = "";
            break;
          }
        }
      }
    }
    endOfTalk = true;
    talkingText = "What should I make for you today?";
    shopMenu.SetActive(true);
    talkPanel.SetActive(false);
  }
  // Update is called once per frame
  void Update()
  {
    textBox.text = talkingText;
    if (!endOfTalk)
    {
      CheckDialogue();
    }
  }
}
