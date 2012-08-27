using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace STDF
{
    public partial class MainForm : Form, System.Collections.IComparer
    {
        /// <summary>
        /// ColumnIndex is used to indicate a column in the listview.
        /// </summary>
        enum ColumnIndex
        {
            Selection = 0,
            Path = 1,
            Name = 2,
            Size = 3,
            Hash = 4
        };

        public delegate void UpdateListItem(ListViewItem lvi, string hash);

        private List<String> _directories;
        private int _directoryCount;

        private int _col;
        private bool _cancel;
        private UpdateListItem _updateHash;

        public MainForm()
        {
            InitializeComponent();

            _col = (int)ColumnIndex.Path;
            listView.ListViewItemSorter = this;

            _directories = new List<string>();
            
            _updateHash = new UpdateListItem(UpdateHash);

            UpdateUI(false);
        }

        /// <summary>
        /// Called when strating and ending a lengthy UI update. When starting 
        /// it obscures the listview with a progress panel. This greatly 
        /// decreases the number repaints necessary in a listview with lots of
        /// items and greatly increases the speed at which they can be updated.
        /// </summary>
        /// <param name="bWorking">Indicates if the start or end of a lengthy UI update.</param>
        void UpdateUI(bool bWorking)
        {
            _cancel = false;
            fileMenuItem.Visible = !bWorking;
            actionMenuItem.Visible = !bWorking;
            cancelMenuItem.Visible = bWorking;

            if (bWorking)
            {
                panelProgress.Visible = true;
                listView.Visible = false;
            }
            else
            {
                listView.Visible = true;
                panelProgress.Visible = false;
            }

            int i = listView.Items.Count;
            if (i > 0)
                countStatusBarPanel.Text = i + " files";
            else
                countStatusBarPanel.Text = "no files";

            progressStatusBarPanel.Text = "";
        }

        /// <summary>
        /// The comparison function used to sort the listview. It determines which column is being sorted on by examining the _col property.
        /// </summary>
        /// <param name="x">first listview item</param>
        /// <param name="y">second listview item</param>
        /// <returns>
        /// Negative vaules indicate the first listview item comes before the second.
        /// Positive vaules indicate the first listview item comes after the second.
        /// Zero indicates the two listview items are equal.
        /// </returns>
        public int Compare(object x, object y)
        {
            ListViewItem lvix = (ListViewItem)x;
            ListViewItem lviy = (ListViewItem)y;

            if (_col == (int)ColumnIndex.Selection)
            {
                if (lvix.Checked == lviy.Checked)
                    return 0;
                else if (lvix.Checked == true)
                    return -1;
                else
                    return 1;
            }
            else
                if (_col == (int)ColumnIndex.Size)
                {
                    System.Int64 diff = Convert.ToInt64(lvix.SubItems[(int)ColumnIndex.Size].Text) - Convert.ToInt64(lviy.SubItems[(int)ColumnIndex.Size].Text);
                    if (diff < 0)
                        return -1;
                    if (diff > 0)
                        return 1;
                    else
                        return 0;
                }
                else
                {
                    return String.Compare(lvix.SubItems[_col].Text, lviy.SubItems[_col].Text);
                }
        }

        /// <summary>
        /// Called by Scan to scan a single directory adding the files found to the listview and queuing any subdirectories for later scans.
        /// </summary>
        /// <param name="path">The directory to scan</param>
        void ScanDir(string path)
        {
            try
            {
                string[] files = Directory.GetFiles(path);
                ListViewItem[] items = new ListViewItem[files.Length];

                for (int i = 0; i < files.Length; i++)
                {
                    items[i] = new ListViewItem();
                    FileInfo fi = new FileInfo(files[i]);

                    items[i].Text = "";
                    items[i].SubItems.Add(fi.DirectoryName);
                    items[i].SubItems.Add(fi.Name);
                    items[i].SubItems.Add(fi.Length.ToString());

                    string hash = "-??-";
                    items[i].SubItems.Add(hash);

                    progressStatusBarPanel.Text = listView.Items.Count + " files with " + _directories.Count + " directories remaining";
                    Application.DoEvents();
                }

                listView.Items.AddRange(items);
            }
            catch
            {
            }

            try
            {
                string[] subdirectories = Directory.GetDirectories(path);
                foreach (string subdirectory in subdirectories)
                {
                    DirectoryInfo di = new DirectoryInfo(subdirectory);

                    _directories.Add(subdirectory);
                    _directoryCount++;

                    progressStatusBarPanel.Text = listView.Items.Count + " files with " + _directories.Count + " directories remaining";
                    Application.DoEvents();
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Scans the specified path recursively adding all files found therein.
        /// </summary>
        /// <param name="path">The directory to scan</param>
        void Scan(string path)
        {
            _directories.Clear();
            _directoryCount = 0;

            UpdateUI(true);

            _directories.Add(path);
            _directoryCount++;

            while (_directories.Count > 0)
            {
                if (_cancel == false)
                {
                    string currentDirectory = _directories[0];
                    _directories.RemoveAt(0);
                    ScanDir(currentDirectory);

                    progressBar.Maximum = _directoryCount;
                    progressBar.Value = _directoryCount - _directories.Count;
                    Application.DoEvents();
                }
                else
                {
                    _directories.Clear();
                }
            }

            if (_cancel == true)
            {
                listView.Items.Clear();
            }

            UpdateUI(false);
        }

        /// <summary>
        /// Prunes the listview of unique items based on the column provided.
        /// </summary>
        /// <param name="columnIndex">the column to prune.</param>
        void Prune(int columnIndex)
        {
            Debug.Assert(columnIndex == (int)ColumnIndex.Name || columnIndex == (int)ColumnIndex.Size || columnIndex == (int)ColumnIndex.Hash);

            _col = columnIndex;
            listView.Sort();
            Application.DoEvents();

            int nCount = 0;
            int nMatches = 0;
            int nState = 0;
            string str1 = "";
            string str2 = "";
            bool bKeep = false;
            bool bColor = false;

            progressBar.Maximum = listView.Items.Count;

            ListViewItem lvi1 = null;
            foreach (ListViewItem lvi in listView.Items)
            {
                switch (nState)
                {
                    case 0: // Initial State
                        str1 = lvi.SubItems[_col].Text;
                        lvi1 = lvi;
                        nState = 1;
                        break;
                    case 1: // looking for new matches
                        str2 = lvi.SubItems[_col].Text;

                        if (_col == (int)ColumnIndex.Size)
                        {
                            bKeep = (Convert.ToInt64(str1) == Convert.ToInt64(str2));
                        }
                        else
                        {
                            bKeep = (String.Compare(str1, str2) == 0);
                        }

                        if (bKeep)
                        {
                            // If we have a match, set the background color different from the previous match
                            // and look for more matches belonging to this group.
                            bColor = !bColor;
                            lvi1.BackColor = (bColor) ? Color.LightBlue : Color.White;
                            lvi1.UseItemStyleForSubItems = true;

                            str1 = str2;
                            lvi1 = lvi;
                            nState = 2;
                            nMatches++;

                            lvi1.BackColor = (bColor) ? Color.LightBlue : Color.White;
                            lvi1.UseItemStyleForSubItems = true;
                        }
                        else
                        {
                            // If we don't have a match, remove the previous item and look for
                            // new matches with this item.
                            listView.Items.Remove(lvi1);
                            str1 = str2;
                            lvi1 = lvi;
                        }
                        break;
                    case 2: // looking for more matches
                        str2 = lvi.SubItems[_col].Text;

                        if (_col == (int)ColumnIndex.Size)
                        {
                            bKeep = (Convert.ToInt64(str1) == Convert.ToInt64(str2));
                        }
                        else
                        {
                            bKeep = (String.Compare(str1, str2) == 0);
                        }

                        if (bKeep)
                        {
                            // If we found another match add it to the other group.
                            str1 = str2;
                            lvi1 = lvi;
                            nMatches++;

                            lvi1.BackColor = (bColor) ? Color.LightBlue : Color.White;
                            lvi1.UseItemStyleForSubItems = true;
                        }
                        else
                        {
                            // If not, start looking for new matches
                            str1 = str2;
                            lvi1 = lvi;
                            nState = 1;
                        }
                        break;
                }

                if (_cancel == true)
                    return;

                nCount++;
                progressStatusBarPanel.Text = nMatches + " potential matches, with " + (listView.Items.Count - nMatches) + " files to process";
                progressBar.Value = nCount;
                Application.DoEvents();
            }

            if (nState == 1 && lvi1 != null)
                listView.Items.Remove(lvi1);
        }

        /// <summary>
        /// Called through a delegate to update the hash of a listview item.
        /// </summary>
        /// <param name="lvi"></param>
        /// <param name="hash"></param>
        void UpdateHash(ListViewItem lvi, string hash)
        {
            lvi.SubItems[(int)ColumnIndex.Hash].Text = hash;
        }

        /// <summary>
        /// Queues HashJobs to compute the hash of all listview items. Then 
        /// watches list of HashJobs and removes those that are complete. 
        /// If the user presses the cancel, cancels and waits for them all 
        /// to acknowledge.
        /// </summary>
        void CalcHashes()
        {
            int nCount = 0;

            List<HashJob> jobs = new List<HashJob>();

            foreach (ListViewItem lvi in listView.Items)
            {
                string filename = Path.Combine(lvi.SubItems[(int)ColumnIndex.Path].Text, lvi.SubItems[(int)ColumnIndex.Name].Text);

                jobs.Add(HashJob.Queue(filename, lvi, listView, _updateHash));
                nCount++;

                progressStatusBarPanel.Text = nCount + " hashes queued, with " + (listView.Items.Count - nCount) + " files to process";
                progressBar.Maximum = listView.Items.Count;
                progressBar.Value = nCount;

                Application.DoEvents();
            }

            while(jobs.Count > 0)
            {
                if (jobs[0].IsDone())
                    jobs.RemoveAt(0);

                if (_cancel == true)
                {
                    foreach(HashJob hj in jobs)
                        hj.Cancel();
                }

                int nDone = listView.Items.Count - jobs.Count;
                progressStatusBarPanel.Text = nDone + " files computed, " + jobs.Count + " remaining";
                progressBar.Maximum = listView.Items.Count;
                progressBar.Value = nDone;
                Application.DoEvents();
            }
        }

        /// <summary>
        /// Prompts the user for a file of save listview items and rehydrates 
        /// the listview from the file.
        /// </summary>
        void LoadFile()
        {
            listView.Items.Clear();

            OpenFileDialog ofd = new OpenFileDialog();

            ofd.CheckFileExists = true;
            ofd.DefaultExt = "stdf";
            ofd.Multiselect = false;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Stream strm = ofd.OpenFile();
                BinaryReader br = new BinaryReader(strm);

                listView.Items.Clear();

                int nCount = br.ReadInt32();

                ListViewItem[] items = new ListViewItem[nCount];

                for (int i = 0; i < nCount; i++)
                {
                    items[i] = new ListViewItem();
                    items[i].Text = "";
                    items[i].SubItems.Add(br.ReadString());
                    items[i].SubItems.Add(br.ReadString());
                    items[i].SubItems.Add(br.ReadInt64().ToString());
                    items[i].SubItems.Add(br.ReadString());

                    progressStatusBarPanel.Text = "Reading " + i + " of " + nCount + " items";
                    progressBar.Maximum = nCount;
                    progressBar.Value = i;
                    Application.DoEvents();

                    if (_cancel == true)
                        return;
                }

                listView.Items.AddRange(items);
            }
        }

        /// <summary>
        /// Prompts the user for a file to save the contents of the listview in.
        /// </summary>
        void SaveFile()
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.DefaultExt = "stdf";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Stream strm = sfd.OpenFile();
                BinaryWriter bw = new BinaryWriter(strm);

                int i = 0;
                int nCount = listView.Items.Count;
                bw.Write(nCount);
                foreach (ListViewItem lvi in listView.Items)
                {
                    bw.Write(lvi.SubItems[1].Text);
                    bw.Write(lvi.SubItems[2].Text);
                    bw.Write(Convert.ToInt64(lvi.SubItems[3].Text));
                    bw.Write(lvi.SubItems[4].Text);
                    i++;

                    progressStatusBarPanel.Text = "Writing " + i + " of " + nCount + " items";
                    progressBar.Maximum = nCount;
                    progressBar.Value = i;
                    Application.DoEvents();

                    if (_cancel == true)
                        return;
                }
            }
        }

        /// <summary>
        /// Called when the user clicks on a column header.
        /// </summary>
        void ListViewColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
        {
            _col = e.Column;

            listView.Sort();
        }

        /// <summary>
        /// Called when a user selects the New menu item.
        /// </summary>
        void NewMenuItemClick(object sender, System.EventArgs e)
        {
            listView.Items.Clear();
        }

        /// <summary>
        /// Called when a user selects the Open menu item.
        /// </summary>
        void OpenMenuItemClick(object sender, System.EventArgs e)
        {
            UpdateUI(true);

            LoadFile();

            UpdateUI(false);
        }

        /// <summary>
        /// Called when a user selects the Save menu item.
        /// </summary>
        void SaveMenuItemClick(object sender, System.EventArgs e)
        {
            UpdateUI(true);

            SaveFile();

            UpdateUI(false);
        }

        /// <summary>
        /// Called when a user selects the Exit menu item.
        /// </summary>
        void ExitMenuItemClick(object sender, System.EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Called when a user selects the Scan menu item.
        /// </summary>
        void ScanMenuItemClick(object sender, System.EventArgs e)
        {
            folderBrowserDialog.Description = "Select the directory to search";

            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                Scan(folderBrowserDialog.SelectedPath);
            }
        }

        /// <summary>
        /// Called when a user selects the Calculate Hashes menu item.
        /// </summary>
        void CalcHashesMenuItemClick(object sender, System.EventArgs e)
        {
            UpdateUI(true);

            CalcHashes();

            UpdateUI(false);
        }

        /// <summary>
        /// Called when a user selects the New menu item.
        /// </summary>
        void UniqueFileNamesMenuItemClick(object sender, System.EventArgs e)
        {
            UpdateUI(true);

            Prune((int)ColumnIndex.Name);

            UpdateUI(false);
        }

        /// <summary>
        /// Called when a user selects the Prune by Unique File Sizes menu item.
        /// </summary>
        void UniqueFileSizesMenuItemClick(object sender, System.EventArgs e)
        {
            UpdateUI(true);

            Prune((int)ColumnIndex.Size);

            UpdateUI(false);
        }

        /// <summary>
        /// Called when a user selects the Prune by Unique File Hashes menu item.
        /// </summary>
        void UniqueFileHashesMenuItemClick(object sender, System.EventArgs e)
        {
            UpdateUI(true);

            Prune((int)ColumnIndex.Hash);

            UpdateUI(false);
        }

        /// <summary>
        /// Called when a user selects the Select Paths Containing menu item.
        /// </summary>
        void PathsContainingMenuItemClick(object sender, System.EventArgs e)
        {
            UpdateUI(true);

            string criteria = SelectionForm.GetSelection(this, true);
            criteria = criteria.Replace(@"\", @"\\");
            Regex re = new Regex(criteria, RegexOptions.IgnoreCase);

            foreach (ListViewItem lvi in listView.Items)
            {
                if (re.IsMatch(lvi.SubItems[(int)ColumnIndex.Path].Text))
                    lvi.Checked = true;

                if (_cancel == true)
                    break;
            }

            UpdateUI(false);
        }

        /// <summary>
        /// Called when a user selects the Select Names Containing menu item.
        /// </summary>
        void NamesContainingMenuItemClick(object sender, System.EventArgs e)
        {
            UpdateUI(true);

            string criteria = SelectionForm.GetSelection(this, false);
            Regex re = new Regex(criteria, RegexOptions.IgnoreCase);

            foreach (ListViewItem lvi in listView.Items)
            {
                if (re.IsMatch(lvi.SubItems[(int)ColumnIndex.Name].Text))
                    lvi.Checked = true;

                if (_cancel == true)
                    break;
            }

            UpdateUI(false);
        }

        /// <summary>
        /// Called when a user selects the Select All menu item.
        /// </summary>
        void SelectAllMenuItemClick(object sender, System.EventArgs e)
        {
            UpdateUI(true);

            foreach (ListViewItem lvi in listView.Items)
            {
                lvi.Checked = true;

                if (_cancel == true)
                    break;
            }

            UpdateUI(false);
        }

        /// <summary>
        /// Called when a user selects the Deselect All menu item.
        /// </summary>
        void DeselectAllMenuItemClick(object sender, System.EventArgs e)
        {
            UpdateUI(true);

            foreach (ListViewItem lvi in listView.Items)
            {
                lvi.Checked = false;

                if (_cancel == true)
                    break;
            }

            UpdateUI(false);
        }

        /// <summary>
        /// Called when a user selects the Invert Selection menu item.
        /// </summary>
        void InvertSelectionMenuItemClick(object sender, System.EventArgs e)
        {
            UpdateUI(true);

            foreach (ListViewItem lvi in listView.Items)
            {
                lvi.Checked = !lvi.Checked;

                if (_cancel == true)
                    break;
            }

            UpdateUI(false);
        }

        /// <summary>
        /// Called when a user selects the Cancel menu item. This is only 
        /// visible when UpdateUI is true.
        /// </summary>
        void CancelMenuItemClick(object sender, System.EventArgs e)
        {
            _cancel = true;
        }

        /// <summary>
        /// Called by the UI before displaying the context menu on a listview 
        /// item.
        /// </summary>
        void ContextMenuPopup(object sender, System.EventArgs e)
        {
            if (listView.SelectedItems.Count == 1)
            {
                openFolderMenuItem.Enabled = true;
                openFileMenuItem.Enabled = true;
            }
            else
            {
                openFolderMenuItem.Enabled = false;
                openFileMenuItem.Enabled = false;
            }
        }

        /// <summary>
        /// Called when the user selects Open Folder from the context menu.
        /// </summary>
        void OpenFolderMenuItemClick(object sender, System.EventArgs e)
        {
            string path = listView.SelectedItems[0].SubItems[(int)ColumnIndex.Path].Text;
            System.Diagnostics.Process.Start(path);
        }

        /// <summary>
        /// Called when the user selects Open File from the context menu.
        /// </summary>
        void OpenFileMenuItemClick(object sender, System.EventArgs e)
        {
            string path = listView.SelectedItems[0].SubItems[(int)ColumnIndex.Path].Text;
            string filename = listView.SelectedItems[0].SubItems[(int)ColumnIndex.Name].Text;
            string pathname = Path.Combine(path, filename);
            System.Diagnostics.Process.Start(pathname);
        }

        /// <summary>
        /// Called when the user selects the Copy menu item.
        /// </summary>
        void CopyMenuItemClick(object sender, System.EventArgs e)
        {
            int nCount = listView.CheckedItems.Count;

            if (nCount > 0)
            {
                folderBrowserDialog.Description = "Select the target directory";

                DialogResult result = folderBrowserDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    ShellLib.ShellFileOperation fo = new ShellLib.ShellFileOperation();
                    string[] source = new string[nCount];
                    string[] dest = new string[nCount];

                    for (int i = 0; i < nCount; i++)
                    {
                        string path = listView.CheckedItems[i].SubItems[(int)ColumnIndex.Path].Text;
                        string filename = listView.CheckedItems[i].SubItems[(int)ColumnIndex.Name].Text;
                        source[i] = Path.Combine(path, filename);

                        dest[i] = Path.GetPathRoot(source[i]);
                        dest[i] = source[i].Remove(0, dest[i].Length);
                        dest[i] = Path.Combine(folderBrowserDialog.SelectedPath, dest[i]);
                    }

                    fo.Operation = ShellLib.ShellApi.FileOperations.FO_COPY;
                    fo.OwnerWindow = this.Handle;
                    fo.ProgressTitle = "Copying Checked Files";
                    fo.SourceFiles = source;
                    fo.DestFiles = dest;

                    bool RetVal = fo.DoOperation();

                    foreach (ListViewItem lvi in listView.CheckedItems)
                        lvi.Checked = false;
                }
            }
        }

        /// <summary>
        /// Called when the user selects the Move menu item.
        /// </summary>
        void MoveMenuItemClick(object sender, System.EventArgs e)
        {
            int nCount = listView.CheckedItems.Count;

            if (nCount > 0)
            {
                folderBrowserDialog.Description = "Select the target directory";

                DialogResult result = folderBrowserDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    ShellLib.ShellFileOperation fo = new ShellLib.ShellFileOperation();

                    string[] source = new string[nCount];
                    string[] dest = new string[nCount];

                    for (int i = 0; i < nCount; i++)
                    {
                        string path = listView.CheckedItems[i].SubItems[(int)ColumnIndex.Path].Text;
                        string filename = listView.CheckedItems[i].SubItems[(int)ColumnIndex.Name].Text;
                        source[i] = Path.Combine(path, filename);

                        dest[i] = Path.GetPathRoot(source[i]);
                        dest[i] = source[i].Remove(0, dest[i].Length);
                        dest[i] = Path.Combine(folderBrowserDialog.SelectedPath, dest[i]);
                    }

                    fo.Operation = ShellLib.ShellApi.FileOperations.FO_MOVE;
                    fo.OwnerWindow = this.Handle;
                    fo.ProgressTitle = "Moving Checked Files";
                    fo.SourceFiles = source;
                    fo.DestFiles = dest;

                    bool RetVal = fo.DoOperation();

                    foreach (ListViewItem lvi in listView.CheckedItems)
                        listView.Items.Remove(lvi);

                }

            }
        }

        /// <summary>
        /// Called when the user selects the Delete menu item.
        /// </summary>
        void DeleteMenuItemClick(object sender, System.EventArgs e)
        {
            int nCount = listView.CheckedItems.Count;

            if (nCount > 0)
            {
                ShellLib.ShellFileOperation fo = new ShellLib.ShellFileOperation();

                string[] source = new string[nCount];

                for (int i = 0; i < nCount; i++)
                {
                    string path = listView.CheckedItems[i].SubItems[(int)ColumnIndex.Path].Text;
                    string filename = listView.CheckedItems[i].SubItems[(int)ColumnIndex.Name].Text;
                    source[i] = Path.Combine(path, filename);
                }

                fo.Operation = ShellLib.ShellApi.FileOperations.FO_DELETE;
                fo.OwnerWindow = this.Handle;
                fo.ProgressTitle = "Deleting Checked Files";
                fo.SourceFiles = source;

                bool RetVal = fo.DoOperation();

                foreach (ListViewItem lvi in listView.CheckedItems)
                    listView.Items.Remove(lvi);
            }
        }

        /// <summary>
        /// Called when the user releases a key on the keyboard and the 
        /// listview has the input focus.
        /// </summary>
        void ListViewKeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                e.Handled = true;
                foreach (ListViewItem lvi in listView.SelectedItems)
                    listView.Items.Remove(lvi);
            }
            else
                e.Handled = false;
        }

        /// <summary>
        /// Called when the user slects the One of Each menu item
        /// </summary>
        void OneOfEachMenuItemClick(object sender, System.EventArgs e)
        {
            UpdateUI(true);

            Color currentColor = Color.Black;

            foreach (ListViewItem lvi in listView.Items)
            {
                if (lvi.BackColor != currentColor)
                {
                    lvi.Checked = true;
                    currentColor = lvi.BackColor;
                }
                else
                {
                    lvi.Checked = false;
                }

                if (_cancel == true)
                    break;
            }

            UpdateUI(false);
        }

        /// <summary>
        /// Called when the user selects the Validate Selection menu item. 
        /// Ensures exactly one item in every group is selected. If not it selects
        /// then item in violation and makes sure it is scrolled into view.
        /// </summary>
        void ValidateSelectionMenuItemClick(object sender, System.EventArgs e)
        {
            UpdateUI(true);

            int beginIndex = 0;
            int checkCount = 0;
            int totalCount = 0;

            Color currentColor = Color.Black;

            for (int i = 0; i < listView.Items.Count; i++)
            {
                ListViewItem lvi = listView.Items[i];
                if (lvi.BackColor != currentColor)
                {
                    if (totalCount != 0)
                    {
                        if ((totalCount != (checkCount + 1)) && (checkCount != 1))
                        {
                            listView.SelectedItems.Clear();
                            listView.EnsureVisible(beginIndex);
                            listView.Items[beginIndex].Focused = true;
                            listView.Items[beginIndex].Selected = true;
                            break;
                        }
                    }

                    beginIndex = i;
                    totalCount = 0;
                    checkCount = 0;
                    currentColor = lvi.BackColor;
                }

                if (lvi.Checked)
                    checkCount++;

                totalCount++;

                if (_cancel == true)
                    break;
            }

            UpdateUI(false);
        }
    }
}
