using UnityEngine;
using UnityEngine.UI;

public class KeyboardToUI : MonoBehaviour
{
    public Text displayText;
    public FPSController fpscontroller;

    private string typedText = "";

    void Update()
    {
        //if (fpscontroller.IsPlayerMoving()) { return; }

        foreach (char c in Input.inputString)
        {
            // Backspace
            if (c == '\b')
            {
                if (typedText.Length > 0)
                    typedText = typedText[..^1];
            }
            // Return / Enter
            //else if (c == '\n' || c == '\r')
            //{
            //    typedText += "\n";
            //}
            else
            {
                if(c == 'e') { return; }
                typedText += c;
            }
        }

        displayText.text = typedText;
    }
}
