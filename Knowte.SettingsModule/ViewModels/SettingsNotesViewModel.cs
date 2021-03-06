﻿using Knowte.Core.Base;
using Knowte.Core.Helpers;
using Knowte.Core.Settings;
using Knowte.Core.Utils;
using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace Knowte.SettingsModule.ViewModels
{
    public class SettingsNotesViewModel : BindableBase
    {
        #region Variables
        private ObservableCollection<FontSizeCorrection> fontSizeCorrections;
        private FontSizeCorrection selectedFontSizeCorrection;
        private int previewFontSize;
        private bool checkBoxEscapeChecked;
        #endregion

        #region Properties
        public ObservableCollection<FontSizeCorrection> FontSizeCorrections
        {
            get { return this.fontSizeCorrections; }
            set { SetProperty<ObservableCollection<FontSizeCorrection>>(ref this.fontSizeCorrections, value); }
        }

        public FontSizeCorrection SelectedFontSizeCorrection
        {
            get { return this.selectedFontSizeCorrection; }
            set
            {
                XmlSettingsClient.Instance.Set<int>("Notes", "FontSizeCorrection", value.Correction);
                PreviewFontSize = Defaults.DefaultNoteFontSize + value.Correction;
                SetProperty<FontSizeCorrection>(ref this.selectedFontSizeCorrection, value);
            }
        }

        public int PreviewFontSize
        {
            get { return this.previewFontSize; }
            set { SetProperty<int>(ref this.previewFontSize, value); }
        }

        public bool CheckBoxEscapeChecked
        {
            get { return this.checkBoxEscapeChecked; }
            set
            {
                XmlSettingsClient.Instance.Set<bool>("Notes", "PressingEscapeClosesNotes", value);
                SetProperty<bool>(ref this.checkBoxEscapeChecked, value);
            }
        }
        #endregion

        #region Construction
        public SettingsNotesViewModel()
        {
            this.LoadFontSizeCorrections();
            this.LoadCheckBoxStates();
        }
        #endregion

        #region Private
        private void LoadCheckBoxStates()
        {
            this.CheckBoxEscapeChecked = XmlSettingsClient.Instance.Get<bool>("Notes", "PressingEscapeClosesNotes");
        }


        private void LoadFontSizeCorrections()
        {
            this.FontSizeCorrections = new ObservableCollection<FontSizeCorrection>();
            this.FontSizeCorrections.Add(new FontSizeCorrection
            {
                Name = ResourceUtils.GetStringResource("Language_Normal"),
                Correction = 0
            });
            this.FontSizeCorrections.Add(new FontSizeCorrection
            {
                Name = ResourceUtils.GetStringResource("Language_Large"),
                Correction =3
            });
            this.FontSizeCorrections.Add(new FontSizeCorrection
            {
                Name = ResourceUtils.GetStringResource("Language_Larger"),
                Correction = 6
            });

            foreach (FontSizeCorrection fzc in this.FontSizeCorrections)
            {
                if (XmlSettingsClient.Instance.Get<int>("Notes", "FontSizeCorrection") == fzc.Correction)
                {
                    this.SelectedFontSizeCorrection = fzc;
                }
            }
        }
        #endregion
    }
}