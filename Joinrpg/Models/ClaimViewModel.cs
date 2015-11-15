﻿using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using JoinRpg.DataModel;
using JoinRpg.Domain;
using JoinRpg.Web.Models.CommonTypes;
using JoinRpg.Web.Models.Plot;

namespace JoinRpg.Web.Models
{
  public class ClaimViewModel : ICharacterFieldsViewModel
  {
    public int ClaimId { get; set; }
    public int ProjectId { get; set; }
    public string ClaimName { get; set; }
    [DisplayName("Игрок")]
    public User Player { get; set; }

    
    public string ProjectName { get; set; }
    public Claim.Status Status { get; set; }
    public bool IsMyClaim { get; set; }

    public bool HasPlayerAccessToCharacter { get; set; }
    public bool HasMasterAccess { get; set; }
    public bool HasApproveRejectClaim { get; set; }

    public IEnumerable<CharacterFieldValue> CharacterFields { get; set; }
    public IEnumerable<Comment> Comments { get; set; }

    [DisplayName("Заявка на персонажа")]
    public string CharacterName { get; set; }

    public int? CharacterId { get; set; }

    [DisplayName("Заявка в группу")]
    public string GroupName { get; set; }

    public int? CharacterGroupId { get; set; }
    public int OtherClaimsForThisCharacterCount { get; set; }
    public int OtherClaimsFromThisPlayerCount { get; set; }

    [Display(Name = "Описание персонажа")]
    public MarkdownViewModel Description { get; set; }

    public IEnumerable<ICharacterGroupLinkViewModel> ParentGroups { get; set; }

    public IEnumerable<PlotElementViewModel> Plot { get; set; }

    [Display(Name = "Ответственный мастер")]
    public int ResponsibleMasterId { get; set; }

    [ReadOnly(true)]
    public IEnumerable<MasterListItemViewModel> Masters { get; set; }

    [ReadOnly(true)]
    public bool HasOtherApprovedClaim { get; set; }


    [ReadOnly(true)]
    public CharacterGroupListViewModel Data { get; set; }

    public bool HasAccess => true;

    public bool HidePlayer => false;

  }
}