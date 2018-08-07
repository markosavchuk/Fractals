using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace FractalsApp.ViewModels {
    public class MainPageViewModel : ViewModelBase {

        public ICommand PaintCommand { get; private set; }

        public MainPageViewModel ( INavigationService navigationService )
            : base( navigationService ) {
            Title = "Main Page";

            PaintCommand = new DelegateCommand<SKPaintSurfaceEventArgs>( Draw );
        }

        void Draw ( SKPaintSurfaceEventArgs canvasArgs ) {

        }

    }
}
