using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class UIController : MonoBehaviour
{
    public GameObject[] menuUI;
    public GameObject settings;
    public GameObject loginbutton;
    public GameObject infoUpdated;
    public InputField infoText;
    public Text[] txtInfo;

    public string LoginURL, UpdateURL, InfoURL;
    public InputField txtEmail, txtPassword;
    WWW php_login;
    WWW php_update;
    WWW php_info;
    WWWForm php_form;
    public string vUser, vPass, vText;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void mulai()
    {
        for (int i = 0; i < menuUI.Length; i++)
        {
            menuUI[i].SetActive(false);
        }
        StartCoroutine(Info());
    }

    public void setting()
    {
        menuUI[2].SetActive(true);
    }

    public void login()
    {
        StartCoroutine(PostLogin());
    }

    public void updateInfo()
    {
        StartCoroutine(PostUpdate());
    }

    IEnumerator PostLogin()
    {
        vUser = txtEmail.text;
        vPass = txtPassword.text;

        php_form = new WWWForm();
        php_form.AddField("loginEmail", vUser);
        php_form.AddField("loginPass", vPass);
        php_login = new WWW(LoginURL, php_form);
        yield return php_login;

        vText = php_login.text;
        if (vText == "Login Success")
        {
            settings.SetActive(true);
            loginbutton.SetActive(false);
        }
    }

    IEnumerator PostUpdate()
    {
        string vInfo = infoText.text;
        php_form = new WWWForm();
        php_form.AddField("userEmail", vUser);
        php_form.AddField("userInfo", vInfo);
        php_update = new WWW(UpdateURL, php_form);
        yield return php_update;

        vText = php_update.text;
        Debug.Log(vText);
        if (vText == "Record updated successfully")
        {
            infoUpdated.SetActive(true);
            yield return new WaitForSeconds(3);
            vUser = "";
            vPass = "";
            infoUpdated.SetActive(false);
            loginbutton.SetActive(true);
            settings.SetActive(false);
            menuUI[0].SetActive(false);
        }
    }

    IEnumerator Info()
    {
        php_info = new WWW(InfoURL);
        yield return php_info;
        for (int i = 0; i < txtInfo.Length; i++)
        {
            txtInfo[i].text = php_info.text;
        }
    }
}

