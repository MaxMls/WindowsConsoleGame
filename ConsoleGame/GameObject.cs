namespace ConsoleGame {
    internal class GameObject {
        private Vector2 _position;

        public Vector2 position
        {
            get {
                return _position;
            }
            set {
                old_position = _position;
                _position = value;
            }
        }
        public Vector2 old_position;
        public Material material;

        public GameObject(Vector2 pos = null) {
            old_position = position = pos ?? new Vector2();
            material = new Material(@"image.bmp");
            ConsoleGame.gameObjectList.Add(this);
        }

    }
}