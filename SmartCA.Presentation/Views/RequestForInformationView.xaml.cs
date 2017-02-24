﻿using System;
using System.Collections;
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
using System.Windows.Shapes;
using SmartCA.Presentation.ViewModels;
using SmartCA.Infrastructure.UI;

namespace SmartCA.Presentation.Views
{
    /// <summary>
    /// Interaction logic for RequestForInformationView.xaml
    /// </summary>

    public partial class RequestForInformationView : Window, IView
    {
        public RequestForInformationView()
        {
            InitializeComponent();
            this.DataContext = new RequestForInformationViewModel(this);
        }
    }
}
