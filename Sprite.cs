namespace VKR {
    class Sprite {
        public Graphics.Image Image { get; private set; }
        public Point Position { get; private set; } = new Point(0, 0);
        public Sprite(string file, Graphics parent) {
            Image = new Graphics.Image(file, parent);
        }

        public void SetPosition(Point position) {
            Point locPosition = Image.Position - Position; 
            
            Position = position;
            Image.SetPosition(Position + locPosition); 
        }

        public void Draw() {
            Image.Draw();
        }
    }
}