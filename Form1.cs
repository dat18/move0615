using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ensyu0608
{
    public partial class Form1 : Form
    {
        [System.Runtime.InteropServices.DllImport("winmm.dll")]
        private static extern int mciSendString(String command,
             StringBuilder buffer, int bufferSize, IntPtr hwndCallback);

        private string aliasName = "MediaFile";

        // const = 定数 / 初期化のみできて、実行中に変更できない
        const int LABEL_COUNT = 50;

        private static Random rand = new Random();
        int[] vx = new int[LABEL_COUNT];
        int[] vy = new int[LABEL_COUNT];
        //int vx1, vy1; -> vx[0], vy[0]
        //int vx2, vy2; -> vx[1], vy[1]
        //int vx3, vy3; -> vx[2], vy[2]

        Label[] labels = new Label[LABEL_COUNT];

        int StarCount = LABEL_COUNT;
        int frameCount = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            frameCount++;
            label2.Text = "Time " + frameCount.ToString("#,0");

            // マウス座標
            Point mpos = MousePosition;
            mpos = PointToClient(mpos);

            for (int i = 0; i < LABEL_COUNT; i++)
            {
                labels[i].Left += vx[i];
                labels[i].Top += vy[i];

                if (labels[i].Left < 0)
                {
                    vx[i] = Math.Abs(vx[i]);
                }
                if (labels[i].Top < 0)
                {
                    vy[i] = Math.Abs(vy[i]);
                }
                if (labels[i].Left > ClientSize.Width - labels[i].Width)
                {
                    vx[i] = -Math.Abs(vx[i]);
                }
                if (labels[i].Top > ClientSize.Height - labels[i].Height)
                {
                    vy[i] = -Math.Abs(vy[i]);
                }

                // マウス判定
                if (    (mpos.X >= labels[i].Left)
                    &&  (mpos.X < labels[i].Left+labels[i].Width)
                    &&  (mpos.Y >= labels[i].Top)
                    &&  (mpos.Y < labels[i].Top+labels[i].Height)) {
                        if (labels[i].Visible)
                        {
                            labels[i].Visible = false;
                            StarCount--;
                            if (StarCount <= 0)
                            {
                                timer1.Enabled = false;
                                button1.Visible = true;
                            }
                        }
                }
            }

            /*
            // 1
            label1.Left += vx[0];
            label1.Top += vy[0];

            if (label1.Left < 0)
            {
                vx[0] = Math.Abs(vx[0]);
            }
            if (label1.Top < 0)
            {
                vy[0] = Math.Abs(vy[0]);
            }
            if (label1.Left > ClientSize.Width - label1.Width)
            {
                vx[0] = -Math.Abs(vx[0]);
            }
            if (label1.Top > ClientSize.Height - label1.Height)
            {
                vy[0] = -Math.Abs(vy[0]);
            }

            // 2
            label2.Left += vx[1];
            label2.Top += vy[1];

            if (label2.Left < 0)
            {
                vx[1] = Math.Abs(vx[1]);
            }
            if (label2.Top < 0)
            {
                vy[1] = Math.Abs(vy[1]);
            }
            if (label2.Left > ClientSize.Width - label2.Width)
            {
                vx[1] = -Math.Abs(vx[1]);
            }
            if (label2.Top > ClientSize.Height - label2.Height)
            {
                vy[1] = -Math.Abs(vy[1]);
            }

            // 3
            label3.Left += vx[2];
            label3.Top += vy[2];

            if (label3.Left < 0)
            {
                vx[2] = Math.Abs(vx[2]);
            }
            if (label3.Top < 0)
            {
                vy[2] = Math.Abs(vy[2]);
            }
            if (label3.Left > ClientSize.Width - label3.Width)
            {
                vx[2] = -Math.Abs(vx[2]);
            }
            if (label3.Top > ClientSize.Height - label3.Height)
            {
                vy[2] = -Math.Abs(vy[2]);
            }
            */

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //再生するファイル名
            string fileName = "bgm_maoudamashii_neorock83.mp3";

    string cmd;
    //ファイルを開く
    cmd = "open \"" + fileName + "\" type mpegvideo alias " + aliasName;
    if (mciSendString(cmd, null, 0, IntPtr.Zero) != 0)
        return;
    //再生する
    cmd = "play " + aliasName;
    mciSendString(cmd, null, 0, IntPtr.Zero);



            for (int i = 0; i < LABEL_COUNT; i++)
            {
                labels[i] = new Label();
                labels[i].AutoSize = true;
                labels[i].Text = "★";
                Controls.Add(labels[i]);
                labels[i].Font = label1.Font;
                labels[i].ForeColor = label1.ForeColor;

                labels[i].Left = rand.Next(0, ClientSize.Width - labels[i].Width);
                labels[i].Top = rand.Next(0, ClientSize.Height - labels[i].Height);
                vx[i] = rand.Next(-10, 11);
                vy[i] = rand.Next(-10, 11);
            }

            /*
            // 1
            label1.Left = rand.Next(0, ClientSize.Width - label1.Width);
            label1.Top = rand.Next(0, ClientSize.Height - label1.Height);
            vx[0] = rand.Next(-10, 11);
            vy[0] = rand.Next(-10, 11);

            // 2
            label2.Left = rand.Next(0, ClientSize.Width - label2.Width);
            label2.Top = rand.Next(0, ClientSize.Height - label2.Height);
            vx[1] = rand.Next(-10, 11);
            vy[1] = rand.Next(-10, 11);

            // 3
            label3.Left = rand.Next(0, ClientSize.Width - label3.Width);
            label3.Top = rand.Next(0, ClientSize.Height - label3.Height);
            vx[2] = rand.Next(-10, 11);
            vy[2] = rand.Next(-10, 11);
            */
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            StarCount = LABEL_COUNT;
            frameCount = 0;
            for (int i = 0; i < LABEL_COUNT; i++)
            {
                labels[i].Visible = true;
                button1.Visible = false;
            }
        }
    }
}
