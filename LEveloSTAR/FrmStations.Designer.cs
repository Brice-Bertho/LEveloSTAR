namespace LEveloSTAR
{
    partial class FrmStations
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmStations));
            this.laCarte = new GMap.NET.WindowsForms.GMapControl();
            this.txtInfosStationChoisie = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // laCarte
            // 
            this.laCarte.Bearing = 0F;
            this.laCarte.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.laCarte.CanDragMap = true;
            this.laCarte.EmptyTileColor = System.Drawing.Color.Navy;
            this.laCarte.GrayScaleMode = false;
            this.laCarte.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.laCarte.LevelsKeepInMemmory = 5;
            this.laCarte.Location = new System.Drawing.Point(0, 0);
            this.laCarte.MarkersEnabled = true;
            this.laCarte.MaxZoom = 18;
            this.laCarte.MinZoom = 2;
            this.laCarte.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.laCarte.Name = "laCarte";
            this.laCarte.NegativeMode = false;
            this.laCarte.PolygonsEnabled = true;
            this.laCarte.RetryLoadTile = 0;
            this.laCarte.RoutesEnabled = true;
            this.laCarte.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.laCarte.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.laCarte.ShowTileGridLines = false;
            this.laCarte.Size = new System.Drawing.Size(1133, 998);
            this.laCarte.TabIndex = 0;
            this.laCarte.Zoom = 13D;
            this.laCarte.OnMarkerClick += new GMap.NET.WindowsForms.MarkerClick(this.laCarte_OnMarkerClick);
            // 
            // txtInfosStationChoisie
            // 
            this.txtInfosStationChoisie.Location = new System.Drawing.Point(8, 8);
            this.txtInfosStationChoisie.Multiline = true;
            this.txtInfosStationChoisie.Name = "txtInfosStationChoisie";
            this.txtInfosStationChoisie.ReadOnly = true;
            this.txtInfosStationChoisie.Size = new System.Drawing.Size(275, 134);
            this.txtInfosStationChoisie.TabIndex = 1;
            this.txtInfosStationChoisie.Text = "Cliquez sur une station pour afficher ses disponibilités.\r\n\r\nVous pouvez égalemen" +
    "t zoomer sur la carte avec la molette de la souris.";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::LEveloSTAR.Properties.Resources.logoVeloStar;
            this.pictureBox1.Location = new System.Drawing.Point(153, 76);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(119, 56);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // FrmStations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1134, 998);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtInfosStationChoisie);
            this.Controls.Add(this.laCarte);
            this.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FrmStations";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Disponibilité des stations ";
            this.Resize += new System.EventHandler(this.FrmStations_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GMap.NET.WindowsForms.GMapControl laCarte;
        private System.Windows.Forms.TextBox txtInfosStationChoisie;
        private System.Windows.Forms.PictureBox pictureBox1;

    }
}