using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace project_cuoi_ky
{
    public class ToastManager
    {
        private readonly Form _parentForm;
        private readonly List<ToastNotification> _activeToasts;
        private readonly int _toastSpacing = 10;
        private readonly int _marginFromEdge = 20;
        
        public event EventHandler<string> ToastClicked;
        
        public ToastManager(Form parentForm)
        {
            _parentForm = parentForm;
            _activeToasts = new List<ToastNotification>();
        }
        
        public void ShowToast(string title, string message, string chatroomId = null)
        {
            // Ensure we're on the UI thread
            if (_parentForm.InvokeRequired)
            {
                _parentForm.Invoke(new Action(() => ShowToast(title, message, chatroomId)));
                return;
            }
            
            // Create new toast
            var toast = new ToastNotification(title, message, chatroomId);
            toast.ToastClicked += OnToastClicked;
            
            // Add to parent form
            _parentForm.Controls.Add(toast);
            toast.BringToFront();
            
            // Position the toast
            PositionToast(toast);
            
            // Add to active toasts list
            _activeToasts.Add(toast);
            
            // Show the toast
            toast.ShowToast();
            
            // Clean up when toast is disposed
            toast.Disposed += (s, e) => RemoveToast(toast);
        }
        
        private void PositionToast(ToastNotification toast)
        {
            // Position in bottom-right corner
            int x = _parentForm.ClientSize.Width - toast.Width - _marginFromEdge;
            int y = _parentForm.ClientSize.Height - toast.Height - _marginFromEdge;
            
            // Stack toasts vertically if there are multiple
            foreach (var existingToast in _activeToasts.Where(t => t.Visible))
            {
                y -= (toast.Height + _toastSpacing);
            }
            
            toast.Location = new Point(x, y);
        }
        
        private void OnToastClicked(object sender, string chatroomId)
        {
            ToastClicked?.Invoke(this, chatroomId);
        }
        
        private void RemoveToast(ToastNotification toast)
        {
            if (_activeToasts.Contains(toast))
            {
                _activeToasts.Remove(toast);
                RepositionToasts();
            }
        }
        
        private void RepositionToasts()
        {
            if (_parentForm.InvokeRequired)
            {
                _parentForm.Invoke(new Action(RepositionToasts));
                return;
            }
            
            int baseY = _parentForm.ClientSize.Height - _marginFromEdge;
            int x = _parentForm.ClientSize.Width - _marginFromEdge;
            
            // Reposition remaining toasts from bottom up
            for (int i = _activeToasts.Count - 1; i >= 0; i--)
            {
                var toast = _activeToasts[i];
                if (toast.Visible && !toast.IsDisposed)
                {
                    baseY -= toast.Height;
                    toast.Location = new Point(x - toast.Width, baseY);
                    baseY -= _toastSpacing;
                }
            }
        }
        
        public void ClearAllToasts()
        {
            var toastsToRemove = _activeToasts.ToList();
            foreach (var toast in toastsToRemove)
            {
                if (!toast.IsDisposed)
                {
                    toast.Dispose();
                }
            }
            _activeToasts.Clear();
        }
    }
}
