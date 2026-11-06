using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace KineticaRT.Studio
{
    //ft deplace dans KineticaRT.Interfaces
    ///// <summary>
    ///// Interface to the splash screen displayed at startup and while loading.
    ///// </summary>
    //public interface ISplash
    //{
    //    /// <summary>
    //    /// Sets the top line of text
    //    /// </summary>
    //    /// <param name="Text">Text to set</param>
    //    void SetText(string Text) ;

    //    /// <summary>
    //    /// Sets the bottom line of text
    //    /// </summary>
    //    /// <param name="Text">Text to set</param>
    //    void SetProgress(string Text) ;

    //    /// <summary>
    //    /// Shows the Window.
    //    /// </summary>
    //    void Show();

    //    /// <summary>
    //    /// Hides the window.
    //    /// </summary>
    //    void Hide();
    //}

    // Original integrated Splash:

    ///// <summary>
    ///// Summary description for Splash.
    ///// </summary>
    //public class Splash : System.Windows.Forms.Form
    //{
    //    private System.Windows.Forms.Label lblProgress;
    //    private System.Windows.Forms.Label lblTitle;
    //    private System.Windows.Forms.PictureBox pictureBox1;
    //    /// <summary>
    //    /// Required designer variable.
    //    /// </summary>
    //    private System.ComponentModel.Container components = null;

    //    /// <summary>
    //    /// Initialises a new instance of the Splash class
    //    /// </summary>
    //    public Splash()//Studio.Loader Loader)
    //    {
    //        //
    //        // Required for Windows Form Designer support
    //        //
    //        InitializeComponent();

    //        //if (Loader != null)	Loader.Progress += new Loader.ProgressEventHandler(OnProgress) ;
    //    }

    //    /// <summary>
    //    /// Clean up any resources being used.
    //    /// </summary>
    //    protected override void Dispose( bool disposing )
    //    {
    //        if( disposing )
    //        {
    //            if(components != null)
    //            {
    //                components.Dispose();
    //            }
    //        }
    //        base.Dispose( disposing );
    //    }

    //    #region Windows Form Designer generated code
    //    /// <summary>
    //    /// Required method for Designer support - do not modify
    //    /// the contents of this method with the code editor.
    //    /// </summary>
    //    private void InitializeComponent()
    //    {
    //        System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Splash));
    //        this.lblTitle = new System.Windows.Forms.Label();
    //        this.lblProgress = new System.Windows.Forms.Label();
    //        this.pictureBox1 = new System.Windows.Forms.PictureBox();
    //        this.SuspendLayout();
    //        // 
    //        // lblTitle
    //        // 
    //        this.lblTitle.Location = new System.Drawing.Point(16, 72);
    //        this.lblTitle.Name = "lblTitle";
    //        this.lblTitle.Size = new System.Drawing.Size(272, 24);
    //        this.lblTitle.TabIndex = 0;
    //        this.lblTitle.Text = "Loading Progress...";
    //        this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
    //        // 
    //        // lblProgress
    //        // 
    //        this.lblProgress.Location = new System.Drawing.Point(16, 96);
    //        this.lblProgress.Name = "lblProgress";
    //        this.lblProgress.Size = new System.Drawing.Size(272, 56);
    //        this.lblProgress.TabIndex = 1;
    //        this.lblProgress.TextAlign = System.Drawing.ContentAlignment.TopCenter;
    //        // 
    //        // pictureBox1
    //        // 
    //        this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
    //        this.pictureBox1.Location = new System.Drawing.Point(0, 0);
    //        this.pictureBox1.Name = "pictureBox1";
    //        this.pictureBox1.Size = new System.Drawing.Size(304, 64);
    //        this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
    //        this.pictureBox1.TabIndex = 2;
    //        this.pictureBox1.TabStop = false;
    //        // 
    //        // Splash
    //        // 
    //        this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
    //        this.BackColor = System.Drawing.Color.White;
    //        this.ClientSize = new System.Drawing.Size(302, 158);
    //        this.ControlBox = false;
    //        this.Controls.Add(this.pictureBox1);
    //        this.Controls.Add(this.lblProgress);
    //        this.Controls.Add(this.lblTitle);
    //        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
    //        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
    //        this.Name = "Splash";
    //        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
    //        this.ResumeLayout(false);

    //    }
    //    #endregion

    //    /// <summary>
    //    /// Sets the top line of text
    //    /// </summary>
    //    /// <param name="Text">Text to set</param>
    //    public void SetText(string Text)
    //    {
    //        lblTitle.Text = Text ;
    //        this.Refresh() ;
    //    }

    //    /// <summary>
    //    /// Sets the bottom line of text
    //    /// </summary>
    //    /// <param name="Text">Text to set</param>
    //    public void SetProgress(string Text)
    //    {
    //        lblProgress.Text = Text ;
    //        this.Refresh() ;
    //    }

    //    private void OnProgress (string Info)
    //    {
    //        lblProgress.Text = Info ;
    //    }
	//}
}
