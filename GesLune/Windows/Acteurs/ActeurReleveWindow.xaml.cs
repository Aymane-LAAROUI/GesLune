﻿using GesLune.Models;
using GesLune.ViewModels.Acteurs;
using System.Windows;

namespace GesLune.Windows.Acteurs
{
    public partial class ActeurReleveWindow : Window
    {
        private readonly ActeurReleveViewModel viewModel;
        public ActeurReleveWindow(Model_Acteur Acteur)
        {
            InitializeComponent();
            viewModel = new(Acteur);
            this.DataContext = viewModel;
        }
    }
}
