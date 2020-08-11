using MB;
using System;
using System.IO;
using System.Text;
using System.Drawing;
using HtmlAgilityPack;
using System.Windows.Forms;
using System.Collections.Generic;


namespace ReptilesData
{
    public partial class Form1 : Form
    {
        TabPage m_newtabPage = null;
        IntPtr m_currentWHD = IntPtr.Zero;

        WebView m_wView = null;
        WebView m_wView_new = null;

        StreamWriter m_log = null;

        int m_iTotalScroll = 0;
        int m_iCurrentScroll = 0;

        char[] m_inputCharArr = null;
        int m_iInputIndex = 0;

        int m_iMoveIndex = 0;
        Point m_TargetPoint = new Point();
        List<Point> m_posList = new List<Point>();
        MoveType m_MoveType = 0;
        enum MoveType
        {
            RightDownX,
            RightDownY,
            RightUpX,
            RightUpY,
            LeftDownX,
            LeftDownY,
            LeftUpX,
            LeftUpY
        }

        int m_iDelIndex = 0;

        int m_iActionStep = 0;
        //List<Func<object[], object>> m_TaskActionList = new List<Func<object[], object>>();
        List<Tuple<Action<object[]>, object[]>> m_TaskActionList = new List<Tuple<Action<object[]>, object[]>>();

        string m_strKey1 = "浏览器内核";
        string m_strKey2 = "世界上最牛逼没有之一的浏览器内核MiniBlink";
        string m_strUrl = "https://www.miniblink.net/";

