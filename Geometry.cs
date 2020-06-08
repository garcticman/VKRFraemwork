namespace VKR {
    class Point {
        public int X, Y;
        public Point(int x, int y) {
            X = x;
            Y = y;
        }

        public static Point operator +(Point p1, Point p2) {
            return new Point(p1.X + p2.X, p1.Y + p2.Y);
        }
        public static Point operator -(Point p1, Point p2) {
            return new Point(p1.X - p2.X, p1.Y - p2.Y);
        }
    }
}