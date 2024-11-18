using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using UDSH.MVVM;

namespace UDSH.ViewModel
{
    internal class HeaderUserControlViewModel : ViewModelBase
    {
        /*
         * Important Note: the toggle button doesn't work as expected when using Left Mouse Button Down.
         * When pressing the toggle button, the IsPenToolButtonClicked gets changed multiple times instead
         * of once.
         * 
         * Expected: False --> {Mouse Pressed} --> True
         * What happens: False --> {Mouse Pressed} --> True --> False
         * 
         * Possible Causes: the boolean changes when an event happens and maybe because the popup "StaysOpen"
         * is equal to "False". What ever happens, there's a "Race" between events that needs to be solved.
         * 
         * The problem needs more investigation. I'm going to leave it as it is, BUT COME BACK TO IT AND SOLVE IT. DO NOT BE LAZY FOR THE LOVE OF GOD!!!!!
         * 
         * UPDATE: I've swapped toggle button with button(I prefer toggle button as it makes more sense).
         * It works, but i'm using losing focus, is it the best solution? of course not, but for now, i
         * will stick with this solution. Also, the "staysOpen" theory is debunked, it doesn't effect the call.
         */

        private bool isPenToolButtonClicked;
        public bool IsPenToolButtonClicked
        {
            get { return isPenToolButtonClicked; }
            set { isPenToolButtonClicked = value; OnPropertyChanged(); }
        }

        private bool canClosePopup;
        public bool CanClosePopup
        {
            get { return canClosePopup; }
            set { canClosePopup = value; OnPropertyChanged(); }
        }

        private List<int> quickActionsList;

        private RelayCommand<Button> quickActionButton1;
        public RelayCommand<Button> QuickActionButton1
        {
            get { return quickActionButton1; }
            set { quickActionButton1 = value; OnPropertyChanged(); }
        }

        // We don't need focus
        public RelayCommand<Button> PenToolLeftMouseButtonDown => new RelayCommand<Button>(execute => ClosePenTool());
        public RelayCommand<Button> PenToolButtonFocus => new RelayCommand<Button>(execute => OnPenToolLoseFocus());
        public RelayCommand<Button> NewFile => new RelayCommand<Button>(execute => CreateNewFile());

        public RelayCommand<string> QNewFile => new RelayCommand<string>(SetQuickAction);

        public HeaderUserControlViewModel()
        {
            IsPenToolButtonClicked = false;
            CanClosePopup = false;
        }

        // TODO: doesn't work, try two commands, to set true or false.
        private void ClosePenTool()
        {
            IsPenToolButtonClicked = !IsPenToolButtonClicked;
            CanClosePopup = IsPenToolButtonClicked;
        }

        private void OnPenToolLoseFocus()
        {
            IsPenToolButtonClicked = false;
        }

        // Remove if PenToolFocus not used
        private bool CanClose()
        {
            return IsPenToolButtonClicked;
        }

        private void CreateNewFile()
        {
            IsPenToolButtonClicked = false;
            CanClosePopup = false;
            MessageBox.Show("Create a new file...");
        }

        private void SetQuickAction(string index)
        {
            int CurrentIndex = Int32.Parse(index);
            if (CurrentIndex == 0)
            {
                QuickActionButton1 = NewFile;
            }
        }

        /*
         * TODO List
         * 
         * Pen Tool
         * 1- Change Pen Tool toggle style
         *  - Change background. [NO NEED]
         *  - Image rotation animation. [DONE]
         * 
         * 2- there's extra stack panel, test if you can remove it, by updating mouse detection.
         *    Also, you can use this stack panel to add sub-popups.[KEEPING IT]
         * 
         * 3- update popups animation, there're build-in ones, test them first![DONE]
         * 
         * 4- do each button commands for the pen tool.[IN-PROGRESS]
         * 
         * {Quick Save}
         * - add a circular button, so when the user add a new quick button, it gets added automatically. Maybe add Control button.[IN-PROGRESS]
         * 
         * {Quick Save Logic}
         * - I will use a list to determine how many quick items we have. When adding a quick item,
         * we first check which index we stopped at, if we didn't exceed it then, we assign the
         * command to the quick action button. We also need to update the image, we can do that
         * in the image styles files through data triggers. Don't forget to animate the items
         * that will get affected by this process. (PenTool Lost Focus fires when assigning quick buttons, adding control input can help us)
         * 
         * 
         * P.S. Do the bindings before commiting to the toggle button style, so you make sure
         * everything is working. Also, the popup only closes when pressing the button, so
         * try losing focus.
        */
    }
}
