using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{

        // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class ATM
        {

            private string קוד_בנקField;

            private string שם_בנקField;

            private string קוד_סניףField;

            private string כתובת_הATMField;

            private string ישובField;

            private string עמלהField;

            private string סוג_ATMField;

            private string מיקום_הATM_ביחס_לסניףField;

            private string גישה_לנכיםField;

            private string קואורדינטת_XField;

            private string קואורדינטת_YField;

            /// <remarks/>
            public string קוד_בנק
            {
                get
                {
                    return this.קוד_בנקField;
                }
                set
                {
                    this.קוד_בנקField = value;
                }
            }

            /// <remarks/>
            public string שם_בנק
            {
                get
                {
                    return this.שם_בנקField;
                }
                set
                {
                    this.שם_בנקField = value;
                }
            }

            /// <remarks/>
            public string קוד_סניף
            {
                get
                {
                    return this.קוד_סניףField;
                }
                set
                {
                    this.קוד_סניףField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("כתובת_ה-ATM")]
            public string כתובת_הATM
            {
                get
                {
                    return this.כתובת_הATMField;
                }
                set
                {
                    this.כתובת_הATMField = value;
                }
            }

            /// <remarks/>
            public string ישוב
            {
                get
                {
                    return this.ישובField;
                }
                set
                {
                    this.ישובField = value;
                }
            }

            /// <remarks/>
            public string עמלה
            {
                get
                {
                    return this.עמלהField;
                }
                set
                {
                    this.עמלהField = value;
                }
            }

            /// <remarks/>
            public string סוג_ATM
            {
                get
                {
                    return this.סוג_ATMField;
                }
                set
                {
                    this.סוג_ATMField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("מיקום_ה-ATM_ביחס_לסניף")]
            public string מיקום_הATM_ביחס_לסניף
            {
                get
                {
                    return this.מיקום_הATM_ביחס_לסניףField;
                }
                set
                {
                    this.מיקום_הATM_ביחס_לסניףField = value;
                }
            }

            /// <remarks/>
            public string גישה_לנכים
            {
                get
                {
                    return this.גישה_לנכיםField;
                }
                set
                {
                    this.גישה_לנכיםField = value;
                }
            }

            /// <remarks/>
            public string קואורדינטת_X
            {
                get
                {
                    return this.קואורדינטת_XField;
                }
                set
                {
                    this.קואורדינטת_XField = value;
                }
            }

            /// <remarks/>
            public string קואורדינטת_Y
            {
                get
                {
                    return this.קואורדינטת_YField;
                }
                set
                {
                    this.קואורדינטת_YField = value;
                }
            }
        }


}


