﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Diagnostics;
using SFML.Window;
using SFML.Graphics;
using SFML.System;

namespace ProjektObiektowe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        int SzerokoscPowierzchni;
        double WysokoscOknaGry;
        public TextBlock Napis;
        public MainWindow()
        {

            InitializeComponent();
            this.DataContext = LogikaGry.Instancja;
            // tabsy.TabIndex = 0; //fail
            Rysowanie.PowierzchniaRys = DrawSurface;
            tabsy.Loaded += WczytaloSie;
            tabsy.SelectionChanged += TabZmieniony;
            Napis = NapisGameOver;
            //dopiero po calkowitym zaladowaniu powierzchniarys ma wlasciwy rozmiar
        }

        private void TabZmieniony(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl && !TabGry.IsSelected)
            {
                grid.RowDefinitions[0].Height = new GridLength(SzerokoscPowierzchni * 640 / 1280f - 20f);
            }
            //if (TabGry.IsSelected)
                this.Height = WysokoscOknaGry;
            GridMenu.Height= SzerokoscPowierzchni * 640 / 1280f -20f;
        }
        private void WczytaloSie(object sender, RoutedEventArgs e)
        {
            SzerokoscPowierzchni = DrawSurface.Width;
            WysokoscOknaGry = this.Height;
            tabsy.SelectedIndex = 0;
        }
        private void StartWcisnieto(object sender, EventArgs e)
        {
            tabsy.SelectedItem = TabGry;
            LogikaGry.Instancja.RozpocznijGre();
        }
        private void WyjscieWcisnieto(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
