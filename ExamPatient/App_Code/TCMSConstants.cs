using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCMS.Shared.BussinessEntity
{
    [Serializable]
    public class TCMSConstants
    {
        public struct ViolationDetails
        {
            public string violationCode;
            public string violationDesc;
            public bool tsEligibleInd;
            public string correctableType;
        }
        public struct RelatedCases
        {
            public string DocketNumber { get; set; }
            public string LocationCode { get; set; }

        }
        #region SequenceRegion
        public const string DB_SEQ_INCIDENT = "SEQ_INCIDENT";
        public const string DB_SEQ_PERSON = "SEQ_PERSON";
        public const string DB_SEQ_R_AGENCY = "SEQ_R_AGENCY";
        public const string DB_SEQ_R_ATTORNEY = "SEQ_R_ATTORNEY";
        public const string DB_SEQ_R_DEPARTMENT = "SEQ_R_DEPARTMENT";
        public const string DB_SEQ_DOCKETEVENT = "SEQ_DOCKETEVENT";
        public const string DB_SEQ_R_OFFICER = "SEQ_R_OFFICER";
        public const string DB_SEQ_R_SPECIAL_SCHEDULE = "SEQ_R_SPECIAL_SCHEDULE";
        public const string DB_SEQ_CONFIGURABLE = "SEQ_CONFIGURABLE";
        public const string DB_SEQ_R_MONETARY_COST = "SEQ_R_MONETARY_COST";
        public const string DB_SEQ_R_AGENCY_LOCATION = "SEQ_R_CITING_AGENCY_LOCATION"; 

        public const string DB_SEQ_ADDRESS = "SEQ_ADDRESS";
        public const string DB_SEQ_R_VIOLATION = "SEQ_R_VIOLATION";
        public const string DB_SEQ_CITATION = "SEQ_CITATION";
        public const string DB_SEQ_VEHICLE = "SEQ_VEHICLE";
        public const string DB_SEQ_DRIVERLICENSE = "SEQ_DRIVERLICENSE";
        public const string DB_SEQ_CITATIONVIOLATION = "SEQ_CITATIONVIOLATION";
        public const string DB_SEQ_PAID_ITEM = "SEQ_PAID_ITEM";
        public const string DB_SEQ_DOCKETNBR = "SEQ_DOCKETNBR";
        public const string DB_SEQ_PAID_VIOLATION = "SEQ_PAID_VIOLATION";
        public const string DB_SEQ_PAID_ITEM_DETAIL = "SEQ_PAID_ITEM_DETAIL";
        public const string DB_SEQ_RECEIPTACTION = "SEQ_RECEIPTACTION";
        public const string DB_SEQ_RECEIPTACTION_RECEIPTNBR = "SEQ_RECEIPTACTION_RECEIPTNBR";
        public const string DB_SEQ_R_VIOLATION_CATEGORY = "SEQ_R_VIOLATION_CATEGORY";
        public const string DB_SEQ_R_VIOLATION_CATEGORY_ASSOC = "SEQ_R_VIOLATION_CATEGORY_ASSOC";
        public const string DB_SEQ_R_REDUCT_FORMULA = "SEQ_R_REDUCT_FORMULA";
        public const string DB_SEQ_R_DISTRIBUTION_FORMULA = "SEQ_R_DISTRIBUTION_FORMULA";
        public const string DB_SEQ_R_DISTRIBUTION_REDUCTION = "SEQ_R_DISTRIBUTION_REDUCTION";
        public const string DB_SEQ_R_DISTRIBUTION_BASETYPE = "SEQ_R_DISTRIBUTION_BASETYPE";
        public const string DB_SEQ_GSPAID_ITEM = "SEQ_GSPAID_ITEM";
        public const string DB_SEQ_GSPAID_ITEM_DETAIL = "SEQ_GSPAID_ITEM_DETAIL";

        public const string DB_SEQ_HOLIDAY = "SEQ_R_HOLIDAYS";
        public const string DB_SEQ_EVENT_PROOF = "SEQ_EVENT_PROOF";
        public const string DB_SEQ_CALENDARHEADER = "SEQ_CALENDARHEADER";
        public const string DB_SEQ_R_BAIL_SCHEDULE = "SEQ_R_BAIL_SCHEDULE";
        public const string DB_SEQ_IMPORTERROR = "SEQ_IMPORT_ERROR";
        public const string DB_SEQ_PRIOR_VIOLATION = "SEQ_PRIOR_VIOLATION";

        public const string DB_SEQ_PAYMENT_DUE_DETAIL = "SEQ_PAYMENT_DUE_DETAIL";
        public const string DB_SEQ_PAYMENT_DUE = "SEQ_PAYMENT_DUE";
        public const string DB_SEQ_PAYMENT_DUE_VIOLATION = "SEQ_PAYMENT_DUE_VIOLATION";

        public const string DB_SEQUENCE_CALENDAR = "SEQ_CALENDAR";

        public const string DB_SEQUENCE_ACCOUNTINGWQ = "SEQ_ACCOUNTINGWQ";
        public const string DB_SEQUENCE_EVENT_REFUND = "SEQ_EVENT_REFUND";
        public const string DB_SEQUENCE_CITATIONSENTENCE = "SEQ_CITATIONSENTENCE";
        public const string DB_SEQ_INSTALLMENT_SCHEDULE = "SEQ_INSTALLMENT_SCHEDULE";
        public const string DB_SEQ_INSTALLMENT_SCHEDULE_DETAIL = "SEQ_INSTALLMENT_SCHEDULE_DET";
        public const string DB_SEQ_PAID_ITEM_DISTRIBUTION = "SEQ_PAID_ITEM_DISTRIBUTION";
        public const string DB_SEQ_PAYMENT_DUE_TARGET = "SEQ_PAYMENT_DUE_TARGET";
        public const string DB_SEQ_RECIPIENT_DISTRIBUTION = "SEQ_RECIPIENT_DISTRIBUTION";
        public const string DB_SEQ_PAID_ITEM_TRANSACTION = "SEQ_PAID_ITEM_TRANSACTION";
        public const string DB_SEQ_DMV_ABSTRACT_INCOMING = "SEQ_DMV_ABSTRACT_INCOMING";

        public const string DB_SEQ_EVENT_NAME_CHANGE = "SEQ_EVENT_NAME_CHANGE";
        public const string DB_SEQ_EVENT_ADDRESS_CHANGE = "SEQ_EVENT_ADDRESS_CHANGE";


        public const string DB_SEQ_JOBTRANSACTION = "SEQ_JOBTRANSACTION";
        public const string DB_SEQ_R_JOB_MANAGER = "SEQ_R_JOB_MANAGER";

        public const string DB_SEQ_EXT_NOTICE = "SEQ_EXT_NOTICE";
        public const string DB_SEQ_WORK_QUEUE = "SEQ_WORK_QUEUE";
        public const string DB_SEQ_EXTERNAL_COLLECTIONS = "SEQ_EXTERNAL_COLLECTIONS";

        public const string DB_SEQ_R_ASSIGN = "SEQ_R_ASSIGNMENT";
        public const string DB_SEQ_R_WORKSPACE = "SEQ_R_WORKSPACE";
        public const string DB_SEQ_S_USER = "SEQ_S_USER";
        public const string DB_SEQ_S_USERJOBROLE = "SEQ_S_USERJOBROLE";
        public const string DB_SEQUENCE_JOBROLES = "SEQ_R_JOBROLES";

        public const string DB_SEQUENCE_TBDID = "SEQ_TBD";
        public const string DB_SEQUENCE_FILING_VIOLATION = "SEQ_FILING_VIOLATION";
        public const string DB_SEQ_R_JOBROLE_WS_ASSIGN = "SEQ_R_JOBROLE_WS_ASSIGN";

        public const string DB_SEQ_R_RECIPIENT = "SEQ_R_RECIPIENT";
        public const string DB_SEQ_R_RECIPIENTFUND = "SEQ_R_RECIPIENT_FUND";
        public const string DB_SEQ_CITATION_DMV = "SEQ_CITATION_DMV";

        public const string DB_SEQ_R_GENERAL_SERVICE = "SEQ_R_GENERAL_SERVICE";

        public const string SEQ_CITATION_VIOLATION_DISPOSITION = "SEQ_CV_DISPOSITION";

        public const string DB_SEQ_R_BOND = "SEQ_R_BOND";

        public const string DB_SEQ_R_SURETY = "SEQ_R_SURETY";

        public const string DB_SEQ_IMAGE_ERROR = "SEQ_IMAGE_ERROR";

        public const string DB_SEQ_AWS_WARRANT_INCOMING = "SEQ_AWS_WARRANT_INCOMING";

        public const string DB_CHECK_SEQ_DISBURSEMENTDET = "SEQ_DISBURSEMENTDET";
        public const string DB_CHECK_SEQ_ACCTTRANSACTION = "SEQ_ACCTTRANSACTION";

        #endregion  SequenceRegion

        #region PersonTypeRegion

        public const string PERSON_DEFENDENT = "DEFENDANT";
        public const string PERSON_DEPOSITOR = "DEPOSITOR";
        public const string PERSON_RESTITUTION = "RESTITUTION";
        public const string PERSON_OWNER = "OWNER";
        public const string PERSON_AGENT = "AGENT";
        public const string PERSON_ATTORNEY = "ATTORNEY";
        public const string PERSON_VICTIM = "VICTIM";
        public const string PERSON_WITNESS = "WITNESS";

        public const string NAMETYPE_TYPO = "Typo";

        #endregion  PersonTypeRegion

        #region CitationTypeRegion

        public const string CITATION_TYPE_TRAFFIC = "Traffic";

        public const string CITATION_DLN = "A0000000";

        #endregion  CitationTypeRegion

        #region StatusRegion
        public const string STATUSACTIVE = "AC";
        public const string STATUSACTIVEPENDING = "AP";
        public const string STATUSDISPOSED = "DS";
        public const string STATUSMODIFY = "MO";
        public const string STATUSREFERREDTOCOLLECTION = "RC";
        public const string STATUSBAILWAVED = "BW";
        public const string STATUSPAYMENTDONE = "PD";
        public const string STATUSSENTENCEENTER = "SN";
        public const string ZERO = "0";
        public const string YES = "YES";
        public const string NA = "NA";
        public const string NO = "NO";
        public const string STATUSINACTIVE = "IA";
        public const string STATUSPENDING = "PE";
        public const string IMAGESTATUSPENDING = "PI";
        public const string IMAGESTATUSREADY = "RE";
        public const string STATUSCOMPLETE = "CO";
        public const string STATUSREADY = "RA";
        public const string STATUSBAILBALANCEEXONERATE = "EX";
        public const string STATUSDECIDED = "DE";
        public const string STATUSDROP = "DP";
        public const string STATUSRESET = "RS";
        public const string STATUSVACATE = "VC";
        public const string STATUSBONDPOST = "OP";
        public const string STATUSBONDEXONERATE = "OE";
        public const string STATUSBONDFORFEIT = "OF";
        public const string CALENDAR_INPROGRESS = "IC";
        public const string CALENDAR_NOTHEARED = "NH";
        public const string STATUSINPROGRESS = "IP";
        public const string STATUSOPEN = "OPEN";
        public const string STATUSCLOSED = "CLOSED";
        public const int CHECKBOXCHECKED = 1;
        public const int CHECKBOXUNCHECKED = 0;
        public const string WITHFEE = "1";
        public const string NOFEE = "2";
        public const string REDUCEDBAIL = "3";
        public const string POCMANDATORY = "M";
        public const string PAYMENTDUESTATUS = "BP";
        public const string PARTIAL_PAYMENT_STATUS = "PP";
        public const string PAYMENTDUE_STATUS_PAID = "PA";
        public const string PAYMENTDUE_STATUS_INSTALMENTPAYMENT = "IP";
        public const string RECEIPT_STATUS_VOID = "VO";
        public const string STATUS_BAILWAIVED = "BW";
        public const string STATUS_CITATION_TRACKERS_ACTIVE = "CA";
        public const string CHECK = "Check";
        public const string CASH = "Cash";
        public const string CREDITCARD = "Credit Card";
        public const string NOBARCODE = "NOBARCODE";
        public const string DISPOSEONLY = "OD";
        public const string COMPLETE_DISPOSE = "CD";
        public const string DISPOSE_COMPLETE = "DC";
        public const string SPECIAL_DISPOSE = "SD";
        public const string ONLY_COMPLETE= "OC";
       
        public const string STATUSLIMBO = "LB";
        public const string STATUSREMOVED = "RM";
        public const string STATUSEXPIRED = "XP";

        public const string LIMBO = "LIMBO";
        public const string REMOVED = "REMOVED";
        public const string EXPIRED = "Expired";

        public const string REFERTOCOLLECTIONS = "REFERTOCOLLECTIONS";
        

        public const string STATUSSETASIDE = "SA";

        public const string STATUSRECALL = "RL";
        #endregion StatusRegion

        #region EventTypeRegion
        public const int EVENT_TYPE_ID_PAYMENT = 1;
        public const int EVENT_TYPE_ID_COURTDATE = 4;
        public const int EVENT_TYPE_ID_COURTBAIL = 91;
        public const int EVENT_TYPE_ID_BAILFROMFINE = 44;
        public const int EVENT_TYPE_ID_BAILFORFIET = 61;
        public const int EVENT_TYPE_ID_BAILEXORNATE = 42;
        public const int EVENT_TYPE_ID_TSINSTALLMENT = 242;
        public const int EVENT_TYPE_ID_TSINSPLAN = 196;
        public const int EVENT_TYPE_ID_TSENROLL = 26;
        public const int EVENT_TYPE_ID_POC = 39;
        public const int EVENT_TYPE_ID_COURTFINE = 87;
        public const int EVENT_TYPE_ID_COURTFEES = 99;
        public const int EVENT_TYPE_ID_JS = 60;
        public const int EVENT_TYPE_ID_DISMISSAL = 47;
        public const int EVENT_TYPE_ID_VIOALTIONFINE = 53;
        public const int EVENT_TYPE_ID_DISPOSITIONFINE = 75;
        public const int EVENT_TYPE_ID_COMMUNITYSERVICEENROLLMENT = 36;
        public const int EVENT_TYPE_ID_CASEUNDERSUBMISSION = 213;
        public const int EVENT_TYPE_ID_TRAFFICCOMPLETIONTSA = 187;
        public const int EVENT_TYPE_ID_TRAFFICCOMPLETIONTSAF = 188;
        public const int EVENT_TYPE_ID_TRAFFICCOMPLETIONTSF = 7;
        public const int EVENT_TYPE_ID_TRAFFICSCHOOLDDISPOSED = 71;
        #endregion EventTypeRegion

        #region DispositionRegion
        public const string R_DISPOSITIONCODE_BF = "BF";
        public const string R_DISPOSITIONCODE_FINE = "FINE";
        public const string R_DISPOSITIONCODE_TS = "TS";
        public const string R_DISPOSITIONCODE_POC = "POC1";

        public const string COL_DISPOTYPE = "DispoType";
        public const string COL_DISPODATE = "DispoDate";
        #endregion DispositionRegion

        #region AdminRegion
        //CalendarHeader Constants
        public const string CALENDARHEADERDAYS_MON = "Mon";
        public const string CALENDARHEADERDAYS_TUE = "Tue";
        public const string CALENDARHEADERDAYS_WED = "Wed";
        public const string CALENDARHEADERDAYS_THU = "Thu";
        public const string CALENDARHEADERDAYS_FRI = "Fri";

        public const string CALENDARHEADERFREQUENCYPERIOD_WEEKLY = "W";
        public const string CALENDARHEADERFREQUENCYPERIOD_BIWEEKLY = "B";
        public const string CALENDARHEADERFREQUENCYPERIOD_MONTHLY = "M";

        public const string ALREADYEXISTS = " already exsists with the same details.";
        public const string ADDUPDATE_TITLE = "ADD / UPDATE ";
        public const string ADD_TITLE = "ADD ";
        public const string UPDATE_TITLE = "UPDATE ";

        public const string ADD_MESSAGE = " Added Successfully.";
        public const string UPDATE_MESSAGE = " Updated Successfully.";

        public const string CALENDARHEADERSTATUS_ACTIVE = "Active";
        public const string CALENDARHEADERSTATUS_INACTIVE = "Inactive";

        public const string FREQUENCYPERIOD_WEEKLY = "Weekly";
        public const string FREQUENCYPERIOD_BIWEEKLY = "Bi-Weekly";
        public const string FREQUENCYPERIOD_MONTHLY = "Monthly";

        # region compliance tracker
        public const string COMPLIANCESTATUS_ACTIVE = "Active";
        public const string COMPLIANCESTATUS_INACTIVE = "InActive";
        #endregion compliance tracker

        # region VoidRecipt
        public const string VOIDRECEIPT_ACTIVE = "Active";
        public const string VOIDRECEIPT_VOID = "Void";
        public const string CANNOTVOIDRECEIPT = "Cannot Void";
        public const string WRONGPAYMENT = "WRONG PAYMENT";

        #endregion VoidRecipt



        //Update Person
        public const string DEFAULT_COUNTRY = "USA";
        public const string CANADA_COUNTRY_CODE = "CAN";
        public const string DEFAULT_STATECODE = "CA";

        //public const string STATUS_ACTIVE = "AC";
        //public const string STATUS_INACTIVE = "IA";

        public const string CITATION_ORGANIZATION = "ORGANIZATION";
        public const string CITATION_INDIVIDUAL = "INDIVIDUAL";
        public const string CITATION_ADD = "Add Citation";

        //CalendarGeneration Constants
        public const int DAYOFWEEK_SUNDAY = 0;
        public const int DAYOFWEEK_MONDAY = 1;
        public const int DAYOFWEEK_TUESDAY = 2;
        public const int DAYOFWEEK_WEDNESDAY = 3;
        public const int DAYOFWEEK_THURSDAY = 4;
        public const int DAYOFWEEK_FRIDAY = 5;
        public const int DAYOFWEEK_SATURDAY = 6;
        public const int ADDDAYS_WEEKLY = 7;
        public const int ADDDAYS_BIWEEKLY = 14;
        public const int CG_TOTALDAYS = 366;

        public const string WEEKDAY_SUNDAY = "Sunday";
        public const string WEEKDAY_MONDAY = "Monday";
        public const string WEEKDAY_TUESDAY = "Tuesday";
        public const string WEEKDAY_WEDNESDAY = "Wednesday";
        public const string WEEKDAY_THURSDAY = "Thursday";
        public const string WEEKDAY_FRIDAY = "Friday";
        public const string WEEkDAY_SATURDAY = "Saturday";

        //public const string CG_SEQUENCE_CALENDAR = "SEQ_CALENDAR";
        public const string CG_CALENDARHEADERID = "CALENDARHDRID";
        public const string CG_CALENDARSTATUS_OPEN = "Open";

        public const string CG_GENERATEDCALENDAR = "Generated Calendars :";
        public const string CG_DELETEDCALENDAR = "Deleted Calendars :";

        public const int CG_FIRST = 1;
        public const int CG_SECOND = 2;
        public const int CG_THIRD = 3;
        public const int CG_FOURTH = 4;
        public const int CG_FIFTH = 5;

        public const int CG_FIRSTWEEK_DAYONE = 1;
        public const int CG_FIRSTWEEK_DAYSEVEN = 7;
        public const int CG_SECONDWEEK_DAYEIGHT = 8;
        public const int CG_SECONDWEEK_DAYFOURTEEN = 14;
        public const int CG_THIRDWEEK_DAYFIFTEEN = 15;
        public const int CG_THIRDWEEK_DAYTWENTYONE = 21;
        public const int CG_FOURTHWEEK_DAYTWENTYTWO = 22;
        public const int CG_FOURTHWEEK_DAYTWENTYEIGHT = 28;
        public const int CG_FIFTHWEEK_DAYTWENTYNINE = 29;
        public const int CG_FIFTHWEEK_DAYTHIRTYONE = 31;

        public const string LOCK = "LK";
        public const string UNLOCK = "UL";
        public const string OVERBOOKED = "OB";
        public const string FULL = "FL";
        //Agency Constants
        public const string AGENCYLASTUPDATED = "Last Updated On:";
        public const string AGENCYDATEFORMAT = "MM/dd/yyyy";
        public const string AGENCYADD = "ADD AGENCY";
        public const string AGENCYUPDATE = "UPDATE AGENCY";

        //Attorney Constants

        public const string PRIVATE_ATTORNEY_TYPE = "PRIVATE ATTORNEY";
        public const string PUBLIC_ATTORNEY_TYPE = "PUBLIC ATTORNEY";
        public const string DISTRICT_ATTORNEY_TYPE = "DISTRICT ATTORNEY";

        //ComboBox Constants

        public const string COMBOBOX_VALUE = "Value";
        public const string COMBOBOX_ID = "Id";

        #endregion

        #region ConfigRegion
        // public const string Traffic_School_Due_Date = "Enrollment";
        public const int Traffic_School_Due_Date = 26;
        public const int TS_InstallmentPlan_Due_Date = 196;
        public const string Community_Service_Due_Date = "Community Service";        
        #endregion

        #region 'R_Monetary_Cost' Table Region

        public const string ASSOCIATION_VIOLATION = "V";
        public const string ASSOCIATION_CITATION = "C";
        public const string ASSOCIATION_BOTH = "B";
        public const string ASSOCIATION_TRAFFICSCHOOL = "TS ENROLLMENT FEE";
        public const string ASSOCIATION_POC = "POC FEE";
        public const string FORMULATYPE_FIXED = "FIXED";
        public const string FORMULATYPE_PERCENT = "PERCENT";
        public const string FORMULATYPE_PER1 = "PER1";
        public const string FORMULATYPE_PER10 = "PER10";
        public const string FORMULATYPE_PER100 = "PER100";

        //public const string FEE_BASEBAIL = "BASE BAIL";
        //public const string BAILCATEGORY = "CATEGORY";
        //public const string PRIORADMINFEE = "PRIOR ADMIN  FEE";
        //public const string NIGHTCOURTASSESSMENT = "NIGHT COURT";
        //public const string PENALTY = "PENALTY";
        //public const string CONFIGURATIONPRIORS = "PRIORS";
        //public const string FEE_PENALTY = "PENALTY";
        public const string FEE_ICNA = "I.C.N.A.";
        public const string FEE_SECURITYFEE = "Security Fee";

        public const int COST_BAILPOST = 0;
        public const int COST_ICNA_INFRACTION_ID = 1;
        public const int COST_ICNA_MISDEMEANOR_ID = 2;
        public const int COST_NIGHTCOURTASSESSMENT_ID = 3;
        public const int COST_SECURITYFEE_ID = 4;
        public const int COST_PENALTY_ID = 5;
        public const int COST_TRAFFICSCHOOL_ID = 6;
        public const int COST_STATESURCHARGE_ID = 7;
        public const int COST_BASEBAIL_ID = 8;
        public const int COST_POC_ID = 9;
        public const int COST_SPEED_ID = 10;
        public const int COST_BAILCATEGORY_ID_1 = 11;
        public const int COST_BAILCATEGORY_ID_2 = 12;
        public const int COST_BAILCATEGORY_ID_3 = 13;
        public const int COST_BAILCATEGORY_ID_4 = 14;
        public const int COST_BAILCATEGORY_ID_5 = 15;
        public const int COST_PRIORADMINFEE_ID = 16;
        public const int COST_COMMUNITYSERVICEHOURAMOUNT_ID = 17;
        public const int COST_OVERAGE_ID = 18;
        public const int COST_TSBAIL_ID = 19;
        //need to goto database
        public const int COST_BFBAIL_ID = 2002;
        public const int COST_WITNESSPROGRAM_ID = 20;
        public const int COST_BADCHECKFEE = 22;
        public const int COST_PARTIALPAYMENT = 23;
        public const int COST_INSTALLMENTPAYMENT = 85;
        public const int COST_BF_INSTALLMENTPAYMENT = 2002;
        public const int COST_INSTALLMENT = 24;
        public const int COST_INSTALLMENT_BF = 2003;
        public const int COST_FINE = 25;
        public const int BADCHKTRACKID = 12;
        public const int COST_WARRANTID = 543;
        public const int COST_CIVIL_ASSESSMENTFEE_ID = 584;
        public const int COST_TRUST = 685;
        public const int COST_WARRANTFEE_ID = 585;
        public const int COST_HOLDFEE_ID = 586;
        public const int COST_MOTIONFEE = 723;
        public const int COST_TSOTHER = 84;
        //public const int FEE_BASEBAIL_ID = 9;
        //public const string FEE_STATESURCHARGE = "STATE SURCHARGE";

        #endregion 'R_Monetary_Cost' Table Region

        #region 'CONFIGURABLE' Table Region

        public const string CONFIG_CHNG_REFUND = "CHANGE OVERAGE REFUND";
        public const string CONFIG_TEMPLATELOCATION = "TEMPLATELOCATION";
        public const string CONFIG_SMOOTHMINUTES = "SMOOTHMINUTES";
        public const string CONFIG_TRAFFICSCHOOLINSTRUCTION = "TRAFFICSCHOOLINSTRUCTION";
        public const string CONFIG_ARRAIGNMENTCOURTTRAIL = "ARRAIGNMENTCOURTTRIAL";
        public const int CONFIGURATION_PRIORS = 17;
        public const int CONFIGURATION_TS_ELIGIBILITY = 18;
        // public const string CONFIG_INSTALLMENT_DUEDAYS = "InstallmentDueDays";

        #endregion 'CONFIGURABLE' Table Region

        #region 'R_Violation' Table Region

        public const string ALL_RECORDS = "ALL";
        public const string LEVELMISDEMEANOR = "M";
        public const string LEVELINFRACTION = "I";
        public const string STATUTE_CVC = "CVC";
        public const string STATUTE_FG = "FG";

        #endregion 'R_Violation' Table Region

        #region ' R_VIOLATION_CATEGORY' Table Region

        public const string STATUTE = "S";
        public const string CHARGETYPE = "T";
        public const string VIOLATION = "V";
        public const string ALREADY_UPDATED = "ALREADYUPDATED";
        public const string COSTTYPE = "COST";


        #endregion 'R_VIOLATION_CATEGORY' Table Region

        #region 'R_Schedule_Type' Table Region

        public const string OVERSPEED = "OVERSPEED";
        public const string OVERWEIGHT = "OVERWEIGHT";
        public const string WITHNET = "WITHNET";
        public const string OVERLIMIT = "OVERLIMIT";
        public const string INTOXICATION = "INTOXICATION";

        public const string OVERSPEED_INTOXICATION = "OVERSPEED_INTOXICATION";
        public const string OVERWEIGHT_INTOXICATION = "OVERWEIGHT_INTOXICATION";
        public const string WITHNET_INTOXICATION = "WITHNET_INTOXICATION";


        public const string VIOLATIONSTATUTE_FG = "FG";
        public const string GENERAL = "GENERAL";
        public const string WITHNET_VALUE = "0";
        public const string OVERLIMIT_VALUE = "7";



        public const string SPEED = "SPEED/CONDITIONS";

        #endregion 'R_Schedule_Type' Table Region

        #region DistributionFormula
        public const string PERSENTAGE = "P";
        public const string FLAT = "F";
        #endregion

        #region DBHelper Constants

        public const string DB_GENERAL = "General";
        public const string DB_SEQUENCEID = "SequenceID";
        public const string DB_REFERENCEID = "ReferenceID";
        #endregion

        public const int TRXTYPEID_POSTBAIL = 1;
        public const int TRXTYPEID_BAILFORFEITURE = 2;
        public const string PAIDTYPET = "T";
        public const string PAIDTYPED = "D";
        public const int TRXTYPEID_TRAFFICSCHOOL = 4;

        #region R_REASON_FOR_TRACKING Constants
        public const int CITATIONDUEDATEID = 1;
        public const int COURTESYID = 2;
        public const int COURTDATEID = 3;
        public const int PROOFID = 4;
        public const int TRAFFICSCHOOL_TRACKINGID = 5;
        public const int COURTFINEID = 6;
        public const int COMMUNITYSERVICE_TRACKINGID = 7;
        public const int FTADUEDATEID = 8;
        public const int FTPDUEDATEID = 9;
        public const int FTCDUEDATEID = 10;
        public const int FEESID = 11;
        public const int BADCHECKID = 12;
        public const int DMVID = 13;
        public const int PAYMENTID = 14;
        public const int HEARINGID = 15;
        public const int RULINGID = 16;
        public const int JAILID = 17;
        public const int COURTFINE_TRACKINGID = 18;
        public const int COURTRIVISED_TRACKINGID = 19;
        public const int COURTTSFEE_TRACKINGID = 20;
        public const int OPTIONSID = 21;
        public const int POCTRACKERID = 22;
        public const int TBDARULINGID = 34;
        #endregion

        #region Set Court Date Calendar Type
        public const string CALTYPE = "WALK-IN";
        public const string CALCOURTTRIALTYPE = "COURTTRAIL";
        public const string ROCSUMMARYJUDGEMENT = "78";
        public const string RESETACTION = "Reset";
        public const string DROPACTION = "Drop";
        public const string VACATEACTION = "Vacate";
        public const string RESETSTATUS = "RS";
        public const string DROPSTATUS = "DP";
        public const string VACATESTATUS = "VC";
        public const int COURTTRAILDATE = 45;
        public const int ALLCALTYPEDATES = 28;

        #endregion

        #region CalendarIndex
        public const string calendarIndexView = "VC";
        public const string calendarIndex = "CI";
        public const string VacateViewCalendar = "VACATE";
        public const string DropViewCalendar = "DROP";
        public const string ResetViewCalendar = "RESET";
        public const string ActiveViewCalendar = "ACTIVE";
        public const string completedViewCalendar = "COMPLETED";
        public const string InProgressViewCalendar = "In Progress";
        public const string RecoredViewCalendar = "Recorded";
        public const string NotHeardViewCalendar = "Not Heard";
        public const string NotAssignedViewCalendar = "N/A";
        public const string ViewCalendarVaccated = "VACATED";
        public const string ViewCalendarDroped = "DROPPED";




        #endregion

        public const string DummyDriverLicense = "A0000000";
        public const string DefalutDocketNumberFormat = "{0:0000000000}";


        public const string DATE_FORMAT = "MM/dd/yyyy hh:mm tt";
        public const string TIME_FORMAT = "hh:mm tt";
        public const string DATE_FORMAT_WITHOUT_TIME = "MM/dd/yyyy";
        public const string END_DATE = "12/31/2999";
        public const string SPACE = " ";
        public const string COMMA = ",";
        public const string COLON = ":";
        public const string PAYMENTDUEREASON = "959";
        #region R_ACCOUNTING_TRANSACTION_TYPE Constants

        public const string BONDEXORNATE = "BondExornate";
        public const string BONDFORFEIT = "BondForfeit";

        public class AccountingTransactionType
        {
            public const string BAILPOSTED = "BlPst";
            public const string BAILFORFEITURE = "BlFor";
            public const string FINE = "Fine";
            public const string TRAFFICSCHOOL = "TS";
            public const string FEES = "Fees";
            public const string POC = "PoC";
            public const string MONEYTRANSFERTO = "Mtrx";
            public const string FORFEITBAIL = "ForBl";
            public const string BAILTOFINETS = "BlFns";
            public const string WARRANT = "Wart";

            public const string TRUSTTOBF = "TRtBF";

            //public const string BAILTOFINE = "BlFn";
            //public const string BAILTOTS = "BlTS";
            //public const string BAILTOFEES = "BlFee";
            public const string TrustToDISTRIBUTE = "TrDbt";
            public const string EXONERATEBAIL = "ExrBl";
            public const string EXONERATEBALANCE = "ExBal";
            public const string OVERAGE = "Ovg";
            public const string BADCHECK = "BadCk";
            public const string STOPPAYMENT = "StPyt";
            public const string SETASIDE = "StAsd";
            public const string VOID = "Void";
            public const string MONEYTRANSFERFROM = "MtrxF";
            public const string TOTALBF = "TotBF";
            public const string TOTALFINE = "TotFn";
            public const string TOTALTS = "TotTs";
            public const string TOTALFEES = "TotFe";
            public const string CREDIT = "Credt";
            public const string DEBIT = "Debit";
            public const string REFUND = "Refnd";
            public const string RECEIPTTRANSFER = "RECTR";
            public const string RECEIPT_TRANSFER_FROM = "RECTRFrom";
            public const string RECEIPT_TRANSFER_FROM_BF = "RECTRFromBF";
            public const string PARTIALPAYMENT = "PaPmt";
            public const string PARTIALPAYMENTBF = "PPtBF";
            public const string PARTIALPAYMENTFINE = "PPtFn";
            public const string INSTALLMENTPAYMENT = "InstalPmt";
            public const string COMMUNITYSERVICE = "CS";
            public const string JAIL = "Jail";
            public const string BAIL = "Bail";
            public const string BAILFORFEIT = "Bail Forfeiture";
            public const string DISMISSAL = "DISM";
            public const string REVISEDBAIL = "Bail";
            public const string REVERSAL = "Rev";
            public const string DISTRIBUTION = "Distr";
            public const string INSTALLMENTFEE = "ISFee";
            public const string TSINSTALLMENT = "TSINS";
            //need to gotodatabase
            public const string BFINSTALLMENT = "BFINS";
            public const string DUMMYFEES = "Dummyfees";
            public const string EVENTDISTRIBUTION = "Distribution";
            public const string GSPayment = "GSPay";
        }
        #endregion

        public struct DispositionCode
        {
            public const string REVISED_BAIL = "BAIL";
            public const string BAIL_FORFEITURE = "BF";
            public const string COMMUNITY_SERVICE = "CS";
            public const string DISMISSAL = "DISM";
            public const string FINE = "FINE";
            public const string FEES = "FEES";
            public const string JAIL = "JAIL";
            public const string POC_AT_COUNTER = "POC1";
            public const string POC_AT_COURTROOM = "POC2";
            public const string TRAFFIC_SCHOOL_TSTYPE_DISPOSED = "TSF";
            public const string TRAFFIC_SCHOOL_TSTYPE_DISMISSED_VIOLATION_ISNOT_TSELIGIBLE = "TSA";
            public const string TRAFFIC_SCHOOL_TSTYPE_DISMISSED_VIOLATION_IS_TSELIGIBLE = "TSAF";
            public const string JUDGEMENTSUSPENDED = "JS";
        }

        public struct DispositionDescription
        {
            public const string BAIL_DESC_FORFEITURE = "BAIL FORFEITURE";

        }

        # region Accounting Exonerate
        public const string EXONERATE_BAIL = "EXONERATE_BAIL";
        public const string EXONERATE_BALANCE = "EXONERATE_BALANCE";
        public const string FINE_FROM_BAIL = "FINE_FROM_BAIL";
        public const string TS_FROM_BAIL = "TS_FROM_BAIL";
        #endregion
        # region Accounting Apportionment
        public const string ALREADY_APPORTIONED = "ALREADYAPPORTIONED";
        #endregion
        public const string DOLLARSIGN = "$";
        public const string NOAMOUNT = "$ 0.00";
        public const int STOP_CHECK = 8;
        public const int STOP_TRANSTYPE = 0;
        public const int STOP_CHECKNUMBER = 2;
        public const int STOP_REFUNDAMT = 3;
        public const int STOP_CITATION = 6;
        public const int STOP_NOTES = 5;
        public const int STOP_REFUND_DOCKET = 7;
        public const string STOPREFUNDEXRBL = "ExrBl";
        public const string STOPREFUNDEXBAL = "ExBal";
        public const string STOPREFUNDOVG = "Ovg";
        public const string COMPSTOPPAYMENT = "StPyt";
        public const string CONFIRMATION = "CONFIRMATION";
        public const string DOCKET_CITATION_NUMBER = "Docket Number or Citation Number";
        public const string RECORDS = "Records";
        public const string DOCKET_NUMBER = "Docket Number:";
        public const string CITATION_NUMBER = "Citation Number:";
        public const string STOPPAYSTATUS = "SP";

        public const string FEES = "Fees";

        public const int TOTAL_INSTALLMENTS = 4;
        //need to gotodatabase
        public const int TOTAL_INSTALLMENTS_BF = 10;
        public const int INSTALLMENTS_BF_GRACEPERIOD = 5;
        public const int INSTALLMENTS_DAYS_BF = 30;
        public const int INSTALLMENTS_BF_PER = 75;
        public const int INSTALLMENTS_POC_BF_PER = 25;
        //end
        public const string CURRENCY_FORMAT = "0.00";
        public const string CURRENCYFORMAT = "{0:C}";
        public const string CURRENCY_FORMAT_SINGLE_DECIMAL = "0.0";
        #region FundAdjustment
        public const int ROWFUNDNAME = 0;
        public const int ROWRECEIPIENTNAME = 1;
        public const int ROWDISTAMT = 2;
        public const int ROWDISTID = 3;
        public const int ROWNEWDISTAMT = 4;
        public const int FUNDNAME = 5;
        public const int RECEIPENTNAME = 6;
        public const int DISTRIBUTIONAMOUNT = 7;
        public const int DISTRIBUTIONID = 8;

        #endregion

        #region Database Table Names
        public const string CALENDARHEADER = "CALENDAR_HEADER";
        public const string DEPARTMENT = "R_DEPARTMENT";
        public const string CITATIONID = "CITATIONID";
        public const string CALENDARID = "CALENDARID";
        public const string SEQDOCKETEVENT = "SEQ_DOCKETEVENT";
        public const string PERSONTYPE = "R_PERSON_TYPE";

        #endregion

        #region EventType Description

        public const string POC_SUBMITTED = "Proof Of Correction";
        #endregion

        #region  MonetaryCost
        public const string COSTTYP_BAIL = "BlPst";
        public const string COSTTYP_TRUST = "Trust";
        #endregion MonetaryCost

        #region Lookup
        public const string SEX = "SEX";
        public const string RACE = "RACE";
        public const string HAIRCOLOR = "HAIRCOLOR";
        public const string SUFFIXNAMECODE = "SUFFIXNAMECODE";
        public const string EYECOLOR = "EYECOLOR";
        public const string HEIGHT = "HEIGHT";
        public const string STATE = "STATE";
        public const string MAIL = "M";
        public const string COUNTER = "C";
        #endregion

        #region CitationSentence

        //public const string SENTENCETYPE_TRAFFICSCHOOL = "TS";
        //public const string SENTENCETYPE_TRAFFICSCHOOL_FEE = "TFEE";
        //public const string SENTENCETYPE_POC = "POC";
        //public const string SENTENCETYPE_JAIL = "JAIL";
        //public const string SENTENCETYPE_COMMUNITYSERVICE = "CS";
        //public const string SENTENCETYPE_FINE = "FINE";
        //public const string SENTENCETYPE_FINEFEES = "FINE/FEES";
        //public const string SENTENCETYPE_FEES = "FEES";
        //public const string SENTENCETYPE_CSINLIEUFINE = "CSINLIEUFINE";
        //public const string SENTENCETYPE_INLIEUFINE = "IN LIEU FINE";
        //public const string SENTENCETYPE_INLIEUTS = "IN LIEU TS FEE";
        //public const string SENTENCETYPE_TSINLIEU = "TSINLIEU";
        //public const string SENTENCETYPE_FINDING = "FIND";
        //public const string SENTENCETYPE_PLEA = "PLEA";
        //public const string SENTENCETYPE_DISMISSAL = "DISM";
        //public const string SENTENCETYPE_OTHERSENTENCE = "OS";
        //public const string SENTENCETYPE_JS = "JS";
        //public const string SENTENCETYPE_BAIL = "BAIL";
        //public const string SENTENCETYPE_POCINLIEU = "POCINLIEU";


        public const string SENTENCETYPE_TRAFFICSCHOOL = "7";
        public const string SENTENCETYPE_TRAFFICSCHOOL_FEE = "3";
        public const string SENTENCETYPE_POC = "5";
        public const string SENTENCETYPE_JAIL = "8";
        public const string SENTENCETYPE_COMMUNITYSERVICE = "6";
        public const string SENTENCETYPE_FINE = "1";
        public const string SENTENCETYPE_FINEFEES = "9";
        public const string SENTENCETYPE_FEES = "2";
        public const string SENTENCETYPE_CSINLIEUFINE = "10";
        public const string SENTENCETYPE_INLIEUFINE = "4";
        public const string SENTENCETYPE_INLIEUTS = "19";
        public const string SENTENCETYPE_TSINLIEU = "11";
        public const string SENTENCETYPE_FINDING = "12";
        public const string SENTENCETYPE_PLEA = "13";
        public const string SENTENCETYPE_DISMISSAL = "14";
        public const string SENTENCETYPE_OTHERSENTENCE = "15";
        public const string SENTENCETYPE_JS = "16";
        public const string SENTENCETYPE_BAIL = "17";
        public const string SENTENCETYPE_POCINLIEU = "18";
        #endregion

        #region sentence correct text
        public const string CORRECTTYPE_EVENTHEARING = "Event Hearing";
        public const string CORRECTTYPE_COMMSRV = "Citation level Community Service";
        public const string CORRECTTYPE_FINEFEES = "Citation level Fine/Fee";
        public const string CORRECTTYPE_JAIL = "Jail";
        public const string CORRECTTYPE_RVSDBAIL = "Revised bail";
        public const string CORRECTTYPE_PLEA = "Plea";
        public const string CORRECTTYPE_FINDING = "Finding";
        public const string CORRECTTYPE_AMMEND = "Amend to Violation";
        public const string CORRECTTYPE_REDUCEDTOI = "Reduced to I";
        public const string CORRECTTYPE_POC = "PoC";
        public const string CORRECTTYPE_TS = "TS";
        public const string CORRECTTYPE_VIOALTIONFINEFEES = "Fine/Fees";
        public const string CORRECTTYPE_VIOALTIONRVSDBAIL = "Rvsd bail";
        public const string CORRECTTYPE_POCINLIEU = "PoC In Lieu";
        public const string CORRECTTYPE_TSINLIEU = "TS In Lieu";
        public const string CORRECTTYPE_JS = "JS";
        public const string CORRECTTYPE_OTHERSENTENCE = "Other Sentence";
        public const string CORRECTTYPE_DSM = "Dsm";
        public const string CORRECTTYPE_CONDITIONALSENTENCE = "Conditional Sentence";
        public const string CORRECTTYPE_WARRANTHOLDS = "Warrant holds";
        public const string CORRECTTYPE_EVENTWAIVERS = "Event waivers";
        public const string CORRECTTYPE_BAILWAIVED = "Bail waived";
        public const string CORRECTTYPE_INTERPRETER = "Interpreter";
        public const string CORRECTTYPE_SCHEDULING = "Scheduling";
        public const string CORRECTTYPE_COURTBAIL = "Court bail";
        public const string CORRECTTYPE_COURTFINE = "Court Fine";
        public const string CORRECTTYPE_SUMMARYJUDJEMENT = "Summary Judgement entered";
        public const string CORRECTTYPE_SUMMARYJUDJEMENTDATE = "Summary Judgement date";

        #endregion

        #region ExtendDueDates
        public class ExtendDueDate
        {
            public const string CourtDate = "Court Date";


        }

        #endregion

        #region CourtRoom
        public class CourtRoom
        {
            public const string HEADER_NAMES = "Count,Original Bail,Plea,Finding,PoC,PoC In Lieu,JS,Rvsd Bail,Fine,Fees,TS,TS Fee,TS In Lieu,Other Sentence,CS Hrs,DSM,Dsm Reason,Reduce to “I”,Amend  to Stat  Violation,Modify Sentence,Dispo Type,Dispo Date,TSElgible,TSFee";
            public const string RULING_HEADER_NAMES = "Count,Original Bail,PoC,PoC In Lieu,Finding,JS,Rvsd Bail,Fine,Fees,TS,TS Fee,TS In Lieu,Other Sentence,CS Hrs,DSM,Dsm Reason";
            public const string MOTIONRULING_HEADER_NAMES = "FileAt,Method,Document Category,CatId,Motion Type,TypeId,AssociateTo,BehaviorCategory,Grant,Deny,Remove";
            //public const string PROOF_HEADER_NAMES = "Count,PoC,Proof of Completion";
            public const string PROOF_HEADER_NAMES = "Count,PoC,TS Completion,Completion Certificate/CS,Completion Certificate/Jail,Completion Certificate/Options,Completion Certificate/NA,Completion Certifcate/TC,Completion Certificate/Smart Start,Completion Certificate/Other";
            public const string MOTION_FILED_AT = "MINUTE ORDER";
            public const int FIXED_COLUMN_COUNT = 3;
            public const int PROOF_FIXED_COLUMN_COUNT = 2;
            public const int FIXED_ROW_COUNT = 11;
            public const string DEFENDANT_PRESET = "PRESENT";
            public const string DEFENDANT_NOT_PRESET = "NOTPRESENT";
            public const string DEFENDANT_EXCUSED = "EXCUSED";
            public const string DEFENDANT_INCUSTODY = "INCUSTODY";
            public const string DEFENDANT_NOTGUILTY = "NOT GUILTY";
            public const string DEFENDANT_GUILTY = "GUILTY";
            public const string DEFENDANT_NOTCONTEST = "NO CONTEST";
            public const string DEFENDANT_WAIVED_TIME = "WAIVEDTIME";
            public const string DEFENDANT_TIME_NOT_WAIVED = "TIMENOTWAIVED";
            public const string PLEA_GUILTY = "GUILTY";
            public const string PLEA_NOTGUILTY = "NOT GUILTY";
            public const string PLEA_NOTCONTEST = "NO CONTEST";
            public const string REASON_CS = "CS";
            public const string REASON_JS = "JS";
            public const string REASON_CSFINE = "CSFINE";
            public const string REASON_CSTS = "CSTS";
            public const string REASON_POC = "POC";
            public const string REASON_POCINLIEU = "POCINLIEU";
            public const string REASON_DISMISSALREASON = "REASON";
            public const string REASON_DISMISSALNOREASON = "NOREASON";
            public const string REASON_AMENDINF = "INF";
            public const string REASON_AMENDVIOLATION = "MODIFY";
            public const string REASON_AMENDNEWVIOLATION = "NEW";
            public const string REASON_TRAFFICSCHOOL = "TS";
            public const string REASON_TRAFFICSCHOOLINLIEU = "TSINLIEU";
            public const string OSC_OTHER = "OTHER";
            public const string OSC_OWNER = "OWNER";
            public const string OSC_DRIVER = "DRIVER";
            public const string PERSON_TYPE = "THIRDPARTY";
            public const string REASON_PURSUANT = "PURSUANT";
            public const string REASON_NOTPURSUANT = "NOTPURSUANT";
            public const string REASON_COURTFINE = "Fine";
            public const string REASON_TBDARULING = "TBDARULING";
            public const string REASON_COURTFEES = "Fees";
            public const string REASON_REVISED = "Revised";
            public const string REASON_ONOSC = "OSC";
            public const string REASON_ONNOTICE = "NOTICE";
            public const string REASON_ONCONTINUE = "CONTINUE";

            public const string COMPLETION_TS = "TS";
            public const string COMPLETION_JAIL = "JAIL";
            public const string COMPLETION_OPTIONS = "OPTIONS";
            public const string COMPLETION_COMMUNITY_SERVICE = "COMMUNITY SERVICE";
            public const string COMPLETION_SETTLED_STATEMENT = "SETTLED STATEMENT";
            public const string BAILFORFEITED = "BAIL FORFEITED";

            public const string DLNRESTRICTED = "Restricted";
            public const string DLNSUSPENDED = "Suspended";
            public const string DLNDELAYED = "Delayed";
            public const string DLNSURRENDERED = "Surrendered";

            public const string CONDITION_TYPE = "PROBATION";


            public const string TSFEES_TYPE = "Traffic School";
            public const string POCFEES_TYPE = "Proof of Correction";
            public const string JSFEES_TYPE = "Judgement Suspended";



        }

        public class ReferenceCodes
        {
            public const string DISMISSAL_REFTABLE = "DISMISSAL";
            public const string TS_REFTABLE = "TS";
            public const string TSDISMISSAL_REFTABLE = "TSDISMISSAL";
            public const string PLEA_REFTABLE = "PLEA";
            public const string FINDING_REFTABLE = "FINDING";
            public const string OTHERSENTENCE_REFTABLE = "OTHERSENTENCE";
            public const string PROOFOFCORRECTION_REFTABLE = "PROOFOFCOMPLETION";
            public const string RESTRICTEDTYPE_REFTABLE = "RESTRICTEDTYPE";
            public const string SUMMARYJUDGEMENT_REFTABLE = "SUMMARYJUDGEMENT";
            public const string NORMALCALENDARS_REFTABLE = "NORMALCALENDARS";
            public const string SUMMARYJUDGEMENTAMOUNT_REFTABLE = "SUMMARYJUDGEMENTAMOUNT";
            public const string DELEGATED_REFTABLE = "SETASIDEREASON";
            public const string AMEND_REFTABLE = "AMEND REASON";

        }
        #endregion

        #region CaseIndex Serach criteria
        public class CaseIndex
        {
            public const string DOCKETNUMBER = "DOCKETNBR.ToUpper()";
            public const string CITATIONNUMBER = "CITATIONNUMBER.ToUpper()";
            public const string VIN = "VIN.ToUpper()";
            public const string PLATENUMBER = "PLATENUMBER.ToUpper()";
            public const string DRIVERLICENSE = "DRIVERLICENSE.ToUpper()";
            public const string ORGANIZATIONNAME = "ORGANIZATIONNAME.ToUpper()";
            public const string COURTLOCATIONCODE = "COURTLOCATIONCODE";
            public const string PERSONTYPE = "PERSONTYPE";
            public const string CITATIONDATE = "CITATIONDATE";
        }
        #endregion

        #region Judicial

        public const string CHECKBOX_CHECKED = "on";
        public const string CHECKBOX_UNCHECKED = "off";

        public const string CHECKED = "true";
        public const string UNCHECKED = "false";

        public const string FEE_BASEBAIL = "BASE BAIL";
        public const string NIGHTCOURTASSESSMENT = "NIGHTCOURT";

        public const string ROCKEY = "ROC";
        public const string ALIGN_RIGHT = "Right";

        public const string COMBO_ALL = "ALL";
        public const string CIVIL_ASSESSMENT = "Civil Assessment";

        public const string SELECTANY = "-SELECT-";
        public const string MOTION = "MOTION";
       
        #endregion

        #region InstallmentTypes
        public const string INSTALLMENT_SCHEDULE = "IS";
        public const string INSTALLMENT_SCHEDULE_BF = "BF";
        public const string INSTALLMENT_BADCHECK_SCHEDULE = "IB";
        public const string INSTALLMENT_BADCHECK = "BC";
        public const string INSTALLMENT_FINE = "FI";
        public const string INSTALLMENT_RECEIPTTRANSFER = "RT";
        public const string INSTALLMENT_TRAFFICSCHOOL = "TS";

        public const string INSTALLMENT_SCHEDULE_DESC = "Installment Schedule";
        public const string INSTALLMENT_BADCHECK_DESC = "Bad Check";
        public const string INSTALLMENT_FINE_DESC = "Fine";
        public const string INSTALLMENT_RECEIPTTRANSFER_DESC = "Receipt Transfer";
        public const string INSTALLMENT_TRAFFICSCHOOL_DESC = "Traffic School";
        public const string INSTALLMENT_BADCHECK_SCHEDULE_DESC = "Bad Check in Installment";
        #endregion InstallmentTypes

        #region Visibility
        public const int VISIBLE = 1;
        #endregion Visibility

        #region EVENT SUB TYPE TITLES
        public const string PAYMENT = "Payment";
        public const string PARTIAL_PAYMENT = "Partial Payment";
        public const string INSTALLMENT_PAYMENT = "Installment Payment";
        public const string HEARING_EVENT_SUBTYPEID = "37";
        public const string COURTFINE_EVENT_SUBTYPEID = "87";
        public const string COMMUNITYSERVICE_EVENT_SUBTYPEID = "36";
        public const string TRAFFICSCHOOL_EVENT_SUBTYPEID = "26";
        public const string PoC_EVENT_SUBTYPEID = "18";
        public const string SENTENCING_NOTES = "Sentencing Notes";
        public const string ROC_SCHEDULE_EVENT_SUBTYPEID = "20";

        #endregion

        #region SpeedSchedule
        public const string Speed = "Speed Schedule";
        public const string Add = "ADD SPEED RANGE";
        public const string UpdateSpeed = "Update SPEED RANGE";
        public const string Standard = "OVERSPEEDSTANDARD";
        public const string CVC42009 = "OVERSPEEDCVC42009";
        public const string CVC42010 = "OVERSPEEDCVC42010";
        public const string PARKSANDRECREATION = "OVERSPEEDPARKSANDRECREATION";
        public const string Fine = "Base Fine($)";
        public const string MinRange = "Over Speed Range Start(MPH)";
        #endregion

        #region Overweight
        public const string FIXED = "Fixed";
        public const string Weight = "OverWeight Range";
        public const string AddWeight = "ADD Overweight RANGE";
        public const string UpdateWeight = "Update Overweight RANGE";
        public const string CVC42031 = "OVERWEIGHTCVC42030.1";
        public const string CVC42030 = "OVERWEIGHTCVC42030";
        public const string MinWeightRange = "Minimum Weight(LBS)";
        #endregion

        #region FeeSchedule
        public const string Fee = "Fee Schedule";
        public const string AddFee = "Add FEE";
        public const string UpdateFee = "Update FEE";
        public const string FeeName = "Fee Name";
        public const string Bail = "%Bail";
        public const string FixedAmount = "Fixed Amount";
        public const string Expiry = "Expiry Date";
        public const string Unit = "Amount/Unit";
        public const string Effictive = "Effective Date";
        #endregion

        #region ReasonForTrackingIds

        public class ReasonForTracking
        {

            public const Int32 EXTENSION = -1;
            public const Int32 CITATIONDUEDATE = 1;
            public const Int32 COURTESYNOTICE = 2;
            public const Int32 COURTDATE = 3;
            public const Int32 PROOF = 4;
            public const Int32 TRAFFICSCHOOL = 5;
            public const Int32 FINE = 6;
            public const Int32 COMMUNITYSERVICE = 7;
            public const Int32 FTADUEDATE = 8;
            public const Int32 FTPDUEDATE = 9;
            public const Int32 FTCDUEDATE = 10;
            public const Int32 FEES = 11;
            public const Int32 BADCHECK = 12;
            public const Int32 DMV = 13;
            public const Int32 PAYMENT = 14;
            public const Int32 HEARING = 15;
            public const Int32 RULING = 16;
            public const Int32 JAIL = 17;
            public const Int32 COURTFINE = 18;
            public const Int32 COURTREVISEDBAIL = 19;
            public const Int32 TSFEE = 20;
            public const Int32 OPTIONS = 21;
            public const Int32 POCTRACKER = 22;
            public const Int32 TRAFFICSCHOOLFINALINSTALLMENTDUEDATE = 23;
            public const Int32 WARRANT = 24;
            public const Int32 CONDITIONALSENTENCE = 25;
            public const Int32 PARTIALPAYMENT = 26;
            public const Int32 SETASIDEBF = 27;
            public const Int32 FILING = 28;
            public const Int32 TBDDEFENDENT = 29;
            public const Int32 TBDARRESTINGOFFICER = 30;
            public const Int32 TBDCITINGOFFICER = 31;
            public const Int32 MOTIONDENIED = 32;
            public const Int32 REVERSAL = 33;
            public const Int32 TBDAFTPDUEDATE = 34;
            public const Int32 TBDAREFERTOCOLLECTIONS = 35;
            public const Int32 BFFINALINSTALLMENTDUEDATE = 37;
            public const Int32 WORKQUEUEFINE = 38;
            public const Int32 TBDAFINEDUEDATE = 39;
            public const Int32 FEES_COURT = 40;

        }
        #endregion

        #region ReasonOnCalender
        public class ReasonOnCalender
        {
            public const Decimal JURYTRAILARRAIGNMENT = 3;//"ARRAIGNMENT";
            public const Decimal ARRAIGNMENT = 264;
            public const Decimal OSCWALKIN = 252;
            public const Decimal OSCARRAIGNMENT = 253;
            public const Decimal ARRAIGNMENTFAILURETOAPPEAR = 4; //"ARRAIGNMENTFAILURETOAPPEAR";
            public const Decimal ARRAIGNMENTFAILURETOCOMPLY = 5;//"ARRAIGNMENTFAILURETOCOMPLY";
            public const Decimal ARRAIGNMENTFAILURETOPAY = 6;// "ARRAIGNMENTFAILURETOPAY";
            public const Decimal ARRAIGNMENTONAWARRANT = 7;//"ARRAIGNMENTONAWARRANT";



            public const Decimal PAYMENTOFFINEEXTENSION = 45;// "PAYMENTOFFINEEXTENSION";
            public const Decimal PAYMENTOFFINE_TRANSACTIONFEE = 48;// "PAYMENTOFFINE/TRANSACTIONFEE";
            public const Decimal PAYMENTOFFINE_TRAFFICSCHOOLFEE = 50;//"PAYMENTOFFINE/TRAFFICSCHOOLFEE";
            public const Decimal PAYMENTOFFINE_TRAFFICSCHOOL = 49;// "PAYMENTOFFINE/TRAFFICSCHOOL";
            public const Decimal PAYMENTOFFINE_PARTIALTRIGGERS40508B = 66;// "PAYMENTOFFINE(PARTIALTRIGGERS40508B)";
            public const Decimal PAYFINETRIALBYDECLARATIONINABSENTIA = 51;//"PAYFINETRIALBYDECLARATIONINABSENTIA";


            public const Decimal COURTTRIAL = 17;// "COURTTRIAL";
            public const Decimal COURTTRIALPROP36 = 97;// "COURTTRIALPROP36";
            public const String ARRAIGNMENTTEXT = "ARRAIGNMENT";
            public const String COURTTRIALTEXT = "COURTTRIAL";
            public const String SUMMARYJUDGMENT = "SUMMARYJUDGMENT";
            public const int SUMMARYJUDGMENTID = 78;

        }
        #endregion

        #region CitaionSourceType
        public class CitaionSourceType
        {
            public const String AUTOCITE = "Auto Cite";
            public const String MANUALENTRY = "Manual Entry";
            public const String PHOTOREDLIGHT = "Photo Red Light";
            public const String SOFTFILE = "Softfile";
            public const String PHOTORADAR = "Photo Radar";
        }

        #endregion

        public class NoticeType
        {

            public const Int32 AdultCourtesyNotice = 1;
            public const Int32 AdultDelinquencyNotice = 2;
            public const Int32 JuvenileCourtesyNotice = 3;
            public const Int32 JuvenileDelinquencyNotice = 4;
            public const Int32 TBDANoticeofFailuretoPay = 5;
            public const Int32 NoticeofFailuretoPayFine = 6;
            public const Int32 MandatoryAdultCourtesyNotice = 7;
            public const Int32 MandatoryAdultDelinquencyNotice = 8;
            public const Int32 MandatoryJuvenileCourtesyNotice = 9;
            public const Int32 MandatoryJuvenileDelinquencyNotice = 10;

        }
        public const string DeliquencyNotice = "DELINQUENCY";
        public const string Notice_Value = "NoticeEngine";

        #region CitaionType
        public class CitaionType
        {
            public const String Boating = "Boating";
            public const String Traffic = "Traffic";
            public const String NONTRAFFIC = "Non Traffic";
            public const String NOTICETOCORRECT = "NoticeToCorrect";

        }

        #endregion

        #region SpeedDetectionMethod
        public class SpeedDetectionMethod
        {
            public const String LIDAR = "LIDAR";
            public const String PHOTO = "PHOTO";
            public const String RADAR = "RADAR";

        }

        #endregion

        #region S_Configurable

        public const int MISDEMEANOR_THRESHOLD = 3;
        public class S_Configurable
        {
            //  public const int MISDEMEANOR_THRESHOLD = 3;
            public const Int32 FTXGRACEPERIOD = 4;
            public const Int32 JUVENILEAGE = 5;
            public const Int32 OVERSPEEDLIMIT = 9;
        }

        #endregion

        #region trafficschool_type



        public class TrafficschoolType
        {
            public const String hour_8 = "8 hour";
            public const String hour_16 = "16 hour";
            public const String Chronic = "Chronic";
            public const String dism_hour_8 = "8 hour/dism";
            public const String dism_hour_16 = "16 hour/dism";
        }

        #endregion

        #region NoticeType
        public const String DELINQUENCYNOTICE = "DelinquencyNotice";
        #endregion

        #region Filing
        public const string COURTDATE_MODE_FILING = "FILING";
        public const string COURTDATE_MODE_SETASIDE = "SETASIDE";
        public const String FILING_COURTDATE = "COURTDATE";
        public const String FILING_QTODEPT = "QTODEPT";
        public const String FILING_PAYMENT = "PAYMENT";
        public const String FILING_NOACTIONREQUIRED = "NOACTION";
        public const String FILING_MOTIONAT = "V";
        public const string WQTYPE_COR = "COR";
        public const string WQTYPE_CUS = "CUS";
        public const string WQTYPE_ACC = "ACC";
        public const string WQTYPE_TBD = "TBD";
        public const string WQTYPE_TBDA = "TBDA";
        public const string WQTYPE_EJR = "EJR";
        public const string FILING_ORAL = "ORAL";

        public const string DEFENDANT_DECLARATIONS_ID = "40";
        public const string ARRESTING_OFFICER_DECLARATIONS_ID = "41";
        public const string CITING_OFFICER_DECLARATIONS_ID = "42";

        public const string DEFENDANT_DECLARATIONS = "DEFENDANT'S DECLARATIONS";
        public const string ARRESTING_OFFICER_DECLARATIONS = "ARRESTING OFFICER'S DECLARATIONS";
        public const string CITING_OFFICER_DECLARATIONS = "CITING OFFICER'S DECLARATIONS";
        public const string VACATE_COURTDATE = "MOTION TO VACATE COURT DATE";
        public const string FILING_DENOVO = "REQUEST FOR TRIAL DE NOVO";
        public const string TBDDEFENDANT_REASONFORTRACKINGID = "29";
        public const string TBDARRESTINGOFFICER_REASONFORTRACKINGID = "30";
        public const string TBDCITINGOFFICER_REASONFORTRACKINGID = "31";
        public const string BEHAVIOR_CATEGORY_ONE = "1";
        public const string BEHAVIOR_CATEGORY_TWO = "2";
        public const string BEHAVIOR_CATEGORY_THREE = "3";
        public const string BEHAVIOR_CATEGORY_FOUR = "4";
        public const string BEHAVIOR_CATEGORY_FIVE = "5";
        public const string BEHAVIOR_CATEGORY_SIX = "6";
        public const string BEHAVIOR_CATEGORY_SEVEN = "7";
        public const string BEHAVIOR_CATEGORY_EIGHT = "8";
        public const string BEHAVIOR_CATEGORY_NINE = "9";
        public const string BEHAVIOR_CATEGORY_TEN = "10";
        public const string BEHAVIOR_CATEGORY_ELEVEN = "11";
        public const string BEHAVIOR_CATEGORY_TWELVE_A = "12A";
        public const string BEHAVIOR_CATEGORY_TWELVE_B = "12B";
        public const string BEHAVIOR_CATEGORY_THIRTEEN = "13";
        public const string BEHAVIOR_CATEGORY_FOURTEEN = "14";
        public const string FILING_EVENT_SUBTYPE_GROUP = "SENTENCE";
        public const string FILING_DOC_TYPE_DENOVO = "REQUEST FOR TRIAL DE NOVO";
        public const string MOTION_TRANSER_COUNTY = "MOTION TO TRANSFER TO COUNTY SEAT";
        public const string MOTION_TRANSER_COUNTY_CODE = "WWM";

        #endregion

        #region User Department Maintenance
        public const string UDM_DEPT = "Department";
        public const string UDM_EVENT = "Event";
        public const string UDM_IMAGE = "Image";
        public const string UDM_OTHERS = "Others";

        public const string UDM_ACCESSIND_D = "D";
        public const string UDM_ACCESSIND_U = "U";
        #endregion

        #region DMV
        public struct DMVTransactionType
        {
            public const String DE1CONVICTION = "DE1C";
            public const String DE1REVERSAL = "DE1R";
            public const String DE1AMENDMENT = "DE1A";
            public const String DE1TSDISMISSAL = "DE1T";
            public const String DE1OWNERSRESPONSIBILITY = "DE1O";
            public const String DE1FTC = "DE1F";
            public const String DE1FTCRECALL = "DE1L";
            public const String DE5FTA = "DE5F";
            public const String DE2FTARECALL = "DE2L";
            public const String DE2FTARELEASE = "DE2R";
            public const String DE4FTPRELEASE = "DE4F";
            public const String DE3FTP = "DE3F";
            public const String DE1TSAMENDMENT = "DE1N";
        }
        public const String DE5OWNERSRESPO = "CVC 40002.1";
        public const String DE1OWNERSRESPO = "CVC 40001";
        public const String FTCISSUEVIOLATION = "40509.1";
        public const String OWNERSRESPODLN = "A0000000";
        public const String DMV_VALUE = "DMV";
        public const String DMV_NAME = "DMV NAME";
        public const String AKA_NAME = "AKA NAME";
        public const String EDIT_ERROR_PROCESS = "EE";
        public const String GOOD_PROCESS = "GO";
        public const String DOB_ERRORCODE = "137";
        public const String DMV_VERIFICATION_ERROR = "DE";
        public const String DMV_VERIFICATION_COMPLETE = "DV";
        #endregion DMV
        public const string prefixReason = "Reason is :";
        #region Installment Payments
        public enum InstallmentType
        {
            TSINSTALLMENT=0,
            BFINSTALLMENT=1
        }
        #endregion       

        #region BONDCOMPANY
        public const string BONDCOMPANY = "Bond Company";
        public const string ADDBONDCOMPANY = "ADD BOND COMPANY";
        public const string UPDATEBONDCOMPANY = "UPDATE BOND COMPANY";
        public const string VALIDSAVEGROUP = "validSave";       
        #endregion

        #region SURETYCOMPANY
        public const string SURETYCOMPANY = "Surety Company";
        public const string ADDSURETYCOMPANY = "ADD SURETY COMPANY";
        public const string UPDATESURETYCOMPANY = "UPDATE SURETY COMPANY";       
        #endregion

        #region Genereal Service Constancts
        public class GeneralService
        {
            public const int CRIMINALCASETYPEID = 1;
            public const int TRAFFICCASETYPEID = 4;
            public const string NOTAPPLICABLE = "N/A";
            public const string RECDATCOUNTER = "C";
            public const string RECDATMAIL = "M";
            public const int EXCESSFEEMINIMUMVALUE = 10;
        }
        #endregion

        #region Handle Recall
        public const string RECALLCA = "Recall Civil Assessment";
        public const string RECALLCITATIONANDCA = "Recall Citation And Civil Assessment";
        #endregion

        public const string DEFAULTLANGUAGE = "Y";
        public const string CALENDARCASE_WS_SCHEDULE = "WS";

        #region Check Writing
        public const string TRANSACTIONTYPE_REFUND = "Refund";
        public const string TRANSACTIONTYPE_STOPPAYMENT = "Stop Payment";
        #endregion


    }
}
