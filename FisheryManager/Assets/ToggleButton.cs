using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ToggleButton: MonoBehaviour
{
    [SerializeField] private Sprite[] buttonSprites;
    [SerializeField] private Image startSoundButton;
    public void ChangeButtonSprite()
    {
        if(startSoundButton.sprite == buttonSprites[0])
        {
            startSoundButton.sprite = buttonSprites[1];
            AudioListener.pause = !AudioListener.pause;
            return;
        }
        AudioListener.pause = !AudioListener.pause;
        startSoundButton.sprite = buttonSprites[0];
    }

}
