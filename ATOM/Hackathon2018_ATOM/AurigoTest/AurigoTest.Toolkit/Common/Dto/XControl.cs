using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml.Serialization;

namespace AurigoTest.Toolkit.Common.Dto
{
    [XmlRoot("Control")]
    public class XControl
    {
        [XmlAttribute("Name")]
        public string Name;

        /// <summary>
        /// The name to render on the display surface.  If caption is not set then the default name is retrieved from the Name attribute
        /// of the control.  Therefore to make a control have no title you must set Caption to "".
        /// </summary>
        [XmlAttribute("Caption")]
        public string Caption;

        /// <summary>
        /// defines the type of the control in the database.  DBType must closely match the Type attribute.
        /// </summary>
        [XmlAttribute("DBType"), DefaultValue("")]
        public string DBType;


        /// <summary>
        /// if set for a dropdownlist or radiobutton ist it defines how the field will be filled from the database
        /// </summary>
        [XmlAttribute("DataSource"), DefaultValue("")]
        public string DataSource;


        /// <summary>
        /// Format allows the control to be formatted in various ways using masked input text.
        ///         /// </summary>
        [XmlAttribute("Format"), DefaultValue("")]
        public string Format;


        /// <summary>
        /// For the Date and the numeric control if null should be allowed
        /// </summary>
        [XmlAttribute("AllowNull"), DefaultValue(false)]
        public bool AllowNull = false;


        [XmlAttribute("ShowInGrid"), DefaultValue(true)]
        public bool ShowInGrid = true;


        /// <summary>
        /// The type of control derived from the ControlType enumeration.  This sets how a control will be rendered by the renderers.  In
        /// general it is completely up to the renderer to decide how to instantiate a control type on the display surface.
        /// </summary>
        [XmlAttribute("Type")]
        public ControlType Type;

        /// <summary>
        /// specifies a static value for the control.  This is typeically used for Display only fields to put some simple
        /// text on the display.  For more complicated text use the Text control container.  The Value field can also be used
        /// to retrieve system level information during display processing.  The current valid format values are:
        /// 
        /// {CURRENTUSER}
        /// {CURRENTUSERNAME}
        /// {CURRENTDATE}
        /// {CURRENTDATETIME}
        /// {CURRENTTIME}
        /// {PROJECTNAME}
        /// {PROJECTCODE}
        /// {CONTRACTNAME}
        /// {CONTRACTCODE}
        /// {PRIMECONTRACTOR}
        /// {_FORMULA: formula}
        /// {_REQUEST: state info field}
        /// {_DB: db expression}
        /// {_Picker: [name Of Thepicker that is defined in this xml]}
        /// "any static text"
        /// </summary>
        [XmlAttribute("Value"), DefaultValue("")]
        public string Value;

    }
}

