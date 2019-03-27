using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace UpDimension
{
    public static class MyExtensions
    {
        public static float distance(this PointF point1, PointF point2)
        {
            return (float)Math.Sqrt((point1.X - point2.X) * (point1.X - point2.X) + (point1.Y - point2.Y) * (point1.Y - point2.Y));
        }

        public static bool intersect(this RectangleF rectangle, PointF point)
        {
            if (rectangle.X <= point.X && rectangle.Y <= point.Y && point.X <= rectangle.X + rectangle.Width && point.Y <= rectangle.Y + rectangle.Height)
                return true;
            else
                return false;
        }

        public static bool isIntersect(this RectangleF rectangle, Line line)
        {
            if (rectangle.intersect(line.P1) && rectangle.intersect(line.P2))
                return true;
            else
            {
                if (rectangle.Y <= line.Y(rectangle.X) && line.Y(rectangle.X) <= rectangle.Y + rectangle.Height)
                    return true;
                if (rectangle.Y <= line.Y(rectangle.X + rectangle.Width) && line.Y(rectangle.X + rectangle.Width) <= rectangle.Y + rectangle.Height)
                    return true;
                if (rectangle.X <= line.X(rectangle.Y) && line.X(rectangle.Y) <= rectangle.X + rectangle.Width)
                    return true;
                if (rectangle.X <= line.X(rectangle.Y + rectangle.Height) && line.X(rectangle.Y + rectangle.Height) <= rectangle.X + rectangle.Width)
                    return true;
                return false;
            }
        }

        public static PointF midPoint(this RectangleF rectangle, vLine vline)
        {
            PointF[] point = new PointF[2];
            int count = 0;

            if (rectangle.Y <= vline.Y(rectangle.X) && vline.Y(rectangle.X) <= rectangle.Y + rectangle.Height)
                point[count++] = new PointF(rectangle.X, vline.Y(rectangle.X));
            if (rectangle.Y <= vline.Y(rectangle.X + rectangle.Width) && vline.Y(rectangle.X + rectangle.Width) <= rectangle.Y + rectangle.Height)
                point[count++] = new PointF(rectangle.X + rectangle.Width, vline.Y(rectangle.X + rectangle.Width));
            if (rectangle.X <= vline.X(rectangle.Y) && vline.X(rectangle.Y) <= rectangle.X + rectangle.Width)
                point[count++] = new PointF(vline.X(rectangle.Y), rectangle.Y);
            if (rectangle.X <= vline.X(rectangle.Y + rectangle.Height) && vline.X(rectangle.Y + rectangle.Height) <= rectangle.X + rectangle.Width)
                point[count++] = new PointF(vline.X(rectangle.Y + rectangle.Height), rectangle.Y + rectangle.Height);

            return new PointF((point[0].X + point[1].X) / 2, (point[0].Y + point[1].Y) / 2);
        }

        public static bool equal(this PointF point1, PointF point2)
        {
            float EPSILON = 0.001F;
            return Math.Abs(point1.X - point2.X) < EPSILON && Math.Abs(point1.Y - point2.Y) < EPSILON;
        }
    }

    public class Point3D
    {
        public float X;
        public float Y;
        public float Z;

        public Point3D(float X, float Y, float Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }
    }

    public interface ISelect { }

    public interface IHighlight { }

    public interface ISave { }

    public class vLine : Line
    {
        public Line line;
        public int orientation;

        public static readonly int Xaxis = 1;
        public static readonly int Yaxis = 2;
        public static readonly int Zaxis = 3;

        public vLine(PointF P1, PointF P2, int orientation) : base(P1, P2)
        {
            line = new Line(P1, P2);
            extend();
            this.orientation = orientation;
        }

        public vLine(Line line, int orientation) : base(line)
        {
            line = new Line(line);
            extend();
            this.orientation = orientation;
        }

        void extend()
        {
            if (b == 0)
            {
                P1 = new PointF(P1.X, -1F);
                P2 = new PointF(P1.X, 10000F);
            }
            else
            {
                P1 = new PointF(-1F, -(a * P1.X + c) / b);
                P2 = new PointF(10000F, -(a * P2.X + c) / b);
            }
        }

        public PointF intersection(vLine vline)
        {
            PointF point = new PointF();
            float det = this.a * vline.b - vline.a * this.b;

            if (det == 0)
            {
                point.X = -1;
                point.Y = -1;
            }
            else
            {
                point.X = 1 / det * (this.b * vline.c - vline.b * this.c);
                point.Y = 1 / det * (this.c * vline.a - vline.c * this.a);
            }

            return point;
        }

        public void draw(Graphics g, RectangleF rectangle, Func<PointF, PointF> image2Picture)
        {
            PointF midPoint = rectangle.midPoint(this);

            if (orientation == Xaxis)
                draw(g, new Pen(Color.Red, 2), rectangle, image2Picture);
            else if (orientation == Yaxis)
                draw(g, new Pen(Color.Green, 2), rectangle, image2Picture);
            else if (orientation == Zaxis)
                draw(g, new Pen(Color.Blue, 2), rectangle, image2Picture);
        }

        public void draw(Graphics g, Pen pen, RectangleF rectangle, Func<PointF, PointF> image2Picture)
        {
            PointF midPoint = rectangle.midPoint(this);
            pen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;

            g.DrawLine(pen, image2Picture(P1), image2Picture(midPoint));
            g.DrawLine(pen, image2Picture(midPoint), image2Picture(P2));
        }
    }

    public class Line : ISelect, IHighlight, ISave
    {
        PointF p1;
        PointF p2;

        public float a;
        public float b;
        public float c;

        public PointF P1
        {
            get
            {
                return p1;
            }
            set
            {
                p1 = value;
                this.update();
            }
        }

        public PointF P2
        {
            get
            {
                return p2;
            }
            set
            {
                p2 = value;
                this.update();
            }
        }

        public Line()
        {
            P1 = new PointF();
            P2 = new PointF();
        }

        public Line(PointF P1, PointF P2)
        {
            this.P1 = P1;
            this.P2 = P2;
        }

        public Line(Line line)
        {
            this.P1 = line.P1;
            this.P2 = line.P2;
        }

        public Line(String s, ref bool success)
        {
            string[] str = s.Split(null);
            P1 = new PointF();
            P2 = new PointF();

            if (str.Length == 4)
            {
                p1.X = float.Parse(str[0], System.Globalization.CultureInfo.InvariantCulture);
                p1.Y = float.Parse(str[1], System.Globalization.CultureInfo.InvariantCulture);
                p2.X = float.Parse(str[2], System.Globalization.CultureInfo.InvariantCulture);
                p2.Y = float.Parse(str[3], System.Globalization.CultureInfo.InvariantCulture);
                this.update();
                success = true;
            }
            else
                success = false;
        }

        public float Y(float X)
        {
            if (b == 0)
                return -1;
            else
            {
                float temp = (-a * X - c) / b;
                if ((P1.Y <= temp && temp <= P2.Y) || (P2.Y <= temp && temp <= P1.Y))
                    return temp;
                else
                    return -1;
            }
        }

        public float X(float Y)
        {
            if (a == 0)
                return -1;
            else
            {
                float temp = (-b * Y - c) / a;
                if ((P1.X <= temp && temp <= P2.X) || (P2.X <= temp && temp <= P1.X))
                    return temp;
                else
                    return -1;
            }
        }

        public void update()
        {
            a = p2.Y - p1.Y;
            b = -(p2.X - p1.X);
            c = p2.X * p1.Y - p1.X * p2.Y;
        }

        public PointF intersection(Line line)
        {
            PointF point = new PointF();
            float det = this.a * line.b - line.a * this.b;

            if (det == 0)
            {
                point.X = -1;
                point.Y = -1;
            }
            else
            {
                point.X = 1 / det * (this.b * line.c - line.b * this.c);
                point.Y = 1 / det * (this.c * line.a - line.c * this.a);
            }

            if ((point.X - this.P1.X) * (point.X - this.P2.X) > 0 || (point.X - line.P1.X) * (point.X - line.P2.X) > 0)
            {
                point.X = -1;
                point.Y = -1;
            }

            return point;
        }

        public PointF projection(PointF point)
        {
            float k = -(a * point.X + b * point.Y + c) / (a * a + b * b);

            return new PointF(point.X + a * k, point.Y + b * k);
        }

        public Line perpLine(PointF point)
        {
            return new Line(point, projection(point));
        }

        public float distance(PointF point)
        {
            float innerProduct1 = (P2.X - P1.X) * (point.X - P1.X) + (P2.Y - P1.Y) * (point.Y - P1.Y);
            float innerProduct2 = (P1.X - P2.X) * (point.X - P2.X) + (P1.Y - P2.Y) * (point.Y - P2.Y);

            PointF H = this.projection(point);
            if ((H.X - P1.X) * (H.X - P2.X) < 0)
                return Math.Abs(a * point.X + b * point.Y + c) / (float)Math.Sqrt(a * a + b * b);
            else
                return Math.Min(point.distance(P1), point.distance(P2));
        }

        public override string ToString()
        {
            return P1.X.ToString() + " " + P1.Y.ToString() + " " + P2.X.ToString() + " " + P2.Y.ToString();
        }
        
        public void draw(Graphics g, Pen pen, Func<PointF, PointF> image2Picture)
        {
            g.DrawLine(pen, image2Picture(P1), image2Picture(P2));
        }
    }

    public class Vertex2D : ISelect, IHighlight, ISave
    {
        public PointF coord;
        public int number;

        public Vertex2D()
        {
            this.coord = new PointF(-1, -1);
            this.number = -1;
        }

        public Vertex2D(Vertex2D vertex)
        {
            this.coord = vertex.coord;
            this.number = vertex.number;
        }

        public Vertex2D(PointF coord, int number)
        {
            this.coord = coord;
            this.number = number;
        }

        public Vertex2D(string s, int number, ref bool success)
        {
            string[] str = s.Split(null);

            if (str.Length == 2)
            {
                this.coord.X = float.Parse(str[0], System.Globalization.CultureInfo.InvariantCulture);
                this.coord.Y = float.Parse(str[1], System.Globalization.CultureInfo.InvariantCulture);
                this.number = number;
                success = true;
            }
            else
            {
                this.coord = new PointF(-1, -1);
                this.number = -1;
                success = false;
            }
                
        }
    }

    public class Vertex3D 
    {
        Point3D coord;
        int ID;

        public Vertex3D(Point3D coord, int ID)
        {
            this.coord = coord;
            this.ID = ID;
        }
    }

    public class Face : ISelect, IHighlight
    {
        public List<int> vertexList;

        public int Count
        {
            get
            {
                return vertexList.Count;
            }
        }

        public Face()
        {
            vertexList = new List<int>();
        }

        public Face(string s)
        {
            string[] str = s.Split(null);
            vertexList = new List<int>();

            for (int i = 0; i < str.Length; i++)
            {
                if (str[i].Length != 0)
                    vertexList.Add(int.Parse(str[i]));
            }
        }

        public override string ToString()
        {
            string s = "";
            foreach (int item in vertexList)
                s = s + item.ToString() + " ";

            return s;
        }
    }

    public class LinePoint : IHighlight
    {
        public PointF coord;
    }
}
