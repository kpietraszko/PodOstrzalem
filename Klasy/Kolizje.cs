﻿using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektObiektowe
{
	class Kolizje
	{
		public static void ReakcjaPostaciNaKolizje()//Vector2f v, FloatRect APrzed) //nie dziala przy kolizji z gorną śc. przy ruchu skosnym (tzn v.X==v.Y)
		{
			float[] Odleglosci = new float[4];
			int IndeksNajmniejszej = 0;
			Vector2f Wypchniecie = new Vector2f(0f, 0f);
			Vector2f NoweWypchniecie = new Vector2f(0f, 0f);
			FloatRect A = LogikaGry.Instancja.Gracz.ProstokatKolizji;
			Vector2f PozycjaPostaci = LogikaGry.Instancja.Gracz.Pozycja;
			FloatRect[] DotknieteSciany;
			if (KolizjaZeScianami(A, out DotknieteSciany))
				{
				//sciana.Color = SFML.Graphics.Color.Red;
				foreach (var sciana in DotknieteSciany)
				{
					Odleglosci[0] = sciana.Left - (A.Left + A.Width);
					Odleglosci[1] = (sciana.Left + sciana.Width) - A.Left;
					Odleglosci[2] = sciana.Top - (A.Top + A.Height);
					Odleglosci[3] = (sciana.Top + sciana.Height) - A.Top;

					IndeksNajmniejszej = 0;
					for (int i = 0; i < 4; i++)
						if (Math.Abs(Odleglosci[i]) < Math.Abs(Odleglosci[IndeksNajmniejszej])) IndeksNajmniejszej = i;


					NoweWypchniecie = IndeksNajmniejszej <= 1 ? new Vector2f(Odleglosci[IndeksNajmniejszej], 0f)
						: new Vector2f(0f, Odleglosci[IndeksNajmniejszej]);
					if (Math.Abs(NoweWypchniecie.X) > Math.Abs(Wypchniecie.X)) Wypchniecie.X = NoweWypchniecie.X;
					if (Math.Abs(NoweWypchniecie.Y) > Math.Abs(Wypchniecie.Y)) Wypchniecie.Y = NoweWypchniecie.Y;
				}

				}
				//else sciana.Color = SFML.Graphics.Color.White;


			//return pozycja + Wypchniecie;
			LogikaGry.Instancja.Gracz.Pozycja += Wypchniecie;
		}
		public static bool KolizjaZeScianami(FloatRect A, out FloatRect[] ScianyDotkniete)
		{
			List<FloatRect> dotkniete = new List<FloatRect>();
			bool kolizja = false;
			bool kolidowal = false;
			foreach (var sciana in Plansza.sciany)
			{
				FloatRect B = sciana.GetGlobalBounds();
				kolizja =  Kolizja(A, B);
				if (kolizja)
				{
					dotkniete.Add(B);
					kolidowal = true;
				}
			}
			ScianyDotkniete = dotkniete.ToArray();
			return kolidowal;
		}
		public static bool Kolizja(FloatRect a, FloatRect b)
		{
			//true jesli prostakaty na siebie nachodzą, czyli nie mozna ich rozgraniczyć linią pion./poziom.
			return (!((a.Left + a.Width) < b.Left || (b.Left + b.Width) < a.Left
					|| (a.Top + a.Height) < b.Top || (b.Top + b.Height) < a.Top)); //Twierdzenie o osi rozdzielającej (SAT)
		}
		public static bool CzyPunktWScianie(Vector2f pkt)
		{
			return false;
		}
		static bool FloatRownyZero(float a)
		{
			return Math.Abs(a) < 0.001;
		}
	}
}
