using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using HavenheimManager.Containers;
using HavenheimManager.Editors;
using HavenheimManager.Enums;
using HavenheimManager.Extensions;
using Condition = HavenheimManager.Enums.Condition;

namespace HavenheimManager.Handlers
{
    public class FeatHandler
    {
        public FeatHandler(MainWindowViewModel vm)
        {
            _vm = vm;
        }

        private readonly MainWindowViewModel _vm;

        // Feat Filter Lists
        private HashSet<Core> CoreFeatFilters = new HashSet<Core>();
        private HashSet<Skill> SkillFeatFilters = new HashSet<Skill>();
        private HashSet<Class> ClassFeatFilters = new HashSet<Class>();
        private HashSet<Combat> CombatFeatFilters = new HashSet<Combat>();
        private HashSet<Role> RoleFeatFilters = new HashSet<Role>();
        private HashSet<Magic> MagicFeatFilters = new HashSet<Magic>();
        private HashSet<Bonus> BonusFeatFilters = new HashSet<Bonus>();
        private HashSet<Condition> ConditionFeatFilters = new HashSet<Condition>();
        private HashSet<Source> SourceFeatFilters = new HashSet<Source>();
        private HashSet<string> CustomFeatFilters = new HashSet<string>();

        private bool _activeFeatFilters => CoreFeatFilters.Any() ||
                                           SkillFeatFilters.Any() ||
                                           ClassFeatFilters.Any() ||
                                           CombatFeatFilters.Any() ||
                                           RoleFeatFilters.Any() ||
                                           ConditionFeatFilters.Any() ||
                                           SourceFeatFilters.Any() ||
                                           CustomFeatFilters.Any() ||
                                           MagicFeatFilters.Any() ||
                                           BonusFeatFilters.Any();

        // Feat Checkbox Actions
        internal void FeatCoreFilterAction(object arg)
        {
            if (arg is string filter)
            {
                Core toggleCore = filter.StringToCore();

                if (CoreFeatFilters.Contains(toggleCore))
                {
                    CoreFeatFilters.Remove(toggleCore);
                }
                else
                {
                    CoreFeatFilters.Add(toggleCore);
                }

                ApplyFeatFilters();
            }
        }

        internal void FeatSkillFilterAction(object arg)
        {
            if (arg is string filter)
            {
                Skill toggleSkill = filter.StringToSkill();

                if (SkillFeatFilters.Contains(toggleSkill))
                {
                    SkillFeatFilters.Remove(toggleSkill);
                }
                else
                {
                    SkillFeatFilters.Add(toggleSkill);
                }

                ApplyFeatFilters();
            }
        }

        internal void FeatClassFilterAction(object arg)
        {
            if (arg is string filter)
            {
                Class toggleClass = filter.StringToClass();

                if (ClassFeatFilters.Contains(toggleClass))
                {
                    ClassFeatFilters.Remove(toggleClass);
                }
                else
                {
                    ClassFeatFilters.Add(toggleClass);
                }

                ApplyFeatFilters();
            }
        }

        internal void FeatCombatFilterAction(object arg)
        {
            if (arg is string filter)
            {
                Combat toggleCombat = filter.StringToCombat();

                if (CombatFeatFilters.Contains(toggleCombat))
                {
                    CombatFeatFilters.Remove(toggleCombat);
                }
                else
                {
                    CombatFeatFilters.Add(toggleCombat);
                }

                ApplyFeatFilters();
            }
        }

        internal void FeatRoleFilterAction(object arg)
        {
            if (arg is string filter)
            {
                Role toggleRole = filter.StringToRole();

                if (RoleFeatFilters.Contains(toggleRole))
                {
                    RoleFeatFilters.Remove(toggleRole);
                }
                else
                {
                    RoleFeatFilters.Add(toggleRole);
                }

                ApplyFeatFilters();
            }
        }

        internal void FeatMagicFilterAction(object arg)
        {
            if (arg is string filter)
            {
                Magic toggleMagic = filter.StringToMagic();

                if (MagicFeatFilters.Contains(toggleMagic))
                {
                    MagicFeatFilters.Remove(toggleMagic);
                }
                else
                {
                    MagicFeatFilters.Add(toggleMagic);
                }

                ApplyFeatFilters();
            }
        }

        internal void FeatBonusFilterAction(object arg)
        {
            if (arg is string filter)
            {
                Bonus toggleBonus = filter.StringToBonus();

                if (BonusFeatFilters.Contains(toggleBonus))
                {
                    BonusFeatFilters.Remove(toggleBonus);
                }
                else
                {
                    BonusFeatFilters.Add(toggleBonus);
                }

                ApplyFeatFilters();
            }
        }

