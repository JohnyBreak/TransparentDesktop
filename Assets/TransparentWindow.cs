using System;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class TransparentWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_TextMeshProUGUI;

    [DllImport("user32.dll")]
    public static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);

    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

    [DllImport("user32.dll", SetLastError = true)]
    static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
    
    [DllImport("user32.dll")]
    static extern int SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);
    
    private struct MARGINS 
    {
        public int cxLeftWidth;
        public int cxRightWidth;
        public int cyTopHeight;
        public int cyBottomHeight;
    }

    [DllImport("Dwmapi.dll")]
    private static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margins);

    const int GWL_EXSTYLE = -20;

    const uint WS_EX_LAYERED = 0x00080000;
    const uint WS_EX_TRANSPARENT = 0x00000020;
    const uint LWA_COLORKEY = 0x00000001;
    static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
    private IntPtr _hWnd;

    private void Start()
    {
        //MessageBox(new IntPtr(0), "Hello", "Dialog", 0);


#if !UNITY_EDITOR
        IntPtr _hWnd = GetActiveWindow();

        MARGINS margins = new MARGINS { cxLeftWidth = -1 };
        DwmExtendFrameIntoClientArea(_hWnd, ref margins );

        SetWindowLong(_hWnd, GWL_EXSTYLE, WS_EX_LAYERED);// | WS_EX_TRANSPARENT);
        SetLayeredWindowAttributes(_hWnd, 0, 0, LWA_COLORKEY);

        SetWindowPos(_hWnd, HWND_TOPMOST,0,0,0,0,0);
#endif
        Application.runInBackground = true;
    }

    private void Update() 
    {
        Collider2D collider = Physics2D.OverlapPoint(Input.mousePosition);

        //SerClickthrough(collider == null);
    }

    private void SerClickthrough(bool clickthrough) 
    {
        if (clickthrough)
        {
            if(m_TextMeshProUGUI != null) m_TextMeshProUGUI.text = "(true)";
            SetWindowLong(_hWnd, GWL_EXSTYLE, WS_EX_LAYERED | WS_EX_TRANSPARENT);
        }
        else 
        {
            if (m_TextMeshProUGUI != null) m_TextMeshProUGUI.text = "(false)";
            SetWindowLong(_hWnd, GWL_EXSTYLE, WS_EX_LAYERED);
        }
    }

}
