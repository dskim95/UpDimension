using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using MathNet.Numerics.LinearAlgebra;

namespace UpDimension
{
    public class Scene
    {
        public string filename;
        public Bitmap originalImage;
        Size pictureBoxSize;
        Size currentImageSize;
        Point currentImageLocation;

        public int currentMagnification;
        public int bufferedMagnification;
        public int maxMagnification;
        public float[] magnification = new float[20] { 1, 1.5F, 2, 3, 4, 6, 8, 12, 16, 24, 32, 48, 64, 85, 127.5F, 170, 244, 340, 510, 640 };

        public List<vLine> vanishingLineX;
        public List<vLine> vanishingLineY;
        public List<vLine> vanishingLineZ;
        public List<vLine> vanishingLine;

        public RectangleF currentImage
        {
            get
            {
                return new RectangleF(currentImageLocation, currentImageSize);
            }
        }

        public float threshold
        {
            get
            {
                return 5F * currentImageSize.Width / pictureBoxSize.Width;
            }
        }

        PointF vanishingPointX
        {
            get
            {
                return vanishingPoint(vanishingLineX);
            }
        }

        PointF vanishingPointY
        {
            get
            {
                return vanishingPoint(vanishingLineY);
            }
        }

        PointF vanishingPointZ
        {
            get
            {
                return vanishingPoint(vanishingLineZ);
            }
        }

        PointF vanishingPoint(List<Line> vlineList)
        {
            if (vlineList.Count < 2)
                return new PointF(-1, -1);
            else
            {
                PointF point = new PointF(0, 0);
                for (int i = 0; i < vlineList.Count; i++)
                    for (int j = i + 1; j < vlineList.Count; j++)
                    {
                        PointF intersect = vlineList[i].intersection(vlineList[j]);
                        point.X = point.X + intersect.X;
                        point.Y = point.Y + intersect.Y;
                    }
                point.X = point.X / (float)vlineList.Count / (float)(vlineList.Count + 1) * 2F;

                return point;
            }
        }

        public Matrix<float> proj
        {
            get
            {
                Matrix<float> P = Matrix<float>.Build.Dense(3, 3);
                Line line1 = new Line(vanishingPointY, vanishingPointZ).perpLine(vanishingPointX);
                Line line2 = new Line(vanishingPointX, vanishingPointZ).perpLine(vanishingPointY);
                PointF O = line1.intersection2(line2);
                float a = vanishingPointX.distance(vanishingPointY);
                float b = O.distance(vanishingPointX);
                float c = O.distance(vanishingPointY);
                float f = (float)Math.Sqrt((double)(a * a - b * b - c * c) / 2);

                Matrix<float> K = MathNet.Numerics.LinearAlgebra.Single.DenseMatrix.OfArray(new float[,]
                {
                    {f, 0, O.X }, {0, f, O.Y}, {0, 0, 1 }
                });

                Vector<float> OX = MathNet.Numerics.LinearAlgebra.Single.DenseVector.OfArray(new float[]
                {
                    vanishingPointX.X - O.X, vanishingPointX.Y - O.Y, f
                });
                Vector<float> OY = MathNet.Numerics.LinearAlgebra.Single.DenseVector.OfArray(new float[]
                {
                    vanishingPointY.X - O.X, vanishingPointY.Y - O.Y, f
                });
                Vector<float> OZ = MathNet.Numerics.LinearAlgebra.Single.DenseVector.OfArray(new float[]
                {
                    vanishingPointZ.X - O.X, vanishingPointZ.Y - O.Y, f
                });
                OX = OX.Normalize(2);
                OY = OY.Normalize(2);
                OZ = OZ.Normalize(2);
                Matrix<float> R = MathNet.Numerics.LinearAlgebra.Single.DenseMatrix.OfArray(new float[,]
                {
                    {OX[0], OY[0], OZ[0] }, {OX[1], OY[1], OZ[1] }, {OX[2], OY[2], OZ[2] }
                });

                P = K * R;

                return P;
            }
        }

