using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Roboldz
{
    class Dukong
    {
        private const int anchoImagen = 9;
        private const int altoImagen = 7;
        Texture2D tdukong;
        private const int velocidadduko = 7;
        private Vector2 _posicion;
        //private Rectangle rectanguloduko;

        public event EventHandler FueraDePantalla;
        public Dukong(Vector2 posicion, int altoPJ, ContentManager Content)
        {

            _posicion = posicion;
            _posicion.Y += (altoPJ / 2);
            _posicion.Y -= (anchoImagen / 2) + 2;
            _posicion.X += anchoImagen * 2;

            tdukong = Content.Load<Texture2D>("dukong");
        }
        public void Update()
        {

            _posicion.X += velocidadduko;

            if (_posicion.X > 700)
                FueraDePantalla(this, null);

        }


        public void Draw(SpriteBatch spbtch)
        {
            spbtch.Draw(tdukong, _posicion, new Rectangle(0,0,9,7), Color.White);
        }
    }
}
