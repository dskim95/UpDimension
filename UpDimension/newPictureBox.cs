using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace UpDimension
{
    delegate void EventHandler(int n);

    class newPictureBox : PictureBox
    {
        Scene scene;
        int pointnumber;

        public event EventHandler displayPointNumber;

        public int pointNumber
        {
            get
            {
                return pointnumber;
            }
            set
            {
                displayPointNumber(value);
                pointnumber = value;
            }
        }

        // 오른쪽 마우스로 점의 위치를 변경 중
        bool pointMove;
        // 선을 그리는 경우 (오른쪽 마우스로 선 위치 변경도 포함)
        bool lineDraw;
        // 선택 영역 지정하는 경우
        bool selectDrag;

        // 점을 그릴 때 마우스의 위치
        PointF mousePoint;
        // 그려지고 있는 선과 면, 선의 경우 image기준임
        Line tempLine;
        Face tempFace;

        // 오른쪽 마우스로 바꾸는 도중 다른 도구로 전환되는 경우를 위해
        ISave saved;

        // 선택 영역
        PointF selectPoint1;
        PointF selectPoint2;
        RectangleF selectArea;

        // 하이라이트 되거나 선택 된 오브젝트들
        List<IHighlight> highlight;
        List<ISelect> select;

        // mouseDown없이 mouseUp되는 경우를 막기 위해
        bool mouseDownCapture;

        public newPictureBox(Scene scene)
        {
            this.scene = scene;
        }

        private void initData()
        {
            pointNumber = scene.maxPointNumber() + 1;

            pointMove = false;
            lineDraw = false;
            selectDrag = false;

            mousePoint = new PointF(-1, -1);
            tempLine = null;
            tempFace = null;

            saved = null;

            selectPoint1 = new PointF(-1, -1);
            selectPoint2 = new PointF(-1, -1);
            selectArea = new RectangleF();

            highlight = new List<IHighlight>;
            select = new List<ISelect>;
        }

        private void InitComponents()
        {
            pointNumber = 0;
            currentSceneNumber = -1;
            currentScene = null;
            highlightPoint = new PointF(-1, -1);
            highlightLinePoint = false;
            mousePoint = new PointF(-1, -1);
            highlightFace = null;

            lineDraw = false;
            tempLine = new Line();

            highlightPointNumber = -1;
            highlightLineList = null;
            select = new List<ISelect>();
            selectDrag = false;
            pointMove = false;
            selectPoint1 = new PointF(-1, -1);
            selectPoint2 = new PointF(-1, -1);
            selectArea = new RectangleF();

            savedVertex = null;
            tempFace = null;

            mouseDownCapture = false;
        }

        private void changePictureBoxImage(Bitmap newImage)
        {
            Image oldImage = pictureBox1.Image;
            pictureBox1.Image = newImage;

            if (oldImage != null)
                oldImage.Dispose();
        }

        private void sceneButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                model.scene.Add(new Scene(openFileDialog1.FileName, this.pictureBox1.Size, model.face));

                imageList.Items.Add("Img" + (model.scene.Count - 1).ToString());
                imageList.SetItemChecked(model.scene.Count - 1, true);

                dataGridView1.Columns.Add("Img" + (model.scene.Count - 1).ToString(), "Img" + (model.scene.Count - 1).ToString());
                dataGridView1.Columns[dataGridView1.Columns.Count - 1].Width = 50;

                if (model.scene.Count == 1)
                {
                    displayList.SetItemChecked(0, true);
                    displayList.SetItemChecked(1, true);
                    displayList.SetItemChecked(2, true);
                    displayList.SetItemChecked(3, true);
                    displayList.SetItemChecked(4, true);
                }
            }
        }

        private void sceneChange(int index)
        {
            currentScene = model.scene[index];
            currentSceneNumber = index;

            currentScene.displayVanishingLine = displayList.GetItemChecked(0);
            currentScene.displayPoint = displayList.GetItemChecked(1);
            currentScene.displayFace = displayList.GetItemChecked(2);
            currentScene.displayGuideLine = displayList.GetItemChecked(3);
            currentScene.displayPointNumber = displayList.GetItemChecked(4);

            pointNumber = currentScene.maxPointNumber() + 1;
            highlightPoint = new PointF(-1, -1);
            highlightLinePoint = false;
            mousePoint = new PointF(-1, -1);
            highlightFace = null;

            lineDraw = false;
            tempLine = new Line();

            highlightPointNumber = -1;
            highlightLineList = null;
            select = new List<ISelect>();
            selectDrag = false;
            pointMove = false;
            selectPoint1 = new PointF(-1, -1);
            selectPoint2 = new PointF(-1, -1);
            selectArea = new RectangleF();

            savedVertex = null;
            tempFace = null;

            changePictureBoxImage(currentScene.display(new Point(0, 0)));
        }

        private void vanishingLineButton_CheckedChanged(object sender, EventArgs e)
        {
            if (this.vanishingLineButton.Checked)
            {
                this.vanishingLineButton.FlatAppearance.BorderSize = 5;
                this.radioButtonX.Enabled = true;
                this.radioButtonY.Enabled = true;
                this.radioButtonZ.Enabled = true;
                this.radioButtonX.Checked = true;
                this.radioButtonY.Checked = false;
                this.radioButtonZ.Checked = false;
                this.pictureBox1.Cursor = Cursors.Cross;
                this.displayList.SetItemChecked(0, true);
                currentScene.displayVanishingLine = true;
                changePictureBoxImage(currentScene.display());
            }
            else
            {
                this.radioButtonX.Enabled = false;
                this.radioButtonY.Enabled = false;
                this.radioButtonZ.Enabled = false;
                this.radioButtonX.Checked = false;
                this.radioButtonY.Checked = false;
                this.radioButtonZ.Checked = false;
                lineDraw = false;
                this.vanishingLineButton.FlatAppearance.BorderSize = 0;
                this.pictureBox1.Cursor = Cursors.Default;
                mouseDownCapture = false;
            }
        }

        private void radioButtonX_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonX.Checked)
                this.radioButtonX.FlatAppearance.BorderSize = 3;
            else
                this.radioButtonX.FlatAppearance.BorderSize = 0;
        }

        private void radioButtonY_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonY.Checked)
                this.radioButtonY.FlatAppearance.BorderSize = 3;
            else
                this.radioButtonY.FlatAppearance.BorderSize = 0;

        }

        private void radioButtonZ_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonZ.Checked)
                this.radioButtonZ.FlatAppearance.BorderSize = 3;
            else
                this.radioButtonZ.FlatAppearance.BorderSize = 0;
        }

        private void pointButton_CheckedChanged(object sender, EventArgs e)
        {
            if (this.pointButton.Checked)
            {
                this.pointButton.FlatAppearance.BorderSize = 5;
                this.displayList.SetItemChecked(1, true);
                mousePoint = new PointF(-1, -1);
                currentScene.displayPoint = true;
                changePictureBoxImage(currentScene.display());
            }
            else
            {
                this.pointButton.FlatAppearance.BorderSize = 0;
                if (pointMove)
                {
                    currentScene.addVertex(savedVertex.coord, savedVertex.number);
                    pointMove = false;
                }
                mouseDownCapture = false;
            }
        }

        private void faceButton_CheckedChanged(object sender, EventArgs e)
        {
            if (this.faceButton.Checked)
            {
                this.faceButton.FlatAppearance.BorderSize = 5;
                this.displayList.SetItemChecked(2, true);
                currentScene.displayFace = true;
                changePictureBoxImage(currentScene.display());
            }
            else
            {
                tempFace = null;
                this.faceButton.FlatAppearance.BorderSize = 0;
                mouseDownCapture = false;
                pictureBox1.Refresh();
            }
        }

        private void guideLineButton_CheckedChanged(object sender, EventArgs e)
        {
            if (guideLineButton.Checked)
            {
                this.guideLineButton.FlatAppearance.BorderSize = 5;
                this.displayList.SetItemChecked(3, true);
                currentScene.displayPoint = true;
                changePictureBoxImage(currentScene.display());
                this.pictureBox1.Cursor = Cursors.Cross;
            }
            else
            {
                this.guideLineButton.FlatAppearance.BorderSize = 0;
                this.pictureBox1.Cursor = Cursors.Default;
                lineDraw = false;
                if (savedLine != null)
                    currentScene.guideLine.Add(savedLine);
                mouseDownCapture = false;
            }
        }

        private void selectButton_CheckedChanged(object sender, EventArgs e)
        {
            if (selectButton.Checked)
            {
                this.selectButton.FlatAppearance.BorderSize = 5;
                this.displayList.SetItemChecked(3, true);
                currentScene.displayPoint = displayList.GetItemChecked(2);
            }
            else
            {
                select = new List<ISelect>();
                selectDrag = false;
                this.selectButton.FlatAppearance.BorderSize = 0;
                mouseDownCapture = false;
            }
        }

        private void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (currentScene != null)
            {
                PointF mousePosition = currentScene.picture2Image(e.Location);
                bool changed;

                if (e.Delta > 0)
                    changed = currentScene.increaseMagnification();
                else
                    changed = currentScene.decreaseMagnification();

                if (changed)
                {
                    changePictureBoxImage(currentScene.display(mousePosition));

                    pictureBox1_MouseMove(sender, e);
                }
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDownCapture = true;
            if (this.vanishingLineButton.Checked)
            {
                tempLine.P1 = currentScene.picture2Image(e.Location);
                tempLine.P2 = currentScene.picture2Image(e.Location);
                lineDraw = true;
            }
            else if (this.guideLineButton.Checked)
            {
                if (highlightPoint.X != -1 && e.Button == MouseButtons.Right)
                {
                    if (highlightLine.P1.equal(highlightPoint) || highlightLine.P2.equal(highlightPoint))
                    {
                        if (highlightLine.P1.equal(highlightPoint))
                            tempLine.P1 = highlightLine.P2;
                        else if (highlightLine.P2.equal(highlightPoint))
                            tempLine.P1 = highlightLine.P1;
                        tempLine.P2 = currentScene.picture2Image(e.Location);
                        savedLine = highlightLine;
                        lineDraw = true;
                        currentScene.removeGuideLine(highlightLine);
                        highlightPoint = new PointF(-1, -1);
                        highlightLine = null;
                        changePictureBoxImage(currentScene.display());
                    }
                }
                else if (e.Button == MouseButtons.Left)
                {
                    // highlightPoint가 있으면 그 점을 선의 시작점으로
                    if (highlightPoint.X != -1)
                    {
                        tempLine.P1 = highlightPoint;
                        tempLine.P2 = highlightPoint;
                    }
                    // highlightLine이 있으면 현재 위치에서 line에 내린 수선의 발을 시작점으로
                    else if (highlightLine != null)
                    {
                        tempLine.P1 = highlightLine.projection(currentScene.picture2Image(e.Location));
                        tempLine.P2 = highlightLine.projection(currentScene.picture2Image(e.Location));
                    }
                    else
                    {
                        tempLine.P1 = currentScene.picture2Image(e.Location);
                        tempLine.P2 = currentScene.picture2Image(e.Location);
                    }
                    lineDraw = true;
                }
            }
            else if (this.pointButton.Checked && e.Button == MouseButtons.Right)
            {
                PointF currentLocation = currentScene.picture2Image(e.Location);
                Vertex2D closestVertex = new Vertex2D();
                float min = currentScene.threshold;

                if (Scene.closestPoint(currentLocation, currentScene.vertexList, ref min, ref closestVertex))
                {
                    currentScene.removeVertex(closestVertex.number);
                    removeDataGridView(closestVertex.number, currentSceneNumber);
                    savedVertex = closestVertex;
                    pointMove = true;
                }

                changePictureBoxImage(currentScene.display());
            }
            // highlightLine이나 Point가 있다면 drag로 선택영역을 지정할 수 없음
            else if (selectButton.Checked)
            {
                select = new List<ISelect>();
                if (highlightLine != null)
                {
                    select.Add(new SelectedObject(highlightLine, highlightLineList));
                }
                else if (highlightPoint.X != -1)
                {
                    select.Add(new SelectedObject(highlightPoint, highlightPointNumber));
                }
                else if (highlightFace != null)
                {
                    select.Add(new SelectedObject(highlightFace));
                }
                else
                {
                    selectDrag = true;
                    selectPoint1 = currentScene.picture2Image(e.Location);
                    selectPoint2 = currentScene.picture2Image(e.Location);
                }
                pictureBox1.Refresh();
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (vanishingLineButton.Checked && lineDraw)
            {
                tempLine.P2 = currentScene.picture2Image(e.Location);

                this.pictureBox1.Invalidate();
            }
            else if (guideLineButton.Checked)
            {
                PointF currentLocation = currentScene.picture2Image(e.Location);
                Vertex2D closestVertex = new Vertex2D();
                Line closestLine = new Line();
                float min = currentScene.threshold;
                bool find = false;

                highlightLinePoint = false;
                if (Scene.closestPoint(currentLocation, currentScene.vertexList, ref min, ref closestVertex, ref find))
                    highlightLinePoint = false;
                if (Scene.closestLinePoint(currentLocation, currentScene.guideLine, ref min, ref closestVertex.coord, ref find))
                    highlightLinePoint = true;
                min = currentScene.threshold;
                Scene.closestLine(currentLocation, currentScene.guideLine, ref min, ref closestLine, ref find);

                if (find)
                {
                    // closestLinePoint만 찾은 경우 -> closestLine은 무조건 찾음
                    if (highlightLinePoint)
                    {
                        highlightLine = closestLine;
                        highlightPoint = closestVertex.coord;
                    }
                    // closestPoint를 찾은 경우
                    else if (closestVertex.coord.X != -1)
                    {
                        highlightLine = null;
                        highlightPoint = closestVertex.coord;
                    }
                    // closestLine을 찾은 경우
                    else
                    {
                        highlightLine = closestLine;
                        highlightPoint = new PointF(-1, -1);
                    }
                }
                // 전혀 못 찾은 경우
                else
                {
                    highlightLine = null;
                    highlightPoint = new PointF(-1, -1);
                }

                if (lineDraw)
                {
                    if (highlightPoint.X != -1)
                        tempLine.P2 = highlightPoint;
                    else if (highlightLine != null)
                        tempLine.P2 = highlightLine.projection(currentLocation);
                    else
                        tempLine.P2 = currentLocation;
                }

                pictureBox1.Invalidate();
            }
            else if (pointButton.Checked)
            {
                PointF currentLocation = currentScene.picture2Image(e.Location);
                Vertex2D closestVertex = new Vertex2D();
                Line closestLine = new Line();
                float min = currentScene.threshold;
                bool find = false;

                highlightLinePoint = false;
                if (displayList.GetItemChecked(3))
                    if (Scene.closestLinePoint(currentLocation, currentScene.guideLine, ref min, ref closestVertex.coord, ref find))
                        highlightLinePoint = true;
                if (Scene.closestPoint(currentLocation, currentScene.vertexList, ref min, ref closestVertex, ref find))
                    highlightLinePoint = false;
                if (displayList.GetItemChecked(3))
                {
                    min = currentScene.threshold;
                    Scene.closestLine(currentLocation, currentScene.guideLine, ref min, ref closestLine, ref find);
                }

                if (find)
                {
                    // closestLinePoint만 찾은 경우 -> closestLine은 무조건 찾음
                    if (highlightLinePoint)
                    {
                        mousePoint = closestVertex.coord;
                        highlightLine = null;
                        highlightPoint = new PointF(-1, -1);
                    }
                    // closestPoint를 찾은 경우
                    else if (closestVertex.coord.X != -1)
                    {
                        mousePoint = currentLocation;
                        highlightLine = null;
                        highlightPoint = closestVertex.coord;
                    }
                    // closestLine을 찾은 경우
                    else
                    {
                        mousePoint = closestLine.projection(currentLocation);
                        highlightLine = null;
                        highlightPoint = new PointF(-1, -1);
                    }
                }
                // 전혀 못 찾은 경우
                else
                {
                    mousePoint = currentLocation;
                    highlightLine = null;
                    highlightPoint = new PointF(-1, -1);
                }

                pictureBox1.Invalidate();
            }
            else if (faceButton.Checked)
            {
                PointF currentLocation = currentScene.picture2Image(e.Location);
                Vertex2D closestVertex = new Vertex2D();
                float min = currentScene.threshold;

                if (Scene.closestPoint(currentLocation, currentScene.vertexList, ref min, ref closestVertex))
                    highlightPoint = closestVertex.coord;
                else
                    highlightPoint = new PointF(-1, -1);

                pictureBox1.Invalidate();
            }
            else if (selectButton.Checked)
            {
                if (selectDrag)
                {
                    selectPoint2 = currentScene.picture2Image(e.Location);
                    pictureBox1.Invalidate();
                }
                else
                {
                    float min = currentScene.threshold;
                    Line closestLine = new Line();
                    Vertex2D closestVertex = new Vertex2D();
                    Face closestFace = new Face();
                    bool line = false;
                    bool point = false;
                    bool face = false;
                    bool faceLine = false;
                    PointF currentLocation = currentScene.picture2Image(e.Location);

                    // vertex중에 가까운 것 찾음
                    if (displayList.GetItemChecked(1))
                    {
                        if (Scene.closestPoint(currentLocation, currentScene.vertexList, ref min, ref closestVertex, ref point))
                            highlightPointNumber = closestVertex.number;
                    }
                    // face중에 가까운 것 찾음
                    if (displayList.GetItemChecked(2))
                    {
                        face = currentScene.closestFace(currentLocation, ref closestFace);
                        faceLine = currentScene.closestFaceLine(currentLocation, ref min, ref closestFace);
                    }
                    // guideLine중에 가까운 것 찾음
                    if (displayList.GetItemChecked(3))
                    {
                        if (Scene.closestLine(currentLocation, currentScene.guideLine, ref min, ref closestLine, ref line))
                            highlightLineList = currentScene.guideLine;
                    }
                    // vanishingLine중에 가까운 것 찾음
                    if (displayList.GetItemChecked(0))
                    {
                        if (Scene.closestLine(currentLocation, currentScene.vanishingLineX, ref min, ref closestLine, ref line))
                            highlightLineList = currentScene.vanishingLineX;
                        if (Scene.closestLine(currentLocation, currentScene.vanishingLineY, ref min, ref closestLine, ref line))
                            highlightLineList = currentScene.vanishingLineY;
                        if (Scene.closestLine(currentLocation, currentScene.vanishingLineZ, ref min, ref closestLine, ref line))
                            highlightLineList = currentScene.vanishingLineZ;
                    }

                    // 가까우면 highlightLine이나 highlightPoint로 지정
                    if (point)
                    {
                        highlightPoint = closestVertex.coord;
                        highlightLine = null;
                        highlightFace = null;
                    }
                    else if (faceLine)
                    {
                        highlightPoint = new PointF(-1, -1);
                        highlightLine = null;
                        highlightFace = closestFace;
                    }
                    else if (line)
                    {
                        highlightLine = closestLine;
                        highlightPoint = new PointF(-1, -1);
                        highlightFace = null;
                    }
                    else if (face)
                    {
                        highlightPoint = new PointF(-1, -1);
                        highlightLine = null;
                        highlightFace = closestFace;
                    }
                    else
                    {
                        highlightLine = null;
                        highlightPoint = new PointF(-1, -1);
                        highlightFace = null;
                    }

                    pictureBox1.Invalidate();
                }
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (!mouseDownCapture)
                return;
            else
                mouseDownCapture = false;

            // 현재 위치를 끝점으로 하는 선을 그림
            if ((vanishingLineButton.Checked || guideLineButton.Checked) && lineDraw)
            {
                if (radioButtonX.Checked)
                    currentScene.addVanishingLine(tempLine, Scene.X);
                else if (radioButtonY.Checked)
                    currentScene.addVanishingLine(tempLine, Scene.Y);
                else if (radioButtonZ.Checked)
                    currentScene.addVanishingLine(tempLine, Scene.Z);
                else
                    currentScene.addGuideLine(tempLine);
                lineDraw = false;
                savedLine = null;

                changePictureBoxImage(currentScene.display());
            }
            else if (pointButton.Checked)
            {
                if (e.Location.X < 0 || e.Location.X > pictureBox1.Size.Width || e.Location.Y < 0 || e.Location.Y > pictureBox1.Size.Height || mousePoint.X == -1)
                    return;
                if (e.Button == MouseButtons.Left)
                {
                    currentScene.addVertex(mousePoint, pointNumber);
                    addDataGridView(pointNumber, currentSceneNumber);
                    pointNumber = currentScene.firstEmptyPointNumber(pointNumber + 1);
                    changePictureBoxImage(currentScene.display());
                }
                else if (e.Button == MouseButtons.Right && pointMove)
                {
                    currentScene.addVertex(mousePoint, savedVertex.number);
                    changePictureBoxImage(currentScene.display());
                    pointMove = false;
                }
            }
            else if (faceButton.Checked && e.Button == MouseButtons.Left)
            {
                PointF currentLocation = currentScene.picture2Image(e.Location);
                Vertex2D closestVertex = new Vertex2D();
                float min = currentScene.threshold;

                if (Scene.closestPoint(currentLocation, currentScene.vertexList, ref min, ref closestVertex))
                {
                    if (tempFace == null)
                        tempFace = new Face();
                    tempFace.vertexList.Add(closestVertex.number);
                }

                pictureBox1.Invalidate();
            }
            else if (faceButton.Checked && e.Button == MouseButtons.Right)
            {
                if (tempFace != null && tempFace.Count >= 2)
                {
                    model.face.Add(tempFace);
                    changePictureBoxImage(currentScene.display());
                }
                tempFace = null;
            }
            else if (selectButton.Checked)
            {
                // 선택 영역 안에 있는 line, point들을 고름
                if (selectDrag)
                {
                    PointF location = new PointF(Math.Min(selectPoint1.X, selectPoint2.X), Math.Min(selectPoint1.Y, selectPoint2.Y));
                    SizeF size = new SizeF(Math.Abs(selectPoint1.X - selectPoint2.X), Math.Abs(selectPoint1.Y - selectPoint2.Y));
                    selectArea = new RectangleF(location, size);

                    if (currentScene.displayVanishingLine)
                    {
                        foreach (Line item in currentScene.vanishingLineX)
                            if (selectArea.isIntersect(item))
                                select.Add(new SelectedObject(item, currentScene.vanishingLineX));
                        foreach (Line item in currentScene.vanishingLineY)
                            if (selectArea.isIntersect(item))
                                select.Add(new SelectedObject(item, currentScene.vanishingLineY));
                        foreach (Line item in currentScene.vanishingLineZ)
                            if (selectArea.isIntersect(item))
                                select.Add(new SelectedObject(item, currentScene.vanishingLineZ));
                    }
                    if (currentScene.displayPoint)
                    {
                        foreach (Vertex2D item in currentScene.vertexList)
                            if (selectArea.intersect(item.coord))
                                select.Add(new SelectedObject(item.coord, item.number));
                    }
                    if (currentScene.displayGuideLine)
                    {
                        foreach (Line item in currentScene.guideLine)
                            if (selectArea.isIntersect(item))
                                select.Add(new SelectedObject(item, currentScene.guideLine));
                    }
                    if (currentScene.displayFace)
                    {
                        foreach (Face item in currentScene.faceList)
                        {
                            bool intersect = false;
                            if (currentScene.isFaceIncluded(item))
                            {
                                PointF[] points = currentScene.face2PointArray(item);

                                for (int i = 0; i < item.Count; i++)
                                {
                                    if (selectArea.intersect(points[i]))
                                        intersect = true;
                                    Line line;
                                    if (i == item.Count - 1)
                                        line = new Line(points[i], points[0]);
                                    else
                                        line = new Line(points[i], points[i + 1]);
                                    if (selectArea.isIntersect(line))
                                        intersect = true;
                                    if (intersect)
                                    {
                                        select.Add(new SelectedObject(item));
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    selectDrag = false;
                    pictureBox1.Invalidate();
                }
            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Line bufferedPictureLine = new Line();
            if (currentScene != null)
                bufferedPictureLine = currentScene.imageLine2PictureLine(tempLine);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            // bufferedPictureLine을 그림
            if (this.vanishingLineButton.Checked && lineDraw)
            {
                if (this.radioButtonX.Checked)
                    new vLine(tempLine, vLine.Xaxis).draw(e.Graphics, currentScene.currentImage, currentScene.image2Picture);
                else if (this.radioButtonY.Checked)
                    new vLine(tempLine, vLine.Yaxis).draw(e.Graphics, currentScene.currentImage, currentScene.image2Picture);
                else if (this.radioButtonZ.Checked)
                    new vLine(tempLine, vLine.Zaxis).draw(e.Graphics, currentScene.currentImage, currentScene.image2Picture);
            }
            // highlightPoint가 있다면 그림
            else if (this.pointButton.Checked)
            {
                if (mousePoint.X != -1)
                {
                    PointF point = currentScene.image2Picture(mousePoint);
                    e.Graphics.DrawEllipse(new Pen(Color.Red, 1), point.X - 3F, point.Y - 3F, 6F, 6F);
                }
                if (highlightPoint.X != -1)
                {
                    PointF point = currentScene.image2Picture(highlightPoint);
                    e.Graphics.FillEllipse(new SolidBrush(Color.White), point.X - 3F, point.Y - 3F, 6F, 6F);
                }
            }
            else if (faceButton.Checked)
            {
                if (highlightPoint.X != -1)
                {
                    PointF point = currentScene.image2Picture(highlightPoint);
                    e.Graphics.FillEllipse(new SolidBrush(Color.White), point.X - 3F, point.Y - 3F, 6F, 6F);
                }

                if (tempFace != null && tempFace.Count >= 2)
                {
                    foreach (int item in tempFace.vertexList)
                    {
                        PointF point = currentScene.image2Picture(currentScene.vertexList[item].coord);
                        e.Graphics.FillEllipse(new SolidBrush(Color.White), point.X - 3F, point.Y - 3F, 6F, 6F);
                    }
                    e.Graphics.FillPolygon(new SolidBrush(Color.FromArgb(128, 192, 192, 192)), currentScene.face2PointArray(tempFace));
                    e.Graphics.DrawPolygon(new Pen(Brushes.Black, 2), currentScene.face2PointArray(tempFace));
                }
            }
            // highlightLine이나 highlightPoint가 있다면 그리고, 현재 선도 그림.
            else if (this.guideLineButton.Checked)
            {
                if (lineDraw)
                    e.Graphics.DrawLine(Pens.HotPink, bufferedPictureLine.P1, bufferedPictureLine.P2);
                if (highlightLine != null)
                    e.Graphics.DrawLine(new Pen(Color.White, 1), currentScene.image2Picture(highlightLine.P1), currentScene.image2Picture(highlightLine.P2));
                if (highlightPoint.X != -1)
                {
                    PointF point = currentScene.image2Picture(highlightPoint);
                    if (highlightLinePoint)
                    {
                        e.Graphics.FillRectangle(new SolidBrush(Color.White), point.X - 4F, point.Y - 4F, 8F, 8F);
                        e.Graphics.DrawRectangle(new Pen(Color.Black, 1), point.X - 4F, point.Y - 4F, 8F, 8F);
                    }
                    else
                    {
                        e.Graphics.FillEllipse(new SolidBrush(Color.White), point.X - 3F, point.Y - 3F, 6F, 6F);
                    }
                }
            }
            // 가까이 있는 highightLine이나 highlightPoint를 표시
            else if (this.selectButton.Checked)
            {
                if (selectDrag)
                {
                    PointF point1 = currentScene.image2Picture(selectPoint1);
                    PointF point2 = currentScene.image2Picture(selectPoint2);
                    PointF location = new PointF(Math.Min(point1.X, point2.X), Math.Min(point1.Y, point2.Y));
                    SizeF size = new SizeF(Math.Abs(point1.X - point2.X), Math.Abs(point1.Y - point2.Y));

                    Pen pen = new Pen(Color.Black, 1);
                    pen.DashPattern = new float[] { 2, 2, 2, 2 };
                    e.Graphics.DrawRectangle(pen, location.X, location.Y, size.Width, size.Height);
                }
                else
                {
                    if (highlightLine != null)
                    {
                        if (highlightLine.GetType() == typeof(Line))
                            highlightLine.draw(e.Graphics, new Pen(Color.White, 1), currentScene.image2Picture);
                        else if (highlightLine.GetType() == typeof(vLine))
                            ((vLine)highlightLine).draw(e.Graphics, new Pen(Color.White, 1), currentScene.currentImage, currentScene.image2Picture);
                    }
                    else if (highlightPoint.X != -1)
                    {
                        PointF point = currentScene.image2Picture(highlightPoint);
                        e.Graphics.FillEllipse(new SolidBrush(Color.White), point.X - 3F, point.Y - 3F, 6F, 6F);
                    }
                    else if (highlightFace != null)
                    {
                        e.Graphics.FillPolygon(new SolidBrush(Color.FromArgb(128, 255, 255, 255)), currentScene.face2PointArray(highlightFace));
                        e.Graphics.DrawPolygon(new Pen(Brushes.White, 1), currentScene.face2PointArray(highlightFace));
                    }
                    foreach (SelectedObject item in select)
                    {
                        if (item.point.X != -1)
                        {
                            PointF point = currentScene.image2Picture(item.point);
                            e.Graphics.FillEllipse(new SolidBrush(Color.White), point.X - 3F, point.Y - 3F, 6F, 6F);
                        }
                        else if (item.line != null)
                        {
                            Line line = currentScene.imageLine2PictureLine(item.line);
                            e.Graphics.DrawLine(new Pen(Color.White, 1), line.P1, line.P2);
                        }
                        else if (item.face != null)
                        {
                            e.Graphics.FillPolygon(new SolidBrush(Color.FromArgb(128, 255, 255, 255)), currentScene.face2PointArray(item.face));
                            e.Graphics.DrawPolygon(new Pen(Brushes.White, 1), currentScene.face2PointArray(item.face));
                        }
                    }
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (currentScene != null)
            {
                if (!lineDraw)
                {
                    if (e.KeyCode == Keys.Z && e.Modifiers == Keys.Control)
                    {
                        //currentScene.undo();
                        //changePictureBoxImage(currentScene.display());
                    }
                }
                if (e.KeyCode == Keys.V)
                {
                    if (!vanishingLineButton.Checked)
                        vanishingLineButton.Checked = true;
                    else
                    {
                        if (radioButtonX.Checked)
                            radioButtonY.Checked = true;
                        else if (radioButtonY.Checked)
                            radioButtonZ.Checked = true;
                        else if (radioButtonZ.Checked)
                            radioButtonX.Checked = true;
                    }
                }
                else if (e.KeyCode == Keys.X)
                {
                    if (vanishingLineButton.Checked)
                        radioButtonX.Checked = true;
                }
                else if (e.KeyCode == Keys.Y)
                {
                    if (vanishingLineButton.Checked)
                        radioButtonY.Checked = true;
                }
                else if (e.KeyCode == Keys.Z)
                {
                    if (vanishingLineButton.Checked)
                        radioButtonZ.Checked = true;
                }
                else if (e.KeyCode == Keys.P)
                {
                    if (!pointButton.Checked)
                        pointButton.Checked = true;
                }
                else if (e.KeyCode == Keys.F)
                {
                    if (!faceButton.Checked)
                        faceButton.Checked = true;
                }
                else if (e.KeyCode == Keys.G)
                {
                    if (!guideLineButton.Checked)
                        guideLineButton.Checked = true;
                }
                else if (e.KeyCode == Keys.S)
                {
                    if (!selectButton.Checked)
                        selectButton.Checked = true;
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    foreach (SelectedObject item in select)
                    {
                        if (item.line != null)
                            item.lineList.Remove(item.line);
                        else if (item.point.X != -1)
                        {
                            currentScene.removeVertex(item.pointNumber);
                            removeDataGridView(item.pointNumber, currentSceneNumber);
                        }
                        else if (item.face != null)
                            model.face.Remove(item.face);
                    }
                    select = new List<SelectedObject>();
                    changePictureBoxImage(currentScene.display());
                    highlightLine = null;
                    highlightPoint = new PointF(-1, -1);
                    highlightFace = null;
                }
            }
        }

        private void displayList_SelectedIndexChanged(object sender, EventArgs e)
        {
            displayList.ClearSelected();
        }

        // 점을 찍는 경우에는 mouse가 pictureBox 밖으로 나가면, mousePoint 없애야 함.
        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            if (pointButton.Checked)
                mousePoint = new PointF(-1, -1);
            pictureBox1.Invalidate();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(Color.White), 145, 200, 59, 30);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (textBox1.Text != "")
                {
                    HideCaret();
                    textBox1.SelectionStart = 0;
                    textBox1.SelectionLength = 0;

                    pointNumber = currentScene.firstEmptyPointNumber(int.Parse(textBox1.Text));
                    e.Handled = true;
                }
            }
            else if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }

        [DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);
        public void HideCaret()
        {
            HideCaret(textBox1.Handle);
        }

        void addDataGridView(int row, int column)
        {
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;

            for (int i = dataGridView1.Rows.Count; i <= row; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = i;
            }

            dataGridView1.Rows[row].Cells[column + 1].Value = "O";
        }

        void removeDataGridView(int row, int column)
        {
            dataGridView1.Rows[row].Cells[column + 1].Value = "";
        }

        private void imageList_SelectedIndexChanged(object sender, EventArgs e)
        {
            changePictureBoxImage(currentScene.display());

            imageList.ClearSelected();
        }

        // 현재 모드에 해당하는 것은 무조건 보이도록 설정
        private void displayList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.Index == 0)
            {
                if (vanishingLineButton.Checked)
                    e.NewValue = CheckState.Checked;
                currentScene.displayVanishingLine = (e.NewValue == CheckState.Checked);
            }
            else if (e.Index == 1)
            {
                if (pointButton.Checked)
                    e.NewValue = CheckState.Checked;
                currentScene.displayPoint = (e.NewValue == CheckState.Checked);
                if (e.CurrentValue == CheckState.Unchecked)
                    displayList.SetItemChecked(4, true);
                else if (e.NewValue == CheckState.Unchecked)
                    displayList.SetItemChecked(4, false);
            }
            else if (e.Index == 2)
            {
                if (faceButton.Checked)
                    e.NewValue = CheckState.Checked;
                currentScene.displayFace = (e.NewValue == CheckState.Checked);
            }
            else if (e.Index == 3)
            {
                if (guideLineButton.Checked)
                    e.NewValue = CheckState.Checked;
                currentScene.displayGuideLine = (e.NewValue == CheckState.Checked);
            }
            else if (e.Index == 4)
            {
                if (!currentScene.displayPoint)
                    e.NewValue = CheckState.Unchecked;
                currentScene.displayPointNumber = (e.NewValue == CheckState.Checked);
            }

            changePictureBoxImage(currentScene.display());
        }

        private void imageList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                for (int i = 0; i < imageList.Items.Count; i++)
                {
                    if (i != e.Index)
                        imageList.SetItemChecked(i, false);
                }

                sceneChange(e.Index);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt";
            saveFileDialog1.DefaultExt = "txt";
            saveFileDialog1.Title = "Save the File";

            // If the file name is not an empty string open it for saving.  
            if (saveFileDialog1.ShowDialog() == DialogResult.OK && saveFileDialog1.FileName != "")
            {
                System.IO.TextWriter tw = new System.IO.StreamWriter(saveFileDialog1.FileName, true);
                for (int i = 0; i < model.scene.Count; i++)
                {
                    Scene scene = model.scene[i];
                    tw.WriteLine("Img" + i.ToString());
                    tw.WriteLine(scene.filename);
                    foreach (Line item in scene.vanishingLineX)
                        tw.WriteLine("vx " + item.ToString());
                    foreach (Line item in scene.vanishingLineY)
                        tw.WriteLine("vy " + item.ToString());
                    foreach (Line item in scene.vanishingLineZ)
                        tw.WriteLine("vz " + item.ToString());
                    foreach (Vertex2D item in scene.vertexList)
                        tw.WriteLine("v " + item.coord.X.ToString() + " " + item.coord.Y.ToString());
                    foreach (Line item in scene.guideLine)
                        tw.WriteLine("g " + item.ToString());
                }
                tw.WriteLine("End Image");
                foreach (Face item in model.face)
                    tw.WriteLine("f " + item.ToString());
                tw.Close();

                fileName = saveFileDialog1.FileName;
            }
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            string line;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "txt files (*.txt)|*.txt";
            openFileDialog.Title = "Open the File";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamReader file = new System.IO.StreamReader(openFileDialog.FileName);
                InitComponents();
                fileName = openFileDialog.FileName;
                Scene scene = null;
                bool success = true;
                int pointNumber = 0;
                int sceneNumber = 0;

                while ((line = file.ReadLine()) != null)
                {
                    if (line == "End Image")
                        break;
                    else if (line.Substring(0, 3) == "Img")
                    {
                        model.scene.Add(new Scene(file.ReadLine(), this.pictureBox1.Size, model.face));
                        sceneNumber = int.Parse(line.Substring(3));
                        imageList.Items.Add("Img" + sceneNumber.ToString());
                        dataGridView1.Columns.Add("Img" + sceneNumber.ToString(), "Img" + sceneNumber.ToString());
                        dataGridView1.Columns[dataGridView1.Columns.Count - 1].Width = 50;
                        scene = model.scene[sceneNumber];
                        pointNumber = 0;
                    }
                    else if (line.Substring(0, 2) == "vx")
                    {
                        Line item = new Line(line.Substring(3), ref success);
                        if (success)
                            scene.vanishingLineX.Add(item);
                    }
                    else if (line.Substring(0, 2) == "vy")
                    {
                        Line item = new Line(line.Substring(3), ref success);
                        if (success)
                            scene.vanishingLineY.Add(item);
                    }
                    else if (line.Substring(0, 2) == "vz")
                    {
                        Line item = new Line(line.Substring(3), ref success);
                        if (success)
                            scene.vanishingLineZ.Add(item);
                    }
                    else if (line.Substring(0, 1) == "v")
                    {
                        Vertex2D item = new Vertex2D(line.Substring(2), pointNumber, ref success);
                        scene.addVertex(item.coord, item.number);
                        addDataGridView(item.number, sceneNumber);
                        pointNumber++;
                    }
                    else if (line.Substring(0, 1) == "g")
                    {
                        Line item = new Line(line.Substring(2), ref success);
                        if (success)
                            scene.guideLine.Add(item);
                    }
                }

                while ((line = file.ReadLine()) != null)
                {
                    if (line.Substring(0, 1) == "f")
                    {
                        model.face.Add(new Face(line.Substring(2)));
                    }
                    else
                        break;
                }

                imageList.SetItemChecked(0, true);
                displayList.SetItemChecked(0, true);
                displayList.SetItemChecked(1, true);
                displayList.SetItemChecked(2, true);
                displayList.SetItemChecked(3, true);
                displayList.SetItemChecked(4, true);

                file.Close();
            }
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            model.create3DModel();
        }
    }

}
}

