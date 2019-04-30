using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iS3.Core
{
    //Summary
    //     the base view interface for iS3 View
    public interface IBaseView
    {
        //Summary
        //     the Name of View,usually display in the control header
        string ViewName { get; }
        //Summary
        //     the Unique ID of View, for Searching usage
        string ViewID { get;  }
        //Summay
        //      show as Project loaded
        bool DefaultShow { get; }
        // Summary
        //       the function for set data
        bool SetData(params object[] objs);
        //Summary
        //       the display position of view
        ViewLocation ViewPos { get; }
    }
    public enum ViewLocation
    {
        Top,
        Left,
        Center,
        Bottom,
        RightCenter,
        RightBottom,
        Floating
    }
}
