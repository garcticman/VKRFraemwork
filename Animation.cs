using System;
using System.Collections.Generic;

namespace VKR {
    class Animation {
        string name;
        public List<Graphics.Image> Frames { get; private set; }
        public Sprite Parent { get; private set; }
        public string Name { 
            get { 
                return name;
            } 
            set {
                Parent.RemoveAnimation(name);

                name = value;

                Parent.AddAnimation(this);
            }
        }
        public Animation(string name, Sprite parent) {
            if (parent == null) 
                throw new Exception("Image constructor error: fill parent parametr");
            Parent = parent;

            if (name == String.Empty)
                throw new Exception("Image constructor error: fill name parametr");
            this.name = name;

            Frames = new List<Graphics.Image>();
            Parent.AddAnimation(this);
        }

        public void AddFrame(string file) {
            Frames.Add(new Graphics.Image(file, Parent.Image.Parent, Parent.Image.Position, Parent.Image.Width, Parent.Image.Height));
        }
        public void RemoveFrame(int index) {
            Frames.RemoveAt(index);
        }
    }
}