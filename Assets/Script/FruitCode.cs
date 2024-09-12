using UnityEngine;

public class FruitCode : MonoBehaviour
{
    #region properties
    //properties
    //properties
    //properties
    //properties
    //properties
    //properties
    //properties
    //properties

    //properties
    //properties
    //properties
    //properties
    //properties
    //properties
    //properties
    //properties
    //properties
    //properties

    #endregion

    #region Variables
    [SerializeField] private string dayLaCodeCuaSora;
    [SerializeField] private string dayLaCodeCuaND;
    // Important 
    [SerializeField] private bool skipTut;

    #endregion


    #region 
    private void Start()
    {
        if (skipTut)
        {

        }
    }
    #endregion

    public void HamCuaND()
    {
        dayLaCodeCuaSora = "sora";
    }

}