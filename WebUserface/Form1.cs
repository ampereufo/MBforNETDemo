using MB;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebUserface
{
    public partial class Form1 : Form, IMessageFilter
    {
        private enum ResizeDirect
        {
            None,
            Left,
            Right,
            Top,
            Bottom,
            LeftTop,
            LeftBottom,
            RightTop,
            RightBottom
        }

        private WebView m_wView = null;
        private ResizeDirect m_rDirect;
        private int m_iResize = 3;
        private bool m_bIsDrop = false;

        public Form1()
        {
            InitializeComponent();
            Application.AddMessageFilter(this);
        }

        public bool PreFilterMessage(ref Message m)
        {
            if (IsDisposed)
            {
                return false;
            }

            if (FormBorderStyle != FormBorderStyle.None)
            {
                return false;
            }

            if (WindowState != FormWindowState.Normal)
            {
                return false;
            }

            var ctrl = FromChildHandle(m.HWnd);
            if (ctrl == null || ctrl.FindForm() != this)
            {
                return false;
            }

            if ((WinConst)m.Msg == WinConst.WM_MOUSEMOVE)
            {
                m_rDirect = SetCursor(PointToClient(MousePosition));
            }
            else if ((WinConst)m.Msg == WinConst.WM_LBUTTONDOWN)
            {
                if (m_rDirect != ResizeDirect.None)
                {
                    ResizeMsg();
                    return true;
                }
            }

            return false;
        }

        private string GetDemoHtml()
        {
            string strHtml = string.Join(".", typeof(Form1).Namespace, "HtmlDemo.html");
            using (Stream st = typeof(Form1).Assembly.GetManifestResourceStream(strHtml))
            {
                StreamReader sr = new StreamReader(st, Encoding.UTF8);
                strHtml = sr.ReadToEnd();
            }

            return strHtml;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            m_wView = new WebView();
            m_wView.Bind(this);
            m_wView.LoadHTML(GetDemoHtml());

            m_wView.BindFunction("max", new wkeJsNativeFunction(MaxFunc));
            m_wView.BindFunction("min", new wkeJsNativeFunction(MinFunc));
            m_wView.BindFunction("shut", new wkeJsNativeFunction(CloseFunc));
            m_wView.BindFunction("drag", new wkeJsNativeFunction(onDrag));
            m_wView.BindFunction("transparent", new wkeJsNativeFunction(SetTransparent));

            // 增加了拖拽支持，也是参考了凹大神的代码 https://gitee.com/aochulai/NetMiniblink
            DragDrop += DragFileDrop;
            DragEnter += DragFileEnter;
        }

        private long MaxFunc(IntPtr es, IntPtr param)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                WindowState = FormWindowState.Normal;
                m_wView.RunJS("document.getElementById('max-btn').innerHTML = '□'");
            }
            else if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
                m_wView.RunJS("document.getElementById('max-btn').innerHTML = '♢'");    // 没找到小方块的符号，只能用♢代替了
            }

            return 0;
        }

        private long MinFunc(IntPtr es, IntPtr param)
        {
            WindowState = FormWindowState.Minimized;
            return 0;
        }

        private long CloseFunc(IntPtr es, IntPtr param)
        {
            Close();
            return 0;
        }

        // 参考了凹大神的代码 https://gitee.com/aochulai/NetMiniblink ，增加拖动到屏幕上方最大化功能
        private long onDrag(IntPtr es, IntPtr param)
        {
            if (!m_bIsDrop && MouseButtons == MouseButtons.Left)
            {
                if (WindowState == FormWindowState.Maximized)
                {
                    WindowState = FormWindowState.Normal;
                    m_wView.RunJS("document.getElementById('max-btn').innerHTML = '□'");
                    Location = new Point(Location.X, MousePosition.Y - 17);
                }

                m_bIsDrop = true;
                m_wView.MouseEnabled = false;
                Point dropPos = MousePosition;
                Point dropLoc = Location;
                Point last = MousePosition;

                Task.Factory.StartNew(() =>
                {
                    SpinWait waiter = new SpinWait();
                    while (MouseButtons.HasFlag(MouseButtons.Left))
                    {
                        Point curr = MousePosition;
                        if (!curr.Equals(last))
                        {
                            int iX = curr.X + dropLoc.X - dropPos.X;
                            int iY = curr.Y + dropLoc.Y - dropPos.Y;

                            Invoke(new Action(() =>
                            {
                                Location = new Point(iX, iY);
                                Cursor = Cursors.SizeAll;

                                if (iY <= 0)
                                {
                                    WindowState = FormWindowState.Maximized;
                                    m_wView.RunJS("document.getElementById('max-btn').innerHTML = '♢'");
                                }
                            }));

                            last = curr;
                        }

                        waiter.SpinOnce();
                    }

                    Invoke(new Action(() =>
                    {
                        ResetCursor();
                        m_wView.MouseEnabled = true;
                    }));

                    m_bIsDrop = false;
                });
            }

            return 0;
        }

        private long SetTransparent(IntPtr es, IntPtr param)
        {
            int iStyle = MB_Common.GetWindowLong(Handle, (int)WinConst.GWL_EXSTYLE);
            MB_Common.SetWindowLong(Handle, (int)WinConst.GWL_EXSTYLE, iStyle | (int)WinConst.WS_EX_LAYERED);

            m_wView.Transparent = m_wView.Transparent ? false : true;
            return 0;
        }

        private ResizeDirect SetCursor(Point point)
        {
            ResizeDirect direct = ResizeDirect.None;

            if (point.X <= m_iResize)
            {
                if (point.Y <= m_iResize)
                {
                    direct = ResizeDirect.LeftTop;
                    Cursor = Cursors.SizeNWSE;
                }
                else if (point.Y + m_iResize >= ClientRectangle.Height)
                {
                    direct = ResizeDirect.LeftBottom;
                    Cursor = Cursors.SizeNESW;
                }
                else
                {
                    direct = ResizeDirect.Left;
                    Cursor = Cursors.SizeWE;
                }
            }
            else if (point.Y <= m_iResize)
            {
                if (point.X <= m_iResize)
                {
                    direct = ResizeDirect.LeftTop;
                    Cursor = Cursors.SizeNWSE;
                }
                else if (point.X + m_iResize >= ClientRectangle.Width)
                {
                    direct = ResizeDirect.RightTop;
                    Cursor = Cursors.SizeNESW;
                }
                else
                {
                    direct = ResizeDirect.Top;
                    Cursor = Cursors.SizeNS;
                }
            }
            else if (point.X + m_iResize >= ClientRectangle.Width)
            {
                if (point.Y <= m_iResize)
                {
                    direct = ResizeDirect.RightTop;
                    Cursor = Cursors.SizeNESW;
                }
                else if (point.Y + m_iResize >= ClientRectangle.Height)
                {
                    direct = ResizeDirect.RightBottom;
                    Cursor = Cursors.SizeNWSE;
                }
                else
                {
                    direct = ResizeDirect.Right;
                    Cursor = Cursors.SizeWE;
                }
            }
            else if (point.Y + m_iResize >= ClientRectangle.Height)
            {
                if (point.X <= m_iResize)
                {
                    direct = ResizeDirect.LeftBottom;
                    Cursor = Cursors.SizeNESW;
                }
                else if (point.X + m_iResize >= ClientRectangle.Width)
                {
                    direct = ResizeDirect.RightBottom;
                    Cursor = Cursors.SizeNWSE;
                }
                else
                {
                    direct = ResizeDirect.Bottom;
                    Cursor = Cursors.SizeNS;
                }
            }
            else if (!m_bIsDrop)
            {
                Cursor = DefaultCursor;
            }

            return direct;
        }

        private void ResizeMsg()
        {
            const int wmszLeft = 0xF001;
            const int wmszRight = 0xF002;
            const int wmszTop = 0xF003;
            const int wmszTopleft = 0xF004;
            const int wmszTopright = 0xF005;
            const int wmszBottom = 0xF006;
            const int wmszBottomleft = 0xF007;
            const int wmszBottomright = 0xF008;

            int iParam = 0;
            switch (m_rDirect)
            {
                case ResizeDirect.Top:
                    {
                        Cursor = Cursors.SizeNS;
                        iParam = wmszTop;
                        break;
                    }
                    
                case ResizeDirect.Bottom:
                    {
                        Cursor = Cursors.SizeNS;
                        iParam = wmszBottom;
                        break;
                    }

                case ResizeDirect.Left:
                    {
                        Cursor = Cursors.SizeWE;
                        iParam = wmszLeft;
                        break;
                    }

                case ResizeDirect.Right:
                    {
                        Cursor = Cursors.SizeWE;
                        iParam = wmszRight;
                        break;
                    }

                case ResizeDirect.LeftTop:
                    {
                        Cursor = Cursors.SizeNWSE;
                        iParam = wmszTopleft;
                        break;
                    }

                case ResizeDirect.LeftBottom:
                    {
                        Cursor = Cursors.SizeNESW;
                        iParam = wmszBottomleft;
                        break;
                    }

                case ResizeDirect.RightTop:
                    {
                        Cursor = Cursors.SizeNESW;
                        iParam = wmszTopright;
                        break;
                    }

                case ResizeDirect.RightBottom:
                    {
                        Cursor = Cursors.SizeNWSE;
                        iParam = wmszBottomright;
                        break;
                    }

                default:
                    {
                        break;
                    }
            }

            if (iParam != 0)
            {
                MB_Common.SendMessage(Handle, (uint)WinConst.WM_SYSCOMMAND, new IntPtr(0xF000 | iParam), IntPtr.Zero);
            }
        }

        private void OnDropFiles(bool isDone, int x, int y, string[] files)
        {
            string data = string.Join(",", files);
            m_wView.CallJsFunc("fireDropFileEvent", data, x, y, isDone);
        }

        private void DragFileEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
                Array items = (Array)e.Data.GetData(DataFormats.FileDrop);
                string[] files = items.Cast<string>().ToArray();
                Point p = PointToClient(new Point(e.X, e.Y));

                OnDropFiles(false, p.X, p.Y, files);
            }
        }

        private void DragFileDrop(object sender, DragEventArgs e)
        {
            Array items = (Array)e.Data.GetData(DataFormats.FileDrop);
            string[] files = items.Cast<string>().ToArray();
            Point p = PointToClient(new Point(e.X, e.Y));

            OnDropFiles(true, p.X, p.Y, files);
        }
    }
}
