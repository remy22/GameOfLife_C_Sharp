namespace GameOfLife
{
    partial class MenuForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuForm));
            this.newGame = new System.Windows.Forms.Button();
            this.loadGame = new System.Windows.Forms.Button();
            this.exitGame = new System.Windows.Forms.Button();
            this.title = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // newGame
            // 
            this.newGame.Location = new System.Drawing.Point(69, 111);
            this.newGame.Name = "newGame";
            this.newGame.Size = new System.Drawing.Size(150, 35);
            this.newGame.TabIndex = 0;
            this.newGame.Text = "button1";
            this.newGame.UseVisualStyleBackColor = true;
            this.newGame.Click += new System.EventHandler(this.newGame_Click);
            // 
            // loadGame
            // 
            this.loadGame.Location = new System.Drawing.Point(69, 173);
            this.loadGame.Name = "loadGame";
            this.loadGame.Size = new System.Drawing.Size(150, 37);
            this.loadGame.TabIndex = 1;
            this.loadGame.Text = "button2";
            this.loadGame.UseVisualStyleBackColor = true;
            this.loadGame.Click += new System.EventHandler(this.loadGame_Click);
            // 
            // exitGame
            // 
            this.exitGame.Location = new System.Drawing.Point(69, 234);
            this.exitGame.Name = "exitGame";
            this.exitGame.Size = new System.Drawing.Size(150, 36);
            this.exitGame.TabIndex = 2;
            this.exitGame.Text = "button3";
            this.exitGame.UseVisualStyleBackColor = true;
            this.exitGame.Click += new System.EventHandler(this.exitGame_Click);
            // 
            // title
            // 
            this.title.Location = new System.Drawing.Point(66, 35);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(153, 51);
            this.title.TabIndex = 3;
            this.title.Text = "Game Of LifeMENU";
            this.title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.title.Click += new System.EventHandler(this.title_Click);
            // 
            // MenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(287, 321);
            this.Controls.Add(this.title);
            this.Controls.Add(this.exitGame);
            this.Controls.Add(this.loadGame);
            this.Controls.Add(this.newGame);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MenuForm";
            this.Text = "MenuForm";
            this.Load += new System.EventHandler(this.MenuForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button newGame;
        private System.Windows.Forms.Button loadGame;
        private System.Windows.Forms.Button exitGame;
        private System.Windows.Forms.Label title;
    }
}