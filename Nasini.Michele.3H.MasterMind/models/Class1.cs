﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Nasini.Michele._3H.MasterMind.models
{
    class SequenzaColori
    {
        public Brush colore1 { get; set; }

        public Brush colore2 { get; set; }

        public Brush colore3 { get; set; }

        public Brush colore4 { get; set; }

        public int colorePos { get; set; }

        public int soloColore { get; set; }

    }
}
