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
    
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        int correfondo = 0;
        Vector2 fondo;
        bool disparoalacabeza = false;
        int width, height;
        int a;
        Vector2 coordes_Pj, posenemigo, posenemigoa, posicionduko, posmuerenemigo, posboss;
        Vector2 posenemigomultiple1, posenemigomultiple2, posenemigomultiple3;

        Rectangle rectangulo_Pj = new Rectangle(0, 0, 25, 22);
        Rectangle rectangulo_bossparado = new Rectangle(0, 740, 59, 74);

        Texture2D tfondo;
        Texture2D tPj, tPjdisparo, tenemigo, tdukong, tboss, tbossdalaorden;
        Texture2D tenemigoa;
        Texture2D tmuerenemy;
        Texture2D tvida;

        Random posicionenemiga = new Random();

        BoundingBox bPj, benemigo, benemigo1, benemigo2, benemigo3;

        int menu = 0;
        int contadorderondasfinales = 0;

        int vidas = 3;
        const int anchoPj = 25;
        const int altoPj = 22;

        int fasesdelboss=0;
        int cronometroenemigosmultiples=0;
        int enemigosmuertos = 0;

        Song musicaGameOver;
        Song musicadeFondo;
        Song songboss;
        SoundEffect efectotecla;
        SoundEffect efectomuereenemigo;
        SoundEffect efectoduko;
        SoundEffect pjpierdevida, chicken;


        int countdrawenemy = 0;
       
        bool izq = false, der = false, disparoactivo = false, enemymuerto=false;
        bool enemy1muerto=false , enemy2muerto=false,  enemy3muerto=false ;
        bool enemigosmultiples = false;

        List<Dukong> dukongs;
        int frameCounter = 40, timer = -1, timerenemy = -1;
        ContentManager content;

        SpriteEffects iPj = SpriteEffects.FlipHorizontally, ienemigo = SpriteEffects.None;
        Animaciones animPj, animenemigo, animenemigoa;
        Animaciones animuerenemigo;
        Animaciones animboss;

        Texture2D[] tmenu = new Texture2D[7];

        Microsoft.Xna.Framework.Color cPj, cenemy;

    
      

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            width = 700;
            height = 400;
            this.graphics.PreferredBackBufferWidth = width;
            this.graphics.PreferredBackBufferHeight = height;
         
        }

        protected override void Initialize()
        {


            fondo = new Vector2(0, 0);
            Window.Title = "Roboldz";

            posenemigo = new Vector2(900, 203);
            posenemigoa = new Vector2(500, 215);
            
            posicionduko = new Vector2(-400, 235);
            coordes_Pj = new Vector2(0, 230);

            posboss = new Vector2(3670, 178);

            posenemigomultiple1 = new Vector2(1000, 203);
            posenemigomultiple2 = new Vector2(1100, 203);
            posenemigomultiple3 = new Vector2(1200, 203);

            dukongs = new List<Dukong>();

            animenemigo = new Animaciones(posenemigo, 6, 15);
            animPj = new Animaciones(coordes_Pj, 11, 30);
            animenemigoa = new Animaciones(posenemigoa, 3, 3);
            animuerenemigo = new Animaciones(posmuerenemigo, 7, 3);

            animboss = new Animaciones(posboss, 11, 10);

            

            efectotecla = Content.Load<SoundEffect>("efectotecla");
            efectomuereenemigo = Content.Load<SoundEffect>("muerteenemigo");
            efectoduko = Content.Load<SoundEffect>("efectoduko");
            pjpierdevida = Content.Load<SoundEffect>("pierdevida");

            musicadeFondo = Content.Load<Song>("escape the fate");
            musicaGameOver=Content.Load<Song>("mp3Game Over");
            songboss = Content.Load<Song>("00 - Noisia - Machine Gun (16Bit remix)");

            chicken=Content.Load<SoundEffect>("chickennnn");

            MediaPlayer.Play(musicadeFondo);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.1f;

            

            base.Initialize();
        }
        protected override void LoadContent()
        {
           
            spriteBatch = new SpriteBatch(GraphicsDevice);
            tfondo = Content.Load<Texture2D>("fondo");
        
            this.content = Content;
           

            posmuerenemigo = new Vector2(300,227);

            


            tPj = Content.Load<Texture2D>("Pj");
            tPjdisparo = Content.Load<Texture2D>("tPjdisparo");
            tenemigo = Content.Load<Texture2D>("enemigo");
            tenemigoa = Content.Load<Texture2D>("enemigoa");
            tmuerenemy = Content.Load<Texture2D>("muerenemigo");
            tdukong = Content.Load<Texture2D>("dukong");

            tboss=Content.Load<Texture2D>("final boss");
            tbossdalaorden = Content.Load<Texture2D>("bossdalaorden");

            tvida = Content.Load<Texture2D>("vidaPj");

            tmenu[0] = Content.Load<Texture2D>("menu0jugar");
            tmenu[1] = Content.Load<Texture2D>("menu0tutorial");
            tmenu[2] = Content.Load<Texture2D>("menu0créditos");
            tmenu[4] = Content.Load<Texture2D>("tgameover");
            tmenu[5] = Content.Load<Texture2D>("tutorial");
            tmenu[6] = Content.Load<Texture2D>("fondo ganaste");
            


            animPj.Load(Content,"Pj");
            animenemigo.Load(Content, "enemigo");
            animenemigoa.Load(Content, "enemigoa");
            animuerenemigo.Load(Content, "muerenemigo");
            animboss.Load(Content, "final boss");

        }


        protected override void UnloadContent()
        {

        }

   
        
        private void UpdatePosition()
        {
            
            izq = false;
            der = false;
            if ((Keyboard.GetState().IsKeyDown(Keys.Right)) && (Keyboard.GetState().IsKeyUp(Keys.Left)) && (coordes_Pj.X > 400) && (correfondo > -3050))
            {
                correfondo -= 2;
                posboss.X -= 2;
                der = true;
            }
          //  if ((Keyboard.GetState().IsKeyDown(Keys.Left)) && (Keyboard.GetState().IsKeyUp(Keys.Right)) && (coordes_Pj.X < 100)&&(correfondo>0))
           // correfondo += 2;
            if ((Keyboard.GetState().IsKeyDown(Keys.Left)) && (Keyboard.GetState().IsKeyUp(Keys.Right)) && (coordes_Pj.X > 2))
            {
                coordes_Pj.X -= 3;
                izq = true;
                
                iPj = SpriteEffects.None;
            }

            if ((Keyboard.GetState().IsKeyDown(Keys.Right)) && (Keyboard.GetState().IsKeyUp(Keys.Left))&&(coordes_Pj.X<402))
            {
                coordes_Pj.X += 3;
                der = true;
                
                iPj = SpriteEffects.FlipHorizontally;
            }
            if ((Keyboard.GetState().IsKeyDown(Keys.Left)) && (Keyboard.GetState().IsKeyUp(Keys.Right)) && Keyboard.GetState().IsKeyDown(Keys.A))
            {
                
                izq = true;

                iPj = SpriteEffects.FlipHorizontally;
            }
           

           
        }
         void UpdateShots()
        {
            frameCounter++;
            if (Keyboard.GetState().IsKeyDown(Keys.A) && dukongs.Count < 1 && frameCounter > 0)
            {
                efectoduko.Play();
                
                Dukong s = new Dukong(coordes_Pj, anchoPj, content);
                dukongs.Add(s);
                
                s.FueraDePantalla += new EventHandler(FueraDePantallaHandler);
                frameCounter = 0;
                disparoactivo = true;
                //rectangulodukong = new Rectangle((int)posicionduko.X, (int)posicionduko.Y, tdukong.Width, tdukong.Height);
            }
            
         
          dukongs.ForEach(x => x.Update());
        }

         private void FueraDePantallaHandler(Object sender, EventArgs args)
         {
             dukongs.Remove((Dukong)sender);
             
             disparoactivo = false;
         }


        protected override void Update(GameTime gameTime)
         {
            

             if (Keyboard.GetState().IsKeyDown(Keys.Escape))
             {
                 
                 MediaPlayer.Stop();
                 MediaPlayer.Play(musicadeFondo);
                 MediaPlayer.IsRepeating = true;
                 MediaPlayer.Volume = 0.1f;

                 ResetElapsedTime();
               
                 coordes_Pj.X = 0;
                 posenemigo.X = 900;
                 posboss.X = 3670;
                 correfondo = 0;

                 posenemigomultiple1.X = 1000;
                 posenemigomultiple2.X = 1100;
                 posenemigomultiple3.X = 1200;

                 vidas = 3;
                 menu = 0;
                 fasesdelboss = 0;//cuenta las vueltas para que se accione la animación del boss

                 enemigosmultiples = false;
                 enemigosmuertos = 0;
                 contadorderondasfinales = 0;
                 cronometroenemigosmultiples = 0;
             }
            if (menu==3)
            {
                 bPj = new BoundingBox(new Vector3(coordes_Pj, 0), new Vector3(coordes_Pj.X + tPj.Width, coordes_Pj.Y + tPj.Height / 11, 1));
                 benemigo = new BoundingBox(new Vector3(posenemigo, 0), new Vector3(posenemigo.X + tenemigo.Width, posenemigo.Y + tenemigo.Height / 6, 0));
                 benemigo1 = new BoundingBox(new Vector3(posenemigomultiple1, 0), new Vector3(posenemigomultiple1.X + tenemigo.Width, posenemigomultiple1.Y + tenemigo.Height / 6, 0));
                 benemigo2 = new BoundingBox(new Vector3(posenemigomultiple2, 0), new Vector3(posenemigomultiple2.X + tenemigo.Width, posenemigomultiple2.Y + tenemigo.Height / 6, 0));
                 benemigo3 = new BoundingBox(new Vector3(posenemigomultiple3, 0), new Vector3(posenemigomultiple3.X + tenemigo.Width, posenemigomultiple3.Y + tenemigo.Height / 6, 0));
                 
                //movimiento enemigo
                 if (countdrawenemy == 0 && enemigosmultiples == false)

                     posenemigo.X -= 5;
                 //movimiento enemigosmúltiples
                 if (countdrawenemy == 0 && enemigosmultiples == true)
                 {
                     posenemigomultiple1.X -= 7;
                     posenemigomultiple2.X -= 7;
                     posenemigomultiple3.X -= 7;
                 }
                 //choque
                 if (bPj.Intersects(benemigo)&&enemigosmultiples==false)
                 {

                     if (timer == -2 && (countdrawenemy == 1 || countdrawenemy == 0))
                     {
                         countdrawenemy = 1;
                         vidas--;
                         if (vidas>0)
                         pjpierdevida.Play();
                     }

                     coordes_Pj.X -= 3;
                     if ((bPj.Intersects(benemigo))&&(countdrawenemy!=2))
                     timer = 40;
                 }
                 if (bPj.Intersects(benemigo1) )
                 {
                     if ((timer == -2) && ((countdrawenemy == 1) || (countdrawenemy == 0)))
                     {
                         countdrawenemy = 1;
                         vidas--;
                         if (vidas > 0)
                             pjpierdevida.Play();
                     }
                     coordes_Pj.X -= 3;
                     if (bPj.Intersects(benemigo1) && countdrawenemy != 2)
                         timer = 40;
                 }
                 if (bPj.Intersects(benemigo2))
                 {
                     if ((timer == -2) && ((countdrawenemy == 1) || (countdrawenemy == 0)))
                     {
                         countdrawenemy = 1;
                         vidas--;
                         if (vidas > 0)
                             pjpierdevida.Play();
                     }
                     coordes_Pj.X -= 3;
                     if (bPj.Intersects(benemigo2) && countdrawenemy != 2)
                         timer = 40;
                 }
                 if (bPj.Intersects(benemigo3))
                 {
                     if ((timer == -2) && ((countdrawenemy == 1) || (countdrawenemy == 0)))
                     {
                         countdrawenemy = 1;
                         vidas--;
                         if (vidas > 0)
                             pjpierdevida.Play();
                     }
                     coordes_Pj.X -= 3;
                     if (bPj.Intersects(benemigo3) && countdrawenemy != 2)
                         timer = 40;
                 }
                 if (posenemigo.X > coordes_Pj.X + 28&& enemigosmultiples==false)
                     if (countdrawenemy == 1)
                         countdrawenemy = 0;
                 if (posenemigomultiple1.X > coordes_Pj.X + 28 && enemigosmultiples == true)
                     if (countdrawenemy == 1)
                         countdrawenemy = 0;
                 if (posenemigomultiple2.X > coordes_Pj.X + 28 && enemigosmultiples == true)
                     if (countdrawenemy == 1)
                         countdrawenemy = 0;
                 if (posenemigomultiple3.X > coordes_Pj.X + 28 && enemigosmultiples == true)
                     if (countdrawenemy == 1)
                         countdrawenemy = 0;
                 if (disparoactivo == true)
                 {
                     if (disparoalacabeza == false )
                     {
                         posicionduko.X = coordes_Pj.X;
                         disparoalacabeza = true;
                     }
                     posicionduko.X += 7;
                 }
                 
                 if (posicionduko.X > 700)
                 {
                     disparoactivo = false;
                     disparoalacabeza = false;
                 }

                 if (disparoactivo == false)                 
                     posicionduko.X = coordes_Pj.X;                 
                 if (timerenemy > -2)
                     timerenemy--;
                 cenemy = Color.White;
                 if ((timerenemy > 0) && ((int)gameTime.TotalGameTime.Milliseconds % 3 == 0))
                     cenemy = Color.Transparent;
                 if (posicionduko.X > posenemigo.X)
                 {                     
                     efectomuereenemigo.Play();
                     timerenemy = 20;
                 }
               if ( posicionduko.X > posenemigomultiple1.X)
               {
                   efectomuereenemigo.Play();
                   timerenemy = 20;
                   enemigosmuertos = 1;
                   contadorderondasfinales++;
               }
               if (posicionduko.X > posenemigomultiple2.X)
               {
                   efectomuereenemigo.Play();
                   timerenemy = 20;
                   enemigosmuertos = 2;
               }
               if (posicionduko.X > posenemigomultiple3.X)
               {
                   efectomuereenemigo.Play();
                   timerenemy = 20;
                   enemigosmuertos = 3;
               }

                 if (timer == 0 && enemigosmultiples == false)
                     posenemigo.X -= 3;
                 if (timer == 0 && enemigosmultiples == true)
                 {
                     posenemigomultiple1.X -= 3;
                     posenemigomultiple2.X -= 3;
                     posenemigomultiple3.X -= 3;
                 }
                 if (timer > -2)
                     timer--;
                 cPj = Color.White;

                 if ((timer > 0) && ((int)gameTime.TotalGameTime.Milliseconds % 2 == 0))
                     cPj = Color.Transparent;

                 
                 /*if (timer == 40)
                     vidas--;
                */
                 if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                     this.Exit();
          
               
                 UpdateShots();
                 UpdatePosition();

                 float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
                 animPj.UpdateFrame(elapsed);
                 animenemigo.UpdateFrame(elapsed);
                 animenemigoa.UpdateFrame(elapsed);
                 animuerenemigo.UpdateFrame(elapsed);
                if (fasesdelboss>0)
                    //para que la animación se empiece a dibujar
                    //    en el momento en el que se llega a la etapa final
                    //        y NO en todo momento-ya que no tendría sentido 
                    //            porque la secuencia sería otra que además
                    //                es irracional(se pone el tapado y se lo saca denuevo)-
                 animboss.UpdateFrame(elapsed);
               /* if (fasesdelboss == 67)
                    //si ya se reprodujo una vez la animación de presentación del boss
                    animboss(continue);*/
                //soundtrack boss final
                if (cronometroenemigosmultiples > 1000 && cronometroenemigosmultiples < 2000)
                    MediaPlayer.Volume = 0.3f;
              

                if (correfondo < -2800 && fasesdelboss == 0)
                   
                    MediaPlayer.Volume -= 0.001f;//decrese de a poco el volumen
                if (fasesdelboss == 1)
                    {
                        MediaPlayer.Play(songboss);
                        MediaPlayer.IsRepeating = true;
                        MediaPlayer.Volume = 0.2f;
                    }
                



                 base.Update(gameTime);
             }
         }


        private void DrawShots(SpriteBatch spbtch)
        {
            foreach (Dukong s in dukongs)
            {
                if (disparoactivo==true)
                s.Draw(spbtch);
                
            }
        }
       
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            if ((menu >-1)&&(menu<3))
            {
                
                spriteBatch.Draw(tmenu[menu], fondo = new Vector2(0, 0), Color.White);
                
                if ((Keyboard.GetState().IsKeyDown(Keys.Down)) && ((int)gameTime.TotalGameTime.Milliseconds % 100 == 0))
                {
                    menu++;
                    efectotecla.Play();
                    

                }
                if ((Keyboard.GetState().IsKeyDown(Keys.Up)) && ((int)gameTime.TotalGameTime.Milliseconds % 100 == 0))
                {
                    menu--;
                    efectotecla.Play();
                }
                if (menu == -1)
                    menu = 2;
                if (menu == 3)
                    menu = 0;
            }
            if ((menu == 0) && (Keyboard.GetState().IsKeyDown(Keys.Enter)))
            {
                menu = 3;
                timer = 40;
            }
            if ((menu == 1) && (Keyboard.GetState().IsKeyDown(Keys.Enter)))
                menu = 5;
            if (menu==3)
            {
                
                if (cronometroenemigosmultiples<1000)
                    spriteBatch.Draw(tfondo, fondo = new Vector2(correfondo, 0), Color.White);
                if (cronometroenemigosmultiples > 1000 && cronometroenemigosmultiples<2000)
                   
                    spriteBatch.Draw(tfondo, fondo = new Vector2(correfondo, 0), Color.Black);
                if (cronometroenemigosmultiples > 2000 && cronometroenemigosmultiples < 3000)
                    if ((int)gameTime.TotalGameTime.Milliseconds % 100 == 0)
                    {
                        spriteBatch.Draw(tfondo, fondo = new Vector2(correfondo, 0), Color.White);
                        MediaPlayer.Volume = 0.37f;
                    }
                if (cronometroenemigosmultiples > 3500 && enemigosmuertos == 3)
                {
                    menu = 6;
                    MediaPlayer.Stop();
                    chicken.Play();
                    spriteBatch.Draw(tmenu[menu], Vector2.Zero, Color.White);

                }
                    //vidas
                if (vidas == 3)
                {
                    spriteBatch.Draw(tvida, new Vector2(5, 5), Color.White);
                    spriteBatch.Draw(tvida, new Vector2(62, 5), Color.White);
                    spriteBatch.Draw(tvida, new Vector2(119, 5), Color.White);
                }
                if (vidas == 2)
                {
                    spriteBatch.Draw(tvida, new Vector2(5, 5), Color.White);
                    spriteBatch.Draw(tvida, new Vector2(62, 5), Color.White);

                }
                if (vidas == 1)
                {
                    spriteBatch.Draw(tvida, new Vector2(5, 5), Color.White);

                }
                if (vidas == 0)
                {
                    menu = 4;
                    MediaPlayer.Stop();
                    MediaPlayer.Play(musicaGameOver);
                    MediaPlayer.IsRepeating = true;
                    MediaPlayer.Volume = 0.1f;
                }
               
                //Enemigo
                if (countdrawenemy == 0 && enemigosmultiples == false)
                    animenemigo.DrawFrame(spriteBatch, posenemigo, Color.White, ienemigo);
                if (countdrawenemy == 0 && enemigosmultiples == true)
                {
                    animenemigo.DrawFrame(spriteBatch, posenemigomultiple1, Color.Red, ienemigo);
                    animenemigo.DrawFrame(spriteBatch, posenemigomultiple2, Color.Red, ienemigo);
                    animenemigo.DrawFrame(spriteBatch, posenemigomultiple3, Color.Red, ienemigo);

                }
                if (countdrawenemy == 1&& enemigosmultiples == false)
                    animenemigoa.DrawFrame(spriteBatch, new Vector2(posenemigo.X, 215), Color.White, ienemigo);
                if (enemigosmuertos == 0 && countdrawenemy == 1)
                {
                    animenemigoa.DrawFrame(spriteBatch, new Vector2(posenemigomultiple1.X,215), Color.Red, ienemigo);
                    animenemigo.DrawFrame(spriteBatch, posenemigomultiple2, Color.Red, ienemigo);
                    animenemigo.DrawFrame(spriteBatch, posenemigomultiple3, Color.Red, ienemigo);
                }
                if (enemigosmuertos == 1 && countdrawenemy == 1)
                {
                    animenemigoa.DrawFrame(spriteBatch, new Vector2(posenemigomultiple2.X, 215), Color.Red, ienemigo);
                    animenemigo.DrawFrame(spriteBatch, posenemigomultiple3, Color.Red, ienemigo);
                }
                if (enemigosmuertos == 2 && countdrawenemy == 1)
                {
                    animenemigoa.DrawFrame(spriteBatch, new Vector2(posenemigomultiple3.X, 215), Color.Red, ienemigo);
                }
                //Colisión entre el disparo y el enemigo
                if (posicionduko.X > posenemigo.X && enemigosmultiples == false)
                {
                    countdrawenemy = 2;
                    enemymuerto = true;
                    disparoactivo = false;
                }
                if (posicionduko.X > posenemigomultiple1.X && enemigosmultiples == true)
                {
                    countdrawenemy = 2;
                    enemy1muerto = true;
                    disparoactivo = false;
                }
                if (posicionduko.X > posenemigomultiple2.X && enemigosmultiples == true)
                {
                    countdrawenemy = 2;
                    enemy2muerto = true;
                    disparoactivo = false;
                }
                if (posicionduko.X > posenemigomultiple3.X && enemigosmultiples == true)
                {
                    countdrawenemy = 2;
                    enemy3muerto = true;
                    disparoactivo = false;
                }
                   /* if (enemigosmultiples == true)
                    {

                        if (enemigosmuertos == 2)
                            enemigosmultiples = false;
                        if (enemigosmuertos == 1)
                            enemigosmuertos = 2;
                        if (enemigosmuertos == 0)
                            enemigosmuertos = 1;
                    }
                }*/

                if (enemymuerto == true && timerenemy > -2 && enemigosmultiples == false)
                {
                    animuerenemigo.DrawFrame(spriteBatch, new Vector2(posenemigo.X, 202), cenemy, ienemigo);
                
                }
                if (enemigosmuertos==1 && timerenemy > -2)
                {
                    animuerenemigo.DrawFrame(spriteBatch, new Vector2(posenemigomultiple1.X, 202), Color.Red, ienemigo);
                    animenemigo.DrawFrame(spriteBatch, posenemigomultiple2, Color.Red, ienemigo);
                    animenemigo.DrawFrame(spriteBatch, posenemigomultiple3, Color.Red, ienemigo);
                }
                if (enemigosmuertos == 2 && timerenemy > -2)
                { 
                    animuerenemigo.DrawFrame(spriteBatch, new Vector2(posenemigomultiple2.X, 202), Color.Red, ienemigo);
                    animenemigo.DrawFrame(spriteBatch, posenemigomultiple3, Color.Red, ienemigo);
                }
                if (enemigosmuertos == 3 && timerenemy > -2)
                {
                    animuerenemigo.DrawFrame(spriteBatch, new Vector2(posenemigomultiple3.X, 202), Color.Red, ienemigo);
                }
                //si muere, vuelve a aparecer otro enemigo
                if (timerenemy == -1 && enemigosmultiples == false)
                {
                    posenemigo.X += posicionenemiga.Next(700,1500);
                    countdrawenemy = 0;
                }
                if (timerenemy == -1 && enemigosmuertos==1)
                {
                    posenemigomultiple1.X += 1000;
                    countdrawenemy = 0;
                }
                if (timerenemy == -1 && enemigosmuertos == 2)
                {
                    posenemigomultiple2.X += 1000;
                    countdrawenemy = 0;
                }
                if (timerenemy == -1 && enemigosmuertos == 3)
                {
                    posenemigomultiple3.X += 1000;
                    countdrawenemy = 0;
                }
                //Dibujando al final boss
                if (fasesdelboss==0)
                    spriteBatch.Draw(tboss, posboss, new Rectangle(0,0,59,74), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                if ((fasesdelboss <=65)&&(correfondo==-3050))//65 vueltas es el tiempo que tarda en hacer su presentación
                {
                    animboss.DrawFrame(spriteBatch, posboss, Color.White, ienemigo);
                    fasesdelboss++;
                }
                if ((fasesdelboss == 66))
                {
                    spriteBatch.Draw(tboss, posboss, rectangulo_bossparado, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                    //enemigos múltiples
                    cronometroenemigosmultiples++;
                    if (cronometroenemigosmultiples == 300)
                    {
                        fasesdelboss = 67;
                        enemigosmultiples = true;//aparecerán 3 lacayos dl boss final
                    }
                }
                if (fasesdelboss >= 67 && fasesdelboss < 120)
                {
                    //levanta la espada por un breve momento
                    spriteBatch.Draw(tbossdalaorden, new Vector2(posboss.X, 163), new Rectangle(0, 0, 59, 88), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                    fasesdelboss++;
                    if (fasesdelboss == 120)
                        fasesdelboss = 66;
                }
                
            
                if (der == true)
                    animPj.DrawFrame(spriteBatch, coordes_Pj, cPj, iPj);
                if (izq == true)
                    animPj.DrawFrame(spriteBatch, coordes_Pj, cPj, iPj);
                if ((izq == false) && (der == false))
                    spriteBatch.Draw(tPj, coordes_Pj, rectangulo_Pj, cPj, 0, Vector2.Zero, 1, iPj, 0);
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {

                    spriteBatch.Draw(tPjdisparo, coordes_Pj, rectangulo_Pj, cPj, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 0);
                }


                DrawShots(spriteBatch);
             
            }
            if (menu==4)
                spriteBatch.Draw(tmenu[menu], Vector2.Zero, Color.White);

            if (menu == 5)
                spriteBatch.Draw(tmenu[menu], Vector2.Zero, Color.White);
            if (menu==6)
            {
                menu = 6;
              
         
                spriteBatch.Draw(tmenu[menu], Vector2.Zero, Color.White);

            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }

}