        public Form1()
        {
            InitializeComponent();
#if DEBUG
            m_log = File.AppendText("日志.txt");
#endif
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            m_currentWHD = tabPage_webView.Handle;

            m_wView = new WebView();
            m_wView.Bind(tabPage_webView);
            m_wView.NavigationToNewWindowEnable = true;

            m_wView.OnTitleChange += new EventHandler<TitleChangeEventArgs>(m_wke_OnTitleChange);
            m_wView.OnCreateView += new EventHandler<CreateViewEventArgs>(m_wke_OnCreateView);

            // 设置任务内容，只能线性执行，不能有判断逻辑，如果有必须进行封装后在此调用
            m_TaskActionList.Add(new Tuple<Action<object[]>, object[]>(LoadUrl, new object[] { "https://www.baidu.com", 5 }));
            m_TaskActionList.Add(new Tuple<Action<object[]>, object[]>(mouseMoveTo, new object[] { "Element", 0.008, "kw", "ID", "" }));
            m_TaskActionList.Add(new Tuple<Action<object[]>, object[]>(mouseClick, new object[] { 1 }));
            m_TaskActionList.Add(new Tuple<Action<object[]>, object[]>(inputChar, new object[] { m_strKey1, 0.3 }));
            m_TaskActionList.Add(new Tuple<Action<object[]>, object[]>(commonDelay, new object[] { 2.5 }));
            m_TaskActionList.Add(new Tuple<Action<object[]>, object[]>(delChar, new object[] { 0.2 }));
            m_TaskActionList.Add(new Tuple<Action<object[]>, object[]>(commonDelay, new object[] { 2.5 }));
            m_TaskActionList.Add(new Tuple<Action<object[]>, object[]>(inputChar, new object[] { m_strKey2, 0.3 }));
            m_TaskActionList.Add(new Tuple<Action<object[]>, object[]>(sendKey, new object[] { Common.VK_RETURN, 5 }));
            m_TaskActionList.Add(new Tuple<Action<object[]>, object[]>(isContainsAddress, new object[] { 1 }));
            m_TaskActionList.Add(new Tuple<Action<object[]>, object[]>(mouseWheel, new object[] { 0.008, "Fixed", -3000 }));
            m_TaskActionList.Add(new Tuple<Action<object[]>, object[]>(commonDelay, new object[] { 1.5 }));
            m_TaskActionList.Add(new Tuple<Action<object[]>, object[]>(mouseMoveTo, new object[] { "Element", 0.015, "pc", "Class", "[1]" }));
            m_TaskActionList.Add(new Tuple<Action<object[]>, object[]>(commonDelay, new object[] { 3 }));
            m_TaskActionList.Add(new Tuple<Action<object[]>, object[]>(mouseClick, new object[] { 5 }));
            m_TaskActionList.Add(new Tuple<Action<object[]>, object[]>(isContainsAddress, new object[] { 2 }));
            m_TaskActionList.Add(new Tuple<Action<object[]>, object[]>(mouseWheel, new object[] { 0.006, "Fixed", -3000 }));
            m_TaskActionList.Add(new Tuple<Action<object[]>, object[]>(commonDelay, new object[] { 3 }));
            m_TaskActionList.Add(new Tuple<Action<object[]>, object[]>(mouseMoveTo, new object[] { "Element", 0.015, "pc", "Class", "[2]" }));
            m_TaskActionList.Add(new Tuple<Action<object[]>, object[]>(commonDelay, new object[] { 3 }));
            m_TaskActionList.Add(new Tuple<Action<object[]>, object[]>(mouseClick, new object[] { 5 }));
            m_TaskActionList.Add(new Tuple<Action<object[]>, object[]>(isContainsAddress, new object[] { 3 }));
            m_TaskActionList.Add(new Tuple<Action<object[]>, object[]>(mouseWheel, new object[] { 0.006, "Fixed", -3000 }));
            m_TaskActionList.Add(new Tuple<Action<object[]>, object[]>(commonDelay, new object[] { 0.9 }));
            m_TaskActionList.Add(new Tuple<Action<object[]>, object[]>(mouseMoveTo, new object[] { "Element", 0.015, "pc", "Class", "[3]" }));
            m_TaskActionList.Add(new Tuple<Action<object[]>, object[]>(commonDelay, new object[] { 3 }));
            m_TaskActionList.Add(new Tuple<Action<object[]>, object[]>(mouseClick, new object[] { 5 }));
            m_TaskActionList.Add(new Tuple<Action<object[]>, object[]>(isContainsAddress, new object[] { 4 }));
            m_TaskActionList.Add(new Tuple<Action<object[]>, object[]>(mouseWheel, new object[] { 0.006, "Fixed", -3000 }));
            m_TaskActionList.Add(new Tuple<Action<object[]>, object[]>(commonDelay, new object[] { 2.2 }));
            m_TaskActionList.Add(new Tuple<Action<object[]>, object[]>(mouseMoveTo, new object[] { "Element", 0.015, "pc", "Class", "[4]" }));
            m_TaskActionList.Add(new Tuple<Action<object[]>, object[]>(commonDelay, new object[] { 3 }));
            m_TaskActionList.Add(new Tuple<Action<object[]>, object[]>(mouseClick, new object[] { 5 }));
            m_TaskActionList.Add(new Tuple<Action<object[]>, object[]>(isContainsAddress, new object[] { 5 }));
            m_TaskActionList.Add(new Tuple<Action<object[]>, object[]>(taskFinish, new object[] { 0 }));
            m_TaskActionList.Add(new Tuple<Action<object[]>, object[]>(CloseBrower, null));

            commonDelay(new object[] { 1 });
        }

        private void OutText(string strText)
        {
            toolStripStatusLabel1.Text = $"{m_iActionStep}、{strText}";
#if DEBUG
            m_log.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} -> {m_iActionStep}、{strText}");
            m_log.Flush();
#endif
        }

        private void newTab(string strUrl)
        {
            m_newtabPage = new TabPage();
            m_newtabPage.Location = new Point(4, 22);
            m_newtabPage.Name = "tabPage_webView";
            m_newtabPage.Padding = new Padding(3);
            m_newtabPage.Size = new Size(1076, 536);
            m_newtabPage.TabIndex = 0;
            m_newtabPage.UseVisualStyleBackColor = true;

            tabControl1.Controls.Add(m_newtabPage);
            tabControl1.SelectedIndex = 1;
            label1.Parent = m_newtabPage;

            m_wView_new = new WebView();

            m_wView_new.Bind(m_newtabPage);
            m_wView_new.OnTitleChange += new EventHandler<TitleChangeEventArgs>(m_wke_OnTitleChange);
            m_wView_new.LoadURL(strUrl);
        }

        void m_wke_OnTitleChange(object sender, TitleChangeEventArgs e)
        {
            WebView v = sender as WebView;
            if (v == m_wView)
            {
                tabPage_webView.Text = e.Title;
            }
            else if (v == m_wView_new)
            {
                m_newtabPage.Text = e.Title;
            }
        }