        internal void FeatConditionFilterAction(object arg)
        {
            if (arg is string filter)
            {
                Condition toggleCondition = filter.StringToCondition();

                if (ConditionFeatFilters.Contains(toggleCondition))
                {
                    ConditionFeatFilters.Remove(toggleCondition);
                }
                else
                {
                    ConditionFeatFilters.Add(toggleCondition);
                }

                ApplyFeatFilters();
            }
        }

        internal void FeatSourceFilterAction(object arg)
        {
            if (arg is string filter)
            {
                Source toggleSource = filter.StringToSource();

                if (SourceFeatFilters.Contains(toggleSource))
                {
                    SourceFeatFilters.Remove(toggleSource);
                }
                else
                {
                    SourceFeatFilters.Add(toggleSource);
                }

                ApplyFeatFilters();
            }
        }

        internal void FeatCustomFilterAction(object arg)
        {
            if (arg is string filter)
            {
                if (CustomFeatFilters.Contains(filter))
                {
                    CustomFeatFilters.Remove(filter);
                }
                else
                {
                    CustomFeatFilters.Add(filter);
                }

                ApplyFeatFilters();
            }
        }

        internal void ApplyFeatFilters()
        {
            _vm.FilteredFeatList.Clear();
            List<Feat> possibleFeats = (_vm.FeatSearchText != RegexHandler.SearchPlaceholderText && _vm.FeatSearchText != "" ?
                _vm.MasterFeatList.Where(x => x.Name.Sanitize().Contains(_vm.FeatSearchText.Sanitize())).ToList() :
                _vm.MasterFeatList).Where(x => !_vm.FavoriteFeatList.Contains(x) && !_vm.HiddenFeatList.Contains(x) &&
                                               x.Level <= int.Parse(_vm.FeatMaxLevel) && x.Level >= int.Parse(_vm.FeatMinLevel)).ToList();

            foreach (Source filter in SourceFeatFilters)
            {
                possibleFeats = possibleFeats.Where(x => x.Source == filter).ToList();
            }

            foreach (Feat feat in BroadFeatFilter(possibleFeats))
            {
                _vm.FilteredFeatList.Add(feat);
            }
        }

        private IEnumerable<Feat> BroadFeatFilter(List<Feat> possibleFeats)
        {
            if (!_activeFeatFilters)
            {
                foreach (Feat feat in possibleFeats)
                {
                    yield return feat;
                }
                yield break;
            }

            foreach (Feat feat in possibleFeats)
            {
                if (CoreFeatFilters.Any(filter => feat.CoreTags.Contains(filter)))
                {
                    yield return feat;
                    continue;
                }

                if (SkillFeatFilters.Any(filter => feat.SkillTags.Contains(filter)))
                {
                    yield return feat;
                    continue;
                }

                if (ClassFeatFilters.Any(filter => feat.ClassTags.Contains(filter)))
                {
                    yield return feat;
                    continue;
                }

                if (CombatFeatFilters.Any(filter => feat.CombatTags.Contains(filter)))
                {
                    yield return feat;
                    continue;
                }

                if (RoleFeatFilters.Any(filter => feat.RoleTags.Contains(filter)))
                {
                    yield return feat;
                    continue;
                }

                if (MagicFeatFilters.Any(filter => feat.MagicTags.Contains(filter)))
                {
                    yield return feat;
                    continue;
                }

                if (BonusFeatFilters.Any(filter => feat.BonusTags.Contains(filter)))
                {
                    yield return feat;
                    continue;
                }

                if (ConditionFeatFilters.Any(filter => feat.ConditionTags.Contains(filter)))
                {
                    yield return feat;
                    continue;
                }

                if (CustomFeatFilters.Any(filter => feat.CustomTags.Contains(filter)))
                {
                    yield return feat;
                }
            }
        }

        private IEnumerable<Feat> StrictFeatFilter(List<Feat> possibleFeats)
        {
            foreach (Core filter in CoreFeatFilters)
            {
                possibleFeats = possibleFeats.Where(x => x.CoreTags.Contains(filter)).ToList();
            }

            foreach (Skill filter in SkillFeatFilters)
            {
                possibleFeats = possibleFeats.Where(x => x.SkillTags.Contains(filter)).ToList();
            }

            foreach (Class filter in ClassFeatFilters)
            {
                possibleFeats = possibleFeats.Where(x => x.ClassTags.Contains(filter)).ToList();
            }

            foreach (Combat filter in CombatFeatFilters)
            {
                possibleFeats = possibleFeats.Where(x => x.CombatTags.Contains(filter)).ToList();
            }

            foreach (Role filter in RoleFeatFilters)
            {
                possibleFeats = possibleFeats.Where(x => x.RoleTags.Contains(filter)).ToList();
            }

            foreach (Magic filter in MagicFeatFilters)
            {
                possibleFeats = possibleFeats.Where(x => x.MagicTags.Contains(filter)).ToList();
            }

            foreach (Bonus filter in BonusFeatFilters)
            {
                possibleFeats = possibleFeats.Where(x => x.BonusTags.Contains(filter)).ToList();
            }

            foreach (Condition filter in ConditionFeatFilters)
            {
                possibleFeats = possibleFeats.Where(x => x.ConditionTags.Contains(filter)).ToList();
            }

            foreach (string filter in CustomFeatFilters)
            {
                possibleFeats = possibleFeats.Where(x => x.CustomTags.Contains(filter)).ToList();
            }

            foreach (Feat feat in possibleFeats)
            {
                yield return feat;
            }
        }

