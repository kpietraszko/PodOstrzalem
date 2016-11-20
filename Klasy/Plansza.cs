﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SFML.Graphics;
using SFML.System;

namespace ProjektObiektowe
{
	static class Plansza
	{
		private static List<Sprite> Sciany = new List<Sprite>();
		public static List<Sprite> sciany { get { return Sciany; } }
		static public void StworzZBitmapy(Bitmap mapaPlanszy, Bitmap teksturaSciany) //mapa 32x16
		{
			Texture TeksturaSciany = new Texture(Rysowanie.BitmapaNaByte(teksturaSciany));
			TeksturaSciany.Smooth = true;
			Sprite temp;
			Sciany = new List<Sprite>();
			int wysokoscMapy = mapaPlanszy.Size.Height;
			int szerokoscMapy = mapaPlanszy.Size.Width;
			for (int i = 0; i < wysokoscMapy; i++)
				for (int j = 0; j < szerokoscMapy; j++)
					if (mapaPlanszy.GetPixel(j, i).GetBrightness() == 0) //plansza z mapy-obrazka
					{
						temp = new Sprite(TeksturaSciany);
						temp.Position = new SFML.System.Vector2f(j * TeksturaSciany.Size.X, i * TeksturaSciany.Size.Y);
						Rysowanie.Rysowane.Add(temp);
						Sciany.Add(temp);
					}
		}

	}
}