        void m_wke_OnCreateView(object sender, CreateViewEventArgs e)
        {
            newTab(e.URL);
            m_currentWHD = m_newtabPage.Handle;
            e.NewWebViewHandle = m_wView_new.Handle;
        }

        void ErrorProc()
        {
            m_TaskActionList.Insert(m_iActionStep, new Tuple<Action<object[]>, object[]>(taskFinish, new object[] { false }));
            m_TaskActionList.Insert(m_iActionStep, new Tuple<Action<object[]>, object[]>(CloseBrower, null));
        }

        private void LoadUrl(object[] args)
        {
            string strUrl = $"{args[0]}";
            float fDelay = float.Parse(args[1].ToString());

            m_wView.LoadURL(strUrl);

            CommonTimer.Interval = (int)(fDelay * 1000);
            CommonTimer.Start();

            OutText($"打开网站：{strUrl}，超时等待{fDelay}秒");
        }

        // 鼠标点击，参数0为点击后等待秒数，参数1为是否在新窗口打开
        private void mouseClick(object[] args)
        {
            int iX = label1.Location.X;
            int iY = label1.Location.Y;
            IntPtr lParam = (IntPtr)((iY << 16) + iX);
            float fDelay = 15;

            Common.SendMessage(m_currentWHD, Common.WM_LBUTTONDOWN, IntPtr.Zero, lParam);
            Common.SendMessage(m_currentWHD, Common.WM_LBUTTONUP, IntPtr.Zero, lParam);

            fDelay = float.Parse(args[0].ToString());

            CommonTimer.Interval = (int)(fDelay * 1000);
            CommonTimer.Start();

            OutText($"鼠标点击：x={iX}，y={iY}，等待{fDelay}秒钟");
        }

        // 页面后退，参数0后退后需要等待的秒数
        private void backLoad(object[] args)
        {
            label1.Parent = tabPage_webView;
            m_currentWHD = tabPage_webView.Handle;

            m_wView_new.Dispose();
            m_newtabPage.Dispose();
            tabControl1.Controls.Remove(m_newtabPage);

            float fDelay = float.Parse(args[0].ToString());

            CommonTimer.Interval = (int)(fDelay * 1000);
            CommonTimer.Start();

            OutText($"返回上一页，等待{fDelay}秒钟");
        }

        // 发送按键（Enter，Del等），参数0为发送的键值，参数1为需要等待的秒数
        private void sendKey(object[] args)
        {
            int iKeyValue = (int)args[0];
            float fDelay = float.Parse(args[1].ToString());

            if (iKeyValue == Common.VK_RETURN)
            {
                Common.SendMessage(m_currentWHD, Common.WM_CHAR, new IntPtr(iKeyValue), IntPtr.Zero);
            }
            else if (iKeyValue == Common.VK_END)
            {
                Common.SendMessage(m_currentWHD, Common.WM_KEYDOWN, new IntPtr(iKeyValue), IntPtr.Zero);
                Common.SendMessage(m_currentWHD, Common.WM_KEYUP, new IntPtr(iKeyValue), IntPtr.Zero);
            }

            CommonTimer.Interval = (int)(fDelay * 1000);
            CommonTimer.Start();

            OutText($"点击按键，键值：{iKeyValue}，等待{fDelay}秒钟");
        }

        // 通用延时，参数0为需要等待的秒数
        private void commonDelay(object[] args)
        {
            float iSecond = float.Parse(args[0].ToString());

            CommonTimer.Interval = (int)(iSecond * 1000);
            CommonTimer.Start();

            OutText($"等待{iSecond}秒");
        }

        // 向窗口发送字符，参数0为字符串，参数1为输入过程间隔（速度）
        private void inputChar(object[] args)
        {
            string strWord = $"{args[0]}";
            float fSpeed = float.Parse(args[1].ToString());

            m_iInputIndex = 0;
            m_inputCharArr = Encoding.Unicode.GetChars(Encoding.Unicode.GetBytes(strWord));

            InputTimer.Interval = (int)(fSpeed * 1000);
            InputTimer.Start();

            OutText($"输入搜索关键词：{strWord}，间隔速度：{fSpeed}秒");
        }