        internal void UpdateFeatCustomTags()
        {
            _vm.CustomFeatFilterList.Clear();
            foreach (Feat feat in _vm.MasterFeatList)
            {
                if (_vm.FavoriteFeatList.Contains(feat) || _vm.HiddenFeatList.Contains(feat))
                    continue;

                foreach (string tag in feat.CustomTags)
                {
                    if (!_vm.CustomFeatFilterList.Contains(tag))
                    {
                        _vm.CustomFeatFilterList.Add(tag);
                    }
                }
            }
        }

        internal void UpdateFeatReqs()
        {
            foreach (Feat feat in _vm.MasterFeatList)
            {
                string sanitizedName = feat.Name.Sanitize();
                foreach (Feat possibleReq in _vm.MasterFeatList)
                {
                    string possibleFeatSanitizedName = possibleReq.Name.Sanitize();
                    if (sanitizedName == possibleFeatSanitizedName) continue;

                    // Antireqs
                    if (possibleReq.Antireqs.Select(x => x.Sanitize()).Contains(sanitizedName) &&
                        !feat.Antireqs.Select(x => x.Sanitize()).Contains(possibleFeatSanitizedName))
                    {
                        feat.Antireqs.Add(possibleReq.Name);
                    }

                    // Postreqs
                    if (possibleReq.Prereqs.Select(x => x.Sanitize()).Contains(sanitizedName))
                    {
                        feat.Postreqs.Add(possibleReq.Name);
                    }
                }
            }
        }

        internal void AddFavoriteFeatAction(object arg)
        {
            if (_vm.SelectedFeat != null && !_vm.FavoriteFeatList.Contains(_vm.SelectedFeat))
            {
                _vm.FavoriteFeatList.Add(_vm.SelectedFeat);
                _vm.HiddenFeatList.Remove(_vm.SelectedFeat);

                UpdateFeatCustomTags();
                ApplyFeatFilters();
                _vm.SelectedFeat = null;
            }
        }

        internal void AddHiddenFeatAction(object arg)
        {
            if (_vm.SelectedFeat != null && !_vm.HiddenFeatList.Contains(_vm.SelectedFeat))
            {
                _vm.HiddenFeatList.Add(_vm.SelectedFeat);
                _vm.FavoriteFeatList.Remove(_vm.SelectedFeat);

                UpdateFeatCustomTags();
                ApplyFeatFilters();
                _vm.SelectedFeat = null;
            }
        }

