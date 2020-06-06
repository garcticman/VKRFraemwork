using System;

namespace VKR {
    abstract class Scene {
        protected Game game;
        public void SetController(Game game) {
            this.game = game;
        }

        public abstract void Start();
        public abstract void Update();
        public abstract void Destroy();

        public virtual void OnKeyAction(InputEvent inputEvent) {
            if (InputEvent.IsKeyPressed(InputEvent.Keys.A)) {
                //Console.WriteLine("A key is pressed");
            }
        }
        public virtual void OnMouseActing(InputEvent inputEvent) {
            //Console.WriteLine(InputEvent.GetMouseCoords().X.ToString() + ", " + InputEvent.GetMouseCoords().Y.ToString());
        }
        public virtual void OnExit(InputEvent inputEvent) {
            //Console.WriteLine("Exit");
        }
    }
}