        // 删除字符串，参数0为删除过程间隔（速度）
        private void delChar(object[] args)
        {
            m_iDelIndex = 0;
            float fSpeed = float.Parse(args[0].ToString());

            DelTimer.Interval = (int)(fSpeed * 1000);
            DelTimer.Start();

            OutText($"删除搜索关键词，间隔速度：{fSpeed}秒");
        }

        // 鼠标滚动，参数0为滚动过程间隔（速度），正值向上，负值向下
        // 参数1为Fixed，则参数2为需要滚动的像素数
        // 参数1为Random，则参数2为随机滚动的正副值范围
        private void mouseWheel(object[] args)
        {
            int iPixel = 0;
            m_iCurrentScroll = 0;
            float fSpeed = float.Parse(args[0].ToString());

            if (args[1].ToString() == "Fixed")
            {
                iPixel = (int)args[2];
            }
            else if (args[1].ToString() == "Random")
            {
                Random random = new Random();
                int iRandom = (int)args[2];
                iPixel = random.Next(-iRandom, iRandom);
            }

            m_iTotalScroll = iPixel / 10;

            ScrollTimer.Interval = (int)(fSpeed * 1000);
            ScrollTimer.Start();

            OutText($"滚动{(args[1].ToString() == "Fixed" ? "固定" : "随机")}滚轮，{(iPixel < 0 ? "向下" : "向上") + Math.Abs(iPixel)}像素，间隔速度：{fSpeed}秒");
        }

