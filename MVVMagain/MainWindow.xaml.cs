﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MVVMagain.Models;
using MVVMagain.ViewModels;

namespace MVVMagain
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            Game game = new Game();
            
            StartViewModel startViewModel = new StartViewModel(game);
            startViewModel.CurrentGame = game;
            this.DataContext = startViewModel;

            GameViewModel gameViewModel = new GameViewModel(game);
            this.GameView.DataContext = gameViewModel;
            this.CategoryView.DataContext = gameViewModel;

            Questions questions = new Questions();
            QuestionsViewModel questionsViewModel = new QuestionsViewModel(questions);
            this.QuestionsView.DataContext = questionsViewModel;
            this.QuestionsOpenView.DataContext = questionsViewModel;

           // this.DataContext = gameViewModel;
        }

    }
}
