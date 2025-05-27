using UnityEngine;
using TMPro;
using System.Collections;

public class LetterSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI letterText;
    private char currentChar = 'A';

    public void Start()
    {
        UpdateLetter();
    }

    public void NextLetter()
    {
        currentChar = (char)(((currentChar - 'A' + 1) % 26) + 'A');
        UpdateLetter();
    }

    public void PreviousLetter()
    {
        currentChar = (char)(((currentChar - 'A' - 1 + 26) % 26) + 'A');
        UpdateLetter();
    }

    public char GetLetter()
    {
        return currentChar;
    }

    private void UpdateLetter()
    {
        if (letterText != null)
        {
            letterText.text = currentChar.ToString();
        }
    }

    public void FlashRed()
    {
        StartCoroutine(WrongAnswer());
    }

    private IEnumerator WrongAnswer()
    {
        var panel = letterText.transform.parent.GetComponent<UnityEngine.UI.Image>();
        if (panel != null)
        {
            Color original = panel.color;
            panel.color = Color.red;
            yield return new WaitForSeconds(2f);
            panel.color = original;
        }
    }
}