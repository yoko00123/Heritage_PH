using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InSys.Models
{
    public class Menu
    {
        public tMenu tMenu { get; set; }
        public List<tMenuTab> tMenuTab { get; set; }
        public List<tMenuDetailTab> tMenuDetailTab { get; set; }
        public List<tMenuTabField> tMenuTabField { get; set; }
        public List<tMenuDetailTabField> tMenuDetailTabField { get; set; }
        public List<tMenuButton> tMenuButton { get; set; }
        public List<tMenuLoadParameters> tMenuLoadParameters { get; set; }

    }

    public class tMenu
    {
        public int? ID;
        public string Name;
        public string DataSource;
        public string BaseDataSource;
        public string TableName;
        public int? ID_Menu;
        public int? SeqNo;
        public string ImageFile;
        public string ReportFile;
        public int? ID_MenuType;
        public string Sort;
        public bool IsVisible;
        public bool AllowNew;
        public bool AllowOpen;
        public bool AllowDelete;
        public bool ReadOnly;
        public string ReportTitle;
        public string ReportSubTitle;
        public string ColorRGB;
        public string DarkColorRGB;
        public bool AllowEdit;
        public int? ID_ListMenu;
        public string ListMenu;
        public string ListSource;
        public string ListRowFieldHeader;
        public string ListRowField;
        public string ListRowCategoryHeader;
        public string ListRowCategory;
        public bool IsUserData;
        public bool IsSpanView;
        public string ListFixedFilter;
        public string StatusTable;
        public string Description;
        public string SaveTrigger;
        public int? ID_MenuInfo;
        public string Icon;
        public string TableFilter;
        public string SearchField;
        public bool? HasChildren;
        public int? ID_ApplicationType;
        public string ApplicationType;
        public int? ParentID;
        public bool? MultiSelect;
        public bool? IsMenuDialog;
        public bool? HasAuditTrail;
        public string MultiSelectIf;
        public string EnableSaveIf;
        public string WritableAttachmentIf;
    }

    public class tMenuTab
    {
        public int? ID_Menu;
        public int? ID;
        public string Name;
        public bool HasTable;
        public string Description;
        public string ImageFile;
        public int? SeqNo;
        public string TableFilter;
        public string VisibleIf;
    }

    public class tMenuDetailTab
    {
        public int? ID;
        public string Code;
        public string Name;
        public int? ID_Menu;
        public string ImageFile;
        public int? SeqNo;
        public bool IsActive;
        public string TableName;
        public string ChildColumn;
        public string ParentColumn;
        public string Description;
        public string Comment;
        public string DataSource;
        public string ListSource;
        public bool CheckBoxes;
        public string ParentTableName;
        public int? ID_DetailMenu;
        public string ReportFile;
        public string Sort;
        public bool Sortable;
        public bool ShowInBrowser;
        public MenuDetailTabType? ID_MenuDetailTabType;
        public string ParentLookUp;
        public int? ID_ListMenu;
        public string ListMenuFixedFilter;
        public string ListMenuDetailSource;
        public bool ShowInInfo;
        public string Label;
        public string DetailTabFilter;
        public bool AllowDuplicateList;
        public string SaveTrigger;
        public string ImportFile;
        public bool? AllowNewRow;
        public bool? AllowDeleteRow;
        public string FileReferenceDataSource;
        public string FileReferenceSort;
        public bool IsSalaryAuthenticatedTab;
        public string VisibleIf;
        public bool? HasAuditTrail;
        public string DisableButtonsIf;
        public bool? DeleteRowBeforeImportFile;
        public string DisableDetailDeleteIf;
        public string ListMenu;
    }

    public class tMenuTabField
    {
        public int? ID;
        public string Name;
        public int? ID_MenuTab;
        public SystemControlType? ID_SystemControlType;
        public int? ID_Menu;
        public string Label;
        public int? SeqNo;
        public bool IsActive;
        public string Header;
        public string Comment;
        public string Menu;
        public string Description;
        public string DataSource;
        public string MenuTab;
        public bool ShowInBrowser;
        public bool ReadOnly;
        public int? Panel;
        public int? MenuTabMenuID;
        public int? MenuTabSeqNo;
        public bool ShowInInfo;
        public bool ShowInList;
        public string SystemControlType;
        public string TableName;
        public string StringFormat;
        public string Sort;
        public string EffectiveLabel;
        public string ImageFile;
        public string ParentLookUp;
        public string ParentLookUpChildColumn;
        public string Expression;
        public int? ID_SystemAggregateFunction;
        public string SystemAggregateFunction;
        public string DefaultValue;
        public bool IsRequired;
        public string FixedFilter;
        public string ListColumn;
        public string VisibleIf;
        public string WritableIf;
        public int? Height;
        public string RequiredIf;
        public string WebWritableIf;
        public bool IsClear;
        public string DataType;
        public string DisplayMember;
        public string DisplayID;
    }

    public class tMenuDetailTabField
    {
        public int? ID;
        public string Name;
        public int? ID_MenuDetailTab;
        public SystemControlType? ID_SystemControlType;
        public int? ID_Menu;
        public string Label;
        public int? SeqNo;
        public bool IsActive;
        public string Header;
        public string Comment;
        public string Menu;
        public string Description;
        public string MenuDetailTab;
        public bool ShowInBrowser;
        public string Formula;
        public bool ReadOnly;
        public int? Width;
        public bool ListKey;
        public string ListColumn;
        public bool IsGroup;
        public string Text;
        public string ListText;
        public string Sort;
        public bool IsColumn;
        public bool CopyFromList;
        public string ImageFile;
        public bool ShowInInfo;
        public string EffectiveLabel;
        public int? MenuDetailTabMenuID;
        public string Expression;
        public string TableName;
        public string ParentLookUp;
        public string ParentLookUpChildColumn;
        public bool IsRequired;
        public string FixedFilter;
        public string Defaultvalue;
        public bool IsFrozen;
        public string ParentLookUpListColumn;
        public bool IsWordWrap;
        public bool IsTextArea;
        public string ReadOnlyif;
        public string WebReadOnlyif;
        public bool IsClear;
        public string DataType;
    }

    public class tMenuButton
    {
        public int? ID;
        public string Code;
        public string Name;
        public int? SeqNo;
        public bool IsActive;
        public string Comment;
        public string CommandText;
        public int? ID_Menu;
        public string ConfirmationText;
        public string SuccessInfoText;
        public bool DisabledOnNewInfo;
        public string ImageFile;
        public int? ID_MenuDetailTab;
        public int? ID_MenuButtonType;
        public bool MustSaveFirst;
        public string EnabledIf;
        public string ListSource;
        public string WebEnabledIf;
        public int? ID_SystemNotification;
        public string ValidateCommandText;
        public string TemplateFile;
        public bool IsGeneratedTextFile;
    }
    public class tMenuLoadParameters
    {
        public string Name;
        public string CommandText;
        public int? ID_Menu;
        public bool isActive;
    }
    public enum SystemControlType : int
    {
        TextBox = 1,
        ComboBox = 2,
        CheckBox = 3,
        DataLookUp = 4,
        DockedTextBox = 5,
        EmailTextBox = 6,
        DataImage = 7,
        MaskedTextBox = 8,
        GSTextBox = 9,
        DataColor = 10,
        DataDate = 11,
        DataTime = 12,
        LinkButton = 13,
        LabelBox = 14,
        HiddenField = 15,
        FileUpload = 16,
        DataDateTime = 17
    }

    public enum MenuDetailTabType : int
    {
        Grid = 1,
        TreeView = 2,
        List = 3,
        Report = 4
    }
}