using IconLib.ViewModels;
using IconFitter64.Commands;
using System.IO;

namespace IconFitter64.ViewModels
{
    public class IconFitterVM : AbstractViewModel
    {
        public bool OptimizeTarget
        {
            get
            {
                if (_optimizeTarget == null)
                    _optimizeTarget = true;
                return _optimizeTarget.Value;
            }
            set
            {
                if (_optimizeTarget != value)
                {
                    _optimizeTarget = value;
                    RaisePropertyChanged("OptimizeTarget");
                }
            }
        }
        private bool? _optimizeTarget;


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



        public override string GetTargetDirectory()
        {
            return Path.Combine(base.GetTargetDirectory(), "IconFitter");
        }
    }
}
