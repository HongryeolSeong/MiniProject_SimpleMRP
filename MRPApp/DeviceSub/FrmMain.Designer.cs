
namespace DeviceSub
{
    partial class FrmMain
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtConnectionString = new System.Windows.Forms.TextBox();
            this.TxtClientID = new System.Windows.Forms.TextBox();
            this.TxtSubcriptionTopic = new System.Windows.Forms.TextBox();
            this.BtnConnect = new System.Windows.Forms.Button();
            this.BtnDisconnect = new System.Windows.Forms.Button();
            this.RtbSubscr = new System.Windows.Forms.RichTextBox();
            this.Timer = new System.Windows.Forms.Timer(this.components);
            this.abc = new System.Windows.Forms.StatusStrip();
            this.LblResult = new System.Windows.Forms.ToolStripStatusLabel();
            this.abc.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("나눔고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(34, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Connection String";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("나눔고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(87, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 14);
            this.label2.TabIndex = 0;
            this.label2.Text = "Client ID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("나눔고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(28, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 14);
            this.label3.TabIndex = 0;
            this.label3.Text = "Subscription Topic";
            // 
            // TxtConnectionString
            // 
            this.TxtConnectionString.Font = new System.Drawing.Font("나눔고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.TxtConnectionString.Location = new System.Drawing.Point(150, 18);
            this.TxtConnectionString.Name = "TxtConnectionString";
            this.TxtConnectionString.Size = new System.Drawing.Size(484, 21);
            this.TxtConnectionString.TabIndex = 1;
            this.TxtConnectionString.Text = "210.119.12.93";
            // 
            // TxtClientID
            // 
            this.TxtClientID.Font = new System.Drawing.Font("나눔고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.TxtClientID.Location = new System.Drawing.Point(150, 50);
            this.TxtClientID.Name = "TxtClientID";
            this.TxtClientID.Size = new System.Drawing.Size(484, 21);
            this.TxtClientID.TabIndex = 1;
            this.TxtClientID.Text = "SUBSCR01";
            // 
            // TxtSubcriptionTopic
            // 
            this.TxtSubcriptionTopic.Font = new System.Drawing.Font("나눔고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.TxtSubcriptionTopic.Location = new System.Drawing.Point(150, 82);
            this.TxtSubcriptionTopic.Name = "TxtSubcriptionTopic";
            this.TxtSubcriptionTopic.Size = new System.Drawing.Size(484, 21);
            this.TxtSubcriptionTopic.TabIndex = 1;
            this.TxtSubcriptionTopic.Text = "factory1/machine1/data/";
            // 
            // BtnConnect
            // 
            this.BtnConnect.Font = new System.Drawing.Font("나눔고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnConnect.Location = new System.Drawing.Point(965, 74);
            this.BtnConnect.Name = "BtnConnect";
            this.BtnConnect.Size = new System.Drawing.Size(86, 35);
            this.BtnConnect.TabIndex = 2;
            this.BtnConnect.Text = "Connect";
            this.BtnConnect.UseVisualStyleBackColor = true;
            this.BtnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
            // 
            // BtnDisconnect
            // 
            this.BtnDisconnect.Font = new System.Drawing.Font("나눔고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnDisconnect.Location = new System.Drawing.Point(1057, 74);
            this.BtnDisconnect.Name = "BtnDisconnect";
            this.BtnDisconnect.Size = new System.Drawing.Size(86, 35);
            this.BtnDisconnect.TabIndex = 2;
            this.BtnDisconnect.Text = "Disconnect";
            this.BtnDisconnect.UseVisualStyleBackColor = true;
            this.BtnDisconnect.Click += new System.EventHandler(this.BtnDisconnect_Click);
            // 
            // RtbSubscr
            // 
            this.RtbSubscr.Font = new System.Drawing.Font("나눔고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.RtbSubscr.Location = new System.Drawing.Point(25, 120);
            this.RtbSubscr.Name = "RtbSubscr";
            this.RtbSubscr.Size = new System.Drawing.Size(1128, 520);
            this.RtbSubscr.TabIndex = 3;
            this.RtbSubscr.Text = "";
            // 
            // abc
            // 
            this.abc.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LblResult});
            this.abc.Location = new System.Drawing.Point(0, 639);
            this.abc.Name = "abc";
            this.abc.Size = new System.Drawing.Size(1184, 22);
            this.abc.TabIndex = 4;
            this.abc.Text = "statusStrip1";
            // 
            // LblResult
            // 
            this.LblResult.Name = "LblResult";
            this.LblResult.Size = new System.Drawing.Size(14, 17);
            this.LblResult.Text = "0";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.abc);
            this.Controls.Add(this.RtbSubscr);
            this.Controls.Add(this.BtnDisconnect);
            this.Controls.Add(this.BtnConnect);
            this.Controls.Add(this.TxtSubcriptionTopic);
            this.Controls.Add(this.TxtClientID);
            this.Controls.Add(this.TxtConnectionString);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.Text = "IoT Device Subsriber";
            this.abc.ResumeLayout(false);
            this.abc.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TxtConnectionString;
        private System.Windows.Forms.TextBox TxtClientID;
        private System.Windows.Forms.TextBox TxtSubcriptionTopic;
        private System.Windows.Forms.Button BtnConnect;
        private System.Windows.Forms.Button BtnDisconnect;
        private System.Windows.Forms.RichTextBox RtbSubscr;
        private System.Windows.Forms.Timer Timer;
        private System.Windows.Forms.StatusStrip abc;
        private System.Windows.Forms.ToolStripStatusLabel LblResult;
    }
}

