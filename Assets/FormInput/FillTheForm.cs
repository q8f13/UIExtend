using UnityEngine;
using UnityEngine.UI;

public class FillTheForm : MonoBehaviour
{
    public InputField Usr;
    public InputField Pswd;

    private string _usrName;
    private string _pswd;

    // Start is called before the first frame update
    void Start()
    {
        Pswd.inputType = InputField.InputType.Password;

        Usr.onEndEdit.AddListener((s)=>
        {
            _usrName = s;
            Pswd.ActivateInputField();
        });

        Pswd.onEndEdit.AddListener((s)=>
        {
            _pswd = s;
            if(string.IsNullOrEmpty(_usrName))
                Usr.ActivateInputField();
            else
                Debug.LogFormat("{0}|{1}", _usrName, _pswd);
        });
    }
}
