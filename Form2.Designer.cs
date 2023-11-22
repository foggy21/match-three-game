namespace MatchThreeGame
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            score = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            timerLabel = new Label();
            SuspendLayout();
            // 
            // score
            // 
            score.AutoSize = true;
            score.Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            score.Location = new Point(1037, 41);
            score.Name = "score";
            score.Size = new Size(71, 26);
            score.TabIndex = 0;
            score.Text = "Score:";
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            // 
            // timerLabel
            // 
            timerLabel.AutoSize = true;
            timerLabel.Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            timerLabel.Location = new Point(1037, 83);
            timerLabel.Name = "timerLabel";
            timerLabel.Size = new Size(118, 26);
            timerLabel.TabIndex = 1;
            timerLabel.Text = "TimerLabel";
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1162, 977);
            Controls.Add(timerLabel);
            Controls.Add(score);
            Name = "Form2";
            Text = "Game Match3";
            WindowState = FormWindowState.Maximized;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label score;
        private System.Windows.Forms.Timer timer1;
        private Label timerLabel;
    }
}