using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// this script is to unlock a specific interactable object ipad in level 3
/// </summary>
public class UnlockPad : MonoBehaviour
{
    [SerializeField]
    UIPanel enterCodePanel;

    [SerializeField]
    UIPanel unlockedPanel;

    [SerializeField]
    Image[] passcodeDots;
  
    const string defaultPasscode = "";
    const string correctPasscode = "0231";

    string passcode = defaultPasscode;

    const float hideDotsTime = .1f;
    const float hidePanelTime = .5f;


    void UnlockIPad()
    {
        if (passcode != correctPasscode)
        {
            StartCoroutine(HidePasscodeDots(hideDotsTime));
            return;
        }
        
        unlockedPanel.Show();
        StartCoroutine(HidePasscodeDots(hideDotsTime));
        StartCoroutine(HidePreviousPanel(hidePanelTime));
    }

    IEnumerator HidePreviousPanel(float waitTime)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        enterCodePanel.Close();
    }

    IEnumerator HidePasscodeDots(float waitTime)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        passcodeDots[passcodeDots.Length - 1].enabled = false;
    }

    /// <summary>
    /// to be addlistener to each passcode buttons
    /// </summary>
    /// <param name="addString"></param>
    public void AddNumberToPassword(string addString)
    {
        if (passcode.Length == correctPasscode.Length)
            passcode = defaultPasscode;
        
        passcode += addString;

        for (int i = 0; i < passcodeDots.Length; i++)
            passcodeDots[i].enabled = (i == passcode.Length - 1);

        if (passcode.Length == correctPasscode.Length)
            UnlockIPad();
    }


}
