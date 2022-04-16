using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UnlockPad : MonoBehaviour
{
    [SerializeField]
    UIPanel enterCodePanel;

    [SerializeField]
    UIPanel unlockedPanel;

    [SerializeField]
    Image[] passcodeDots;

    string passcode = "";
    const string correctPasscode = "0231";


    void UnlockIPad()
    {
        if (passcode != correctPasscode)
        {
            StartCoroutine(HidePasscodeDots(.1f));
            return;
        }
        
        unlockedPanel.Show();
        StartCoroutine(HidePasscodeDots(.1f));
        StartCoroutine(HidePreviousPanel(.5f));
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

    public void AddNumberToPassword(string addString)
    {
        if (passcode.Length == 4)
            passcode = "";
        
        passcode += addString;

        for (int i = 0; i < passcodeDots.Length; i++)
            passcodeDots[i].enabled = (i == passcode.Length - 1);

        if (passcode.Length == 4)
            UnlockIPad();
    }


}