        internal void EditFeatAction(object arg)
        {
            try
            {
                if (_vm.SelectedFeat != null)
                {
                    var vm = new FeatViewModel(_vm.SelectedFeat);
                    var configWindow = new FeatView(vm);

                    if (configWindow.ShowDialog() == true)
                    {
                        Feat newFeat = vm.GetFeat();

                        if (_vm.MasterFeatList.Contains(_vm.SelectedFeat))
                        {
                            _vm.MasterFeatList.Remove(_vm.SelectedFeat);
                            _vm.MasterFeatList.Add(newFeat);

                            if (_vm.FavoriteFeatList.Contains(_vm.SelectedFeat))
                            {
                                _vm.FavoriteFeatList.Remove(_vm.SelectedFeat);
                                _vm.FavoriteFeatList.Add(newFeat);
                            }

                            if (_vm.HiddenFeatList.Contains(_vm.SelectedFeat))
                            {
                                _vm.HiddenFeatList.Remove(_vm.SelectedFeat);
                                _vm.HiddenFeatList.Add(newFeat);
                            }

                            UpdateFeatReqs();
                            UpdateFeatCustomTags();
                            ApplyFeatFilters();

                            _vm.SelectedFeat = newFeat;
                        }
                    }
                }
                else
                {
                    string messageBoxText = "No Feat selected to edit";
                    string caption = "Select Feat";
                    MessageBoxButton button = MessageBoxButton.OK;
                    MessageBoxImage icon = MessageBoxImage.Exclamation;
                    MessageBox.Show(messageBoxText, caption, button, icon);
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                string messageBoxText = "Exception when adding Feat";
                string caption = "Exception";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show(messageBoxText, caption, button, icon);
            }

            _vm.RefreshButtonState();
        }

        internal void NewFeatAction(object arg)
        {
            try
            {
                var vm = new FeatViewModel(new Feat());
                var configWindow = new FeatView(vm);

                if (configWindow.ShowDialog() == true)
                {
                    Feat newFeat = vm.GetFeat();
                    if (_vm.MasterFeatList.Select(x => x.Name).Contains(newFeat.Name))
                    {
                        string messageBoxText = "Feat with same name already exists";
                        string caption = "Duplicate";
                        MessageBoxButton button = MessageBoxButton.OK;
                        MessageBoxImage icon = MessageBoxImage.Exclamation;
                        MessageBox.Show(messageBoxText, caption, button, icon);
                    }
                    else
                    {
                        _vm.MasterFeatList.Add(newFeat);

                        UpdateFeatReqs();
                        UpdateFeatCustomTags();
                        ApplyFeatFilters();
                        if (_vm.FilteredFeatList.Contains(newFeat))
                        {
                            _vm.SelectedFeat = newFeat;
                        }
                    }
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                string messageBoxText = "Exception when adding Feat";
                string caption = "Exception";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show(messageBoxText, caption, button, icon);
            }

            _vm.RefreshButtonState();
        }

        internal void FeatSearchRemovePlaceholderTextAction(object arg)
        {
            if (_vm.FeatSearchText == RegexHandler.SearchPlaceholderText)
            {
                _vm.FeatSearchText = "";
            }
        }

        internal void FeatSearchAddPlaceholderTextAction(object arg)
        {
            if (string.IsNullOrWhiteSpace(_vm.FeatSearchText))
            {
                _vm.FeatSearchText = RegexHandler.SearchPlaceholderText;
            }
        }

        internal void FeatMinLevelRemovePlaceholderTextAction(object arg)
        {
            if (_vm.FeatMinLevel == RegexHandler.FeatMinLevelPlaceholder.ToString())
            {
                _vm.FeatMinLevel = "";
            }
        }

        internal void FeatMinLevelAddPlaceholderTextAction(object arg)
        {
            if (string.IsNullOrWhiteSpace(_vm.FeatMinLevel))
            {
                _vm.FeatMinLevel = RegexHandler.FeatMinLevelPlaceholder.ToString();
            }
        }

        internal void FeatMaxLevelRemovePlaceholderTextAction(object arg)
        {
            if (_vm.FeatMaxLevel == RegexHandler.FeatMaxLevelPlaceholder.ToString())
            {
                _vm.FeatMaxLevel = "";
            }
        }

        internal void FeatMaxLevelAddPlaceholderTextAction(object arg)
        {
            if (string.IsNullOrWhiteSpace(_vm.FeatMaxLevel))
            {
                _vm.FeatMaxLevel = RegexHandler.FeatMaxLevelPlaceholder.ToString();
            }
        }

        internal void RemoveFeatAction(object arg)
        {
            if (_vm.SelectedFeat != null)
            {
                string messageBoxText = "Feat will be removed. Are you sure?";
                string caption = "Warning";
                MessageBoxButton button = MessageBoxButton.YesNo;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

                if (result == MessageBoxResult.Yes)
                {
                    _vm.MasterFeatList.Remove(_vm.SelectedFeat);
                    _vm.HiddenFeatList.Remove(_vm.SelectedFeat);
                    _vm.FavoriteFeatList.Remove(_vm.SelectedFeat);
                    _vm.SelectedFeat = null;
                    UpdateFeatCustomTags();
                    ApplyFeatFilters();
                }
            }
        }

        internal void RemoveFavoriteFeatAction(object arg)
        {
            if (_vm.SelectedFeat != null && _vm.FavoriteFeatList.Contains(_vm.SelectedFeat))
            {
                _vm.FavoriteFeatList.Remove(_vm.SelectedFeat);

                _vm.SelectedFeat = null;
                UpdateFeatCustomTags();
                ApplyFeatFilters();
            }
        }

        internal void RemoveHiddenFeatAction(object arg)
        {
            if (_vm.SelectedFeat != null && _vm.HiddenFeatList.Contains(_vm.SelectedFeat))
            {
                _vm.HiddenFeatList.Remove(_vm.SelectedFeat);

                _vm.SelectedFeat = null;
                UpdateFeatCustomTags();
                ApplyFeatFilters();
            }
        }
    }
}
