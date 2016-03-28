#region Using Statements
using MarkLight.Views.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
#endregion

namespace MarkLight.Examples.UI.Basic
{
    /// <summary>
    /// Example demonstrating how content can be organized in a tab panel.
    /// </summary>
    [HideInPresenter]
    public class TabPanelExample : UIView
    {
        #region Fields

        public TabPanel TabPanel;

        #endregion

        #region Methods

        /// <summary>
        /// Changes the tab orientation based on the radio button selection.
        /// </summary>
        public void SetTabOrientation(RadioButton source)
        {
            switch (source.Text)
            {
                default:
                case "Horizontal TopLeft":
                    TabPanel.TabListOrientation.Value = ElementOrientation.Horizontal;
                    TabPanel.TabListAlignment.Value = ElementAlignment.TopLeft;
                    break;
                case "Horizontal Top":
                    TabPanel.TabListOrientation.Value = ElementOrientation.Horizontal;
                    TabPanel.TabListAlignment.Value = ElementAlignment.Top;
                    break;
                case "Horizontal TopRight":
                    TabPanel.TabListOrientation.Value = ElementOrientation.Horizontal;
                    TabPanel.TabListAlignment.Value = ElementAlignment.TopRight;
                    break;

                case "Horizontal BottomLeft":
                    TabPanel.TabListOrientation.Value = ElementOrientation.Horizontal;
                    TabPanel.TabListAlignment.Value = ElementAlignment.BottomLeft;
                    break;
                case "Horizontal Bottom":
                    TabPanel.TabListOrientation.Value = ElementOrientation.Horizontal;
                    TabPanel.TabListAlignment.Value = ElementAlignment.Bottom;
                    break;
                case "Horizontal BottomRight":
                    TabPanel.TabListOrientation.Value = ElementOrientation.Horizontal;
                    TabPanel.TabListAlignment.Value = ElementAlignment.BottomRight;
                    break;

                case "Vertical TopLeft":
                    TabPanel.TabListOrientation.Value = ElementOrientation.Vertical;
                    TabPanel.TabListAlignment.Value = ElementAlignment.TopLeft;
                    break;
                case "Vertical Left":
                    TabPanel.TabListOrientation.Value = ElementOrientation.Vertical;
                    TabPanel.TabListAlignment.Value = ElementAlignment.Left;
                    break;
                case "Vertical BottomLeft":
                    TabPanel.TabListOrientation.Value = ElementOrientation.Vertical;
                    TabPanel.TabListAlignment.Value = ElementAlignment.BottomLeft;
                    break;

                case "Vertical TopRight":
                    TabPanel.TabListOrientation.Value = ElementOrientation.Vertical;
                    TabPanel.TabListAlignment.Value = ElementAlignment.TopRight;
                    break;
                case "Vertical Right":
                    TabPanel.TabListOrientation.Value = ElementOrientation.Vertical;
                    TabPanel.TabListAlignment.Value = ElementAlignment.Right;
                    break;
                case "Vertical BottomRight":
                    TabPanel.TabListOrientation.Value = ElementOrientation.Vertical;
                    TabPanel.TabListAlignment.Value = ElementAlignment.BottomRight;
                    break;
            }
        }

        /// <summary>
        /// Initializes the view.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            // disable unsupported tab alignments when using the toon theme
            if (Theme == "Toon")
            {
                this.ForEachChild<RadioButton>(x =>
                {
                    var text = x.Text.Value;
                    if (!text.StartsWith("Horizontal Top"))
                    {
                    // disable radio button
                    x.IsDisabled.Value = true;
                    }
                });
            }
        }

        #endregion
    }    
}

