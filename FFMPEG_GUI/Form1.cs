using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

/*
 *http://home.a00.itscom.net/hatada/csharp/process03.html
 *https://qiita.com/skitoy4321/items/10c47eea93e5c6145d48
 *http://home.a00.itscom.net/hatada/csharp/process03.html
 *http://cammy.co.jp/technical/2017/05/16/c-%E6%A8%99%E6%BA%96%E5%87%BA%E5%8A%9B%E3%82%92%E8%87%AA%E5%88%86%E3%81%AE%E3%83%97%E3%83%AD%E3%82%B0%E3%83%A9%E3%83%A0%E3%81%AB%E3%83%AA%E3%83%80%E3%82%A4%E3%83%AC%E3%82%AF%E3%83%88%E3%81%99/
 *http://gushwell.ldblog.jp/archives/52381508.html
 *https://qiita.com/sensuikan1973/items/347eb0b674184bb52384
 *https://www.atmarkit.co.jp/ait/articles/0710/11/news123.html
 *
 *
*/

namespace FFMPEG_GUI
{
  public partial class Form1 : Form
  {

    // 外部プロセス(XXX.exe)を宣言
    Process process = null;

    Dictionary<string, string> Radiko_StationToID = new Dictionary<string, string>()
      {
        {"TBSラジオ", "TBS"},
        {"文化放送", "QRR"},
        {"ニッポン放送", "LFR"},
        {"ラジオNIKKEI第1","RN1" },
        {"ラジオNIKKEI第2","RN2" },        
        {"InterFM897","INT" },
        {"TOKYO FM","TFM" },
        {"J-WAVE","FMJ" },
        {"ラジオ日本","JORF" },
        {"bayfm78","BAYFM78" },
        {"NACK5","NACK5" },
        {"ＦＭヨコハマ","YFM" },
        {"放送大学","HOUSOU-DAIGAKU" },
        {"NHKラジオ第1（東京）","JOAK" },
        {"NHK-FM（東京）","JOAK-FM" }
      };

    Dictionary<string, string> Rajiru_StationToID = new Dictionary<string, string>()
      {
        {"NHKラジオ第1(らじるらじる)", "https://nhkradioakr1-i.akamaihd.net/hls/live/511633/1-r1/1-r1-01.m3u8"},
        {"NHK-FM(らじるらじる)", "https://nhkradioakfm-i.akamaihd.net/hls/live/512290/1-fm/1-fm-01.m3u8"},
        {"NHKラジオ第2(らじるらじる)", "https://nhkradioakr2-i.akamaihd.net/hls/live/511929/1-r2/1-r2-01.m3u8"},
      };

    /**
         * @brief コンストラクタ
         *        XXX.exeを起動する
         
    */
    public Form1()
    {
      InitializeComponent();
      //イベントハンドラを作成
      

      var station= Radiko_StationToID.Keys.Concat(Rajiru_StationToID.Keys).ToArray();

      comboBox1.Items.AddRange(station );
      comboBox1.Text = station.Last();

      string[] InputCommand = { "q\n", "y\n" };
      comboBox2.Items.AddRange(InputCommand);
      comboBox2.Text = InputCommand.First();
      

      textBox4.Text = "時間を指定してください";
      textBox3.Text = "保存する場所を指定して下さい";
    }

    /**
        * @brief Formロード時
        */
    private void Form1_Load(object sender, EventArgs e)
    {
      // Formが閉じられた時のイベントを登録
      this.FormClosed += new FormClosedEventHandler(Form1_Closed);
    }

    /**
     * @brief Formが閉じられた時の処理
     *        外部プロセス(xxx.exe)をkillする
     */
    private void Form1_Closed(object sender, FormClosedEventArgs e)
    {
      Properties.Settings.Default.Save();
      KillProcess();
    }