        //public Matrix<float> proj;

        public List<Vertex2D> vertexList;
        public List<Line> guideLine;
        public List<Face> faceList;

        public bool displayVanishingLine;
        public bool displayPoint;
        public bool displayFace;
        public bool displayGuideLine;
        public bool displayPointNumber;
        
        public Scene(string filename, Size pictureBoxSize, List<Face> faceList)
        {
            this.filename = filename;
            this.pictureBoxSize = pictureBoxSize;
            this.faceList = faceList;
            initImage(filename, pictureBoxSize);

            vanishingLineX = new List<vLine>();
            vanishingLineY = new List<vLine>();
            vanishingLineZ = new List<vLine>();
            vanishingLine = new List<vLine>();
            //proj = Matrix<float>.Build.Dense(3, 4);
            vertexList = new List<Vertex2D>();
            guideLine = new List<Line>();

            displayVanishingLine = true;
            displayPoint = true;
            displayFace = true;
            displayGuideLine = true;
            displayPointNumber = true;
        }

        public static bool closestLine<T>(PointF currentLocation, List<T> lineList, ref float min, ref T line) where T : Line
        {
            bool changed = false;

            foreach (T item in lineList)
            {
                if (item.distance(currentLocation) <= min)
                {
                    min = item.distance(currentLocation);
                    line = item;
                    changed = true;
                }
            }

            return changed;
        }

        public static bool closestLine<T>(PointF currentLocation, List<T> lineList, ref float min, ref T line, ref bool find) where T : Line
        {
            if (closestLine<T>(currentLocation, lineList, ref min, ref line))
            {
                find = true;
                return true;
            }
            else
                return false;
        }

        public static bool closestLinePoint(PointF currentLocation, List<Line> lineList, ref float min, ref PointF point)
        {
            List<Line> closeLine = new List<Line>();
            Line closestLine = new Line();
            bool changed = false;

            // 가까운 line들을 guideLine중에서 찾음
            foreach (Line item in lineList)
            {
                if (item.distance(currentLocation) <= min)
                {
                    closeLine.Add(item);
                }
            }
            // 가까운 line들의 교점, 끝점과 현재 위치의 거리 계산
            foreach (Line item1 in closeLine)
            {
                if (item1.P1.distance(currentLocation) <= min)
                {
                    min = item1.P1.distance(currentLocation);
                    point = item1.P1;
                    changed = true;
                }
                if (item1.P2.distance(currentLocation) <= min)
                {
                    min = item1.P2.distance(currentLocation);
                    point = item1.P2;
                    changed = true;
                }
                foreach (Line item2 in closeLine)
                {
                    if (item1 != item2)
                    {
                        PointF intersect = item1.intersection(item2);
                        if (intersect.X != -1 && intersect.distance(currentLocation) <= min)
                        {
                            min = intersect.distance(currentLocation);
                            point = intersect;
                            changed = true;
                        }
                    }
                }
            }

            return changed;
        }

        public static bool closestLinePoint(PointF currentLocation, List<Line> lineList, ref float min, ref PointF point, ref bool find)
        {
            if (closestLinePoint(currentLocation, lineList, ref min, ref point))
            {
                find = true;
                return true;
            }
            else
                return false;
        }

        public static bool closestPoint(PointF currentLocation, List<Vertex2D> vertexList, ref float min, ref Vertex2D vertex)
        {
            bool changed = false;

            foreach (Vertex2D item in vertexList)
            {
                if (item.coord.X != -1 && item.coord.distance(currentLocation) <= min)
                {
                    min = item.coord.distance(currentLocation);
                    vertex = new Vertex2D(item);
                    changed = true;
                }
            }

            return changed;
        }

        public static bool closestPoint(PointF currentLocation, List<Vertex2D> vertexList, ref float min, ref Vertex2D vertex, ref bool find)
        {
            if (closestPoint(currentLocation, vertexList, ref min, ref vertex))
            {
                find = true;
                return true;
            }
            else
                return false;
        }

