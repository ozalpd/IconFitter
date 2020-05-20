using IconFitter.Commands;
using IconFitter.Models;
using IconLib;
using System;
using System.ComponentModel;

namespace IconFitter.ViewModels
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
                RaisePropertyChanged("ElapsedTimeString");
            }
        }
        private TimeSpan? _elapsedTime;



        public AppendResizeCommand AppendResize
        {
            set { _appendResize = value; }
            get
            {
                if (_appendResize == null)
                    _appendResize = new AppendResizeCommand(this);
                return _appendResize;
            }
        }
        private AppendResizeCommand _appendResize;

        public string ElapsedTimeString
        {
            get
            {
                return ElapsedTime.ToElapsedString();
            }
        }


        public ExecuteResizeWorks ExecuteResizeWorks
        {
            set { _execResizeWorks = value; }
            get
            {
                if (_execResizeWorks == null)
                    _execResizeWorks = new ExecuteResizeWorks(this);
                return _execResizeWorks;
            }
        }
        private ExecuteResizeWorks _execResizeWorks;


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

        public OpenCommand OpenImageCommand
        {
            get
            {
                if (_openImageCommand == null)
                    _openImageCommand = new OpenCommand(this, FileType.ImageFile);
                return _openImageCommand;
            }
        }
        private OpenCommand _openImageCommand;
        public OpenCommand OpenResizeWorkSetCmd
        {
            get
            {
                if (_openResizeWorkSetCmd == null)
                    _openResizeWorkSetCmd = new OpenCommand(this, FileType.ResizeWorkSet);
                return _openResizeWorkSetCmd;
            }
        }
        private OpenCommand _openResizeWorkSetCmd;


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



        public SaveResizeWorks SaveResizeWorks
        {
            set { _saveResizeWorks = value; }
            get
            {
                if (_saveResizeWorks == null)
                    _saveResizeWorks = new SaveResizeWorks(this);
                return _saveResizeWorks;
            }
        }
        private SaveResizeWorks _saveResizeWorks;



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
