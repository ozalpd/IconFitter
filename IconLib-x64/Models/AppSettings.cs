using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IconLib.Models
{
    public class AppSettings : PersistableBase
    {
        public WindowPosition MainWindowPosition
        {
            set { _mainWindowPosition = value; }
            get
            {
                if (_mainWindowPosition == null)
                {
                    _mainWindowPosition = new WindowPosition();
                }
                return _mainWindowPosition;
            }
        }
        WindowPosition _mainWindowPosition;


        public static AppSettings OpenFromFile(string fileName)
        {
            return (AppSettings)GetInstanceFromFile(fileName, typeof(AppSettings));
        }
    }
}