    private async void button1_Click(object sender, EventArgs e)
    {
      string m3u8 ;
      //FFMPEGが終了するまでボタンを使えなくする
      button1.Enabled = false;

      //コンボボックスの放送局名がらじるに含まれるか
      if (Rajiru_StationToID.Keys.Contains(comboBox1.Text))
        m3u8 = Rajiru_StationToID[comboBox1.Text];
      else {
        radikoAPI Radiko = new radikoAPI();
        //https://qiita.com/takutoy/items/d45aa736ced25a8158b3
        m3u8 = await Radiko.Tuning(Radiko_StationToID[comboBox1.Text]);
      }

      //保存先の指定
      string outputdirectorypath;
      if (string.IsNullOrEmpty(textBox3.Text))
        outputdirectorypath = Directory.GetCurrentDirectory();
      else
        outputdirectorypath = textBox3.Text;

      //保存時間の指定
      string Rec_Duration="";
      if (!string.IsNullOrEmpty(textBox4.Text))
        Rec_Duration = " -t " + textBox4.Text;

      //FFMPEGに入れる引数
      DateTime dt = DateTime.Now;
      var date=dt.ToString("-yyyy-MM-dd-HH-mm-ss");
      string filename = comboBox1.Text + date + ".aac";
      string args = "-i " + m3u8 + Rec_Duration + " -acodec copy" +" \""+outputdirectorypath+"\\"+filename + "\"";

      Debug.WriteLine("SEND THIS ARGS TO EXE:"+ args);
      AsyncProcTest(textBox2.Text, args);
    }
    
    private void AsyncProcTest(string exe_Path, string args)
    {
      process = new Process();
      var si = new ProcessStartInfo(exe_Path,args);
      // ウィンドウ表示を完全に消したい場合
      si.CreateNoWindow = true;

      // 標準入出力をリダイレクト
      si.RedirectStandardError = true;
      si.RedirectStandardOutput = true;
      si.RedirectStandardInput = true;
      // 非同期処理のために、ShellExecuteを使わない設定にする
      // BeginOutputReadLine()を利用するための条件
      si.UseShellExecute = false;
  
      
      {
        process.EnableRaisingEvents = true;
        process.StartInfo = si;
        process.SynchronizingObject = this;
        // コールバックの設定
        process.OutputDataReceived += (sender, ev) =>
        {
          this.Invoke((MethodInvoker)(() => { this.textBox1.Text += ev.Data; }));
        };
        process.ErrorDataReceived += (sender, ev) =>
        {
          this.Invoke((MethodInvoker)(() =>{this.textBox1.Text += ev.Data+"\r\n";}));
        };
        process.Exited += (sender, ev) =>
        {
          KillProcess();
          button1.Enabled = true;
          Console.WriteLine($"exited");
          MessageBox.Show("終了しました。");
        };
        // プロセスの開始
        process.Start();
        // 非同期出力読出し開始
        process.BeginErrorReadLine();
        process.BeginOutputReadLine();
       
      }
    }

    private void KillProcess()
    {
      try
      {
        if (process != null && !process.HasExited)
        {
          // Discard cached information about the process.
          process.Refresh();
          
          // Wait 2 seconds.
          process.Kill();
          Task.Delay(2000);

          process.CloseMainWindow();
          process.Dispose();
          process = null;
        }
      }
      catch (InvalidOperationException ex)
      {
        MessageBox.Show(ex.ToString());
      }
    }

    //FFMPEGに標準入力からコマンドを送信
    private void button3_Click(object sender, EventArgs e)
    {
      if (process == null || process.HasExited)
        return;

      if (MessageBox.Show("入力を送信するか?", "確認",
         MessageBoxButtons.YesNo) == DialogResult.No)
        return;

      StreamWriter Input = process.StandardInput;
      Input.WriteLine(comboBox2.Text);
    }

    private void Form1_Closing(object sender, FormClosingEventArgs e)
    {
      e.Cancel = true;

      if (MessageBox.Show("終了しますか?", "確認",
         MessageBoxButtons.YesNo) == DialogResult.Yes)
        e.Cancel = false;

      return;
    }

    //初期値を入れる
    private void button4_Click(object sender, EventArgs e)
    {
      textBox3.Text = "D:\\Radio";
      textBox4.Text = "3600";
      textBox3.ForeColor = textBox4.ForeColor = SystemColors.WindowText;
    }
  }
}