        public bool closestFaceLine(PointF currentLocation, ref float min, ref Face face)
        {
            bool changed = false;

            foreach (Face item in faceList)
            {
                if (item.Count == 2 && isFaceIncluded(item))
                {
                    PointF point1 = vertexList[item.vertexList[0]].coord;
                    PointF point2 = vertexList[item.vertexList[1]].coord;
                    Line line = new Line(point1, point2);

                    if (line.distance(currentLocation) <= min)
                    {
                        min = line.distance(currentLocation);
                        face = item;
                        changed = true;
                    }
                }
            }

            return changed;
        }

        public bool closestFace(PointF currentLocation, ref Face face)
        {
            bool changed = false;

            foreach (Face item in faceList)
            {
                if (item.Count >= 2 && isFaceIncluded(item))
                {
                    int count = 0;
                    Line line = new Line(new PointF(0, 0), currentLocation);

                    for (int i = 0; i < item.Count; i++)
                    {
                        PointF point1 = vertexList[item.vertexList[i]].coord;
                        PointF point2;
                        if (i == item.Count - 1)
                            point2 = vertexList[item.vertexList[0]].coord;
                        else
                            point2 = vertexList[item.vertexList[i + 1]].coord;
                        Line line1 = new Line(point1, point2);

                        if (line.intersection(line1).X != -1)
                            count++;
                    }

                    if (count % 2 == 1)
                    {
                        face = item;
                        changed = true;
                        break;
                    }
                }
            }

            return changed;
        }
        
        public void initImage(string filename, Size pictureBoxSize)
        {
            Bitmap image = new Bitmap(filename);
            SolidBrush myBrush = new SolidBrush(Color.FromArgb(100, 100, 100));

            float imageRatio = (float)image.Width / (float)image.Height;
            float pictureBoxRatio = (float)pictureBoxSize.Width / (float)pictureBoxSize.Height;

            // pictureBox와 image의 비율이 다를 경우, image의 비율을 pictureBox에 맞춰준다.
            if (imageRatio > pictureBoxRatio)
            {
                originalImage = new Bitmap(image.Width, (int)((float)image.Width / pictureBoxRatio));
                Graphics g = Graphics.FromImage(originalImage);

                g.FillRectangle(myBrush, 0, 0, originalImage.Width, originalImage.Height);
                g.DrawImage(image, new Point(0, (originalImage.Height - image.Height) / 2));

                g.Dispose();
            }
            else
            {
                originalImage = new Bitmap((int)((float)image.Height * pictureBoxRatio), image.Height);
                Graphics g = Graphics.FromImage(originalImage);

                g.FillRectangle(myBrush, 0, 0, originalImage.Width, originalImage.Height);
                g.DrawImage(image, new Point((originalImage.Width - image.Width) / 2, 0));

                g.Dispose();
            }

            currentImageSize = new Size(originalImage.Width, originalImage.Height);
            currentImageLocation = new Point(0, 0);

            currentMagnification = 0;
            bufferedMagnification = 0;

            for (int i = 0; i < magnification.Length; i++)
            {
                if ((float)Math.Min(image.Width, image.Height) / magnification[i] > 20.0F)
                    maxMagnification = i;
                else
                    break;
            }
        }

        // mousePosition을 중심으로해서 bufferedMagnification에 기록된 배율로 변경
        public void magnifyImage(PointF mousePosition)
        {
            float ratio = magnification[currentMagnification] / magnification[bufferedMagnification];
            currentMagnification = bufferedMagnification;

            currentImageSize.Width = (int)Math.Round(((float)originalImage.Width / magnification[currentMagnification]));
            currentImageSize.Height = (int)Math.Round(((float)originalImage.Height / magnification[currentMagnification]));

            currentImageLocation.X = (int)Math.Round(mousePosition.X - (ratio * (mousePosition.X - (float)currentImageLocation.X)));
            currentImageLocation.Y = (int)Math.Round(mousePosition.Y - (ratio * (mousePosition.Y - (float)currentImageLocation.Y)));

            if (currentImageLocation.X < 0)
                currentImageLocation.X = 0;
            else if (currentImageLocation.X + currentImageSize.Width >= originalImage.Width)
                currentImageLocation.X = originalImage.Width - currentImageSize.Width;

            if (currentImageLocation.Y < 0)
                currentImageLocation.Y = 0;
            else if (currentImageLocation.Y + currentImageSize.Height >= originalImage.Height)
                currentImageLocation.Y = originalImage.Height - currentImageSize.Height;
        }

