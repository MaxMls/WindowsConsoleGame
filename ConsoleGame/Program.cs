using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ConsoleGame {
    class ConsoleGame {
        public static int height, width;
        public static List<GameObject> gameObjectList = new List<GameObject>();

        static void Main(string[] args) {
            ConsoleGame consoleGame = new ConsoleGame();
            consoleGame.Awake();
        }



        public static ConsoleKey key;
        void Awake() {

            //width = Console.WindowWidth;
            //height = Console.WindowHeight;
            Console.CursorVisible = false;


            Start();



            while (true) {
                key = default(ConsoleKey);
                if (Console.KeyAvailable) {
                    key = Console.ReadKey(true).Key;
                }


                Thread.Sleep(10);
                Update();
                Draw.Go();
            }
        }

        GameObject point, point2;
        void Start() {
            //point = new GameObject();
            //point.material.texture;


            point2 = new GameObject();
            //point2.material.Color(ConsoleColor.Red);
        }


        bool vector;
        float speed = 0.8f;
        void Update() {
            if (width != Console.WindowWidth || height != Console.WindowHeight) {
                width = Console.WindowWidth;
                height = Console.WindowHeight;
                Draw.buffer = new Pixel[ConsoleGame.width, ConsoleGame.height];
                Draw.screen = Draw.buffer;
                Console.CursorVisible = false;
                Draw.full = true;
            }


            //if (point.position.x >= width) vector = false;
            //else if (point.position.x <= 0) vector = true;

            //if (vector) point.position.x += 1 * speed;
            //else point.position.x -= 1 * speed;

            if (key == ConsoleKey.W) point2.position.y -= 1 * speed;
            if (key == ConsoleKey.A) point2.position.x -= 1 * speed;
            if (key == ConsoleKey.S) point2.position.y += 1 * speed;
            if (key == ConsoleKey.D) point2.position.x += 1 * speed;


        }





    }




    public static class Draw {
        public static Pixel[,] buffer = new Pixel[ConsoleGame.width, ConsoleGame.height];
        public static Pixel[,] screen = buffer;

        static Pixel clear = new Pixel(ConsoleColor.Black, new Vector2());

        public static bool full;


        static int time = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;

        public static void Go() {
            Set();

            if (full && -time + (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds > 1) {
                time = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;

                DrawBuffer(full);
                full = false;

            }
            else DrawBuffer();

        }

        static void Set() {
            //

            for (int x = 0; x < ConsoleGame.width; x++) {
                for (int y = 0; y < ConsoleGame.height; y++) {
                    buffer[x, y] = clear;
                }
            }

            foreach (GameObject o in ConsoleGame.gameObjectList) {

                foreach (Pixel pixel in o.material.texture) {

                    int x = (int)(o.position.x + pixel.pos.x),
                    y = (int)(o.position.y + pixel.pos.y);

                    if (x >= ConsoleGame.width - 1 || y >= ConsoleGame.height - 1 || x < 0 || y < 0) continue;

                    buffer[x, y] = pixel;
                }


            }
        }

        static bool first = true;

        static void DrawBuffer(bool full = false) {




            for (int x = 0; x < ConsoleGame.width; x++) {
                for (int y = 0; y < ConsoleGame.height; y++) {

                    if (!full && screen[x, y].color == buffer[x, y].color) { continue; }


                    Console.SetCursorPosition(x, y);
                    Console.BackgroundColor = buffer[x, y].color;
                    Console.Write(" ");
                    Console.ResetColor();

                }
            }

            screen = Copy(buffer);

        }

        static T[,] Copy<T>(T[,] array) {
            T[,] newArray = new T[array.GetLength(0), array.GetLength(1)];
            for (int i = 0; i < array.GetLength(0); i++)
                for (int j = 0; j < array.GetLength(1); j++)
                    newArray[i, j] = array[i, j];
            return newArray;
        }
    }













}
