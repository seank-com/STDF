namespace STDF
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenu = new System.Windows.Forms.ContextMenu();
            this.openFolderMenuItem = new System.Windows.Forms.MenuItem();
            this.openFileMenuItem = new System.Windows.Forms.MenuItem();
            this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
            this.fileMenuItem = new System.Windows.Forms.MenuItem();
            this.newMenuItem = new System.Windows.Forms.MenuItem();
            this.openMenuItem = new System.Windows.Forms.MenuItem();
            this.saveMenuItem = new System.Windows.Forms.MenuItem();
            this.seperatorMenuItem1 = new System.Windows.Forms.MenuItem();
            this.exitMenuItem = new System.Windows.Forms.MenuItem();
            this.actionMenuItem = new System.Windows.Forms.MenuItem();
            this.scanMenuItem = new System.Windows.Forms.MenuItem();
            this.calcHashesMenuItem = new System.Windows.Forms.MenuItem();
            this.seperatorMenuItem2 = new System.Windows.Forms.MenuItem();
            this.pruneMenuItem = new System.Windows.Forms.MenuItem();
            this.uniqueFileNamesMenuItem = new System.Windows.Forms.MenuItem();
            this.uniqueFileSizesMenuItem = new System.Windows.Forms.MenuItem();
            this.uniqueFileHashesMenuItem = new System.Windows.Forms.MenuItem();
            this.selectMenuItem = new System.Windows.Forms.MenuItem();
            this.pathsContainingMenuItem = new System.Windows.Forms.MenuItem();
            this.namesContainingMenuItem = new System.Windows.Forms.MenuItem();
            this.seperatorMenuItem3 = new System.Windows.Forms.MenuItem();
            this.selectAllMenuItem = new System.Windows.Forms.MenuItem();
            this.deselectAllMenuItem = new System.Windows.Forms.MenuItem();
            this.invertSelectionMenuItem = new System.Windows.Forms.MenuItem();
            this.oneOfEachMenuItem = new System.Windows.Forms.MenuItem();
            this.validateSelectionMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.selectionMenuItem = new System.Windows.Forms.MenuItem();
            this.copyMenuItem = new System.Windows.Forms.MenuItem();
            this.moveMenuItem = new System.Windows.Forms.MenuItem();
            this.deleteMenuItem = new System.Windows.Forms.MenuItem();
            this.cancelMenuItem = new System.Windows.Forms.MenuItem();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.listView = new System.Windows.Forms.ListView();
            this.columnSelection = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHash = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panelProgress = new System.Windows.Forms.Panel();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.statusBar = new System.Windows.Forms.StatusBar();
            this.progressStatusBarPanel = new System.Windows.Forms.StatusBarPanel();
            this.countStatusBarPanel = new System.Windows.Forms.StatusBarPanel();
            this.panelProgress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressStatusBarPanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.countStatusBarPanel)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenu
            // 
            this.contextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.openFolderMenuItem,
            this.openFileMenuItem});
            // 
            // openFolderMenuItem
            // 
            this.openFolderMenuItem.Index = 0;
            this.openFolderMenuItem.Text = "Open Folder";
            this.openFolderMenuItem.Click += new System.EventHandler(this.OpenFolderMenuItemClick);
            // 
            // openFileMenuItem
            // 
            this.openFileMenuItem.Index = 1;
            this.openFileMenuItem.Text = "Open File";
            this.openFileMenuItem.Click += new System.EventHandler(this.OpenFileMenuItemClick);
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.fileMenuItem,
            this.actionMenuItem,
            this.cancelMenuItem});
            // 
            // fileMenuItem
            // 
            this.fileMenuItem.Index = 0;
            this.fileMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.newMenuItem,
            this.openMenuItem,
            this.saveMenuItem,
            this.seperatorMenuItem1,
            this.exitMenuItem});
            this.fileMenuItem.Text = "&File";
            // 
            // newMenuItem
            // 
            this.newMenuItem.Index = 0;
            this.newMenuItem.Text = "&New";
            this.newMenuItem.Click += new System.EventHandler(this.NewMenuItemClick);
            // 
            // openMenuItem
            // 
            this.openMenuItem.Index = 1;
            this.openMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
            this.openMenuItem.Text = "&Open...";
            this.openMenuItem.Click += new System.EventHandler(this.OpenMenuItemClick);
            // 
            // saveMenuItem
            // 
            this.saveMenuItem.Index = 2;
            this.saveMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
            this.saveMenuItem.Text = "&Save...";
            this.saveMenuItem.Click += new System.EventHandler(this.SaveMenuItemClick);
            // 
            // seperatorMenuItem1
            // 
            this.seperatorMenuItem1.Index = 3;
            this.seperatorMenuItem1.Text = "-";
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Index = 4;
            this.exitMenuItem.Text = "E&xit";
            this.exitMenuItem.Click += new System.EventHandler(this.ExitMenuItemClick);
            // 
            // actionMenuItem
            // 
            this.actionMenuItem.Index = 1;
            this.actionMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.scanMenuItem,
            this.calcHashesMenuItem,
            this.seperatorMenuItem2,
            this.pruneMenuItem,
            this.selectMenuItem,
            this.menuItem1,
            this.selectionMenuItem});
            this.actionMenuItem.Text = "&Action";
            // 
            // scanMenuItem
            // 
            this.scanMenuItem.Index = 0;
            this.scanMenuItem.Text = "&Scan...";
            this.scanMenuItem.Click += new System.EventHandler(this.ScanMenuItemClick);
            // 
            // calcHashesMenuItem
            // 
            this.calcHashesMenuItem.Index = 1;
            this.calcHashesMenuItem.Text = "Calc Hashes";
            this.calcHashesMenuItem.Click += new System.EventHandler(this.CalcHashesMenuItemClick);
            // 
            // seperatorMenuItem2
            // 
            this.seperatorMenuItem2.Index = 2;
            this.seperatorMenuItem2.Text = "-";
            // 
            // pruneMenuItem
            // 
            this.pruneMenuItem.Index = 3;
            this.pruneMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.uniqueFileNamesMenuItem,
            this.uniqueFileSizesMenuItem,
            this.uniqueFileHashesMenuItem});
            this.pruneMenuItem.Text = "Prune";
            // 
            // uniqueFileNamesMenuItem
            // 
            this.uniqueFileNamesMenuItem.Index = 0;
            this.uniqueFileNamesMenuItem.Text = "Unique File Names";
            this.uniqueFileNamesMenuItem.Click += new System.EventHandler(this.UniqueFileNamesMenuItemClick);
            // 
            // uniqueFileSizesMenuItem
            // 
            this.uniqueFileSizesMenuItem.Index = 1;
            this.uniqueFileSizesMenuItem.Text = "Unique Files Sizes";
            this.uniqueFileSizesMenuItem.Click += new System.EventHandler(this.UniqueFileSizesMenuItemClick);
            // 
            // uniqueFileHashesMenuItem
            // 
            this.uniqueFileHashesMenuItem.Index = 2;
            this.uniqueFileHashesMenuItem.Text = "Unique File Hashes";
            this.uniqueFileHashesMenuItem.Click += new System.EventHandler(this.UniqueFileHashesMenuItemClick);
            // 
            // selectMenuItem
            // 
            this.selectMenuItem.Index = 4;
            this.selectMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.pathsContainingMenuItem,
            this.namesContainingMenuItem,
            this.seperatorMenuItem3,
            this.selectAllMenuItem,
            this.deselectAllMenuItem,
            this.invertSelectionMenuItem,
            this.oneOfEachMenuItem,
            this.validateSelectionMenuItem});
            this.selectMenuItem.Text = "Select";
            // 
            // pathsContainingMenuItem
            // 
            this.pathsContainingMenuItem.Index = 0;
            this.pathsContainingMenuItem.Text = "Paths containing...";
            this.pathsContainingMenuItem.Click += new System.EventHandler(this.PathsContainingMenuItemClick);
            // 
            // namesContainingMenuItem
            // 
            this.namesContainingMenuItem.Index = 1;
            this.namesContainingMenuItem.Text = "Names containing...";
            this.namesContainingMenuItem.Click += new System.EventHandler(this.NamesContainingMenuItemClick);
            // 
            // seperatorMenuItem3
            // 
            this.seperatorMenuItem3.Index = 2;
            this.seperatorMenuItem3.Text = "-";
            // 
            // selectAllMenuItem
            // 
            this.selectAllMenuItem.Index = 3;
            this.selectAllMenuItem.Text = "Select All";
            this.selectAllMenuItem.Click += new System.EventHandler(this.SelectAllMenuItemClick);
            // 
            // deselectAllMenuItem
            // 
            this.deselectAllMenuItem.Index = 4;
            this.deselectAllMenuItem.Text = "Deselect All";
            this.deselectAllMenuItem.Click += new System.EventHandler(this.DeselectAllMenuItemClick);
            // 
            // invertSelectionMenuItem
            // 
            this.invertSelectionMenuItem.Index = 5;
            this.invertSelectionMenuItem.Text = "Invert Selection";
            this.invertSelectionMenuItem.Click += new System.EventHandler(this.InvertSelectionMenuItemClick);
            // 
            // oneOfEachMenuItem
            // 
            this.oneOfEachMenuItem.Index = 6;
            this.oneOfEachMenuItem.Text = "One of Each";
            this.oneOfEachMenuItem.Click += new System.EventHandler(this.OneOfEachMenuItemClick);
            // 
            // validateSelectionMenuItem
            // 
            this.validateSelectionMenuItem.Index = 7;
            this.validateSelectionMenuItem.Shortcut = System.Windows.Forms.Shortcut.F4;
            this.validateSelectionMenuItem.Text = "Validate Selection";
            this.validateSelectionMenuItem.Click += new System.EventHandler(this.ValidateSelectionMenuItemClick);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 5;
            this.menuItem1.Text = "-";
            // 
            // selectionMenuItem
            // 
            this.selectionMenuItem.Index = 6;
            this.selectionMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.copyMenuItem,
            this.moveMenuItem,
            this.deleteMenuItem});
            this.selectionMenuItem.Text = "Selection";
            // 
            // copyMenuItem
            // 
            this.copyMenuItem.Index = 0;
            this.copyMenuItem.Text = "Copy";
            this.copyMenuItem.Click += new System.EventHandler(this.CopyMenuItemClick);
            // 
            // moveMenuItem
            // 
            this.moveMenuItem.Index = 1;
            this.moveMenuItem.Text = "Move";
            this.moveMenuItem.Click += new System.EventHandler(this.MoveMenuItemClick);
            // 
            // deleteMenuItem
            // 
            this.deleteMenuItem.Index = 2;
            this.deleteMenuItem.Text = "Delete";
            this.deleteMenuItem.Click += new System.EventHandler(this.DeleteMenuItemClick);
            // 
            // cancelMenuItem
            // 
            this.cancelMenuItem.Index = 2;
            this.cancelMenuItem.Text = "Cancel";
            this.cancelMenuItem.Click += new System.EventHandler(this.CancelMenuItemClick);
            // 
            // listView
            // 
            this.listView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView.CheckBoxes = true;
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnSelection,
            this.columnPath,
            this.columnName,
            this.columnSize,
            this.columnHash});
            this.listView.ContextMenu = this.contextMenu;
            this.listView.FullRowSelect = true;
            this.listView.Location = new System.Drawing.Point(-1, -1);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(667, 339);
            this.listView.TabIndex = 3;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ListViewColumnClick);
            this.listView.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ListViewKeyUp);
            // 
            // columnSelection
            // 
            this.columnSelection.Text = "";
            this.columnSelection.Width = 20;
            // 
            // columnPath
            // 
            this.columnPath.Text = "Path";
            this.columnPath.Width = 100;
            // 
            // columnName
            // 
            this.columnName.Text = "Name";
            this.columnName.Width = 100;
            // 
            // columnSize
            // 
            this.columnSize.Text = "Size";
            this.columnSize.Width = 100;
            // 
            // columnHash
            // 
            this.columnHash.Text = "Hash";
            this.columnHash.Width = 100;
            // 
            // panelProgress
            // 
            this.panelProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelProgress.Controls.Add(this.progressBar);
            this.panelProgress.Location = new System.Drawing.Point(-1, -1);
            this.panelProgress.Name = "panelProgress";
            this.panelProgress.Size = new System.Drawing.Size(667, 339);
            this.panelProgress.TabIndex = 1;
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(12, 310);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(643, 23);
            this.progressBar.TabIndex = 2;
            this.progressBar.Value = 50;
            // 
            // statusBar
            // 
            this.statusBar.Location = new System.Drawing.Point(0, 337);
            this.statusBar.Name = "statusBar";
            this.statusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.progressStatusBarPanel,
            this.countStatusBarPanel});
            this.statusBar.ShowPanels = true;
            this.statusBar.Size = new System.Drawing.Size(665, 22);
            this.statusBar.TabIndex = 0;
            this.statusBar.Text = "Scan something dude!";
            // 
            // progressStatusBarPanel
            // 
            this.progressStatusBarPanel.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
            this.progressStatusBarPanel.MinWidth = 200;
            this.progressStatusBarPanel.Name = "progressStatusBarPanel";
            this.progressStatusBarPanel.Text = "Select something!";
            this.progressStatusBarPanel.Width = 548;
            // 
            // countStatusBarPanel
            // 
            this.countStatusBarPanel.MinWidth = 100;
            this.countStatusBarPanel.Name = "countStatusBarPanel";
            this.countStatusBarPanel.Text = "no files";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 359);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.panelProgress);
            this.Menu = this.mainMenu;
            this.Name = "MainForm";
            this.Text = "Same Thing Different File";
            this.panelProgress.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.progressStatusBarPanel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.countStatusBarPanel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenu contextMenu;
        private System.Windows.Forms.MenuItem openFolderMenuItem;
        private System.Windows.Forms.MenuItem openFileMenuItem;
        private System.Windows.Forms.MainMenu mainMenu;
        private System.Windows.Forms.MenuItem fileMenuItem;
        private System.Windows.Forms.MenuItem newMenuItem;
        private System.Windows.Forms.MenuItem openMenuItem;
        private System.Windows.Forms.MenuItem saveMenuItem;
        private System.Windows.Forms.MenuItem seperatorMenuItem1;
        private System.Windows.Forms.MenuItem exitMenuItem;
        private System.Windows.Forms.MenuItem actionMenuItem;
        private System.Windows.Forms.MenuItem scanMenuItem;
        private System.Windows.Forms.MenuItem calcHashesMenuItem;
        private System.Windows.Forms.MenuItem seperatorMenuItem2;
        private System.Windows.Forms.MenuItem pruneMenuItem;
        private System.Windows.Forms.MenuItem uniqueFileNamesMenuItem;
        private System.Windows.Forms.MenuItem uniqueFileSizesMenuItem;
        private System.Windows.Forms.MenuItem uniqueFileHashesMenuItem;
        private System.Windows.Forms.MenuItem selectMenuItem;
        private System.Windows.Forms.MenuItem pathsContainingMenuItem;
        private System.Windows.Forms.MenuItem namesContainingMenuItem;
        private System.Windows.Forms.MenuItem seperatorMenuItem3;
        private System.Windows.Forms.MenuItem selectAllMenuItem;
        private System.Windows.Forms.MenuItem deselectAllMenuItem;
        private System.Windows.Forms.MenuItem invertSelectionMenuItem;
        private System.Windows.Forms.MenuItem oneOfEachMenuItem;
        private System.Windows.Forms.MenuItem validateSelectionMenuItem;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem selectionMenuItem;
        private System.Windows.Forms.MenuItem copyMenuItem;
        private System.Windows.Forms.MenuItem moveMenuItem;
        private System.Windows.Forms.MenuItem deleteMenuItem;
        private System.Windows.Forms.MenuItem cancelMenuItem;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ColumnHeader columnSelection;
        private System.Windows.Forms.ColumnHeader columnPath;
        private System.Windows.Forms.ColumnHeader columnName;
        private System.Windows.Forms.ColumnHeader columnSize;
        private System.Windows.Forms.ColumnHeader columnHash;
        private System.Windows.Forms.Panel panelProgress;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.StatusBar statusBar;
        private System.Windows.Forms.StatusBarPanel progressStatusBarPanel;
        private System.Windows.Forms.StatusBarPanel countStatusBarPanel;
    }
}

