//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Audio;
//using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;
//using Microsoft.Xna.Framework.Media;

//namespace Roboldz
//{
//    class PJ
//    {
        

//        public PJ(int altoVentana, int anchoVentana)
//        {
           

//        }
        
//        public void LoadContent(ContentManager Content)
//        {
           
            
//        }
//        public void Update()
//        {
//            UpdateShots();
//            UpdatePosition();
//            UpdateRectangle();
         
//        }
//        private void UpdateShots()
//        {
//            frameCounter++;
//            if (Keyboard.GetState().IsKeyDown(Keys.A) && dukongs.Count < 1 && frameCounter > 40)
//            {
//                Dukong s = new Dukong(coordes_Pj, anchoPj, content);
//                dukongs.Add(s);
//                s.FueraDePantalla += new EventHandler(FueraDePantallaHandler);
//                frameCounter = 0;
//            }
//            dukongs.ForEach(x => x.Update());
//        }
//        private void FueraDePantallaHandler(Object sender, EventArgs args)
//        {
//            dukongs.Remove((Dukong)sender);
//        }
//        private void UpdatePosition()
//        {


//            //Movimiento del personaje
//            izq = false;
//            der = false;
//            if ((Keyboard.GetState().IsKeyDown(Keys.Left)) && (Keyboard.GetState().IsKeyUp(Keys.Right)))
//            {
//                coordes_Pj.X -= 5;
//                izq = true;
//                scrollX--;
//                iPj = SpriteEffects.None;
//            }
          
//            if ((Keyboard.GetState().IsKeyDown(Keys.Right)) && (Keyboard.GetState().IsKeyUp(Keys.Left)))
//            {
//                coordes_Pj.X += 5;
//                der = true;
//                scrollX++;
//                iPj = SpriteEffects.FlipHorizontally;
//            }
           


//        }
//         public void UpdateRectangle()
//          {
   
//             if (Keyboard.GetState().IsKeyDown(Keys.Left)) 
//                CrearRectangulo(anchoPj * 4, 0);
           
//             else if (Keyboard.GetState().IsKeyDown(Keys.Right)) 
//                CrearRectangulo(anchoPj*2, 0);
           
//             else
//                CrearRectangulo(0, 0);              
//          }
//        void CrearRectangulo(int x, int y)
//        {
//            rectangulo = new Rectangle(x, y, anchoPj, altoPj);
//        }  
//        public void Draw(SpriteBatch spbtch)
//        {


//            //Dibujando el pj
//            if (der == true)
//                animPj.DrawFrame(spbtch, coordes_Pj, cPj, iPj);
//            if (izq == true)
//                animPj.DrawFrame(spbtch, coordes_Pj, cPj, iPj);
//            if ((izq == false) && (der == false)) spbtch.Draw(Pj, coordes_Pj, null, cPj, 0, Vector2.Zero, 1, iPj, 0);
//            DrawShots(spbtch);
//        }
//        private void DrawShots(SpriteBatch spbtch)
//        {
//            foreach (Dukong s in dukongs)
//            {
//                s.Draw(spbtch);
//            }
//        }
//    }
//}