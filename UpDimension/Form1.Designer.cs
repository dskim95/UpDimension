namespace UpDimension
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.openButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.sceneButton = new System.Windows.Forms.Button();
            this.exportButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Point = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.imageList = new System.Windows.Forms.CheckedListBox();
            this.displayList = new System.Windows.Forms.CheckedListBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.radioButtonX = new System.Windows.Forms.RadioButton();
            this.radioButtonZ = new System.Windows.Forms.RadioButton();
            this.radioButtonY = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.selectButton = new System.Windows.Forms.RadioButton();
            this.guideLineButton = new System.Windows.Forms.RadioButton();
            this.faceButton = new System.Windows.Forms.RadioButton();
            this.pointButton = new System.Windows.Forms.RadioButton();
            this.vanishingLineButton = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "JPEG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|BMP Files (*.bmp)|*.bmp|All file" +
    "s (*.*)|*.*";
            this.openFileDialog1.Title = "Open an image file";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureBox1.Location = new System.Drawing.Point(308, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1356, 1017);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseLeave += new System.EventHandler(this.pictureBox1_MouseLeave);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            this.pictureBox1.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseWheel);
            // 
            // openButton
            // 
            this.openButton.BackColor = System.Drawing.Color.Transparent;
            this.openButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("openButton.BackgroundImage")));
            this.openButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.openButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.openButton.FlatAppearance.BorderSize = 0;
            this.openButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.openButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.openButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.openButton.Location = new System.Drawing.Point(15, 24);
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(61, 50);
            this.openButton.TabIndex = 2;
            this.openButton.UseVisualStyleBackColor = false;
            this.openButton.Click += new System.EventHandler(this.openButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.BackColor = System.Drawing.Color.Transparent;
            this.saveButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("saveButton.BackgroundImage")));
            this.saveButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.saveButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.saveButton.FlatAppearance.BorderSize = 0;
            this.saveButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.saveButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.saveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveButton.Location = new System.Drawing.Point(82, 24);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(50, 50);
            this.saveButton.TabIndex = 3;
            this.saveButton.UseVisualStyleBackColor = false;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // sceneButton
            // 
            this.sceneButton.BackColor = System.Drawing.Color.Transparent;
            this.sceneButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("sceneButton.BackgroundImage")));
            this.sceneButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.sceneButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.sceneButton.FlatAppearance.BorderSize = 0;
            this.sceneButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.sceneButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.sceneButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sceneButton.Location = new System.Drawing.Point(145, 24);
            this.sceneButton.Name = "sceneButton";
            this.sceneButton.Size = new System.Drawing.Size(58, 50);
            this.sceneButton.TabIndex = 4;
            this.sceneButton.UseVisualStyleBackColor = false;
            this.sceneButton.Click += new System.EventHandler(this.sceneButton_Click);
            // 
            // exportButton
            // 
            this.exportButton.BackColor = System.Drawing.Color.Transparent;
            this.exportButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("exportButton.BackgroundImage")));
            this.exportButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.exportButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.exportButton.FlatAppearance.BorderSize = 0;
            this.exportButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.exportButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.exportButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exportButton.Location = new System.Drawing.Point(214, 19);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(55, 55);
            this.exportButton.TabIndex = 5;
            this.exportButton.UseVisualStyleBackColor = false;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.imageList);
            this.panel1.Controls.Add(this.displayList);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.exportButton);
            this.panel1.Controls.Add(this.sceneButton);
            this.panel1.Controls.Add(this.saveButton);
            this.panel1.Controls.Add(this.openButton);
            this.panel1.Cursor = System.Windows.Forms.Cursors.Default;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(289, 1017);
            this.panel1.TabIndex = 7;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Point});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.GridColor = System.Drawing.Color.Black;
            this.dataGridView1.Location = new System.Drawing.Point(21, 735);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(249, 256);
            this.dataGridView1.TabIndex = 21;
            // 
            // Point
            // 
            this.Point.Frozen = true;
            this.Point.HeaderText = "";
            this.Point.Name = "Point";
            this.Point.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Point.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Point.Width = 30;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.label4.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(21, 713);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(249, 19);
            this.label4.TabIndex = 20;
            this.label4.Text = "<Point List>";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.label3.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(21, 565);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(249, 19);
            this.label3.TabIndex = 19;
            this.label3.Text = "<Image List>";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.label2.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(21, 456);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(249, 19);
            this.label2.TabIndex = 18;
            this.label2.Text = "<Display List>";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // imageList
            // 
            this.imageList.CheckOnClick = true;
            this.imageList.ColumnWidth = 80;
            this.imageList.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.imageList.FormattingEnabled = true;
            this.imageList.Location = new System.Drawing.Point(21, 587);
            this.imageList.MultiColumn = true;
            this.imageList.Name = "imageList";
            this.imageList.ScrollAlwaysVisible = true;
            this.imageList.Size = new System.Drawing.Size(249, 99);
            this.imageList.TabIndex = 14;
            this.imageList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.imageList_ItemCheck);
            this.imageList.SelectedIndexChanged += new System.EventHandler(this.imageList_SelectedIndexChanged);
            // 
            // displayList
            // 
            this.displayList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.displayList.CheckOnClick = true;
            this.displayList.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.displayList.FormattingEnabled = true;
            this.displayList.Items.AddRange(new object[] {
            "Vanishing Line",
            "Point",
            "Face",
            "Guide Line",
            "Point Number"});
            this.displayList.Location = new System.Drawing.Point(21, 478);
            this.displayList.MultiColumn = true;
            this.displayList.Name = "displayList";
            this.displayList.Size = new System.Drawing.Size(249, 57);
            this.displayList.TabIndex = 13;
            this.displayList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.displayList_ItemCheck);
            this.displayList.SelectedIndexChanged += new System.EventHandler(this.displayList_SelectedIndexChanged);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.Controls.Add(this.textBox1);
            this.panel4.Location = new System.Drawing.Point(145, 180);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(124, 70);
            this.panel4.TabIndex = 12;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.textBox1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(0, 26);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(59, 20);
            this.textBox1.TabIndex = 12;
            this.textBox1.Text = "adf";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.Controls.Add(this.radioButtonX);
            this.panel3.Controls.Add(this.radioButtonZ);
            this.panel3.Controls.Add(this.radioButtonY);
            this.panel3.Location = new System.Drawing.Point(145, 90);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(124, 84);
            this.panel3.TabIndex = 11;
            // 
            // radioButtonX
            // 
            this.radioButtonX.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonX.BackColor = System.Drawing.Color.White;
            this.radioButtonX.Enabled = false;
            this.radioButtonX.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.radioButtonX.FlatAppearance.BorderSize = 0;
            this.radioButtonX.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.radioButtonX.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.radioButtonX.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.radioButtonX.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioButtonX.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonX.Location = new System.Drawing.Point(0, 17);
            this.radioButtonX.Name = "radioButtonX";
            this.radioButtonX.Size = new System.Drawing.Size(35, 35);
            this.radioButtonX.TabIndex = 10;
            this.radioButtonX.Text = "X";
            this.radioButtonX.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButtonX.UseVisualStyleBackColor = false;
            this.radioButtonX.CheckedChanged += new System.EventHandler(this.radioButtonX_CheckedChanged);
            // 
            // radioButtonZ
            // 
            this.radioButtonZ.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonZ.BackColor = System.Drawing.Color.White;
            this.radioButtonZ.Enabled = false;
            this.radioButtonZ.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.radioButtonZ.FlatAppearance.BorderSize = 0;
            this.radioButtonZ.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.radioButtonZ.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.radioButtonZ.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.radioButtonZ.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioButtonZ.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonZ.Location = new System.Drawing.Point(88, 17);
            this.radioButtonZ.Name = "radioButtonZ";
            this.radioButtonZ.Size = new System.Drawing.Size(35, 35);
            this.radioButtonZ.TabIndex = 9;
            this.radioButtonZ.Text = "Z";
            this.radioButtonZ.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButtonZ.UseVisualStyleBackColor = false;
            this.radioButtonZ.CheckedChanged += new System.EventHandler(this.radioButtonZ_CheckedChanged);
            // 
            // radioButtonY
            // 
            this.radioButtonY.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonY.BackColor = System.Drawing.Color.White;
            this.radioButtonY.Enabled = false;
            this.radioButtonY.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.radioButtonY.FlatAppearance.BorderSize = 0;
            this.radioButtonY.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.radioButtonY.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.radioButtonY.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.radioButtonY.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioButtonY.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonY.Location = new System.Drawing.Point(44, 17);
            this.radioButtonY.Name = "radioButtonY";
            this.radioButtonY.Size = new System.Drawing.Size(35, 35);
            this.radioButtonY.TabIndex = 8;
            this.radioButtonY.Text = "Y";
            this.radioButtonY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButtonY.UseVisualStyleBackColor = false;
            this.radioButtonY.CheckedChanged += new System.EventHandler(this.radioButtonY_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.selectButton);
            this.panel2.Controls.Add(this.guideLineButton);
            this.panel2.Controls.Add(this.faceButton);
            this.panel2.Controls.Add(this.pointButton);
            this.panel2.Controls.Add(this.vanishingLineButton);
            this.panel2.Location = new System.Drawing.Point(21, 90);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(248, 352);
            this.panel2.TabIndex = 10;
            // 
            // selectButton
            // 
            this.selectButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.selectButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(195)))), ((int)(((byte)(195)))));
            this.selectButton.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.selectButton.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.selectButton.FlatAppearance.BorderSize = 0;
            this.selectButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.selectButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.selectButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.selectButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.selectButton.Font = new System.Drawing.Font("Century Gothic", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectButton.Location = new System.Drawing.Point(0, 270);
            this.selectButton.Name = "selectButton";
            this.selectButton.Size = new System.Drawing.Size(113, 70);
            this.selectButton.TabIndex = 4;
            this.selectButton.TabStop = true;
            this.selectButton.Text = "Select";
            this.selectButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.selectButton.UseVisualStyleBackColor = false;
            this.selectButton.CheckedChanged += new System.EventHandler(this.selectButton_CheckedChanged);
            // 
            // guideLineButton
            // 
            this.guideLineButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.guideLineButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(195)))), ((int)(((byte)(195)))));
            this.guideLineButton.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.guideLineButton.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.guideLineButton.FlatAppearance.BorderSize = 0;
            this.guideLineButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.guideLineButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.guideLineButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.guideLineButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.guideLineButton.Font = new System.Drawing.Font("Century Gothic", 13F, System.Drawing.FontStyle.Bold);
            this.guideLineButton.Location = new System.Drawing.Point(135, 180);
            this.guideLineButton.Name = "guideLineButton";
            this.guideLineButton.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.guideLineButton.Size = new System.Drawing.Size(113, 70);
            this.guideLineButton.TabIndex = 3;
            this.guideLineButton.TabStop = true;
            this.guideLineButton.Text = "Guide Line";
            this.guideLineButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.guideLineButton.UseVisualStyleBackColor = false;
            this.guideLineButton.CheckedChanged += new System.EventHandler(this.guideLineButton_CheckedChanged);
            // 
            // faceButton
            // 
            this.faceButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.faceButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(195)))), ((int)(((byte)(195)))));
            this.faceButton.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.faceButton.FlatAppearance.BorderSize = 0;
            this.faceButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.faceButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.faceButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.faceButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.faceButton.Font = new System.Drawing.Font("Century Gothic", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.faceButton.Location = new System.Drawing.Point(0, 180);
            this.faceButton.Name = "faceButton";
            this.faceButton.Size = new System.Drawing.Size(113, 70);
            this.faceButton.TabIndex = 2;
            this.faceButton.TabStop = true;
            this.faceButton.Text = "Face";
            this.faceButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.faceButton.UseVisualStyleBackColor = false;
            this.faceButton.CheckedChanged += new System.EventHandler(this.faceButton_CheckedChanged);
            // 
            // pointButton
            // 
            this.pointButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.pointButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(195)))), ((int)(((byte)(195)))));
            this.pointButton.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.pointButton.FlatAppearance.BorderSize = 0;
            this.pointButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.pointButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.pointButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.pointButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pointButton.Font = new System.Drawing.Font("Century Gothic", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pointButton.Location = new System.Drawing.Point(0, 90);
            this.pointButton.Name = "pointButton";
            this.pointButton.Size = new System.Drawing.Size(113, 70);
            this.pointButton.TabIndex = 1;
            this.pointButton.TabStop = true;
            this.pointButton.Text = "Point";
            this.pointButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.pointButton.UseVisualStyleBackColor = false;
            this.pointButton.CheckedChanged += new System.EventHandler(this.pointButton_CheckedChanged);
            // 
            // vanishingLineButton
            // 
            this.vanishingLineButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.vanishingLineButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(195)))), ((int)(((byte)(195)))));
            this.vanishingLineButton.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.vanishingLineButton.FlatAppearance.BorderSize = 0;
            this.vanishingLineButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.vanishingLineButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.vanishingLineButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.vanishingLineButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.vanishingLineButton.Font = new System.Drawing.Font("Century Gothic", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vanishingLineButton.Location = new System.Drawing.Point(0, 0);
            this.vanishingLineButton.Name = "vanishingLineButton";
            this.vanishingLineButton.Size = new System.Drawing.Size(113, 70);
            this.vanishingLineButton.TabIndex = 0;
            this.vanishingLineButton.TabStop = true;
            this.vanishingLineButton.Text = "Vanishing Line";
            this.vanishingLineButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.vanishingLineButton.UseVisualStyleBackColor = false;
            this.vanishingLineButton.CheckedChanged += new System.EventHandler(this.vanishingLineButton_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.ClientSize = new System.Drawing.Size(1920, 1017);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "UpDimension";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button openButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button sceneButton;
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton vanishingLineButton;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton faceButton;
        private System.Windows.Forms.RadioButton pointButton;
        private System.Windows.Forms.RadioButton radioButtonZ;
        private System.Windows.Forms.RadioButton radioButtonY;
        private System.Windows.Forms.RadioButton radioButtonX;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.CheckedListBox displayList;
        private System.Windows.Forms.RadioButton selectButton;
        private System.Windows.Forms.CheckedListBox imageList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton guideLineButton;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Point;
    }
}

