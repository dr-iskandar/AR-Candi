using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public string link;

    private void OnMouseDown()
    {
        Application.OpenURL(link);
    }
}
