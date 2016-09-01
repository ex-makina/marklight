#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
#endregion

namespace MarkLight
{
    /// <summary>
    /// Binding value observer.
    /// </summary>
    public class BindingValueObserver : ValueObserver
    {
        #region Fields

        public List<BindingSource> Sources;
        public ViewFieldData Target;
        public BindingType BindingType;
        public string FormatString;
        public MethodInfo TransformMethod;
        public View ParentView;        

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes static instance of the class.
        /// </summary>
        public BindingValueObserver()
        {
            Sources = new List<BindingSource>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Notifies the binding value observer that value has changed.
        /// </summary>
        public override bool Notify(HashSet<ViewFieldData> callstack)
        {
            try
            {
                base.Notify(callstack);
                bool hasValue;

                // check if target has been destroyed
                if (Target.SourceView == null)
                {
                    return false; 
                }

                //Debug.Log(String.Format("Source(s) updated. Updating target field: {0}", Target.ViewFieldPath));
                switch (BindingType)
                {
                    default:
                    case BindingType.SingleBinding:
                        var value = Sources[0].GetValue(out hasValue);
                        if (hasValue)
                        {
                            // use to debug
                            //Debug.Log(String.Format("Propagating Value \"{4}\": {0}.{1} -> {2}.{3}", Sources[0].ViewFieldData.TargetView.ViewTypeName, Sources[0].ViewFieldData.TargetViewFieldPath,
                            //    Target.TargetView.ViewTypeName, Target.TargetViewFieldPath, value.ToString()));

                            // set value
                            Target.SetValue(value, callstack); 
                        }
                        break;

                    case BindingType.MultiBindingTransform:
                        object[] pars = Sources.Count > 0 ? new object[Sources.Count] : null;
                        for (int i = 0; i < pars.Length; ++i)
                        {
                            pars[i] = Sources[i].GetValue(out hasValue);
                        }

                        // set transformed value
                        if (TransformMethod.IsStatic)
                        {
                            Target.SetValue(TransformMethod.Invoke(null, pars), callstack);
                        }
                        else
                        {
                            Target.SetValue(TransformMethod.Invoke(ParentView, pars), callstack);                            
                        }
                        break;

                    case BindingType.MultiBindingFormatString:
                        object[] formatPars = Sources.Count > 0 ? new object[Sources.Count] : null;
                        for (int i = 0; i < formatPars.Length; ++i)
                        {
                            formatPars[i] = Sources[i].GetValue(out hasValue);
                        }

                        // set format string value
                        Target.SetValue(String.Format(FormatString, formatPars), callstack);
                        break;

                }
            }
            catch (Exception e)
            {
                PrintBindingError(e);
            }

            return true;
        }

        /// <summary>
        /// Prints a formatted error message.
        /// </summary>
        private void PrintBindingError(Exception e)
        {
            switch (BindingType)
            {
                default:
                case BindingType.SingleBinding:
                    Debug.LogError(String.Format("[MarkLight] Exception thrown when propagating single binding value from source \"{0}\" to target \"{1}.{2}\": {3}", Sources[0].BindingSourceString, Target.SourceView.ViewTypeName, Target.ViewFieldPath, Utils.GetError(e)));
                    break;

                case BindingType.MultiBindingTransform:
                case BindingType.MultiBindingFormatString:
                    StringBuilder sb = new StringBuilder();
                    foreach (var source in Sources)
                    {
                        if (source != Sources[0])
                        {
                            sb.Append(", ");
                        }

                        sb.AppendFormat(source.BindingSourceString);
                    }

                    Debug.LogError(String.Format("[MarkLight] Exception thrown when propagating single binding value from sources \"{0}\" to target \"{1}.{2}\": {3}", sb.ToString(), Target.SourceView.ViewTypeName, Target.ViewFieldPath, Utils.GetError(e)));
                    break;
            }
        }    

        #endregion
    }
}
