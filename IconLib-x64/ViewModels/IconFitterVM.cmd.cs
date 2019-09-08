using IconLib.Commands;
using IconLib.Models;
using System;
using System.ComponentModel;
using System.IO;

namespace IconLib.ViewModels
{
    public partial class IconFitterVM : INotifyPropertyChanged
    {
        public TimeSpan? ElapsedTime
        {
            get { return _elapsedTime; }
            set
            {
                _elapsedTime = value;
                RaisePropertyChanged("ElapsedTime");
            }
        }
        private TimeSpan? _elapsedTime;


        public MakeWin32Icon MakeWin32Icon
        {
            get
            {
                if (_makeWin32icon == null)
                    _makeWin32icon = new MakeWin32Icon(this);
                return _makeWin32icon;
            }
        }
        private MakeWin32Icon _makeWin32icon;

        public OpenCommand OpenCommand
        {
            get
            {
                if (_openCommand == null)
                    _openCommand = new OpenCommand(this);
                return _openCommand;
            }
        }
        private OpenCommand _openCommand;

        public OptimizeTargetCommand OptimizeTargetImage
        {
            get
            {
                if (_optimizeTargImg == null)
                    _optimizeTargImg = new OptimizeTargetCommand(this);
                return _optimizeTargImg;
            }
        }
        private OptimizeTargetCommand _optimizeTargImg;


        public ResizeCommand ResizeCommand
        {
            get
            {
                if (_resizeCmd == null)
                    _resizeCmd = new ResizeCommand(this);
                return _resizeCmd;
            }
        }
        private ResizeCommand _resizeCmd;



        protected virtual void RaisePropertyChanged(string propertyName)
        {
            if (!string.IsNullOrEmpty(propertyName) && PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
