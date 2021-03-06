﻿using Kraken.Tracing;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kraken.SharePoint.Client {
  public class ListOptions : ListCreationInformation {

    // TODO wish list - default content type
    // TODO wish list - content type order
    // TODO wish list - new or old experience
    // TODO wish list - API and REST access

    // these are implicit in ListCreationInformation
    /*
      public string Title { get; set; }
      public string Description { get; set; }
      //base.DocumentTemplateType
      //base.ListTemplate
      //base.TemplateType
      //base.TemplateFeatureId
      //base.QuickLaunchOption
      //base.Url
      //base.DataSourceProperties
      //base.CustomSchemaXml
    */

    public bool? OnQuickLaunch { get; set; }
    public bool? ContentTypesEnabled { get; set; }

    public Uri DocumentTemplateUrl { get; set; }
    public DraftVisibilityType? DraftVersionVisibility { get; set; }
    public bool? EnableAttachments { get; set; }
    public Uri DefaultDisplayFormUrl { get; set; }
    public Uri DefaultEditFormUrl { get; set; }
    public Uri DefaultNewFormUrl { get; set; }
    public bool? EnableFolderCreation { get; set; }
    public bool? EnableMinorVersions { get; set; }
    public bool? EnableModeration { get; set; }
    public bool? EnableVersioning { get; set; }
    public bool? ForceCheckout { get; set; }
    public bool? Hidden { get; set; }

    public Uri ImageUrl { get; set; }
    public bool? NoCrawl { get; set; }

    /// <summary>
    /// Specify a list definition by emum.
    /// Automatically sets ListTemplate, TemplateType, and TemplateFeatureId
    /// </summary>
    public ListTemplateType? TemplateTypeDefined { get; set; } = null;

    /// <summary>
    /// If you have a named List Template stored in the gallery
    /// you can specify it here.
    /// Automatically sets ListTemplate, TemplateType, and TemplateFeatureId
    /// </summary>
    public string TemplateTypeCustom { get; set; } = SKIP_PROPERTY;
    public string ValidationFormula { get; set; } = SKIP_PROPERTY;

    public string ValidationMessage { get; set; } = SKIP_PROPERTY;

    /// <summary>
    /// One or more contents types that should be added
    /// to the list; e.g. "Document Set" or some custom type.
    /// Expects a name, but might with with CtId also.
    /// </summary>
    public string[] EnsureContentTypes { get; set; }
    /// <summary>
    /// One or more contents types that should be removed
    /// from the list; e.g. "Item", "Document", or "Folder".
    /// Expects a name, but might with with CtId also.
    /// </summary>
    public string[] RemoveContentTypes { get; set; }

    /// <summary>
    /// Specifies the display name of the list default content type.
    /// </summary>
    public string DefaultContentType { get; set; } = SKIP_PROPERTY;

    /// <summary>
    /// Specify to change the default view
    /// </summary>
    public string DefaultView { get; set; } = SKIP_PROPERTY;

    /// <summary>
    /// When specified, used to set the Url for the list
    /// </summary>
    public string RootFolderName { get; set; } = SKIP_PROPERTY;

    /// <summary>
    /// Reading direction ltr or rtl. Default is ltr.
    /// </summary>
    public string Direction { get; set; } = SKIP_PROPERTY;

    /// <summary>
    /// Number of major versions to retain
    /// </summary>
    public int? MajorVersionLimit { get; set; } = null;
    /// <summary>
    /// Number of minor versions to retain
    /// </summary>
    public int? MajorWithMinorVersionsLimit { get; set; } = null;

    /// <summary>
    /// ???
    /// </summary>
    public bool? ParserDisabled { get; set; } = null;
    /// <summary>
    /// ???
    /// </summary>
    public bool? MultipleDataList { get; set; } = null;
    /// <summary>
    /// ???
    /// </summary>
    public int? ReadSecurity { get; set; } = null;

    public int? WriteSecurity { get; set; } = null;

    /// <summary>
    /// This is the 2016 UX
    /// </summary>
    public ListExperience? UXExperience { get; set; } = null;

    public bool? AllowDeletion { get; set; } = null;

    public bool? CrawlNonDefaultViews { get; set; } = null;

    public bool? EnableAssignToEmail { get; set; } = null;

    public bool? ExcludeFromOfflineClient { get; set; } = null;

    public bool? ExemptFromBlockDownloadOfNonViewableFiles { get; set; } = null;

    public bool? IrmEnabled { get; set; } = null;
    public bool? IrmExpire { get; set; } = null;
    public bool? IrmReject { get; set; } = null;

    public bool? IsApplicationList { get; set; } = null;

    #region Problematic fields that require legacy web services
    
    public bool? Ordered { get; set; } = null;
    public bool? ShowUser { get; set; } = null;
    public bool? PreserveEmptyValues { get; set; } = null;
    public bool? EnforceDataValidation { get; set; } = null;
    /// <summary>
    /// Affects whether pages load in dialog or new page
    /// TRUE for new page, FALSE for pop-up dialog
    /// </summary>
    /// <remarks>
    /// This property is experimental and set doesn't have any effect
    /// </remarks>
    public bool? NavigateForFormsPages { get; set; } = null;

    #endregion

    public const string SKIP_PROPERTY = "[SKIP_PROPERTY]";

    /// <summary>
    /// Indicates if the add / update operation
    /// should throw an error if it fails
    /// </summary>
    public bool ThrowOnError { get; set; } = true;

    public bool HasChangedValue(string val, string compareTo = "") {
      return (val != SKIP_PROPERTY && val != compareTo);
    }

    /// <summary>
    /// Returns true if one of the properties will
    /// have to be set after list creation because
    /// it is not supported in ListCreationInformation
    /// </summary>
    public bool HasExtendedSettings {
      get {
        return (this.ContentTypesEnabled.HasValue
          || this.DraftVersionVisibility.HasValue
          || this.EnableAttachments.HasValue
          || this.EnableFolderCreation.HasValue
          || this.EnableMinorVersions.HasValue
          || this.EnableModeration.HasValue
          || this.EnableVersioning.HasValue
          || this.ForceCheckout.HasValue
          || this.Hidden.HasValue
          || this.NoCrawl.HasValue
          || this.MajorVersionLimit.HasValue
          || this.MajorWithMinorVersionsLimit.HasValue
          || this.ParserDisabled.HasValue
          || this.MultipleDataList.HasValue
          || HasChangedValue(this.Direction)
          || this.DefaultDisplayFormUrl != null
          || this.DefaultEditFormUrl != null
          || this.DefaultNewFormUrl != null
          || HasChangedValue(this.DefaultView)
          || this.DocumentTemplateUrl != null
          || this.ImageUrl != null
          || HasChangedValue(this.ValidationFormula)
          || HasChangedValue(this.ValidationMessage)
          || this.ReadSecurity.HasValue
          || this.UXExperience.HasValue
          || HasChangedValue(this.DefaultContentType)
          || NavigateForFormsPages.HasValue
          || AllowDeletion.HasValue
          || CrawlNonDefaultViews.HasValue
          || EnableAssignToEmail.HasValue
          || ExcludeFromOfflineClient.HasValue
          || ExemptFromBlockDownloadOfNonViewableFiles.HasValue
          || IrmEnabled.HasValue
          || IrmExpire.HasValue
          || IrmReject.HasValue
          || IsApplicationList.HasValue
          || WriteSecurity.HasValue
          || Ordered.HasValue
          || ShowUser.HasValue
          || PreserveEmptyValues.HasValue
          || EnforceDataValidation.HasValue
        //|| this.OnQuickLaunch.HasValue
        //|| this.QuickLaunchOption != QuickLaunchOptions.DefaultValue
        //|| HasChangedValue(this.Description)
        //|| HasChangedValue(this.Title)
        );
      }
    }

    public bool Validate(ClientContext context, bool checkCreate, bool throwOnFail = true, ITrace trace = null) {
      bool isValid = true;
      try {
        if (checkCreate) {
          // these checks apply only when creating a list
          if (!HasChangedValue(Title))
            throw new ArgumentNullException("Title");
          ConvertTemplateOptions(context);
        }
        // checks that apply to edit and create
        if (string.IsNullOrWhiteSpace(Title))
          throw new ArgumentNullException("Title");
        if (HasChangedValue(Direction) && Direction != "LTR" && Direction != "RTL")
          throw new ArgumentOutOfRangeException("Direction");
      } catch {
        isValid = false;
        if (throwOnFail)
          throw;
      }
      return isValid;
    }

    public void CopyFrom(List list) {
      this.Title = list.Title;
      this.DefaultDisplayFormUrl = (string.IsNullOrEmpty(list.DefaultDisplayFormUrl)) ? null : new Uri(list.DefaultDisplayFormUrl);
      this.DefaultEditFormUrl = (string.IsNullOrEmpty(list.DefaultEditFormUrl)) ? null : new Uri(list.DefaultEditFormUrl);
      this.DefaultNewFormUrl = (string.IsNullOrEmpty(list.DefaultNewFormUrl)) ? null : new Uri(list.DefaultNewFormUrl);
      this.DefaultView = list.DefaultView.Title; // TODO may need something special to load this...
      this.Description = list.Description;
      this.Direction = list.Direction;
      this.DocumentTemplateUrl = (string.IsNullOrEmpty(list.DocumentTemplateUrl)) ? null : new Uri(list.DocumentTemplateUrl);
      this.DraftVersionVisibility = list.DraftVersionVisibility;
      this.EnableAttachments = list.EnableAttachments;
      this.EnableFolderCreation = list.EnableFolderCreation;
      this.EnableMinorVersions = list.EnableMinorVersions;
      this.EnableModeration = list.EnableModeration;
      this.EnableVersioning = list.EnableVersioning;
      this.ForceCheckout = list.ForceCheckout;
      this.Hidden = list.Hidden;
      this.ImageUrl = (string.IsNullOrWhiteSpace(list.ImageUrl)) ? null : new Uri(list.ImageUrl);
      this.MajorVersionLimit = list.MajorVersionLimit;
      this.MajorWithMinorVersionsLimit = list.MajorWithMinorVersionsLimit;
      this.MultipleDataList = list.MultipleDataList;
      this.NoCrawl = list.NoCrawl;
      this.OnQuickLaunch = list.OnQuickLaunch;
      this.QuickLaunchOption = (list.OnQuickLaunch) ? QuickLaunchOptions.On : QuickLaunchOptions.Off;
      this.ParserDisabled = list.ParserDisabled;
      this.TemplateFeatureId = list.TemplateFeatureId;
      this.Title = list.Title;
      this.ValidationFormula = list.ValidationFormula;
      this.ValidationMessage = list.ValidationMessage;
      this.ReadSecurity = list.ReadSecurity;
      //this.DocumentTemplateType
      this.TemplateFeatureId = list.TemplateFeatureId;
      this.TemplateType = list.BaseTemplate;
      string folder = list.RootFolder.Name;
      this.RootFolderName = (folder == list.Title) ? string.Empty : folder;
      this.UXExperience = list.ListExperienceOptions;
      this.NavigateForFormsPages = list.GetNavigateForFormsPages();
      this.AllowDeletion = list.AllowDeletion;
      this.CrawlNonDefaultViews = list.CrawlNonDefaultViews;
      this.EnableAssignToEmail = list.EnableAssignToEmail;
      this.ExcludeFromOfflineClient = list.ExcludeFromOfflineClient;
      this.ExemptFromBlockDownloadOfNonViewableFiles = list.ExemptFromBlockDownloadOfNonViewableFiles;
      this.IrmEnabled = list.IrmEnabled;
      this.IrmExpire = list.IrmExpire;
      this.IrmReject = list.IrmReject;
      this.IsApplicationList = list.IsApplicationList;
      this.WriteSecurity = list.WriteSecurity;
      // TODO we need a way to get these from schemaxml
      /*
      this.Ordered = list.Ordered;
      this.ShowUser = list.ShowUser;
      this.PreserveEmptyValues = list.PreserveEmptyValues;
      this.EnforceDataValidation = list.EnforceDataValidation;
      */

      // This is stuff we didn't know how to handle yet
      /*
      // used for resources files installed to system
      // of limited value only in SP on-prem
      list.TitleResource
      list.DescriptionResource
      // not really sure what this is good for
      list.Tag
      // unsupported/advanced stuff
      this.CustomSchemaXml = list.SchemaXml;
      this.DataSourceProperties = list.DataSourceProperties;
      // complex objects
      list.RoleAssignments
      list.UserCustomActions
      */
      ConvertTemplateOptions((ClientContext)list.Context);
    }

    /*
WARNING:   You specified a proeprty name 'NavigateForFormsPages' that is either supported by CSOM or not supported by
the Lists.asmx web service. Most likely this call will have no effect.
VERBOSE:   <List DocTemplateUrl="" DefaultViewUrl="/cpdev2/Lists/Temp_6543bab532ce4caf92299b5a537013a1/AllItems.aspx"
MobileDefaultViewUrl="" ID="{2E447F39-B917-42A5-AD4D-B0E4F003FB60}" Title="" Description=""
ImageUrl="/_layouts/15/images/itgen.png?rev=44" Name="{2E447F39-B917-42A5-AD4D-B0E4F003FB60}" BaseType="0"
FeatureId="{00BFEA71-DE22-43B2-A848-C05709900100}" ServerTemplate="100" Created="20170428 23:12:24" Modified="20170428
23:12:56" LastDeleted="20170428 23:12:24" Version="1" Direction="none" ThumbnailSize="0" WebImageWidth="0"
WebImageHeight="0" Flags="536875264" ItemCount="0" AnonymousPermMask="0"
RootFolder="/cpdev2/Lists/Temp_6543bab532ce4caf92299b5a537013a1" ReadSecurity="1" WriteSecurity="1" Author="12"
EventSinkAssembly="" EventSinkClass="" EventSinkData="" EmailAlias="" WebFullUrl="/cpdev2"
WebId="7e6e99ab-0b1f-4fac-8bba-b08207935177" SendToLocation="" ScopeId="6f953502-d4c1-439a-bd92-bf05a405f236"
MajorVersionLimit="0" MajorWithMinorVersionsLimit="0" WorkFlowId="00000000-0000-0000-0000-000000000000"
HasUniqueScopes="False" NoThrottleListOperations="False" HasRelatedLists="False" Followable="False" Acl="" Flags2="0"
RootFolderId="04df393d-eff2-41c3-b0d7-74dc057dfa6c" ComplianceTag="" ComplianceFlags="0" UserModified="20170428
23:12:24" ListSchemaVersion="1" AllowDeletion="True" AllowMultiResponses="False" EnableAttachments="True"
EnableModeration="False" EnableVersioning="False" HasExternalDataSource="False" Hidden="True" MultipleDataList="False"
Ordered="False" ShowUser="True" EnablePeopleSelector="False" EnableResourceSelector="False" EnableMinorVersion="False"
RequireCheckout="False" ThrottleListOperations="False" ExcludeFromOfflineClient="False" CanOpenFileAsync="True"
EnableFolderCreation="False" IrmEnabled="False" IrmSyncable="False" IsApplicationList="False"
PreserveEmptyValues="False" StrictTypeCoercion="False" EnforceDataValidation="False"
MaxItemsPerThrottledOperation="5000" NavigateForFormsPages="True">     */

    private void ConvertTemplateOptions(ClientContext context = null) { // List list = null
      if (this.ListTemplate != null) {
        this.TemplateType = (Int32)this.ListTemplate.ListTemplateTypeKind;
        this.TemplateFeatureId = this.ListTemplate.FeatureId;
      } else if (this.TemplateTypeDefined.HasValue) {
        this.TemplateType = (Int32)this.TemplateTypeDefined.Value;
        // feature ID is set below
      } else if (HasChangedValue(this.TemplateTypeCustom)) {
        // TODO this won't really pull custom templates, fix it
        if (context == null)
          throw new ArgumentNullException("context");
        //ClientContext context = (ClientContext)list.Context;
        Web web = context.Web;
        context.Load(web, w => w.ListTemplates);
        context.ExecuteQuery();
        ListTemplate listTemplate = web.ListTemplates.FirstOrDefault(lt => lt.Name == this.TemplateTypeCustom);
        if (listTemplate == null) {
          if (this.ThrowOnError)
            throw new ArgumentOutOfRangeException(string.Format("List template with name '{0}' does not exist in web '{1}'.", this.TemplateTypeCustom, web.UrlSafeFor2010()), "TemplateTypeCustom");
          return;
        }
        this.TemplateFeatureId = listTemplate.FeatureId;
        this.TemplateType = listTemplate.ListTemplateTypeKind;
      } else if (this.TemplateType <= 0) {
        if (this.ThrowOnError)
          throw new ArgumentNullException(string.Format("You must specify a pre-defined or custom list template."), "ListTemplate, TemplateTypeDefined, or TemplateTypeCustom");
        return;
      }
      if (this.TemplateFeatureId == Guid.Empty) {
        switch (this.TemplateType) {
          case 101: // ListTemplateType.DocumentLibrary: // 101
            this.TemplateFeatureId = new Guid("00bfea71-e717-4e80-aa17-d0c71b360101");
            break;
          case 115: //ListTemplateType.XMLForm: // 115 Form Library
            this.TemplateFeatureId = new Guid("00bfea71-1e1d-4562-b56a-f05371bb0115");
            break;
          case 119: //ListTemplateType.WebPageLibrary: // 119 Wiki Page Library
            this.TemplateFeatureId = new Guid("00bfea71-c796-4402-9f2f-0eb9a6e71b18");
            break;
          case 109: //ListTemplateType.PictureLibrary: // 109
            this.TemplateFeatureId = new Guid("00bfea71-52d4-45b3-b544-b1c71b620109");
            break;
          case 130: //ListTemplateType.DataConnectionLibrary: // 130
            this.TemplateFeatureId = new Guid("00bfea71-dbd7-4f72-b8cb-da7ac0440130");
            break;
          case 104: //ListTemplateType.Announcements: // 104
            this.TemplateFeatureId = new Guid("00bfea71-d1ce-42de-9c63-a44004ce0104");
            break;
          case 105: //ListTemplateType.Contacts: // 105
            this.TemplateFeatureId = new Guid("00bfea71-7e6d-4186-9ba8-c047ac750105");
            break;
          case 108: //ListTemplateType.DiscussionBoard: // 108
            this.TemplateFeatureId = new Guid("00bfea71-6a49-43fa-b535-d15c05500108");
            break;
          case 103: //ListTemplateType.Links: // 103
            this.TemplateFeatureId = new Guid("00bfea71-2062-426c-90bf-714c59600103");
            break;
          case 106: //ListTemplateType.Events: // 106 Calendar
            this.TemplateFeatureId = new Guid("00bfea71-ec85-4903-972d-ebe475780106");
            break;
          case 107: //ListTemplateType.Tasks: // 107
            this.TemplateFeatureId = new Guid("00bfea71-a83e-497e-9ba0-7a5c597d0107");
            break;
          case 150: //ListTemplateType.GanttTasks: // 150 Project Tasks
            this.TemplateFeatureId = new Guid("00bfea71-513d-4ca0-96c2-6a47775c0119");
            break;
          case 1100: //ListTemplateType.IssueTracking: // 1100
            this.TemplateFeatureId = new Guid("00bfea71-5932-4f9c-ad71-1557e5751100");
            break;
          case 102: //ListTemplateType.Survey: // 102
            this.TemplateFeatureId = new Guid("00bfea71-eb8a-40b1-80c7-506be7590102");
            break;
          case 100: //ListTemplateType.GenericList: // 100
            this.TemplateFeatureId = new Guid("00bfea71-de22-43b2-a848-c05709900100");
            break;
          case 120: //ListTemplateType.CustomGrid: // 120 Custom List in Datasheet View
            this.TemplateFeatureId = new Guid("00bfea71-3a1d-41d3-a0ee-651d11570120");
            break;
          case 432: //ListTemplateType.InvalidType: // 432 KPI List
            this.TemplateFeatureId = new Guid("065c78be-5231-477e-a972-14177cc5b3c7");
            break;
          case 433: //ListTemplateType.InvalidType: // 433 Report Library
            this.TemplateFeatureId = new Guid("2510d73f-7109-4ccc-8a1c-314894deeb3a");
            break;
          case 1300: //ListTemplateType.InvalidType: // 1300 Translation Management Library
            this.TemplateFeatureId = new Guid("29d85c25-170c-4df9-a641-12db0b9d4130");
            break;
          case 1301: //ListTemplateType.InvalidType: // 1301 Languages and Translators
            this.TemplateFeatureId = new Guid("29d85c25-170c-4df9-a641-12db0b9d4130");
            break;
          case 2100: //ListTemplateType.InvalidType: // 2100 Slide Library
            this.TemplateFeatureId = new Guid("0be49fe9-9bc9-409d-abf9-702753bd878d");
            break;
            //default: TODO warning
        }
      }
    }

    public ListCreationInformation ConvertSP14Safe() {
      return new ListCreationInformation() {
        CustomSchemaXml = this.CustomSchemaXml,
        DataSourceProperties = this.DataSourceProperties,
        Title = this.Title,
        Description = this.Description,
        QuickLaunchOption = this.QuickLaunchOption, // (this.OnQuickLaunch ?? false) ? QuickLaunchOptions.On : QuickLaunchOptions.Off,
        TemplateFeatureId = this.TemplateFeatureId,
        TemplateType = this.TemplateType,
        ListTemplate = this.ListTemplate,
        DocumentTemplateType = this.DocumentTemplateType,
        Url = this.Url,
      };
    }

  }

}
