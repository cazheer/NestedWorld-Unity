using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using SimpleJSON;

public class MenuScript : MonoBehaviour
{
    public HttpRequest requester;

    public Canvas main;
    public Canvas login;
    public Canvas register;

    private Canvas currentActivated;

    // login
    public Text emailLogin;
    public Text passwordLogin;
    // register
    public Text pseudoRegister;
    public Text emailRegister;
    public Text passwordRegister;
    public Text passwordConfirmRegister;

	// Use this for initialization
	void Start ()
    {
        login.enabled = false;
        register.enabled = false;

        currentActivated = main;

        MaterialUI.ToastControl.InitToastSystem(GetComponent<Canvas>());
    }

    /*
    ** Enable
    */
    public void MainEnable()
    {
        currentActivated.enabled = false;
        currentActivated = main;
        main.enabled = true;

        MaterialUI.ToastControl.InitToastSystem(currentActivated);
    }

    public void LoginEnable()
    {
        currentActivated.enabled = false;
        currentActivated = login;
        login.enabled = true;

        MaterialUI.ToastControl.InitToastSystem(currentActivated);
    }

    public void RegisterEnable()
    {
        currentActivated.enabled = false;
        currentActivated = register;
        register.enabled = true;

        MaterialUI.ToastControl.InitToastSystem(currentActivated);
    }

    /*
    ** Login
    */
    public void Connexion()
    {
        if (emailLogin.text.Length == 0)
            MaterialUI.ToastControl.MakeToast("Vous devez rentrer votre email", 5.0f, Color.white, Color.black, 32);
        else if (passwordLogin.text.Length == 0)
            MaterialUI.ToastControl.MakeToast("Vous devez rentrer votre mot de passe", 5.0f, Color.white, Color.black, 32);
        else
        {
            requester.PostLogIn(ConnexionOnSuccess, ConnexionOnFailure, emailLogin.text, passwordLogin.text);
        }
    }

    private bool ConnexionOnSuccess(JSONNode node)
    {
        string sessionToken = node["token"].Value;
        if (sessionToken == null)
        {
            MaterialUI.ToastControl.MakeToast("Erreur de connexion : token de session introuvable", 5.0f, Color.white, Color.black, 32);
            return false;
        }
        HttpRequest.sessionToken = sessionToken;
        SceneManager.LoadScene("GameScene");
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
        if (emailLogin.text.Length != 0)
        {
            requester.PostResetPassword(PasswordForgotOnSuccess, PasswordForgotOnFailure, emailLogin.text);
            return;
        }
        MaterialUI.ToastControl.MakeToast("Vous devez rentrer votre email", 5.0f, Color.white, Color.black, 32);
    }

    private bool PasswordForgotOnSuccess(JSONNode node)
    {
        MaterialUI.ToastControl.MakeToast("Le mot de passe a bien été réinitialiser.", 5.0f, Color.white, Color.black, 32);
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

    /*
    ** Register
    */
    public void Register()
    {
        if (pseudoRegister.text.Length == 0)
            MaterialUI.ToastControl.MakeToast("Vous devez rentrer votre pseudo", 5.0f, Color.white, Color.black, 32);
        else if (emailRegister.text.Length == 0)
            MaterialUI.ToastControl.MakeToast("Vous devez rentrer votre email", 5.0f, Color.white, Color.black, 32);
        else if (passwordRegister.text.Length == 0)
            MaterialUI.ToastControl.MakeToast("Vous devez rentrer votre mot de passe", 5.0f, Color.white, Color.black, 32);
        else if (!passwordRegister.text.Equals(passwordConfirmRegister.text))
            MaterialUI.ToastControl.MakeToast("Mot de passe différents", 5.0f, Color.white, Color.black, 32);
        else
        {
            requester.PostRegister(RegisterOnSuccess, RegisterOnFailure, pseudoRegister.text, emailRegister.text, passwordRegister.text);
        }
    }

    private bool RegisterOnSuccess(JSONNode node)
    {
        LoginEnable();
        MaterialUI.ToastControl.MakeToast("Connection réussit", 5.0f, Color.white, Color.black, 32);
        return true;
    }

    private bool RegisterOnFailure(WWW error)
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