        // 移动虚拟鼠标到指定的坐标（相对于浏览器窗口坐标）
        // 如果参数0为Element，则参数1为滚动过程间隔（速度），参数2为要移动到的元素，参数3为元素类型，参数4为元素索引
        // 如果参数0为Point，则参数1为滚动过程间隔（速度），参数2为当前鼠标位置X轴随机移动的正副值范围，参数3为Y轴随机移动的正副值范围
        private void mouseMoveTo(object[] args)
        {
            m_iMoveIndex = 0;
            m_posList.Clear();

            Point currentPoint = label1.Location;
            float fSpeed = float.Parse(args[1].ToString());

            if ($"{args[0]}" == "Element")
            {
                m_TargetPoint = Common.GetElementPointByJs(m_wView, $"{args[2]}", $"{args[3]}", $"{args[4]}");
                OutText($"移动鼠标到指定元素坐标，x={m_TargetPoint.X}，y={m_TargetPoint.Y}，间隔速度：{fSpeed}秒");
            }
            else if ($"{args[0]}" == "Point")
            {
                int iX = (int)args[2];
                int iY = (int)args[3];

                Random random = new Random();
                int iTargetX = currentPoint.X + random.Next(-iX, iX);
                iTargetX = iTargetX < 0 ? 0 : iTargetX;
                iTargetX = iTargetX > 1000 ? 1000 : iTargetX;

                int iTargetY = currentPoint.Y + random.Next(-iY, iY);
                iTargetY = iTargetY < 0 ? 0 : iTargetY;
                iTargetY = iTargetY > 920 ? 920 : iTargetY;

                m_TargetPoint = new Point(iTargetX, iTargetY);

                OutText($"移动鼠标到随机位置，x={m_TargetPoint.X}，y={m_TargetPoint.Y}，间隔速度：{fSpeed}秒");
            }

            if (m_TargetPoint.X == -9999 && m_TargetPoint.Y == -9999)
            {
                ErrorProc();

                CommonTimer.Interval = 2000;
                CommonTimer.Start();

                OutText($"查找元素{args[2]}不存在，任务失败");
                return;
            }

            int iSubX = currentPoint.X - m_TargetPoint.X;
            int iSubY = currentPoint.Y - m_TargetPoint.Y;

            if (iSubX >= 0 && iSubY >= 0)    // 当前位置相对目标位置在右下
            {
                if (Math.Abs(iSubX) >= Math.Abs(iSubY))
                {
                    m_MoveType = MoveType.RightDownX;
                    if (iSubY == 0)
                    {
                        iSubY = 1;
                    }
                    for (int i = 0; i < Math.Abs(iSubY); i++)
                    {
                        m_posList.Add(new Point(currentPoint.X - (Math.Abs(iSubX) / Math.Abs(iSubY) + 1) * (i + 1), currentPoint.Y - (i + 1)));
                    }
                }
                else
                {
                    m_MoveType = MoveType.RightDownY;
                    if (iSubX == 0)
                    {
                        iSubX = 1;
                    }
                    for (int i = 0; i < Math.Abs(iSubX); i++)
                    {
                        m_posList.Add(new Point(currentPoint.X - (i + 1), currentPoint.Y - (Math.Abs(iSubY) / Math.Abs(iSubX) + 1) * (i + 1)));
                    }
                }
            }
            else if (iSubX >= 0 && iSubY < 0)    // 当前位置相对目标位置在右上
            {
                if (Math.Abs(iSubX) >= Math.Abs(iSubY))
                {
                    m_MoveType = MoveType.RightUpX;
                    if (iSubY == 0)
                    {
                        iSubY = 1;
                    }
                    for (int i = 0; i < Math.Abs(iSubY); i++)
                    {
                        m_posList.Add(new Point(currentPoint.X - (Math.Abs(iSubX) / Math.Abs(iSubY) + 1) * (i + 1), currentPoint.Y + (i + 1)));
                    }
                }
                else
                {
                    m_MoveType = MoveType.RightUpY;
                    if (iSubX == 0)
                    {
                        iSubX = 1;
                    }
                    for (int i = 0; i < Math.Abs(iSubX); i++)
                    {
                        m_posList.Add(new Point(currentPoint.X - (i + 1), currentPoint.Y + (Math.Abs(iSubY) / Math.Abs(iSubX) + 1) * (i + 1)));
                    }
                }
            }
            else if (iSubX < 0 && iSubY >= 0)    // 当前位置相对目标位置在左下
            {
                if (Math.Abs(iSubX) >= Math.Abs(iSubY))
                {
                    m_MoveType = MoveType.LeftDownX;
                    if (iSubY == 0)
                    {
                        iSubY = 1;
                    }
                    for (int i = 0; i < Math.Abs(iSubY); i++)
                    {
                        m_posList.Add(new Point(currentPoint.X + (Math.Abs(iSubX) / Math.Abs(iSubY) + 1) * (i + 1), currentPoint.Y - (i + 1)));
                    }
                }
                else
                {
                    m_MoveType = MoveType.LeftDownY;
                    if (iSubX == 0)
                    {
                        iSubX = 1;
                    }
                    for (int i = 0; i < Math.Abs(iSubX); i++)
                    {
                        m_posList.Add(new Point(currentPoint.X + (i + 1), currentPoint.Y - (Math.Abs(iSubY) / Math.Abs(iSubX) + 1) * (i + 1)));
                    }
                }
            }
            else if (iSubX < 0 && iSubY < 0)    // 当前位置相对目标位置在左上
            {
                if (Math.Abs(iSubX) >= Math.Abs(iSubY))
                {
                    m_MoveType = MoveType.LeftUpX;
                    if (iSubY == 0)
                    {
                        iSubY = 1;
                    }
                    for (int i = 0; i < Math.Abs(iSubY); i++)
                    {
                        m_posList.Add(new Point(currentPoint.X + (Math.Abs(iSubX) / Math.Abs(iSubY) + 1) * (i + 1), currentPoint.Y + (i + 1)));
                    }
                }
                else
                {
                    m_MoveType = MoveType.LeftUpY;
                    if (iSubX == 0)
                    {
                        iSubX = 1;
                    }
                    for (int i = 0; i < Math.Abs(iSubX); i++)
                    {
                        m_posList.Add(new Point(currentPoint.X + (i + 1), currentPoint.Y + (Math.Abs(iSubY) / Math.Abs(iSubX) + 1) * (i + 1)));
                    }
                }
            }

            MouseMoveTimer.Interval = (int)(fSpeed * 1000);
            MouseMoveTimer.Start();
        }

