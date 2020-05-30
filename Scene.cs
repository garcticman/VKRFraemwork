namespace VKR {
    abstract class Scene {
        protected Game game;
        public void SetController(Game game) {
            this.game = game;
        }

        public abstract void Start();
        public abstract void Update();
        public abstract void Destroy();
    }
}