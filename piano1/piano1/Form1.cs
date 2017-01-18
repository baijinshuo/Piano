using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Media;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Drawing;

namespace piano1
{
    public partial class Form1 : Form
    {
        private StreamReader 指导曲目StreamReader;
       
        int i = 1,k=1;
        [DllImport("Winmm.dll")]
        private static extern int waveOutSetVolume(int hwo, System.UInt32 pdwVolume);//设置音量
        [DllImport("Winmm.dll")]
        private static extern uint waveOutGetVolume(IntPtr hwo, out   System.UInt32 pdwVolume); //获取音量
        //存放上下移动时移动方向
        public static int j = 0;
        //存放移动速度
        public static int space = 40;

        Button[] b = new Button[88];
            SoundPlayer[] bSound=
            {new SoundPlayer(piano1.Properties.Resources._1),
            new SoundPlayer(piano1.Properties.Resources._2),
            new SoundPlayer(piano1.Properties.Resources._3),
            new SoundPlayer(piano1.Properties.Resources._4),
            new SoundPlayer(piano1.Properties.Resources._5),
            new SoundPlayer(piano1.Properties.Resources._6),
            new SoundPlayer(piano1.Properties.Resources._7),
            new SoundPlayer(piano1.Properties.Resources._8),
            new SoundPlayer(piano1.Properties.Resources._9),
            new SoundPlayer(piano1.Properties.Resources._10),

            new SoundPlayer(piano1.Properties.Resources._11),
            new SoundPlayer(piano1.Properties.Resources._12),
            new SoundPlayer(piano1.Properties.Resources._13),
            new SoundPlayer(piano1.Properties.Resources._14),
            new SoundPlayer(piano1.Properties.Resources._15),
            new SoundPlayer(piano1.Properties.Resources._16),
            new SoundPlayer(piano1.Properties.Resources._17),
            new SoundPlayer(piano1.Properties.Resources._18),
            new SoundPlayer(piano1.Properties.Resources._19),
            new SoundPlayer(piano1.Properties.Resources._20),
            
            new SoundPlayer(piano1.Properties.Resources._21),
            new SoundPlayer(piano1.Properties.Resources._22),
            new SoundPlayer(piano1.Properties.Resources._23),
            new SoundPlayer(piano1.Properties.Resources._24),
            new SoundPlayer(piano1.Properties.Resources._25),
            new SoundPlayer(piano1.Properties.Resources._26),
            new SoundPlayer(piano1.Properties.Resources._27),
            new SoundPlayer(piano1.Properties.Resources._28),
            new SoundPlayer(piano1.Properties.Resources._29),
            new SoundPlayer(piano1.Properties.Resources._30),
            
            new SoundPlayer(piano1.Properties.Resources._31),
            new SoundPlayer(piano1.Properties.Resources._32),
            new SoundPlayer(piano1.Properties.Resources._33),
            new SoundPlayer(piano1.Properties.Resources._34),
            new SoundPlayer(piano1.Properties.Resources._35),
            new SoundPlayer(piano1.Properties.Resources._36),
            new SoundPlayer(piano1.Properties.Resources._37),
            new SoundPlayer(piano1.Properties.Resources._38),
            new SoundPlayer(piano1.Properties.Resources._39),
            new SoundPlayer(piano1.Properties.Resources._40),
            
            new SoundPlayer(piano1.Properties.Resources._41),
            new SoundPlayer(piano1.Properties.Resources._42),
            new SoundPlayer(piano1.Properties.Resources._43),
            new SoundPlayer(piano1.Properties.Resources._44),
            new SoundPlayer(piano1.Properties.Resources._45),
            new SoundPlayer(piano1.Properties.Resources._46),
            new SoundPlayer(piano1.Properties.Resources._47),
            new SoundPlayer(piano1.Properties.Resources._48),
            new SoundPlayer(piano1.Properties.Resources._49),
            new SoundPlayer(piano1.Properties.Resources._50),
            
            new SoundPlayer(piano1.Properties.Resources._51),
            new SoundPlayer(piano1.Properties.Resources._52),
            new SoundPlayer(piano1.Properties.Resources._53),
            new SoundPlayer(piano1.Properties.Resources._54),
            new SoundPlayer(piano1.Properties.Resources._55),
            new SoundPlayer(piano1.Properties.Resources._56),
            new SoundPlayer(piano1.Properties.Resources._57),
            new SoundPlayer(piano1.Properties.Resources._58),
            new SoundPlayer(piano1.Properties.Resources._59),
            new SoundPlayer(piano1.Properties.Resources._60),
            
            new SoundPlayer(piano1.Properties.Resources._61),
            new SoundPlayer(piano1.Properties.Resources._62),
            new SoundPlayer(piano1.Properties.Resources._63),
            new SoundPlayer(piano1.Properties.Resources._64),
            new SoundPlayer(piano1.Properties.Resources._65),
            new SoundPlayer(piano1.Properties.Resources._66),
            new SoundPlayer(piano1.Properties.Resources._67),
            new SoundPlayer(piano1.Properties.Resources._68),
            new SoundPlayer(piano1.Properties.Resources._69),
            new SoundPlayer(piano1.Properties.Resources._70),
            
            new SoundPlayer(piano1.Properties.Resources._71),
            new SoundPlayer(piano1.Properties.Resources._72),
            new SoundPlayer(piano1.Properties.Resources._73),
            new SoundPlayer(piano1.Properties.Resources._74),
            new SoundPlayer(piano1.Properties.Resources._75),
            new SoundPlayer(piano1.Properties.Resources._76),
            new SoundPlayer(piano1.Properties.Resources._77),
            new SoundPlayer(piano1.Properties.Resources._78),
            new SoundPlayer(piano1.Properties.Resources._79),
            new SoundPlayer(piano1.Properties.Resources._80),
            
            new SoundPlayer(piano1.Properties.Resources._81),
            new SoundPlayer(piano1.Properties.Resources._82),
            new SoundPlayer(piano1.Properties.Resources._83),
            new SoundPlayer(piano1.Properties.Resources._84),
            new SoundPlayer(piano1.Properties.Resources._85),
            new SoundPlayer(piano1.Properties.Resources._86),
            new SoundPlayer(piano1.Properties.Resources._87),
            new SoundPlayer(piano1.Properties.Resources._88)};

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            实体键盘button.Focus();
            richTextBox1.Text += "'.'左边高八度 右边低八度\n";
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult responsrDialogResult;
            openFileDialog2.InitialDirectory = Directory.GetCurrentDirectory();
            responsrDialogResult = openFileDialog2.ShowDialog();
        }

