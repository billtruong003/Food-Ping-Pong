using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebBrowse : MonoBehaviour
{
    public void TeamWeb()
    {
        string url = "https://culinaryjourney.github.io/"; // Thay thế đường dẫn bằng địa chỉ trang web bạn muốn mở

        Application.OpenURL(url);
    }
    public void GithubReposite()
    {
        string url = "https://github.com/billtruong003/Food-Ping-Pong"; // Thay thế đường dẫn bằng địa chỉ trang web bạn muốn mở

        Application.OpenURL(url);
    }
}