        // 判断当前可视区域是否需要向下滚动，需要的话则向下滚动，把指定高度的坐标显示出来，
        // 参数0为滚动的速度，参数1为要移动到的元素，参数2为元素类型，参数3为元素索引
        private void isNeedDownSocll(object[] args)
        {
            Point TargetPoint = Common.GetElementPointByJs(m_wView, $"{args[1]}", $"{args[2]}", $"{args[3]}");
            int iScreenHeight = Common.GetScreenHeightByJs(m_wView);
            int iSocllHeight = Common.GetSocrllHeightByJs(m_wView);

            if (TargetPoint.Y > iSocllHeight + iScreenHeight)
            {
                int iRange = TargetPoint.Y - iScreenHeight - iSocllHeight + 240;
                mouseWheel(new object[] { 0.006F, "Fixed", -iRange });
            }
            else if (TargetPoint.Y < iSocllHeight)
            {
                int iRange = iSocllHeight - TargetPoint.Y + 50;
                mouseWheel(new object[] { 0.008F, "Fixed", iRange });
            }
            else
            {
                mouseWheel(new object[] { 0.007F, "Fixed", 0 });
            }
        }

        // 判断页面中有没有需要的网址，参数0是百度搜索结果第几页
        private void isContainsAddress(object[] args)
        {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(m_wView.GetSource());
            int iPage = (int)args[0] - 1;

            for (int i = 1; i <= 10; i++)
            {
                HtmlNode node = doc.DocumentNode.SelectSingleNode($"//*[@id=\"{iPage * 10 + i}\"]/div[1]/div[2]/div[2]/a[1]");
                if (node == null)
                {
                    continue;
                }

                string strUrl = node.InnerText;
                if (strUrl.Contains(m_strUrl) || m_strUrl.Contains(strUrl))
                {
                    m_TaskActionList.Insert(m_iActionStep, new Tuple<Action<object[]>, object[]>(isNeedDownSocll, new object[] { 0.009, $"{iPage * 10 + i}", "ID", "" }));
                    m_TaskActionList.Insert(m_iActionStep + 1, new Tuple<Action<object[]>, object[]>(mouseMoveTo, new object[] { "Element", 0.01, $"{iPage * 10 + i}", "ID", "" }));
                    m_TaskActionList.Insert(m_iActionStep + 2, new Tuple<Action<object[]>, object[]>(mouseClick, new object[] { 5 }));
                    m_TaskActionList.Insert(m_iActionStep + 3, new Tuple<Action<object[]>, object[]>(mouseWheel, new object[] { 0.05, "Random", 1000 }));
                    m_TaskActionList.Insert(m_iActionStep + 4, new Tuple<Action<object[]>, object[]>(mouseWheel, new object[] { 0.05, "Random", 1000 }));
                    m_TaskActionList.Insert(m_iActionStep + 5, new Tuple<Action<object[]>, object[]>(mouseWheel, new object[] { 0.05, "Random", 1000 }));
                    m_TaskActionList.Insert(m_iActionStep + 6, new Tuple<Action<object[]>, object[]>(mouseWheel, new object[] { 0.05, "Random", 1000 }));
                    m_TaskActionList.Insert(m_iActionStep + 7, new Tuple<Action<object[]>, object[]>(mouseWheel, new object[] { 0.05, "Random", 1000 }));
                    m_TaskActionList.Insert(m_iActionStep + 8, new Tuple<Action<object[]>, object[]>(commonDelay, new object[] { 5 }));
                    m_TaskActionList.Insert(m_iActionStep + 8, new Tuple<Action<object[]>, object[]>(taskFinish, new object[] { iPage * 10 + i }));
                    m_TaskActionList.Insert(m_iActionStep + 9, new Tuple<Action<object[]>, object[]>(CloseBrower, null));

                    OutText($"【匹配】，当前网址：{strUrl}，查找网址：{m_strUrl}");
                    break;
                }
                else
                {
                    OutText($"【不匹配】，当前网址：{strUrl}，查找网址：{m_strUrl}");
                }
            }

            CommonTimer.Interval = 2000;
            CommonTimer.Start();
        }

