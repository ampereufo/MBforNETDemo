using MB;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Download
{
    public partial class Form1 : Form
    {
        WebView m_wView = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            panel1.Location = new Point(panel1.Location.X, Height);

            m_wView = new WebView();
            m_wView.Bind(this);
            m_wView.LoadURL("https://pc.weixin.qq.com/");

            m_wView.OnDownload += OnDownload;
        }

        void OnDownload(object sender, DownloadEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "All files(*.*)|*.*";
            sfd.RestoreDirectory = true;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                e.SaveFilePath = sfd.FileName;
                timer1.Start();
            }
            
            FileStream file = File.Create(e.SaveFilePath);

            e.Progress += (obj, pe) =>
            {
                Invoke(new Action(() =>
                {
                    label1.Text = $"{Math.Round(((double)pe.Received / pe.Total * 100), 2)}%";
                    progressBar1.Value = (int)pe.Received;
                }));

                file.Write(pe.Data, 0, pe.Data.Length);
            };

            e.Finish += (obj, fe) =>
            {
                Invoke(new Action(() =>
                {
                    label1.Text = "下载完成";
                }));
                
                file.Close();

                if (fe.Error != null)
                {
                    File.Delete(e.SaveFilePath);
                }
            };

            StartDownload(e);
        }

        int m_iPanelY = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if ((m_iPanelY += 2) < 70)
            {
                panel1.Location = new Point(panel1.Location.X, Height - m_iPanelY);
            }
            else
            {
                timer1.Stop();
            }
        }

        public void StartDownload(DownloadEventArgs e)
        {
            HttpWebRequest downloadRequest = (HttpWebRequest)WebRequest.Create(e.URL);
            downloadRequest.Method = "get";
            downloadRequest.AllowAutoRedirect = true;

            WebResponse response = downloadRequest.GetResponse();
            e.ContentLength = response.ContentLength;

            progressBar1.Maximum = (int)e.ContentLength;

            Task.Factory.StartNew(() =>
            {
                int iSeek = 0;
                long iReceive = 0;
                byte[] data = new byte[1024 * 128];
                MemoryStream mBuf = new MemoryStream(1024 * 128);
                Stream rs = response.GetResponseStream();

                while ((iSeek = rs.Read(data, 0, data.Length)) > 0)
                {
                    mBuf.Write(data, 0, iSeek);
                    iReceive += iSeek;

                    if (iReceive <= e.ContentLength)
                    {
                        Invoke(new Action<DownloadProgressEventArgs>(e.OnProgress), new DownloadProgressEventArgs
                        {
                            Total = e.ContentLength,
                            Received = iReceive,
                            Data = mBuf.ToArray(),
                            Cancel = false,
                        });

                        mBuf.SetLength(0);
                        mBuf.Position = 0;
                    }
                    else
                    {
                        Invoke(new Action(() =>
                        {
                            label1.Text = "数据异常，下载失败";
                            throw new Exception("数据异常，下载失败");
                        }));
                    }
                }
            }).ContinueWith((task) =>
            {
                Invoke(new Action<DownloadFinishEventArgs>(e.OnFinish), new DownloadFinishEventArgs
                {
                    Error = null,
                    IsCompleted = true
                });

                response?.Close();
            });
        }
    }
}
