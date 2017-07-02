﻿using System.Collections.Generic;
using System.Linq;
using JoinRpg.DataModel;
using JetBrains.Annotations;
using JoinRpg.Helpers;

// ReSharper disable once CheckNamespace
namespace JoinRpg.Domain
{
  public class FieldWithValue 
  {
    private string _value;

    private IReadOnlyList<int> SelectedIds { get; set; }
    public FieldWithValue(ProjectField field, [CanBeNull] string value)
    {
      Field = field;
      Value = value;
    }

    public ProjectField Field { get; }

    [CanBeNull]
    public string Value
    {
      get { return _value; }
      set
      {
        _value = value;
        if (Field.HasValueList())
        {
          SelectedIds = Value.ToIntList();
        }
      }
    }

    [NotNull]
    public string DisplayString => GetDisplayValue(Value, SelectedIds);

    protected string GetDisplayValue(string value, IReadOnlyList<int> selectedIDs)
    {
      if (!Field.HasValueList())
      {
        return value ?? "";
      }
      return
        Field.DropdownValues.Where(dv => selectedIDs.Contains(dv.ProjectFieldDropdownValueId))
          .Select(dv => dv.Label)
          .JoinStrings(", ");
    }

    public bool HasEditableValue => !string.IsNullOrWhiteSpace(Value);

    public bool HasViewableValue => !string.IsNullOrWhiteSpace(Value) || !Field.CanHaveValue();

    public IEnumerable<ProjectFieldDropdownValue> GetPossibleValues()
    {
      return Field.GetOrderedValues().Where(v => v.IsActive || SelectedIds.Contains(v.ProjectFieldDropdownValueId));
    }

    [ItemNotNull, NotNull]
    public IEnumerable<ProjectFieldDropdownValue> GetDropdownValues()
    {
      return Field.GetOrderedValues().Where(v => SelectedIds.Contains(v.ProjectFieldDropdownValueId));
    }

    [NotNull, ItemNotNull]
    public IEnumerable<CharacterGroup> GetSpecialGroupsToApply()
    {
      return Field.HasSpecialGroup() ? GetDropdownValues().Select(c => c.CharacterGroup) : Enumerable.Empty<CharacterGroup>();
    }

    public bool HasViewAccess(bool masterAccess, bool characterAccess, bool claimAccess)
    {
      return Field.IsPublic
        || masterAccess
        ||
        (characterAccess && Field.CanPlayerView &&
         Field.FieldBoundTo == FieldBoundTo.Character)
        ||
        (claimAccess && Field.CanPlayerView &&
         Field.FieldBoundTo == FieldBoundTo.Claim);
    }

    public bool HasEditAccess(bool masterAccess, bool characterAccess, bool claimAccess, IClaimSource target)
    {
      return (masterAccess
             ||
             (characterAccess && Field.CanPlayerEdit &&
              Field.FieldBoundTo == FieldBoundTo.Character)
             ||
             (claimAccess && Field.CanPlayerEdit && (Field.ShowOnUnApprovedClaims || characterAccess)));
    }

    public override string ToString() => $"{Field.FieldName}={Value}";

    public const string CheckboxValueOn = "on";
  }
}