        // 任务结束，参数0表示是否匹配到指定的网址
        private void taskFinish(object[] args)
        {
            int iPos = (int)args[0];

            if (iPos != 0)
            {
                OutText("任务执行完毕，找到匹配结果\n\n\n");
                MessageBox.Show($"指定查找网站排名{iPos}位", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                OutText("任务执行完毕，百度前5页未找到匹配结果\n\n\n");
                MessageBox.Show("百度前5页未找到匹配结果", "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            CommonTimer.Interval = 2000;
            CommonTimer.Start();
        }

        private void CloseBrower(object[] args)
        {
            Common.SendMessage(Handle, Common.WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
        }

        private void ScrollTimer_Tick(object sender, EventArgs e)
        {
            if (m_iTotalScroll <= 0)
            {
                if (m_iCurrentScroll-- > m_iTotalScroll)
                {
                    Common.SendMessage(m_currentWHD, Common.WM_MOUSEWHEEL, new IntPtr(-10 << 16), IntPtr.Zero);
                }
                else
                {
                    ScrollTimer.Stop();
                    m_TaskActionList[m_iActionStep].Item1(m_TaskActionList[m_iActionStep++].Item2);
                }
            }
            else
            {
                if (m_iCurrentScroll++ < m_iTotalScroll)
                {
                    Common.SendMessage(m_currentWHD, Common.WM_MOUSEWHEEL, new IntPtr(10 << 16), IntPtr.Zero);
                }
                else
                {
                    ScrollTimer.Stop();
                    m_TaskActionList[m_iActionStep].Item1(m_TaskActionList[m_iActionStep++].Item2);
                }
            }
        }

        private void MouseMoveTimer_Tick(object sender, EventArgs e)
        {
            if (m_iMoveIndex < m_posList.Count)
            {
                int iMoveX = 0;
                int iMoveY = 0;
                switch (m_MoveType)
                {
                    case MoveType.RightDownX:
                    case MoveType.RightUpX:
                        iMoveX = m_posList[m_iMoveIndex].X < m_TargetPoint.X ? m_TargetPoint.X : m_posList[m_iMoveIndex].X;
                        iMoveY = m_posList[m_iMoveIndex].Y;
                        break;

                    case MoveType.RightDownY:
                    case MoveType.LeftDownY:
                        iMoveX = m_posList[m_iMoveIndex].X;
                        iMoveY = m_posList[m_iMoveIndex].Y < m_TargetPoint.Y ? m_TargetPoint.Y : m_posList[m_iMoveIndex].Y;
                        break;

                    case MoveType.RightUpY:
                    case MoveType.LeftUpY:
                        iMoveX = m_posList[m_iMoveIndex].X;
                        iMoveY = m_posList[m_iMoveIndex].Y > m_TargetPoint.Y ? m_TargetPoint.Y : m_posList[m_iMoveIndex].Y;
                        break;

                    case MoveType.LeftDownX:
                    case MoveType.LeftUpX:
                        iMoveX = m_posList[m_iMoveIndex].X > m_TargetPoint.X ? m_TargetPoint.X : m_posList[m_iMoveIndex].X;
                        iMoveY = m_posList[m_iMoveIndex].Y;
                        break;
                }

                m_iMoveIndex++;
                label1.Location = new Point(iMoveX, iMoveY);
            }
            else
            {
                MouseMoveTimer.Stop();
                m_TaskActionList[m_iActionStep].Item1(m_TaskActionList[m_iActionStep++].Item2);
            }
        }

        private void CommonTimer_Tick(object sender, EventArgs e)
        {
            CommonTimer.Stop();
            m_TaskActionList[m_iActionStep].Item1(m_TaskActionList[m_iActionStep++].Item2);
        }

        private void InputTimer_Tick(object sender, EventArgs e)
        {
            if (m_iInputIndex < m_inputCharArr.Length)
            {
                Common.SendMessage(m_currentWHD, Common.WM_CHAR, new IntPtr(m_inputCharArr[m_iInputIndex++]), IntPtr.Zero);
            }
            else
            {
                InputTimer.Stop();
                m_TaskActionList[m_iActionStep].Item1(m_TaskActionList[m_iActionStep++].Item2);
            }
        }

        private void DelTimer_Tick(object sender, EventArgs e)
        {
            if (m_iDelIndex++ < m_strKey1.Length)
            {
                Common.SendMessage(m_currentWHD, Common.WM_KEYDOWN, new IntPtr(Common.VK_BACK), IntPtr.Zero);
                Common.SendMessage(m_currentWHD, Common.WM_KEYUP, new IntPtr(Common.VK_BACK), IntPtr.Zero);
            }
            else
            {
                DelTimer.Stop();
                m_TaskActionList[m_iActionStep].Item1(m_TaskActionList[m_iActionStep++].Item2);
            }
        }
    }
}
