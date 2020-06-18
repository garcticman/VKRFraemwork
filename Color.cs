namespace VKR {
    class Color {
        public byte R;
        public byte G;
        public byte B;
        public byte A;

        public Color() {
            R = G = B = A = 255;
        }
        public Color(byte r, byte g, byte b) {
            R = r; 
            G = g; 
            B = b; 
            A = 255;
        }
        public Color(byte r, byte g, byte b, byte a) {
            R = r; 
            G = g; 
            B = b; 
            A = a;
        }
    }
}