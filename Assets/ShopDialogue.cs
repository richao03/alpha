using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopDialogue : MonoBehaviour
{
  [SerializeField] TextMeshProUGUI textBox;
  [TextArea(15, 20)] [SerializeField] List<string> TextList;
  [SerializeField] string talkingText = "";
  private string currentText;
  // Start is called before the first frame update
  void Start()
  {
    StartCoroutine("StartTalking");
  }

  private void CheckDialogue()
  {
    if (Input.GetButtonDown("Fire1"))
    {
      print("fire");
      StopCoroutine("StartTalking");
      talkingText = currentText;
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
        for (int j = 0; j < TextList[i].Length; j++)
        {
          talkingText = talkingText + TextList[i][j];
          yield return new WaitForSeconds(.04f);
        }
      }
      else
      {
        print("TEXT DONE");
        yield return new WaitForSeconds(2f);

      }
    }
  }
  // Update is called once per frame
  void Update()
  {
    textBox.text = talkingText;
    CheckDialogue();
  }
}
