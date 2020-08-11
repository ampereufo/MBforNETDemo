using MB;
using System;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;


namespace ReptilesData
{
    class Common
    {
        public const int MOUSEEVENTF_MOVE = 0x1;
        public const int MOUSEEVENTF_LEFTDOWN = 0x2;
        public const int MOUSEEVENTF_LEFTUP = 0x4;
        public const int MOUSEEVENTF_RIGHTDOWN = 0x8;
        public const int MOUSEEVENTF_RIGHTUP = 0x10;
        public const int MOUSEEVENTF_MIDDLEDOWN = 0x20;
        public const int MOUSEEVENTF_MIDDLEUP = 0x40;
        public const int MOUSEEVENTF_WHEEL = 0x800;
        public const int MOUSEEVENTF_ABSOLUTE = 0x8000;

        public const int WM_COPYDATA = 0x004A;
        public const int WM_LBUTTONDOWN = 0x201;
        public const int WM_LBUTTONUP = 0x202;
        public const int WM_RBUTTONDOWN = 0x0204;
        public const int WM_RBUTTONUP = 0x0205;
        public const int WM_MOUSEWHEEL = 0x020A;
        public const int WM_PASTE = 0x0302;
        public const int WM_CHAR = 0x102;
        public const int WM_SETTEXT = 0x0C;
        public const int WM_KEYDOWN = 0x0100;
        public const int WM_KEYUP = 0x0101;
        public const int WM_CLOSE = 0x0010;

        public const int VK_END = 0x23;
        public const int VK_BACK = 0x08;
        public const int VK_RETURN = 0x0D;
        public const int VK_Delete = 0x2E;

        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        public static extern int SetCursorPos(int x, int y);

        [DllImport("user32.dll", EntryPoint = "mouse_event")]
        public static extern void mouse_event(int flags, int dX, int dY, int buttons, int extraInfo);

        [DllImport("user32.dll", EntryPoint = "keybd_event")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

        [DllImport("user32.dll", EntryPoint = "SendMessageW")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(string strclassName, string strWindowText);

        [DllImport("kernel32", EntryPoint = "GetPrivateProfileString")]
        public static extern long GetPrivateProfileString(string strSection, string strKey, string strDef, StringBuilder sbBuffer, int iSize, string strFilePath);


        // 获取当前滚动条滚动的高度
        public static int GetSocrllHeightByJs(WebView wView)
        {
            string strGetScrollTop = "$(window).scrollTop();";
            object jv = wView.RunJS("return " + strGetScrollTop);

            return Convert.ToInt32(jv);
        }

        // 获取整个窗口的高度
        public static int GetScreenHeightByJs(WebView wView)
        {
            string strGetScreenHeight = "$(window).height();";
            object jv = wView.RunJS("return " + strGetScreenHeight);

            return Convert.ToInt32(jv);
        }

        // 获取网页元素相对于浏览器的坐标（加一点偏移，方便点击），返回Point(-9999, -9999)表示指定查找的元素不存在
        public static Point GetElementPointByJs(WebView wView, string strElement, string strType, string strIndex)
        {
            string strGetTopJs = "function getOffsetTop(el){return el.offsetParent? el.offsetTop + getOffsetTop(el.offsetParent): el.offsetTop}\n";
            string strGetLeftJs = "function getOffsetLeft(el){return el.offsetParent? el.offsetLeft + getOffsetLeft(el.offsetParent): el.offsetLeft}\n";
            string strJs = null;
            switch (strType)
            {
                case "ID":
                    strJs = strGetTopJs + strGetLeftJs + $"return new Array(getOffsetLeft(document.getElementById(\"{strElement}\"){strIndex}), getOffsetTop(document.getElementById(\"{strElement}\"){strIndex}))";
                    break;

                case "Name":
                    strJs = strGetTopJs + strGetLeftJs + $"return new Array(getOffsetLeft(document.getElementByName(\"{strElement}\"){strIndex}), getOffsetTop(document.getElementByName(\"{strElement}\"){strIndex}))";
                    break;

                case "Tag":
                    strJs = strGetTopJs + strGetLeftJs + $"return new Array(getOffsetLeft(document.getELementsByTagName(\"{strElement}\"){strIndex}), getOffsetTop(document.getELementsByTagName(\"{strElement}\"){strIndex}))";
                    break;

                case "Class":
                    strJs = strGetTopJs + strGetLeftJs + $"return new Array(getOffsetLeft(document.getElementsByClassName(\"{strElement}\"){strIndex}), getOffsetTop(document.getElementsByClassName(\"{strElement}\"){strIndex}))";
                    break;
            }

            object[] jv = (object[])wView.RunJS(strJs);

            if (jv.Length == 2)
            {
                int iX = 25 + Convert.ToInt32(jv[0]);     // 加一点偏移，方便点击
                int iY = 10 + Convert.ToInt32(jv[1]) - GetSocrllHeightByJs(wView);

                return new Point(iX, iY);
            }
            else
            {
                return new Point(-9999, -9999);    // 指定查找的元素不存在
            }
        }
    }
}
