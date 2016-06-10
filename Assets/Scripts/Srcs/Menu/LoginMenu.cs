﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using SimpleJSON;

public class LoginMenu : MonoBehaviour
{
    [SerializeField]
    InputField email;
    [SerializeField]
    InputField password;
    [SerializeField]
    Toggle keepConnected;

    HttpRequest request;
    UserData userData;

    // Use this for initialization
    void Start()
    {
        MaterialUI.ToastControl.InitToastSystem(GetComponent<Canvas>());

        GameObject requester = GameObject.FindGameObjectWithTag("Requester");
        userData = requester.GetComponent<UserData>();
        request = requester.GetComponent<HttpRequest>();

        // get the email from previous data
        if (PlayerPrefs.HasKey(Constants.emailCache))
        {
            email.text = PlayerPrefs.GetString(Constants.emailCache);
            password.text = PlayerPrefs.GetString(Constants.passwordCache);
            Connexion();
        }
    }

    public void Connexion()
    {
        if (email.text.Length == 0)
            MaterialUI.ToastControl.MakeToast("Vous devez rentrer votre email", 5.0f, Color.white, Color.black, 32);
        else if (password.text.Length == 0)
            MaterialUI.ToastControl.MakeToast("Vous devez rentrer votre mot de passe", 5.0f, Color.white, Color.black, 32);
        else
        {
            userData.email = email.text;
            request.PostLogIn(ConnexionOnSuccess, ConnexionOnFailure, email.text, password.text);
        }
    }

    private bool ConnexionOnSuccess(JSONNode node)
    {
        // Save email
        if (keepConnected.isOn)
        {
            PlayerPrefs.SetString(Constants.emailCache, email.text);
            PlayerPrefs.SetString(Constants.passwordCache, password.text);
            PlayerPrefs.Save();
        }

        string sessionToken = node["token"].Value;
        if (sessionToken == null)
        {
            MaterialUI.ToastControl.MakeToast("Erreur de connexion : token de session introuvable", 5.0f, Color.white, Color.black, 32);
            return false;
        }
        userData.token = sessionToken;
        SceneManager.LoadScene("HomeScene");
        return true;
    }

    private bool ConnexionOnFailure(WWW error)
    {
        JSONNode node = JSON.Parse(error.text);

        string errorText = error.error;
        if (node != null && node["message"] != null)
        {
            errorText += " : " + node["message"].Value;
            if (errorText.StartsWith("400"))
            {
                errorText = node["message"].Value;
            }
        }
        MaterialUI.ToastControl.MakeToast(errorText, 5.0f, Color.white, Color.black, 32);
        return true;
    }

    public void PasswordForgot()
    {
        if (email.text.Length != 0)
            request.PostResetPassword(PasswordForgotOnSuccess, PasswordForgotOnFailure, email.text);
        else
            MaterialUI.ToastControl.MakeToast("Vous devez rentrer votre email", 5.0f, Color.white, Color.black, 32);
    }

    private bool PasswordForgotOnSuccess(JSONNode node)
    {
        MaterialUI.ToastControl.MakeToast("Votre demande de réinitialisation a bien été prise en compte. Vous devriez recevoir un e-mail sous peu.", 5.0f, Color.white, Color.black, 32);
        return true;
    }

    private bool PasswordForgotOnFailure(WWW error)
    {
        JSONNode node = JSON.Parse(error.text);

        string errorText = error.error;

        if (node != null && node["message"] != null)
        {
            errorText += " : " + node["message"].Value;
            if (errorText.StartsWith("400"))
            {
                errorText = node["message"].Value;
            }
        }
        MaterialUI.ToastControl.MakeToast(errorText, 5.0f, Color.white, Color.black, 32);
        return true;
    }
}
