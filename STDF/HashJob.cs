using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Windows.Forms;

namespace STDF
{
    class HashJob
    {
        private string _filename;
        private ListView _listView;
        private ListViewItem _listViewItem;
        private MainForm.UpdateListItem _updateListItem;
        private EventWaitHandle _done;
        private bool _cancel;

        static WaitCallback _callback = new WaitCallback(CalcHash);

        /// <summary>
        /// Queues a HashJob on the ThreadPool.
        /// </summary>
        /// <param name="filename">the full path to the file to calculate the hash for</param>
        /// <param name="listViewItem">the item to update with the hash when complete</param>
        /// <param name="listView">the listview that contains the item</param>
        /// <param name="updateListItem">the delegate that will update the listviewitem with the hash</param>
        /// <returns>A HashJob object representing the queued HashJob</returns>
        public static HashJob Queue(string filename, ListViewItem listViewItem, ListView listView, MainForm.UpdateListItem updateListItem)
        {
            HashJob hj = new HashJob();
            hj._filename = filename;
            hj._listViewItem = listViewItem;
            hj._listView = listView;
            hj._updateListItem = updateListItem;
            hj._cancel = false;
            hj._done = new ManualResetEvent(false);
            
            ThreadPool.QueueUserWorkItem(_callback, hj);
            return hj;
        }

        /// <summary>
        /// Cancels the HashJob regardless of whether it has already completed or not.
        /// </summary>
        public void Cancel()
        {
            lock(this)
            {
                _cancel = true;
            }
        }

        /// <summary>
        /// Indicates if the HashJob has been cancelled.
        /// </summary>
        public bool IsCancelled()
        {
            bool result = false;
            lock(this)
            {
                result = _cancel;
            }
            return result;
        }

        /// <summary>
        /// Indicates if the HashJob has completed.
        /// </summary>
        public bool IsDone()
        {
            return _done.WaitOne(1);
        }

        /// <summary>
        /// Called on a ThreadPool thread to actually calculate the hash. 
        /// 
        /// BE CAREFUL NOT TO UPDATE ANY UI DIRECTLY FROM THIS FUNCTION.
        /// </summary>
        /// <param name="state">The HashJob associated with this invocation.</param>
        private static void CalcHash(object state)
        {
            string hash = "-Cannot Compute-";
            HashJob hj = state as HashJob;

            try
            {
                if (!hj.IsCancelled())
                {
                    HashAlgorithm ha = HashAlgorithm.Create();
                    if (!hj.IsCancelled())
                    {
                        FileStream fs = new FileStream(hj._filename, FileMode.Open, FileAccess.Read);
                        if (!hj.IsCancelled())
                        {
                            byte[] hashBytes = ha.ComputeHash(fs);
                            fs.Close();
                            hash = BitConverter.ToString(hashBytes);
                        }
                    }
                }
            }
            catch
            {
            }
            
            // Since UI objects can only be updated on the UI thread, lets invoke a function that updates them there instead.
            hj._listView.Invoke(hj._updateListItem, new Object[] {hj._listViewItem, hash });
            hj._done.Set();
        }
    }
}
