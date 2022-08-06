using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
    //variable for the fade game object
    public GameObject blackOutSquare;

    // creates a coroutine for transitions
    public IEnumerator FadeBlackOutSquare(bool fadeToBlack = true, int fadeSpeed =5){
        //enables the fade
        blackOutSquare.SetActive(true);
        //makes the colour of the object the same
        Color objectColor = blackOutSquare.GetComponent<Image>().color;
        // how much it should fade
        float fadeAmount; 

        if(fadeToBlack){
            //Fades the game object in
            while(blackOutSquare.GetComponent<Image>().color.a < 1){
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOutSquare.GetComponent<Image>().color = objectColor;
                blackOutSquare.SetActive(true);
                yield return null;
            }
        } else {
            //fades game object out
            while (blackOutSquare.GetComponent<Image>().color.a > 0) {
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOutSquare.GetComponent<Image>().color = objectColor;
                yield return null;
            }
        }

    }
}