        // 확대가 필요한 경우
        public Bitmap display(PointF mousePosition)
        {
            magnifyImage(mousePosition);

            return display();
        }

        // 확대가 필요 없는 경우
        public Bitmap display()
        {
            Bitmap newImage = new Bitmap(pictureBoxSize.Width, pictureBoxSize.Height);
            Graphics g = Graphics.FromImage(newImage);
            RectangleF rec = new RectangleF(currentImageLocation, currentImageSize);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.DrawImage(originalImage, new Rectangle(0, 0, pictureBoxSize.Width, pictureBoxSize.Height), currentImageLocation.X, currentImageLocation.Y, currentImageSize.Width, currentImageSize.Height, GraphicsUnit.Pixel);

            if (displayVanishingLine)
            {
                foreach (vLine item in vanishingLine)
                    item.draw(g, rec, image2Picture);
            }

            if (displayGuideLine)
            {
                foreach (Line item in guideLine)
                    g.DrawLine(new Pen(Brushes.HotPink, 2), image2Picture(item.P1), image2Picture(item.P2));
            }
            

            if (displayFace)
            {
                foreach (Face item in faceList)
                {
                    if (isFaceIncluded(item))
                    {
                        g.FillPolygon(new SolidBrush(Color.FromArgb(128, 192, 192, 192)), face2PointArray(item));
                        g.DrawPolygon(new Pen(Brushes.Black, 2), face2PointArray(item));
                    }
                }
            }

            if (displayPointNumber)
            {
                foreach (Vertex2D item in vertexList)
                {
                    if (item.coord.X != -1)
                    {
                        PointF point = image2Picture(item.coord);
                        float dx = 10 / currentImageSize.Width * pictureBoxSize.Width;
                        float dy = 10 / currentImageSize.Height * pictureBoxSize.Height;

                        g.DrawString(item.number.ToString(), new Font("Arial", 10), Brushes.Black, new PointF(point.X + dx, point.Y + dy));
                    }
                }
            }

            if (displayPoint)
            {
                foreach (Vertex2D item in vertexList)
                {
                    if (item.coord.X != -1)
                    {
                        PointF point = image2Picture(item.coord);

                        g.FillEllipse(new SolidBrush(Color.Red), point.X - 3F, point.Y - 3F, 6F, 6F);
                    }
                }
            }

            g.Dispose();

            return newImage;
        }

        public PointF[] face2PointArray (Face face)
        {
            PointF[] array = new PointF[face.Count];

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = image2Picture(vertexList[face.vertexList[i]].coord);
            }

            return array;
        }

        public bool isFaceIncluded (Face face)
        {
            for (int i = 0; i < face.Count; i++)
            {
                if (face.vertexList[i] >= vertexList.Count)
                    return false;
                else
                {
                    if (vertexList[face.vertexList[i]].coord.X == -1)
                        return false;
                }
            }

            return true;
        }

        // 현재 display되는 창의 점을 image기준으로 변경
        public PointF picture2Image(PointF picturePoint)
        {
            PointF imagePoint = new PointF();

            imagePoint.X = (float)currentImageLocation.X + (float)picturePoint.X / (float)pictureBoxSize.Width * (float)currentImageSize.Width;
            imagePoint.Y = (float)currentImageLocation.Y + (float)picturePoint.Y / (float)pictureBoxSize.Height * (float)currentImageSize.Height;

            return imagePoint;
        }

