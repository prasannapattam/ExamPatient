using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Runtime.InteropServices;
using System.Web.Configuration;
using System.IO;
using System.Collections;

public delegate void SelectedFileChangedHandler(object sender, EventArgs e);

public partial class FileBrowser : System.Web.UI.UserControl
{
    public event SelectedFileChangedHandler SelectedFileChanged;
    public event SelectedFileChangedHandler SelectedFolderChanged;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public string UserName
    {
        get { return ViewState["UserName"].ToString(); }
        set { ViewState["UserName"] = value; }
    }
    public string UserDomain
    {
        get { return ViewState["UserDomain"].ToString(); }
        set { ViewState["UserDomain"] = value; }
    }
    public string UserPassword
    {
        get { return ViewState["UserPassword"].ToString(); }
        set { ViewState["UserPassword"] = value; }
    }
    public FileInfo SelectedFile
    {
        get { return (FileInfo)ViewState["SelectedFile"]; }
        private set { ViewState["SelectedFile"] = value; }
    }
    public string CurrentFolder
    {
        get { return ViewState["CurrentFolder"].ToString(); }
        set { ViewState["CurrentFolder"] = value; }
    }
    public string RootFolder
    {
        get { return ViewState["RootFolder"].ToString(); }
        set { ViewState["RootFolder"] = value; }
    }

    protected void ListView_Folders_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
    {
        if (e.NewSelectedIndex > -1)
        {
            ListViewItem selItem = ListView_Folders.Items[e.NewSelectedIndex];
            LinkButton myButton = (LinkButton)selItem.FindControl("Button_Name");
            if (myButton != null)
            {
                CurrentFolder = Path.Combine(CurrentFolder, myButton.Text);
                if (SelectedFolderChanged != null) SelectedFolderChanged(this, new EventArgs());
                Refresh();
            }
        }
    }

    protected void ListView_Files_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
    {
        if (e.NewSelectedIndex > -1)
        {
            ListViewItem selItem = ListView_Files.Items[e.NewSelectedIndex];
            using (LinkButton myButton = (LinkButton)selItem.FindControl("Button_Name"))
            {
                if (myButton != null)
                {
                    SelectedFile = new FileInfo(myButton.ToolTip);
                    if (SelectedFileChanged != null) SelectedFileChanged(this, new EventArgs());
                }
            }

        }
    }

    protected void Button_Parent_Click(object sender, EventArgs e)
    {
        if (CurrentFolder != Button_Parent.ToolTip)
        {
            CurrentFolder = Button_Parent.ToolTip;
            Refresh();
        }
    }

    public void Refresh()
    {
        IntPtr userToken = ImpersonateUser(UserName, UserDomain, UserPassword);

        if (Directory.Exists(CurrentFolder))
        {
            DirectoryInfo dirInfo = new DirectoryInfo(CurrentFolder);
            if (dirInfo != null)
            {
                if (CurrentFolder != RootFolder)
                {
                    Button_Parent.ToolTip = dirInfo.Parent.FullName;
                }
                else
                {
                    Button_Parent.ToolTip = dirInfo.FullName;
                }
                FileSystemInfoComparer myComparer = new FileSystemInfoComparer();

                DirectoryInfo[] DirArray = dirInfo.GetDirectories();
                Array.Sort(DirArray, myComparer);

                FileInfo[] FileArray = dirInfo.GetFiles();
                Array.Sort(FileArray, myComparer);

                ListView_Folders.DataSource = DirArray;
                ListView_Folders.DataBind();

                ListView_Files.DataSource = FileArray;
                ListView_Files.DataBind();
            }
        }

        UndoImpersonation(userToken);
    }

    private IntPtr ImpersonateUser(string user, string domain, string password)
    {
        IntPtr lnToken = new IntPtr(0);
        Int32 TResult = LogonUser(user, domain, password, LOGON32_LOGON_NETWORK_CLEARTEXT, LOGON32_PROVIDER_WINNT50, ref lnToken);

        if (TResult > 0)
        {
            ImpersonateLoggedOnUser(lnToken);
        }

        return lnToken;
    }

    private void UndoImpersonation(IntPtr lnToken)
    {
        RevertToSelf();
        CloseHandle(lnToken);
    }

    #region Interop

    [DllImport("advapi32.dll", SetLastError = true)]
    private static extern Int32 LogonUser(string lpszUsername, string lpszDomain, string lpszPassword, Int32 dwLogonType, Int32 dwLogonProvider, ref IntPtr phToken);

    [DllImport("advapi32.dll", SetLastError = true)]
    private static extern Int32 ImpersonateLoggedOnUser(IntPtr hToken);

    [DllImport("advapi32.dll", SetLastError = true)]
    private static extern Int32 RevertToSelf();

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern Int32 CloseHandle(IntPtr hObject);

    const Int32 LOGON32_LOGON_INTERACTIVE = 2;
    const Int32 LOGON32_LOGON_NETWORK = 3;
    const Int32 LOGON32_LOGON_BATCH = 4;
    const Int32 LOGON32_LOGON_SERVICE = 5;
    const Int32 LOGON32_LOGON_UNLOCK = 7;
    const Int32 LOGON32_LOGON_NETWORK_CLEARTEXT = 8;
    const Int32 LOGON32_LOGON_NEW_CREDENTIALS = 9;
    const Int32 LOGON32_PROVIDER_DEFAULT = 0;
    const Int32 LOGON32_PROVIDER_WINNT35 = 1;
    const Int32 LOGON32_PROVIDER_WINNT40 = 2;
    const Int32 LOGON32_PROVIDER_WINNT50 = 3;

    #endregion Interop



}

public class FileSystemInfoComparer : IComparer
{
    public FileSystemInfoComparer()
        : base()
    {
    }

    #region IComparer Members

    public int Compare(object x, object y)
    {
        FileSystemInfo fsi1 = (FileSystemInfo)x;
        FileSystemInfo fsi2 = (FileSystemInfo)y;

        if (fsi1 == null)
        {
            if (fsi2 == null)
            {
                // If fsi1 is null and fsi2 is null, they're
                // equal. 
                return 0;
            }
            else
            {
                // If fsi1 is null and fsi2 is not null, fsi2
                // is greater. 
                return -1;
            }
        }
        else
        {
            // If fsi1 is not null...
            //
            if (fsi2 == null)
            // ...and fsi2 is null, fsi1 is greater.
            {
                return 1;
            }
            else
            {
                // ...and fsi2 is not null, compare the 
                // lengths of the two strings.
                //
                int retval = fsi1.Name.CompareTo(fsi2.Name);
                return retval;

            }
        }
    }

    #endregion
}

