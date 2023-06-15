using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace CnqC.EndLessGame
{

    public class Dialog : MonoBehaviour
    {
        public TextMeshProUGUI titleText;
        public TextMeshProUGUI contentText;

        public virtual void Show(bool isShow)
        {
            gameObject.SetActive(isShow);
        }

        public virtual void UpdateDialog(string title, string content)
        {
            if (titleText)
                titleText.text = title;

            if (contentText)
                contentText.text = content;
        }

        public virtual void UpdateDialog()
        {

        }

        public virtual void Close()
        {
            gameObject.SetActive(false);
        }
    }
}