        private void 保存ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DialogResult responsrDialogResult;
            saveFileDialog2.InitialDirectory = Directory.GetCurrentDirectory();
            responsrDialogResult = saveFileDialog2.ShowDialog();
        }

        private void 打开指导曲目ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult responsrDialogResult;
            openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
            responsrDialogResult = openFileDialog1.ShowDialog();
            try
            {
                if (responsrDialogResult != DialogResult.Cancel)
                {
                    指导曲目StreamReader = new StreamReader(openFileDialog1.FileName);
                    richTextBox1.Text += "请跟着下列字母进行演奏:\n";
                }
                while (指导曲目StreamReader.Peek() != -1)
                    richTextBox1.Text += 指导曲目StreamReader.ReadLine() + "\n";
                richTextBox1.Text += "\n你所演奏的曲谱为：\n";
            }
            catch
            {
                MessageBox.Show("文件为空！！");
            }
        }

        private void 保存曲谱ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult responsrDialogResult;
            saveFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
            responsrDialogResult = saveFileDialog1.ShowDialog();
            if (responsrDialogResult!=DialogResult.Cancel)
            {
                File.AppendAllText(saveFileDialog1.FileName, richTextBox1.Text);
            }
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 88; i++)
                bSound[i].Stop();
            if (指导曲目StreamReader!=null)
            指导曲目StreamReader.Close();
            this.Close();
        }

        private void 电脑键盘窗口ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(i % 2 == 0)
                groupBox2.Visible = true;
            else
                groupBox2.Visible = false;
            i++;
        }

        private void 实体键盘窗口ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (k % 2 == 0)
                实体键盘button.Enabled = true;
            else
                实体键盘button.Enabled = false;
            k++;
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }

        private void 音量trackBar_Scroll(object sender, EventArgs e)
        {
            System.UInt32 Value =
                (System.UInt32)
                ((double)0xffff * (double)音量trackBar.Value / (double)(音量trackBar.Maximum - 音量trackBar.Minimum));
            if (Value < 0) Value = 0;
            if (Value > 0xffff) Value = 0xffff;
            System.UInt32 left = (System.UInt32)Value;
            System.UInt32 right = (System.UInt32)Value;
            waveOutSetVolume(0, left << 16 | right); 
            uint v;
            IntPtr p = new IntPtr(0);
            uint i = waveOutGetVolume(p, out   v);
            uint vleft = v & 0xFFFF;
            uint vright = (v & 0xFFFF0000) >> 16;
            音量trackBar.Value =
                (int.Parse(vleft.ToString()) | int.Parse(vright.ToString())) *
                (this.音量trackBar.Maximum - this.音量trackBar.Minimum) / 0xFFFF;
        }

        private void 指导曲目comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string 欢乐颂String = "E E R T T R E W \n" + "Q Q W E E W W \n" + "E E R T T R E W \n"
            + "Q Q W E W Q Q  \n" + "W W E Q W E R E Q \n" + "W E R E Q Q W G \n" + "E E R T T R E W \n" + "E E R T T R E W \n";
            string 婚礼进行曲String = "HKKK HLJK HKNNMLKJKL\n" + "HKKK HLJK HKMOMKILMK\n" + "NMLII JKLL NMLII JKLL\n"
                + "HKKK HLJK HKMOMKILMK\n" + "ILMKK\n";
            string 梁祝String = "LJIH IGFE\n" + "NMNLMKJL IJLIJKJIH\n" + "LGIFHE FHE\n" + "CEFHI FHE\n"
                + "LOMLJLI\n" + "IJGFEFHICHFEFHE\n" + "JLGIFHE CECEFGIF\n" + "EFHILJIJIHFECH FHFECEFHE\n" + "JLIJIHGFE\n";
            string 小燕子String = "E T I Y T\n" + "E T Y I T\n"
                + "I P O I O I Y I T \n" + "E E T Y T Y I O T Y\n"+"E W Q W\n"+"W W E T T I W E T\n";
            string 生日快乐String = "EEFEHG EEFEIH\n" + "EELJHGF KKJHIH\n";
            string 卡农String = "（括号是一起按）\n" + "H-JLO E-ILN F-HJM C-GIL\n"
                + "D-FHK A-EHJ D-FHK E-GIL\n" + "(HQ)-JLO (EP)-GIL (OF)-HJM (NC)-GJL \n";
            string 致爱丽斯String = "QPQPQNPOMHJMNJLNOJQPQPQNPOMHJMNJONMNOPQLRQPKQPOJPONNJQQQ\n"
                + "QPQPQNPOMHJMNJLNOJQPQPQNPOMHJMNJONMORQQPPRTSR\n" + "QPONMMLMNOPPQRMOPNOPQSPNOPQSPNQQQQQP\n";
            string 梦中的婚礼String = "MMNNOONNMMJJHHFFLLKKJKLK KKLLMMNNLLIIKKJJIIKJ JFHJIJ FHJIJ FHKJK FHKJK KJKKLLMLMJ\n"
                + "QMOQPQ MOQPQ MORQR MORQR RQRRSSTSTQ\n" + "O JJKK IINN IIJJ HHMLM HHII GJIJ O OOPP ONML LMLJ O OOPP ONML LMLM\n";
            string 雪绒花String = "CEI HED CCCDEFE\n" + "CEI HED CEEFGHH\n" + "I EEGFECEH\n" + "FHIHGE\n" + "CEI HED\n" + "CEEFGHH\n";
            string 青花瓷String = "LLJ IJF IJLJ I\n" + "LLJ IJE IJLI H\n" + "HIJLMLJ LJJI I\n" + "HIH IHI IJLJ J\n"
                + "LLJ IJF IJLJI\n" + "LLJ IJE IJLIH\n" + "HIJ LMLJ LJJII EJIIH\n";
            string 北京欢迎你String = "QSQPQPQQPMO QP\n" + "POMOPQSPQTSSPO\n" + "POMOPQSPQTSSQ\n" + "PQPOSTQMQPPO\n"
                + "QSVSTTS QQ SS QS TV WV SQ P S Q Q\n" + "QS VS TV WV SQ SVT QP QS XW VV\n";
            string astring = "请跟着下列字母进行演奏:\n";
            string 指导曲目String = 指导曲目comboBox.Text;
            switch (指导曲目String)
            {
                case "欢乐颂":
                    richTextBox1.Text =astring+ 欢乐颂String;
                    break;
                case "婚礼进行曲":
                    richTextBox1.Text =astring+ 婚礼进行曲String;
                    break;
                case "梁祝":
                    richTextBox1.Text =astring+ 梁祝String;
                    break;
                case "小燕子":
                    richTextBox1.Text =astring+小燕子String;
                    break;
                case "生日快乐":
                    richTextBox1.Text =astring+ 生日快乐String;
                    break;
                case "卡农":
                    richTextBox1.Text =astring+ 卡农String;
                    break;
                case "致爱丽斯":
                    richTextBox1.Text =astring+ 致爱丽斯String;
                    break;
                case "梦中的婚礼":
                    richTextBox1.Text =astring+ 梦中的婚礼String;
                    break;
                case "雪绒花":
                    richTextBox1.Text =astring+ 雪绒花String;
                    break;
                case "青花瓷":
                    richTextBox1.Text =astring+ 青花瓷String;
                    break;
                case "北京欢迎你":
                    richTextBox1.Text =astring+ 北京欢迎你String;
                    break;
                default:
                    打开指导曲目ToolStripMenuItem_Click(sender, e);
                    break;
            }
            richTextBox1.Text += "\n你所演奏的曲谱为：\n";

            实体键盘button.Focus();
        }

        //用于延音，控制当不延音时的声音长度为1秒
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!延音checkBox.Checked)
            {
                for (int i = 0; i < 88;i++ )
                     bSound[i].Stop();
            }
        }

        //实体键盘的使用
        private void 实体键盘button_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                    //最顶排的控制键
                case Keys.F1:
                    延音_Click(sender, e);
                    break;
                case Keys.F5:
                    F5升八度button_Click(sender, e);
                    break;
                case Keys.F6:
                    F6降八度button_Click(sender, e);
                    break;

                //第一排
                case Keys.D1://1&！以下类似
                    key1_Click(sender, e);
                    break;
                case Keys.D2:
                    key2_Click(sender, e);
                    break;
                case Keys.D3:
                    key3_Click(sender, e);
                    break;
                case Keys.D4:
                    key4_Click(sender, e);
                    break;
                case Keys.D5:
                    key5_Click(sender, e);
                    break;
                case Keys.D6:
                    key6_Click(sender, e);
                    break;
                case Keys.D7:
                    key7_Click(sender, e);
                    break;
                case Keys.D8:
                    key8_Click(sender, e);
                    break;
                case Keys.D9:
                    key9_Click(sender, e);
                    break;
                case Keys.D0:
                    key0_Click(sender, e);
                    break;
                case Keys.OemMinus://减号
                    key减号_Click(sender, e);
                    break;
                case Keys.Oemplus://加号&等号
                    key等号_Click(sender, e);
                    break;

                //第二排
                case Keys.Q:
                    keyQ_Click(sender, e);
                    break;
                case Keys.W:
                    keyW_Click(sender, e);
                    break;
                case Keys.E:
                    keyE_Click(sender, e);
                    break;
                case Keys.R:
                    keyR_Click(sender, e);
                    break;
                case Keys.T:
                    keyT_Click(sender, e);
                    break;
                case Keys.Y:
                    keyY_Click(sender, e);
                    break;
                case Keys.U:
                    keyU_Click(sender, e);
                    break;
                case Keys.I:
                    keyI_Click(sender, e);
                    break;
                case Keys.O:
                    keyO_Click(sender, e);
                    break;
                case Keys.P:
                    keyP_Click(sender, e);
                    break;
                case Keys.OemOpenBrackets://{[
                    key左中括号_Click(sender, e);
                    break;
                case Keys.OemCloseBrackets://}]
                    key右中括号_Click(sender, e);
                    break;
                case Keys.OemBackslash://|\
                    keyBackslash_Click(sender, e);
                    break;

                //第三行
                case Keys.A:
                    keyA_Click(sender, e);
                    break;
                case Keys.S:
                    keyS_Click(sender, e);
                    break;
                case Keys.D:
                    keyD_Click(sender, e);
                    break;
                case Keys.F:
                    keyF_Click(sender, e);
                    break;
                case Keys.G:
                    keyG_Click(sender, e);
                    break;
                case Keys.H:
                    keyH_Click(sender, e);
                    break;
                case Keys.J:
                    keyJ_Click(sender, e);
                    break;
                case Keys.K:
                    keyK_Click(sender, e);
                    break;
                case Keys.L:
                    keyL_Click(sender, e);
                    break;
                case Keys.OemSemicolon://:;
                    key分号_Click(sender, e);
                    break;
                case Keys.OemQuotes://"'
                    key引号_Click(sender, e);
                    break;

                //第四行
                case Keys.Z:
                    keyZ_Click(sender, e);
                    break;
                case Keys.X:
                    keyX_Click(sender, e);
                    break;
                case Keys.C:
                    keyC_Click(sender, e);
                    break;
                case Keys.V:
                    keyV_Click(sender, e);
                    break;
                case Keys.B:
                    keyB_Click(sender, e);
                    break;
                case Keys.N:
                    keyN_Click(sender, e);
                    break;
                case Keys.M:
                    keyM_Click(sender, e);
                    break;
                case Keys.Oemcomma://<,
                         key逗号_Click(sender, e);
                         break;
                case Keys.OemPeriod://>.
                    key句号_Click(sender, e);
                    break;
                case Keys.OemQuestion://?/
                    key问号_Click(sender, e);
                    break;
            }
        }

        //在黑白键盘中88个音的载入与播放
        
        private void b1_Click(object sender, EventArgs e)
        {
            colorChange();
            b1.BackColor = Color.Tan;
            //q = b1.Location.X;
            pictureMove6();
            bSound[0].Play();
            richTextBox1.Text += "6....  ";
            实体键盘button.Focus();
        }

        private void b2_Click(object sender, EventArgs e)
        {
            colorChange();
            b2.BackColor = Color.Tan;
            bSound[1].Play();
            richTextBox1.Text += "#6....  ";
            实体键盘button.Focus();
        }

        private void b3_Click(object sender, EventArgs e)
        {
           // q = b3.Location.X;
            pictureMove7();
            colorChange();
            b3.BackColor = Color.Tan;
            bSound[2].Play();
            richTextBox1.Text += "7.... ";
            实体键盘button.Focus();
        }

        private void b4_Click(object sender, EventArgs e)
        {
            //q = b4.Location.X;
            pictureMove1();
            colorChange();
            b4.BackColor = Color.Tan;
            bSound[3].Play();
            richTextBox1.Text += "1...  ";
            实体键盘button.Focus();
        }

        private void b5_Click(object sender, EventArgs e)
        {
            colorChange();
            b5.BackColor = Color.Tan;
            bSound[4].Play();
            richTextBox1.Text += "#1...  ";
            实体键盘button.Focus();
        }

        private void b6_Click(object sender, EventArgs e)
        {
            //q = b6.Location.X;
            pictureMove2();
            colorChange();
            b6.BackColor = Color.Tan;
            bSound[5].Play();
            richTextBox1.Text += "2...  ";
            实体键盘button.Focus();
        }

        private void b7_Click(object sender, EventArgs e)
        {
            colorChange();
            b7.BackColor = Color.Tan;
            bSound[6].Play();
            richTextBox1.Text += "#2...  ";
            实体键盘button.Focus();
        }

        private void b8_Click(object sender, EventArgs e)
        {
            //q = b8.Location.X;
            pictureMove3();
            colorChange();
            b8.BackColor = Color.Tan;
            bSound[7].Play();
            richTextBox1.Text += "3...  ";
            实体键盘button.Focus();
        }

        private void b9_Click(object sender, EventArgs e)
        {
       //     q = b9.Location.X;
            pictureMove4();
            colorChange();
            b9.BackColor = Color.Tan;
            bSound[8].Play();
            richTextBox1.Text += "4...  ";
            实体键盘button.Focus();
        }

        private void b10_Click(object sender, EventArgs e)
        {
            colorChange();
            b10.BackColor = Color.Tan;
            bSound[9].Play();
            richTextBox1.Text += "#4...  ";
            实体键盘button.Focus();
        }

        private void b11_Click(object sender, EventArgs e)
        {
           // q = b11.Location.X;
            pictureMove5();
            colorChange();
            b11.BackColor = Color.Tan;
            bSound[10].Play();
            richTextBox1.Text += "5...  ";
            实体键盘button.Focus();
        }

        private void b12_Click(object sender, EventArgs e)
        {
            colorChange();
            b12.BackColor = Color.Tan;
            bSound[11].Play();
            richTextBox1.Text += "#5...  ";
            实体键盘button.Focus();
        }

        private void b13_Click(object sender, EventArgs e)
        {
         //   q = b13.Location.X;
            pictureMove6();
            colorChange();
            b13.BackColor = Color.Tan;
            bSound[12].Play();
            richTextBox1.Text += "6...  ";
            实体键盘button.Focus();
        }

        private void b14_Click(object sender, EventArgs e)
        {
            colorChange();
            b14.BackColor = Color.Tan;
            bSound[13].Play();
            richTextBox1.Text += "#6...  ";
            实体键盘button.Focus();
        }

        private void b15_Click(object sender, EventArgs e)
        {
           // q = b15.Location.X;
            pictureMove7();
            colorChange();
            b15.BackColor = Color.Tan;
            bSound[14].Play();
            richTextBox1.Text += "7...  ";
            实体键盘button.Focus();
        }

        private void b16_Click(object sender, EventArgs e)
        {
           // q = b16.Location.X;
            pictureMove1();
            colorChange();
            b16.BackColor = Color.Tan;
            bSound[15].Play();
            richTextBox1.Text += "1..  ";
            实体键盘button.Focus();
        }

        private void b17_Click(object sender, EventArgs e)
        {
            colorChange();
            b17.BackColor = Color.Tan;
            bSound[16].Play();
            richTextBox1.Text += "#1..  ";
            实体键盘button.Focus();
        }

        private void b18_Click(object sender, EventArgs e)
        {
            //q = b18.Location.X;
            pictureMove2();
            colorChange();
            b18.BackColor = Color.Tan;
            bSound[17].Play();
            richTextBox1.Text += "2..  ";
            实体键盘button.Focus();
        }

        private void b19_Click(object sender, EventArgs e)
        {
            colorChange();
            b19.BackColor = Color.Tan;
            bSound[18].Play();
            richTextBox1.Text += "#2..  ";
            实体键盘button.Focus();
        }

        private void b20_Click(object sender, EventArgs e)
        {
            //q = b20.Location.X;
            pictureMove3();
            colorChange();
            b20.BackColor = Color.Tan;
            bSound[19].Play();
            richTextBox1.Text += "3..  ";
            实体键盘button.Focus();
        }

        private void b21_Click(object sender, EventArgs e)
        {
           // q = b21.Location.X;
            pictureMove4();
            colorChange();
            b21.BackColor = Color.Tan;
            bSound[20].Play();
            richTextBox1.Text += "4..  ";
            实体键盘button.Focus();
        }

        private void b22_Click(object sender, EventArgs e)
        {
            colorChange();
            b22.BackColor = Color.Tan;
            bSound[21].Play();
            richTextBox1.Text += "#4..  ";
            实体键盘button.Focus();
        }

        private void b23_Click(object sender, EventArgs e)
        {
           // q = b23.Location.X;
            pictureMove5();
            colorChange();
            b23.BackColor = Color.Tan;
            bSound[22].Play();
            richTextBox1.Text += "5..  ";
            实体键盘button.Focus();
        }

        private void b24_Click(object sender, EventArgs e)
        {
            colorChange();
            b24.BackColor = Color.Tan;
            bSound[23].Play();
            richTextBox1.Text += "#5..  ";
            实体键盘button.Focus();
        }

        private void b25_Click(object sender, EventArgs e)
        {
            //q = b25.Location.X;
            pictureMove6();
            colorChange();
            b25.BackColor = Color.Tan;
            bSound[24].Play();
            richTextBox1.Text += "6..  ";
            实体键盘button.Focus();
        }

        private void b26_Click(object sender, EventArgs e)
        {
            colorChange();
            b26.BackColor = Color.Tan;
            bSound[25].Play();
            richTextBox1.Text += "#6..  ";
            实体键盘button.Focus();
        }

        private void b27_Click(object sender, EventArgs e)
        {
           // q = b27.Location.X;
            pictureMove7();
            colorChange();
            b27.BackColor = Color.Tan;
            bSound[26].Play();
            richTextBox1.Text += "7..  ";
            实体键盘button.Focus();
        }

        private void b28_Click(object sender, EventArgs e)
        {
            //q = b28.Location.X;
            pictureMove1();
            colorChange();
            b28.BackColor = Color.Tan;
            bSound[27].Play();
            richTextBox1.Text += "1.  ";
            实体键盘button.Focus();
        }

        private void b29_Click(object sender, EventArgs e)
        {
            colorChange();
            b29.BackColor = Color.Tan;
            bSound[28].Play();
            richTextBox1.Text += "#1.  ";
            实体键盘button.Focus();
        }

        private void b30_Click(object sender, EventArgs e)
        {
           // q = b30.Location.X;
            pictureMove2();
            colorChange();
            b30.BackColor = Color.Tan;
            bSound[29].Play();
            richTextBox1.Text += "2.  ";
            实体键盘button.Focus();
        }

        private void b31_Click(object sender, EventArgs e)
        {
            colorChange();
            b31.BackColor = Color.Tan;
            bSound[30].Play();
            richTextBox1.Text += "#2.  ";
            实体键盘button.Focus();
        }

        private void b32_Click(object sender, EventArgs e)
        {
           // q = b32.Location.X;
            pictureMove3();
            colorChange();
            b32.BackColor = Color.Tan;
            bSound[31].Play();
            richTextBox1.Text += "3.  ";
            实体键盘button.Focus();
        }

        private void b33_Click(object sender, EventArgs e)
        {
           // q = b33.Location.X;
            pictureMove4();
            colorChange();
            b33.BackColor = Color.Tan;
            bSound[32].Play();
            richTextBox1.Text += "4.  ";
            实体键盘button.Focus();
        }

        private void b34_Click(object sender, EventArgs e)
        {
            colorChange();
            b34.BackColor = Color.Tan;
            bSound[33].Play();
            richTextBox1.Text += "#4.  ";
            实体键盘button.Focus();
        }

        private void b35_Click(object sender, EventArgs e)
        {
           // q = b35.Location.X;
            pictureMove5();
            colorChange();
            b35.BackColor = Color.Tan;
            bSound[34].Play();
            richTextBox1.Text += "5.  ";
            实体键盘button.Focus();
        }

        private void b36_Click(object sender, EventArgs e)
        {
            colorChange();
            b36.BackColor = Color.Tan;
            bSound[35].Play();
            richTextBox1.Text += "#5.  ";
            实体键盘button.Focus();
        }

        private void b37_Click(object sender, EventArgs e)
        {
           // q = b37.Location.X;
            pictureMove6();
            colorChange();
            b37.BackColor = Color.Tan;
            bSound[36].Play();
            richTextBox1.Text += "6.  ";
            实体键盘button.Focus();
        }

        private void b38_Click(object sender, EventArgs e)
        {
            colorChange();
            b38.BackColor = Color.Tan;
            bSound[37].Play();
            richTextBox1.Text += "#6.  ";
            实体键盘button.Focus();
        }

        private void b39_Click(object sender, EventArgs e)
        {
           // q = b39.Location.X;
            pictureMove7();
            colorChange();
            b39.BackColor = Color.Tan;
            bSound[38].Play();
            richTextBox1.Text += "7.  ";
            实体键盘button.Focus();
        }

        private void b40_Click(object sender, EventArgs e)
        {
           // q = b40.Location.X;
            pictureMove1();
            colorChange();
            b40.BackColor = Color.Tan;
            bSound[39].Play();
            richTextBox1.Text += "1  ";
            实体键盘button.Focus();
        }

        private void b41_Click(object sender, EventArgs e)
        {
            colorChange();
            b41.BackColor = Color.Tan;
            bSound[40].Play();
            richTextBox1.Text += "#1  ";
            实体键盘button.Focus();
        }

        private void b42_Click(object sender, EventArgs e)
        {
          //  q = b42.Location.X;
            pictureMove2();
            colorChange();
            b42.BackColor = Color.Tan;
            bSound[41].Play();
            richTextBox1.Text += "2  ";
            实体键盘button.Focus();
        }

        private void b43_Click(object sender, EventArgs e)
        {
            colorChange();
            b43.BackColor = Color.Tan;
            bSound[42].Play();
            richTextBox1.Text += "#2  ";
            实体键盘button.Focus();
        }

        private void b44_Click(object sender, EventArgs e)
        {
           // q = b44.Location.X;
            pictureMove3();
            colorChange();
            b44.BackColor = Color.Tan;
            bSound[43].Play();
            richTextBox1.Text += "3  ";
            实体键盘button.Focus();
        }

        private void b45_Click(object sender, EventArgs e)
        {
          //  q = b45.Location.X;
            pictureMove4();
            colorChange();
            b45.BackColor = Color.Tan;
            bSound[44].Play();
            richTextBox1.Text += "4  ";
            实体键盘button.Focus();
        }

        private void b46_Click(object sender, EventArgs e)
        {
            colorChange();
            b46.BackColor = Color.Tan;
            bSound[45].Play();
            richTextBox1.Text += "#4  ";
            实体键盘button.Focus();
        }

        private void b47_Click(object sender, EventArgs e)
        {
           // q = b47.Location.X;
            pictureMove5();
            colorChange();
            b47.BackColor = Color.Tan;
            bSound[46].Play();
            richTextBox1.Text += "5  ";
            实体键盘button.Focus();
        }

        private void b48_Click(object sender, EventArgs e)
        {
            colorChange();
            b48.BackColor = Color.Tan;
            bSound[47].Play();
            richTextBox1.Text += "#5  ";
            实体键盘button.Focus();
        }

        private void b49_Click(object sender, EventArgs e)
        {
           // q = b49.Location.X;
            pictureMove6();
            colorChange();
            b49.BackColor = Color.Tan;
            bSound[48].Play();
            richTextBox1.Text += "6  ";
            实体键盘button.Focus();
        }

        private void b50_Click(object sender, EventArgs e)
        {
            colorChange();
            b50.BackColor = Color.Tan;
            bSound[49].Play();
            richTextBox1.Text += "#6  ";
            实体键盘button.Focus();
        }

        private void b51_Click(object sender, EventArgs e)
        {
        //    q = b51.Location.X;
            pictureMove7();
            colorChange();
            b51.BackColor = Color.Tan;
            bSound[50].Play();
            richTextBox1.Text += "7  ";
            实体键盘button.Focus();
        }

        private void b52_Click(object sender, EventArgs e)
        {
          //  q = b52.Location.X;
            pictureMove1();
            colorChange();
            b52.BackColor = Color.Tan;
            bSound[51].Play();
            richTextBox1.Text += ".1  ";
            实体键盘button.Focus();
        }

        private void b53_Click(object sender, EventArgs e)
        {
            colorChange();
            b53.BackColor = Color.Tan;
            bSound[52].Play();
            richTextBox1.Text += "#.1  ";
            实体键盘button.Focus();
        }

        private void b54_Click(object sender, EventArgs e)
        {
            //q = b54.Location.X;
            pictureMove2();
            colorChange();
            b54.BackColor = Color.Tan;
            bSound[53].Play();
            richTextBox1.Text += ".2  ";
            实体键盘button.Focus();
        }

        private void b55_Click(object sender, EventArgs e)
        {
            colorChange();
            b55.BackColor = Color.Tan;
            bSound[54].Play();
            richTextBox1.Text += "#.2  ";
            实体键盘button.Focus();
        }

        private void b56_Click(object sender, EventArgs e)
        {
           // q = b56.Location.X;
            pictureMove3();
            colorChange();
            b56.BackColor = Color.Tan;
            bSound[55].Play();
            richTextBox1.Text += ".3  ";
            实体键盘button.Focus();
        }

        private void b57_Click(object sender, EventArgs e)
        {
           // q = b57.Location.X;
            pictureMove4();
            colorChange();
            b57.BackColor = Color.Tan;
            bSound[56].Play();
            richTextBox1.Text += ".4  ";
            实体键盘button.Focus();
        }

        private void b58_Click(object sender, EventArgs e)
        {
           // colorChange();
            b58.BackColor = Color.Tan;
            bSound[57].Play();
            richTextBox1.Text += "#.4  ";
            实体键盘button.Focus();
        }

        private void b59_Click(object sender, EventArgs e)
        {
           // q = b59.Location.X;
            pictureMove5();
            colorChange();
            b59.BackColor = Color.Tan;
            bSound[58].Play();
            richTextBox1.Text += ".5  ";
            实体键盘button.Focus();
        }

        private void b60_Click(object sender, EventArgs e)
        {
            colorChange();
            b60.BackColor = Color.Tan;
            bSound[59].Play();
            richTextBox1.Text += "#.5  ";
            实体键盘button.Focus();
        }

        private void b61_Click(object sender, EventArgs e)
        {
           // q = b61.Location.X;
            pictureMove6();
            colorChange();
            b61.BackColor = Color.Tan;
            bSound[60].Play();
            richTextBox1.Text += ".6  ";
            实体键盘button.Focus();
        }

        private void b62_Click(object sender, EventArgs e)
        {
            colorChange();
            b62.BackColor = Color.Tan;
            bSound[61].Play();
            richTextBox1.Text += "#.6  ";
            实体键盘button.Focus();
        }

        private void b63_Click(object sender, EventArgs e)
        {
           // q = b63.Location.X;
            pictureMove7();
            colorChange();
            b63.BackColor = Color.Tan;
            bSound[62].Play();
            richTextBox1.Text += ".7  ";
            实体键盘button.Focus();
        }

        private void b64_Click(object sender, EventArgs e)
        {
           // q = b64.Location.X;
            pictureMove1();
            colorChange();
            b64.BackColor = Color.Tan;
            bSound[63].Play();
            richTextBox1.Text += "..1  ";
            实体键盘button.Focus();
        }

        private void b65_Click(object sender, EventArgs e)
        {
            colorChange();
            b65.BackColor = Color.Tan;
            bSound[64].Play();
            richTextBox1.Text += "#..1  ";
            实体键盘button.Focus();
        }

        private void b66_Click(object sender, EventArgs e)
        {
           // q = b66.Location.X;
            pictureMove2();
            colorChange();
            b66.BackColor = Color.Tan;
            bSound[65].Play();
            richTextBox1.Text += "..2  ";
            实体键盘button.Focus();
        }

        private void b67_Click(object sender, EventArgs e)
        {
            colorChange();
            b67.BackColor = Color.Tan;
            bSound[66].Play();
            richTextBox1.Text += "#..2  ";
            实体键盘button.Focus();
        }

        private void b68_Click(object sender, EventArgs e)
        {
           // q = b68.Location.X;
            pictureMove3();
            colorChange();
            b68.BackColor = Color.Tan;
            bSound[67].Play();
            richTextBox1.Text += "..3  ";
            实体键盘button.Focus();
        }

        private void b69_Click(object sender, EventArgs e)
        {
          //  q = b69.Location.X;
            pictureMove4();
            colorChange();
            b69.BackColor = Color.Tan;
            bSound[68].Play();
            richTextBox1.Text += "..4  ";
            实体键盘button.Focus();
        }

        private void b70_Click(object sender, EventArgs e)
        {
            colorChange();
            b70.BackColor = Color.Tan;
            bSound[69].Play();
            richTextBox1.Text += "#..4  ";
            实体键盘button.Focus();
        }

        private void b71_Click(object sender, EventArgs e)
        {
            //q = b71.Location.X;
            pictureMove5();
            colorChange();
            b71.BackColor = Color.Tan;
            bSound[70].Play();
            richTextBox1.Text += "..5  ";
            实体键盘button.Focus();
        }

        private void b72_Click(object sender, EventArgs e)
        {
            colorChange();
            b72.BackColor = Color.Tan;
            bSound[71].Play();
            richTextBox1.Text += "#..5  ";
            实体键盘button.Focus();
        }

        private void b73_Click(object sender, EventArgs e)
        {
           // q = b73.Location.X;
            pictureMove6();
            colorChange();
            b73.BackColor = Color.Tan;
            bSound[72].Play();
            richTextBox1.Text += "..6  ";
            实体键盘button.Focus();
        }

        private void b74_Click(object sender, EventArgs e)
        {
            colorChange();
            b74.BackColor = Color.Tan;
            bSound[73].Play();
            richTextBox1.Text += "#..6  ";
            实体键盘button.Focus();
        }

        private void b75_Click(object sender, EventArgs e)
        {
           // q = b75.Location.X;
            pictureMove7();
            colorChange();
            b75.BackColor = Color.Tan;
            bSound[74].Play();
            richTextBox1.Text += "..7  ";
            实体键盘button.Focus();
        }

        private void b76_Click(object sender, EventArgs e)
        {
           // q = b76.Location.X;
            pictureMove1();
            colorChange();
            b76.BackColor = Color.Tan;
            bSound[75].Play();
            richTextBox1.Text += "...1  ";
            实体键盘button.Focus();
        }

        private void b77_Click(object sender, EventArgs e)
        {
            colorChange();
            b77.BackColor = Color.Tan;
            bSound[76].Play();
            richTextBox1.Text += "#...1  ";
            实体键盘button.Focus();
        }

        private void b78_Click(object sender, EventArgs e)
        {
           // q = b78.Location.X;
            pictureMove2();
            colorChange();
            b78.BackColor = Color.Tan;
            bSound[77].Play();
            richTextBox1.Text += "...2  ";
            实体键盘button.Focus();
        }

        private void b79_Click(object sender, EventArgs e)
        {
            colorChange();
            b79.BackColor = Color.Tan;
            bSound[78].Play();
            richTextBox1.Text += "#...2  ";
            实体键盘button.Focus();
        }

        private void b80_Click(object sender, EventArgs e)
        {
           // q = b80.Location.X;
            pictureMove3();
            colorChange();
            b80.BackColor = Color.Tan;
            bSound[79].Play();
            richTextBox1.Text += "...3  ";
            实体键盘button.Focus();
        }

        private void b81_Click(object sender, EventArgs e)
        {
           // q = b81.Location.X;
            pictureMove4();
            colorChange();
            b81.BackColor = Color.Tan;
            bSound[80].Play();
            richTextBox1.Text += "...4  ";
            实体键盘button.Focus();
        }

        private void b82_Click(object sender, EventArgs e)
        {
            colorChange();
            b82.BackColor = Color.Tan;
            bSound[81].Play();
            richTextBox1.Text += "#...4  ";
            实体键盘button.Focus();
        }

        private void b83_Click(object sender, EventArgs e)
        {
           // q = b83.Location.X;
            pictureMove5();
            colorChange();
            b83.BackColor = Color.Tan;
            bSound[82].Play();
            richTextBox1.Text += "...5  ";
            实体键盘button.Focus();
        }

        private void b84_Click(object sender, EventArgs e)
        {
            colorChange();
            b84.BackColor = Color.Tan;
            bSound[83].Play();
            richTextBox1.Text += "#...5  ";
            实体键盘button.Focus();
        }

        private void b85_Click(object sender, EventArgs e)
        {
           // q = b85.Location.X;
            pictureMove6();
            colorChange();
            b85.BackColor = Color.Tan;
            bSound[84].Play();
            richTextBox1.Text += "...6  ";
            实体键盘button.Focus();
        }

        private void b86_Click(object sender, EventArgs e)
        {
            colorChange();
            b86.BackColor = Color.Tan;
            bSound[85].Play();
            richTextBox1.Text += "#...6  ";
            实体键盘button.Focus();
        }

        private void b87_Click(object sender, EventArgs e)
        {
           // q = b87.Location.X;
            pictureMove7();
            colorChange();
            b87.BackColor = Color.Tan;
            bSound[86].Play();
            richTextBox1.Text += "...7  ";
            实体键盘button.Focus();
        }

        private void b88_Click(object sender, EventArgs e)
        {
           // q = b88.Location.X;
            pictureMove1();
            colorChange();
            b88.BackColor = Color.Tan;
            bSound[87].Play();
            richTextBox1.Text += "....1  ";
            实体键盘button.Focus();
        }

        //屏幕上显示的电脑键盘
        string keyString;
        private void keyClick(object sender, EventArgs e)
        {
            switch (keyString)
            {
                    //第一行
                case "key1":
                    if (升降8度NumericUpDown.Value == 1)
                    {
                        b64_Click(sender, e);
                    }
                    else if (升降8度NumericUpDown.Value == 0)
                    {
                        b52_Click(sender, e);
                    }
                    else if (升降8度NumericUpDown.Value == -1)
                    {
                        b40_Click(sender, e);
                    }
                    break;
                case "key2":
                    if (升降8度NumericUpDown.Value == 1)
                    {
                        b66_Click(sender, e);
                    }
                    else if (升降8度NumericUpDown.Value == 0)
                    {
                        b54_Click(sender, e);
                    }
                    else if (升降8度NumericUpDown.Value == -1)
                    {
                        b42_Click(sender, e);
                    }
                    break;
                case "key3":
                    if (升降8度NumericUpDown.Value == 1)
                    {
                        b68_Click(sender, e);

                    }
                    else if (升降8度NumericUpDown.Value == 0)
                    {
                        b56_Click(sender, e);
                    }
                    else if (升降8度NumericUpDown.Value == -1)
                    {
                        b44_Click(sender, e);
                    }
                    break;
                case "key4":
                    if (升降8度NumericUpDown.Value == 1)
                    {
                        b69_Click(sender, e);

                    }
                    else if (升降8度NumericUpDown.Value == 0)
                    {
                        b57_Click(sender, e);
                    }
                    else if (升降8度NumericUpDown.Value == -1)
                    {
                        b45_Click(sender, e);
                    }
                    break;
                case "key5":
                    if (升降8度NumericUpDown.Value == 1)
                    {
                        b71_Click(sender, e);

                    }
                    else if (升降8度NumericUpDown.Value == 0)
                    {
                        b59_Click(sender, e);
                    }
                    else if (升降8度NumericUpDown.Value == -1)
                    {
                        b47_Click(sender, e);
                    }
                    break;
                case "key6":
                    if (升降8度NumericUpDown.Value == 1)
                    {
                        b73_Click(sender, e);

                    }
                    else if (升降8度NumericUpDown.Value == 0)
                    {
                        b61_Click(sender, e);
                    }
                    else if (升降8度NumericUpDown.Value == -1)
                    {
                        b49_Click(sender, e);
                    }
                    break;
                case "key7":
                    if (升降8度NumericUpDown.Value == 1)
                    {
                        b75_Click(sender, e);

                    }
                    else if (升降8度NumericUpDown.Value == 0)
                    {
                        b63_Click(sender, e);
                    }
                    else if (升降8度NumericUpDown.Value == -1)
                    {
                        b51_Click(sender, e);
                    }
                    break;
                case "key8":
                    if (升降8度NumericUpDown.Value == 1)
                    {
                        b76_Click(sender, e);

                    }
                    else if (升降8度NumericUpDown.Value == 0)
                    {
                        b64_Click(sender, e);
                    }
                    else if (升降8度NumericUpDown.Value == -1)
                    {
                        b52_Click(sender, e);
                    }
                    break;
                case "key9":
                    if (升降8度NumericUpDown.Value == 1)
                    {
                        b78_Click(sender, e);

                    }
                    else if (升降8度NumericUpDown.Value == 0)
                    {
                        b66_Click(sender, e);
                    }
                    else if (升降8度NumericUpDown.Value == -1)
                    {
                        b54_Click(sender, e);
                    }
                    break;
                case "key0":
                    if (升降8度NumericUpDown.Value == 1)
                    {
                        b80_Click(sender, e);

                    }
                    else if (升降8度NumericUpDown.Value == 0)
                    {
                        b68_Click(sender, e);
                    }
                    else if (升降8度NumericUpDown.Value == -1)
                    {
                        b56_Click(sender, e);
                    }
                    break;
                case "key减号":
                    if (升降8度NumericUpDown.Value == 1)
                    {
                        b81_Click(sender, e);

                    }
                    else if (升降8度NumericUpDown.Value == 0)
                    {
                        b69_Click(sender, e);
                    }
                    else if (升降8度NumericUpDown.Value == -1)
                    {
                        b57_Click(sender, e);
                    }
                    break;
                case "key等号":
                    if (升降8度NumericUpDown.Value == 1)
                    {
                        b83_Click(sender, e);

                    }
                    else if (升降8度NumericUpDown.Value == 0)
                    {
                        b71_Click(sender, e);
                    }
                    else if (升降8度NumericUpDown.Value == -1)
                    {
                        b59_Click(sender, e);
                    }
                    break;

                    //第二行
                case "keyQ":
                    if (升降8度NumericUpDown.Value == 1)
                    {
                        b52_Click(sender, e);

                    }
                    else if (升降8度NumericUpDown.Value == 0)
                    {
                        b40_Click(sender, e);
                    }
                    else if (升降8度NumericUpDown.Value == -1)
                    {
                        b28_Click(sender, e);
                    }
                    break;
                case "keyW":
                    if (升降8度NumericUpDown.Value == 1)
                    {
                        b54_Click(sender, e);

                    }
                    else if (升降8度NumericUpDown.Value == 0)
                    {
                        b42_Click(sender, e);
                    }
                    else if (升降8度NumericUpDown.Value == -1)
                    {
                        b30_Click(sender, e);
                    }
                    break;
                case "keyE":
                    if (升降8度NumericUpDown.Value == 1)
                    {
                        b56_Click(sender, e);

                    }
                    else if (升降8度NumericUpDown.Value == 0)
                    {
                        b44_Click(sender, e);
                    }
                    else if (升降8度NumericUpDown.Value == -1)
                    {
                        b32_Click(sender, e);
                    }
                    break;
                case "keyR":
                    if (升降8度NumericUpDown.Value == 1)
                    {
                        b57_Click(sender, e);

                    }
                    else if (升降8度NumericUpDown.Value == 0)
                    {
                        b45_Click(sender, e);
                    }
                    else if (升降8度NumericUpDown.Value == -1)
                    {
                        b33_Click(sender, e);
                    }
                    break;
                case "keyT":
                    if (升降8度NumericUpDown.Value == 1)
                    {
                        b59_Click(sender, e);

                    }
                    else if (升降8度NumericUpDown.Value == 0)
                    {
                        b47_Click(sender, e);
                    }
                    else if (升降8度NumericUpDown.Value == -1)
                    {
                        b35_Click(sender, e);
                    }
                    break;
                case "keyY":
                    if (升降8度NumericUpDown.Value == 1)
                    {
                        b61_Click(sender, e);

                    }
                    else if (升降8度NumericUpDown.Value == 0)
                    {
                        b49_Click(sender, e);
                    }
                    else if (升降8度NumericUpDown.Value == -1)
                    {
                        b37_Click(sender, e);
                    }
                    break;
                case "keyU":
                    if (升降8度NumericUpDown.Value == 1)
                    {
                        b63_Click(sender, e);

                    }
                    else if (升降8度NumericUpDown.Value == 0)
                    {
                        b51_Click(sender, e);
                    }
                    else if (升降8度NumericUpDown.Value == -1)
                    {
                        b39_Click(sender, e);
                    }
                    break;
                case "keyI":
                    if (升降8度NumericUpDown.Value == 1)
                    {
                        b64_Click(sender, e);

                    }
                    else if (升降8度NumericUpDown.Value == 0)
                    {
                        b52_Click(sender, e);
                    }
                    else if (升降8度NumericUpDown.Value == -1)
                    {
                        b40_Click(sender, e);
                    }
                    break;
                case "keyO":
                    if (升降8度NumericUpDown.Value == 1)
                    {
                        b66_Click(sender, e);

                    }
                    else if (升降8度NumericUpDown.Value == 0)
                    {
                        b54_Click(sender, e);
                    }
                    else if (升降8度NumericUpDown.Value == -1)
                    {
                        b42_Click(sender, e);
                    }
                    break;
                case "keyP":
                    if (升降8度NumericUpDown.Value == 1)
                    {
                        b68_Click(sender, e);

                    }
                    else if (升降8度NumericUpDown.Value == 0)
                    {
                        b56_Click(sender, e);
                    }
                    else if (升降8度NumericUpDown.Value == -1)
                    {
                        b44_Click(sender, e);
                    }
                    break;
                case "key左中括号":
                    if (升降8度NumericUpDown.Value == 1)
                    {
                        b69_Click(sender, e);

                    }
                    else if (升降8度NumericUpDown.Value == 0)
                    {
                        b57_Click(sender, e);
                    }
                    else if (升降8度NumericUpDown.Value == -1)
                    {
                        b45_Click(sender, e);
                    }
                    break;
                case "key右中括号":
                    if (升降8度NumericUpDown.Value == 1)
                    {
                        b71_Click(sender, e);

                    }
                    else if (升降8度NumericUpDown.Value == 0)
                    {
                        b59_Click(sender, e);
                    }
                    else if (升降8度NumericUpDown.Value == -1)
                    {
                        b47_Click(sender, e);
                    }
                    break;
                case "keyBackslash":
                    if (升降8度NumericUpDown.Value == 1)
                    {
                        b73_Click(sender, e);

                    }
                    else if (升降8度NumericUpDown.Value == 0)
                    {
                        b61_Click(sender, e);
                    }
                    else if (升降8度NumericUpDown.Value == -1)
                    {
                        b49_Click(sender, e);
                    }
                    break;

                    //第三行
                case "keyA":
                    if (升降8度NumericUpDown.Value == 1)
                    {
                        b40_Click(sender, e);

                    }
                    else if (升降8度NumericUpDown.Value == 0)
                    {
                        b28_Click(sender, e);
                    }
                    else if (升降8度NumericUpDown.Value == -1)
                    {
                        b16_Click(sender, e);
                    }
                    break;
                case "keyS":
                    if (升降8度NumericUpDown.Value == 1)
                    {
                        b42_Click(sender, e);

                    }
                    else if (升降8度NumericUpDown.Value == 0)
                    {
                        b30_Click(sender, e);
                    }
                    else if (升降8度NumericUpDown.Value == -1)
                    {
                        b18_Click(sender, e);
                    }
                    break;
                case "keyD":
                    if (升降8度NumericUpDown.Value == 1)
                    {
                        b44_Click(sender, e);

                    }
                    else if (升降8度NumericUpDown.Value == 0)
                    {
                        b32_Click(sender, e);
                    }
                    else if (升降8度NumericUpDown.Value == -1)
                    {
                        b20_Click(sender, e);
                    }
                    break;
                case "keyF":
                    if (升降8度NumericUpDown.Value == 1)
                    {
                        b45_Click(sender, e);

                    }
                    else if (升降8度NumericUpDown.Value == 0)
                    {
                        b33_Click(sender, e);
                    }
                    else if (升降8度NumericUpDown.Value == -1)
                    {
                        b21_Click(sender, e);
                    }
                    break;
                case "keyG":
                    if (升降8度NumericUpDown.Value == 1)
                    {
                        b47_Click(sender, e);

                    }
                    else if (升降8度NumericUpDown.Value == 0)
                    {
                        b35_Click(sender, e);
                    }
                    else if (升降8度NumericUpDown.Value == -1)
                    {
                        b23_Click(sender, e);
                    }
                    break;
                case "keyH":
                    if (升降8度NumericUpDown.Value == 1)
                    {
                        b49_Click(sender, e);

                    }
                    else if (升降8度NumericUpDown.Value == 0)
                    {
                        b37_Click(sender, e);
                    }
                    else if (升降8度NumericUpDown.Value == -1)
                    {
                        b25_Click(sender, e);
                    }
                    break;
                case "keyJ":
                    if (升降8度NumericUpDown.Value == 1)
                    {
                        b51_Click(sender, e);

                    }
                    else if (升降8度NumericUpDown.Value == 0)
                    {
                        b39_Click(sender, e);
                    }
                    else if (升降8度NumericUpDown.Value == -1)
                    {
                        b27_Click(sender, e);
                    }
                    break;
                case "keyK":
                    if (升降8度NumericUpDown.Value == 1)
                    {
                        b52_Click(sender, e);

                    }
                    else if (升降8度NumericUpDown.Value == 0)
                    {
                        b40_Click(sender, e);
                    }
                    else if (升降8度NumericUpDown.Value == -1)
                    {
                        b28_Click(sender, e);
                    }
                    break;
                case "keyL":
                    if (升降8度NumericUpDown.Value == 1)
                    {
                        b54_Click(sender, e);

                    }
                    else if (升降8度NumericUpDown.Value == 0)
                    {
                        b42_Click(sender, e);
                    }
                    else if (升降8度NumericUpDown.Value == -1)
                    {
                        b30_Click(sender, e);
                    }
                    break;
                case "key分号":
                    if (升降8度NumericUpDown.Value == 1)
                    {
                        b56_Click(sender, e);

                    }
                    else if (升降8度NumericUpDown.Value == 0)
                    {
                        b44_Click(sender, e);
                    }
                    else if (升降8度NumericUpDown.Value == -1)
                    {
                        b32_Click(sender, e);
                    }
                    break;
                case "key引号":
                    if (升降8度NumericUpDown.Value == 1)
                    {
                        b57_Click(sender, e);

                    }
                    else if (升降8度NumericUpDown.Value == 0)
                    {
                        b45_Click(sender, e);
                    }
                    else if (升降8度NumericUpDown.Value == -1)
                    {
                        b33_Click(sender, e);
                    }
                    break;

                    //第四行
                case "keyZ":
                    if (升降8度NumericUpDown.Value == 1)
                    {
                        b28_Click(sender, e);

                    }
                    else if (升降8度NumericUpDown.Value == 0)
                    {
                        b16_Click(sender, e);
                    }
                    else if (升降8度NumericUpDown.Value == -1)
                    {
                        b4_Click(sender, e);
                    }
                    break;
                case "keyX":
                    if (升降8度NumericUpDown.Value == 1)
                    {
                        b30_Click(sender, e);

                    }
                    else if (升降8度NumericUpDown.Value == 0)
                    {
                        b18_Click(sender, e);
                    }
                    else if (升降8度NumericUpDown.Value == -1)
                    {
                        b6_Click(sender, e);
                    }
                    break;
                case "keyC":
                    if (升降8度NumericUpDown.Value == 1)
                    {
                        b32_Click(sender, e);

                    }
                    else if (升降8度NumericUpDown.Value == 0)
                    {
                        b20_Click(sender, e);
                    }
                    else if (升降8度NumericUpDown.Value == -1)
                    {
                        b8_Click(sender, e);
                    }
                    break;
                case "keyV":
                    if (升降8度NumericUpDown.Value == 1)
                    {
                        b33_Click(sender, e);

                    }
                    else if (升降8度NumericUpDown.Value == 0)
                    {
                        b21_Click(sender, e);
                    }
                    else if (升降8度NumericUpDown.Value == -1)
                    {
                        b9_Click(sender, e);
                    }
                    break;
                case "keyB":
                    if (升降8度NumericUpDown.Value == 1)
                    {
                        b35_Click(sender, e);

                    }
                    else if (升降8度NumericUpDown.Value == 0)
                    {
                        b23_Click(sender, e);
                    }
                    else if (升降8度NumericUpDown.Value == -1)
                    {
                        b11_Click(sender, e);
                    }
                    break;
                case "keyN":
                    if (升降8度NumericUpDown.Value == 1)
                    {
                        b37_Click(sender, e);

                    }
                    else if (升降8度NumericUpDown.Value == 0)
                    {
                        b25_Click(sender, e);
                    }
                    else if (升降8度NumericUpDown.Value == -1)
                    {
                        b13_Click(sender, e);
                    }
                    break;
                case "keyM":
                    if (升降8度NumericUpDown.Value == 1)
                    {
                        b39_Click(sender, e);

                    }
                    else if (升降8度NumericUpDown.Value == 0)
                    {
                        b27_Click(sender, e);
                    }
                    else if (升降8度NumericUpDown.Value == -1)
                    {
                        b15_Click(sender, e);
                    }
                    break;
                case "key逗号":
                    if (升降8度NumericUpDown.Value == 1)
                    {
                        b40_Click(sender, e);

                    }
                    else if (升降8度NumericUpDown.Value == 0)
                    {
                        b28_Click(sender, e);
                    }
                    else if (升降8度NumericUpDown.Value == -1)
                    {
                        b16_Click(sender, e);
                    }
                    break;
                case "key句号":
                    if (升降8度NumericUpDown.Value == 1)
                    {
                        b42_Click(sender, e);

                    }
                    else if (升降8度NumericUpDown.Value == 0)
                    {
                        b30_Click(sender, e);
                    }
                    else if (升降8度NumericUpDown.Value == -1)
                    {
                        b18_Click(sender, e);
                    }
                    break;
                case "key问号":
                    if (升降8度NumericUpDown.Value == 1)
                    {
                        b44_Click(sender, e);

                    }
                    else if (升降8度NumericUpDown.Value == 0)
                    {
                        b32_Click(sender, e);
                    }
                    else if (升降8度NumericUpDown.Value == -1)
                    {
                        b20_Click(sender, e);
                    }
                    break;
            }
        }

        //第一行数字，包括从1—0，和-=号
        private void key1_Click(object sender, EventArgs e)
        {
            keyString = "key1";
            keyClick(sender, e);
        }

        private void key2_Click(object sender, EventArgs e)
        {
            keyString = "key2";
            keyClick(sender, e);
        }

        private void key3_Click(object sender, EventArgs e)
        {
            keyString = "key3";
            keyClick(sender, e);
        }

        private void key4_Click(object sender, EventArgs e)
        {
            keyString = "key4";
            keyClick(sender, e);
        }

        private void key5_Click(object sender, EventArgs e)
        {
            keyString = "key5";
            keyClick(sender, e);
        }

        private void key6_Click(object sender, EventArgs e)
        {
            keyString = "key6";
            keyClick(sender, e);
        }

        private void key7_Click(object sender, EventArgs e)
        {
            keyString = "key7";
            keyClick(sender, e);
        }

        private void key8_Click(object sender, EventArgs e)
        {
            keyString = "key8";
            keyClick(sender, e);
        }

        private void key9_Click(object sender, EventArgs e)
        {
            keyString = "key9";
            keyClick(sender, e);
        }

        private void key0_Click(object sender, EventArgs e)
        {
            keyString = "key0";
            keyClick(sender, e);
        }

        private void key减号_Click(object sender, EventArgs e)
        {
            keyString = "key减号";
            keyClick(sender, e);
        }

        private void key等号_Click(object sender, EventArgs e)
        {
            keyString = "key等号";
            keyClick(sender, e);
        }

        //第二行，QWERTYUIOP[]\
        private void keyQ_Click(object sender, EventArgs e)
        {
            keyString = "keyQ";
            keyClick(sender, e);
        }

        private void keyW_Click(object sender, EventArgs e)
        {
            keyString = "keyW";
            keyClick(sender, e);
        }

        private void keyE_Click(object sender, EventArgs e)
        {
            keyString = "keyE";
            keyClick(sender, e);
        }

        private void keyR_Click(object sender, EventArgs e)
        {
            keyString = "keyR";
            keyClick(sender, e);
        }

        private void keyT_Click(object sender, EventArgs e)
        {
            keyString = "keyT";
            keyClick(sender, e);
        }

        private void keyY_Click(object sender, EventArgs e)
        {
            keyString = "keyY";
            keyClick(sender, e);
        }

        private void keyU_Click(object sender, EventArgs e)
        {
            keyString = "keyU";
            keyClick(sender, e);
        }

        private void keyI_Click(object sender, EventArgs e)
        {
            keyString = "keyI";
            keyClick(sender, e);
        }

        private void keyO_Click(object sender, EventArgs e)
        {
            keyString = "keyO";
            keyClick(sender, e);
        }

        private void keyP_Click(object sender, EventArgs e)
        {
            keyString = "keyP";
            keyClick(sender, e);
        }

        private void key左中括号_Click(object sender, EventArgs e)
        {
            keyString = "key左中括号";
            keyClick(sender, e);
        }

        private void key右中括号_Click(object sender, EventArgs e)
        {
            keyString = "key右中括号";
            keyClick(sender, e);
        }

        private void keyBackslash_Click(object sender, EventArgs e)
        {
            keyString = "keyBackslash";
            keyClick(sender, e);
        }

        //第三行，ASDFGHJKL;'
        private void keyA_Click(object sender, EventArgs e)
        {
            keyString = "keyA";
            keyClick(sender, e);
        }

        private void keyS_Click(object sender, EventArgs e)
        {
            keyString = "keyS";
            keyClick(sender, e);
        }

        private void keyD_Click(object sender, EventArgs e)
        {
            keyString = "keyD";
            keyClick(sender, e);
        }

        private void keyF_Click(object sender, EventArgs e)
        {
            keyString = "keyF";
            keyClick(sender, e);
        }

        private void keyG_Click(object sender, EventArgs e)
        {
            keyString = "keyG";
            keyClick(sender, e);
        }

        private void keyH_Click(object sender, EventArgs e)
        {
            keyString = "keyH";
            keyClick(sender, e);
        }

        private void keyJ_Click(object sender, EventArgs e)
        {
            keyString = "keyJ";
            keyClick(sender, e);
        }

        private void keyK_Click(object sender, EventArgs e)
        {
            keyString = "keyK";
            keyClick(sender, e);
        }

        private void keyL_Click(object sender, EventArgs e)
        {
            keyString = "keyL";
            keyClick(sender, e);
        }

        private void key分号_Click(object sender, EventArgs e)
        {
            keyString = "key分号";
            keyClick(sender, e);
        }

        private void key引号_Click(object sender, EventArgs e)
        {
            keyString = "key引号";
            keyClick(sender, e);
        }

        //第四行，ZXCVBNM  ,./
        private void keyZ_Click(object sender, EventArgs e)
        {
            keyString = "keyZ";
            keyClick(sender, e);
        }

        private void keyX_Click(object sender, EventArgs e)
        {
            keyString = "keyX";
            keyClick(sender, e);
        }

        private void keyC_Click(object sender, EventArgs e)
        {
            keyString = "keyC";
            keyClick(sender, e);
        }

        private void keyV_Click(object sender, EventArgs e)
        {
            keyString = "keyV";
            keyClick(sender, e);
        }

        private void keyB_Click(object sender, EventArgs e)
        {
            keyString = "keyB";
            keyClick(sender, e);
        }

        private void keyN_Click(object sender, EventArgs e)
        {
            keyString = "keyN";
            keyClick(sender, e);
        }

        private void keyM_Click(object sender, EventArgs e)
        {
            keyString = "keyM";
            keyClick(sender, e);
        }

        private void key逗号_Click(object sender, EventArgs e)
        {
            keyString = "key逗号";
            keyClick(sender, e);
        }

        private void key句号_Click(object sender, EventArgs e)
        {
            keyString = "key句号";
            keyClick(sender, e);
        }

        private void key问号_Click(object sender, EventArgs e)
        {
            keyString = "key问号";
            keyClick(sender, e);
        }

        private void 清除button_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            实体键盘button.Focus();
        }

        private void 延音_Click(object sender, EventArgs e)
        {
            if (延音checkBox.Checked)
                延音checkBox.Checked = false;
            else
                延音checkBox.Checked = true;
                
        }

        private void F5升八度button_Click(object sender, EventArgs e)
        {
            升降8度NumericUpDown.Value++;
        }

        private void F6降八度button_Click(object sender, EventArgs e)
        {
            升降8度NumericUpDown.Value--;
        }

        private void 升降8度NumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (升降8度NumericUpDown.Value == 1)
            {
                key1.BackgroundImage = piano1.Properties.Resources.__1;
                key2.BackgroundImage = piano1.Properties.Resources.__2;
                key3.BackgroundImage = piano1.Properties.Resources.__3;
                key4.BackgroundImage = piano1.Properties.Resources.__4;
                key5.BackgroundImage = piano1.Properties.Resources.__5;
                key6.BackgroundImage = piano1.Properties.Resources.__6;
                key7.BackgroundImage = piano1.Properties.Resources.__7;
                key8.BackgroundImage = piano1.Properties.Resources.___1;
                key9.BackgroundImage = piano1.Properties.Resources.___2;
                key0.BackgroundImage = piano1.Properties.Resources.___3;
                key减号.BackgroundImage = piano1.Properties.Resources.___4;
                key等号.BackgroundImage = piano1.Properties.Resources.___5;
                keyQ.BackgroundImage = piano1.Properties.Resources._110;
                keyW.BackgroundImage = piano1.Properties.Resources._210;
                keyE.BackgroundImage = piano1.Properties.Resources._310;
                keyR.BackgroundImage = piano1.Properties.Resources._410;
                keyT.BackgroundImage = piano1.Properties.Resources._510;
                keyY.BackgroundImage = piano1.Properties.Resources._610;
                keyU.BackgroundImage = piano1.Properties.Resources._710; 
                keyI.BackgroundImage = piano1.Properties.Resources.__1;
                keyO.BackgroundImage = piano1.Properties.Resources.__2;
                keyP.BackgroundImage = piano1.Properties.Resources.__3; 
                key左中括号.BackgroundImage = piano1.Properties.Resources.__4;
                key右中括号.BackgroundImage = piano1.Properties.Resources.__5;
                keyBackslash.BackgroundImage = piano1.Properties.Resources.__6;
                keyA.BackgroundImage = piano1.Properties.Resources._111;
                keyS.BackgroundImage = piano1.Properties.Resources._211;
                keyD.BackgroundImage = piano1.Properties.Resources._311;
                keyF.BackgroundImage = piano1.Properties.Resources._411;
                keyG.BackgroundImage = piano1.Properties.Resources._511;
                keyH.BackgroundImage = piano1.Properties.Resources._611;
                keyJ.BackgroundImage = piano1.Properties.Resources._711;
                keyK.BackgroundImage = piano1.Properties.Resources._110;
                keyL.BackgroundImage = piano1.Properties.Resources._210;
                key分号.BackgroundImage = piano1.Properties.Resources._310;
                key引号.BackgroundImage = piano1.Properties.Resources._410;
                keyZ.BackgroundImage = piano1.Properties.Resources._1_;
                keyX.BackgroundImage = piano1.Properties.Resources._2_;
                keyC.BackgroundImage = piano1.Properties.Resources._3_;
                keyV.BackgroundImage = piano1.Properties.Resources._4_;
                keyB.BackgroundImage = piano1.Properties.Resources._5_;
                keyN.BackgroundImage = piano1.Properties.Resources._6_;
                keyM.BackgroundImage = piano1.Properties.Resources._7_;
                key逗号.BackgroundImage = piano1.Properties.Resources._111;
                key句号.BackgroundImage = piano1.Properties.Resources._211;
                key问号.BackgroundImage = piano1.Properties.Resources._311;
            }
            else if (升降8度NumericUpDown.Value == 0)
            {
                key1.BackgroundImage = piano1.Properties.Resources._110;
                key2.BackgroundImage = piano1.Properties.Resources._210;
                key3.BackgroundImage = piano1.Properties.Resources._310;
                key4.BackgroundImage = piano1.Properties.Resources._410;
                key5.BackgroundImage = piano1.Properties.Resources._510;
                key6.BackgroundImage = piano1.Properties.Resources._610;
                key7.BackgroundImage = piano1.Properties.Resources._710;
                key8.BackgroundImage = piano1.Properties.Resources.__1;
                key9.BackgroundImage = piano1.Properties.Resources.__2;
                key0.BackgroundImage = piano1.Properties.Resources.__3;
                key减号.BackgroundImage = piano1.Properties.Resources.__4;
                key等号.BackgroundImage = piano1.Properties.Resources.__5;
                keyQ.BackgroundImage = piano1.Properties.Resources._111;
                keyW.BackgroundImage = piano1.Properties.Resources._211;
                keyE.BackgroundImage = piano1.Properties.Resources._311;
                keyR.BackgroundImage = piano1.Properties.Resources._411;
                keyT.BackgroundImage = piano1.Properties.Resources._511;
                keyY.BackgroundImage = piano1.Properties.Resources._611;
                keyU.BackgroundImage = piano1.Properties.Resources._711;
                keyI.BackgroundImage = piano1.Properties.Resources._110;
                keyO.BackgroundImage = piano1.Properties.Resources._210;
                keyP.BackgroundImage = piano1.Properties.Resources._310;
                key左中括号.BackgroundImage = piano1.Properties.Resources._410;
                key右中括号.BackgroundImage = piano1.Properties.Resources._510;
                keyBackslash.BackgroundImage = piano1.Properties.Resources._610;
                keyA.BackgroundImage = piano1.Properties.Resources._1_;
                keyS.BackgroundImage = piano1.Properties.Resources._2_;
                keyD.BackgroundImage = piano1.Properties.Resources._3_;
                keyF.BackgroundImage = piano1.Properties.Resources._4_;
                keyG.BackgroundImage = piano1.Properties.Resources._5_;
                keyH.BackgroundImage = piano1.Properties.Resources._6_;
                keyJ.BackgroundImage = piano1.Properties.Resources._7_;
                keyK.BackgroundImage = piano1.Properties.Resources._111;
                keyL.BackgroundImage = piano1.Properties.Resources._211;
                key分号.BackgroundImage = piano1.Properties.Resources._311;
                key引号.BackgroundImage = piano1.Properties.Resources._411;
                keyZ.BackgroundImage = piano1.Properties.Resources._1__;
                keyX.BackgroundImage = piano1.Properties.Resources._2__;
                keyC.BackgroundImage = piano1.Properties.Resources._3__;
                keyV.BackgroundImage = piano1.Properties.Resources._4__;
                keyB.BackgroundImage = piano1.Properties.Resources._5__;
                keyN.BackgroundImage = piano1.Properties.Resources._6__;
                keyM.BackgroundImage = piano1.Properties.Resources._7__;
                key逗号.BackgroundImage = piano1.Properties.Resources._1_;
                key句号.BackgroundImage = piano1.Properties.Resources._2_;
                key问号.BackgroundImage = piano1.Properties.Resources._3_;
            }
            else if (升降8度NumericUpDown.Value == -1)
            {
                key1.BackgroundImage = piano1.Properties.Resources._111;
                key2.BackgroundImage = piano1.Properties.Resources._211;
                key3.BackgroundImage = piano1.Properties.Resources._311;
                key4.BackgroundImage = piano1.Properties.Resources._411;
                key5.BackgroundImage = piano1.Properties.Resources._511;
                key6.BackgroundImage = piano1.Properties.Resources._611;
                key7.BackgroundImage = piano1.Properties.Resources._711;
                key8.BackgroundImage = piano1.Properties.Resources.__1;
                key9.BackgroundImage = piano1.Properties.Resources.__2;
                key0.BackgroundImage = piano1.Properties.Resources.__3;
                key减号.BackgroundImage = piano1.Properties.Resources.__4;
                key等号.BackgroundImage = piano1.Properties.Resources.__5;
                keyQ.BackgroundImage = piano1.Properties.Resources._1_;
                keyW.BackgroundImage = piano1.Properties.Resources._2_;
                keyE.BackgroundImage = piano1.Properties.Resources._3_;
                keyR.BackgroundImage = piano1.Properties.Resources._4_;
                keyT.BackgroundImage = piano1.Properties.Resources._5_;
                keyY.BackgroundImage = piano1.Properties.Resources._6_;
                keyU.BackgroundImage = piano1.Properties.Resources._7_;
                keyI.BackgroundImage = piano1.Properties.Resources._111;
                keyO.BackgroundImage = piano1.Properties.Resources._211;
                keyP.BackgroundImage = piano1.Properties.Resources._311;
                key左中括号.BackgroundImage = piano1.Properties.Resources._411;
                key右中括号.BackgroundImage = piano1.Properties.Resources._511;
                keyBackslash.BackgroundImage = piano1.Properties.Resources._611;
                keyA.BackgroundImage = piano1.Properties.Resources._1__;
                keyS.BackgroundImage = piano1.Properties.Resources._2__;
                keyD.BackgroundImage = piano1.Properties.Resources._3__;
                keyF.BackgroundImage = piano1.Properties.Resources._4__;
                keyG.BackgroundImage = piano1.Properties.Resources._5__;
                keyH.BackgroundImage = piano1.Properties.Resources._6__;
                keyJ.BackgroundImage = piano1.Properties.Resources._7__;
                keyK.BackgroundImage = piano1.Properties.Resources._1_;
                keyL.BackgroundImage = piano1.Properties.Resources._2_;
                key分号.BackgroundImage = piano1.Properties.Resources._3_;
                key引号.BackgroundImage = piano1.Properties.Resources._4_;
                keyZ.BackgroundImage = piano1.Properties.Resources._1___;
                keyX.BackgroundImage = piano1.Properties.Resources._2___;
                keyC.BackgroundImage = piano1.Properties.Resources._3___;
                keyV.BackgroundImage = piano1.Properties.Resources._4___;
                keyB.BackgroundImage = piano1.Properties.Resources._5___;
                keyN.BackgroundImage = piano1.Properties.Resources._6___;
                keyM.BackgroundImage = piano1.Properties.Resources._7___;
                key逗号.BackgroundImage = piano1.Properties.Resources._1__;
                key句号.BackgroundImage = piano1.Properties.Resources._2__;
                key问号.BackgroundImage = piano1.Properties.Resources._3__;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            richTextBox1.Text += "\n" + dateTimePicker1.Value + "\n";
        }

        private void colorChange()
        {
            b1.BackColor = Color.White;
            b2.BackColor = Color.Black;
            b3.BackColor = Color.White;
            b4.BackColor = Color.White;
            b5.BackColor = Color.Black;
            b6.BackColor = Color.White;
            b7.BackColor = Color.Black;
            b8.BackColor = Color.White;
            b9.BackColor = Color.White;
            b10.BackColor = Color.Black;

            b11.BackColor = Color.White;
            b12.BackColor = Color.Black;
            b13.BackColor = Color.White;
            b14.BackColor = Color.Black;
            b15.BackColor = Color.White;
            b16.BackColor = Color.White;
            b17.BackColor = Color.Black;
            b18.BackColor = Color.White;
            b19.BackColor = Color.Black;
            b20.BackColor = Color.White;

            b21.BackColor = Color.White;
            b22.BackColor = Color.Black;
            b23.BackColor = Color.White;
            b24.BackColor = Color.Black;
            b25.BackColor = Color.White;
            b26.BackColor = Color.Black;
            b27.BackColor = Color.White;
            b28.BackColor = Color.White;
            b29.BackColor = Color.Black;
            b30.BackColor = Color.White;

            b31.BackColor = Color.Black;
            b32.BackColor = Color.White;
            b33.BackColor = Color.White;
            b34.BackColor = Color.Black;
            b35.BackColor = Color.White;
            b36.BackColor = Color.Black;
            b37.BackColor = Color.White;
            b38.BackColor = Color.Black;
            b39.BackColor = Color.White;
            b40.BackColor = Color.White;

            b41.BackColor = Color.Black;
            b42.BackColor = Color.White;
            b43.BackColor = Color.Black;
            b44.BackColor = Color.White;
            b45.BackColor = Color.White;
            b46.BackColor = Color.Black;
            b47.BackColor = Color.White;
            b48.BackColor = Color.Black;
            b49.BackColor = Color.White;
            b50.BackColor = Color.Black;

            b51.BackColor = Color.White;
            b52.BackColor = Color.White;
            b53.BackColor = Color.Black;
            b54.BackColor = Color.White;
            b55.BackColor = Color.Black;
            b56.BackColor = Color.White;
            b57.BackColor = Color.White;
            b58.BackColor = Color.Black;
            b59.BackColor = Color.White;
            b60.BackColor = Color.Black;

            b61.BackColor = Color.White;
            b62.BackColor = Color.Black;
            b63.BackColor = Color.White;
            b64.BackColor = Color.White;
            b65.BackColor = Color.Black;
            b66.BackColor = Color.White;
            b67.BackColor = Color.Black;
            b68.BackColor = Color.White;
            b69.BackColor = Color.White;
            b70.BackColor = Color.Black;

            b71.BackColor = Color.White;
            b72.BackColor = Color.Black;
            b73.BackColor = Color.White;
            b74.BackColor = Color.Black;
            b75.BackColor = Color.White;
            b76.BackColor = Color.White;
            b77.BackColor = Color.Black;
            b78.BackColor = Color.White;
            b79.BackColor = Color.Black;
            b80.BackColor = Color.White;

            b81.BackColor = Color.White;
            b82.BackColor = Color.Black;
            b83.BackColor = Color.White;
            b84.BackColor = Color.Black;
            b85.BackColor = Color.White;
            b86.BackColor = Color.Black;
            b87.BackColor = Color.White;
            b88.BackColor = Color.White;
        }

        private void 恢复默认值ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorChange();
            电脑键盘窗口ToolStripMenuItem.Checked = true;
            实体键盘窗口ToolStripMenuItem.Checked = true;
            升降8度NumericUpDown.Value = 0;
            richTextBox1.Text = "";
            指导曲目comboBox.Text = "";
            延音checkBox.Checked = true;
            实体键盘button.Focus();
        }

        private void 播放button_Click(object sender, EventArgs e)
        {
            if (指导曲目comboBox.Text == "欢乐颂")
            {
                //1
                keyE_Click(sender, e);
                Thread.Sleep(500);
                keyE_Click(sender, e);
                Thread.Sleep(500);
                keyR_Click(sender, e);
                Thread.Sleep(500);
                keyT_Click(sender, e);
                Thread.Sleep(500);

                keyT_Click(sender, e);
                Thread.Sleep(500);
                keyR_Click(sender, e);
                Thread.Sleep(500);
                keyE_Click(sender, e);
                Thread.Sleep(500);
                keyW_Click(sender, e);
                Thread.Sleep(500);

                //2
                keyQ_Click(sender, e);
                Thread.Sleep(500);
                keyQ_Click(sender, e);
                Thread.Sleep(500);
                keyW_Click(sender, e);
                Thread.Sleep(500);
                keyE_Click(sender, e);
                Thread.Sleep(500);

                keyE_Click(sender, e);
                Thread.Sleep(750);
                keyW_Click(sender, e);
                Thread.Sleep(250);
                keyW_Click(sender, e);
                Thread.Sleep(1000);

                //3
                keyE_Click(sender, e);
                Thread.Sleep(500);
                keyE_Click(sender, e);
                Thread.Sleep(500);
                keyR_Click(sender, e);
                Thread.Sleep(500);
                keyT_Click(sender, e);
                Thread.Sleep(500);

                keyT_Click(sender, e);
                Thread.Sleep(500);
                keyR_Click(sender, e);
                Thread.Sleep(500);
                keyE_Click(sender, e);
                Thread.Sleep(500);
                keyW_Click(sender, e);
                Thread.Sleep(500);

                //4
                keyQ_Click(sender, e);
                Thread.Sleep(500);
                keyQ_Click(sender, e);
                Thread.Sleep(500);
                keyW_Click(sender, e);
                Thread.Sleep(500);
                keyE_Click(sender, e);
                Thread.Sleep(500);

                keyW_Click(sender, e);
                Thread.Sleep(750);
                keyQ_Click(sender, e);
                Thread.Sleep(250);
                keyQ_Click(sender, e);
                Thread.Sleep(1000);

                //5
                keyW_Click(sender, e);
                Thread.Sleep(500);
                keyW_Click(sender, e);
                Thread.Sleep(500);
                keyE_Click(sender, e);
                Thread.Sleep(500);
                keyQ_Click(sender, e);
                Thread.Sleep(500);

                keyW_Click(sender, e);
                Thread.Sleep(500);
                keyE_Click(sender, e);
                Thread.Sleep(250);
                keyR_Click(sender, e);
                Thread.Sleep(250);
                keyE_Click(sender, e);
                Thread.Sleep(500);
                keyQ_Click(sender, e);
                Thread.Sleep(500);

                //6
                keyW_Click(sender, e);
                Thread.Sleep(500);
                keyE_Click(sender, e);
                Thread.Sleep(250);
                keyR_Click(sender, e);
                Thread.Sleep(250);
                keyE_Click(sender, e);
                Thread.Sleep(500);
                keyQ_Click(sender, e);
                Thread.Sleep(500);

                keyQ_Click(sender, e);
                Thread.Sleep(750);
                keyW_Click(sender, e);
                Thread.Sleep(250);
                keyG_Click(sender, e);
                Thread.Sleep(1000);

                //7
                keyE_Click(sender, e);
                Thread.Sleep(750);
                keyE_Click(sender, e);
                Thread.Sleep(500);
                keyR_Click(sender, e);
                Thread.Sleep(500);
                keyT_Click(sender, e);
                Thread.Sleep(500);

                keyT_Click(sender, e);
                Thread.Sleep(500);
                keyR_Click(sender, e);
                Thread.Sleep(500);
                keyE_Click(sender, e);
                Thread.Sleep(500);
                keyW_Click(sender, e);
                Thread.Sleep(500);

                //8
                keyQ_Click(sender, e);
                Thread.Sleep(500);
                keyQ_Click(sender, e);
                Thread.Sleep(500);
                keyW_Click(sender, e);
                Thread.Sleep(500);
                keyE_Click(sender, e);
                Thread.Sleep(500);

                keyW_Click(sender, e);
                Thread.Sleep(750);
                keyQ_Click(sender, e);
                Thread.Sleep(250);
                keyQ_Click(sender, e);
                Thread.Sleep(1000);
            }

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            yfpictureBox.Top -= space;
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            yfpictureBox2.Top -= space;
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            yfpictureBox3.Top -= space;
        }

        private void timer5_Tick(object sender, EventArgs e)
        { 
            yfpictureBox4.Top -= space;
        }

        private void timer6_Tick(object sender, EventArgs e)
        {
            yfpictureBox5.Top -= space;
        }

        private void timer7_Tick(object sender, EventArgs e)
        {
            yfpictureBox6.Top -= space;
        }

        private void timer8_Tick(object sender, EventArgs e)
        {
            yfpictureBox7.Top -= space;
        }

        private void pictureMove1()
        {
            yfpictureBox.Location = new Point(540, 207);
            yfpictureBox.Visible = true;
            timer2.Start();
        }

        private void pictureMove2()
        {
            yfpictureBox2.Location = new Point(566, 207);
            yfpictureBox2.Visible = true;
            timer3.Start();
        }

        private void pictureMove3()
        {
            yfpictureBox3.Location = new Point(599, 207);
            yfpictureBox3.Visible = true;
            timer4.Start();
        }

        private void pictureMove4()
        {
            yfpictureBox4.Location = new Point(633, 207);
            yfpictureBox4.Visible = true;
            timer5.Start();
        }

        private void pictureMove5()
        {
            yfpictureBox5.Location = new Point(666, 207);
            yfpictureBox5.Visible = true;
            timer6.Start();
        }

        private void pictureMove6()
        {
            yfpictureBox6.Location = new Point(699, 207);
            yfpictureBox6.Visible = true;
            timer7.Start();
        }

        private void pictureMove7()
        {
            yfpictureBox7.Location = new Point(733, 207);
            yfpictureBox7.Visible = true;
            timer8.Start();
        }
    }
}
