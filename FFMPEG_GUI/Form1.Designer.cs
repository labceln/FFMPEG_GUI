namespace FFMPEG_GUI
{
  partial class Form1
  {
    /// <summary>
    /// 必要なデザイナー変数です。
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// 使用中のリソースをすべてクリーンアップします。
    /// </summary>
    /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows フォーム デザイナーで生成されたコード

    /// <summary>
    /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
    /// コード エディターで変更しないでください。
    /// </summary>
    private void InitializeComponent()
    {
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.button1 = new System.Windows.Forms.Button();
      this.textBox2 = new System.Windows.Forms.TextBox();
      this.textBox3 = new System.Windows.Forms.TextBox();
      this.comboBox1 = new System.Windows.Forms.ComboBox();
      this.button3 = new System.Windows.Forms.Button();
      this.textBox4 = new System.Windows.Forms.TextBox();
      this.comboBox2 = new System.Windows.Forms.ComboBox();
      this.button4 = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // textBox1
      // 
      this.textBox1.Location = new System.Drawing.Point(12, 12);
      this.textBox1.Multiline = true;
      this.textBox1.Name = "textBox1";
      this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.textBox1.Size = new System.Drawing.Size(538, 426);
      this.textBox1.TabIndex = 0;
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(569, 12);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(219, 85);
      this.button1.TabIndex = 1;
      this.button1.Text = "外部アプリケーション実行";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // textBox2
      // 
      this.textBox2.Location = new System.Drawing.Point(569, 112);
      this.textBox2.Name = "textBox2";
      this.textBox2.Size = new System.Drawing.Size(219, 19);
      this.textBox2.TabIndex = 2;
      this.textBox2.Text = "D:\\NHK\\ffmpeg.exe";
      // 
      // textBox3
      // 
      this.textBox3.ForeColor = System.Drawing.SystemColors.GrayText;
      this.textBox3.Location = new System.Drawing.Point(569, 149);
      this.textBox3.Name = "textBox3";
      this.textBox3.Size = new System.Drawing.Size(219, 19);
      this.textBox3.TabIndex = 3;
      // 
      // comboBox1
      // 
      this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.comboBox1.FormattingEnabled = true;
      this.comboBox1.Location = new System.Drawing.Point(569, 188);
      this.comboBox1.Name = "comboBox1";
      this.comboBox1.Size = new System.Drawing.Size(219, 20);
      this.comboBox1.TabIndex = 4;
      // 
      // button3
      // 
      this.button3.Location = new System.Drawing.Point(569, 402);
      this.button3.Name = "button3";
      this.button3.Size = new System.Drawing.Size(151, 36);
      this.button3.TabIndex = 6;
      this.button3.Text = "標準入力を送信";
      this.button3.UseVisualStyleBackColor = true;
      this.button3.Click += new System.EventHandler(this.button3_Click);
      // 
      // textBox4
      // 
      this.textBox4.ForeColor = System.Drawing.SystemColors.GrayText;
      this.textBox4.Location = new System.Drawing.Point(569, 296);
      this.textBox4.Name = "textBox4";
      this.textBox4.Size = new System.Drawing.Size(219, 19);
      this.textBox4.TabIndex = 7;
      // 
      // comboBox2
      // 
      this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.comboBox2.FormattingEnabled = true;
      this.comboBox2.Location = new System.Drawing.Point(732, 418);
      this.comboBox2.Name = "comboBox2";
      this.comboBox2.Size = new System.Drawing.Size(56, 20);
      this.comboBox2.TabIndex = 8;
      // 
      // button4
      // 
      this.button4.Location = new System.Drawing.Point(569, 338);
      this.button4.Name = "button4";
      this.button4.Size = new System.Drawing.Size(119, 43);
      this.button4.TabIndex = 9;
      this.button4.Text = "初期値を入力";
      this.button4.UseVisualStyleBackColor = true;
      this.button4.Click += new System.EventHandler(this.button4_Click);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(800, 450);
      this.Controls.Add(this.button4);
      this.Controls.Add(this.comboBox2);
      this.Controls.Add(this.textBox4);
      this.Controls.Add(this.button3);
      this.Controls.Add(this.comboBox1);
      this.Controls.Add(this.textBox3);
      this.Controls.Add(this.textBox2);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.textBox1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.Name = "Form1";
      this.Text = "Form1";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_Closing);
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_Closed);
      this.Load += new System.EventHandler(this.Form1_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.TextBox textBox2;
    private System.Windows.Forms.TextBox textBox3;
    private System.Windows.Forms.ComboBox comboBox1;
    private System.Windows.Forms.Button button3;
    private System.Windows.Forms.TextBox textBox4;
    private System.Windows.Forms.ComboBox comboBox2;
    private System.Windows.Forms.Button button4;
  }
}