        // image위의 점을 display되는 창의 점으로 변경
        public PointF image2Picture(PointF imagePoint)
        {
            PointF picturePoint = new PointF();

            picturePoint.X = (imagePoint.X - (float)currentImageLocation.X) * (float)pictureBoxSize.Width / (float)currentImageSize.Width;
            picturePoint.Y = (imagePoint.Y - (float)currentImageLocation.Y) * (float)pictureBoxSize.Height / (float)currentImageSize.Height;

            return picturePoint;
        }

        public Line imageLine2PictureLine(Line imageLine)
        {
            Line pictureLine = new Line();

            pictureLine.P1 = image2Picture(imageLine.P1);
            pictureLine.P2 = image2Picture(imageLine.P2);

            return pictureLine;
        }

        public Rectangle imageRectangle2PictureRectangle(RectangleF imageRectangle)
        {
            Rectangle pictureRectangle = new Rectangle();
            Size size = new Size();

            pictureRectangle.X = (int)image2Picture(imageRectangle.Location).X;
            pictureRectangle.Y = (int)image2Picture(imageRectangle.Location).Y;
            size.Width = (int)((float)imageRectangle.Width * (float)pictureBoxSize.Width / (float)currentImageSize.Width);
            size.Height = (int)((float)imageRectangle.Height * (float)pictureBoxSize.Width / (float)currentImageSize.Width);
            pictureRectangle.Size = size;

            return pictureRectangle;
        }

        // Magnification을 증가시키고 현재 값과 달라졌는지 반환
        public bool increaseMagnification()
        {
            if (bufferedMagnification == maxMagnification)
                return false;
            else
            {
                bufferedMagnification++;
                return true;
            }
        }

        // Magnification을 감소시키고 현재 값과 달라졌는지 반환
        public bool decreaseMagnification()
        {
            if (bufferedMagnification == 0)
                return false;
            else
            {
                bufferedMagnification--;
                return true;
            }
        }

        // 점을 vertex list에 추가
        public void addVertex(PointF coord, int number)
        {
            if (vertexList.Count <= number)
            {
                for (int i = vertexList.Count; i <= number; i++)
                    vertexList.Add(new Vertex2D());
            }

            vertexList[number] = new Vertex2D(coord, number);
        }

        // 점을 vertex list에서 제거
        public void removeVertex(int number)
        {
            vertexList[number] = new Vertex2D();
        }

        // bufferedImageLine을 vanishingLine에 추가
        public void addVanishingLine(Line line, int orientation)
        {
            vLine newLine = new vLine(line, orientation);
            vanishingLine.Add(newLine);

            if (orientation == vLine.Xaxis)
                vanishingLineX.Add(newLine);
            else if (orientation == vLine.Yaxis)
                vanishingLineY.Add(newLine);
            else if (orientation == vLine.Zaxis)
                vanishingLineZ.Add(newLine);
        }

        public void removeVanishingLine(vLine vline)
        {
            vanishingLine.Remove(vline);
            if (vline.orientation == vLine.Xaxis)
                vanishingLineX.Remove(vline);
            else if (vline.orientation == vLine.Yaxis)
                vanishingLineY.Remove(vline);
            else if (vline.orientation == vLine.Zaxis)
                vanishingLineZ.Remove(vline);
        }

        // bufferedImageLine을 guideLine에 추가
        public void addGuideLine(Line line)
        {
            guideLine.Add(new Line(line));
        }

        public void removeGuideLine(Line line)
        {
            guideLine.Remove(line);
        }

        // 처음으로 비어있는 pointNumber반환
        public int firstEmptyPointNumber(int currentNumber)
        {
            if (currentNumber >= vertexList.Count)
                return currentNumber;

            for (int i = currentNumber; i < vertexList.Count; i++)
                if (vertexList[i].coord.X == -1)
                    return i;

            return vertexList.Count;
        }

        public int maxPointNumber()
        {
            for (int i = vertexList.Count - 1; i >= 0; i--)
                if (vertexList[i].coord.X != -1)
                    return i;

            return -1;
        }
    }
}
