using MB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HookNetwork
{
    public partial class Form1 : Form
    {
        WebView m_wView = null;
        IntPtr m_AsynJob = IntPtr.Zero;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            m_wView = new WebView();
            m_wView.Bind(this);
            m_wView.LoadURL("https://www.baidu.com/");    // 不同网站的网络请求情况不同，请结合具体情况调试接口

            m_wView.OnLoadUrlBegin += OnLoadUrlBegin;
            m_wView.OnLoadUrlEnd += OnLoadUrlEnd;
            m_wView.OnNetResponse += OnNetResponse;
            m_wView.OnLoadUrlFail += OnLoadUrlFail;
        }

        private void OnLoadUrlBegin(object sender, LoadUrlBeginEventArgs e)
        {
            // 查询指定的网络请求
            string strUrl = e.URL;
            string strHttpHead = m_wView.NetGetHTTPHeaderField(e.Job, "cookie");    // 或accept、method等
            wkeRequestType RequestMethod = m_wView.GetRequestMethod(e.Job);
            wkeSlist rawHead = m_wView.NetGetRawHttpHead(e.Job);
            List<string> headList = new List<string>();
            while (true)    // wkeSlist是个链表结构
            {
                string strHead = rawHead.str.UTF8PtrToStr();
                if (strHead == null)
                {
                    break;
                }
                headList.Add(strHead);

                if (rawHead.next == IntPtr.Zero)
                {
                    break;
                }

                rawHead = (wkeSlist)rawHead.next.UTF8PtrToStruct(typeof(wkeSlist));
            }

            Dictionary<string, string> strPostData = new Dictionary<string, string>();
            if (RequestMethod == wkeRequestType.Post)
            {
                wkePostBodyElements eles = m_wView.NetGetPostBody(e.Job);
                //m_wView.NetFreePostBodyElements(eles.StructToUTF8Ptr());    // 如果需要也可以干掉这组post数据
                List<wkePostBodyElement> eleList = new List<wkePostBodyElement>();
                IntPtr ptrEles = eles.element;    // 这是个二维指针

                for (int i = 0; i < eles.elementSize; i++)    // wkePostBodyElements中的elements是个指针数组
                {
                    IntPtr ptrTemp = (IntPtr)ptrEles.UTF8PtrToStruct(typeof(IntPtr));
                    //m_wView.NetFreePostBodyElement(ptrTemp);    // 如果需要也可以干掉这个post数据
                    eleList.Add((wkePostBodyElement)ptrTemp.UTF8PtrToStruct(typeof(wkePostBodyElement)));
                    ptrEles = new IntPtr(ptrEles.ToInt64() + IntPtr.Size);
                }

                foreach (var item in eleList)
                {
                    if (item.type == wkeHttBodyElementType.wkeHttBodyElementTypeData)
                    {
                        wkeMemBuf memBuf = (wkeMemBuf)item.data.UTF8PtrToStruct(typeof(wkeMemBuf));
                        byte[] data = new byte[memBuf.length];
                        Marshal.Copy(memBuf.data, data, 0, data.Length);

                        string strData = Encoding.UTF8.GetString(data);
                        if (strData == string.Empty || strData == null || strData.StartsWith("--"))
                        {
                            continue;
                        }

                        foreach (string strKV in strData.Split('&'))
                        {
                            string[] kv = strKV.Split('=');
                            if (kv.Length == 2)    // 只保留合法的数据
                            {
                                strPostData.Add(kv[0], kv[1]);
                            }
                        }
                    }
                }
            }

            // 插入新的网络请求
            //wkePostBodyElement newEle = m_wView.NetCreatePostBodyElement();
            //wkePostBodyElements newEles = m_wView.NetCreatePostBodyElements(1000);

            HttpWebRequest newRequest = (HttpWebRequest)WebRequest.Create("新的url");
            newRequest.Method = "post";
            newRequest.AllowAutoRedirect = true;

            byte[] postData = Encoding.UTF8.GetBytes("新的要post的数据");
            newRequest.ContentLength = postData.Length;
            using (Stream reqStream = newRequest.GetRequestStream())
            {
                reqStream.Write(postData, 0, postData.Length);
                reqStream.Close();
            }

            WebResponse newResponse = newRequest.GetResponse();

            // 删除指定的网络请求
            if (strUrl.Contains("你想干掉的关键字"))
            {
                m_wView.NetCancelRequest(e.Job);
                e.Cancel = true;
            }

            // 修改指定的网络请求
            m_wView.NetSetData(e.Job, "alert('test')");    // 网络还未发送时，塞一段自己的数据到数据流中，有三个重载
            m_wView.NetHookRequest(e.Job);    // 设置此钩子后，收到响应数据流后，会触发OnLoadUrlEnd，数据在会触发OnLoadUrlEnd中修改
            m_wView.NetChangeRequestUrl(e.Job, "新的url");    // 修改url
            m_wView.NetSetMIMEType(e.Job, "text/html");    // 设置新的mime

            // 如果某个请求需要进行一些耗时修改或其他操作，不想影响主流程可以异步
            if (m_wView.NetHoldJobToAsynCommit(e.Job))
            {
                Task.Factory.StartNew(() =>
                {
                    m_AsynJob = e.Job;    // 等异步操作结束后，job可能已经变成其他任务的了，所以要保存下来
                    File.ReadAllText("某个文件");    // 或等待其他响应等耗时操作
                }).ContinueWith(arg =>
                {
                    Invoke(new Action(() => 
                    {
                        m_wView.NetContinueJob(m_AsynJob);
                    }));
                });
            }
        }

        private void OnLoadUrlEnd(object sender, LoadUrlEndEventArgs e)
        {
            string strData = Encoding.UTF8.GetString(e.Data);
            if (strData.Contains("你关注的数据内容"))
            {
                e.Data = Encoding.UTF8.GetBytes("修改后的数据");
            }
        }

        private void OnNetResponse(object sender, NetResponseEventArgs e)
        {
            string strUrl = e.URL;
            string strResponseHead = m_wView.NetGetHTTPHeaderFieldFromResponse(e.Job, "content-type");    // 或status等
            string strMimeType = m_wView.NetGetMIMEType(e.Job);

            wkeSlist rawResponseHead = m_wView.NetGetRawResponseHead(e.Job);
            List<string> headList = new List<string>();
            while (true)    // wkeSlist是个链表结构
            {
                string strHead = rawResponseHead.str.UTF8PtrToStr();
                if (strHead == null)
                {
                    break;
                }
                headList.Add(strHead);

                if (rawResponseHead.next == IntPtr.Zero)
                {
                    break;
                }

                rawResponseHead = (wkeSlist)rawResponseHead.next.UTF8PtrToStruct(typeof(wkeSlist));
            }
        }

        private void OnLoadUrlFail(object sender, LoadUrlFailEventArgs e)
        {
            string strUrl = e.URL;
        }
    }
}
